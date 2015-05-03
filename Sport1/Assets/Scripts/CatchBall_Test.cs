using UnityEngine;
using System.Collections;

public class CatchBall_Test : MonoBehaviour {

	public GameObject ball;
	public string ballObject;
	public Camera camera;


	public GameObject prfb_kickBall;
	private GameObject kickBall;

	private Vector3 kickBallPos;
	private Quaternion kickBallRot;
	private Rigidbody kickBallRB;

	private Rigidbody ballRB;
	public bool debugCatch;
	private bool debugBallCaught;
	private bool canCatch;
	public bool ballCaught;
	private bool ballThrown;
	private bool ballDropped;

	public string handsObject;
	private GameObject hands;

	public Vector3 CatchRoation;
	public float Catch_Z_Position;

	public float CatchXOffset;
	public float CatchYOffset;
	public float TotalCatchOffset;

	private Quaternion q_catchRot = Quaternion.identity;

	private bool scrumHalfAlive = false;

	private Transform ballParentTransform;

	private Vector2 touchStartPos;

	private Vector3 ballCatchPos;

	private Vector3 lastBallCatchPos;

	public delegate void BallCaught();
	public static event BallCaught onBallCaught;



	public delegate void SpawnedKickBall();
	public static event SpawnedKickBall onKickBallSpawned;

		


	// Use this for initialization
	void Start () {

		PrefabSpawner.onScrumhalfSpawned += ScrumHalfSpawned;
		PrefabSpawner.onScrumhalfDestroyed += ScrumHalfDestroyed;
		ThrowBall_Test.onBallThrown += BallThrown;

		debugBallCaught = false;
		canCatch = false;
		ballCaught = false;
		ballThrown = false;
		ballDropped = false;
		q_catchRot.eulerAngles = CatchRoation;

	}

	void BallThrown(){
		ballThrown = true;
		ballDropped = false;

	}

	void ScrumHalfSpawned()
	{
		if (ballObject != null) {
			ball = GameObject.FindWithTag(ballObject);
			//print (ball.name);
		}
		if (handsObject != null) {
			hands = GameObject.FindWithTag(handsObject);
		}
		scrumHalfAlive = true;
	}

	void ScrumHalfDestroyed()
	{
		scrumHalfAlive = false;
		ball = null;
		hands = null;
		debugBallCaught = false;
		canCatch = false;
		ballCaught = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (scrumHalfAlive) {
			if (canCatch) {
				if (Input.GetMouseButtonDown (0)) {
					if ((!ballCaught)&(ballThrown)){
						CatchBall ();
					}
				}
				if (ballCaught){
					if (!Input.GetMouseButton (0))
						{
							DropBall ();
						}
					else
					{
						updateTouchBallPosition ();
					}
				}

			}
			
		}
	}

	void updateTouchBallPosition ()
	{
		Vector2 touchPositionOffset = Input.GetTouch (0).position - touchStartPos;
		 // or some other distance, just don't leave it with 0f
		print (touchPositionOffset * 0.005f);
		touchPositionOffset = touchPositionOffset * 0.005f;

		float tempx = Mathf.Clamp (touchPositionOffset.x, -1.0f, 1.0f);
		float tempy = Mathf.Clamp (touchPositionOffset.y, -0.6f, 0.6f);



		tempx = ballCatchPos.x + (tempx);
		tempy = ballCatchPos.y + (tempy);

		tempx = Mathf.Lerp (lastBallCatchPos.x,tempx,0.25f);
		tempy = Mathf.Lerp (lastBallCatchPos.y,tempy,0.05f);

		ball.transform.localPosition = new Vector3(tempx,tempy , Catch_Z_Position);

		lastBallCatchPos = ball.transform.localPosition;

	}

	void CatchBall(){
		ballCaught = true;
		ballThrown = false;
		ballRB = ball.GetComponent<Rigidbody>();
		ballRB.isKinematic = true;

		touchStartPos = Input.GetTouch (0).position;
		//Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
		//print (ray);

		ballParentTransform = ball.transform.root;

		ball.transform.parent = hands.transform;

		ball.transform.localPosition = new Vector3(ball.transform.localPosition.x,ball.transform.localPosition.y , Catch_Z_Position);
		ballCatchPos = ball.transform.localPosition;
	//	CatchXOffset = (ball.transform.localPosition.x);
	//	CatchYOffset = (ball.transform.localPosition.y);
	//	TotalCatchOffset = Mathf.Abs(CatchXOffset) + Mathf.Abs(CatchYOffset);
		//print (TotalCatchOffset);

		ball.transform.localRotation = q_catchRot;
		onBallCaught ();
		//print ("Caught Ball");
	}

	void DropBall(){

		CatchXOffset = (ball.transform.localPosition.x);
		CatchYOffset = (ball.transform.localPosition.y);
		TotalCatchOffset = Mathf.Abs(CatchXOffset) + Mathf.Abs(CatchYOffset);

		ballDropped = true;
		kickBallPos = ball.transform.position;
		kickBallRot = ball.transform.rotation;
		kickBall = Instantiate (prfb_kickBall, kickBallPos, kickBallRot) as GameObject;

		if (onKickBallSpawned != null) {
			onKickBallSpawned ();
		}

		kickBallRB = kickBall.GetComponent<Rigidbody>();
		kickBallRB.isKinematic = false;

		ball.transform.parent = ballParentTransform;
		//ballRB = ball.GetComponent<Rigidbody>();
		//ballRB.isKinematic = false;
		ballCaught = false;

		Destroy (ball);



	}

	void OnTriggerEnter(Collider other) {
		//print (other.name);
		//print (ball.name);
		//print ("Can Catch = " + canCatch);
		if (ball != null) {
			//print(ball.name);
			//print (other.name);
			if (other.name == ball.name) {
				canCatch = true;
				//print ("Can Catch");
			}
		}
	}

	void OnTriggerExit(Collider other) {
		//print (other.name);
		if (ball != null)
			if (other.name == ball.name) {
				if (! ballCaught)
					canCatch = false;
			//print ("Can Not Catch");
		}
	}
}
