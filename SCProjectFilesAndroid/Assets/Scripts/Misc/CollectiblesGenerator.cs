using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour
{
    //TODO This better be BananasGenerator, PortalsGenerator, etc. Check with Sri
    private bool _bIsOkToSpawn;
    private const float PLAYER_DISTANCE_SPAWN_LAND_PART = 200.0f; //TODO use this for Player Y Position
    private const int MAX_COLLECTIBLES = 1;

    [SerializeField] private float _mDefaultMoveSpeed;
    [SerializeField] private GameObject _mCollectiblePrefab;
    [SerializeField] Transform _mRaycastBottom , _mRaycastTop;

    public static int m_TotalCollectibles;

    void Start()
    {
        m_TotalCollectibles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        Movement();
        SpawnCollectible();
    }

    void Movement()
    {
        transform.Translate(-Vector2.left * _mDefaultMoveSpeed * Time.deltaTime , Space.Self);
    }

    bool IsOkToSpawn()
    {
        Debug.DrawLine(_mRaycastTop.position , _mRaycastBottom.position , Color.red);
        RaycastHit2D hit2D = Physics2D.Raycast(_mRaycastTop.position , _mRaycastBottom.position);

        if(hit2D)
        {
            if(hit2D.collider.gameObject.GetComponent<Platform>())
            {
                _bIsOkToSpawn = true;
            }

            else if(!hit2D.collider.gameObject.GetComponent<Platform>())
            {
                _bIsOkToSpawn = false;
            }
        }

        else if(!hit2D)
        {
            _bIsOkToSpawn = false;
        }

        Debug.Log("Is Ok to Spawn Outcome : " + _bIsOkToSpawn); //TODO Issue is here
        return _bIsOkToSpawn;
    }

    void SpawnCollectible()
    {
        if(IsOkToSpawn())
        {
            if(m_TotalCollectibles < MAX_COLLECTIBLES)
            {
                float randomYPos = Random.Range(transform.position.y - 1.5f , transform.position.y + 1.5f);
                Instantiate(_mCollectiblePrefab , new Vector3(transform.position.x + 15.5f , Mathf.Clamp(randomYPos , -4.90f , 0.70f) , transform.position.z) , Quaternion.identity);
                m_TotalCollectibles++;
                //Debug.Log("Collectibles Addition Through CollectiblesGenerator.cs as Collectible Spawned");
            }
        }
    }
}
