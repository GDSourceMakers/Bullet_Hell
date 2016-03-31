using UnityEngine;
using System.Collections;

public class BoundaryDestoy : MonoBehaviour {

	void OnTriggerExit2D(Collider2D c)
	{
		//Debug.Log("des");
		Destroy(c.gameObject);
	}
}
