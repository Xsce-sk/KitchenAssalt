﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        changeDirection = false;
        moveMod = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeDirection)
        {
            moveMod *= -1;
            changeDirection = false;
        }

        m_Rigidbody2D.velocity = new Vector2(moveSpeed * moveMod, m_Rigidbody2D.velocity.y);
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        Debug.Log("I Collided");
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
            if (col.gameObject.name == "Bounds")
            {
                changeDirection = true;
            }
        }
    }
}