using UnityEngine;
using System.Collections;

enum PlayerState { Enabled, Disabled };
enum PlayerStatus { Idle, MovinUp, MovingDown, Sinking, Crashed };
enum PlayerVulnerability { Enabled, Disabled, Shielded };
enum PowerupUsage { Enabled, Disabled };

public class PlayerManager : MonoBehaviour
{
	SoundManager m_soundsContainer;

    public LevelManager levelManager;                                   //A link to the Level Manager

    public Sprite[] subTextures;										//The array containing the sub and sub damaged textures
    public SpriteRenderer subRenderer;									//A link to the sub material

    public Transform extraSpeedFront;                                   //The extra speed front sprite
    public Transform extraSpeedTail;                                    //The extra speed trail sprite
    public Transform shield;                                            //The shield sprite

    public float minDepth;                                              //Minimum depth
    public float maxDepth;						                        //Maximum depth
    public float maxRotation;						                    //The maximum rotation of the submarine

    public float maxVerticalSpeed;					                    //The maximum vertical speed
    public float depthEdge;					                            //The edge fo the smoothing zone (minDepth- depthEdge and maxDepth - depthEdge)

    public ParticleSystem smoke;										//The smoke particle
    public ParticleSystem bubbles;										//The submarine bubble particle system
    public ParticleSystem reviveParticle;								//The revive particle

    public PolygonCollider2D normalCollider;                            //The normal collider of the submarine
    public CircleCollider2D shieldCollider;                             //The collider of the shield

    private PlayerStatus playerStatus;
    private PlayerState playerState;
    private PlayerVulnerability playerVulnerability;
    private PowerupUsage powerupUsage;

	private int currentSkinID;

    private float speed;                                                //The actual vertical speed of the submarine
    private float newSpeed;						                        //The new speed of the submarine, used at the edges

    private float rotationDiv;										    //A variable used to calculate rotation
    private Vector3 newRotation;	                                    //Stores the new rotation angles

    private float distanceToMax;                                        //The current distance to the maximum depth
    private float distanceToMin;                                        //The current distance to the minimum depth

    private Vector2 startingPos;                                        //Holds the starting position of the submarine

    //Used for initialization
    void Start()
    {
        newRotation = new Vector3();
        startingPos = transform.position;

        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Disabled;
        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;

        rotationDiv = maxVerticalSpeed / maxRotation;

		currentSkinID = SaveManager.currentSkinID;
		subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
		m_soundsContainer = FindObjectOfType<SoundManager>();
    }
    //Called at every frame
    void Update()
    {
        if(playerState == PlayerState.Enabled)
        {
            if(playerStatus == PlayerStatus.MovingDown || playerStatus == PlayerStatus.MovinUp)
            {
                //Calculate smooth zone distance
                CalculateDistances();

                //Calculate player movement
                CalculateMovement();

                //Move and rotate the submarine
                MoveAndRotate();
            }
            else if(playerStatus == PlayerStatus.Sinking)
            {
                Sink();
            }
        }
    }
    //Called when the player enters a triggerer zone
    void OnTriggerEnter2D(Collider2D other)
    {
        //If the submarine is collided with a coin
        if(other.tag == "Coin")
        {
            //Notify the level manager, and disable the coin's renderer and collider
            ScoreManager.m_scoreValue += 5;
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;

			AudioManager.Instance.PlayCoinCollected();
        }
        //If the submarine is collided with an obstacle
        else if(other.tag == "Obstacle")
        {
            //Notify the level manager, and disable the obstacle's renderer and collider
            levelManager.Collision(other.name, other.transform.position);
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;

			AudioManager.Instance.PlayExplosion();

            //If the obstacle is a torpedo, disable it's child as well
            if(other.name == "Torpedo")
                other.transform.Find("TorpedoFire").gameObject.SetActive(false);

            //If the player is vulnerable
            if(playerVulnerability == PlayerVulnerability.Enabled)
            {
                //Sink the submarine
                powerupUsage = PowerupUsage.Disabled;
                playerVulnerability = PlayerVulnerability.Disabled;
                playerStatus = PlayerStatus.Sinking;

				subRenderer.sprite = subTextures[currentSkinID * 2];
                bubbles.Stop();
            }
            //If the player is shielded, collapse it
            else if(playerVulnerability == PlayerVulnerability.Shielded)
            {
                CollapseShield();
            }
        }
        //If the submarine is collided with a powerup
        else if(other.tag == "Super")
        {
            //Notify the level manager, and disable the powerup's components
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;
            other.transform.Find("Trail").gameObject.SetActive(false);
			ScoreManager.m_supersCount++;
			BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
			m_soundsContainer.m_soundsSource.clip = m_soundsContainer.m_superCollected;
			m_soundsContainer.m_soundsSource.Play();
            //levelManager.PowerupPickup(other.name);
        }
    }

