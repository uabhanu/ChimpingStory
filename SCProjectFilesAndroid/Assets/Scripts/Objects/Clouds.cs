using UnityEngine;

public class Clouds : MonoBehaviour 
{
	[SerializeField] private float m_offset;
	private Transform m_landPuss;

	private void Start()
    {
        m_landPuss = GameObject.FindGameObjectWithTag("Player").transform;
    }
	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
        m_offset = transform.position.x - m_landPuss.position.x;

		if(m_offset < -19.0f)
        {
			gameObject.SetActive(false);
			CloudsGenerator.m_totalClouds--; //TODO Use Events system to do this from within the CloudsGenerator class in the future
        }
	}
}
