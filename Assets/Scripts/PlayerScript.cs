using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IDamageable
{
    public int maxHealth;

    private int m_CurrentHealth;

    public SpriteRenderer sr;

    public float opacity = 0.5f;
    public float blinkDuration = 0.25f;

    private Color startColor;
    private Color damagedColor;
    private bool isDamagable = true;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = maxHealth;
        startColor = sr.color;
        damagedColor = new Color(startColor.r, startColor.g, startColor.b, opacity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth(int healthDelta)
    {
        if (isDamagable)
        {
            m_CurrentHealth -= healthDelta;

            if (m_CurrentHealth > 0)
            {
                StartCoroutine("Blink");
            }
        }

        // Do something when we die/get hit by something.
    }

    public void Stun(float duration)
    {
        // For if we want to be able to stun the player.
    }

    IEnumerator Blink()
    {
        sr.color = damagedColor;
        isDamagable = false;
        yield return new WaitForSeconds(blinkDuration);
        sr.color = startColor;
        isDamagable = true;
    }
}
