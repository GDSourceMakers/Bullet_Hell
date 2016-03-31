using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed;

	public float sens;
	public float sensMouse;

	public float tilt;
	public float tiltSpeed;
	public float tiltMax;
	public GameObject graphic;


	Rigidbody2D r2d2;

	public Boundary boundary;

	Vector2 lastSpeed;

	public PlayerController playCon;

	// Use this for initialization
	void Start()
	{
		r2d2 = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (GameController.instance.state == GameState.Playing)
		{
			//Cursor.visible = false;
			//Cursor.lockState = CursorLockMode.Confined;

			Vector2 newSpeed = new Vector3(sens * Input.GetAxis("Horizontal"), sens * Input.GetAxis("Vertical")) * speed;


			//newSpeed += new Vector2(sensMouse * Input.GetAxis("Horizontal-Mouse"), sensMouse * Input.GetAxis("Vertical-Mouse"))* speed;


			//Debug.Log(Input.GetAxis("Horizontal-Mouse"));

			Vector3 esPos = (Vector2)transform.localPosition + newSpeed * Time.fixedDeltaTime;

			if (esPos.x <= boundary.xMin ||
				esPos.x >= boundary.xMax)
			{
				newSpeed.x = 0;
			}
			if (esPos.y <= boundary.yMin ||
				esPos.y >= boundary.yMax)
			{
				newSpeed.y = 0;
			}



			newSpeed = Vector2.ClampMagnitude(newSpeed, speed * sens * sensMouse);

			//Debug.Log(newSpeed);

			lastSpeed = Vector2.Lerp(lastSpeed, newSpeed, speed / 3 * Time.fixedDeltaTime);

			r2d2.velocity = newSpeed;

			float tilting = Mathf.Lerp(graphic.transform.localRotation.eulerAngles.y, 180 + r2d2.velocity.x * -tilt, tiltSpeed * Time.fixedDeltaTime);

			tilting = Mathf.Clamp(tilting, 180 - tiltMax, 180 + tiltMax);

			graphic.transform.localRotation = Quaternion.Euler(0, tilting, 90);


			//transform.position += delata;
		}
	}
}

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

