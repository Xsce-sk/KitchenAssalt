﻿using System;
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
    public KeyCode crouchKey;
    public float acceleration;
    public float maxMoveSpeed;
    public float verticalDampener;

    private float m_HorizontalAxis;
    private float m_MoveSpeed;

    public KeyPressEvent OnInteractKeyPressed;
    public KeyPressEvent OnJumpKeyPressed;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;
    protected PlatformEffector2D m_PlatformEffector2D;

    private void Start()
    {
        m_Transform = this.gameObject.transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_PlatformEffector2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            OnInteractKeyPressed.Invoke(this);
        }
        else if (Input.GetKey(crouchKey))
        {
            if (Input.GetKeyDown(jumpKey))
            {
                m_PlatformEffector2D.rotationalOffset = 0;
            }
        }
        else if (Input.GetKeyDown(jumpKey))
        {
            OnJumpKeyPressed.Invoke(this);
        }

        

        if (Input.GetKeyUp(crouchKey))
        {
            m_PlatformEffector2D.rotationalOffset = 180;
        }

        m_HorizontalAxis = Input.GetAxisRaw("Horizontal");
        m_MoveSpeed = CalculateMoveSpeed();
        Move();
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
}
