using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IDamageable
{
    public int maxHealth;

    private int m_CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth(int healthDelta)
    {
        m_CurrentHealth -= healthDelta;

        // Do something when we die/get hit by something.
    }

    public void Stun(float duration)
    {
        // For if we want to be able to stun the player.
    }
}
