using UnityEngine;
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
	private Quaternion EndRotation;

	public Quaternion camParentRotOffset;

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
				camParent.transform.eulerAngles = new Vector3(90,270,0);
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
		
			//lastCamRot = offsetGyroRot;

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

		// Need to figure out the difference between how much the player has rotated from the starting position and apply that tranformed rotation to the camparent
		//camParent.transform.eulerAngles = new Vector3(0,0,0);

		//only works for the first calibration for some reason????????

		EndRotation = transform.rotation;
		Quaternion inverseEndRotation = Quaternion.Inverse (EndRotation);
		Quaternion camParentInitial = Quaternion.identity;
		camParentInitial.eulerAngles = new Vector3 (90,270,0);
		Quaternion calibratedCamParentRot = inverseEndRotation * camParentInitial;
		camParent.transform.rotation = calibratedCamParentRot;

		 
		//resetParent ();

		}

	void resetParent()
	{
		/////  Try removing and re-paarenting the cam parent on re-calibration
		transform.SetParent (null, true);
		GameObject oldCamParent = camParent;
		Destroy (oldCamParent);
		//	Transform originalParent = transform.parent; // check if this transform has a parent
		camParent = new GameObject ("camParent"); // make a new parent
		camParent.transform.position = transform.position; // move the new parent to this transform position
		//transform.parent = camParent.transform; // make this transform a child of the new parent
		transform.SetParent (camParent.transform);
		//camParent.transform.parent = originalParent; // make the new parent a child of the original parent
		//	camParent.transform.SetParent (originalParent);
		camParent.transform.eulerAngles = new Vector3(90,270,0);
	}

}
