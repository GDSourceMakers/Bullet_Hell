using UnityEngine;
using System.Collections;

public class AstreoidScript : MonoBehaviour
{


	public float hp;
	public float maxHp;
	public int score;

	public float rotation;
	public float speed;

	public GameObject powerup;
	public GameObject explosion;

	public Vector3 rot;

	public GameObject graphic;

	void Start()
	{
		rot =  Random.insideUnitSphere*rotation;
		//GetComponent<Rigidbody2D>().angularVelocity = Random.insideUnitSphere * rotation;
	}

	void Update()
	{
		graphic.transform.rotation = Quaternion.Lerp(graphic.transform.rotation, graphic.transform.rotation * Quaternion.Euler( rot * Time.deltaTime * rotation), Time.deltaTime);
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
				GameController.instance.score += score;

				GameObject go = (GameObject)Instantiate(powerup);
				go.transform.position = transform.position;

				GameObject goex = (GameObject)Instantiate(explosion);
				goex.transform.position = transform.position;

				Destroy(gameObject);
			}
			Destroy(c.gameObject);
		}
	}
}
