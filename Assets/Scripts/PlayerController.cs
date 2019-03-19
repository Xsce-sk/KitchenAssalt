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

    public AudioClip MoveClip;

    private float m_HorizontalAxis;
    private float m_MoveSpeed;
    private PlayAudio m_PlayAudio;
    private Jumper m_Jumper;

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
    protected bool playingSound;
    private bool paused;
    public GameObject pausePanel;

    private void Start()
    {
        paused = false;
        GameController.pauseEvent.AddListener(PauseListener);
        m_PlayAudio = this.GetComponent<PlayAudio>();
        m_Transform = this.gameObject.transform;
        m_Rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_PlatformEffector2D = this.GetComponent<PlatformEffector2D>();
        m_Jumper = this.GetComponent<Jumper>();
        insideBlock = false;
        crouching = false;
        playingSound = false;
    }

    void PauseListener()
    {
        paused = !paused;
    }

    private void Update()
    {
        if (!paused)
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


            if (Input.GetKeyDown(nextWeaponKey))
            {
                OnNextWeaponKeyPressed.Invoke(this);
            }
            else if (Input.GetKeyDown(previousWeaponKey))
            {
                OnPreviousWeaponKeyPressed.Invoke(this);
            }

            if (Input.GetKeyUp(crouchKey))
            {
                crouching = false;
                AfterDrop();
            }

            m_HorizontalAxis = Input.GetAxisRaw("Horizontal");
            m_MoveSpeed = CalculateMoveSpeed();
            Move();
        }

        if (Input.GetKeyDown(resetKey))
        {
            GameController.TogglePause();
            if(pausePanel)
                GameController.TogglePanels(pausePanel);
            OnResetKeyPressed.Invoke(this);
        }
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
        
        Vector3 m_MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(m_MousePos.x > transform.position.x)
            transform.GetChild(0).localScale = new Vector3(1,transform.localScale.y,0);
        else if(m_MousePos.x < transform.position.x)
            transform.GetChild(0).localScale = new Vector3(-1,transform.localScale.y,0);
        
        if(m_Rigidbody2D.velocity.x != 0 && m_Jumper.IsGrounded() && !playingSound)
        {
            StartCoroutine(MoveSound());
        }
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
        if (collider.CompareTag("Wall"))
        {
            m_PlatformEffector2D.surfaceArc = 180;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            insideBlock = false;
            AfterDrop();
        }
        if (collider.CompareTag("Wall"))
        {
            m_PlatformEffector2D.surfaceArc = 175;
        }
    }

    private IEnumerator MoveSound()
    {
        playingSound = true;
        m_PlayAudio.PlayClip(MoveClip);
        yield return new WaitForSeconds(0.3f);
        playingSound = false;
    }
}
