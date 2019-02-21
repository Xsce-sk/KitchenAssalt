using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class KeyPressEvent : UnityEvent<PlayerController>
    { }

    public KeyCode interactKey;
    public KeyCode jumpKey;
    public float acceleration;
    public float maxMoveSpeed;
    public float verticalDampener;

    private float m_HorizontalAxis;
    private float m_MoveSpeed;

    public KeyPressEvent OnInteractKeyPressed;
    public KeyPressEvent OnJumpKeyPressed;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    private void Start()
    {
        m_Transform = this.gameObject.transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            OnInteractKeyPressed.Invoke(this);
        }

        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpKeyPressed.Invoke(this);
        }

        m_HorizontalAxis = Input.GetAxisRaw("Horizontal");
        m_MoveSpeed = CalculateMoveSpeed();
        Move();
        UpdateDirection();
    }

    private float CalculateMoveSpeed()
    {
        float newSpeed = m_MoveSpeed + acceleration;
        newSpeed *= Math.Abs(m_HorizontalAxis);
        return Mathf.Clamp(newSpeed, 0, maxMoveSpeed);
    }

    private void Move()
    {
        m_Rigidbody2D.velocity = new Vector2(m_HorizontalAxis * m_MoveSpeed, m_Rigidbody2D.velocity.y);
    }

    private void UpdateDirection()
    {
        if (m_HorizontalAxis != 0)
        {
            Vector3 currentScale = m_Transform.localScale;
            m_Transform.localScale = new Vector3(m_HorizontalAxis,
                                                 currentScale.y,
                                                 currentScale.z);
        }
    }
}
