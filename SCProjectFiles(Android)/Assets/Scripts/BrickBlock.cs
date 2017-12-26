using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    bool m_isBreakable;
    GameManager m_gameManager;
    int m_hitsTaken;
    SpriteRenderer m_brickRenderer;

    [SerializeField] AudioClip m_brickSound;
    [SerializeField] float m_volume;
    [SerializeField] GameObject m_smokeObj;
    [SerializeField] ParticleSystem m_puffParticleSystem;
    [SerializeField] Sprite[] m_brickSprites;
  
    public static int m_breakableBricksCount = 0;

    void Start()
    {
        m_brickRenderer = GetComponent<SpriteRenderer>();
        m_hitsTaken = 0;
        m_isBreakable = tag == "Breakable";
        m_gameManager = FindObjectOfType<GameManager>();

        if(m_isBreakable)
        {
            m_breakableBricksCount++;
            Debug.Log(m_breakableBricksCount);
        }
    }

    void HandleHits()
    {
        m_hitsTaken++;
        int m_maxHits = m_brickSprites.Length + 1;    

        if(m_hitsTaken >= m_maxHits)
        {
            PuffSmoke();
            m_breakableBricksCount--;
            m_gameManager.BrickDestroyed();
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
        m_brickRenderer.sprite = m_brickSprites[spriteIndex];
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {
        AudioSource.PlayClipAtPoint(m_brickSound , transform.position , m_volume);

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
