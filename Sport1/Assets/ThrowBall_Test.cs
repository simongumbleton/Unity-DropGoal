using UnityEngine;
using System.Collections;

public class ThrowBall_Test : MonoBehaviour {
	public float rotationSpeed;
	private Rigidbody rb;

	public string playerObject;
	private GameObject player;

	private Vector3 playerPosition;
	private Vector3 ballPosition;
	public float throwHeightOffset;
	public bool Wait;
	public float waitTime;
	private float timer;
	private bool thrown;
	public float angle = 20;

	private Vector3 targetPos;
	private Vector3 targetPosNorm;
		
	public Vector3 ballSpin;
	private bool hasCaught = false;
	private bool stopSpin = false;

	public delegate void BallThrown();
	public static event BallThrown onBallThrown;




	//public CatchBall_Test catchScript;


	// Use this for initialization
	void Start () {

		if (playerObject != null) {
			player = GameObject.FindWithTag(playerObject);
		}

		CatchBall_Test.onBallCaught += BallCaught;
		rb = this.gameObject.GetComponent<Rigidbody>();
		hasCaught = false;
		thrown = false;
		timer = 0.0f;
		playerPosition = player.transform.position;
		ballPosition = transform.position;
	}

	void Awake() {

	}

	void BallCaught(){
		hasCaught = true;
	}


	void ThrowBall(){


		rb.WakeUp ();
		//rb.AddForce(playerPosition * thrust, ForceMode.Acceleration);
		thrown = true;


		/////////////
		Vector3 dir = playerPosition - ballPosition;
		float h = dir.y;
		dir.y = 0;
		float dist = dir.magnitude;
		float a = angle * Mathf.Deg2Rad;
		dir.y = dist * Mathf.Tan (a);
		dist += h / Mathf.Tan (a);
		float vel = Mathf.Sqrt (dist * Physics.gravity.magnitude / Mathf.Sin (2 * a));
		Vector3 velocity = vel * dir.normalized;

		rb.velocity = velocity;

		if (onBallThrown != null) {
			onBallThrown ();
		}



	}

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		if (!thrown) {
			rb.Sleep ();
		}

		if ((!Wait) & (!thrown)) {
			ThrowBall ();
		}
		if ((Wait) & (!thrown)) {
			if (timer > waitTime)
				ThrowBall ();
			else
				return;
			
		}

			
		//print (hasCaught);
		if ((!hasCaught) & (thrown)) {
			rb.AddRelativeTorque (Vector3.right * rotationSpeed, ForceMode.VelocityChange);
			//rb.AddTorque (ballSpin, ForceMode.VelocityChange);
		}
		else
			if (!stopSpin) {
			rb.angularVelocity = new Vector3 (0, 0, 0);
			stopSpin = true;
			}
			else
				return;


	
	}
}

