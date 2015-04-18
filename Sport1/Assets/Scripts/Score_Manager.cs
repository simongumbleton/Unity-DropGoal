using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour {


	public GameObject ScoreText;
	public GameObject MessageText;
	public float GoalMessageDisplayTime = 2.0f;

	private float score = 0;
	private Text ui_scoreText;
	private Text ui_messageText;

	// Use this for initialization
	void Start () {

		Goal.onGoalScoredEvent += HandleonGoalScoredEvent;

		ui_scoreText = ScoreText.GetComponent<Text>();
		ui_messageText = MessageText.GetComponent<Text>();
		score = 0;
		ui_scoreText.text = ("Score: " + score);
	
	}

	void HandleonGoalScoredEvent ()
	{
		print ("Score Manager::Goal Scored!!!!");

		score += 1;
		ui_scoreText.text = ("Score: " + score);
		ui_messageText.text = ("Goal Scored!");
		StartCoroutine(ClearScoreMessage());

	}

	IEnumerator ClearScoreMessage()
	{
		yield return new WaitForSeconds(GoalMessageDisplayTime);
		ui_messageText.text = ("");
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
