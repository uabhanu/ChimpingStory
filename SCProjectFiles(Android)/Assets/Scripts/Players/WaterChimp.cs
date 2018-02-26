using UnityEngine;
using System.Collections;

enum PlayerState { Enabled, Disabled };
enum PlayerStatus { Idle, MovinUp, MovingDown, DROWN, Crashed };
enum PlayerVulnerability { Enabled, Disabled, Shielded };
enum PowerupUsage { Enabled, Disabled };

public class WaterChimp : MonoBehaviour
{
	Animator m_animator;
    bool m_isDying = false;
    GameManager m_gameManager;
    GameObject m_explosionPrefab , m_explosionSystemObj;
    LevelGenerator m_levelGenerator;
	SoundManager m_soundManager;

	[SerializeField] Sprite[] m_chimpSprites;		

    public LevelManager levelManager;                                   //A link to the Level Manager
    public SpriteRenderer subRenderer;									//A link to the sub material
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
        StopAllCoroutines();

		m_animator = GetComponent<Animator>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        newRotation = new Vector3();
        startingPos = transform.position;

        playerStatus = PlayerStatus.Idle;
        playerState = PlayerState.Disabled;
        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;

        rotationDiv = maxVerticalSpeed / maxRotation;

		subRenderer.sprite = m_chimpSprites[currentSkinID * 2 + 1];

        Invoke("BackToLandWin" , 30f);
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

            else if(playerStatus == PlayerStatus.DROWN)
            {
                Drown();
            }
        }

        if(m_isDying && m_levelGenerator.speedMultiplier == 0)
        {
            BackToLandLose();
        }
    }

    void BackToLandLose()
    {
        m_gameManager.BackToLandLoseMenu();
    }

    void BackToLandWin()
    {
        m_gameManager.BackToLandWinMenu();
    }
		
    void OnTriggerEnter2D(Collider2D tri2D)
    {
        if(tri2D.tag == "Coin")
        {
            ScoreManager.m_scoreValue += 25;
            ScoreManager.m_scoreDisplay.text = ScoreManager.m_scoreValue.ToString();
            BhanuPrefs.SetHighScore(ScoreManager.m_scoreValue);
            tri2D.GetComponent<Renderer>().enabled = false;
            tri2D.GetComponent<Collider2D>().enabled = false;
			m_soundManager.m_soundsSource.clip = m_soundManager.m_coinCollected;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }
        }
        
        else if(tri2D.tag == "Obstacle")
        {
            //m_animator.SetBool("Dying" , true); //TODO use this or the position of player depending on performance
            m_isDying = true;
            levelManager.Collision(tri2D.name, tri2D.transform.position);
            tri2D.GetComponent<Renderer>().enabled = false;
            tri2D.GetComponent<Collider2D>().enabled = false;
            m_soundManager.m_soundsSource.clip = m_soundManager.m_spikesBallDeath;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            if(tri2D.name == "Torpedo")
                tri2D.transform.Find("TorpedoFire").gameObject.SetActive(false);

            if(playerVulnerability == PlayerVulnerability.Enabled)
            {
                powerupUsage = PowerupUsage.Disabled;
                playerVulnerability = PlayerVulnerability.Disabled;
                playerStatus = PlayerStatus.DROWN;

				subRenderer.sprite = m_chimpSprites[currentSkinID * 2];
                bubbles.Stop();
            }
            
            else if(playerVulnerability == PlayerVulnerability.Shielded)
            {
                CollapseShield();
            }
        }
        
        else if(tri2D.tag == "Super")
        {
            tri2D.GetComponent<Renderer>().enabled = false;
            tri2D.GetComponent<Collider2D>().enabled = false;
			ScoreManager.m_supersCount++;
			BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }

            m_gameManager.BackToLandWithSuperMenu();
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
        //StartCoroutine("ReviveProcess");
    }

    public void UpdateInput(bool inputActive)
    {
        if (playerStatus == PlayerStatus.DROWN || playerStatus == PlayerStatus.Crashed)
            return;

        if (playerStatus == PlayerStatus.Idle || playerStatus == PlayerStatus.MovingDown || inputActive)
            playerStatus = PlayerStatus.MovinUp;
        else if (playerStatus == PlayerStatus.MovinUp)
            playerStatus = PlayerStatus.MovingDown;
	}
    //Disables the crash smoke particle
    public void DisableSmoke()
    {
        //smoke.enableEmission = false;
    }

    
    public void ActivateExtraSpeed()
    {
        //extraSpeedFront.gameObject.SetActive(true);
        //extraSpeedTail.gameObject.SetActive(true);
        RaiseShield();

        playerVulnerability = PlayerVulnerability.Disabled;
        powerupUsage = PowerupUsage.Disabled;
    }
    
    public void DisableExtraSpeed()
    {
        //extraSpeedFront.gameObject.SetActive(false);
        //extraSpeedTail.gameObject.SetActive(false);
        CollapseShield();

        powerupUsage = PowerupUsage.Enabled;
    }
    //Raises the shield
    public void RaiseShield()
    {
        playerVulnerability = PlayerVulnerability.Shielded;

        //normalCollider.enabled = false;
        //shieldCollider.enabled = true;

        //StartCoroutine(FunctionLibrary.ScaleTo(shield, new Vector2(1, 1), 0.25f));
    }
    
    public void CollapseShield()
    {
        playerVulnerability = PlayerVulnerability.Disabled;
        //StartCoroutine(FunctionLibrary.ScaleTo(shield, new Vector2(0, 0), 0.25f));
        //StartCoroutine(EnableVulnerability(0.3f));
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
   
    private void Drown()
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
        }
        
        else
        {
            playerStatus = PlayerStatus.Crashed;
            levelManager.StopLevel();
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
}