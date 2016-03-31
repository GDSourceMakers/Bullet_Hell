using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{

	public Vector3 distance;
	public Vector3 position;

	public float speed;
	public AIType aiType;

	public RotType rotType;

	#region Catmull

	public CatmullRomType RomType;

	public List<Vector3> controlPointsList = new List<Vector3>();
		//public List<Vector3> controlPointsList = new List<Vector3>();


	public bool isLooping = true;

	float proggres;

	int lastSegmentNumber;
	float lastSegmentU;

	bool doneFlying;
	float flyingtime;

	#endregion

	#region Line

	public bool turn = false;
	public Vector3 line0;
	public Vector3 line1;

	#endregion

	#region Sin
	public float width = 1;
	public float height = 1;
	float distanceSin = 0;
	#endregion

	public Vector3 originalPos;
	public Quaternion originalRot;

	public PlayerController player;

	public bool active = true;



	// Use this for initialization
	void Start()
	{
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		if (player_go != null)
		{
			player = player_go.GetComponent<PlayerController>();
		}
		originalPos = transform.position;
		originalRot = transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		if (active)
		{
			Vector3 anglePos = Vector3.zero;
			Vector3 newpos;

			#region Movement
			switch (aiType)
			{
				case AIType.Linear:

					Vector3 delta = originalRot * new Vector3(0, -speed, 0) * Time.deltaTime;
					transform.position += delta;

					break;
				case AIType.Sinus:

					#region Sinus
					distanceSin += Time.deltaTime * speed;

					newpos = originalPos + originalRot * Quaternion.Euler(0, 0, -90) * new Vector3(distanceSin, Mathf.Sin((distanceSin * Mathf.PI) / height) * width);
					anglePos = newpos - transform.position;

					transform.position = newpos;
					#endregion

					break;
				case AIType.CatmullRom:

					#region Catmull Rom
					if (doneFlying)
					{
						int segmentNumber;
						float segmentU;

						if (RomType == CatmullRomType.Percentige)
						{
							proggres += Time.deltaTime * speed;

							if (isLooping && proggres > controlPointsList.Count)
							{
								proggres = 0;
							}

							segmentNumber = Mathf.FloorToInt(proggres);
							segmentU = proggres - segmentNumber;
						}
						else
						{

							float distanceAll = Vector3.Distance(controlPointsList[lastSegmentNumber], controlPointsList[CatmullRom.ClampListPos(lastSegmentNumber + 1, controlPointsList.Count)]);

							float distance = distanceAll * lastSegmentU;
							distance -= speed * Time.deltaTime;
							float segmentUDelta = lastSegmentU - distance / distanceAll;

							proggres += segmentUDelta;

							if (isLooping && proggres > controlPointsList.Count)
							{
								proggres = 0;
							}

							segmentNumber = Mathf.FloorToInt(proggres);
							segmentU = proggres - segmentNumber;

							lastSegmentNumber = segmentNumber;
							lastSegmentU = segmentU;

						}
						Vector3 p0;
						Vector3 p1;
						Vector3 p2;
						Vector3 p3;

						p0 = controlPointsList[CatmullRom.ClampListPos(segmentNumber - 1, controlPointsList.Count)];
						p1 = controlPointsList[segmentNumber];
						p2 = controlPointsList[CatmullRom.ClampListPos(segmentNumber + 1, controlPointsList.Count)];
						p3 = controlPointsList[CatmullRom.ClampListPos(segmentNumber + 2, controlPointsList.Count)];


						newpos = CatmullRom.ReturnCatmullRom(segmentU, p0, p1, p2, p3);

						anglePos = newpos - transform.position;

						transform.position = newpos;


					}
					else
					{
						flyingtime += Time.deltaTime * speed;
						if (isLooping)
						{
							transform.position = Vector3.Lerp(originalPos, controlPointsList[0], flyingtime);
						}
						else
						{
							transform.position = Vector3.Lerp(originalPos, controlPointsList[1], flyingtime);
						}
						if (Mathf.Round(Vector3.Distance(transform.position, controlPointsList[0])) <= 0.5)
						{
							doneFlying = true;
						}
					}
					#endregion

					break;
				case AIType.StopPos:
					break;
				case AIType.None:
					break;
				default:
					break;
			}
			#endregion

			#region Rotation
			switch (rotType)
			{
				case RotType.Velocity:
					float angle = Vector3.Angle(anglePos, Vector3.down);
					int sign = Vector3.Cross(anglePos, Vector3.down).z < 0 ? 1 : -1;

					Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, sign * angle));

					transform.rotation = newRot;

					break;
				case RotType.Palyer:
					if (player != null)
					{
						Vector3 dir = player.transform.position - transform.position;
						float angleB = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
						angleB += 90;
						Quaternion q = Quaternion.AngleAxis(angleB, Vector3.forward);
						transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
					}
					break;
				case RotType.Up:
					break;
				case RotType.Down:
					break;
				case RotType.None:
					break;
				default:
					break;
			}
			#endregion


		}
	}
}

public enum AIType
{
	Linear,
	Sinus,
	CatmullRom,
	StopPos,
	None
};

public enum CatmullRomType
{
	Distance,
	Percentige
}

public enum RotType
{
	Velocity,
	Palyer,
	Up,
	Down,
	None
};