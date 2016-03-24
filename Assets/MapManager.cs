using UnityEngine;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

public class MapManager: MonoBehaviour
{
	public List<EnemyType> gameIzek;

	List<LevelLine> map = new List<LevelLine>();

	public List<GameObject> spawnPoints;

	public int pointer;

	public float rate;

	public static MapManager instance;

	public bool active;

	void Start()
	{
		instance = this;
		Read(Application.dataPath + "/Map1.csv");
		pointer = map.Count-1;
		StartCoroutine(SpawnLevel());
	}

	IEnumerator SpawnLevel()
	{
		while (active)
		{
			Debug.Log("run");
			map[pointer].Deploy();
			pointer--;
			yield return new WaitForSeconds(rate);
		}
	}

	public void Read(string p)
	{
		Debug.Log(p);

		FileStream stream = new FileStream(p, FileMode.Open);
		StreamReader reader = new StreamReader(stream);

		reader.ReadLine();

		string line;

		while ((line = reader.ReadLine()) != null)
		{
			//string a = reader.ReadLine();
			LevelLine newSec = new LevelLine(line);
			map.Add(newSec);
		}
	}
}

public class LevelLine
{
	int[] enemies;
	string command;


	string stringLine;

	public LevelLine(string line)
	{
		stringLine = line;

		string[] segments = line.Split(';');

		enemies = new int[segments.Length];

		if (segments[0] != null)
		{

			command = segments[0];

		}

		for (int i = 1; i < segments.Length; i++)
		{
			if (segments[i] != "")
			{
				enemies[i-1] = int.Parse(segments[i]);
			}
		}
	}


	public void Deploy()
	{
		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i] != 0)
			{
				int type = enemies[i] / 10;
				int a = enemies[i] % 10;
				GameObject go = GameObject.Instantiate(MapManager.instance.gameIzek[type-1].types[a-1]);

				go.transform.position = MapManager.instance.spawnPoints[i].transform.position;
			}
		}
	}
}

[System.Serializable]
public class EnemyType
{
	public string name;
	public List<GameObject> types;
}
