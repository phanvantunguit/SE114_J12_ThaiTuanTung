using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

		private float fingerStartTime  = 0.0f;
		private Vector2 fingerStartPos = Vector2.zero;

		private bool isSwipe = false;
		private float minSwipeDist  = 20.0f;
		private float maxSwipeTime = 1f;

	private float HorizontalMove = 0.0f; //bien di chuyen shpere khi nguoi dung bam nut

	private int LaneNumber = 0; //cho biet vi tri hien tai cua sphere voi -1, 0, 1 lan luot la lane trai, giua, phai

	private int LockControl = 0;

	void Start() {
	}

		// Update is called once per frame
		void FixedUpdate () {
		GetComponent<Rigidbody>().velocity = new Vector3 (HorizontalMove, GameController.VerticalVelo, GameController.GameSpeed);

			if (Input.touchCount > 0){

				foreach (Touch touch in Input.touches)
				{
					switch (touch.phase)
					{
					case TouchPhase.Began :
						/* this is a new touch */
						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;

					case TouchPhase.Canceled :
						/* The touch is being canceled */
						isSwipe = false;
						break;

					case TouchPhase.Ended :

						float gestureTime = Time.time - fingerStartTime;
						float gestureDist = (touch.position - fingerStartPos).magnitude;

						if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
					{
							Vector2 direction = touch.position - fingerStartPos;
							Vector2 swipeType = Vector2.zero;

							if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
							{
								// the swipe is horizontal:
								swipeType = Vector2.right * Mathf.Sign(direction.x);
							}
							
							else
							{
								// the swipe is vertical:
								swipeType = Vector2.up * Mathf.Sign(direction.y);
							}

						if (swipeType.x != 0.0f) {
							if (swipeType.x > 0.0f) 
							{
								if (LaneNumber != 1 && HorizontalMove == 0 && LockControl != 1) {
									//move right
									HorizontalMove = 7f;
									LaneNumber = LaneNumber + 1;
									LockControl = 1;
									StartCoroutine (Stopslide ());	
								}
							}
							if (swipeType.x < 0.0f) 
							{
								if (LaneNumber != -1 && HorizontalMove == 0 && LockControl != 1)
								{
									//move left
									HorizontalMove = -7f; //di chuyen sang trai
									LaneNumber = LaneNumber - 1; //do di chuyen sang trai nen lane giam 1 don vi
									LockControl = 1;
									StartCoroutine (Stopslide ()); //doi 0,5s
								}
							}
						}
							if(swipeType.y != 0.0f )
							{
								if(swipeType.y > 0.0f)
								{
									// MOVE UP
								}
								else
								{
									// MOVE DOWN
								}
							}

						}

						break;
					}
				}
			}
			
	}
	IEnumerator Stopslide()
	{
		yield return new WaitForSeconds (0.1f); //doi 0,5s
		HorizontalMove = 0; //cho shpere di chuyen thang? tro lai
		if (LaneNumber == 0 && this.transform.position.x != 0) {
			Vector3 move = new Vector3 (0, this.transform.position.y, this.transform.position.z);
			GetComponent<Rigidbody> ().MovePosition (move);
		}
		if (LaneNumber == 1 && this.transform.position.x != 1){
			Vector3 move = new Vector3 (1, this.transform.position.y, this.transform.position.z);
			GetComponent<Rigidbody> ().MovePosition (move);
		}
		if (LaneNumber == -1 && this.transform.position.x != -1){
			Vector3 move = new Vector3 (-1, this.transform.position.y, this.transform.position.z);
			GetComponent<Rigidbody> ().MovePosition (move);
		}
		LockControl = 0;
	}
}