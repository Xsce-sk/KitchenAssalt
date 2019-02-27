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
    public KeyCode crouchKey;
    public KeyCode nextWeaponKey;
    public KeyCode previousWeaponKey;
    public KeyCode resetKey;
    public float acceleration;
    public float maxMoveSpeed;
    public float verticalDampener;

    private float m_HorizontalAxis;
    private float m_MoveSpeed;

    public KeyPressEvent OnInteractKeyPressed;
    public KeyPressEvent OnInteractKeyHeld;
    public KeyPressEvent OnJumpKeyPressed;
    public KeyPressEvent OnNextWeaponKeyPressed;
    public KeyPressEvent OnPreviousWeaponKeyPressed;
    public KeyPressEvent OnResetKeyPressed;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;
    protected PlatformEffector2D m_PlatformEffector2D;
    protected bool insideBlock;
    protected bool crouching;

    private void Start()
    {
        m_Transform = this.gameObject.transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_PlatformEffector2D = GetComponent<PlatformEffector2D>();
        insideBlock = false;
        crouching = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            OnInteractKeyPressed.Invoke(this);
        }
        else if (Input.GetKey(interactKey))
        {
            OnInteractKeyHeld.Invoke(this);
        }

        if (Input.GetKey(crouchKey))
        {
            crouching = true;
            if (Input.GetKeyDown(jumpKey))
            {
                Drop();
            }
        }
        else if (Input.GetKeyDown(jumpKey))
        {
            OnJumpKeyPressed.Invoke(this);
        }

        
        if(Input.GetKeyDown(nextWeaponKey))
        {
            OnNextWeaponKeyPressed.Invoke(this);
        }
        else if(Input.GetKeyDown(previousWeaponKey))
        {
            OnPreviousWeaponKeyPressed.Invoke(this);
        }

        if (Input.GetKeyUp(crouchKey))
        {
            crouching = false;
            AfterDrop();
        }

        if (Input.GetKeyDown(resetKey))
        {
            GameController.LoadSceneByIndex(0);
            OnResetKeyPressed.Invoke(this);
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

    private void Drop()
    {
        RaycastHit2D[] collisions = new RaycastHit2D[3];

        Vector2 middleOrigin = new Vector2(m_Transform.position.x, m_Transform.position.y);
        Vector2 leftOrigin = new Vector2(middleOrigin.x - (1f / 2), middleOrigin.y);
        Vector2 rightOrigin = new Vector2(middleOrigin.x + (1f / 2), middleOrigin.y);

        collisions[0] = Physics2D.Raycast(middleOrigin, Vector2.down, 1.1f);
        collisions[1] = Physics2D.Raycast(leftOrigin, Vector2.down, 1.1f);
        collisions[2] = Physics2D.Raycast(rightOrigin, Vector2.down, 1.1f);

        foreach (RaycastHit2D hit in collisions)
        {
            if (hit && hit.collider.CompareTag("Ground"))
            {
                m_PlatformEffector2D.rotationalOffset = 0;
            }
        }
    }
    private void AfterDrop()
    {
        if (!insideBlock && !crouching)
            m_PlatformEffector2D.rotationalOffset = 180;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Floor"))
        {
            m_PlatformEffector2D.rotationalOffset = 180;
        }
        if(collider.CompareTag("Ground"))
        {
            insideBlock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            insideBlock = false;
            AfterDrop();
        }
    }

}
