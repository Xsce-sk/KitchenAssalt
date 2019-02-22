using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public float speed;
    public bool destroyOnBecomeInvisible;


    //private float m_Direction;

    protected Transform m_Transform;
    protected Rigidbody2D m_RigidBody2D;

    [NonSerialized]
    public Vector2 m_Direction; 

    void Start()
    {
        m_Transform = this.gameObject.transform;
        m_RigidBody2D = GetComponent<Rigidbody2D>();

        //m_Direction = m_Transform.localScale.x;
        m_RigidBody2D.AddForce(m_Direction * speed * 2, ForceMode2D.Impulse);
        Destroy(this.gameObject, 5f);
    }

    private void OnBecameInvisible()
    {
        if (destroyOnBecomeInvisible)
        {
            Destroy(this.gameObject);
        }
    }
}
