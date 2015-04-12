using UnityEngine;
using System.Collections;

public class KickBallDestructor : MonoBehaviour {

	public float lifeTime;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartKickedBallDestruction(lifeTime));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartKickedBallDestruction(float lifeTime)
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy (gameObject);
		//print ("Destroyed Kicked ball");
	}
}
