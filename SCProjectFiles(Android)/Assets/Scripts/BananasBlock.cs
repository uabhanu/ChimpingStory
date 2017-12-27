using UnityEngine;

public class BananasBlock : MonoBehaviour
{
    bool m_isBreakable;
    GameManager m_gameManager;
    int m_hitsTaken;
    SpriteRenderer m_brickRenderer;

    [SerializeField] AudioClip m_bananaSound;
    [SerializeField] float m_volume;
    [SerializeField] GameObject m_smokeObj;
    [SerializeField] ParticleSystem m_puffParticleSystem;
    [SerializeField] Sprite[] m_bananaSprites;
  
    public static int m_breakableBananasCount = 0 , m_destroyedBananasCount = 0;

    void Start()
    {
        m_brickRenderer = GetComponent<SpriteRenderer>();
        m_hitsTaken = 0;
        m_isBreakable = tag == "Breakable";
        m_gameManager = FindObjectOfType<GameManager>();

        if(m_isBreakable)
        {
            m_breakableBananasCount++;
            Ball.m_totalDestroyableBananas = m_breakableBananasCount;
            Debug.Log(m_breakableBananasCount);
        }
    }

    void HandleHits()
    {
        m_hitsTaken++;
        m_destroyedBananasCount++;
        int m_maxHits = m_bananaSprites.Length + 1;    

        if(m_hitsTaken >= m_maxHits)
        {
            PuffSmoke();
            m_breakableBananasCount--;
            gameObject.SetActive(false);
        }
        else
        {
            LoadSprite();
        }
    }

    void LoadSprite()
    {
        int spriteIndex = m_hitsTaken - 1;
        m_brickRenderer.sprite = m_bananaSprites[spriteIndex];
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {
        AudioSource.PlayClipAtPoint(m_bananaSound , transform.position , m_volume);

        if(m_isBreakable)
        {
            HandleHits();
        }
    }

    void PuffSmoke()
    {
        var puffParticleSystemMain = m_puffParticleSystem.main;
        puffParticleSystemMain.startColor = m_brickRenderer.color;
        Instantiate(m_smokeObj , transform.position , Quaternion.identity);
    }
}
