using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    public int m_Damage = 1;
    public string m_Target = "Enemy";

    [Serializable]
    public class OnHitEvent : UnityEvent<Damager>
    { }

    public OnHitEvent OnHitTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.name.Contains(m_Target))
        {
            col.gameObject.GetComponent<Damageable>().TakeDamage(m_Damage);
            //OnHitTarget.Invoke(this);
        }
    }
}
