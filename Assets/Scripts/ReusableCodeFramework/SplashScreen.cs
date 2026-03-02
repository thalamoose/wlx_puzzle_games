using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace RCF
{
	/// <summary>
	/// Splash screen Implementation.
	/// </summary>
	public class SplashScreen : MonoBehaviour 
	{
		//public Animator stache, afro, hat;

		/// <summary>
		/// List of panels to show for the splash screen
		/// </summary>
		[Tooltip("List of panels to show for the splash screen")]
		public List<GameObject> splashScreenPanels;
		/// <summary>
		/// A black panel that is used to add a fade in and out effect for the splashScreenPanels.
		/// </summary>
		[Tooltip("A black panel that is used to add a fade in and out effect for the splashScreenPanels.")]
		public Image fadePanel;
		/// <summary>
		/// if true then the fadePanel will fade in and out between the splashScreenPanels.
		/// </summary>
		[Tooltip("if true then the fadePanel will fade in and out between the splashScreenPanels.")]
		public bool shouldFadeInAndOutBetweenPanels;
		/// <summary>
		/// The minimum amount of time the splash screen will be shown for.
		/// </summary>
		protected const float minSplashScreenTime = 6.0f;
		/// <summary>
		/// The maximum amount of time the splash screen could be shown for.
		/// </summary>
		protected const float maxSplashScreenTime = 20.0f;
		/// <summary>
		/// Internal timer that counts up.
		/// </summary>
		private float timer = 0.0f;

		public virtual void Start () 
		{
			Invoke ("StartSplashScreenAnimation", 2.0f);
			InitializePlayerPrefs ();
			InitializeGame();
			if(shouldFadeInAndOutBetweenPanels)
				StartCoroutine (CyclePanels());
		}

		/// <summary>
		/// Starts the splash screen animations, if necessary.
		/// </summary>
		protected virtual void StartSplashScreenAnimation()
		{
			//stache.Play ("Stache Recoil");
			//afro.Play ("Afro Recoil");
			//hat.Play ("Hat Drop");
			//SoundManager.Instance.PlaySoundEffect(SoundManager.OOOHH, true, 0.1f);
			//SoundManager.Instance.PlaySoundEffect(SoundManager.MUSTACHE_WIGGLE, true, 1.4f);
		}

		/// <summary>
		/// Initializes the player prefs.
		/// </summary>
		protected virtual void InitializePlayerPrefs()
		{
			//		PlayerPrefs.DeleteAll();
			//		if(!PlayerPrefs.HasKey("Difficulty"))
			//			PlayerPrefs.SetInt("Difficulty",1);
			//		if(!PlayerPrefs.HasKey("Music"))
			//			PlayerPrefs.SetInt("Music",1);
			//		if(!PlayerPrefs.HasKey("Sound"))
			//			PlayerPrefs.SetInt("Sound",1);
			//		if(!PlayerPrefs.HasKey("Subtitles"))
			//			PlayerPrefs.SetInt("Subtitles",1);
			//
			//      Initialize High Scores
		}

		/// <summary>
		/// Initializes the game.
		/// </summary>
		protected virtual void InitializeGame()
		{
			if(Application.platform == RuntimePlatform.Android)
			{
				//if(!PlayGamesPlatform.Instance.IsAuthenticated())
				//{
				//	print ("Sign IN");
				//	MultiplayerController.Instance.SignIn ();
				//}
			}
			else if(Application.platform == RuntimePlatform.IPhonePlayer)
			{
				//if(!GameCenterManager.IsInitialized && !GameCenterManager.IsPlayerAuthenticated)
				//	MultiplayerController.Instance.SignIn();
			}
		}

		/// <summary>
		/// Cycles the splash screen panels and fades in and out to black between each panel.
		/// </summary>
		IEnumerator CyclePanels()
		{
			for (int i = 0; i < splashScreenPanels.Count; i++)
			{
				//Fade out the black panel
				if (fadePanel != null)
				{
					fadePanel.gameObject.SetActive (true);
					fadePanel.CrossFadeAlpha (0, 1f, false);
					yield return new WaitForSeconds (1.0f);
				}
				//Show splash screen panel
				splashScreenPanels [i].gameObject.SetActive (true);

				if (fadePanel != null)
				{
					//Fade in the black panel
					fadePanel.CrossFadeAlpha (0, 1f, false);
					yield return new WaitForSeconds (3f);
					fadePanel.canvasRenderer.SetAlpha (0);
					fadePanel.CrossFadeAlpha (1, 1f, false);
					yield return new WaitForSeconds (1.0f);
				}
			}
		}
		
		void Update () 
		{
			timer += Time.deltaTime;
	        if(ReadyToMoveToNextScene())
			{
	            MoveToNextScene();
			}
		}

		/// <summary>
		/// Determines whether splash screen is ready to move to the next scene or not.
		/// </summary>
		/// <returns><c>true</c>, if the game is ready to move to the next scene, <c>false</c> otherwise.</returns>
		protected virtual bool ReadyToMoveToNextScene()
		{
			return (timer >= minSplashScreenTime || timer >= maxSplashScreenTime);
		}

		/// <summary>
		/// Code implementation that moves to next scene.
		/// </summary>
		protected virtual void MoveToNextScene()
		{
			
		}
	}
}