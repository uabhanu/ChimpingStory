using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhanuObjectsGenerator : MonoBehaviour
{
    bool m_canModifySpeed, m_paused;
    float m_lastSpeedMultiplier, m_speedMultiplier;

    [SerializeField] List<BhanuObjectsMover> m_bhanuObjectsMovers;
    [SerializeField] BhanuParticles m_bhanuParticles;
    [SerializeField] float m_distance , m_speedIncreaseRate;

    public void Reset()
    {
        m_paused = true;
        m_canModifySpeed = false;

        m_speedIncreaseRate = 0.005f;
        m_speedMultiplier = 1;
        m_distance = 0;

        foreach(BhanuObjectsMover mover in m_bhanuObjectsMovers)
        {
            mover.Reset();
        }

        m_bhanuParticles.Reset();
    }

    void Start()
    {
        m_speedMultiplier = 1;
        m_paused = true;
    }

    void Update()
    {
        if(!m_paused)
        {
            if(m_canModifySpeed)
            {
                m_speedMultiplier += m_speedIncreaseRate * Time.deltaTime;
            }

            m_distance += 10 * m_speedMultiplier * Time.deltaTime;

            foreach(BhanuObjectsMover mover in m_bhanuObjectsMovers)
            {
                mover.UpdateSpeedMultiplier(m_speedMultiplier);
            }
        }
    }

    public void AddBananaParticle(Vector2 contactPoint)
    {
        m_bhanuParticles.AddBananaParticle(contactPoint);
    }

    public void AddExplosionParticle(Vector2 contactPoint)
    {
        m_bhanuParticles.AddExplosion(contactPoint);
    }

    public int CurrentDistance()
    {
        return (int)m_distance;
    }

    public void EndExtraSpeed()
    {
        m_speedMultiplier = m_lastSpeedMultiplier;
        m_canModifySpeed = true;
    }

    public void SetPauseState(bool state)
    {
        m_paused = state;

        foreach(BhanuObjectsMover mover in m_bhanuObjectsMovers)
        {
            mover.SetPauseState(state);
        }

        m_bhanuParticles.SetPauseState(state);
    }

    public void StartExtraSpeed(float newSpeed)
    {
        m_lastSpeedMultiplier = m_speedMultiplier;
        m_canModifySpeed = false;
        m_speedMultiplier = newSpeed;
    }

    public void StartToGenerate()
    {
        m_paused = false;
        m_canModifySpeed = true;

        foreach(BhanuObjectsMover mover in m_bhanuObjectsMovers)
        {
            mover.StartGenerating();
        }

        m_bhanuParticles.StartGenerating();
    }
    public void StopGeneration(float time)
    {
        m_lastSpeedMultiplier = m_speedMultiplier;
        m_canModifySpeed = false;
    }
}