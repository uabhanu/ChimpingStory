#pragma strict

enum PlayerStateJS { Enabled, Disabled };
enum PlayerStatusJS { Idle, MovinUp, MovingDown, Sinking, Crashed };
enum PlayerVulnerabilityJS { Enabled, Disabled, Shielded };
enum PowerupUsageJS { Enabled, Disabled };

public class PlayerManagerJS extends MonoBehaviour
{
    public var levelManager				: LevelManagerJS;				//A link to the Level Manager

    public var subTextures				: Sprite[];						//The array containing the sub and sub damaged textures
    public var subRenderer				: SpriteRenderer;				//A link to the sub material

    public var extraSpeedFront			: Transform;					//The extra speed front sprite
    public var extraSpeedTail			: Transform;					//The extra speed trail sprite
    public var shield					: Transform;					//The shield sprite

    public var minDepth					: float;						//Minimum depth
    public var maxDepth					: float;						//Maximum depth
    public var maxRotation				: float;						//The maximum rotation of the submarine

    public var maxVerticalSpeed			: float;						//The maximum vertical speed
    public var depthEdge				: float;						//The edge fo the smoothing zone (minDepth- depthEdge and maxDepth - depthEdge)

    public var smoke					: ParticleSystem;				//The smoke particle
    public var bubbles					: ParticleSystem;				//The submarine bubble particle system
    public var reviveParticle			: ParticleSystem;				//The revive particle

    public var normalCollider			: PolygonCollider2D;			//The normal collider of the submarine
    public var shieldCollider			: CircleCollider2D;				//The collider of the shield

    private var playerStatus			: PlayerStatusJS;
    private var playerState				: PlayerStateJS;
    private var playerVulnerability		: PlayerVulnerabilityJS;
    private var powerupUsage			: PowerupUsageJS;

	private var currentSkinID			: int;

    private var speed					: float;						//The actual vertical speed of the submarine
    private var newSpeed				: float;						//The new speed of the submarine, used at the edges

    private var rotationDiv				: float;						//A variable used to calculate rotation
    private var newRotation				: Vector3;						//Stores the new rotation angles
	
    private var distanceToMax			: float;						//The current distance to the maximum depth
    private var distanceToMin			: float;						//The current distance to the minimum depth

    private var startingPos				: Vector2;						//Holds the starting position of the submarine

    //Used for initialization
    function Start()
    {
        newRotation = new Vector3();
        startingPos = this.transform.position;

        playerStatus = PlayerStatusJS.Idle;
        playerState = PlayerStateJS.Disabled;
        playerVulnerability = PlayerVulnerabilityJS.Disabled;
        powerupUsage = PowerupUsageJS.Disabled;

        rotationDiv = maxVerticalSpeed / maxRotation;
        
        currentSkinID = SaveManagerJS.currentSkinID;
		subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
    }
    //Called at every frame
    function Update()
    {
        if (playerState == PlayerStateJS.Enabled)
        {
            if (playerStatus == PlayerStatusJS.MovingDown || playerStatus == PlayerStatusJS.MovinUp)
            {
                //Calculate smooth zone distance
                CalculateDistances();

                //Calculate player movement
                CalculateMovement();

                //Move and rotate the submarine
                MoveAndRotate();
            }
            else if (playerStatus == PlayerStatusJS.Sinking)
            {
                Sink();
            }
        }
    }
    //Called when the player enters a triggerer zone
    function OnTriggerEnter2D(other : Collider2D)
    {
        //If the submarine is collided with a coin
        if (other.tag == "Coin")
        {
            //Notify the level manager, and disable the coin's renderer and collider
            levelManager.CoinCollected(other.transform.position);
            other.GetComponent.<Renderer>().enabled = false;
            other.GetComponent.<Collider2D>().enabled = false;
            AudioManagerJS.Instance().PlayCoinCollected();
        }
        //If the submarine is collided with an obstacle
        else if (other.tag == "Obstacle")
        {
            //Notify the level manager, and disable the obstacle's renderer and collider
            levelManager.Collision(other.name, other.transform.position);
            other.GetComponent.<Renderer>().enabled = false;
            other.GetComponent.<Collider2D>().enabled = false;

			AudioManagerJS.Instance().PlayExplosion();

            //If the obstacle is a torpedo, disable it's child as well
            if (other.name == "Torpedo")
                other.transform.Find("TorpedoFire").gameObject.SetActive(false);

            //If the player is vulnerable
            if (playerVulnerability == PlayerVulnerabilityJS.Enabled)
            {
                //Sink the submarine
                powerupUsage = PowerupUsageJS.Disabled;
                playerVulnerability = PlayerVulnerabilityJS.Disabled;
                playerStatus = PlayerStatusJS.Sinking;

                subRenderer.sprite = subTextures[currentSkinID * 2];
                bubbles.Stop();
            }
            //If the player is shielded, collapse it
            else if (playerVulnerability == PlayerVulnerabilityJS.Shielded)
            {
                CollapseShield();
            }
        }
        //If the submarine is collided with a powerup
        else if (other.tag == "Powerup")
        {
            //Notify the level manager, and disable the powerup's components
            other.GetComponent.<Renderer>().enabled = false;
            other.GetComponent.<Collider2D>().enabled = false;
            other.transform.Find("Trail").gameObject.SetActive(false);

			AudioManagerJS.Instance().PlayPowerupCollected();
            levelManager.PowerupPickup(other.name);
        }
    }

