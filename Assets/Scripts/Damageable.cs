using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int m_MaxHealth = 3;
    public int m_CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;
    }
}
