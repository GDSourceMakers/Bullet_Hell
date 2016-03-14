using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float remRate;

	public int active;

	public Weapon[] weapons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Weapon activeW = weapons[active];

		if (Input.GetButtonDown("Fire") && (remRate < 0f||activeW.conFire))
		{
			for (int i = 0; i < activeW.firePos.Length; i++)
			{
				GameObject go = Instantiate(activeW.bullet);

				go.transform.position = activeW.firePos[i].transform.position;
				go.transform.rotation = activeW.firePos[i].transform.rotation;

				Rigidbody2D r2d2 = go.GetComponent<Rigidbody2D>();
				r2d2.AddForce(go.transform.rotation *  Vector2.up * activeW.speed *100, ForceMode2D.Force);
			}

			remRate = weapons[active].fireRate;
		}
		else if (remRate >= 0f)
		{
			remRate -= Time.deltaTime;
		}
	}
}


[System.Serializable]
public class Weapon
{
	public string name;
	public float speed;
	public bool conFire;
	public float fireRate;

	public GameObject[] firePos;

	public GameObject bullet;

	public Sprite sprite;

	public Color color;

}
