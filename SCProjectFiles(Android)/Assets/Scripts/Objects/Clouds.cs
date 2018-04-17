using UnityEngine;

public class Clouds : MonoBehaviour 
{
	void Update() 
	{
		if(Time.timeScale == 0f)
		{
			return;
		}
			
        transform.Translate(Vector2.left * (LevelCreator.m_gameSpeed / 8) * Time.deltaTime);

		if(transform.position.x <= -43.2f)
		{
			transform.position = new Vector3(0f , transform.position.y , transform.position.z);
		}
	}
}
