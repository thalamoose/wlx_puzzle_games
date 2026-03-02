using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedOptionsPanel : OptionsPanel
{
	public Image musicButtonImage;
	public Image sfxButtonImage;
	public Sprite musicOn, musicOff, sfxOn, sfxOff;

	protected override void OnMusicOptionIsOff(OptionState state)
	{
		musicButtonImage.sprite = musicOff;
		if (state == OptionState.OptionChangeOccurred)
		{
			SoundManager.Instance.PlaySoundEffect(SoundManager.ITEM_SELECT, false);
			SoundManager.Instance.GetBackgroundMusicAudioSource().enabled = false;
		}
	}

	protected override void OnMusicOptionIsOn(OptionState state)
	{
		musicButtonImage.sprite = musicOn;
		if (state == OptionState.OptionChangeOccurred)
		{
			SoundManager.Instance.PlaySoundEffect(SoundManager.ITEM_SELECT, false);
			SoundManager.Instance.GetBackgroundMusicAudioSource().enabled = true;
			SoundManager.Instance.GetBackgroundMusicAudioSource().Play();
		}
	}

	protected override void OnSFXOptionIsOff(OptionState state)
	{
		sfxButtonImage.sprite = sfxOff;
		if (state == OptionState.OptionChangeOccurred)
		{
			SoundManager.Instance.GetUIAndOtherAudioSource().enabled = false;
		}
	}

	protected override void OnSFXOptionIsOn(OptionState state)
	{
		sfxButtonImage.sprite = sfxOn;
		if (state == OptionState.OptionChangeOccurred)
		{
			SoundManager.Instance.GetUIAndOtherAudioSource().enabled = true;
			SoundManager.Instance.PlaySoundEffect(SoundManager.ITEM_SELECT, false);
		}
	}
}
