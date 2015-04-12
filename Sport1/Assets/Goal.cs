using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public string ballObject;
	private GameObject ball;


	// Use this for initialization
	void Start () {
		//PrefabSpawner.onScrumhalfSpawned += ScrumHalfSpawned;
		//CatchBall_Test.onKickBallSpawned += KickBallSpawned;
		KickZone.onBallKicked += OnBallKicked;
	}

	void ScoreGoal(){
		print ("Goal Scored!!!!");
	}

	void OnBallKicked(){
		if (ballObject != null) {
			ball = GameObject.FindWithTag(ballObject);
			print (ball.name);
		}
	}

	void ScrumHalfSpawned(){

	}

	void OnTriggerEnter(Collider other) {

		////  Look at storing the kicked balls in an array 
		if (ball != null) {
			if (other.name == ball.name) 
				ScoreGoal ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
