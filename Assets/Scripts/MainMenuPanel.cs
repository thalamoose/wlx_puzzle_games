using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
	Endless = 0,
	Timed = 1,
	Campaign = 2
};

public class MainMenuPanel : MonoBehaviour
{
	public GameObject campaignLevelSelectPanel;
	public GameObject modeSelectPanel;
	public GameObject highScorePanel;
	public GameObject optionPanel;
	public GameObject howToPlayPanel;
	public GameObject timedModeIntervalSelectPanel;

	public void ButtonClickEffect() 
	{
		if (SoundManager.Instance != null)
			SoundManager.Instance.PlaySoundEffect(SoundManager.ITEM_SELECT, true);
	}

	public void ButtonBackClickEffect()
	{
		if (SoundManager.Instance != null)
			SoundManager.Instance.PlaySoundEffect (SoundManager.BACK_BUTTON, true);
	}

	public void ShowModeSelectPanel()
	{
		ButtonClickEffect ();
		modeSelectPanel.SetActive (true);
	}

	public void HideModeSelectPanel()
	{
		ButtonBackClickEffect ();
		modeSelectPanel.SetActive (false);
	}

	public void ShowOptionPanel()
	{
		ButtonClickEffect ();
		optionPanel.SetActive (true);
	}

	public void HideOptionPanel()
	{
		ButtonBackClickEffect ();
		optionPanel.SetActive (false);
	}

	public void ShowHowToPlayPanel()
	{
		ButtonClickEffect ();
		howToPlayPanel.SetActive (true);
	}

	public void HideHowToPlayPanel()
	{
		ButtonBackClickEffect ();
		howToPlayPanel.SetActive (false);
	}

	public void SetGameMode(int mode)
	{
		ButtonClickEffect ();
		ExpandedPlayerPrefs.SetInt ("GameMode", mode);
		if (mode != (int)GameMode.Timed)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene ("GameScene");
		}
		else
		{
			ShowTimedModeIntervalSelectPanel ();
		}
	}

	private void ShowTimedModeIntervalSelectPanel()
	{
		timedModeIntervalSelectPanel.gameObject.SetActive (true);
	}

	public void SetTimeInterval(int interval)
	{
		ButtonClickEffect ();
		ExpandedPlayerPrefs.SetInt ("TimeInterval", interval);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("GameScene");
	}

	public void HideTimedModeIntervalSelectPanel()
	{
		ButtonBackClickEffect ();
		timedModeIntervalSelectPanel.gameObject.SetActive (false);
	}

	public void ShowChallengeLevelSelect()
	{
		ButtonClickEffect ();
		campaignLevelSelectPanel.SetActive (true);
	}

	public void ShowHighScorePanel()
	{
		ButtonClickEffect ();
		highScorePanel.SetActive (true);
	}

	public void HideHighScorePanel()
	{
		ButtonBackClickEffect ();
		highScorePanel.SetActive (false);
	}

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}