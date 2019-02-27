using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [Serializable]
    public class OnHitEvent : UnityEvent<Damager>
    { }

    public int damage;
    public bool stuns;
    public float stunTime;
    public bool destroyOnCollision;
    public OnHitEvent OnHitTarget;

    void OnTriggerEnter2D (Collider2D collision)
    {
        Damageable target = collision.gameObject.GetComponent<Damageable>();

        if (target != null)
        {
            target.TakeDamage(damage);
            OnHitTarget.Invoke(this);

            if (stuns)
            {
                target.Stun(stunTime);
            }

            if (destroyOnCollision)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
