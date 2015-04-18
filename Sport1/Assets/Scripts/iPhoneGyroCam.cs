﻿using UnityEngine;
using System.Collections;

public class iPhoneGyroCam : MonoBehaviour {

	// iPhone gyroscope-controlled camera demo v0.3 8/8/11
	// Perry Hoberman <hoberman@bway.net>
	// Directions: Attach this script to main camera.
	// Note: Unity Remote does not currently support gyroscope. 
	
	private bool gyroBool;
	private Gyroscope gyro;
	private Quaternion rotFix;
	private Quaternion lastCamRot;
	public float smoothRate;
	GameObject camParent;
	DeviceOrientation orientation;

	private Quaternion StartingGyroRotation;

	private Quaternion StartingRotation;

	Quaternion offsetGyroRot;
	
	void Start() {

		StartingRotation = transform.rotation;

		Transform originalParent = transform.parent; // check if this transform has a parent
		camParent = new GameObject ("camParent"); // make a new parent
		camParent.transform.position = transform.position; // move the new parent to this transform position
		//transform.parent = camParent.transform; // make this transform a child of the new parent
		transform.SetParent (camParent.transform);
		//camParent.transform.parent = originalParent; // make the new parent a child of the original parent
		camParent.transform.SetParent (originalParent);
		lastCamRot = transform.localRotation;
		
		gyroBool = Input.isGyroAvailable;
		;
		
		if (gyroBool) {
			orientation = Input.deviceOrientation;
			print (orientation);
			gyro = Input.gyro;
			gyro.enabled = true;


			
//			if ((Screen.orientation == ScreenOrientation.LandscapeLeft)||(Screen.orientation == ScreenOrientation.LandscapeRight)) {
//				camParent.transform.eulerAngles = new Vector3(90,90,0);
//			} else if (Screen.orientation == ScreenOrientation.Portrait) {
//				camParent.transform.eulerAngles = new Vector3(90,180,0);
//			}
//			
//			if ((Screen.orientation == ScreenOrientation.LandscapeLeft)||(Screen.orientation == ScreenOrientation.LandscapeRight)) {
//				rotFix = new Quaternion(0f,0f,0.7071f,0.7071f);
//			} else if (Screen.orientation == ScreenOrientation.Portrait) {
//				rotFix = new Quaternion(0f,0f,1f,0f);
//			}
			//Screen.sleepTimeout = 0;

			if ((orientation == DeviceOrientation.LandscapeLeft)||(orientation == DeviceOrientation.LandscapeRight)) {
				camParent.transform.eulerAngles = new Vector3(90,90,180);
			} else if (orientation == DeviceOrientation.Portrait) {
				camParent.transform.eulerAngles = new Vector3(90,180,0);
			}
			
			if ((orientation == DeviceOrientation.LandscapeLeft)||(orientation == DeviceOrientation.LandscapeRight)) {
				rotFix = new Quaternion(0f,0f,1f,0f);
			} else if (orientation == DeviceOrientation.Portrait) {
				rotFix = new Quaternion(0f,0f,1f,0f);
			}


			StartingGyroRotation = gyro.attitude; //* rotFix;
			print ("Normal quat = " + StartingGyroRotation);
			print ("Inverse quat = " + Quaternion.Inverse(StartingGyroRotation));

		} else {
			print("NO GYRO");
		}
	}
	
	void Update () {
		if (gyroBool) {

			Quaternion camRot = gyro.attitude * rotFix;
			//print ("Gyro with fix = " + camRot);
			offsetGyroRot = StartingGyroRotation * camRot;
			//print ("Inverse offset = " + offsetGyroRot);
			//print ("Starting Gyro Rotation = " + StartingGyroRotation);
			transform.localRotation = camRot;
		
			lastCamRot = offsetGyroRot;

			//print(Quaternion.Dot(camRot, StartingGyroRotation));

//
//			Quaternion camRot = gyro.attitude * rotFix;
//			
//			transform.localRotation = Quaternion.Slerp(lastCamRot,camRot,smoothRate*Time.deltaTime);
//			
//			lastCamRot = camRot;



		}

		if (Input.GetKeyDown("c")){
			RecalibrateGyroCam();
		}

	}

	void OnDestroy(){
		orientation = Input.deviceOrientation;
	}

	void RecalibrateGyroCam(){

	}

}
