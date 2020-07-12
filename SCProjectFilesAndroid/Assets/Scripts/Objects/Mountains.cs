using UnityEngine;

public class Mountains : MonoBehaviour 
{
	private float m_offset;
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

		if(m_offset < -4.0f)
        {
			gameObject.SetActive(false);
			MountainsGenerator.m_TotalMountains--; //TODO Use Events system to do this from within the MountainsGenerator class in the future
        }
	}
}
