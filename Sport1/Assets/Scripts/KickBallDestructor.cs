using UnityEngine;
using System.Collections;

public class KickBallDestructor : MonoBehaviour {

	public float lifeTime;
	private bool ApplyKickSpin = false;
	private Rigidbody rb;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartKickedBallDestruction(lifeTime));
		KickZone.onBallKicked += HandleonBallKicked;
		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 17;

		ApplyKickSpin = false;
	}

	void HandleonBallKicked ()
	{
		ApplyKickSpin = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (ApplyKickSpin) {
			rb.AddTorque (1 * rotationSpeed,0,0, ForceMode.Acceleration);
			//transform.Rotate (0,0,100*Time.deltaTime);
		}
	}

	IEnumerator StartKickedBallDestruction(float lifeTime)
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy (gameObject);
		//print ("Destroyed Kicked ball");
	}

	void OnTriggerEnter(Collider other) {
		if (ApplyKickSpin) 
			ApplyKickSpin = false;

		
	}
}