    //Enables the submarine
	public function EnableSubmarine()
    {
        playerStatus = PlayerStatusJS.Idle;
        playerState = PlayerStateJS.Enabled;
        playerVulnerability = PlayerVulnerabilityJS.Enabled;
        powerupUsage = PowerupUsageJS.Enabled;

        subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
        bubbles.Play();

        StartCoroutine(FunctionLibraryJS.MoveElementBy(this.transform, new Vector2(0.4f, 0.2f), 0.5f));
	}
    //Sets the pause state of the submarine
    public function SetPauseState(pauseState : boolean)
    {
        if (pauseState)
        {
            playerState = PlayerStateJS.Disabled;
            bubbles.Pause();
        }
        else
        {
            playerState = PlayerStateJS.Enabled;
            bubbles.Play();
        }
    }
    //Changes the active skin ID
	public function ChangeSkin(id : int)
	{
		currentSkinID = id;

		if (playerStatus != PlayerStatusJS.Crashed)
			subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
	}
    //Resets the submarine
	public function Reset()
    {
        playerStatus = PlayerStatusJS.Idle;
        playerState = PlayerStateJS.Disabled;
        playerVulnerability = PlayerVulnerabilityJS.Disabled;
        powerupUsage = PowerupUsageJS.Disabled;

        newRotation = new Vector3(0, 0, 0);

        subRenderer.sprite = subTextures[currentSkinID * 2 + 1];

        bubbles.Stop();
        bubbles.Clear();

        this.transform.position = startingPos;
        this.transform.eulerAngles = newRotation;
	}
    //Revives the submarine
	public function Revive()
    {
        StartCoroutine(ReviveProcess());
	}
    //Updates player input
	public function UpdateInput(inputActive : boolean)
    {
        if (playerStatus == PlayerStatusJS.Sinking || playerStatus == PlayerStatusJS.Crashed)
            return;

        if (playerStatus == PlayerStatusJS.Idle || playerStatus == PlayerStatusJS.MovingDown || inputActive)
            playerStatus = PlayerStatusJS.MovinUp;
        else if (playerStatus == PlayerStatusJS.MovinUp)
            playerStatus = PlayerStatusJS.MovingDown;
	}
    //Disables the crash smoke particle
    public function DisableSmoke()
    {
        smoke.enableEmission = false;
    }

    //Activates the extra speed submarine effects
    public function ActivateExtraSpeed()
    {
        extraSpeedFront.gameObject.SetActive(true);
        extraSpeedTail.gameObject.SetActive(true);
        RaiseShield();

        playerVulnerability = PlayerVulnerabilityJS.Disabled;
        powerupUsage = PowerupUsageJS.Disabled;
    }
    //Deactivates the extra speed submarine effects
    public function DisableExtraSpeed()
    {
        extraSpeedFront.gameObject.SetActive(false);
        extraSpeedTail.gameObject.SetActive(false);
        CollapseShield();

        powerupUsage = PowerupUsageJS.Enabled;
    }
    //Raises the shield
    public function RaiseShield()
    {
        playerVulnerability = PlayerVulnerabilityJS.Shielded;

        normalCollider.enabled = false;
        shieldCollider.enabled = true;

        StartCoroutine(FunctionLibraryJS.ScaleTo(shield, new Vector2(1, 1), 0.25f));
    }
    //Collapses the shield
    public function CollapseShield()
    {
        playerVulnerability = PlayerVulnerabilityJS.Disabled;

        normalCollider.enabled = true;
        shieldCollider.enabled = false;

        StartCoroutine(FunctionLibraryJS.ScaleTo(shield, new Vector2(0, 0), 0.25f));
        StartCoroutine(EnableVulnerability(0.3f));
    }
    //Returns true, if a powerup can be activated
    public function CanUsePowerup()
    {
        return playerState == PlayerStateJS.Enabled && powerupUsage == PowerupUsageJS.Enabled;
    }

