using UnityEngine;

public class Mountains : MonoBehaviour 
{
	[SerializeField] float m_speed;
	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}

        transform.Translate(Vector2.left * m_speed * Time.deltaTime);

		if(transform.position.x <= -28.74f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
