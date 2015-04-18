using UnityEngine;
using System.Collections;

public class TiltLook : MonoBehaviour {
	
	private Gyroscope gyro;
	
	void Start ()

	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;
		}
		//Input.compensateSensors = false;
	}
	

	// Update is called once per frame
	void Update () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) || (Input.deviceOrientation == DeviceOrientation.LandscapeRight))
		transform.localRotation = Input.gyro.attitude;

	}
}
