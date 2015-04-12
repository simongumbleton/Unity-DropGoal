using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	private GameObject target;
	public string targetName;

	public bool lookAtOnStart;
	
	
	// Use this for initialization
	void Start () {
		PrefabSpawner.onScrumhalfSpawned += LookAt;
		//print ("This is " + this.gameObject.name + " Target is " + targetName);
		if (lookAtOnStart)
			LookAt ();

	}

	void OnDestroy(){
		PrefabSpawner.onScrumhalfSpawned -= LookAt;
	}
	
	void Awake(){
		
	}

	void LookAt()
	{
		//print (targetName);
		if (targetName != null) {

			target = GameObject.FindWithTag(targetName);
			transform.LookAt (target.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//	transform.LookAt(player);
	}
}
