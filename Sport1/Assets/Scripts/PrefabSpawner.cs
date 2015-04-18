using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrefabSpawner : MonoBehaviour {

	public GameObject prfb_scrumHalf;	//The scrumhalf prefab to spawn
//	public Vector3[] ScrumHalfPositions;	// = new Vector3[4]; // Array of positions for scrumHalf to spawn at
	
	private GameObject spawnedScrumHalf;	// A ref to hold the spawned instance of scrumHalf
	
	private Transform[] ScrumHalfPositionPoints;


	// Set up delegates and events for spawning and destroying
	public delegate void SpawnedScrumHalf();
	public static event SpawnedScrumHalf onScrumhalfSpawned;
	public static event SpawnedScrumHalf onScrumhalfDestroyed;

	//Set up ref to the CatchZone
	public string catchZoneObject = "CatchZone";
	private GameObject CatchZone;
	private CatchBall_Test catchScript;

	private int lastPosIndex = 0;

	private bool canDestroy = true;

	public GameObject timerUI;
	public float timer = 0.0f;
	private bool SnapTimer = false;
	private Text timerText;


	// Use this for initialization
	void Start () {

		if (catchZoneObject != null){
			CatchZone = GameObject.FindWithTag (catchZoneObject);
			catchScript = CatchZone.GetComponent<CatchBall_Test> ();
		}
		CatchBall_Test.onBallCaught += OnBallCaught;
		KickZone.onBallKicked += OnBallKicked;
		ScrumHalfDestructor.onScrumHalf_Awake += HandleonScrumHalf_Awake;
		ThrowBall_Test.onBallThrown += HandleonBallThrown;

		GameObject spawnPointCollection = GameObject.FindWithTag("SpawnPoints");
		ScrumHalfPositionPoints = spawnPointCollection.GetComponentsInChildren<Transform>();
		print (ScrumHalfPositionPoints.Length);
		//ScrumHalfPositions.Length = ScrumHalfPositionPoints.Length - 1;
		for (int i=1; i < ScrumHalfPositionPoints.Length; i++)	// start i at 1 because the spawn collection includes the parent
		{
			print (ScrumHalfPositionPoints[i].name);
			//ScrumHalfPositions[i-1] = ScrumHalfPositionPoints[i].transform.position;
		}
		timerText = timerUI.GetComponent<Text>();
		timerText.text = "";

	}

	void HandleonBallThrown ()
	{
		SnapTimer = true;

	}

	void HandleonScrumHalf_Awake ()
	{
		canDestroy = true;
	}

	void OnBallCaught(){

		canDestroy = false;

	}
	void OnBallKicked(){

		canDestroy = true;
		SnapTimer = false;
		timer = 0.0f;
		//timerText.text = "";
	}



	void SpawnPrefabs(){

		int posIndex = Random.Range (1,ScrumHalfPositionPoints.Length -1);

		if (posIndex == lastPosIndex)
			if (posIndex == ScrumHalfPositionPoints.Length - 1)
				posIndex = 1;
			else
				posIndex += 1;

		lastPosIndex = posIndex;


		spawnedScrumHalf = Instantiate (prfb_scrumHalf, ScrumHalfPositionPoints[posIndex].transform.position, Quaternion.identity) as GameObject ;

		if (onScrumhalfSpawned != null) {
			onScrumhalfSpawned ();
		}

		SnapTimer = false;
		timer = 0.0f;
		timerText.text = "";

	}

	void DestroyPrefabs()
	{
		Destroy (spawnedScrumHalf);


		if (catchScript.ballCaught)
			Destroy(catchScript.ball);


		if (onScrumhalfDestroyed != null)
			onScrumhalfDestroyed ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (1)) {
			if (canDestroy){
				DestroyPrefabs ();
				canDestroy = false;
				Invoke ("SpawnPrefabs", 1);
				print("SpawnPrefabs Triggered");

			}
		}
		if (SnapTimer) {
			timer += Time.deltaTime;
			timerText.text = timer.ToString();
		}
	}
}
