using System.Collections;
using UnityEngine;

public class BananaEnabler : MonoBehaviour
{
    const float ENABLE_TIME = 10.5f;

    [SerializeField] GameObject[] m_bananasObjs;

    void Start()
    {
        BananaReset();
        //StartCoroutine("BananaEnableRoutine");
    }

    IEnumerator BananaEnableRoutine()
    {
        yield return new WaitForSeconds(ENABLE_TIME);
        BananaReset();
        StartCoroutine("BananaEnableRoutine");
    }

    void BananaReset()
    {
        if(m_bananasObjs[Random.Range(0 , m_bananasObjs.Length)].activeInHierarchy)
        {
            m_bananasObjs[Random.Range(0 , m_bananasObjs.Length)].SetActive(false);
        }

        else if(!m_bananasObjs[Random.Range(0 , m_bananasObjs.Length)].activeInHierarchy)
        {
            m_bananasObjs[Random.Range(0 , m_bananasObjs.Length)].SetActive(true);
        }
    }
}
