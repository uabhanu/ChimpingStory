using UnityEngine;
using System.Collections;

enum PlayerState { Enabled, Disabled };
enum PlayerStatus { Idle, MovinUp, MovingDown, Sinking, Crashed };
enum PlayerVulnerability { Enabled, Disabled, Shielded };
enum PowerupUsage { Enabled, Disabled };

public class PlayerManager : MonoBehaviour
{
	GameObject m_explosionPrefab , m_explosionSystemObj;
	SoundManager m_soundManager;
    WaitForSeconds m_reviveRoutineDelay = new WaitForSeconds(0.5f);

	[SerializeField] Sprite[] m_chimpSprites;		

    public LevelManager levelManager;                                   //A link to the Level Manager
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

    //public PolygonCollider2D normalCollider;                            //The normal collider of the submarine
    //public CircleCollider2D shieldCollider;                             //The collider of the shield

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
		m_explosionPrefab = Resources.Load("PF_Explosion") as GameObject;
		m_explosionSystemObj = GameObject.FindGameObjectWithTag("Explosion");
        newRotation = new Vector3();
        startingPos = transform.position;

        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Disabled;
        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;

        rotationDiv = maxVerticalSpeed / maxRotation;

		currentSkinID = SaveManager.currentSkinID;
		subRenderer.sprite = m_chimpSprites[currentSkinID * 2 + 1];
		m_soundManager = FindObjectOfType<SoundManager>();
    }
    
    void Update()
    {
        if(playerState == PlayerState.Enabled)
        {
            if(playerStatus == PlayerStatus.MovingDown || playerStatus == PlayerStatus.MovinUp)
            {
                CalculateDistances();
                CalculateMovement();
                MoveAndRotate();
            }

            else if(playerStatus == PlayerStatus.Sinking)
            {
                Sink();
            }
        }
    }
		
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Coin")
        {
            ScoreManager.m_scoreValue += 5;
            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;
			m_soundManager.m_soundsSource.clip = m_soundManager.m_coin;
			m_soundManager.m_soundsSource.Play();
        }
        
        else if(other.tag == "Obstacle")
        {
            levelManager.Collision(other.name, other.transform.position);
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;

            if(other.name == "Torpedo")
                other.transform.Find("TorpedoFire").gameObject.SetActive(false);

            if(playerVulnerability == PlayerVulnerability.Enabled)
            {
                powerupUsage = PowerupUsage.Disabled;
                playerVulnerability = PlayerVulnerability.Disabled;
                playerStatus = PlayerStatus.Sinking;

				subRenderer.sprite = m_chimpSprites[currentSkinID * 2];
                bubbles.Stop();
            }
            
            else if(playerVulnerability == PlayerVulnerability.Shielded)
            {
                CollapseShield();
            }
        }
        
        else if(other.tag == "Super")
        {
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;
            other.transform.Find("Trail").gameObject.SetActive(false);
			ScoreManager.m_supersCount++;
			BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			m_soundManager.m_soundsSource.Play();
        }
    }
		
	public void EnableChimp()
    {
        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Enabled;
        playerVulnerability = PlayerVulnerability.Enabled;
        powerupUsage = PowerupUsage.Enabled;

		subRenderer.sprite = m_chimpSprites[currentSkinID * 2 + 1];
        bubbles.Play();

        StartCoroutine(FunctionLibrary.MoveElementBy(this.transform, new Vector2(0.4f, 0.2f), 0.5f));
	}
    
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
			subRenderer.sprite = m_chimpSprites[currentSkinID * 2 + 1];
	}
    
	public void Reset()
    {
        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Disabled;
        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;

        newRotation = new Vector3(0, 0, 0);

		subRenderer.sprite = m_chimpSprites[currentSkinID * 2 + 1];

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

    
    public void ActivateExtraSpeed()
    {
        extraSpeedFront.gameObject.SetActive(true);
        extraSpeedTail.gameObject.SetActive(true);
        RaiseShield();

        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;
    }
    
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

        //normalCollider.enabled = false;
        //shieldCollider.enabled = true;

        StartCoroutine(FunctionLibrary.ScaleTo(shield, new Vector2(1, 1), 0.25f));
    }
    
    public void CollapseShield()
    {
        playerVulnerability = PlayerVulnerability.Disabled;
        StartCoroutine(FunctionLibrary.ScaleTo(shield, new Vector2(0, 0), 0.25f));
        StartCoroutine(EnableVulnerability(0.3f));
    }
    
    public bool CanUsePowerup()
    {
        return playerState == PlayerState.Enabled && powerupUsage == PowerupUsage.Enabled;
    }
		
    private void CalculateDistances()
    {
        distanceToMax = this.transform.position.y - maxDepth;
        distanceToMin = minDepth - this.transform.position.y;
    }
    
    private void CalculateMovement()
    {
        if (playerStatus == PlayerStatus.MovinUp)
        {
            speed += Time.deltaTime * maxVerticalSpeed;

            if (distanceToMin < depthEdge)
            {
                newSpeed = maxVerticalSpeed * (minDepth - this.transform.position.y) / depthEdge;

                if (newSpeed < speed)
                    speed = newSpeed;
            }
        
            else if (distanceToMax < depthEdge)
            {
                newSpeed = maxVerticalSpeed * (maxDepth - this.transform.position.y) / depthEdge;

                if (newSpeed > speed)
                    speed = newSpeed;
            }
        }
        
        else
        {
            speed -= Time.deltaTime * maxVerticalSpeed;

            if (distanceToMax < depthEdge)
            {
                newSpeed = maxVerticalSpeed * (maxDepth - this.transform.position.y) / depthEdge;

                if (newSpeed > speed)
                    speed = newSpeed;
            }
            
            else if (distanceToMin < depthEdge)
            {
                newSpeed = maxVerticalSpeed * (minDepth - this.transform.position.y) / depthEdge;

                if (newSpeed < speed)
                    speed = newSpeed;
            }
        }
    }
    
    private void MoveAndRotate()
    {
        newRotation.z = speed / rotationDiv;
        this.transform.eulerAngles = newRotation;
        this.transform.position += Vector3.up * speed * Time.deltaTime;
    }
   
    private void Sink()
    {
        float crashDepth = maxDepth - 0.8f;
        float crashDepthEdge = 0.5f;

        float distance = transform.position.y - crashDepth;

        if (distanceToMin < depthEdge)
        {
            newSpeed = maxVerticalSpeed * (minDepth - this.transform.position.y) / depthEdge;

            if (newSpeed < speed)
                speed = newSpeed;
        }
        
        if (distance > 0.1f)
        {
            speed -= Time.deltaTime * maxVerticalSpeed * 0.6f;

            if (distance < crashDepthEdge)
            {
                newSpeed = maxVerticalSpeed * (crashDepth - this.transform.position.y) / crashDepthEdge;

                if (newSpeed > speed)
                    speed = newSpeed;
            }
				
            MoveAndRotate();

            if (distance < 0.25f)
                smoke.enableEmission = true;
        }
        
        else
        {
            playerStatus = PlayerStatus.Crashed;
            levelManager.StopLevel();
            StartCoroutine(FunctionLibrary.CallWithDelay(DisableSmoke, 2));
        }
    }
		
    private IEnumerator EnableVulnerability(float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;

        while (i < 1.0)
        {
            if (playerState == PlayerState.Enabled)
                i += Time.deltaTime * rate;

            yield return 0;
        }

        playerVulnerability = PlayerVulnerability.Enabled;
    }

    private IEnumerator ReviveProcess()
    {
		subRenderer.sprite = m_chimpSprites[currentSkinID * 2 + 1];
        reviveParticle.Play();
        newRotation = new Vector3(0, 0, 0);
        transform.eulerAngles = newRotation;
        StartCoroutine(FunctionLibrary.MoveElementBy(transform, new Vector2(0, Mathf.Abs(transform.position.y - maxDepth)), 0.5f));

        yield return m_reviveRoutineDelay;

        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Enabled;
        playerVulnerability = PlayerVulnerability.Enabled;

        bubbles.Play();

        yield return m_reviveRoutineDelay;
        powerupUsage = PowerupUsage.Enabled;
        reviveParticle.Clear();
    }
}