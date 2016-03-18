using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

	public GameObject MeteorExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnColliderEnter2D(Collider2D c)
	{
		if (c.gameObject.layer == 9)
		{
			Instantiate(MeteorExplosion, transform.position, transform.rotation);
			Instantiate(MeteorExplosion, transform.position, transform.rotation);

			Destroy(c.gameObject);

		}
		c.GetComponent<PlayerController>().EquipWeapon(2);
	}
}
