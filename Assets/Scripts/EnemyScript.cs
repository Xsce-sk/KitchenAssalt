using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public GameObject bloodParticle;
    public GameObject remainsPrefab;
    public float remainsDuration;
    private KillUI killUI;

    protected PlayAudio m_PlayAudio;

    [SerializeField]
    private AudioClip HurtSound;

    private int m_currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_currentHealth = maxHealth;
        m_PlayAudio = this.GetComponent<PlayAudio>();
        killUI = GameObject.Find("Kills").GetComponent<KillUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoseHealth(int healthDelta)
    {
        m_currentHealth -= healthDelta;
        GameObject particle = Instantiate(bloodParticle, transform.position, transform.rotation) as GameObject;
        Destroy(particle, 1f);

        if(!m_PlayAudio.isPlaying())
            m_PlayAudio.PlayClip(HurtSound);

        if (m_currentHealth <= 0)
        {
            Destroy(this.gameObject);
            if (remainsPrefab != null)
            {
                GameObject remains = Instantiate(remainsPrefab, this.transform.position, Quaternion.identity);
                Destroy(remains, remainsDuration);
            }
        }
    }

    void OnDestroy()
    {
        if(killUI != null)
            killUI.IterateKillNum(1);
    }

    public void Stun(float duration)
    {
        //Do the dew...Mountian Dew
    }
}
