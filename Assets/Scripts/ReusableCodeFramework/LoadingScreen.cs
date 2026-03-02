using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Generic Loading screen.
/// </summary>
public class LoadingScreen : MonoBehaviour
{
	/// <summary>
	/// if true then loadingText will update while loading the scene.
	/// </summary>
	[Tooltip("if true then loadingText will update while loading the scene.")]
	public bool allowTypingTextOnLoadingScreen;
	/// <summary>
	/// The text to alter while loading the scene.
	/// </summary>
	public Text loadingText;
	/// <summary>
	/// static instance of LoadingScreen.
	/// </summary>
	private static LoadingScreen instance;
	/// <summary>
	/// reference to gameobject loadingscreen gameobject.
	/// </summary>
	private GameObject loadingScreenGameObject;
	/// <summary>
	/// The string to write out as the scene is loading and allowTypingTextOnLoadingScreen is true.
	/// </summary>
	private const string loadingString = "Loading...";
	/// <summary>
	/// Reference to the current scene load operation.
	/// </summary>
	private AsyncOperation currentSceneLoadOperation = null;

	void Awake()
	{
		//find the LoadingCanvas gameobject from the Hierarchy
		loadingScreenGameObject = GameObject.Find("LoadingCanvas");
		//destroy the already existing instance, if any
		if (instance)
		{
			Destroy(gameObject);
			Hide();     //call hide function to hide the loading screen
			return;
		}
		instance = this;
		instance.loadingScreenGameObject.SetActive(false);
		DontDestroyOnLoad(this.gameObject);  //make this object persistent between scenes
	}

	/// <summary>
	/// Coroutine that types out the text while the scene is loading.
	/// </summary>
	IEnumerator TypeText()
	{
		char[] loadingStringCharArray = loadingString.ToCharArray ();
		for (int i = 0; i < loadingStringCharArray.Length; i++)
		{
			loadingText.text += loadingStringCharArray [i];
			yield return new WaitForSeconds (0.1f);
		}
		loadingText.SetText ("");
		yield return null;
	}

	/// <summary>
	/// Show the Loading screen.
	/// </summary>
	/// <param name="sceneLoadOperation">the operation to start runnning.</param>
	public static void Show(AsyncOperation sceneLoadOperation)
	{
		//if instance does not exists return from this function
		if (!DoesAnInstanceExist()) 
		{
			return;
		}
		instance.currentSceneLoadOperation = sceneLoadOperation;
		//enable the loading image object 
		instance.loadingScreenGameObject.SetActive(true);
		instance.StartCoroutine (instance.ShowLoadingScreenCoroutine ());
	}

	/// <summary>
	/// Handles showing the coroutine and when to hide it
	/// </summary>
	private IEnumerator ShowLoadingScreenCoroutine()
	{
		if(instance.allowTypingTextOnLoadingScreen)
			StartCoroutine (TypeText ());
		while (!instance.currentSceneLoadOperation.isDone)
		{
			yield return null;
		}
		Hide ();
	}

	/// <summary>
	/// Hide the Loading screen.
	/// </summary>
	public static void Hide()
	{
		if (!DoesAnInstanceExist()) 
		{
			return;
		}
		instance.loadingScreenGameObject.SetActive(false);
	}

	/// <summary>
	/// Method to check if the instance already exists
	/// </summary>
	/// <returns><c>true</c>, if an instance exists, <c>false</c> otherwise.</returns>
	static bool DoesAnInstanceExist()
	{
		if (!instance)
		{
			return false;
		}
		return true;
	}
}