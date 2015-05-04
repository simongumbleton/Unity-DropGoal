using UnityEngine;
using System.Collections;

public class PlayerPositionManager : MonoBehaviour {

	public GameObject player;
	private Transform[] playerPositionPoints;
	private int lastPosIndex;

	// Set up delegates and events for spawning and destroying
	public delegate void PlayerMoved();
	public static event PlayerMoved onPlayerMoved;


	// Use this for initialization
	void Start () {
		PrefabSpawner.onNewBall += HandleonNewBall;




		playerPositionPoints = gameObject.GetComponentsInChildren<Transform>();
		print (playerPositionPoints.Length);
		//ScrumHalfPositions.Length = ScrumHalfPositionPoints.Length - 1;
		for (int i=1; i < playerPositionPoints.Length; i++)	// start i at 1 because the spawn collection includes the parent
		{
			print (playerPositionPoints[i].name);
			//ScrumHalfPositions[i-1] = ScrumHalfPositionPoints[i].transform.position;
		}

	
	}

	void HandleonNewBall ()
	{
		int posIndex = Random.Range (1,playerPositionPoints.Length -1);
		
		if (posIndex == lastPosIndex)
			if (posIndex == playerPositionPoints.Length - 1)
				posIndex = 1;
		else
			posIndex += 1;
		
		lastPosIndex = posIndex;


		player.transform.position = playerPositionPoints [posIndex].transform.position;
		player.transform.rotation = playerPositionPoints [posIndex].transform.rotation;
		if (onPlayerMoved != null) {
			onPlayerMoved ();
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
