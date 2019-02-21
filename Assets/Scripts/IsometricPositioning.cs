using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPositioning : MonoBehaviour
{
    public float spriteOffset;
    public bool staticObject;
    public bool showDebug;

    private Vector3 m_TargetPosition;
    private float m_SpriteHeight;

    protected SpriteRenderer m_SpriteRenderer;
    protected Transform m_Transform;

    private void Start()
    {
        m_Transform = this.transform;
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();

        m_SpriteHeight = m_SpriteRenderer.size.y;
        UpdateZPos();
    }

    private void Update()
    {
        if (!staticObject)
        {
            UpdateZPos();
        }
        

        if (showDebug)
        {
            Vector3 debugPosition = new Vector3(m_TargetPosition.x - 1,
                                            m_TargetPosition.y + spriteOffset - (m_SpriteHeight / 2),
                                            m_TargetPosition.z);
            Debug.DrawLine(debugPosition, debugPosition + (Vector3.right * 2), Color.black, Time.deltaTime, false);
        }
    }

    private void UpdateZPos()
    {
        m_TargetPosition = new Vector3(m_Transform.position.x,
                                       m_Transform.position.y,
                                       m_Transform.position.y + spriteOffset);
        m_Transform.position = m_TargetPosition;
    }
}
