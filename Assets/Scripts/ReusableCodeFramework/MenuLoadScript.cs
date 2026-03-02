using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Globalization;

/// <summary>
/// Menu load script, run it when the main menu is shown.
/// </summary>
public class MenuLoadScript : MonoBehaviour 
{
	/// <summary>
	/// Returns true if the date was retrieved successfully.
	/// </summary>
    private bool dateRequestSuccessfull = true;

	/// <summary>
	/// The URL of the server to check.
	/// </summary>
	public virtual string TIME_SERVER_URL { get {return "http://www.funkyfinger.ca/rapstime/time.php"; }}

	void Start ()
	{
		OnGameStartLoad();

		if (!ExpandedPlayerPrefs.GetBool ("FirstLaunch", false, true))
		{
			OnGameFirstLaunch ();
		}
		
		CheckInternetConnection();
	}

	/// <summary>
	/// Executes before checking date, used to set SoundManager AudioSources.
	/// </summary>
	protected virtual void OnGameStartLoad()
	{
		Application.targetFrameRate = 60;
		if(ExpandedPlayerPrefs.GetInt(CommonKeysForPlayerPrefs.MUSIC_KEY, 1) == 0)
		{
			SoundManager.Instance.GetBackgroundMusicAudioSource().enabled = false;
		}
		else if(ExpandedPlayerPrefs.GetInt(CommonKeysForPlayerPrefs.MUSIC_KEY, 1) == 1)
		{
			SoundManager.Instance.GetBackgroundMusicAudioSource().enabled = true;
		}
		
		if(ExpandedPlayerPrefs.GetInt(CommonKeysForPlayerPrefs.SFX_KEY, 1) == 0)
		{
			SoundManager.Instance.GetUIAndOtherAudioSource().enabled = false;
		}
		else if(ExpandedPlayerPrefs.GetInt(CommonKeysForPlayerPrefs.SFX_KEY, 1) == 1)
		{
			SoundManager.Instance.GetUIAndOtherAudioSource().enabled = true;
		}
		ExpandedPlayerPrefs.Save ();
        //SoundManager.Instance.PlayBackgroundMusic (SoundManager.MONKEYS_SPINNING_MONKEYS);
	}

	/// <summary>
	/// Executes when the game first launches.
	/// </summary>
	protected virtual void OnGameFirstLaunch()
	{
		ExpandedPlayerPrefs.SetBool ("FirstLaunch", true, true);
        //  ExpandedPlayerPrefs.SetBool("FirstLaunch", true);
		//	StoreInventory.GiveItem(IAPAssets.RAPSCALLIONS_CURRENCY.ItemId, 100);
        //  StoreInventory.GiveItem(IAPAssets.PARROT_ITEM.ItemId, 5);
        //  StoreInventory.GiveItem(IAPAssets.TWINE_ITEM.ItemId, 4);
        //  StoreInventory.GiveItem(IAPAssets.DAGGER_ITEM.ItemId, 4);
        //  StoreInventory.GiveItem(IAPAssets.GROG_ITEM.ItemId, 3);
        //  StoreInventory.GiveItem(IAPAssets.SPIDER_ITEM.ItemId, 2);
        //  StoreInventory.GiveItem(IAPAssets.CUTLASS_ITEM.ItemId, 2);
        //  StoreInventory.GiveItem(IAPAssets.PISTOL_ITEM.ItemId, 1);
	}

	/// <summary>
	/// Checks the internet connection.
	/// </summary>
	protected void CheckInternetConnection()
	{
		if (!IsConnectedToInternet ())
		{
			print ("No Internet Connection");
			UnsuccessfulConnection ();
		}
		else 
		{
			SuccessfullConnection ();
		}
	}

	/// <summary>
	/// Determines whether connected to the internet or not.
	/// </summary>
	/// <returns><c>true</c> if connected to internet; otherwise, <c>false</c>.</returns>
	protected virtual bool IsConnectedToInternet()
	{
		return false;
		//return Advertisement.isInitialized
	}

	/// <summary>
	/// Executes when connection to the internet was unsuccessfull.
	/// </summary>
	private void UnsuccessfulConnection()
	{
		OnLoadFinished();
	}

	/// <summary>
	/// Executes when connection to the internet was successfull.
	/// </summary>
	private void SuccessfullConnection()
    {
        StartCoroutine(CheckDate());

		OnLoadFinished();
	}

	/// <summary>
	/// Checks the current date.
	/// </summary>
    private IEnumerator CheckDate()
    {
		dateRequestSuccessfull = true;
		WWW wURLResponse = new WWW(TIME_SERVER_URL);
        yield return wURLResponse;

        if (wURLResponse.error != null)
        {
            Debug.Log("Error .. " + wURLResponse.error);
            dateRequestSuccessfull = false;
        }
        else
        {
            Debug.Log("Found ... ==>" + wURLResponse.text + "<==");
        }

        if (dateRequestSuccessfull)
        {
            OnCheckDateResponseSuccessful(wURLResponse);
        }
        else
        {
            OnCheckDateResponseUnsuccessfull(wURLResponse);
        }
    }

	/// <summary>
	/// Executes when Check date received a response.
	/// </summary>
	/// <param name="wResponse">Received web response.</param>
	protected virtual void OnCheckDateResponseSuccessful(WWW wResponse)
	{
		print("Date response found");
		int currentDay = 0;
		bool successfulParse = int.TryParse(wResponse.text, out currentDay);
		if (successfulParse && currentDay > ExpandedPlayerPrefs.GetInt("PreviousDay"))
		{
			OnNewDay ();
			print("Previous Day " + ExpandedPlayerPrefs.GetInt("PreviousDay"));
			ExpandedPlayerPrefs.SetInt("PreviousDay", currentDay);
			print(currentDay);
		}
		else
		{
			OnSameDay ();
		}
	}

	/// <summary>
	/// Executes when the time server determines it's a new day.
	/// </summary>
	protected virtual void OnNewDay()
	{
		//GameStats.Instance.SetShowBountyPanel(true);
	}

	/// <summary>
	/// Executes when the time server determines it's the same day.
	/// </summary>
	protected virtual void OnSameDay()
	{
		//GameStats.Instance.SetShowBountyPanel(false);
	}

	/// <summary>
	/// Executes when check date received an error.
	/// </summary>
	/// <param name="wResponse">Received web response.</param>
	protected virtual void OnCheckDateResponseUnsuccessfull(WWW wResponse)
	{
		print("No Date response");
	}

	/// <summary>
	/// Executes at the end after the CheckDate has occurred or 
	/// if the UnsuccessfulConnection is called.
	/// </summary>
	protected virtual void OnLoadFinished()
	{
		//if(GameStats.Instance.IsComingFromSplashScreen())
		//{
		//	menuCanvas.SetActive(false);
		//	titleScreen.SetActive(true);
		//}
		//else
		//{
		//	menuCanvas.SetActive(true);
		//	titleScreen.SetActive(false);
		//}
	}
}