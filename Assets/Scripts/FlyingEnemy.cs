using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Shooter shooter;
    public Transform m_Transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(m_Transform.position.x, m_Transform.position.y - 1), Vector2.down, Mathf.Infinity);
        //Debug.Log(hit.collider.tag);
        if(hit && hit.collider.CompareTag("Player"))
        {
            shooter.Shoot();
        }
    }
}
