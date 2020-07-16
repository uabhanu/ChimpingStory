using UnityEditor;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_COLLECTIBLE = 200.0f;
    private const int MAX_COLLECTIBLES = 1;

    private bool _bIsOkToSpawn;
    private float _moveSpeed;

    [SerializeField] private Transform _collectiblesEndPosition;
    [SerializeField] private Transform _collectiblesPartToSpawn;
    [SerializeField] private Transform _raycastBottom;
    [SerializeField] private Transform _raycastTop;
    [SerializeField] private LandPuss _landPuss;
    [SerializeField] private Vector3 _lastEndPosition;

    public static int m_TotalCollectibles;

    private void Awake() 
    {
        _lastEndPosition = _collectiblesEndPosition.transform.position;
        m_TotalCollectibles = 0;
    }

    private void Update() 
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        _bIsOkToSpawn = IsOkToSpawn();

        Movement();

        if(_bIsOkToSpawn)
        {
            if(Vector3.Distance(_landPuss.GetPosition() , _lastEndPosition) < PLAYER_DISTANCE_SPAWN_COLLECTIBLE) 
            {
                // Spawn another collectibles part
                SpawnCollectiblesPart();
            }
        }
    }

    private void SpawnCollectiblesPart() 
    {
        if(m_TotalCollectibles < MAX_COLLECTIBLES)
        {
            Transform chosenCollectiblesPart = _collectiblesPartToSpawn;
            Transform lastCollectiblesPartTransform = SpawnCollectiblesPart(chosenCollectiblesPart , _lastEndPosition);
            _lastEndPosition = lastCollectiblesPartTransform.Find("EndPosition").position;
            m_TotalCollectibles++;
        }
    }

    private Transform SpawnCollectiblesPart(Transform collectiblesPart , Vector3 position)
    {
        position = new Vector3(transform.position.x , transform.position.y , transform.position.z);
        Transform collectiblesPartTransform = Instantiate(collectiblesPart , position , Quaternion.identity);
        return collectiblesPartTransform;
    }

    bool IsOkToSpawn()
    {
        Debug.DrawLine(_raycastTop.position , _raycastBottom.position , Color.red);
        RaycastHit2D hit2D = Physics2D.Raycast(_raycastTop.position , _raycastBottom.position);

        if(hit2D)
        {
            //Debug.Log("Object Hit : " + hit2D.collider.gameObject.name);

            if(hit2D.collider.gameObject.tag == "Collectibles")
            {
                //Debug.Log("Raycast Hit : " + hit2D.collider.gameObject);
                _bIsOkToSpawn = false;
            }

            else if(hit2D.collider.gameObject.tag == "Platform")
            {
                //Debug.Log("Raycast Hit : " + hit2D.collider.gameObject);
                _bIsOkToSpawn = true;
            }

            else
            {
                //Debug.Log("Raycast Hit : " + hit2D.collider.gameObject);
                _bIsOkToSpawn = false;
            }
        }

        else if(!hit2D)
        {
            _bIsOkToSpawn = false;
        }

        return _bIsOkToSpawn;
    }

    void Movement()
    {
        _moveSpeed = _landPuss.GetMoveSpeed();
        transform.Translate(-Vector2.left * _moveSpeed * Time.deltaTime , Space.Self);
    }
}
