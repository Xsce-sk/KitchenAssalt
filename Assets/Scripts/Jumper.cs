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
    private int remainingJumps;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        m_Transform = this.gameObject.transform;
        remainingJumps = maxJumps;
    }

    public void Jump()
    {
        isGrounded();

        if(remainingJumps > 0)
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);
            remainingJumps -= 1;
        }
    }

    private void isGrounded()
    {
        Vector2 origin = new Vector2(m_Transform.position.x, m_Transform.position.y - offset);
        Debug.DrawRay(origin, Vector2.down, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.1f);

        if (hit && hit.collider.CompareTag("Ground"))
            remainingJumps = maxJumps;

    }
}
