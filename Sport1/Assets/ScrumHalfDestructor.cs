using UnityEngine;
using System.Collections;

public class ScrumHalfDestructor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		print("OnEnable this scrum half");
	}

	void OnDestroy()
	{
		print ("OnDestroy this scrum half");
	}
}
