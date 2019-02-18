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

    private float m_HorizontalAxis;
    private float m_VerticalAxis;
    private float m_MoveSpeed;

    public KeyPressEvent OnInteractKeyPressed;
    public KeyPressEvent OnJumpKeyPressed;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    private void Start()
    {
        m_Transform = this.gameObject.transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_Rigidbody2D.gravityScale = 0;
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
        m_VerticalAxis = Input.GetAxisRaw("Vertical");
        m_MoveSpeed = CalculateMoveSpeed();
        Move();
    }

    private float CalculateMoveSpeed()
    {
        float newSpeed = m_MoveSpeed + acceleration;
        newSpeed *= Mathf.Max(Math.Abs(m_HorizontalAxis), Math.Abs(m_VerticalAxis));
        return Mathf.Clamp(newSpeed, 0, maxMoveSpeed);
    }

    private void Move()
    {
        m_Rigidbody2D.velocity = (Vector3.right * m_HorizontalAxis * m_MoveSpeed) +
                                 (Vector3.up * m_VerticalAxis * (m_MoveSpeed / 2));
    }


}
