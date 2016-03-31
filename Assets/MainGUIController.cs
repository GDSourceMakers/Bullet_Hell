using UnityEngine;
using System.Collections;

public class MainGUIController : MonoBehaviour {

	public Animator LeftAnim;
	public Animator RightAnim;

	GameController GameCon;

	// Use this for initialization
	void Start () {
		GameCon = GameController.instance;
	}
	
	// Update is called once per frame
	void Update () {

		switch (GameCon.state)
		{
			case GameState.Menu:
				LeftAnim.SetBool("Open",false);
				RightAnim.SetBool("Open", false);
				break;
			case GameState.Playing:
				LeftAnim.SetBool("Open", true);
				RightAnim.SetBool("Open", true);
				break;
			case GameState.Won:
				LeftAnim.SetBool("Open", false);
				RightAnim.SetBool("Open", false);
				break;
			case GameState.Failed:
				LeftAnim.SetBool("Open", false);
				RightAnim.SetBool("Open", false);
				break;
			default:
				break;
		}

	}
}
