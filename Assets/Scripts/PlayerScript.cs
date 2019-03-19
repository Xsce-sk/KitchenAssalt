using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour, IDamageable
{
    public int maxHealth;

    public SpriteRenderer sr;
    public GameObject endGamePanel;

    public float opacity = 0.5f;
    public float blinkDuration = 0.25f;
    public int blinkAmount = 10;
    public GameObject bloodParticle;
    public Sprite DeathSprite;

    protected PlayAudio m_PlayAudio;

    [SerializeField]
    private AudioClip HurtSound;

    protected Rigidbody2D m_Rigidbody2D;

    private int m_CurrentHealth;
    private Color startColor;
    private Color damagedColor;
    private bool isDamagable = true;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        m_CurrentHealth = maxHealth;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
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
        if (isDamagable && !dead)
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
                dead = true;
                RagDoll();
                // StartCoroutine(Die());
            }
        }

        // Do something when we die/get hit by something.
    }

    public void RagDoll()
    {
        Destroy(this.GetComponent<PlayerController>());
        Destroy(this.GetComponent<Jumper>());
        Destroy(this.GetComponent<PlatformEffector2D>());
        Destroy(this.transform.GetChild(1).gameObject);
        sr.sprite = DeathSprite;
        m_Rigidbody2D.AddForce(Vector2.up * 500);
        this.transform.Rotate(0,0,90);

        StartCoroutine(Die());
    }

    public int CurrentHealth()
    {
        return m_CurrentHealth;
    }

    public void Stun(float duration)
    {
        // For if we want to be able to stun the player.
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        GameController.TogglePanels(endGamePanel);
        GameController.TogglePause();
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
