using UnityEngine;

public class Clouds : MonoBehaviour 
{
	[SerializeField] float m_speed;
	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
        transform.Translate(Vector2.left * m_speed * Time.deltaTime);

		if(transform.position.x <= -43.2f) //Use Player Position here
		{
			//transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
