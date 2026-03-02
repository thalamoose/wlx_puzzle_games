using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RCF
{
	/// <summary>
	/// Generic Swipe detector.
	/// </summary>
	public class SwipeDetector : MonoBehaviour 
	{
		/// <summary>
		/// The max time allowed to pass for a swipe to be considered a short swipe.
		/// </summary>
		public float maxShortSwipeTime;
		/// <summary>
		/// The minimum swipe distance allowed to be considered a short swipe in the x axis.
		/// </summary>
		protected float minShortSwipeDistX;
		/// <summary>
		/// The maximum swipe distance allowed to be considered a short swipe in the x axis.
		/// </summary>
		protected float maxShortSwipeDistX;
		/// <summary>
		/// The minimum distance allowed for a swipe to be considered a swipe in the y axis.
		/// </summary>
		protected float minSwipeDistY;
		/// <summary>
		/// The minimum distance allowed for a swipe to be considered a swipe in the x axis.
		/// </summary>
		protected float minSwipeDistX;

		protected float minSwipeDistYPercent = 0.1f;
		protected float minSwipeDistXPercent = 0.5f;
		protected float maxShortSwipeDistXPercent = 0.4f;
		protected float minShortSwipeDistXPercent = 0.03f;

		protected Vector2 startPos;
		protected float startTime;

		/// <summary>
		/// is a touch down on the screen.
		/// This will also be true when a click is active
		/// </summary>
		protected bool isATouchDown = false;

		void Start()
		{
			InitializeSwipeDetector ();
		}

		/// <summary>
		/// Initializes the swipe detector.
		/// </summary>
		protected virtual void InitializeSwipeDetector()
		{
			minSwipeDistY = Screen.height * minSwipeDistYPercent;
			minSwipeDistX = Screen.width * minSwipeDistXPercent;

			maxShortSwipeDistX = Screen.width * maxShortSwipeDistXPercent;
			minShortSwipeDistX = Screen.width * minShortSwipeDistXPercent;
		}
		
		void Update ()
		{
			if (IgnoreSwipes ())//if true then swipes will be ignored
			{
				isATouchDown = false;
			}
			else if (Input.touchCount > 0)
			{
				Touch touch = Input.touches [0];
				switch (touch.phase)
				{
				case TouchPhase.Began:	
					PotentialSwipeBegin (touch.position);
					break;
				case TouchPhase.Moved:
					CheckXValue (touch.position.x);
					break;
				case TouchPhase.Ended:
					if (isATouchDown)
					{
						float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
						float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
						bool shortSwipe = false;
						if (swipeDistHorizontal < minShortSwipeDistX && swipeDistVertical < minSwipeDistY)
						{
							shortSwipe = true;
						}
						else if (swipeDistHorizontal < maxShortSwipeDistX && swipeDistHorizontal > minShortSwipeDistX && swipeDistVertical <= minSwipeDistY
						        && Time.time - startTime <= maxShortSwipeTime)
						{
							float swipeValue = Mathf.Sign (touch.position.x - startPos.x);	
							shortSwipe = CheckShortSwipeDirection (swipeValue);
						}
						else if (swipeDistHorizontal > minSwipeDistX)
						{
							float swipeValue = Mathf.Sign (touch.position.x - startPos.x);	
							CheckLeftOrRightSwipe (swipeValue);
						}
						else if (swipeDistVertical > minSwipeDistY)
						{
							float swipeValue = Mathf.Sign (touch.position.y - startPos.y);	
							CheckUpOrDownSwipe (swipeValue);
						}
						isATouchDown = false;
					}
					break;
				}
			}
			else
			{
				if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
				{
					if (Input.GetMouseButtonDown (0))
					{
						PotentialSwipeBegin (Input.mousePosition);
					}
					if ((Input.GetAxisRaw ("Mouse X") != 0 || Input.GetAxisRaw ("Mouse Y") != 0) && Input.GetMouseButton (0))
					{
						CheckXValue (Input.mousePosition.x);
					}
					if (Input.GetMouseButtonUp (0))
					{
						if (isATouchDown)
						{
							float swipeDistVertical = (new Vector3 (0, Input.mousePosition.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
							float swipeDistHorizontal = (new Vector3 (Input.mousePosition.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
							bool shortSwipe = false;
							if (swipeDistHorizontal < minShortSwipeDistX && swipeDistVertical < minSwipeDistY)
							{
			
							}
							else if (swipeDistHorizontal < maxShortSwipeDistX && swipeDistHorizontal > minShortSwipeDistX && swipeDistVertical <= minSwipeDistY
							        && Time.time - startTime <= maxShortSwipeTime)
							{
								float swipeValue = Mathf.Sign (Input.mousePosition.x - startPos.x);
								shortSwipe = CheckShortSwipeDirection (swipeValue);
							}
							else if (swipeDistHorizontal > minSwipeDistX)
							{
								float swipeValue = Mathf.Sign (Input.mousePosition.x - startPos.x);
								CheckLeftOrRightSwipe (swipeValue);
							}
							else if (swipeDistVertical > minSwipeDistY)
							{
								float swipeValue = Mathf.Sign (Input.mousePosition.y - startPos.y);
								CheckUpOrDownSwipe (swipeValue);
							}
							isATouchDown = false;
						}
					}
				}
			}
		}

		protected void CheckXValue(float xValue)
		{
			float distance = (new Vector3 (xValue, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;
			CheckDistance (distance);
		}

		/// <summary>
		/// Determine if the swipes should be ignored or not.
		/// </summary>
		/// <returns><c>true</c>, if swipes should be ignored, <c>false</c> otherwise.</returns>
		protected virtual bool IgnoreSwipes()
		{
			return false;
		}

		private void PotentialSwipeBegin(Vector3 startPositionOfSwipe)
		{
			startPos =  startPositionOfSwipe;
			startTime = Time.time;
			isATouchDown = true;
		}

		protected void CheckDistance(float distance)
		{
			if(distance > maxShortSwipeDistX || Time.time - startTime > maxShortSwipeTime)
			{
				
			}
		}

		/// <summary>
		/// Checks the direction of the short swipe.
		/// </summary>
		/// <returns>Should always returns true</returns>
		/// <param name="swipeValue">Swipe value.</param>
		protected bool CheckShortSwipeDirection(float swipeValue)
		{
			if (swipeValue > 0)
			{
				RightShortSwipe ();
			}
			else if (swipeValue < 0)
			{
				LeftShortSwipe ();
			}
			return true;
		}

		/// <summary>
		/// Checks whether it was a left or right swipe.
		/// </summary>
		/// <param name="swipeDirection">Swipe direction.</param>
		protected void CheckLeftOrRightSwipe(float swipeDirection)
		{
			if (swipeDirection > 0)
				LeftSwipe ();
			else if (swipeDirection < 0)		
				RightSwipe ();
		}

		/// <summary>
		/// Checks whether it was an up or down swipe.
		/// </summary>
		/// <param name="swipeDirection">Swipe direction.</param>
		protected void CheckUpOrDownSwipe(float swipeDirection)
		{
			if (swipeDirection > 0)//up swipe
				UpSwipe ();
			else if (swipeDirection < 0)//down swipe
				DownSwipe ();
		}

		/// <summary>
		/// Up swipe occurs.
		/// </summary>
		public virtual void UpSwipe()
		{
			
		}

		/// <summary>
		/// Down swipe occurs.
		/// </summary>
		public virtual void DownSwipe()
		{
			
		}

		/// <summary>
		/// Left short swipe occurs.
		/// </summary>
		public virtual void LeftShortSwipe()
		{
			
		}

		/// <summary>
		/// Right short swipe occurs.
		/// </summary>
		public virtual void RightShortSwipe()
		{
			
		}

		/// <summary>
		/// Left swipe occurs.
		/// </summary>
		public virtual void LeftSwipe()
		{

		}

		/// <summary>
		/// Right swipe occurs.
		/// </summary>
		public virtual void RightSwipe()
		{

		}
	}
}