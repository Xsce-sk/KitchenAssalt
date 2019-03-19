using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected Rigidbody2D m_Rigidbody2D;
    public bool changeOnFloorEdge = false;
    public bool changeOnCollision = false;
    public float moveSpeed;
    private float moveMod;
    private bool changeDirection;
    private bool beenSeen;

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    void Start()
    {
        m_Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        changeDirection = false;
        moveMod = -1;
        beenSeen = false;
    }

    void Update()
    {
        if (beenSeen)
        {
            if (changeDirection)
            {
                moveMod *= -1;
                changeDirection = false;
            }

            m_Rigidbody2D.velocity = new Vector2(moveSpeed * moveMod, m_Rigidbody2D.velocity.y);
            if(m_Rigidbody2D.velocity.x > 0)
                transform.localScale = new Vector3(-1,transform.localScale.y,0);
            else if(m_Rigidbody2D.velocity.x < 0)
                transform.localScale = new Vector3(1,transform.localScale.y,0);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (changeOnCollision)
        {
            if(!col.gameObject.name.Contains("Floor"))
            {
                
                changeDirection = true;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (changeOnFloorEdge)
        {
            if (col.gameObject.CompareTag("Bound"))
            {
                changeDirection = true;
            }
        }
    }
    private void OnBecameVisible()
    {
        beenSeen = true;
    }
}
