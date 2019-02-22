using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    protected Rigidbody2D m_Rigidbody2D;
    protected Transform m_Transform;
    public float offset;
    public float jumpForce = 50;
    public int maxJumps = 1;
    [SerializeField]
    private int remainingJumps;
    public float terminalVelocity = -5;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        m_Transform = this.gameObject.transform;
        remainingJumps = maxJumps;
    }

    void Update()
    {
        TerminalCheck();
    }

    private void TerminalCheck()
    {
        if(m_Rigidbody2D.velocity.y < 0 && m_Rigidbody2D.velocity.y < terminalVelocity)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, terminalVelocity);
        }
    }

    public void Jump()
    {
        if(remainingJumps > 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);
            --remainingJumps;
        }
    }

    private void GroundCheck()
    {
        Vector2 origin = new Vector2(m_Transform.position.x, m_Transform.position.y - offset);
        Debug.DrawRay(origin, Vector2.down * 0.1f, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.05f);

        if (hit && hit.collider.CompareTag("Ground"))
        {
            remainingJumps = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            if (remainingJumps == maxJumps)
                --remainingJumps;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GroundCheck();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GroundCheck();
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
