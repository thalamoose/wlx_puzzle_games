using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Represents a Renderable game object.
/// Provides some useful methods.
/// </summary>
public class RenderableGameObject : MonoBehaviour
{
	/// <summary>
	/// The Animator component of the game object.
	/// </summary>
	[Tooltip("The Animator component of the game object.")]
	public Animator gameObjectAnimator;
	/// <summary>
	/// Internal reference to the latest running LerpRectTransformToNewPositionValue coroutine 
	/// </summary>
	private IEnumerator lerpToRectPositionCoroutine;
	/// <summary>
	/// Internal reference to the latest running LerpTransformToNewPositionValue coroutine 
	/// </summary>
	private IEnumerator lerpToPositionCoroutine;
	/// <summary>
	/// Internal reference to the latest running LerpTransformToNewRotationValue coroutine 
	/// </summary>
	private IEnumerator lerpToRotationCoroutine;
	/// <summary>
	/// Internal reference to the latest running LerpTransformToNewScaleValue coroutine 
	/// </summary>
	private IEnumerator lerpToScaleCoroutine;

	/// <summary>
	/// Plays the animation.
	/// </summary>
	/// <param name="animationName">Animation name.</param>
	/// <param name="animationLayer">Layer the animation is on.</param>
	/// <param name="normalizedTime">Normalized time of the animation, the point to start in the animation.</param>
	public void PlayAnimation(string animationName, int animationLayer = -1, float normalizedTime = float.NegativeInfinity)
	{
		if(gameObjectAnimator != null)
			gameObjectAnimator.Play (animationName, animationLayer, normalizedTime);
	}

	/// <summary>
	/// Cross fades into the animation.
	/// </summary>
	/// <param name="timeToWait">Time to wait til starting the crossfade.</param>
	/// <param name="crossFadeDuration">duration of the Cross fade.</param>
	/// <param name="animationName">Name of the animation to crossfade into.</param>
	public void CrossFadeAnimation(float timeToWait, float crossFadeDuration, string animationName)
	{
		StartCoroutine (CrossFadeIntoNewAnimationAfter (timeToWait, crossFadeDuration, animationName));
	}

	/// <summary>
	/// Internal coroutine Representation that executes CrossFadeAnimation().
	/// </summary>
	/// <param name="timeToWait">Time to wait til starting the crossfade.</param>
	/// <param name="crossFadeDuration">duration of the Cross fade.</param>
	/// <param name="animationName">Name of the animation to crossfade into.</param>
	private IEnumerator CrossFadeIntoNewAnimationAfter(float timeToWait, float crossFadeDuration, string animationName)
	{
		yield return new WaitForSeconds (timeToWait);
		gameObjectAnimator.CrossFade (animationName, crossFadeDuration);
	}

	/// <summary>
	/// Lerps the rect transform to new position value.
	/// </summary>
	/// <param name="waitTime">time to wait until movement starts.</param>
	/// <param name="newPositionValue">New position to move to.</param>
	/// <param name="duration">Duration of the movement.</param>
	/// <param name="targetTransform">Target transform to move.</param>
	/// <param name="onCompletion">Method to call after movement completes.</param>
	public void LerpRectTransformToNewPositionValue(float waitTime, Vector3 newPositionValue, float duration, RectTransform targetTransform, Action onCompletion = null)
	{
		lerpToRectPositionCoroutine = LerpToNewRectPosition (waitTime, newPositionValue, duration, targetTransform, onCompletion);
		StartCoroutine (lerpToRectPositionCoroutine);
	}

