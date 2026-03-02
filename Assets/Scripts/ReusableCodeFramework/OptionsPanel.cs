using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum OptionState
{
	LoadFromStart,
	OptionChangeOccurred
}

/// <summary>
/// Used for showing an options screen to the player.
/// </summary>
public class OptionsPanel : MonoBehaviour 
{
	/// <summary>
	/// music button toggle, sfx button toggle, subtitle button toggle.
	/// </summary>
	public Button musicToggle, sfxToggle, subtitleToggle;
	/// <summary>
	/// List of buttons to represent each language button.
	/// </summary>
	public List<Button> languageButtons;
	/// <summary>
	/// Reference to a credits panel if there is one.
	/// </summary>
	[Tooltip("Reference to a credits panel if there is one.")]
	public GameObject creditsPanel;

	/// <summary>
	/// Set the options to show the correctly enabled buttons.
	/// </summary>
	public virtual void Start () 
	{
		if (musicToggle != null)
		{
			if (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.MUSIC_KEY, 1) == 1)
			{
				OnMusicOptionIsOn (OptionState.LoadFromStart);
			}
			else
			{
				OnMusicOptionIsOff (OptionState.LoadFromStart);
			}
		}

		if (sfxToggle != null)
		{
			if (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.SFX_KEY, 1) == 1)
			{
				OnSFXOptionIsOn (OptionState.LoadFromStart);
			}
			else
			{
				OnSFXOptionIsOff (OptionState.LoadFromStart);
			}
		}

		if (subtitleToggle != null)
		{
			if (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.SUBTITLES_KEY, 0) == 1)
			{
				OnSubtitlesOptionIsOn (OptionState.LoadFromStart);
			}
			else
			{
				OnSubtitlesOptionIsOff (OptionState.LoadFromStart);
			}
		}

		if (languageButtons.Count > 0)
		{
			SetLanguage (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.LANGUAGE_KEY, 0));
		}
	}

	/// <summary>
	/// Executes when the music option is on.
	/// </summary>
	protected virtual void OnMusicOptionIsOn(OptionState state)
	{
		
	}

	/// <summary>
	/// Executes when the music option is off.
	/// </summary>
	protected virtual void OnMusicOptionIsOff(OptionState state)
	{

	}

	/// <summary>
	/// Executes when the sfx option is on.
	/// </summary>
	protected virtual void OnSFXOptionIsOn(OptionState state)
	{

	}

	/// <summary>
	/// Executes when the sfx option is off.
	/// </summary>
	protected virtual void OnSFXOptionIsOff(OptionState state)
	{

	}

	/// <summary>
	/// Executes when the subtitle option is on.
	/// </summary>
	protected virtual void OnSubtitlesOptionIsOn(OptionState state)
	{

	}

	/// <summary>
	/// Executes when the subtitles option is off.
	/// </summary>
	protected virtual void OnSubtitlesOptionIsOff(OptionState state)
	{

	}

	/// <summary>
	/// Shows the credits panel.
	/// </summary>
	public void ShowCreditsPanel()
	{
		if(creditsPanel != null)
			creditsPanel.SetActive (true);
	}

	/// <summary>
	/// Hides the credits panel.
	/// </summary>
	public void HideCreditsPanel()
	{
		if(creditsPanel != null)
			creditsPanel.SetActive (false);
	}

	/// <summary>
	/// Changes the music option value.
	/// </summary>
	public virtual void ChangeMusic()
	{
		if (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.MUSIC_KEY,1) == 1)
		{
			OnMusicOptionIsOff (OptionState.OptionChangeOccurred);
			ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.MUSIC_KEY, 0);
		}
		else
		{
			OnMusicOptionIsOn (OptionState.OptionChangeOccurred);
			ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.MUSIC_KEY, 1);
		}
	}

	/// <summary>
	/// Changes the sfx option value.
	/// </summary>
	public virtual void ChangeSFX()
	{
		if (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.SFX_KEY,1) == 1)
		{
			OnSFXOptionIsOff (OptionState.OptionChangeOccurred);
			ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.SFX_KEY, 0);
		}
		else
		{
			OnSFXOptionIsOn (OptionState.OptionChangeOccurred);
			ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.SFX_KEY, 1);
		}
	}

	/// <summary>
	/// Changes the subtitles options value.
	/// </summary>
	public virtual void ChangeSubtitles()
	{
		if (ExpandedPlayerPrefs.GetInt (CommonKeysForPlayerPrefs.SUBTITLES_KEY) == 1)
		{
			OnSubtitlesOptionIsOff (OptionState.OptionChangeOccurred);
			ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.SUBTITLES_KEY, 0);
		}
		else
		{
			OnSubtitlesOptionIsOn (OptionState.OptionChangeOccurred);
			ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.SUBTITLES_KEY, 1);
		}
	}

	/// <summary>
	/// Sets the active language.
	/// </summary>
	/// <param name="languageToSetTo">integer value of the language to set to.</param>
	public virtual void SetLanguage(int languageToSetTo)
	{
		ExpandedPlayerPrefs.SetInt (CommonKeysForPlayerPrefs.LANGUAGE_KEY, languageToSetTo);
	}
}