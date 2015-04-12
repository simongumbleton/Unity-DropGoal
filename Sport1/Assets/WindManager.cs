using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindManager : MonoBehaviour {

	public string kickedBallTag = "KickedBall";

	public GameObject windArrow;
	public GameObject windArrowMesh;
	private Vector3 relativeArrowDirection;


	public bool enableWind;
	public Vector3 windDirection;
	public float windStrength;

	public float MaxWindStrength = 3.0f;

	private Vector3 relativeWindDirection;

	public List<GameObject> windEffectedObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {

		//GenerateNewWind ();
		PrefabSpawner.onScrumhalfSpawned += GenerateNewWind;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateNewWind()
	{
		windStrength = Random.Range (0.0f, MaxWindStrength);

		windDirection = new Vector3 ((Random.Range(-1.5f,1.5f)),0.0f,(Random.Range(-1.0f,1.0f)));

		print ("Wind direction " + windDirection);

		relativeWindDirection = transform.InverseTransformDirection(windDirection);

		relativeArrowDirection = windArrow.transform.position + windDirection;

		print ("Arrow direction " + relativeArrowDirection);

		MeshRenderer arrowRenderer = windArrowMesh.GetComponent<MeshRenderer>();
		Color arrowColor = arrowRenderer.material.color;
		arrowColor.a = windStrength /  MaxWindStrength;
		if (arrowColor.a < 0.1f) {
			arrowColor.a = 0.1f;
		}

		arrowRenderer.material.color = arrowColor;

		//relativeArrowDirection = new Vector3 (relativeArrowDirection.x, windArrow.transform.position.y, relativeArrowDirection.z);

		windArrow.transform.LookAt (relativeArrowDirection);


	}

	void FixedUpdate(){

		if (windEffectedObjects.Count >= 1) {
			for(int cnt = 0; cnt < windEffectedObjects.Count; cnt++)
			{
				if (windEffectedObjects[cnt] != null){
					//print (windEffectedObjects[cnt]);
					Rigidbody objRB = windEffectedObjects[cnt].GetComponent<Rigidbody>();

					objRB.AddForce(relativeWindDirection * windStrength , ForceMode.Acceleration);

				}

			}


		}

	}


	void addGameObjectToList(GameObject obj){

		windEffectedObjects.Add (obj);

	}

	void removeGameObjectToList(GameObject obj){
		windEffectedObjects.Remove (obj);
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == kickedBallTag) {
			addGameObjectToList(other.gameObject);
		}

		
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == kickedBallTag) {
			removeGameObjectToList(other.gameObject);
		}

		
	}

}
