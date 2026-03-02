using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles movement of a gameObject
/// </summary>
public class GameObjectMovement : MonoBehaviour 
{
	/// <summary>
	/// Enum to represent Direction.
	/// </summary>
    public enum Direction
    {
        Left = -1,
        NoDirection = 0,
        Right = 1
    };
	/// <summary>
	/// should use camera bounds.
	/// </summary>
    public bool shouldUseCameraBounds;
	/// <summary>
	/// The current direction of the gameObject.
	/// </summary>
    public Direction currentDirection;
	/// <summary>
	/// The speed to move at.
	/// </summary>
    public float speed;
	/// <summary>
	/// The respawn point for left side.
	/// </summary>
    public float respawnPointForLeftSide = 150;
	/// <summary>
	/// The respawn point for right side.
	/// </summary>
	public float respawnPointForRightSide = -150;
	/// <summary>
	/// Is true when the gameObject is moving, false otherwise
	/// </summary>
	protected bool isObjectMoving = false;
	/// <summary>
	/// Internal vector that stores the speed for the gameObject to move at
	/// </summary>
    private Vector2 speedVector = new Vector2(1,0);
	/// <summary>
	/// The Frustum Planes of the camera.
	/// </summary>
    private Plane[] planes;
	private SpriteRenderer gameObjectRenderer;

    void Start()
    {
		gameObjectRenderer = GetComponent<SpriteRenderer> ();
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        isObjectMoving = true;
    }

	/// <summary>
	/// Determines whether this gameObject is moving.
	/// </summary>
	/// <returns><c>true</c> if this gameObject is moving; otherwise, <c>false</c>.</returns>
    public bool IsObjectMoving()
    {
        return isObjectMoving;
    }

	/// <summary>
	/// Sets the direction to move in.
	/// </summary>
	/// <param name="movingDirection">Direction to move in.</param>
    public void SetObjectMoving(Direction movingDirection)
    {
        currentDirection = movingDirection;
		if (currentDirection != Direction.NoDirection)
			isObjectMoving = true;
		else
			isObjectMoving = false;
    }
	
	void Update () 
    {
		//Set the speedvector
		speedVector.SetX(speed * (int)currentDirection * Time.deltaTime);
		//if currentDirection != NoDirection
        if (currentDirection > 0 || currentDirection < 0)
        {
			//Move the gameObject
            gameObject.transform.Translate(speedVector);

			if (shouldUseCameraBounds && !GeometryUtility.TestPlanesAABB(planes, gameObjectRenderer.bounds))
            {
                if (currentDirection == Direction.Left)
                {
                    ReachedLeftSide();
                }
                else if (currentDirection == Direction.Right)
                {
                    ReachedRightSide();
                }
            }
            else if (!shouldUseCameraBounds)
            {
                if (currentDirection == Direction.Left && transform.position.x < respawnPointForRightSide)
                {
                    ReachedLeftSide();
                }
                else if (currentDirection == Direction.Right && transform.position.x > respawnPointForLeftSide)
                {
                    ReachedRightSide();
                }
            }
        }
	}

	/// <summary>
	/// Gets called when the GameObject reaches the left side.
	/// </summary>
    protected virtual void ReachedLeftSide()
    {
        Vector3 pos = gameObject.transform.position;
		pos.SetX (respawnPointForLeftSide);
		gameObject.transform.SetPosition (pos);
    }

	/// <summary>
	/// Gets called when the GameObject reaches the right side.
	/// </summary>
    protected virtual void ReachedRightSide()
    {
        Vector3 pos = gameObject.transform.position;
		pos.SetX (respawnPointForRightSide);
		gameObject.transform.SetPosition (pos);
    }
}