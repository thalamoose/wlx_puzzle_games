using UnityEngine;
using System.Collections;

/// <summary>
/// A GameObject that is intended to move in the background.
/// </summary>
public class BackgroundGameObject : GameObjectMovement 
{
	/// <summary>
	/// Gets called when the GameObject reaches the left side.
	/// </summary>
    protected override void ReachedLeftSide()
    {
        isObjectMoving = false;
        gameObject.SetActive(false);
    }

	/// <summary>
	/// Gets called when the GameObject reaches the right side.
	/// </summary>
    protected override void ReachedRightSide()
    {
        isObjectMoving = false;
        gameObject.SetActive(false);
    }
}