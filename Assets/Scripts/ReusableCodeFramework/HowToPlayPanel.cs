using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used for showing a how to play screen to the player
/// </summary>
public class HowToPlayPanel : MonoBehaviour
{
	/// <summary>
	/// The how to play page title text.
	/// </summary>
	public Text howToPlayPageTitleText;
	/// <summary>
	/// The how to play page description text.
	/// </summary>
	public Text howToPlayPageDescriptionText;
	/// <summary>
	/// The how to play page image.
	/// </summary>
	public Image howToPlayPageImage;
	/// <summary>
	/// List of possible how to play images.
	/// </summary>
	public List<Sprite> howToPlayImages;
	/// <summary>
	/// The index of the current how to play page.
	/// </summary>
	protected int currentHowToPlayPageIndex;

	void Start ()
	{
		currentHowToPlayPageIndex = 0;
		OnHowToPlayPageChanged ();
	}

	/// <summary>
	/// Moves to the next how to play page.
	/// </summary>
	public virtual void NextHowToPlayPage()
	{
		currentHowToPlayPageIndex++;
		if (currentHowToPlayPageIndex >= howToPlayImages.Count)
		{
			currentHowToPlayPageIndex = 0;
		}
		OnHowToPlayPageChanged ();
	}

	/// <summary>
	/// Moves to the previous how to play page.
	/// </summary>
	public virtual void PrevHowToPlayPage()
	{
		currentHowToPlayPageIndex--;
		if (currentHowToPlayPageIndex < 0)
		{
			currentHowToPlayPageIndex = howToPlayImages.Count - 1;
		}
		OnHowToPlayPageChanged ();
	}

	/// <summary>
	/// Gets called when the current How to play page changes.
	/// </summary>
	protected virtual void OnHowToPlayPageChanged()
	{
		SetHowToPlayPageText ();
		SetHowToPlayPageImage ();
	}

	/// <summary>
	/// Sets the how to play page text.
	/// </summary>
	protected virtual void SetHowToPlayPageText()
	{
		
	}

	/// <summary>
	/// Sets the how to play page image.
	/// </summary>
	protected virtual void SetHowToPlayPageImage()
	{
		if (howToPlayPageImage != null)
			howToPlayPageImage.SetImageSprite (howToPlayImages [currentHowToPlayPageIndex]);
	}
}