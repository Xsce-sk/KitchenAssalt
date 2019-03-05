using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyShooter : MonoBehaviour
{
    public Transform player;
    public EnemyMovement m_Movement;
    public GameObject projectileType;
    private Transform m_Transform;
    public float range = 5;
    private bool m_CanShoot = true;
    public float cooldownTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCloseToPlayer() && m_CanShoot)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }
    }

    private bool IsCloseToPlayer()
    {
        if (Vector2.Distance(player.position, m_Transform.position) <= range)
            return true;

        return false;
    }

    public void Shoot()
    {
        Vector3 playerPos = player.position; ;
        Vector3 dir = new Vector3(playerPos.x - m_Transform.position.x,
                                  playerPos.y - m_Transform.position.y, 0);

        //Debug.Log($"{v.normalized}  {v}");

        Vector3 spawnPosition = m_Transform.position + dir.normalized;

        GameObject newProjectile = Instantiate(projectileType, spawnPosition, Quaternion.identity) as GameObject;

        newProjectile.GetComponent<EnemyProjectile>().m_Direction = dir.normalized;
    }

    IEnumerator Cooldown()
    {
        m_CanShoot = false;
        float startSpeed = m_Movement.moveSpeed;
        m_Movement.moveSpeed = 0;
        yield return new WaitForSeconds(cooldownTime);
        m_Movement.moveSpeed = startSpeed;
        m_CanShoot = true;
    }
}
