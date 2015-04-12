using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

	public string ballObject;
	private GameObject ball;

	public GameObject ScoreText;
	public GameObject messageText;

	private float score = 0;
	private Text ui_scoreText;
	private Text ui_messageText;

	// Use this for initialization
	void Start () {
		//PrefabSpawner.onScrumhalfSpawned += ScrumHalfSpawned;
		//CatchBall_Test.onKickBallSpawned += KickBallSpawned;
		KickZone.onBallKicked += OnBallKicked;

		ui_scoreText = ScoreText.GetComponent<Text>();
		ui_messageText = messageText.GetComponent<Text>();
		score = 0;
		ui_scoreText.text = ("Score: " + score);
	}

	void ScoreGoal(){
		print ("Goal Scored!!!!");
		score += 1;
		ui_scoreText.text = ("Score: " + score);
		ui_messageText.text = ("Goal Scored!");

		StartCoroutine(ClearScoreMessage());


	}

	IEnumerator ClearScoreMessage()
	{
		yield return new WaitForSeconds(2);
		ui_messageText.text = ("");

	}


	void OnBallKicked(){
		if (ballObject != null) {
			ball = GameObject.FindWithTag(ballObject);
			//print (ball.name);
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
