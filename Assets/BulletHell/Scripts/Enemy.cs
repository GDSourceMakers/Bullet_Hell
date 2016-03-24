using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public float hp;
	public float maxHp;
	public int score;

	public float fireRate;

	public GameObject[] firePos;

	public GameObject bullet;
	public float bulletSpeed;

	float remFireRate;

	public float enemyWarmUp;
	float remEnemyRate;

	public bool active;

	GameObject player;

	// Use this for initialization
	void Start()
	{
		remEnemyRate = enemyWarmUp;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if (active)
		{
			remFireRate -= Time.deltaTime;
			Shoot();
		}
		else if (remEnemyRate > 0)
		{
			remEnemyRate -= Time.deltaTime;
			if (remEnemyRate <= 0)
			{
				active = true;
				remFireRate = fireRate;
			}
		}
		if (player == null)
		{
			active = false;
		}
	}


	public void Shoot()
	{
		if (remFireRate < 0)
		{
			for (int i = 0; i < firePos.Length; i++)
			{
				GameObject go = Instantiate(bullet);

				go.transform.position = firePos[i].transform.position;
				go.transform.rotation = firePos[i].transform.rotation;

				Rigidbody2D r2d2 = go.GetComponent<Rigidbody2D>();
				r2d2.AddForce(go.transform.rotation * Vector2.up * -bulletSpeed * 100, ForceMode2D.Force);
			}
			remFireRate = fireRate;
		}

	}

	void OnTriggerEnter2D(Collider2D c)
	{
		
		//Debug.Log("out");
		if (c.gameObject.layer == 9)
		{
			//Debug.Log("in");
			hp -= c.gameObject.GetComponent<BulletScript>().damage;
			if (hp <= 0)
			{
				Destroy(gameObject);
			}
			Destroy(c.gameObject);
		}
		
	}
}
