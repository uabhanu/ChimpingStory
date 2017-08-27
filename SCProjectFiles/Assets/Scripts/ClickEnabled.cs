using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEnabled : MonoBehaviour
{
    ChimpController m_chimpControlScript;

    [SerializeField] bool m_clickEnabled;

    void OnMouseDown()
    {
        m_chimpControlScript = FindObjectOfType<ChimpController>();
        m_chimpControlScript.m_clickEnableTest = m_clickEnabled;
    }
}
