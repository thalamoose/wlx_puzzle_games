using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages all sound for the game
/// Note: Do not use the sound manager in awake functions
/// as the manager may not have been initialized by then
/// therefore it could trigger null exceptions.
/// </summary>
public class SoundManager : MonoBehaviour
{
	/// <summary>
	/// Background Music
	/// </summary>
	public const string MAIN_MENU_BACKGROUND_MUSIC = "Ouroboros";
	public const string IN_GAME_BACKGROUND_MUSIC = "Latin Industries";

	/// <summary>
	/// Front-End & Menus
	/// </summary>
	public const string ITEM_SELECT = "MenuButton";
	public const string GENERIC_BUTTON = "020_generic_button";
	public const string BACK_BUTTON = "BackButton";

	public const string CLOCK_TICK = "Tick";

	public const string CANNON_LAUNCH = "CandyLaunch";
	public const string NORMAL_BLOCK_DESTROYED = "DestroyBlock";
	public const string TIME_FOR_NEXT_ROW = "BlocksDown";
	public const string DESTROY_ROW = "DestroyRowColumn";
	public const string DESTROY_3X3 = "Destroy3x3";
	public const string ADD_PIECE_PICKUP = "PickupCollectedCandyPiece";
	public const string MULTIPLIER_PICKUP = "PickupCollectedMultiplier";
	public const string FIREBALL_PICKUP = "PickupCollectedFireball";

	public const string BLOCK_DESTROYED_BY_FIREBALL = "DestroyRowColumn";
	public const string GAME_OVER = "GameOver";

	/// <summary>
	/// List of ui and other sound effect clips
	/// </summary>
	public List<AudioClip> uiAndOtherSounds;
	/// <summary>
	/// List of background music audio clips
	/// </summary>
	public List<AudioClip> backgroundMusic;
	/// <summary>
	/// The AudioSource that plays ui and other sound effects
	/// </summary>
	public AudioSource uiAndOtherSource;
	/// <summary>
	/// The AudioSource that plays background music
	/// </summary>
	public AudioSource backgroundMusicSource;

	private static SoundManager instance = null;
	/// <summary>
	/// Internal dictionary of ui and other sound clips
	/// used for lookup optimization
	/// </summary>
	private Dictionary<string,AudioClip> uiAndOtherSoundsDictionary;
	/// <summary>
	/// Internal dictionary of background music AudioClips
	/// used for lookup optimization
	/// </summary>
	private Dictionary<string,AudioClip> backgroundMusicDictionary;

	/// <summary>
	/// Static reference to the SoundManager class
	/// </summary>
	public static SoundManager Instance
	{
		get { return instance; }
	}

