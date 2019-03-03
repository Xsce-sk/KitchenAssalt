using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public List<GameObject> weapons;
	private GameObject currWeapon;
	private Shooter currShooter;
	private int currIndex;
    private bool m_CanChangeWeapon = true;
    public float cooldownTime = 1;

	void Start()
	{
		currIndex = 0;
        ChangeWeapon();
	}

	public void NextWeapon()
	{
		++currIndex;
		if(currIndex == weapons.Count)
			currIndex = 0;
        ChangeWeapon();
	}

	public void PreviousWeapon()
	{
		--currIndex;
		if(currIndex < 0)
			currIndex = weapons.Count - 1;
        ChangeWeapon();
	}

	public void ExplicitWeapon(int weaponIndex)
	{
		currIndex = weaponIndex;
        ChangeWeapon();
	}

	public void ShootWeapon()
	{
		currShooter.Shoot();
	}

	private void ChangeWeapon()
	{
        if (m_CanChangeWeapon)
        {
            if (currWeapon != null)
                Destroy(currWeapon);
            currWeapon = Instantiate(weapons[currIndex], transform);
            currShooter = currWeapon.GetComponent<Shooter>();
            StartCoroutine("Cooldown");
        }
	}

    private IEnumerator Cooldown()
    {
        m_CanChangeWeapon = false;
        yield return new WaitForSeconds(cooldownTime);
        m_CanChangeWeapon = true;
    }

}
