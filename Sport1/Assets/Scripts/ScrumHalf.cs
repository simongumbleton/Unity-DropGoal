using UnityEngine;
using System.Collections;

public class ScrumHalf : MonoBehaviour {
	
	
	public Transform player;
	
	
	// Use this for initialization
	void Start () {
		transform.LookAt(player);
	}
	
	void Awake(){

	}
	
	// Update is called once per frame
	void Update () {
	//	transform.LookAt(player);
	}
}