	void Awake ()
	{
		if (instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
		{
			InitializeSoundManager ();

		}
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// Initializes the sound manager.
	/// </summary>
	private void InitializeSoundManager()
	{
		instance = this;
		//Make the dictionaries
		uiAndOtherSoundsDictionary = new Dictionary<string, AudioClip> ();
		backgroundMusicDictionary = new Dictionary<string, AudioClip> ();
		for (int i = 0; i < uiAndOtherSounds.Count; i++)
		{
			uiAndOtherSoundsDictionary.Add (uiAndOtherSounds [i].name, uiAndOtherSounds [i]);
		}

		for (int i = 0; i < backgroundMusic.Count; i++)
		{
			backgroundMusicDictionary.Add (backgroundMusic [i].name, backgroundMusic [i]);
		}
	}

	/// <summary>
	/// Gets the BackgroundMusicAudioSource
	/// used for easily affecting properties of the AudioSource such 
	/// as volume and enabling or disabling it.
	/// </summary>
	/// <returns>The background music AudioSource.</returns>
	public AudioSource GetBackgroundMusicAudioSource ()
	{
		return backgroundMusicSource;
	}

	/// <summary>
	/// Gets the uiAndOtherSource
	/// used for easily affecting properties of the AudioSource such
	/// as volume and enabling or disabling it.
	/// </summary>
	/// <returns>The ui and other AudioSource.</returns>
	public AudioSource GetUIAndOtherAudioSource ()
	{
		return uiAndOtherSource;
	}

	/// <summary>
	/// Plays the named background AudioClip
	/// </summary>
	/// <param name="audioClipName">Name of the Background AudioClip.</param>
	public void PlayBackgroundMusic (string audioClipName)
	{
		if (backgroundMusicSource.enabled && backgroundMusicSource.clip != null && backgroundMusicSource.clip.name == audioClipName)
		{
			backgroundMusicSource.Play ();
		}
		else
		{
			AudioClip sound = GetBackgroundMusic (audioClipName);
			if (sound != null)
			{
				backgroundMusicSource.clip = sound;
				if (backgroundMusicSource.enabled)
					backgroundMusicSource.Play ();
			}
			else
			{
				Debug.LogError ("backgroundmusic was not found");
			}
		}
	}

	/// <summary>
	/// Gets the background music AudioClip.
	/// Returns null if the clip does not exist
	/// </summary>
	/// <returns>The background music AudioClip.</returns>
	/// <param name="name">Name of the AudioClip.</param>
	private AudioClip GetBackgroundMusic (string name)
	{
		return backgroundMusicDictionary[name];
	}

	/// <summary>
	/// Plays the provided AudioClip after the delay
	/// </summary>
	/// <param name="clipToPlay">The clip to play after the delay.</param>
	/// <param name="playOneShot">If set to <c>true</c> play one shot, plays the sound effect on a separate AudioSource.</param>
	/// <param name="delay">Time to wait until playing the sound effect.</param>
	public void PlaySoundEffectClip(AudioClip clipToPlay, bool playOneShot, float delay = 0.0f)
	{
		if (uiAndOtherSource.enabled)
		{
			if (clipToPlay != null)
			{
				if (!playOneShot && uiAndOtherSource.clip != null && uiAndOtherSource.clip == clipToPlay)
				{
					uiAndOtherSource.Play ();
				}
				else
				{
					if (clipToPlay != null)
					{
						if (delay > 0.0f)
						{
							StartCoroutine (PlaySoundEffectWithDelay (clipToPlay, playOneShot, delay));
						}
						else if (playOneShot)
						{
							uiAndOtherSource.PlayOneShot (clipToPlay);
						}
						else
						{
							uiAndOtherSource.PlayClip (clipToPlay);
						}
					}
					else
					{
						Debug.LogError ("Sound was not found");
					}
				}
			}
		}
		else
		{
			print ("CLIP IS NULL");
		}
	}

	/// <summary>
	/// Similar to the PlaySoundEffectClip method except this method takes a AudioClip name as a parameter
	/// and finds the audio clip and then plays it
	/// </summary>
	/// <param name="name">Name of the sound effect to play after the delay.</param>
	/// <param name="playOneShot">If set to <c>true</c> play one shot, plays the sound effect on a separate AudioSource.</param>
	/// <param name="delay">Time to wait until playing the sound effect.</param>
	public void PlaySoundEffect (string name, bool playOneShot, float delay = 0.0f)
	{
		if (uiAndOtherSource.enabled)
		{

			if (!playOneShot && uiAndOtherSource.clip != null && uiAndOtherSource.clip.name == name)
			{
				uiAndOtherSource.Play ();
			}
			else
			{
				AudioClip soundEffect = GetAudio (name);
				if (soundEffect != null)
				{
					if (delay > 0.0f)
					{
						StartCoroutine (PlaySoundEffectWithDelay (soundEffect, playOneShot, delay));
					}
					else if (playOneShot)
					{
						uiAndOtherSource.PlayOneShot (soundEffect);
					}
					else
					{
						uiAndOtherSource.PlayClip (soundEffect);
					}
				}
				else
				{
					Debug.LogError ("Sound was not found");
				}
			}
		}
	}

	/// <summary>
	/// This is an Internal helper method for PlaySoundEffect().
	/// </summary>
	/// <param name="soundEffect">Sound effect to play.</param>
	/// <param name="playOneShot">If set to <c>true</c> play one shot, plays the sound effect on a separate AudioSource.</param>
	/// <param name="delay">Time to wait until playing the sound effect.</param>
	private IEnumerator PlaySoundEffectWithDelay (AudioClip soundEffect, bool playOneShot, float delay)
	{
		yield return new WaitForSeconds (delay);
		if (playOneShot)
		{
			uiAndOtherSource.PlayOneShot (soundEffect);
		}
		else
		{
			uiAndOtherSource.PlayClip (soundEffect);
		}
	}

	/// <summary>
	/// Gets the named AudioClip.
	/// Returns null if the clip does not exist
	/// </summary>
	/// <returns>The AudioClip.</returns>
	/// <param name="name">Name of the AudioClip.</param>
	public AudioClip GetAudio (string name)
	{
		return uiAndOtherSoundsDictionary[name];
	}
}