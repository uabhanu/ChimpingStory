using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuObjectsMover : MonoBehaviour
{
    protected bool m_paused , m_removeLast;
    protected float m_speedMultiplier;
    protected List<Transform> m_activeElements , m_inactiveElements;

    [SerializeField] bool m_childCheck , m_generateInMiddle;
    [SerializeField] float m_delayBeforeFirst , m_resetAt , m_spawningRate , m_startAt , m_startingSpeed;
    [SerializeField] Transform m_container;

    public virtual void Reset()
    {
        m_childCheck = true;
        m_container = gameObject.transform;
        m_delayBeforeFirst = 2.0f;
        m_generateInMiddle = false;
        m_paused = true;
        m_resetAt = -12.0f;
        m_spawningRate = 3.5f;
        m_startAt = 21.0f;
        m_startingSpeed = 5.0f;

        StopAllCoroutines();

        foreach (Transform item in m_activeElements)
        {
            item.transform.position = new Vector3(m_startAt, item.transform.position.y, 0);

            if (m_childCheck)
            {
                EnableChild(item);
            }

            item.gameObject.SetActive(false);
            m_inactiveElements.Add(item);
        }

        m_activeElements.Clear();

        if (m_generateInMiddle)
        {
            SpawnElement(true);
        }
    }

    public virtual void Start()
    {
        m_speedMultiplier = 1;
        m_paused = true;

        m_activeElements = new List<Transform>();
        m_inactiveElements = new List<Transform>();

        foreach(Transform child in m_container)
        {
            m_inactiveElements.Add(child);
        }

        if(m_generateInMiddle)
        {
            SpawnElement(true);
        }
    }
    
    void Update()
    {
        if(!m_paused)
        {
            foreach(Transform item in m_activeElements)
            {
                item.transform.position -= Vector3.right * m_startingSpeed * m_speedMultiplier * Time.deltaTime;

                if (item.transform.position.x < m_resetAt)
                {
                    m_removeLast = true;
                }
            }

            if(m_removeLast)
            {
                m_removeLast = false;
                RemoveElement(m_activeElements[0]);
            }
        }
    }

    private IEnumerator SpawnDelayedElement(float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;

        while (i < 1.0)
        {
            if (!m_paused)
            {
                i += Time.deltaTime * rate;
            }

            yield return 0;
        }

        StartCoroutine("Generator");
    }

    private IEnumerator Generator()
    {
        SpawnElement(false);

        float i = 0.0f;
        float rate = 1.0f / m_spawningRate;

        while (i < 1.0)
        {
            if (!m_paused)
            {
                i += (Time.deltaTime * rate) * m_speedMultiplier;
            }

            yield return 0;
        }

        StartCoroutine("Generator");
    }

    private void EnableChild(Transform element)
    {
        foreach (Transform item in element)
        {
            item.GetComponent<Renderer>().enabled = true;
            item.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void RemoveElement(Transform item)
    {
        if(m_childCheck)
        {
            EnableChild(item);
        }

        m_activeElements.Remove(item);
        m_inactiveElements.Add(item);
        item.gameObject.SetActive(false);
        item.transform.position = new Vector3(m_startAt , item.transform.position.y , 0);
    }

    public virtual void SetPauseState(bool newState)
    {
        m_paused = newState;
    }

    public virtual void SpawnElement(bool inMiddle)
    {
        Transform item = m_inactiveElements[Random.Range(0, m_inactiveElements.Count)];

        if (inMiddle)
        {
            item.transform.position = new Vector3(0, item.transform.position.y, 0);
        }

        item.gameObject.SetActive(true);
        m_inactiveElements.Remove(item);
        m_activeElements.Add(item);
    }

    public virtual void StartGenerating()
    {
        m_paused = false;
        StartCoroutine(SpawnDelayedElement(m_delayBeforeFirst));
    }

    public void UpdateSpeedMultiplier(float n)
    {
        m_speedMultiplier = n;
    }
}
