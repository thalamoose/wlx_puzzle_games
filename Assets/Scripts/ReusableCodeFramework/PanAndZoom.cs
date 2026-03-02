using UnityEngine;
using System.Collections;

/// <summary>
/// Pans and zooms the camera.
/// Only works on mobile devices.
/// </summary>
public class PanAndZoom : MonoBehaviour
{
	/// <summary>
	/// Returns true when player is pinching, false otherwise.
	/// </summary>
	public static bool isPinching = false;
	/// <summary>
	/// Returns true when player is panning, false otherwise.
	/// </summary>
	public static bool isPanning = false;
	/// <summary>
	/// The lowest height value.
	/// </summary>
	[Tooltip("The lowest height the camera can reach")]
	public int minYValue = 14;
	/// <summary>
	/// The highest heigth value.
	/// </summary>
	[Tooltip("The highest height the camera can reach")]
	public int maxYValue = 70;
	/// <summary>
	/// The speed of panning.
	/// </summary>
	private float PanSpeed = 0.075f;
	/// <summary>
	/// The speed of pinching.
	/// </summary>
	private float PinchSpeed = 10;
	/// <summary>
	/// Used to determine whether to do a pan or zoom when using two fingers
	/// </summary>
	private const float PINCH_MIN = 0.3f;
	/// <summary>
	/// The distance between touches for the previous frame.
	/// </summary>
	private Vector2 prevDist = new Vector2 (0, 0);
	/// <summary>
	/// The current distance between touches.
	/// </summary>
	private Vector2 curDist = new Vector2 (0, 0);
	/// <summary>
	/// The middle point between the touches.
	/// </summary>
	private Vector2 midPoint = new Vector2 (0, 0);
	/// <summary>
	/// The internal position storage.
	/// </summary>
	private Vector3 internalPositionStorage = Vector3.zero;

	void Update ()
	{
		if (Input.touchCount > 0)
		{
			//Check if two fingers are down
			if (Input.touchCount == 2)
			{
				isPinching = true;
				Touch touch1 = Input.GetTouch (0);
				Touch touch2 = Input.GetTouch (1);
				if ((touch1.phase == TouchPhase.Began || touch1.phase == TouchPhase.Stationary) || (touch2.phase == TouchPhase.Began || touch2.phase == TouchPhase.Stationary))
				{
					midPoint = (touch1.position + touch2.position) / 2;
				}

				// Find out how the touches have moved relative to eachother.
				curDist = touch1.position - touch2.position;
				prevDist = (touch1.position - touch1.deltaPosition) - (touch2.position - touch2.deltaPosition);

				float dot = Vector2.Dot (touch1.deltaPosition.normalized, touch2.deltaPosition.normalized);

				float touchDelta = curDist.magnitude - prevDist.magnitude;

				if (dot < -PINCH_MIN && ((Camera.main.transform.position.y > minYValue + 1 && touchDelta > 0) || Camera.main.transform.position.y < maxYValue - 1 && touchDelta < 0))
				{
					ZoomCamera (touchDelta);
				}
				else if (dot > PINCH_MIN)
				{
					PanCamera ();
				}

				ClampCameraPosition ();
			}
			// Check if only one finger is down and if it's moved, then pan the camera.
			else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved)
			{
				PanCamera ();
			}
		}
	}

	/// <summary>
	/// Clamps the camera position to prevent it from zooming in to low or out to high.
	/// </summary>
	private void ClampCameraPosition()
	{
		internalPositionStorage.x = Camera.main.transform.position.x;
		internalPositionStorage.y = Mathf.Clamp (Camera.main.transform.position.y, minYValue, maxYValue);
		internalPositionStorage.z = Camera.main.transform.position.z;
		Camera.main.transform.position = internalPositionStorage;
	}

	/// <summary>
	/// Zooms the camera in or out depending on the value of touchDelta.
	/// </summary>
	/// <param name="touchDelta">Touch delta is the change in magnitude of the current distance - the previous distance.</param>
	private void ZoomCamera(float touchDelta)
	{
		Ray ray = Camera.main.ScreenPointToRay (midPoint);
		float zoomDistance = PinchSpeed * touchDelta;
		Camera.main.transform.Translate (ray.direction * zoomDistance * Time.deltaTime, Space.World);
	}

	/// <summary>
	/// Pans the camera around.
	/// </summary>
	private void PanCamera()
	{
		isPanning = true;
		Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
		Camera.main.transform.position -= new Vector3 (touchDeltaPosition.x * PanSpeed, 0, touchDeltaPosition.y * PanSpeed);
	}
}