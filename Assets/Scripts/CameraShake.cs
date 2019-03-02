using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Screen Shake Settings")]
    public float duration = 0.25f;
    public float delay = 0.05f;
    public float shakeAmount = 0.3f;

    protected Transform m_Transform;
    protected float m_Multiplier = 2f;

    public void StartShake()
    {
        m_Multiplier = 1f;
        StartCoroutine("Shake");
    }

    public void StartLargeShake()
    {
        m_Multiplier = 3f;
        StartCoroutine("Shake");
    }

    void Start()
    {
        m_Transform = transform;
    }

    public IEnumerator Shake()
    {
        print("Shaking Screen");
        float t = duration * m_Multiplier;
        Vector3 startPos = m_Transform.position;
        Vector3 newPos = Vector3.zero;
        float xOffset = 0f;
        float yOffset = 0f;
        while (t >= 0)

        {
            xOffset = Random.Range(-shakeAmount, shakeAmount) * m_Multiplier;
            yOffset = Random.Range(-shakeAmount, shakeAmount) * m_Multiplier;
            newPos = new Vector3(startPos.x + xOffset, startPos.y + yOffset, startPos.z);
            m_Transform.position = newPos;
            Debug.Log(m_Transform.gameObject.name);

            yield return new WaitForSeconds(delay);
            t -= Time.deltaTime;
        }

        m_Transform.position = startPos;
    }
}
