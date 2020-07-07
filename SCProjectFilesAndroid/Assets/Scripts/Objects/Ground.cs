using UnityEngine;

public class Ground : MonoBehaviour 
{
	private float m_offset;
	private Transform m_landPuss;

    private void Start()
    {
        m_landPuss = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
        m_offset = transform.position.x - m_landPuss.position.x;

		if(m_offset < -12.05f)
        {
			gameObject.SetActive(false);
        }
	}
}
