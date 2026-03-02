using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary>
/// Game over next action state.
/// Action to do after gameOver
/// </summary>
public enum GameOverNextActionState
{
	Quit = 0,
	Restart = 1,
	NextLevel = 2
};

/// <summary>
/// Used for showing a game over screen to the player
/// Look at GameOverPanel in Rapscallions for a more advanced example.
/// </summary>
public class GameOverPanel : MonoBehaviour 
{
    public InputField initialsField;

    /// <summary>
    /// References to the restart button, next Level button, and quit button.
    /// </summary>
    public Button restartButton, nextLevelButton, quitButton;
	/// <summary>
	/// Reference to active keyboard, will be null if no keyboard is currently showing.
	/// </summary>
    private TouchScreenKeyboard keyBoard;
	/// <summary>
	/// Is true when player has a new high score.
	/// </summary>
	protected bool playerHasANewHighScore = false;
	/// <summary>
	/// The next action to do after game over.
	/// </summary>
	private GameOverNextActionState currentGameOverNextActionState;

	void Awake()
	{
		GameOver ();
	}

	/// <summary>
	/// Calls when the game over panel first displays,
	/// Initializes the look of the Game Over Panel
	/// </summary>
	public virtual void GameOver()
	{
		//print out the GameStats
		Debug.Log(GameStats.Instance.PrintGameStats ());
		playerHasANewHighScore = DoesPlayerHaveANewHighScore ();
//		print ("High SCORE: " + PlayerPrefs.GetInt("HighScore5"));
//
//		score = (GameStats.Instance.GetPlayerScore());
//
//		if(score > PlayerPrefs.GetInt("HighScore5"))
//		{
//		    playerHasANewHighScore = true;
//		}
//		playerScore.text = "Score: " + string.Format ("{0:n0}", score);
	}

	/// <summary>
	/// Does the player have A new high score.
	/// override method with logic to determine if the player has a high score
	/// </summary>
	/// <returns><c>true</c>, if player has A new high score, <c>false</c> otherwise.</returns>
	protected virtual bool DoesPlayerHaveANewHighScore()
	{
		return false;
	}

	/// <summary>
	/// Tries to Quit, restart, or move to the next level. Will be unable to if the player
	/// has a new highscore, instead they will be prompted to enter their name.
	/// </summary>
	/// <param name="gameOverNextActionIntegerState">Next Action integer state.</param>
	public void Quit_0_Restart_1_Or_NextLevel_2(int gameOverNextActionIntegerState)
	{
		currentGameOverNextActionState = (GameOverNextActionState)gameOverNextActionIntegerState;

		print ("State = " + currentGameOverNextActionState); 

		if (playerHasANewHighScore)
		{
//			SoundManager.Instance.PlaySoundEffect(SoundManager.MENU_BUTTON_CLICK, false);
			SetButtonInteractible(restartButton, false);
			SetButtonInteractible(nextLevelButton, false);
			SetButtonInteractible(quitButton, false);
            //BringUpKeyboard ();
            TriggerInitialsField ();
		}
		else
		{
			ExecuteCurrentGameOverNextActionState ();
		}
	}

    protected void TriggerInitialsField()
    {
        initialsField.transform.parent.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(initialsField.gameObject);
    }

    /// <summary>
    /// Checks the status of keyboard
    /// Waits for the keyboard to dismiss
    /// When the keyboard is dismissed the player will be added to the highscore.
    /// </summary>
    public void CheckStatusOfKeyboard()
    {
        if (playerHasANewHighScore)
            AddingNameAndScoreToHighScore(initialsField.text);
    }

    /// <summary>
    /// Sets buttonToAlter's interactable property to "valueToSet".
    /// </summary>
    /// <param name="buttonToAlter">Button to alter.</param>
    /// <param name="valueToSet">the value to set buttonToAlter to.</param>
    private void SetButtonInteractible(Button buttonToAlter, bool valueToSet)
	{
		if (buttonToAlter != null)
		{
			buttonToAlter.interactable = valueToSet;
		}
		else
		{
			Debug.Log ("ButtonToAlter is null");
		}
	}

	/// <summary>
	/// Brings up keyboard.
	/// </summary>
	/*protected void BringUpKeyboard()
	{
		ShowKeyboard();
	}*/

	/// <summary>
	/// Opens the keyboard,
	/// additionally calls a coroutine to check when the keyboard gets dismissed.
	/// </summary>
	/// <param name="textToChange">Text label to change if it is not null.</param>
	/*protected void ShowKeyboard(Text textToChange = null)
	{
		if (textToChange != null)
			textToChange.SetText ("Please Wait...");
		keyBoard = TouchScreenKeyboard.Open("");
		StartCoroutine(CheckStatusOfKeyboard());
	}*/

	/// <summary>
	/// Checks the status of keyboard
	/// Waits for the keyboard to dismiss
	/// When the keyboard is dismissed the player will be added to the highscore.
	/// </summary>
	/*private IEnumerator CheckStatusOfKeyboard()
	{
		while (keyBoard != null)
		{
			if (keyBoard.done)
			{
				AddingNameAndScoreToHighScore(keyBoard.text);
				keyBoard = null;
			}
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();
	}*/

	/// <summary>
	/// Adds the name and score to the highscore table and adjusts the high score
	/// table to reflect the new scores.
	/// Override this with your own implementation, but include base.AddingNameAndScoreToHighScore(playerName)
	/// at the end of the overriden method
	/// </summary>
	/// <param name="playerName">Name of the player.</param>
	protected virtual void AddingNameAndScoreToHighScore(string playerName)
	{
//		string newName = string.Empty;
//		if (playerName.Length >= 3)
//			newName = playerName.Substring (0, 3);
//		else
//			newName = playerName;
//		int newScore = score;
//		int oldScore;
//		string oldName;
//
//		for(int i = 1; i < 6; i++)
//		{
//			if(PlayerPrefs.HasKey("HighScore"+i))
//		    {
//				if(PlayerPrefs.GetInt("HighScore"+i) < newScore)
//		        {
//					oldScore = PlayerPrefs.GetInt("HighScore"+i);
//					oldName = PlayerPrefs.GetString("Initials"+i);
//					PlayerPrefs.SetInt("HighScore"+i,newScore);
//					PlayerPrefs.SetString("Initials"+i,newName);
//		            newScore = oldScore;
//		            newName = oldName;
//		        }
//		    }
//		}
		playerHasANewHighScore = false;
		ExecuteCurrentGameOverNextActionState ();
	}

	/// <summary>
	/// Executes the currentGameOverNextActionState.
	/// </summary>
	protected void ExecuteCurrentGameOverNextActionState()
	{
		if (currentGameOverNextActionState == GameOverNextActionState.Restart)
		{
			Restart ();
		}
		else if (currentGameOverNextActionState == GameOverNextActionState.NextLevel)
		{
			NextLevel ();
		}
		else
		{
			BackToMainMenu ();
		}
	}

	/// <summary>
	/// Restarts the Level/Game
	/// </summary>
	protected virtual void Restart()
	{
		print ("Level Restarted");
		//SceneManager.LoadScene ("GameScene");
	}

	/// <summary>
	/// Starts the next level
	/// </summary>
	protected virtual void NextLevel()
	{
		print ("Going to Next Level");
	}

	/// <summary>
	/// Returns back to the menu
	/// </summary>
	protected virtual void BackToMainMenu()
	{
		print ("Quiting back to main menu");
        //SceneManager.LoadSceneAsync ("MainMenu");
	}
}