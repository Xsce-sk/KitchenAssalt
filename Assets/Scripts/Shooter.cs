using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectileType;
    public Vector3 offset;

    protected Transform m_Transform;

    #region Public Functions

    public void Shoot()
    {
        /*Vector3 spawnPosition = new Vector3(m_Transform.position.x + offset.x * m_Transform.localScale.x,
                                            m_Transform.position.y + offset.y,
                                            m_Transform.position.z + offset.z);*/
        Vector2 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 v = new Vector3(worldMouse.x - m_Transform.position.x,
                worldMouse.y - m_Transform.position.y, 0);

        Debug.Log($"{v.normalized}  {v}");

        Vector3 spawnPosition = m_Transform.position + v.normalized;

        GameObject newProjectile = Instantiate(projectileType, spawnPosition, Quaternion.identity) as GameObject;

        newProjectile.GetComponent<Projectile>().m_Direction = v.normalized;
        /*Vector3 initialScale = newProjectile.transform.localScale;
        newProjectile.transform.localScale = new Vector3(initialScale.x * m_Transform.localScale.x,
                                                         initialScale.y,
                                                         initialScale.z);*/
    }

    #endregion

    private void Start()
    {
        m_Transform = this.gameObject.transform;
    }
}
