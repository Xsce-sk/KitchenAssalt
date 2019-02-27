using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth = 3;
    public EnemyMovement movementScript;

    private int m_CurrentHealth;
    private bool m_IsStunned;
    private float m_initialMoveSpeed;
    
    public void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;
        m_initialMoveSpeed = movementScript.GetMoveSpeed();

        if (m_CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Stun(float duration)
    {
        if (!m_IsStunned)
        {
            StartCoroutine(TimedStun(duration));
        }
    }

    private void Start()
    {
        m_CurrentHealth = maxHealth;
    }

    private IEnumerator TimedStun(float duration)
    {
        movementScript.SetMoveSpeed(0);
        m_IsStunned = true;

        yield return new WaitForSeconds(duration);

        movementScript.SetMoveSpeed(3);
        m_IsStunned = false;
    }
    
}
