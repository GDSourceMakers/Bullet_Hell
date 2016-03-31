using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{


	public float hp;
	public float maxHp;

	public float shieldTime;
	public float shieldTimeRem;
	public bool hasShield;

	public Weapon[] weapons;

	public GameObject graphic;


	public Animator shieldAnim;

	// Use this for initialization
	void Start()
	{
		EquipWeapon(0);
		hp = maxHp;
	}

	// Update is called once per frame
	void Update()
	{
		if (shieldTimeRem >= 0 && hasShield)
		{
			shieldTimeRem -= Time.deltaTime;
		}
		else
		{
			hasShield = false;
			shieldAnim.SetBool("Active", false);
		}

		
	}

	public void EquipWeapon(int index)
	{
		//active = index;
		weapons[index].Equip();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		//Debug.Log("out");
		if (c.gameObject.layer == 11)
		{
			//Debug.Log("in");
			if (!hasShield)
			{
				hp -= c.gameObject.GetComponent<BulletScript>().damage;
			}
			if (hp <= 0)
			{
				GameController.instance.StartCoroutine(GameController.instance.RequestNewPLayer(3));
				Destroy(gameObject);
			}

		}
		else if (c.gameObject.layer == 10 )
		{
			if (!hasShield)
			{
				GameController.instance.StartCoroutine(GameController.instance.RequestNewPLayer(3));

				Destroy(gameObject);
				hp = 0;
			}
			GameObject goex = (GameObject)Instantiate(c.GetComponent<Enemy>().explosion);
			goex.transform.position = c.gameObject.transform.position;
		}
		Destroy(c.gameObject);
	}

	public void ActivateShield()
	{
		hasShield = true;
		shieldTimeRem = shieldTime;
		shieldAnim.SetBool("Active", true);
	}

}
