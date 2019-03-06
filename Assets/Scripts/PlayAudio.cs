using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource m_Source;

    // Start is called before the first frame update
    void Start()
    {
        m_Source = this.GetComponent<AudioSource>();        
    }
    
    public void PlayClip(AudioClip clip)
    {
        m_Source.clip = clip;
        m_Source.Play();
    }

    public void StopClip()
    {
        m_Source.Stop();
    }
}