    //Enables the submarine
	public void EnableSubmarine()
    {
        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Enabled;
        playerVulnerability = PlayerVulnerability.Enabled;
        powerupUsage = PowerupUsage.Enabled;

		subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
        bubbles.Play();

        StartCoroutine(FunctionLibrary.MoveElementBy(this.transform, new Vector2(0.4f, 0.2f), 0.5f));
	}
    //Sets the pause state of the submarine
    public void SetPauseState(bool pauseState)
    {
        if(pauseState)
        {
            playerState = PlayerState.Disabled;
            bubbles.Pause();
        }
        else
        {
            playerState = PlayerState.Enabled;
            bubbles.Play();
        }
    }
	//Changes the active skin ID
	public void ChangeSkin(int id)
	{
		currentSkinID = id;

		if (playerStatus != PlayerStatus.Crashed)
			subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
	}
    //Resets the submarine
	public void Reset()
    {
        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Disabled;
        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;

        newRotation = new Vector3(0, 0, 0);

		subRenderer.sprite = subTextures[currentSkinID * 2 + 1];

        bubbles.Stop();
        bubbles.Clear();

        transform.position = startingPos;
        transform.eulerAngles = newRotation;
	}
    //Revives the submarine
	public void Revive()
    {
        StartCoroutine("ReviveProcess");
	}
    //Updates player input
	public void UpdateInput(bool inputActive)
    {
        if (playerStatus == PlayerStatus.Sinking || playerStatus == PlayerStatus.Crashed)
            return;

        if (playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.MovingDown || inputActive)
            playerStatus = PlayerStatus.MovinUp;
        else if (playerStatus == PlayerStatus.MovinUp)
            playerStatus = PlayerStatus.MovingDown;
	}
    //Disables the crash smoke particle
    public void DisableSmoke()
    {
        smoke.enableEmission = false;
    }

    //Activates the extra speed submarine effects
    public void ActivateExtraSpeed()
    {
        extraSpeedFront.gameObject.SetActive(true);
        extraSpeedTail.gameObject.SetActive(true);
        RaiseShield();

        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;
    }
    //Deactivates the extra speed submarine effects
    public void DisableExtraSpeed()
    {
        extraSpeedFront.gameObject.SetActive(false);
        extraSpeedTail.gameObject.SetActive(false);
        CollapseShield();

        powerupUsage = PowerupUsage.Enabled;
    }
    //Raises the shield
    public void RaiseShield()
    {
        playerVulnerability = PlayerVulnerability.Shielded;

        normalCollider.enabled = false;
        shieldCollider.enabled = true;

        StartCoroutine(FunctionLibrary.ScaleTo(shield, new Vector2(1, 1), 0.25f));
    }
    //Collapses the shield
    public void CollapseShield()
    {
        playerVulnerability = PlayerVulnerability.Disabled;

        normalCollider.enabled = true;
        shieldCollider.enabled = false;

        StartCoroutine(FunctionLibrary.ScaleTo(shield, new Vector2(0, 0), 0.25f));
        StartCoroutine(EnableVulnerability(0.3f));
    }
    //Returns true, if a powerup can be activated
    public bool CanUsePowerup()
    {
        return playerState == PlayerState.Enabled && powerupUsage == PowerupUsage.Enabled;
    }

    //Calculate distances to minDepth and maxDepth
    private void CalculateDistances()
    {
        distanceToMax = this.transform.position.y - maxDepth;
        distanceToMin = minDepth - this.transform.position.y;
    }
    //Calculate movement based on input
    private void CalculateMovement()
    {
        //If the sub is moving up
        if (playerStatus == PlayerStatus.MovinUp)
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
    private void MoveAndRotate()
    {
        //Calculate new rotation
        newRotation.z = speed / rotationDiv;

        //Apply new rotation and position
        this.transform.eulerAngles = newRotation;
        this.transform.position += Vector3.up * speed * Time.deltaTime;
    }
    //Sinks the submarine until it crashes to the sand
    private void Sink()
    {
        float crashDepth = maxDepth - 0.8f;
        float crashDepthEdge = 0.5f;

        float distance = transform.position.y - crashDepth;

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
            playerStatus = PlayerStatus.Crashed;
            levelManager.StopLevel();

            //Disable the smoke
            StartCoroutine(FunctionLibrary.CallWithDelay(DisableSmoke, 2));
        }
    }

    //Enables player vulnerability after time
    private IEnumerator EnableVulnerability(float time)
    {
        //Declare variables, get the starting position, and move the object
        float i = 0.0f;
        float rate = 1.0f / time;

        while (i < 1.0)
        {
            //If the game is not paused, increase t
            if (playerState == PlayerState.Enabled)
                i += Time.deltaTime * rate;

            //Wait for the end of frame
            yield return 0;
        }

        playerVulnerability = PlayerVulnerability.Enabled;
    }
    //Revives the player, and moves the submarine upward
    private IEnumerator ReviveProcess()
    {
        //Change the texture to intact, and play the revive particle
		subRenderer.sprite = subTextures[currentSkinID * 2 + 1];
        reviveParticle.Play();

        //Reset rotation, and move the submarine up
        newRotation = new Vector3(0, 0, 0);
        transform.eulerAngles = newRotation;
        StartCoroutine(FunctionLibrary.MoveElementBy(transform, new Vector2(0, Mathf.Abs(transform.position.y - maxDepth)), 0.5f));

        yield return new WaitForSeconds(0.5f);

        //Reset states
        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Enabled;
        playerVulnerability = PlayerVulnerability.Enabled;

        bubbles.Play();

        yield return new WaitForSeconds(0.5f);
        powerupUsage = PowerupUsage.Enabled;
        reviveParticle.Clear();
    }
}