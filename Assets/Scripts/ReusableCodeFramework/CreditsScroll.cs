using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RCF
{
	/// <summary>
	/// Scrolls a rectTransform, useful as a Credits panel
	/// </summary>
	public class CreditsScroll : MonoBehaviour 
	{
		/// <summary>
		/// The scrolling credits box,
		/// the Text box that scrolls up until reaching the top.
		/// </summary>
		public RectTransform scrollingCreditsBox;
		/// <summary>
		/// The ceiling, backToMenu() will be called when scrollingCreditsBox reaches the
		/// height of the creditsCeilingPosition
		/// </summary>
		public RectTransform creditsCeilingPosition;
		public float scrollSpeed = 125.0f;

		/// <summary>
		/// Go back to the menu, Resets the y value of the offsetMax back its original position
		/// include the base.BackToMenu() when overriding this method
		/// </summary>
		public virtual void BackToMenu()
		{
			scrollingCreditsBox.offsetMax = new Vector2(scrollingCreditsBox.offsetMax.x, 0);
		}
		
		void Update () 
		{
			scrollingCreditsBox.offsetMax = new Vector2 (scrollingCreditsBox.offsetMax.x, scrollingCreditsBox.offsetMax.y + (scrollSpeed * Time.deltaTime));
			float creditsTextHeight = Mathf.Abs(creditsCeilingPosition.rect.height);
			float creditsRectTop = Mathf.Abs(scrollingCreditsBox.offsetMax.y);
			if(creditsTextHeight > 0 && creditsRectTop >= creditsTextHeight)
			{
				BackToMenu();
			}
		}
	}
}