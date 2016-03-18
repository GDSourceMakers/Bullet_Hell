using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public string name;

	public FireType fireType;
	public float fireRate;

	public GameObject[] firePos;

	public GameObject bullet;
	public float bulletSpeed;
	public Color color;


	public bool active;

	public Animator anim;

	public float remRate;

	void Update()
	{
		if (active)
		{
			Shoot();
		}
		if (active && remRate >= 0f)
		{
			remRate -= Time.deltaTime;
		}
	}

	public void Shoot()
	{
		if ((Input.GetButtonDown("Fire") && remRate < 0f && fireType == FireType.Single) ||
			(Input.GetButton("Fire") && remRate < 0f && fireType == FireType.Cons))
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

				remRate = fireRate;
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
	}

	public void DeEquip()
	{

	}
}

public enum FireType
{
	Cons,
	Single,
}
