using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public List<GameObject> weapons;
	private GameObject currWeapon;
	private Shooter currShooter;
	private int currIndex;

	void Start()
	{
		currIndex = 0;
	}

	public void nextWeapon()
	{
		++currIndex;
		if(currIndex == weapons.Count)
			currIndex = 0;
		changeWeapon();
	}

	public void previousWeapon()
	{
		++currIndex;
		if(currIndex < 0)
			currIndex = weapons.Count - 1;
		changeWeapon();
	}

	public void explicitWeapon(int weaponIndex)
	{
		currIndex = weaponIndex;
		changeWeapon();
	}

	public void shootWeapon()
	{
		currShooter.shoot();
	}

	private void changeWeapon()
	{
		if(currWeapon != null)
			Destroy(currWeapon);
		currWeapon = Instantiate(weapons[currIndex], transform);
		currShooter = currWeapon.GetComponent<Shooter>();
	}

}
