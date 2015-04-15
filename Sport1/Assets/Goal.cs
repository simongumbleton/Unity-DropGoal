using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public string ballObject;
	private GameObject ball;
	
	public delegate void OnGoalScored();
	public static event OnGoalScored onGoalScoredEvent;

	// Use this for initialization
	void Start () {
		KickZone.onBallKicked += OnBallKicked;
	}

	void ScoreGoal(){

		if (onGoalScoredEvent != null) {
			onGoalScoredEvent ();
		}

	}


	void OnBallKicked(){
		if (ballObject != null) {
			ball = GameObject.FindWithTag(ballObject);
		}
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
