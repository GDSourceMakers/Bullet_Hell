using UnityEngine;
using System;
using System.Collections.Generic;


public class CatmullRom
{
	public static Vector3 ReturnCatmullRom(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Vector3 a = 0.5f * (2f * p1);
		Vector3 b = 0.5f * (p2 - p0);
		Vector3 c = 0.5f * (2f * p0 - 5f * p1 + 4f * p2 - p3);
		Vector3 d = 0.5f * (-p0 + 3f * p1 - 3f * p2 + p3);

		Vector3 pos = a + (b * t) + (c * t * t) + (d * t * t * t);

		return pos;
	}

	public static int ClampListPos(int pos, int count)
	{
		if (pos < 0)
		{
			pos = count - 1;
		}

		if (pos > count)
		{
			pos = 1;
		}
		else if (pos > count - 1)
		{
			pos = 0;
		}

		return pos;
	}

}
