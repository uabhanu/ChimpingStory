using UnityEngine;

public class Clouds : MonoBehaviour 
{
	private float m_offset;
	
	[SerializeField] private Transform m_landPuss;
	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
        m_offset = transform.position.x - m_landPuss.position.x;

		if(m_offset < -39.0f)
        {
			transform.position = new Vector3(m_landPuss.position.x , transform.position.y , transform.position.z);
        }
	}
}
