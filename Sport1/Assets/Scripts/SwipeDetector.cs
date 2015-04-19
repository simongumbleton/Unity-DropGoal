﻿using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour 
{
	
	public float minSwipeDistY;
	
	public float minSwipeDistX;
	
	private Vector2 startPos;
	private Vector2 endPos;

	float startTime;
	float endTime;
	float swipetime;
	float distance;

	float swipeDistVertical;
	float swipeDistHorizontal;
	float swipeVertValue;
	float swipeHorizValue;
	Vector2 deltaPos;
	float deltaTime;
	Vector2 division;

	private bool debug = true;

	protected void OnGUI()
	{
		if (!debug)
			return;
		GUILayout.BeginArea(new Rect(800, 10, 300, 300));

		
		GUILayout.Label ("swipe Dist Vertical: " + swipeDistVertical );
		GUILayout.Label ("swipe Vert Value: " + swipeVertValue);
		GUILayout.Label ("swipe Dist Horizontal: " + swipeDistHorizontal);
		GUILayout.Label ("swipe Horiz Value: " + swipeHorizValue);

		GUILayout.Label ("swipetime: " + swipetime);
		GUILayout.Label ("distance: " + distance);
		GUILayout.EndArea();
	}

	void Update()
	{
		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
			
		{
			
			Touch touch = Input.touches[0];
			
			
			
			switch (touch.phase) 
				
			{
				
			case TouchPhase.Began:
				
				startPos = touch.position;
				startTime = Time.time;
				
				break;
				
				
				
			case TouchPhase.Ended:

				endPos = touch.position;
				endTime = Time.time;
				swipetime = endTime - startTime;
				distance = Vector2.Distance(endPos,startPos);

				deltaPos = Input.touches[0].deltaPosition;
				deltaTime = Input.touches[0].deltaTime;
				division = deltaPos / deltaTime;

				swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;


				
				if (swipeDistVertical > minSwipeDistY) 
					
				{
					
					swipeVertValue = Mathf.Sign(touch.position.y - startPos.y);
					
					if (swipeVertValue > 0)//up swipe
						
						print ("Up Swipe");
						
					else if (swipeVertValue < 0)//down swipe
							
						print ("Down Swipe");
							
				}
				
				swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				
				if (swipeDistHorizontal > minSwipeDistX) 
					
				{
					
					swipeHorizValue = Mathf.Sign(touch.position.x - startPos.x);
					
					if (swipeHorizValue > 0)//right swipe
						
						print ("Right Swipe");
						
					else if (swipeHorizValue < 0)//left swipe
							
						print ("Left Swipe");
							
				}
				break;
			}
		}
	}
}