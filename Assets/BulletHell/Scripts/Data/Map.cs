using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Map
{
	public List<LevelLine> map = new List<LevelLine>();
}


public class LevelLine
{
	int[] enemies;
	char command;
	int num;




	public LevelLine(string line)
	{


		string[] segments = line.Split(';');

		enemies = new int[segments.Length];

		if (segments[0] != null && segments[0] != "")
		{
			command = segments[0][0];
			num = int.Parse(segments[0].Substring(1));
		}

		for (int i = 1; i < segments.Length; i++)
		{
			if (segments[i] != "")
			{
				enemies[i - 1] = int.Parse(segments[i]);
			}
		}
	}


	public void Deploy()
	{
		//Debug.Log("deploy");

		switch (command)
		{
			case 'R':
				GameController.instance.mapMan.rate = num / 10f;
				break;
			case 'S':
				Debug.Log("Sound: " + num);
				break;
			case 'B':
				Debug.Log("Background:" + num);
				break;
			case 'T':
				Debug.Log("Title:" + num);
				break;
			default:
				break;
		}

		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i] != 0)
			{
				int type = enemies[i] / 10;
				int a = enemies[i] % 10;
				GameObject go = GameObject.Instantiate(GameController.instance.mapMan.gameIzek[type - 1].types[a]);

				go.transform.position = GameController.instance.mapMan.spawnPoints[i].transform.position;

				GameController.instance.mapMan.enyemies.Add(go);
			}
		}
	}
}

public enum CommnadType
{
	Rate,
	Sound,
	BGColor,
	Title
	//LevelChange
}
