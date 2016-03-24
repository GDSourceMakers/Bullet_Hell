using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public int weaponNum;

	void OnTriggerEnter2D(Collider2D c)
	{
		//Debug.Log("asdasdasdsadsadsa");
		if (c.gameObject.layer == 8)
		{
			//Debug.Log("asdasdasdsadsadsa");
			c.gameObject.GetComponent<PlayerController>().EquipWeapon(weaponNum);
			Destroy(gameObject);
		}
	}
}
