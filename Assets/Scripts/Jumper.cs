using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce;
    public int maxJumps;
    public float terminalVelocity;

    [Header("Grounded Detection")]
    public Vector2 offset;
    public float objectWidth;
    public float raycastLength;
    public bool debug;

    [SerializeField]
    private int m_RemainingJumps;
    private bool m_IsGrounded = false;

    protected Transform m_Transform;
    protected Rigidbody2D m_Rigidbody2D;

    #region Public Functions

    public void Jump()
    {
        if (m_RemainingJumps > 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);
            --m_RemainingJumps;
        }
    }

    public bool IsGrounded()
    {
        return m_IsGrounded;
    }

    #endregion

    private void Start()
    {
        m_Transform = this.gameObject.transform;
        m_Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();

        m_RemainingJumps = maxJumps;
    }

    private void Update()
    {
        TerminalCheck();
    }

    private void TerminalCheck()
    {
        if(m_Rigidbody2D.velocity.y < -terminalVelocity)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -terminalVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundCheck();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_IsGrounded = false;
            if (m_RemainingJumps == maxJumps)
                --m_RemainingJumps;
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D[] collisions = new RaycastHit2D[3];

        Vector2 middleOrigin = new Vector2(m_Transform.position.x + offset.x, m_Transform.position.y + offset.y);
        Vector2 leftOrigin = new Vector2(middleOrigin.x - (objectWidth / 2), middleOrigin.y);
        Vector2 rightOrigin = new Vector2(middleOrigin.x + (objectWidth / 2), middleOrigin.y);

        collisions[0] = Physics2D.Raycast(middleOrigin, Vector2.down, raycastLength);
        collisions[1] = Physics2D.Raycast(leftOrigin, Vector2.down, raycastLength);
        collisions[2] = Physics2D.Raycast(rightOrigin, Vector2.down, raycastLength);
        
        foreach (RaycastHit2D hit in collisions)
        {
            if (hit && hit.collider.CompareTag("Ground"))
            {
                print(hit.collider.gameObject.name);
                m_RemainingJumps = maxJumps;
                m_IsGrounded = true;
            }
        }

        /*
        Vector2 middleOrigin = new Vector2(m_Transform.position.x + offset.x, m_Transform.position.y + offset.y);
        RaycastHit2D hit = Physics2D.Raycast(middleOrigin, Vector2.down, raycastLength);

        if (hit && hit.collider.CompareTag("Ground"))
        {
            print(hit.collider.gameObject.name);
            m_RemainingJumps = maxJumps;
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
            if (m_RemainingJumps == maxJumps)
                --m_RemainingJumps;
        }
        */
        
        


        if (debug)
        {
            Debug.DrawRay(middleOrigin, Vector3.down * raycastLength, Color.blue, 1);
            Debug.DrawRay(leftOrigin, Vector3.down * raycastLength, Color.blue, 1);
            Debug.DrawRay(rightOrigin, Vector3.down * raycastLength, Color.blue, 1);
        }
    }
}
