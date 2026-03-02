using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RCF
{
	public enum AspectRatio
	{
        TwoByOne,
		FourByThree,
		ThreeByTwo,
		SixteenByNine,
		SixteenByTen
	};

	/// <summary>
	/// Sets the appropriate camera for the provided canvas
	/// depending on the aspect ratio of the screen and screen orientation.
	/// </summary>
	public class CanvasScript : MonoBehaviour 
	{
		/// <summary>
		/// The phone camera.
		/// </summary>
		public Camera phoneCamera;
		/// <summary>
		/// The tablet camera.
		/// </summary>
		public Camera tabletCamera;
		/// <summary>
		/// The canvas for the scene.
		/// </summary>
		public Canvas sceneCanvas;
		public static AspectRatio Ratio { get; set; }

		void Start () 
		{
			bool isInLandscapeOrientation = isInLandScape ();
			float windowAspect = GetAspectRatioValue (isInLandscapeOrientation);
			if (isInLandscapeOrientation)
			{
				if (windowAspect >= 1.7f)//16/9
				{
					Ratio = AspectRatio.SixteenByNine;
					Landscape16By9 ();
				}
				else if (windowAspect >= 1.6f)//16/10
				{
					Ratio = AspectRatio.SixteenByTen;
					Landscape16By10 ();
				}
				else if (windowAspect >= 1.5f)//3/2
				{
					Ratio = AspectRatio.ThreeByTwo;
					Landscape3By2 ();
				}
				else//4/3
				{
					Ratio = AspectRatio.FourByThree;
					Landscape4By3 ();
				}
			}
			else
			{
                if(windowAspect <=0.5f)
                {
                    Ratio = AspectRatio.TwoByOne;
                    Portrait1By2();

                }
				else if (windowAspect <= 0.563381f)//9/16
				{
					Ratio = AspectRatio.SixteenByNine;
					Portrait9By16 ();
				}
				else if (windowAspect <= 0.6252f)//10/16
				{
					Ratio = AspectRatio.SixteenByTen;
					Portrait10By16 ();
				}
				else if (windowAspect <= 0.7f)//2/3
				{
					Ratio = AspectRatio.ThreeByTwo;
					Portrait2By3 ();
				}
				else//3/4
				{
					Ratio = AspectRatio.FourByThree;
					Portrait3By4 ();
				}
			}
		}

		/// <summary>
		/// returns true if the Screen is in landscape.
		/// </summary>
		/// <returns><c>true</c>, if in landscape orientation, <c>false</c> otherwise.</returns>
		private bool isInLandScape()
		{
			if (Screen.width > Screen.height)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the aspect ratio value depending on the orientation of the screen.
		/// </summary>
		/// <returns>The aspect ratio value.</returns>
		/// <param name="isLandscape">If set to <c>true</c> the screen is in landscape.</param>
		private float GetAspectRatioValue(bool isLandscape)
		{
			//if (isLandscape)
			//{
				return (float)Screen.width / (float)Screen.height;
			//}
			//else
			//{
			//	return (float)Screen.height / (float)Screen.width;
			//}
		}

		/// <summary>
		/// Code that executes when in landscape 16:9
		/// </summary>
		public virtual void Landscape16By9()
		{
			Aspect16By9_9By16 ();
		}

		/// <summary>
		/// Code that executes when in landscape 16:10
		/// </summary>
		public virtual void Landscape16By10()
		{
			Aspect16By10_10By16 ();
		}

		/// <summary>
		/// Code that executes when in landscape 3:2
		/// </summary>
		public virtual void Landscape3By2()
		{
			Aspect3By2_2By3 ();
		}

		/// <summary>
		/// Code that executes when in landscape 4:3
		/// </summary>
		public virtual void Landscape4By3()
		{
			Aspect4By3_3By4 ();
		}

		/// <summary>
		/// Code that executes when in portrait 9:16
		/// </summary>
		public virtual void Portrait1By2()
        {
            Aspect2By1_1By2();
        }

        /// <summary>
        /// Code that executes when in portrait 9:16
        /// </summary>
        public virtual void Portrait9By16()
		{
			Aspect16By9_9By16 ();
		}

		/// <summary>
		/// Code that executes when in portrait 10:16
		/// </summary>
		public virtual void Portrait10By16()
		{
			Aspect16By10_10By16 ();
		}

		/// <summary>
		/// Code that executes when in portrait 2:3
		/// </summary>
		public virtual void Portrait2By3()
		{
			Aspect3By2_2By3 ();
		}

		/// <summary>
		/// Code that executes when in portrait 3:4
		/// </summary>
		public virtual void Portrait3By4()
		{
			Aspect4By3_3By4 ();
		}

		/// <summary>
		/// Executes when in 16:9 or 9:16.
		/// </summary>
		protected virtual void Aspect2By1_1By2()
        {
            tabletCamera.enabled = false;
            tabletCamera.gameObject.SetActive(false);
            phoneCamera.enabled = true;
            phoneCamera.orthographicSize = 17.56f;// 17.56f;
            //phoneCamera.fieldOfView = 60;
            sceneCanvas.worldCamera = phoneCamera;
        }

        /// <summary>
        /// Executes when in 16:9 or 9:16.
        /// </summary>
        protected virtual void Aspect16By9_9By16()
		{
			tabletCamera.enabled = false;
			tabletCamera.gameObject.SetActive (false);
			phoneCamera.enabled = true;
			phoneCamera.fieldOfView = 60;
			sceneCanvas.worldCamera = phoneCamera;
		}

		/// <summary>
		/// Executes when in 16:10 or 10:16.
		/// </summary>
		protected virtual void Aspect16By10_10By16()
		{
			tabletCamera.enabled = false;
			tabletCamera.gameObject.SetActive (false);
			phoneCamera.enabled = true;
			phoneCamera.fieldOfView = 54;
			sceneCanvas.worldCamera = phoneCamera;
		}

		/// <summary>
		/// Executes when in 3:2 or 2:3.
		/// </summary>
		protected virtual void Aspect3By2_2By3()
		{
			tabletCamera.enabled = false;
			tabletCamera.gameObject.SetActive (false);
			phoneCamera.enabled = true;
			phoneCamera.fieldOfView = 52;
			sceneCanvas.worldCamera = phoneCamera;
		}

		/// <summary>
		/// Executes when in 4:3 or 3:4.
		/// </summary>
		protected virtual void Aspect4By3_3By4()
		{
			tabletCamera.enabled = true;
			phoneCamera.enabled = false;
			phoneCamera.gameObject.SetActive (false);
			sceneCanvas.worldCamera = tabletCamera;
		}
	}
}