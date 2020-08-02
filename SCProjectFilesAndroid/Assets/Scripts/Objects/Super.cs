using UnityEngine;

public class Super : MonoBehaviour 
{
	private LandPuss _landPuss;
	private SoundManager m_soundManager;

	void Start() 
	{
		_landPuss = GameObject.Find("LandPuss").GetComponent<LandPuss>();
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void Update() 
	{
		if(Time.timeScale == 0)
        {
            return;
        }
	}
		
	void OnTriggerEnter2D(Collider2D tri2D)
	{
		if(tri2D.gameObject.tag.Equals("Player"))
		{
            ScoreManager.m_supersCount--;
            BhanuPrefs.SetSupers(ScoreManager.m_supersCount);
			m_soundManager.m_soundsSource.clip = m_soundManager.m_superCollected;
			
            if(m_soundManager.m_soundsSource.enabled)
            {
                m_soundManager.m_soundsSource.Play();
            }
        }
	}
}
