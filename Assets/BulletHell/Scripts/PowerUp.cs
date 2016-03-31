using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public PowerUpType type;

	public int weaponNum;
	public int addHp;
	public int addLives;


	void OnTriggerEnter2D(Collider2D c)
	{
		//Debug.Log("asdasdasdsadsadsa");
		if (c.gameObject.layer == 8)
		{
			switch (type)
			{
				case PowerUpType.Weapon:
					c.gameObject.GetComponent<PlayerController>().EquipWeapon(weaponNum);
					break;
				case PowerUpType.Hp:
					c.gameObject.GetComponent<PlayerController>().hp += addHp;
					break;
				case PowerUpType.Live:
					GameController.instance.lives += addLives;
					break;
				case PowerUpType.Shield:
					c.gameObject.GetComponent<PlayerController>().ActivateShield();
					break;
				default:
					break;
			}
			c.gameObject.GetComponent<PlayerController>().EquipWeapon(weaponNum);
			Destroy(gameObject);
		}
	}

	public enum PowerUpType
	{
		Weapon,
		Hp,
		Live,
		Shield,
	}
}