	/// <summary>
	/// Internal coroutine representation that executes LerpRectTransformToNewPositionValue().
	/// </summary>
	/// <param name="waitTime">time to wait until movement starts.</param>
	/// <param name="newPositionValue">New position to move to.</param>
	/// <param name="duration">Duration of the movement.</param>
	/// <param name="targetTransform">Target transform to move.</param>
	/// <param name="onCompletion">Method to call after movement completes.</param>
	private IEnumerator LerpToNewRectPosition(float waitTime, Vector3 newPositionValue, float duration, RectTransform targetTransform, Action onCompletion)
	{
		float elapsedTime = 0;
		Vector3 startingPos = targetTransform.anchoredPosition3D;
		while(waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if(waitTime <= 0)
		{
			while(elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				targetTransform.anchoredPosition3D = Vector3.Lerp (startingPos, newPositionValue, (elapsedTime/duration));
				yield return new WaitForEndOfFrame();
			}
			if (onCompletion != null)
				onCompletion ();
		}
	}

	/// <summary>
	/// Stops the lerp to rect position coroutine.
	/// </summary>
	protected void StopLerpToRectPositionCoroutine()
	{
		StopCoroutine (lerpToRectPositionCoroutine);
	}

	/// <summary>
	/// Lerps the transform to new position value.
	/// </summary>
	/// <param name="waitTime">time to wait until movement starts.</param>
	/// <param name="newPositionValue">New position to move to.</param>
	/// <param name="duration">Duration of the movement.</param>
	/// <param name="targetTransform">Target transform to move.</param>
	/// <param name="onCompletion">Method to call after movement completes.</param>
	public void LerpTransformToNewPositionValue(float waitTime, Vector3 newPositionValue, float duration, Transform targetTransform, Action onCompletion = null)
	{
		lerpToPositionCoroutine = LerpToNewPosition (waitTime, newPositionValue, duration, targetTransform, onCompletion);
		StartCoroutine (lerpToPositionCoroutine);
	}

	/// <summary>
	/// Internal coroutine representation that executes LerpTransformToNewPositionValue().
	/// </summary>
	/// <param name="waitTime">time to wait until movement starts.</param>
	/// <param name="newPositionValue">New position to move to.</param>
	/// <param name="duration">Duration of the movement.</param>
	/// <param name="targetTransform">Target transform to move.</param>
	/// <param name="onCompletion">Method to call after movement completes.</param>
	private IEnumerator LerpToNewPosition(float waitTime, Vector3 newPositionValue, float duration, Transform targetTransform, Action onCompletion)
	{
		float elapsedTime = 0;
		Vector3 startingPos = targetTransform.position;
		while(waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if(waitTime <= 0)
		{
			while(elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				targetTransform.SetPosition (Vector3.Lerp (startingPos, newPositionValue, (elapsedTime / duration)));
				yield return new WaitForEndOfFrame();
			}
			if (onCompletion != null)
				onCompletion ();
		}
	}

	/// <summary>
	/// Stops the lerp to position coroutine.
	/// </summary>
	protected void StopLerpToPositionCoroutine()
	{
		StopCoroutine (lerpToPositionCoroutine);
	}

	/// <summary>
	/// Lerps the transform rotation to new rotation value.
	/// </summary>
	/// <param name="waitTime">time to wait until rotation starts.</param>
	/// <param name="newRotationValue">New rotation to rotate to.</param>
	/// <param name="duration">Duration of the rotation.</param>
	/// <param name="targetTransform">Target transform to rotate.</param>
	/// <param name="onCompletion">Method to call after rotation completes.</param>
	public void LerpTransformToNewRotationValue(float waitTime, Quaternion newRotationValue, float duration, Transform targetTransform, Action onCompletion = null)
	{
		lerpToRotationCoroutine = SlerpToNewRotation (waitTime, newRotationValue, duration, targetTransform, onCompletion);
		StartCoroutine (lerpToRotationCoroutine);
	}

	/// <summary>
	/// Internal coroutine representation that executes LerpTransformToNewRotationValue().
	/// </summary>
	/// <param name="waitTime">time to wait until rotation starts.</param>
	/// <param name="newRotationValue">New rotation to rotate to.</param>
	/// <param name="duration">Duration of the rotation.</param>
	/// <param name="targetTransform">Target transform to rotate.</param>
	/// <param name="onCompletion">Method to call after rotation completes.</param>
	private IEnumerator SlerpToNewRotation(float waitTime, Quaternion newRotationValue, float duration, Transform targetTransform, Action onCompletion)
	{
		float elapsedTime = 0;
		Quaternion startingRot = targetTransform.rotation;
		while(waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if(waitTime <= 0)
		{
			while(elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				targetTransform.rotation = Quaternion.Slerp (startingRot, newRotationValue, (elapsedTime/duration));
				yield return new WaitForEndOfFrame();
			}
			if (onCompletion != null)
				onCompletion ();
		}
	}

	/// <summary>
	/// Stops the lerp to rotation coroutine.
	/// </summary>
	protected void StopLerpToRotationCoroutine()
	{
		StopCoroutine (lerpToRotationCoroutine);
	}

	/// <summary>
	/// Lerps the transform scale to a new scale value.
	/// </summary>
	/// <param name="waitTime">time to wait until scaling starts.</param>
	/// <param name="newScaleValue">New value to scale to.</param>
	/// <param name="duration">Duration of the scaling.</param>
	/// <param name="targetTransform">Target transform to scale.</param>
	/// <param name="onCompletion">Method to call after scaling completes.</param>
	public void LerpTransformToNewScaleValue(float waitTime, Vector3 newScaleValue, float duration, Transform targetTransform, Action onCompletion = null)
	{
		lerpToScaleCoroutine = LerpToNewScale (waitTime, newScaleValue, duration, targetTransform, onCompletion);
		StartCoroutine (lerpToScaleCoroutine);
	}

	/// <summary>
	/// Internal coroutine representation that executes LerpTransformToNewScaleValue().
	/// </summary>
	/// <param name="waitTime">time to wait until scaling starts.</param>
	/// <param name="newScaleValue">New value to scale to.</param>
	/// <param name="duration">Duration of the scaling.</param>
	/// <param name="targetTransform">Target transform to scale.</param>
	/// <param name="onCompletion">Method to call after scaling completes.</param>
	protected IEnumerator LerpToNewScale(float waitTime, Vector3 newScaleValue, float duration, Transform targetTransform, Action onCompletion)
	{
		float elapsedTime = 0;
		Vector3 startingScale = targetTransform.localScale;
		while(waitTime > 0)
		{
			waitTime -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if(waitTime <= 0)
		{
			while(elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				targetTransform.localScale = Vector3.Lerp (startingScale, newScaleValue, (elapsedTime/duration));
				yield return new WaitForEndOfFrame();
			}
			if (onCompletion != null)
				onCompletion ();
		}
	}

	/// <summary>
	/// Stops the lerp to scale coroutine.
	/// </summary>
	protected void StopLerpToScaleCoroutine()
	{
		StopCoroutine (lerpToScaleCoroutine);
	}
}