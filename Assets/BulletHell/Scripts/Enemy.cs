using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public float hp;
	public float maxHp;
	public int score;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D c)

	{
		Debug.Log("asdasdasdsadsadsa");
		if (c.gameObject.layer == 9)
		{
			Debug.Log("asdasdasdsadsadsa");
			hp -= c.gameObject.GetComponent<BulletScript>().damagge;
			if (hp <= 0)
			{
				Destroy(gameObject);
			}

		}
	}
}
