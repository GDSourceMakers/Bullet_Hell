using UnityEngine;
using System.Collections;

public class AstreoidScript : MonoBehaviour {


	public float hp;
	public float maxHp;
	public int score;

	public GameObject powerup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		//Debug.Log("asdasdasdsadsadsa");
		if (c.gameObject.layer == 9)
		{
			//Debug.Log("asdasdasdsadsadsa");
			hp -= c.gameObject.GetComponent<BulletScript>().damage;
			if (hp <= 0)
			{
				Destroy(gameObject);
				GameObject go = (GameObject)Instantiate(powerup);
				go.transform.position = transform.position;
			}

		}
	}
}
