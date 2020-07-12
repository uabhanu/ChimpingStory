using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private float _offset;
    private Transform _landPuss;
    
    void Start()
    {
        _landPuss = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
        _offset = transform.position.x - _landPuss.position.x;

		if(_offset < -2.5f)
        {
			gameObject.SetActive(false);
			CollectiblesGenerator.m_TotalCollectibles--;
            //Debug.Log("Collectibles Subtract Through Banana.cs as out of screen");
        }
	}
}
