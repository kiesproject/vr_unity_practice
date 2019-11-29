using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Homing : MonoBehaviour
{
    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;
    public float m_speed2 = 0.1f;

    private Vector3 m_velocity;

    private void Update()
    {
        m_velocity += (m_target.position - transform.position * m_speed2) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }
}