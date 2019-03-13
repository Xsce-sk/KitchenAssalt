using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour, IDamageable
{
    public int maxHealth;

    public SpriteRenderer sr;

    public float opacity = 0.5f;
    public float blinkDuration = 0.25f;
    public int blinkAmount = 10;
    public GameObject bloodParticle;

    protected PlayAudio m_PlayAudio;

    [SerializeField]
    private AudioClip HurtSound;

    private int m_CurrentHealth;
    private Color startColor;
    private Color damagedColor;
    private bool isDamagable = true;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = maxHealth;
        m_PlayAudio = this.transform.GetChild(2).GetComponent<PlayAudio>();
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
            m_PlayAudio.PlayClip(HurtSound);

            if (m_CurrentHealth > 0)
            {
                StartCoroutine("Blink");
                GameObject particle = Instantiate(bloodParticle, transform.position, transform.rotation) as GameObject;
                Destroy(particle, 1f);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        // Do something when we die/get hit by something.
    }

    public int CurrentHealth()
    {
        return m_CurrentHealth;
    }

    public void Stun(float duration)
    {
        // For if we want to be able to stun the player.
    }

    IEnumerator Blink()
    {
        isDamagable = false;

        for (int x = 0; x < blinkAmount; ++x)
        {
            sr.color = damagedColor;
            yield return new WaitForSeconds(blinkDuration / blinkAmount);
            sr.color = startColor;
            yield return new WaitForSeconds(blinkDuration / blinkAmount);
        }

        isDamagable = true;
    }
}
