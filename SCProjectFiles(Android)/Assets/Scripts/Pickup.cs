using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    bool m_releasePickup = false;
    float m_minY , m_maxY;
	int m_direction = 1;
	SpriteRenderer m_powerUpRenderer;

    public bool m_inPlay = true;

	void Start()
    {
		m_maxY = transform.position.y + 0.5f;
		m_minY = m_maxY - 1.0f;
		m_powerUpRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

		transform.position = new Vector2(transform.position.x , transform.position.y + (m_direction * 0.05f));

		if(transform.position.y > m_maxY)
        {
            m_direction = -1;
        }
						
		if(transform.position.y < m_minY)
        {
            m_direction = 1;
        }
						

		if(!m_inPlay && !m_releasePickup)
        {
            Respawn();
        }
	}

	void OnTriggerEnter2D(Collider2D tri2D)
    {
		if(tri2D.gameObject.tag == "Player")
        {
			switch(m_powerUpRenderer.sprite.name)
            {

			    case "crates_0":
				    GameObject.Find("Main Camera").GetComponent<LevelCreator>().gameSpeed -= 1.0f; // CHANGE V4 BEGINNING 8
			    break;

			    case "crates_1":
				    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6000);
			    break;

			    case "crates_2":
				    //GameObject.Find("Main Camera").GetComponent<ScoreHandler>().Points +=10;
			    break;
			}

			m_inPlay = false;
			transform.position = new Vector2(transform.position.x , transform.position.y + 30.0f);
			GameObject.Find("Main Camera").GetComponent<PlaySound>().SoundToPlay("Pickup");

		}
	}

	void Respawn()
    {
		m_releasePickup = true;
		Invoke("PlacePickup", Random.Range(3f , 10f));
	}

	void PlacePickup()
    {
		m_inPlay = true;
		m_releasePickup = false;
		GameObject tmpTile = GameObject.Find ("Main Camera").GetComponent<LevelCreator>().tilePos;
		transform.position = new Vector2 (tmpTile.transform.position.x , tmpTile.transform.position.y + 5.5f); 
		m_maxY = transform.position.y + 0.5f;
		m_minY = m_maxY - 1.0f;
	}











}
