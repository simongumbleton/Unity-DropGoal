using UnityEngine;
using System.Collections;

public class NewThrow : MonoBehaviour {

	public float Range;
	public float rangeOffset = 0;
	public float Angle = 65;
	public float thrust = 20;
	public Vector3 ballSpin;
	public Transform player;
	private Rigidbody rb;
	private bool hasCaught = false;


	public CatchBall_Test catchScript;
	
	void Start ()
	{
		rb = this.gameObject.GetComponent<Rigidbody>();

		Range = Vector3.Distance (player.position, transform.position);

		// Calculate trajectory velocity
		//Vector3 v = new Vector3(Mathf.Cos(Angle * Mathf.Deg2Rad), Mathf.Sin(Angle * Mathf.Deg2Rad), 0);
		//rb.velocity = v * Mathf.Sqrt(Range * Physics.gravity.magnitude / Mathf.Sin(2*Angle*Mathf.Deg2Rad));

		Vector3 v = new Vector3(0,0, 20);

		rb.velocity = v;// * Mathf.Sqrt(Range * Physics.gravity.magnitude / Mathf.Sin(2*Angle*Mathf.Deg2Rad));

	}

	void Update()
	{
		if (catchScript.ballCaught)
			hasCaught = true;
		//print (hasCaught);
		//if (!hasCaught) 
		//	rb.AddTorque(ballSpin,ForceMode.Force);
		//else
			//rb.angularVelocity = new Vector3(0,0,0);
	}
}
