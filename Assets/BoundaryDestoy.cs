using UnityEngine;
using System.Collections;

public class BoundaryDestoy : MonoBehaviour {

	void OnTriggerExit2D(Collider2D c)
	{
		Destroy(c.gameObject);
	}
}
