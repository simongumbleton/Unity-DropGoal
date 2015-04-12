using UnityEngine;
using System.Collections;

public class KickZone : MonoBehaviour {


	private Quaternion q_Rotation = Quaternion.identity;
	
	public GameObject playerCam;

	public string ballObject;
	private GameObject ball;

	public string kickPointObject;
	private GameObject kickPoint;

	public float kickedBallLifeTime;

	public bool CatchEffectsPower;
	public bool CatchEffectsDirection;
	public bool useSquare;

	private Rigidbody ballRB;
	public Vector3 kickVector;
	public float kickPower;
	public float kickHeight;
	private bool hasKicked;
	private Vector3 kickPosition;
	private float maxKickPower;
	private float kickCatchOffset;
	private float kickDirectionXOffset;
	//private CatchBall_Test catchScript;

	public delegate void BallKicked();
	public static event BallKicked onBallKicked;

	public string catchBallObject;
	private GameObject CatchBall;
	private CatchBall_Test catchScript;

	public float kickPowerModifierAmount;
	private float kickPowerCatchOffset;
	public float kickDirectionModifierAmount;



	// Use this for initialization
	void Start () {

		//PrefabSpawner.onScrumhalfSpawned += ScrumHalfSpawned;
		//PrefabSpawner.onScrumhalfDestroyed += ScrumHalfDestroyed;

		CatchBall_Test.onKickBallSpawned += KickBallSpawned;

		if (catchBallObject != null){
			CatchBall = GameObject.FindWithTag (catchBallObject);
			catchScript = CatchBall.GetComponent<CatchBall_Test> ();
		}

		hasKicked = false;
		maxKickPower = 900;
	
	}

	void ScrumHalfSpawned(){

	}

	void ScrumHalfDestroyed(){

	}

	void KickBallSpawned()
	{
		if (ballObject != null) {
			ball = GameObject.FindWithTag(ballObject);
			ballRB = ball.GetComponent<Rigidbody>();
		}
		if (kickPointObject != null) {
			kickPoint = GameObject.FindWithTag(kickPointObject);
		}
	}

	void KickBall(){

		//kickCatchOffset = catchScript.TotalCatchOffset;
		//print (kickCatchOffset);

		float newkickPower = kickPower;

		float kickPowerModifier = kickPower * kickPowerModifierAmount;
		float kickDirectionModifier = 0;

		if (CatchEffectsPower) {
			kickPowerCatchOffset = catchScript.TotalCatchOffset;
			if (kickPowerCatchOffset > 1.0f) {
				kickPowerCatchOffset = 1.0f;
			}

			if (useSquare) {
				kickPowerCatchOffset = Mathf.Pow (kickPowerCatchOffset,2);
			}

			newkickPower = kickPower - (kickPowerCatchOffset * kickPowerModifier);

		}

		kickVector = playerCam.transform.forward + playerCam.transform.up;
		kickVector.y += kickHeight;



		if (CatchEffectsDirection) {
			kickDirectionXOffset = catchScript.CatchXOffset;
			bool isNegative = false;

			float tempKickDirectionOffset = kickDirectionXOffset;

			if (kickDirectionXOffset < 0){
				isNegative = true;
				tempKickDirectionOffset = Mathf.Abs(tempKickDirectionOffset);
			}

			if (useSquare) {
				tempKickDirectionOffset = Mathf.Pow (tempKickDirectionOffset,2);
			}

			if (isNegative)
				kickDirectionXOffset = -tempKickDirectionOffset;
			else
				kickDirectionXOffset = tempKickDirectionOffset;

			kickDirectionModifier = kickDirectionXOffset * kickDirectionModifierAmount;
			kickVector.x += kickDirectionModifier;
		}


		//print (newkickPower);
		//print (kickVector.x);


		kickPosition = kickPoint.transform.position;
		//ballRB.AddForce(kickVector * kickPower, ForceMode.Acceleration);
		ballRB.AddForceAtPosition(kickVector * newkickPower,kickPosition, ForceMode.Acceleration);
		ballRB.tag = "KickedBall";
		kickPoint.tag = "Untagged";

		if (onBallKicked != null) {
			onBallKicked ();
		}

		//Invoke ("StartKickedBallDestruction(ballRB.tag)", kickedBallLifeTime);
		//StartCoroutine(StartKickedBallDestruction("KickedBall",kickedBallLifeTime));

	}

	IEnumerator StartKickedBallDestruction(string kickedBallTag, float lifeTime)
	{
		GameObject destructableBall = GameObject.FindWithTag(kickedBallTag);
		print ("Waiting for .." + lifeTime + " seconds");
		yield return new WaitForSeconds(lifeTime);
		Destroy (destructableBall);
		print ("Destroyed Kickball");
	}


	void OnTriggerEnter(Collider other) {
		if (other.name == ball.name) 
		if (!hasKicked)
				KickBall ();
		
	}


	// Update is called once per frame
	void Update () {

		//q_Rotation.eulerAngles = new Vector3 (transform.eulerAngles.x,playerCam.transform.eulerAngles.y,transform.eulerAngles.z );
		//print (playerCam.transform.localRotation.y);

		//transform.localRotation = q_Rotation;
		q_Rotation.eulerAngles = new Vector3 (0, playerCam.transform.eulerAngles.y, 0);
		transform.rotation = q_Rotation;
	
	}
}
