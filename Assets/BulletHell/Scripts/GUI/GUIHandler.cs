using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIHandler : MonoBehaviour {

	GameController GameCon;

	public GameObject Ingame;
	public GameObject Menu;
	public GameObject Failed;
	public GameObject Won;

	public Text title;

	public Image Hp;
	public Image Weapon_cannon;
	public Image Weapon_angle;
	public Image Shield;
	public Text Score;
	public Text Lives;

	public PlayerController player;

	public Animator menu_anim;
	public Animator bar_anim;

	public bool MenuAnimDone;
	public bool MenuAnimOut;
	public bool MenuAnimDoneOut;

	public bool EndGameAnimDone;
	public bool EndGameAnimRune;

	// Use this for initialization
	void Start () {
		GameCon = GameController.instance;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (GameCon.state)
		{
			case GameState.Menu:
				

				//Ingame.SetActive(false);

				if (Input.GetButtonUp("Fire") && MenuAnimDone)
				{
					menu_anim.SetBool("Active", false);
					MenuAnimOut = true;
				}
				else if(!MenuAnimOut)
				{
					menu_anim.SetBool("Active", true);
					bar_anim.SetBool("Active", false);
				}

				if (MenuAnimDoneOut)
				{
					GameCon.StartGame();
					Ingame.SetActive(true);
					MenuAnimDone = false;
				}
				

				break;
			case GameState.Playing:
				bar_anim.SetBool("Active", true);

				player = GameController.instance.player;
				Hp.fillAmount = player.hp / player.maxHp;

				Weapon_cannon.fillAmount = player.weapons[1].remWeaponRate / player.weapons[1].weaponCooldown;
				Weapon_angle.fillAmount = player.weapons[2].remWeaponRate / player.weapons[2].weaponCooldown;
				Shield.fillAmount = player.shieldTimeRem / player.shieldTime;

				Score.text = "Score: \n" + GameCon.score.ToString("D6");
				Lives.text = "Lives: \n" + GameCon.lives.ToString();

				break;
			case GameState.Dead:

				Hp.fillAmount = 0;

				Weapon_cannon.fillAmount = 0;
				Weapon_angle.fillAmount = 0;
				Shield.fillAmount = 0;

				Score.text = "Score: \n" + GameCon.score.ToString("D6");
				Lives.text = "Lives: \n" + GameCon.lives.ToString();

				break;
			case GameState.Won:
				if (!EndGameAnimRune)
				{
					Won.GetComponent<Animator>().SetTrigger("Active");
					EndGameAnimRune = true;
				}
				else if (EndGameAnimDone)
				{
					GameCon.state = GameState.Menu;
				}
				break;
			case GameState.Failed:
				if (!EndGameAnimRune)
				{
					Failed.GetComponent<Animator>().SetTrigger("Active");
					EndGameAnimRune = true;
				}
				else if (EndGameAnimDone)
				{
					GameCon.state = GameState.Menu;
				}
				break;
			default:
				break;
		}
	}

	public void Title(int index)
	{
		title.text = "Level " + index;
		title.gameObject.GetComponent<Animator>().SetTrigger("Active");

	}
}
