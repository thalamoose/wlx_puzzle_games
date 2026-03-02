using UnityEngine;
using System.Collections;
using RCF;

namespace RCF
{
	/// <summary>
	/// Different states of the UI.
	/// </summary>
	public enum GameUIState
	{
		Normal = 0,
		Paused = 1
	};
}

/// <summary>
/// Manages the UI when playing in game (not to be used for main menu).
/// </summary>
public class UIManager : MonoBehaviour
{
	/// <summary>
	/// A reference to the pause panel.
	/// </summary>
	public GameObject pausePanel;
	/// <summary>
	/// Static instance of the UIManager.
	/// </summary>
	protected static UIManager instance = null;
	/// <summary>
	/// The state of the UIManager.
	/// </summary>
	private GameUIState uiManagerState = GameUIState.Normal;

	private float timeScaleBeforePause = 0;

	/// <summary>
	/// Returns the static instance of the UIManager.
	/// </summary>
	/// <value>The instance.</value>
	public static UIManager Instance
	{
		get { return instance; }
	}

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
	}
	
	void Start () 
	{
		OnInitializeUI ();
	}

	void Update () 
	{
		OnUpdateUI ();
	}

	/// <summary>
	/// Gets the state of the UIManager.
	/// </summary>
	/// <returns>The UIManager state.</returns>
	public GameUIState GetUIManagerState()
	{
		return uiManagerState;
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	public virtual void PauseGame()
	{
		uiManagerState = GameUIState.Paused;
		timeScaleBeforePause = Time.timeScale;
		Time.timeScale = 0;
		if (pausePanel != null)
		{
			pausePanel.SetActive (true);
		}
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	public virtual void UnPauseGame()
	{
		uiManagerState = GameUIState.Normal;
		Time.timeScale = timeScaleBeforePause;
		if (pausePanel != null)
		{
			pausePanel.SetActive (false);
		}
	}

	/// <summary>
	/// initializes the UI.
	/// </summary>
	protected virtual void OnInitializeUI()
	{
		uiManagerState = GameUIState.Normal;
	}

	/// <summary>
	/// Updates the UI.
	/// </summary>
	protected virtual void OnUpdateUI()
	{
		CheckBackButtonPressed ();
	}

	/// <summary>
	/// Virtual method that will reset the time scale
	/// could be overridden to send the user back to the
	/// main menu.
	/// Can be called when the player, for example, quits from the game
	/// from the pause panel.
	/// </summary>
	public virtual void BackToMainMenuFromPause()
	{
		Time.timeScale = 1;
	}

	/// <summary>
	/// Checks if the back button was pressed on android.
	/// </summary>
	private void CheckBackButtonPressed()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				OnAndroidBackButtonPressed ();
			}
		}
	}

	/// <summary>
	/// Gets called when the user presses the back button
	/// on Android.
	/// </summary>
	protected virtual void OnAndroidBackButtonPressed()
	{
		
	}
}