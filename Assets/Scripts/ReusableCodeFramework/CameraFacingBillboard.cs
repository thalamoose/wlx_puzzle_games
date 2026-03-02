using UnityEngine;
using System.Collections;

/// <summary>
/// Points the attached gameObject to face the referenced Camera.
/// </summary>
public class CameraFacingBillboard : MonoBehaviour
{
	//Enum that represents the axis direction
	public enum Axis {up, down, left, right, forward, back};
	//The camera to face
	[Tooltip("The camera for the gameObject to face, will default to the Main camera if not set")]
	Camera referenceCamera;
	/// <summary>
	/// if false then face the gameObject towards the camera, 
	/// if true then face away from the gameObject
	/// </summary>
	public bool reverseFace = false;
	/// <summary>
	/// The up axis of the gameObject.
	/// </summary>
	public Axis axis = Axis.up;

	void  Awake ()
	{
		// if no camera referenced, grab the main camera
		if (!referenceCamera)
			referenceCamera = Camera.main;
	}

	void  Update ()
	{
		// rotates the object relative to the camera
		Vector3 targetPos = transform.position + referenceCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back);
		Vector3 targetOrientation = referenceCamera.transform.rotation * GetVector3ForAxis(axis);
		transform.LookAt (targetPos, targetOrientation);
	}

	/// <summary>
	/// return a direction based upon chosen axis.
	/// </summary>
	/// <returns>The Vector3 direction.</returns>
	/// <param name="refAxis">Reference axis.</param>
	public Vector3 GetVector3ForAxis (Axis refAxis)
	{
		switch (refAxis)
		{
		case Axis.down:
			return Vector3.down; 
		case Axis.forward:
			return Vector3.forward; 
		case Axis.back:
			return Vector3.back; 
		case Axis.left:
			return Vector3.left; 
		case Axis.right:
			return Vector3.right;
		default:
			return Vector3.up;
		}
	}
}