using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float remRate;

	public int defaultw;
	public int active;

	public Weapon[] weapons;

	// Use this for initialization
	void Start () {
		EquipWeapon(0);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void EquipWeapon(int index)
	{
		active = index;
		weapons[active].Equip();
	}

}
