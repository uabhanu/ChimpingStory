using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource coinSound;
    CircleCollider2D coinCollider2D;
    //float startXPos , startYPos;

    //[SerializeField] float randomYValue;

    GameObject coinSoundObj;
    Ground groundScript;
    
    //ParticleSystem coinParticle;
    Rigidbody2D coinBody2D;
    ScoreManager scoreManagementScript;

    void Start()
    {
        coinBody2D = GetComponent<Rigidbody2D>();
        coinCollider2D = GetComponent<CircleCollider2D>();
        //coinParticle = GetComponent<ParticleSystem>();
        groundScript = FindObjectOfType<Ground>();
        scoreManagementScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine("SoundObjectTimer");
        //startXPos = transform.position.x;
        //startYPos = transform.position.y;
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        coinBody2D.velocity = new Vector2(-groundScript.speed , coinBody2D.velocity.y);
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
        if(col2D.gameObject.tag.Equals("Cleaner"))
        {
            //transform.position = new Vector2(startXPos , Random.Range(startYPos , startYPos + randomYValue));
            Destroy(gameObject);
        }

        if(col2D.gameObject.tag.Equals("Player"))
        {
            //coinParticle.Play();

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

            //transform.position = new Vector2(startXPos , Random.Range(startYPos , startYPos - randomYValue));
            Destroy(gameObject);
        }
    }
}
