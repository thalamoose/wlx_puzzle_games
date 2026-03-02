using UnityEngine;
using System.Collections;

/// <summary>
/// Generic Controller for social elements in game
/// i.e leaderboards, achievements, signing in and out
/// </summary>
public class SocialController
{
	private static SocialController instance = null;

	/// <summary>
	/// Gets a static reference to the SocialController 
	/// </summary>
	public static SocialController Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new SocialController();
			}
			return instance;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SocialController"/> class.
	/// </summary>
	private SocialController()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			InitializeSocialControllerAndroid ();
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			InitializeSocialControlleriOS ();
		}
		else
		{
			InitializeSocialControllerUnsupportedPlatform ();
		}
	}

	/// <summary>
	/// Initializes the social controller - Android implementation.
	/// </summary>
	protected virtual void InitializeSocialControllerAndroid()
	{
//		PlayGamesPlatform.DebugLogEnabled = true;
//		PlayGamesPlatform.Activate ();
	}

	/// <summary>
	/// Initializes the social controller - iOS implementation.
	/// </summary>
	protected virtual void InitializeSocialControlleriOS()
	{
		
	}

	/// <summary>
	/// Initializes the social controller - Unsupported Platform implementation.
	/// </summary>
	protected virtual void InitializeSocialControllerUnsupportedPlatform()
	{
		
	}

	/// <summary>
	/// Signs the user in
	/// </summary>
	/// <returns><c>true</c>, if signed in successfully, <c>false</c> otherwise.</returns>
	public bool SignIn()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			return SignInAndroid ();
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return SignIniOS ();
		}
		else
		{
			return SignInUnsupportedPlatform ();
		}
	}

	/// <summary>
	/// Sign in - Android implementation.
	/// </summary>
	/// <returns><c>true</c>, if signed in successfully, <c>false</c> otherwise.</returns>
	protected virtual bool SignInAndroid()
	{
//		if(!PlayGamesPlatform.Instance.localUser.authenticated)
//		{
//			PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
//			{
//		        result = success;
//				if(success)
//				{
//					Debug.Log ("We're signed in! Welcome " + PlayGamesPlatform.Instance.localUser.userName);
//				}
//				else
//				{
//					Debug.Log("Oh... we're not signed in.");
//				}
//			});
//		}
//		else
//		{
//		    result = true;
//			Debug.Log("You're already signed in.");
//		}
		return false;
	}

	/// <summary>
	/// Sign in - iOS implementation.
	/// </summary>
	/// <returns><c>true</c>, if signed in successfully, <c>false</c> otherwise.</returns>
	protected virtual bool SignIniOS()
	{
//		if(!GameCenterManager.IsInitialized)
//			GameCenterManager.Init();
//		result = true;
		return false;
	}

	/// <summary>
	/// Sign in - Unsupported Platforms implementation.
	/// </summary>
	/// <returns><c>true</c>, if signed in successfully, <c>false</c> otherwise.</returns>
	protected virtual bool SignInUnsupportedPlatform()
	{
		Debug.LogWarning ("Can't sign in - Not a supported Platform");
		return false;
	}

	/// <summary>
	/// Determines whether the user is authenticated.
	/// </summary>
	/// <returns><c>true</c> if this user is authenticated; otherwise, <c>false</c>.</returns>
	public bool IsAuthenticated()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			return IsAuthenticatedAndroid ();
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return IsAuthenticatediOS ();
		}
		else
		{
			return isAuthenticatedUnsupportedPlatform ();
		}
	}

	/// <summary>
	/// isAuthenticated - Android implemention.
	/// </summary>
	/// <returns><c>true</c> if this user is authenticated; otherwise, <c>false</c>.</returns>
	protected virtual bool IsAuthenticatedAndroid()
	{
//  		return PlayGamesPlatform.Instance.localUser.authenticated;
		return false;
	}

	/// <summary>
	/// isAuthenticated - iOS implemention.
	/// </summary>
	/// <returns><c>true</c> if this user is authenticated; otherwise, <c>false</c>.</returns>
	protected virtual bool IsAuthenticatediOS()
	{
//		return GameCenterManager.IsPlayerAuthenticated;
		return false;
	}

	/// <summary>
	/// isAuthenticated - Unsupported Platforms implemention.
	/// </summary>
	/// <returns><c>true</c>, if this user is authenticated, <c>false</c> otherwise.</returns>
	protected virtual bool isAuthenticatedUnsupportedPlatform()
	{
		Debug.LogWarning ("Can't authenticate - Not a supported Platform");
		return false;
	}

	/// <summary>
	/// Increments the achievement.
	/// Used for quantity based achievements
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	/// <param name="amount">Amount to increment by.</param>
	public void IncrementAchievement(string achievementId, int amount)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			IncrementAchievementAndroid (achievementId, amount);
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			IncrementAchievementiOS (achievementId, amount);
		}
		else
		{
			IncrementAchievementUnsupportedPlatform (achievementId, amount);
		}
	}

	/// <summary>
	/// IncrementAchievement - Android Implementation.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	/// <param name="amount">Amount to increment by.</param>
	protected virtual void IncrementAchievementAndroid(string achievementId, int amount)
	{
//		PlayGamesPlatform.Instance.IncrementAchievement (achievementId, amount, (bool success) => 
//		{
//			if(!success)
//			{
//				Debug.Log ("Increment PROblem!!!!!!");
//			}
//		});
	}

	/// <summary>
	/// IncrementAchievement - iOS Implementation.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	/// <param name="amount">Amount to increment by.</param>
	protected virtual void IncrementAchievementiOS(string achievementId, int amount)
	{
//		float percentageToAdd = 0.0f;
//		if(achievementId == AchievementsIds.achievement_davy_jones_lunchbox)
//			percentageToAdd = 2.0f*amount;
//		else if(achievementId == AchievementsIds.achievement_davy_jones_locker)
//			percentageToAdd = amount;
//		else if(achievementId == AchievementsIds.achievement_davy_jones_walkin_closet)
//			percentageToAdd = 0.4f*amount;
//		else if(achievementId == AchievementsIds.achievement_davy_jones_garage)
//			percentageToAdd = 0.2f*amount;
//		GameCenterManager.SubmitAchievement((GameCenterManager.GetAchievementProgress(achievementId) + percentageToAdd), achievementId, true);
	}

	/// <summary>
	/// IncrementAchievement - Unsupported Platform Implementation.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	/// <param name="amount">Amount to increment by.</param>
	protected virtual void IncrementAchievementUnsupportedPlatform(string achievementId, int amount)
	{
		Debug.LogWarning ("Can't increment achievement - Not a supported Platform");
	}

	/// <summary>
	/// Unlocks the specified achievement.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	public void UnlockAchievement(string achievementId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			UnlockAchievementAndroid (achievementId);
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			UnlockAchievementiOS (achievementId);
		}
		else
		{
			UnlockAchievementUnsupportedPlatform (achievementId);
		}
	}

	/// <summary>
	/// UnlockAchievement - Android implementation.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	protected virtual void UnlockAchievementAndroid(string achievementId)
	{
		Social.ReportProgress(achievementId, 100.0f, (bool success) => 
		{
			if(!success)
			{
				Debug.Log ("Achievement unlocking was unsuccesfull");
			}
		});
	}

	/// <summary>
	/// UnlockAchievement - iOS implementation.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	protected virtual void UnlockAchievementiOS(string achievementId)
	{
//		GameCenterManager.SubmitAchievement(100.0f, achievementId, true);
	}

	/// <summary>
	/// UnlockAchievement - Unsupported Platforms implementation.
	/// </summary>
	/// <param name="achievementId">Achievement identifier.</param>
	protected virtual void UnlockAchievementUnsupportedPlatform(string achievementId)
	{
		
	}

	/// <summary>
	/// Determines whether the achievement with the specified achievementId is unlocked.
	/// </summary>
	/// <returns><c>true</c> if this achievement is unlocked; otherwise, <c>false</c>.</returns>
	/// <param name="achievementId">Achievement identifier.</param>
	public bool IsAchievementUnlocked(string achievementId)
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			return IsAchievementUnlockedAndroid (achievementId);
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return IsAchievementUnlockediOS (achievementId);
		}
		else
		{
			return IsAchievementUnlockedUnsupportedPlatform (achievementId);
		}
	}

	/// <summary>
	/// IsAchievementUnlocked - Android implementation
	/// </summary>
	/// <returns><c>true</c> if this achievement is unlocked; otherwise, <c>false</c>.</returns>
	/// <param name="achievementId">Achievement identifier.</param>
	protected virtual bool IsAchievementUnlockedAndroid(string achievementId)
	{
//		if(!PlayGamesPlatform.Instance.localUser.authenticated)
//			return true;
//		else if(PlayGamesPlatform.Instance.GetAchievement(achievementId) == null)
//			return true;
//		else
//			return PlayGamesPlatform.Instance.GetAchievement(achievementId).IsUnlocked;
		return false;
	}

	/// <summary>
	/// IsAchievementUnlocked - iOS implementation
	/// </summary>
	/// <returns><c>true</c> if this achievement is unlocked; otherwise, <c>false</c>.</returns>
	/// <param name="achievementId">Achievement identifier.</param>
	protected virtual bool IsAchievementUnlockediOS(string achievementId)
	{
//		if(!GameCenterManager.IsPlayerAuthenticated)
//			return true;
//		else if(GameCenterManager.GetAchievement(achievementId) == null)
//			return true;
//		else
//			return GameCenterManager.GetAchievementProgress(achievementId) >= 100.0f;
		return false;
	}

	/// <summary>
	/// IsAchievementUnlocked - Unsupported Platforms implementation
	/// </summary>
	/// <returns><c>true</c> if this achievement is unlocked; otherwise, <c>false</c>.</returns>
	/// <param name="achievementId">Achievement identifier.</param>
	protected virtual bool IsAchievementUnlockedUnsupportedPlatform(string achievementId)
	{
		Debug.LogWarning ("Running isAchievementUnlocked on an unsupported Platform");
		return false;
	}


	/// <summary>
	/// Posts the high score to the specified leaderboard.
	/// </summary>
	/// <param name="potentialHighScore">Potential high score.</param>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	public void PostHighScore(int potentialHighScore, string leaderboardId)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			PostHighScoreAndroid (potentialHighScore, leaderboardId);
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			PostHighScoreiOS (potentialHighScore, leaderboardId);
		}
		else
		{
			PostHighScoreUnsupportedPlatform (potentialHighScore, leaderboardId);
		}
	}

	/// <summary>
	/// PostHighScore - Android implemention
	/// </summary>
	/// <param name="potentialHighScore">Potential high score.</param>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	protected virtual void PostHighScoreAndroid(int potentialHighScore, string leaderboardId)
	{
		Social.ReportScore (potentialHighScore, leaderboardId, (bool success) => 
		{
			if(!success)
			{
				Debug.Log ("Posting High score was unsuccesfull");
			}
		});
	}

	/// <summary>
	/// PostHighScore - iOS implemention
	/// </summary>
	/// <param name="potentialHighScore">Potential high score.</param>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	protected virtual void PostHighScoreiOS(int potentialHighScore, string leaderboardId)
	{
//		GameCenterManager.ReportScore((long)amount, leaderboardId);
	}

	/// <summary>
	/// PostHighScore - Unsupported Platforms implemention
	/// </summary>
	/// <param name="potentialHighScore">Potential high score.</param>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	protected virtual void PostHighScoreUnsupportedPlatform(int potentialHighScore, string leaderboardId)
	{
		
	}

	/// <summary>
	/// Signs out the user
	/// </summary>
	public void SignOut()
	{
		if (Application.platform == RuntimePlatform.Android)
			SignOutAndroid ();
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			SignOutiOS ();
		else
			SignOutUnsupportedPlatform ();
	}

	/// <summary>
	/// SignOut - Android Implementation
	/// </summary>
	protected virtual void SignOutAndroid()
	{
//		PlayGamesPlatform.Instance.SignOut ();
	}

	/// <summary>
	/// SignOut - iOS Implementation
	/// </summary>
	protected virtual void SignOutiOS()
	{
		
	}

	/// <summary>
	/// SignOut - Unsupported Platforms Implementation
	/// </summary>
	protected virtual void SignOutUnsupportedPlatform()
	{

	}
}