using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource coinSound;
    CircleCollider2D coinCollider2D;
    GameObject coinSoundObj;
    ScoreManager scoreManagementScript;

    void Start()
    {
        coinCollider2D = GetComponent<CircleCollider2D>();
        scoreManagementScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine("SoundObjectTimer");
    }
	
	void Update()
    {
		
	}

    IEnumerator SoundObjectTimer()
    {
        yield return new WaitForSeconds(0.4f);

        coinSoundObj = GameObject.Find("CoinSound");

        if (coinSoundObj != null)
        {
            coinSound = coinSoundObj.GetComponent<AudioSource>();
        }

        StartCoroutine("SoundObjectTimer");
    }

    void OnTriggerEnter2D(Collider2D col2D)
    {
        if(col2D.gameObject.tag.Equals("Player"))
        {
            if (coinSound != null)
            {
                if (!coinSound.isPlaying)
                {
                    coinSound.Stop();
                    coinSound.Play();
                }
                else
                {
                    coinSound.Play();
                }
            }

            Destroy(gameObject);
        }
    }
}
