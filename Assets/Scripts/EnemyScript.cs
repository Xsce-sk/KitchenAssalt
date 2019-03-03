using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    public int maxHealth;

    private int m_currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoseHealth(int healthDelta)
    {
        m_currentHealth -= healthDelta;

        if (m_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Stun(float duration)
    {
        //Do the dew...Mountian Dew
    }
}
