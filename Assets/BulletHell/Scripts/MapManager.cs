using UnityEngine;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

public class MapManager: MonoBehaviour
{
	public List<EnemyType> gameIzek;

	Map map;

	public List<GameObject> spawnPoints;

	public int pointer;

	public float rate;

	public List<GameObject> enyemies = new List<GameObject>();

	public bool active;

	void Start()
	{
		/*
		Read(Application.dataPath + "/Map1.csv");
		pointer = map.Count-1;
		StartCoroutine(SpawnLevel());
		*/
	}

	public void StartMap(Map level)
	{
		map = level;
		pointer = map.map.Count - 1;
		StopAllCoroutines();
		foreach (GameObject item in enyemies)
		{
			Destroy(item);
		}
		StartCoroutine(SpawnLevel());
	}

	IEnumerator SpawnLevel()
	{
		while (active)
		{
			Debug.Log("run");
			map.map[pointer].Deploy();
			if (pointer - 1 >= 0)
			{
				pointer--;

			}
			else
			{
				break;
			}
			yield return new WaitForSeconds(rate);
		}
	}

	
}

[System.Serializable]
public class EnemyType
{
	public string name;
	public List<GameObject> types;
}


