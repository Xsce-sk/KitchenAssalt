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

    private float m_HorizontalAxis;
    private float m_VerticalAxis;

    public KeyPressEvent OnInteractKeyPressed;
    public KeyPressEvent OnJumpKeyPressed;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        
    }

    private void Start()
    {
        m_Transform = transform;
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

        m_HorizontalAxis = Input.GetAxis("Horizontal");
        m_VerticalAxis = Input.GetAxis("Vertical");
        HorizontalMove();
    }

    private void HorizontalMove()
    {
        m_Rigidbody2D.velocity = (Vector3.right * m_HorizontalAxis) + (Vector3.up * m_VerticalAxis);
    }


}
