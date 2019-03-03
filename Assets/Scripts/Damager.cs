using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage;
    public float stunDuration;
    public bool destroyOnCollision;
    public List<string> tagIgnores = new List<string>(); // For things you don't want the projectile to interact with

    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageableComponent = other.GetComponent<IDamageable>();
        if (damageableComponent != null && !tagIgnores.Contains(other.tag))
        {
            damageableComponent.LoseHealth(damage);

            // Currently will only destroy if collides with something that can take damage
            // If that needs to change, just need to move this into an if that checks !tagIgnores.Contains(other.tag) 
            if (destroyOnCollision)
            {
                Destroy(this.gameObject);
            }

            if (stunDuration > 0)
            {
                damageableComponent.Stun(stunDuration);
            }
        }
    }
}

#region Old Damager
/*
 
    Kept just in case we need things from it

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
*/
#endregion
