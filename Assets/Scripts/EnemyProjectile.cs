using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public bool destroyOnBecomeInvisible;


    //private float m_Direction;

    protected Transform m_Transform;
    protected Rigidbody2D m_RigidBody2D;
    public Vector2 m_Direction;

    void Start()
    {
        m_Transform = this.gameObject.transform;
        m_RigidBody2D = GetComponent<Rigidbody2D>();

        //m_Direction = m_Transform.localScale.x;
        m_RigidBody2D.AddForce(m_Direction * speed, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
