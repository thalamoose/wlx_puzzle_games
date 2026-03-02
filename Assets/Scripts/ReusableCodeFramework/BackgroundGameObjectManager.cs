using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A manager class that moves objects from one side to the other, one at a time.
/// </summary>
public class BackgroundGameObjectManager : MonoBehaviour 
{
	/// <summary>
	/// The close spawn positions.
	/// </summary>
    public List<GameObject> closeSpawnPositions;
	/// <summary>
	/// The far spawn positions.
	/// </summary>
    public List<GameObject> farSpawnPositions;
	/// <summary>
	/// The game objects to possibly spawn.
	/// </summary>
	public List<GameObject> gameObjectsList;
	/// <summary>
	/// The index that the close GameObjects end at in the gameObjectsList
	/// </summary>
	[Tooltip("The index that the close GameObjects end at in the gameObjectsList")]
	public int closeGameObjectsEndingIndex = 1;
	/// <summary>
	/// The minimum time till a respawn occurs and the maximum time till a respawn occurs.
	/// </summary>
    public float minRespawnTime, maxRespawnTime;
	/// <summary>
	/// Internal representation of the time until a respawn will occur.
	/// </summary>
	private float timeTillRespawn;
	/// <summary>
	/// The timer that counts until the next spawn.
	/// </summary>
    private float timer;
    private Vector3 tempScale = Vector3.one;
	/// <summary>
	/// The index of the previous game object that was spawned.
	/// </summary>
    private int prevGameObjectIndex = -1;
	/// <summary>
	/// The current GameObject that is moving.
	/// </summary>
	private BackgroundGameObject currentMovingGameObject;

    void Start()
    {
        timeTillRespawn = Random.Range(minRespawnTime, maxRespawnTime);
    }

	void Update ()
    {
		//If there is no moving GameObject then increment the timer
        if(currentMovingGameObject == null || !currentMovingGameObject.IsObjectMoving())
            timer += Time.deltaTime;

		//If the timer is greater than the backgroundRespawnTime then spawn a new BackgroundGameObject
        if (timer >= timeTillRespawn)
        {
            SpawnBackgroundGameObject();
        }
	}

	/// <summary>
	/// Spawns a BackgroundGameObject.
	/// </summary>
	protected void SpawnBackgroundGameObject()
	{
		timer = 0;
		//Calculate a new respawn time
        timeTillRespawn = Random.Range(minRespawnTime, maxRespawnTime);

		currentMovingGameObject = GetABackgroundGameObject();
	}

	/// <summary>
	/// Gets a BackgroundGameObject from the list.
	/// </summary>
	/// <returns>A BackgroundGameObject from the list.</returns>
	protected BackgroundGameObject GetABackgroundGameObject()
	{
		int gameObjectIndex = GetAnIndexForABackgroundGameObject();
		
        GameObject gameObjectToChange = gameObjectsList[gameObjectIndex];
		BackgroundGameObject backgroundGameObjectComponent = gameObjectToChange.GetComponent<BackgroundGameObject> ();
		//if the current gameObject is below the index then spawn it in a closeSpawnPosition
		if(gameObjectIndex < closeGameObjectsEndingIndex)
			gameObjectToChange.transform.position = closeSpawnPositions[Random.Range(0, closeSpawnPositions.Count)].transform.position;
		else
			gameObjectToChange.transform.position = farSpawnPositions[Random.Range(0, farSpawnPositions.Count)].transform.position;
		gameObjectToChange.SetActive(true);

		//Set the localScale of the gameObject to cause the gameObject to be facing the correct direction
		if (gameObjectToChange.transform.position.x > 0)
		{
			backgroundGameObjectComponent.SetObjectMoving(GameObjectMovement.Direction.Left);
			tempScale = gameObjectToChange.transform.localScale;
			if (tempScale.x > 0)
				tempScale.x = -tempScale.x;
			gameObjectToChange.transform.localScale = tempScale;
		}
		else if (gameObjectToChange.transform.position.x < 0)
		{
			backgroundGameObjectComponent.SetObjectMoving(GameObjectMovement.Direction.Right);
			tempScale = gameObjectToChange.transform.localScale;
			if (tempScale.x < 0)
				tempScale.x = Mathf.Abs(tempScale.x);
			gameObjectToChange.transform.localScale = tempScale;
		}
		return backgroundGameObjectComponent;
	}

	/// <summary>
	/// Gets an index for A background game object.
	/// </summary>
	/// <returns>The index of a background game object to spawn.</returns>
	private int GetAnIndexForABackgroundGameObject()
	{
		bool foundGameObject = false;
		int gameObjectIndex = -1;
        while (!foundGameObject)
        {
            gameObjectIndex = Random.Range(0, gameObjectsList.Count);
            if (gameObjectIndex != prevGameObjectIndex)
            {
                foundGameObject = true;
                prevGameObjectIndex = gameObjectIndex;
            }
        }
		return gameObjectIndex;
	}
}