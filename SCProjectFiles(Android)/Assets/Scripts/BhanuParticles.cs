using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuParticles : MonoBehaviour
{
    bool m_paused;
    float m_speedMultiplier;
    List<Transform> m_activeElements, m_inactiveElements;

    [SerializeField] float m_startingSpeed;
    [SerializeField] GameObject m_bananaParticlePrefab , m_explosionPrefab;
    [SerializeField] Transform m_container;

    public void Reset()
    {
        m_paused = true;
        m_startingSpeed = 5.0f;

        StopAllCoroutines();

        foreach(Transform item in m_activeElements)
        {
            item.transform.position = new Vector3(-12 , item.transform.position.y , 0);
            item.gameObject.SetActive(false);
            m_inactiveElements.Add(item);
        }

        m_activeElements.Clear();
    }

    void Start()
    {
        m_speedMultiplier = 1;
        m_paused = true;

        m_activeElements = new List<Transform>();
        m_inactiveElements = new List<Transform>();

        foreach(Transform child in m_container)
        {
            m_inactiveElements.Add(child);
        }
    }
    
    void Update()
    {
        if(!m_paused)
        {
            foreach (Transform item in m_activeElements)
            {
                item.transform.position -= Vector3.right * m_startingSpeed * m_speedMultiplier * Time.deltaTime;
            }
        }
    }

    IEnumerator PlayParticle(Transform particle , float time)
    {
        ParticleSystem p = particle.GetComponent("ParticleSystem") as ParticleSystem;
        p.Play();

        float i = 0.0f;
        float rate = 1.0f / time;

        while(i < 1.0)
        {
            if(!m_paused)
            {
                i += Time.deltaTime * rate;
            }

            yield return 0;
        }

        p.Stop();
        p.Clear();

        RemoveElement(particle);
    }

    public void AddBananaParticle(Vector2 position)
    {
        Transform item = m_inactiveElements.Find(x => x.name == "PF_BananaParticle");

        if(item == null)
        {
            item = SpawnNewParticle(m_bananaParticlePrefab);
        }

        AddElement(item , position);
    }

    void AddElement(Transform item , Vector2 pos)
    {
        item.transform.position = pos;
        item.gameObject.SetActive(true);

        m_inactiveElements.Remove(item);
        m_activeElements.Add(item);

        StartCoroutine(PlayParticle(item , 2f));
    }

    public void AddExplosion(Vector2 position)
    {
        Transform item = m_inactiveElements.Find(x => x.name == "PF_Explosion");

        if(item == null)
        {
            item = SpawnNewParticle(m_explosionPrefab);
        }

        AddElement(item , position);
    }

    void RemoveElement(Transform item)
    {
        item.transform.position = new Vector3(-12 , item.transform.position.y , 0);
        m_activeElements.Remove(item);
        m_inactiveElements.Add(item);
        item.gameObject.SetActive(false);
    }

    public void SetPauseState(bool state)
    {
        m_paused = state;
    }

    Transform SpawnNewParticle(GameObject prefab)
    {
        GameObject newGo = Instantiate(prefab);
        newGo.name = prefab.name;
        newGo.transform.parent = transform;
        m_inactiveElements.Add(newGo.transform);
        return newGo.transform;
    }

    public void StartGenerating()
    {
        m_paused = false;
    }
    
    public void UpdateSpeedMultiplier(float n)
    {
        m_speedMultiplier = n;
    }
}
