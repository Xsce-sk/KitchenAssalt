using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectileType;
    public float cooldownTime;
    public Vector3 offset;
    public List<float> pepperOffset = new List<float>();
    private int pepperOffsetIndex = 0;
    private int lastIndex;

    private bool m_CanShoot;

    protected Transform m_Transform;
    protected PlayAudio m_PlayAudio;

    [SerializeField]
    private AudioClip ShootSound;

    #region Public Functions

    public void Shoot()
    {
        if (m_CanShoot)
        {
            if(projectileType.name.Contains("Pepper"))
            {
                offset.y = pepperOffset[pepperOffsetIndex];
                lastIndex = pepperOffsetIndex;
            }

            Vector3 spawnOffset = (m_Transform.right * offset.x) + (m_Transform.up * offset.y) + (m_Transform.forward * offset.z);
            Vector3 spawnPosition = new Vector3(m_Transform.position.x + spawnOffset.x,
                                                m_Transform.position.y + spawnOffset.y,
                                                m_Transform.position.z + spawnOffset.z);

            Instantiate(projectileType, spawnPosition, m_Transform.rotation);

            if (ShootSound != null)
            {
                m_PlayAudio.PlayClip(ShootSound);
            }

            if (projectileType.name.Contains("Pepper"))
            {
                /*pepperOffsetIndex++;
                if (pepperOffsetIndex >= pepperOffset.Count)
                {
                    pepperOffsetIndex = 0;
                }*/

                if(pepperOffsetIndex == lastIndex)
                    pepperOffsetIndex = Random.Range(0, pepperOffset.Count);
            }

            StartCoroutine(Cooldown());
        }
    }

    /*
    public void ShootTowardsMouse()
    {
        Vector2 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = new Vector3(worldMouse.x - m_Transform.position.x,
                                  worldMouse.y - m_Transform.position.y, 0);

        //Debug.Log($"{v.normalized}  {v}");

        Vector3 spawnPosition = m_Transform.position + dir.normalized;

        GameObject newProjectile = Instantiate(projectileType, spawnPosition, Quaternion.identity) as GameObject;

        //newProjectile.GetComponent<Projectile>().m_Direction = dir.normalized;
    }
    */

    #endregion

    private void Start()
    {
        m_Transform = this.gameObject.transform;
        m_PlayAudio = this.GetComponent<PlayAudio>();
        m_CanShoot = true;
    }

    private IEnumerator Cooldown()
    {
        m_CanShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        m_CanShoot = true;
    }
}
