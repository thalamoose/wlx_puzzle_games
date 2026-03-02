using UnityEngine;
using System.Collections;

namespace RCF
{
	/// <summary>
	/// Fade object in or out.
	/// </summary>
	public class FadeObjectInOut : MonoBehaviour
	{
		/// <summary>
		/// Starting delay until fade.
		/// </summary>
		public float fadeDelay = 0.0f;
		/// <summary>
		/// The fade duration.
		/// </summary>
		public float fadeDuration = 0.5f;
		/// <summary>
		/// Should fade in on start.
		/// </summary>
		public bool fadeInOnStart = false;
		/// <summary>
		/// Should fade out on start
		/// </summary>
		public bool fadeOutOnStart = false;
		private bool logInitialFadeInSequence = false;
		
		/// <summary>
		/// The original colors.
		/// </summary>
		private Color[] originalColors;
		
		/// <summary>
		/// Allow automatic fading on the start of the scene by using start as a coroutine.
		/// </summary>
		IEnumerator Start ()
		{
			yield return new WaitForSeconds (fadeDelay); 
			
			if (fadeInOnStart)
			{
				logInitialFadeInSequence = true; 
				FadeIn (fadeDuration); 
			}
			
			if (fadeOutOnStart)
			{
				FadeOut (fadeDuration); 
			}
		}

		/// <summary>
		/// Fade in gameObject.
		/// </summary>
		/// <param name="fadingDuration">Fading duration.</param>
		public void FadeIn (float fadingDuration)
		{
			StopAllCoroutines(); 
			StartCoroutine(FadeSequence(fadingDuration)); 
		}

		/// <summary>
		/// Fade out gameObject.
		/// </summary>
		/// <param name="fadingDuration">Fading duration.</param>
		public void FadeOut (float fadingDuration)
		{
			StopAllCoroutines();
			StartCoroutine(FadeSequence(-fadingDuration));
		}
		
		/// <summary>
		/// Runs fade sequence, causes the gameObject to fade in or out.
		/// </summary>
		/// <param name="duration">duration of fade effect.</param>
		IEnumerator FadeSequence (float duration)
		{
			//Determine whether fading in or out
			bool fadingOut = (duration < 0.0f);
			//Calculate fade speed based on duration
			float fadingSpeed = 1.0f / duration;
			
			//Retrieve all renderable children
			SpriteRenderer[] renderableObjects = GetComponentsInChildren<SpriteRenderer>();
			//Cache the original colors for the renderableObjects
			if (originalColors == null)
			{
				originalColors = new Color[renderableObjects.Length];

				for (int i = 0; i < renderableObjects.Length; i++)
				{
					originalColors[i] = renderableObjects[i].color;
				}
			}
			
			//Make all sprite renderers visible
			for (int i = 0; i < renderableObjects.Length; i++)
			{
				renderableObjects[i].enabled = true;
			}

			//Calculate the highest alpha value needed
			float alphaValue = MaxAlpha(renderableObjects);
			
			// This is a special case for objects that are set to fade in on start. 
			// it will force them to have an alpha value of 0.
			if (logInitialFadeInSequence && !fadingOut)
			{
				alphaValue = 0.0f;
				logInitialFadeInSequence = false;
			}
			
			//Start the fade effect until fully faded in or fully faded out
			while ((alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut))
			{
				alphaValue += Time.deltaTime * fadingSpeed;
				
				for (int i = 0; i < renderableObjects.Length; i++)
				{
					Color newColor = (originalColors != null ? originalColors[i] : renderableObjects[i].color);
	                newColor.a = Mathf.Max ( newColor.a, alphaValue );
					newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f);
	                renderableObjects[i].color = newColor;
				}
				
				yield return null;
			}
			
			//Disable the renderers after fading out
			if (fadingOut)
			{
				for (int i = 0; i < renderableObjects.Length; i++)
				{
					renderableObjects[i].enabled = false;
				}
			}	
		}

		/// <summary>
		/// Determine the highest alpha value
		/// </summary>
		/// <returns>The highest alpha value.</returns>
		/// <param name="allRenderableObjects">Array of all renderable objects to look at.</param>
		private float MaxAlpha(SpriteRenderer[] allRenderableObjects)
		{
			float maxAlpha = 0.0f;
			for (int i = 0; i < allRenderableObjects.Length; i++)
			{
				maxAlpha = Mathf.Max (maxAlpha, allRenderableObjects [i].color.a);
			}
			return maxAlpha;
		}
	}
}