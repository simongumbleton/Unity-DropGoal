using UnityEngine;
using System.Collections;

public class ScrumHalfDestructor : MonoBehaviour {


	public delegate void ScrumHalf_Awake();
	public static event ScrumHalf_Awake onScrumHalf_Awake;


	// Use this for initialization
	void Start () {

		if (onScrumHalf_Awake != null) {
			onScrumHalf_Awake ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		//print("OnEnable this scrum half");
	}

	void OnDestroy()
	{
		//print ("OnDestroy this scrum half");
	}
}
