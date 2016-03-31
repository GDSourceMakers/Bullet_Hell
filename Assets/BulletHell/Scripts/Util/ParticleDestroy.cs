using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

	public float time;
	float RemTime;

	// Use this for initialization
	void Start () {
		RemTime = time;
	}
	
	// Update is called once per frame
	void Update () {
		if (RemTime >= 0)
		{
			RemTime -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
