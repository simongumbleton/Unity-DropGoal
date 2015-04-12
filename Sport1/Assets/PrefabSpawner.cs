using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour {

	public GameObject prfb_scrumHalf;
	public Vector3[] ScrumHalfPositions = new Vector3[4]; 
	
	public GameObject spawnedScrumHalf;

	public delegate void SpawnedScrumHalf();
	public static event SpawnedScrumHalf onScrumhalfSpawned;
	public static event SpawnedScrumHalf onScrumhalfDestroyed;

	public string catchZoneObject = "CatchZone";
	private GameObject CatchZone;
	private CatchBall_Test catchScript;

	private int lastPosIndex = 0;

	private bool canDestroy = true;


	// Use this for initialization
	void Start () {

		if (catchZoneObject != null){
			CatchZone = GameObject.FindWithTag (catchZoneObject);
			catchScript = CatchZone.GetComponent<CatchBall_Test> ();
		}
		CatchBall_Test.onBallCaught += OnBallCaught;
		KickZone.onBallKicked += OnBallKicked;
	}

	void OnBallCaught(){
		canDestroy = false;
	}
	void OnBallKicked(){
		canDestroy = true;
	}



	void SpawnPrefabs(){

		int posIndex = Random.Range (0,ScrumHalfPositions.Length);


		if (posIndex == lastPosIndex)
			if (posIndex == ScrumHalfPositions.Length - 1)
				posIndex = 0;
			else
				posIndex += 1;

		lastPosIndex = posIndex;

		print (posIndex);
		spawnedScrumHalf = Instantiate (prfb_scrumHalf, ScrumHalfPositions [posIndex], Quaternion.identity) as GameObject ;
		//print ("Spawned Prefab");
		//Instantiate (prfb_Player, PlayerPositions [0], Quaternion.identity);
		if (onScrumhalfSpawned != null) {
			onScrumhalfSpawned ();
		}
		//print ("Spawned Prefab Event Invoked");

	}

	void DestroyPrefabs()
	{
		Destroy (spawnedScrumHalf);
		//print ("Destroyed Object");

		if (catchScript.ballCaught) {
			Destroy(catchScript.ball);
		}

		if (onScrumhalfDestroyed != null)
			onScrumhalfDestroyed ();
		//print ("Destroy Event Invoked");
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (1)) {
			if (canDestroy){
				DestroyPrefabs ();
		//		print ("Destroy Prefabs Called");
				Invoke ("SpawnPrefabs", 1);
			}
		}
	
	}
}
