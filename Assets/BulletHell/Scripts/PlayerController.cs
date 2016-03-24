using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float hp;
	public float maxHp;

	public Weapon[] weapons;

	// Use this for initialization
	void Start () {
		EquipWeapon(0);
		hp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void EquipWeapon(int index)
	{
		//active = index;
		weapons[index].Equip();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		Debug.Log("out");
		if (c.gameObject.layer == 11)
		{
			Debug.Log("in");
			hp -= c.gameObject.GetComponent<BulletScript>().damage;
			if (hp <= 0)
			{
				Destroy(gameObject);
			}

		}
		Destroy(c.gameObject);
	}

}
