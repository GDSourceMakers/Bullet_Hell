using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour
{


	public static GameController instance;

	public PlayerController player;
	public MapManager mapMan;
	public GUIHandler GuiHandler;

	public GameState state = GameState.Menu;

	public int levelnum;

	public List<string> levelPath = new List<string>();
	public List<Map> levels = new List<Map>();
	public bool readed;

	public int maxLives;
	public int lives;
	public int score;

	public GameObject player_prefab;
	public GameObject playerExplosion_prefab;

	void Awake()
	{
		instance = this;

	}

	// Use this for initialization
	void Start()
	{

		if (!readed)
		{
			ReadLevelTable(Application.dataPath + "/Levels.txt");
			for (int i = 0; i < levelPath.Count; i++)
			{
				levels.Add(Read(Application.dataPath + levelPath[i]));
			}
			readed = false;
		}

		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

		//DontDestroyOnLoad(gameObject);


	}

	// Update is called once per frame
	void Update()
	{
		switch (state)
		{
			case GameState.Menu:
				mapMan.StopAllCoroutines();
				break;
			case GameState.Playing:
				if (mapMan.pointer == 0)
				{
					levelnum++;
					LoadLevel();
				}
				break;
			case GameState.Dead:
				break;
			case GameState.Won:
				//ReloadLevel();
				break;
			case GameState.Failed:
				//ReloadLevel();
				break;
			default:
				break;
		}

		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void LoadLevel()
	{
		mapMan.StartMap(levels[levelnum]);
		GuiHandler.Title(levelnum);
		state = GameState.Playing;
	}

	public void StartGame()
	{
		state = GameState.Playing;
		GameObject go = Instantiate(player_prefab);
		player = go.GetComponent<PlayerController>();
		lives = maxLives;
		LoadLevel();
	}

	public IEnumerator RequestNewPLayer(float time)
	{
		state = GameState.Dead;
		Instantiate(playerExplosion_prefab, player.transform.position, Quaternion.identity);

		lives--;

		yield return new WaitForSeconds(time);

		if (lives > 1)
		{
			state = GameState.Playing;
			GameObject go = Instantiate(player_prefab);
			player = go.GetComponent<PlayerController>();
			player.ActivateShield();
			player.shieldTimeRem = 3f;
		}
		else
		{
			state = GameState.Failed;
		}
	}

	public Map Read(string p)
	{
		//Debug.Log(p);
		Map a = new Map();

		FileStream stream = new FileStream(p, FileMode.Open);
		StreamReader reader = new StreamReader(stream);

		reader.ReadLine();

		string line;

		while ((line = reader.ReadLine()) != null)
		{
			//string a = reader.ReadLine();
			LevelLine newSec = new LevelLine(line);
			a.map.Add(newSec);
		}

		return a;
	}

	public void ReadLevelTable(string p)
	{
		Debug.Log(p);


		FileStream stream = new FileStream(p, FileMode.Open);
		StreamReader reader = new StreamReader(stream);

		string line;

		while ((line = reader.ReadLine()) != null)
		{
			Debug.Log(line);
			string[] parts = line.Split(',');

			if (parts[0] != null)
			{
				levelPath.Add(parts[1]);
				//Debug.Log(parts[1]);
			}

		}
	}
}

public enum GameState
{
	Menu,
	Playing,
	Dead,
	Won,
	Failed,
}
