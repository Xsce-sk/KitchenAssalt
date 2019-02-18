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
        Vector3 spawnPosition = new Vector3(m_Transform.position.x + offset.x * m_Transform.localScale.x,
                                            m_Transform.position.y + offset.y,
                                            m_Transform.position.z + offset.z);
        GameObject newProjectile = Instantiate(projectileType, spawnPosition, Quaternion.identity) as GameObject;
        Vector3 initialScale = newProjectile.transform.localScale;
        newProjectile.transform.localScale = new Vector3(initialScale.x * m_Transform.localScale.x,
                                                         initialScale.y,
                                                         initialScale.z);
    }

    #endregion

    private void Start()
    {
        m_Transform = this.gameObject.transform;
    }
}
