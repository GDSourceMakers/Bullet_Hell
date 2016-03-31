using UnityEngine;
using System.Collections;

public class BoundaryDestoy : MonoBehaviour {

	void OnTriggerExit2D(Collider2D c)
	{
<<<<<<< HEAD:Assets/BulletHell/Scripts/Util/BoundaryDestoy.cs
		//Debug.Log("des");
=======
		Debug.Log("des");
>>>>>>> origin/master:Assets/BulletHell/Scripts/BoundaryDestoy.cs
		Destroy(c.gameObject);
	}
}
