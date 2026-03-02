using UnityEngine;
using System.Collections;

namespace RCF
{
	/// <summary>
	/// Enum that contains the different states the Ray Caster can be in.
	/// </summary>
	public enum RayCasterState
	{
		Idle,
		TouchDown,
		TouchDrag,
		TouchLetGo
	};

	/// <summary>
	/// Handles ray casting
	/// </summary>
	public class RayCaster : MonoBehaviour 
	{
		/// <summary>
		/// The mask that the raycast hits are applied to.
		/// </summary>
		[Tooltip("The mask that the raycast hits are applied to.")]
		public LayerMask mask;
		/// <summary>
		/// Whether or not dragging is enabled
		/// </summary>
		[Tooltip("Whether or not dragging is enabled")]
		public bool draggingEnabled;
		/// <summary>
		/// The resulting hit from ray cast.
		/// </summary>
		protected RaycastHit resultingHitFromRayCast;
		/// <summary>
		/// The collider that was detected on touch down.
		/// </summary>
		protected Collider colliderOnTouchDown;
		/// <summary>
		/// The collider that was detected on touch drag.
		/// </summary>
		protected Collider colliderOnTouchDrag;
		/// <summary>
		/// The collider that was detected on touch let go.
		/// </summary>
		protected Collider colliderOnTouchLetGo;
		/// <summary>
		/// Ray to perform ray cast with.
		/// </summary>
		protected Ray rayToPerformRayCastWith;
		/// <summary>
		/// The current state of the ray caster.
		/// </summary>
		protected RayCasterState currentRayCasterState;

		void Awake()
		{
			currentRayCasterState = RayCasterState.Idle;
		}

		void Update ()
		{
			// if ignore raycasts is false then perform the raycast
			if (!IgnoreRayCasts ())
			{
				if(Input.touchCount > 0)
					rayToPerformRayCastWith = Camera.main.ScreenPointToRay (Input.touches[0].position);
				else
					rayToPerformRayCastWith = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Began))
				{
					TouchDown ();
				}
				else if (draggingEnabled && (Input.GetMouseButton (0) || (Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Moved)))
				{
					TouchDrag ();
				}
				else if (Input.GetMouseButtonUp (0))
				{
					TouchLetGo ();
				}
			}
		}

		/// <summary>
		/// Determines if ray casts should be ignored
		/// </summary>
		/// <returns><c>true</c>, when ray casts should be ignored, <c>false</c> otherwise.</returns>
		protected virtual bool IgnoreRayCasts()
		{
			return false;
		}

		/// <summary>
		/// Calls when a TouchDown event occurs.
		/// </summary>
		protected virtual void TouchDown()
		{
			print ("Touch Down");
			CancelInvoke ("ResetStateToIdle");
			colliderOnTouchDrag = colliderOnTouchLetGo = null;
			colliderOnTouchDown = PerformRayCast ();
			currentRayCasterState = RayCasterState.TouchDown;
		}

		/// <summary>
		/// Calls when a TouchDrag event occurs.
		/// </summary>
		protected virtual void TouchDrag()
		{
			print ("Touch Drag");
			colliderOnTouchDown = colliderOnTouchLetGo = null;
			colliderOnTouchDrag = PerformRayCast ();
			currentRayCasterState = RayCasterState.TouchDrag;
		}

		/// <summary>
		/// Calls when a TouchLetGo event occurs.
		/// </summary>
		protected virtual void TouchLetGo()
		{
			print ("Touch Let Go");
			colliderOnTouchDown = colliderOnTouchDrag = null;
			colliderOnTouchLetGo = PerformRayCast();
			currentRayCasterState = RayCasterState.TouchLetGo;
			Invoke ("ResetStateToIdle", 0.2f);
		}

		/// <summary>
		/// Resets the state to idle.
		/// </summary>
		private void ResetStateToIdle()
		{
			print ("Back To Idle");
			currentRayCasterState = RayCasterState.Idle;
			colliderOnTouchLetGo = null;
		}

		/// <summary>
		/// Performs the ray cast.
		/// </summary>
		/// <returns>The collider of what the raycast hit.</returns>
		protected virtual Collider PerformRayCast()
		{
			if(Physics.Raycast(rayToPerformRayCastWith, out resultingHitFromRayCast, Mathf.Infinity, mask))
			{
				RayCastHitDetected(resultingHitFromRayCast);
			}
			return resultingHitFromRayCast.collider;
		}

		/// <summary>
		/// Executes when the ray cast hits something.
		/// </summary>
		/// <param name="receivedRayCastHit">Received ray cast hit.</param>
		protected virtual void RayCastHitDetected(RaycastHit receivedRayCastHit)
		{
			
		}
	}
}