    //Calculate distances to minDepth and maxDepth
    private function CalculateDistances()
    {
        distanceToMax = this.transform.position.y - maxDepth;
        distanceToMin = minDepth - this.transform.position.y;
    }
    //Calculate movement based on input
    private function CalculateMovement()
    {
        //If the sub is moving up
        if (playerStatus == PlayerStatusJS.MovinUp)
        {
            //Increase speed
            speed += Time.deltaTime * maxVerticalSpeed;

            //If the sub is too close to the minDepth
            if (distanceToMin < depthEdge)
            {
                //Calculate maximum speed at this depth (without this, the sub would leave the gameplay are)
                newSpeed = maxVerticalSpeed * (minDepth - this.transform.position.y) / depthEdge;

                //If the newSpeed is lesser the the current speed
                if (newSpeed < speed)
                    //Make newSpeed the current speed
                    speed = newSpeed;
            }
            //If the sub is too close to the maxDepth
            else if (distanceToMax < depthEdge)
            {
                //Calculate maximum speed at this depth (without this, the sub would leave the gameplay are)
                newSpeed = maxVerticalSpeed * (maxDepth - this.transform.position.y) / depthEdge;

                //If the newSpeed is greater the the current speed
                if (newSpeed > speed)
                    //Make newSpeed the current speed
                    speed = newSpeed;
            }
        }
        //If the sub is moving down
        else
        {
            //Decrease speed
            speed -= Time.deltaTime * maxVerticalSpeed;

            //If the sub is too close to the maxDepth
            if (distanceToMax < depthEdge)
            {
                //Calculate maximum speed at this depth (without this, the sub would leave the gameplay are)
                newSpeed = maxVerticalSpeed * (maxDepth - this.transform.position.y) / depthEdge;

                //If the newSpeed is greater the the current speed
                if (newSpeed > speed)
                    //Make newSpeed the current speed
                    speed = newSpeed;
            }
            //If the sub is too close to the minDepth
            else if (distanceToMin < depthEdge)
            {
                //Calculate maximum speed at this depth (without this, the sub would leave the gameplay are)
                newSpeed = maxVerticalSpeed * (minDepth - this.transform.position.y) / depthEdge;

                //If the newSpeed is lesser the the current speed
                if (newSpeed < speed)
                    //Make newSpeed the current speed
                    speed = newSpeed;
            }
        }
    }
    //Move and rotate the submarine based on speed
    private function MoveAndRotate()
    {
        //Calculate new rotation
        newRotation.z = speed / rotationDiv;

        //Apply new rotation and position
        this.transform.eulerAngles = newRotation;
        this.transform.position += Vector3.up * speed * Time.deltaTime;
    }
    //Sinks the submarine until it crashes to the sand
    private function Sink()
    {
        var crashDepth : float = maxDepth - 0.8f;
        var crashDepthEdge : float = 0.5f;

        var distance : float = this.transform.position.y - crashDepth;

        //If the sub is too close to minDepth
        if (distanceToMin < depthEdge)
        {
            //Calculate maximum speed at this depth (without this, the sub would leave the gameplay are)
            newSpeed = maxVerticalSpeed * (minDepth - this.transform.position.y) / depthEdge;

            //If the newSpeed is greater the the current speed
            if (newSpeed < speed)
                //Make newSpeed the current speed
                speed = newSpeed;
        }
        //If the distance to the sand is greater than 0.1
        if (distance > 0.1f)
        {
            //Reduce speed
            speed -= Time.deltaTime * maxVerticalSpeed * 0.6f;

            //If the distance to the sand smaller than the crashDepthEdge
            if (distance < crashDepthEdge)
            {
                //Calculate new speed for impact
                newSpeed = maxVerticalSpeed * (crashDepth - this.transform.position.y) / crashDepthEdge;

                //If newSpeed is greater than speed
                if (newSpeed > speed)
                    //Apply new speed to speed
                    speed = newSpeed;
            }

            //Apply the above to the submarine
            MoveAndRotate();

            //If distance to sand smaller than 0.2
            if (distance < 0.25f)
                //Enable smoke emission
                smoke.enableEmission = true;
        }
        //If the distance to the sand is smaller than 0.1
        else
        {
            //Disable this function from calling, and stop the level
            playerStatus = PlayerStatusJS.Crashed;
            levelManager.StopLevel();

            //Disable the smoke
            StartCoroutine(FunctionLibraryJS.CallWithDelay(DisableSmoke, 2));
        }
    }

    //Enables player vulnerability after time
    private function EnableVulnerability(time : float)
    {
        //Declare variables, get the starting position, and move the object
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        while (i < 1.0)
        {
            //If the game is not paused, increase t
            if (playerState == PlayerStateJS.Enabled)
                i += Time.deltaTime * rate;

            //Wait for the end of frame
            yield;
        }

        playerVulnerability = PlayerVulnerabilityJS.Enabled;
    }
    //Revives the player, and moves the submarine upward
    private function ReviveProcess()
    {
        //Change the texture to intact, and play the revive particle
        subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
        reviveParticle.Play();

        //Reset rotation, and move the submarine up
        newRotation = new Vector3(0, 0, 0);
        this.transform.eulerAngles = newRotation;
        StartCoroutine(FunctionLibraryJS.MoveElementBy(this.transform, new Vector2(0, Mathf.Abs(this.transform.position.y - maxDepth)), 0.5f));

        yield WaitForSeconds(0.5f);

        //Reset states
        playerStatus = PlayerStatusJS.Idle;
        playerState = PlayerStateJS.Enabled;
        playerVulnerability = PlayerVulnerabilityJS.Enabled;

        bubbles.Play();

        yield WaitForSeconds(0.5f);
        powerupUsage = PowerupUsageJS.Enabled;
        reviveParticle.Clear();
    }
}