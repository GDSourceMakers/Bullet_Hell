using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public new string name;

	public FireType fireType;
	public float fireRate;

	public GameObject[] firePos;

	public GameObject bullet;
	public float bulletSpeed;

	public Animator anim;
	public ParticleSystem[] particles; 

	float remFireRate;
	public float weaponCooldown;
<<<<<<< HEAD
	public float remWeaponRate;
=======
	float remWeaponRate;
>>>>>>> origin/master

	public bool active;

	void Update()
	{
<<<<<<< HEAD
		if (GameController.instance.state == GameState.Playing)
		{
			if (active)
			{
				Shoot();
			}
			if (active && remFireRate >= 0f)
			{
				remFireRate -= Time.deltaTime;
			}
			if (active && remWeaponRate >= 0f)
			{
				remWeaponRate -= Time.deltaTime;
			}
			else if (active && remWeaponRate < 0)
			{
				DeEquip();
			}
=======
		if (active)
		{
			Shoot();
		}
		if (active && remFireRate >= 0f)
		{
			remFireRate -= Time.deltaTime;
		}
		if (active && remWeaponRate >= 0f)
		{
			remWeaponRate -= Time.deltaTime;
		}
		else if (active && remWeaponRate < 0)
		{
			DeEquip();
>>>>>>> origin/master
		}
	}

	public void Shoot()
	{
		if ((Input.GetButtonDown("Fire") && remFireRate < 0f && fireType == FireType.Single) ||
			(Input.GetButton("Fire") && remFireRate < 0f && fireType == FireType.Cons))
		{
			for (int i = 0; i < firePos.Length; i++)
			{
				if (anim != null)
				{
					anim.SetTrigger("Shoot");
				}

				GameObject go = Instantiate(bullet);

				go.transform.position = firePos[i].transform.position;
				go.transform.rotation = firePos[i].transform.rotation;

				Rigidbody2D r2d2 = go.GetComponent<Rigidbody2D>();
				r2d2.AddForce(go.transform.rotation * Vector2.up * bulletSpeed * 100, ForceMode2D.Force);

<<<<<<< HEAD
				foreach (ParticleSystem item in particles)
				{
					item.Play();
				}

=======
>>>>>>> origin/master
				remFireRate = fireRate;
			}
		}
	}

	public void Equip()
	{
		if (anim != null)
		{
			anim.SetBool("Active", true);
		}
		else
		{
			active = true;
		}
		remWeaponRate = weaponCooldown;
	}

	public void DeEquip()
	{
		if (anim != null)
		{
			anim.SetBool("Active", false);
		}
		
		
			active = false;
		
		remWeaponRate = 0;
	}

	public void ForceChange(bool state)
	{
		if (state)
		{
			Equip();
		}
		else
		{
			DeEquip();
		}
	}
}

public enum FireType
{
	Cons,
	Single,
}
