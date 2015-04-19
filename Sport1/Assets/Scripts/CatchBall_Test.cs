using UnityEngine;
using System.Collections;

public class CatchBall_Test : MonoBehaviour {

	public GameObject ball;
	public string ballObject;


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
				if (Input.GetMouseButton (0)) {
					if ((!ballCaught)&(ballThrown)){
						CatchBall ();
					}
				}
				else
					if (ballCaught){
						DropBall ();
					}

			}
			
		}


//		if (Input.GetMouseButton (0)) {
//			print ("Touching");
//		}
//		else {
//			print ("Not Touching");
//		}



//		if (scrumHalfAlive) {
//			if (canCatch) {
//				if ((debugCatch) & (!debugBallCaught)) {
//					if ((!ballCaught)&(ballThrown)) {
//						CatchBall ();
//						debugBallCaught = true;
//					} else
//				if (Input.GetMouseButtonDown (0)) {
//						if (!ballDropped)
//							DropBall ();
//					}
//				} else
//			if (Input.GetMouseButtonDown (0)) {
//					if ((!ballCaught)&(ballThrown)){
//						CatchBall ();
//					}
//					else if (!ballDropped) {
//						DropBall ();
//					}
//				}
//			}
//
//		}
		//print (ball.name);
	}

	void CatchBall(){
		ballCaught = true;
		ballThrown = false;
		ballRB = ball.GetComponent<Rigidbody>();
		ballRB.isKinematic = true;


		ballParentTransform = ball.transform.root;

		ball.transform.parent = hands.transform;

		ball.transform.localPosition = new Vector3(ball.transform.localPosition.x,ball.transform.localPosition.y , Catch_Z_Position);
		CatchXOffset = (ball.transform.localPosition.x);
		CatchYOffset = (ball.transform.localPosition.y);
		TotalCatchOffset = Mathf.Abs(CatchXOffset) + Mathf.Abs(CatchYOffset);
		//print (TotalCatchOffset);

		ball.transform.localRotation = q_catchRot;
		onBallCaught ();
		//print ("Caught Ball");
	}

	void DropBall(){
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
				canCatch = false;
			//print ("Can Not Catch");
		}
	}
}
