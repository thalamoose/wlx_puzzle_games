using UnityEngine;
using System.Collections;

/// <summary>
/// Object will move back and forth.
/// </summary>
public class Arrow : RenderableGameObject 
{
	/// <summary>
	/// if this GameObject has a rectTransform then include a reference to the RectTransform.
	/// </summary>
	[Tooltip("if this GameObject has a rectTransform then include a reference to the RectTransform.")]
	public RectTransform rectTransform;
	/// <summary>
	/// is true when the GameObject is moving towards the targetPosition,
	/// in other words when false the GameObject is moving towards the startPosition.
	/// </summary>
	private bool movingTowardsTargetPosition = true;
	/// <summary>
	/// The starting position of the GameObject.
	/// </summary>
	private Vector3 startPosition;
	/// <summary>
	/// The amount to move the rect transform by.
	/// </summary>
	private float amountToMoveByRectTransform = 150.0f;
	/// <summary>
	/// The amount to move the transform by.
	/// </summary>
	private float amountToMoveByTransform = 1.0f;
	/// <summary>
	/// The duration to complete the movement in.
	/// </summary>
	private float timeToMoveIn = 0.3f;
	/// <summary>
	/// The target position to move to.
	/// </summary>
	private Vector3 targetPosition;

	void OnEnable()
	{
		if (rectTransform != null)
			startPosition = rectTransform.anchoredPosition;
		else
		{
			startPosition = transform.position;
		}
		if (rectTransform != null)
		{
			targetPosition = startPosition + (rectTransform.right * amountToMoveByRectTransform);
			LerpRectTransformToNewPositionValue (0, targetPosition, timeToMoveIn, rectTransform, ChangeMovementDirection);
		}
		else
		{
			targetPosition = startPosition + (transform.right*amountToMoveByTransform);
			LerpTransformToNewPositionValue (0, targetPosition, timeToMoveIn, transform, ChangeMovementDirection);
		}
	}

	void OnDisable()
	{
		if (rectTransform != null)
			rectTransform.anchoredPosition = startPosition;
		else
			transform.position = startPosition;
		movingTowardsTargetPosition = true;
	}

	/// <summary>
	/// Changes the movement direction
	/// Gets called when the gameObject reaches its target position.
	/// </summary>
	protected void ChangeMovementDirection()
	{
		if(movingTowardsTargetPosition)
		{
			movingTowardsTargetPosition = false;
			if (rectTransform != null)
			{
				LerpRectTransformToNewPositionValue (0, startPosition, timeToMoveIn, rectTransform, ChangeMovementDirection);
			}
			else
			{
				LerpTransformToNewPositionValue (0, startPosition, timeToMoveIn, transform, ChangeMovementDirection);
			}
		}
		else
		{
			movingTowardsTargetPosition = true;
			if (rectTransform != null)
			{
				LerpRectTransformToNewPositionValue (0, targetPosition, timeToMoveIn, rectTransform, ChangeMovementDirection);
			}
			else
			{
				LerpTransformToNewPositionValue (0, targetPosition, timeToMoveIn, transform, ChangeMovementDirection);
			}
		}
	}
}