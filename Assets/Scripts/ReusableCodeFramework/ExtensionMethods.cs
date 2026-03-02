using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{
	/*--------------------------------------------------------------------------------------*/
	/* Float Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Returns true if float is greater than or equal to low
	/// and less than or equal to high
	/// </summary>
	/// <returns><c>true</c>, if float between or equal to low and/or high, <c>false</c> otherwise.</returns>
	/// <param name="numberToCheck">Number to check.</param>
	/// <param name="lowNumber">Low number.</param>
	/// <param name="highNumber">High number.</param>
	public static bool isFloatBetweenLowAndHighInclusive(this float numberToCheck, float lowNumber, float highNumber)
	{
		if (numberToCheck >= lowNumber && numberToCheck <= highNumber)
			return true;
		else
			return false;
	}

	/// <summary>
	/// Returns true if float is greater than low
	/// and less than high
	/// </summary>
	/// <returns><c>true</c>, if float is between low and high, <c>false</c> otherwise.</returns>
	/// <param name="numberToCheck">Number to check.</param>
	/// <param name="lowNumber">Low number.</param>
	/// <param name="highNumber">High number.</param>
	public static bool isFloatBetweenLowAndHighExclusive(this float numberToCheck, float lowNumber, float highNumber)
	{
		if (numberToCheck > lowNumber && numberToCheck < highNumber)
			return true;
		else
			return false;
	}

	/*--------------------------------------------------------------------------------------*/
	/* Integer Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Returns true if integer is greater than or equal to low
	/// and less than or equal to high
	/// </summary>
	/// <returns><c>true</c>, if integer between or equal to low and/or high, <c>false</c> otherwise.</returns>
	/// <param name="numberToCheck">Number to check.</param>
	/// <param name="lowNumber">Low number.</param>
	/// <param name="highNumber">High number.</param>
	public static bool isIntegerBetweenLowAndHighInclusive(this int numberToCheck, int lowNumber, int highNumber)
	{
		if (numberToCheck >= lowNumber && numberToCheck <= highNumber)
			return true;
		else
			return false;
	}

	/// <summary>
	/// Returns true if integer is greater than low
	/// and less than high
	/// </summary>
	/// <returns><c>true</c>, if integer between or equal to low and/or high, <c>false</c> otherwise.</returns>
	/// <param name="numberToCheck">Number to check.</param>
	/// <param name="lowNumber">Low number.</param>
	/// <param name="highNumber">High number.</param>
	public static bool isIntegerBetweenLowAndHighExclusive(this int numberToCheck, int lowNumber, int highNumber)
	{
		if (numberToCheck > lowNumber && numberToCheck < highNumber)
			return true;
		else
			return false;
	}

	/*--------------------------------------------------------------------------------------*/
	/* Text component Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the text.
	/// </summary>
	/// <param name="textBoxToSetTextOn">Text box to set text on.</param>
	/// <param name="newText">New text.</param>
	public static void SetText(this Text textBoxToSetTextOn, string newText)
	{
		textBoxToSetTextOn.text = newText;
	}

	/// <summary>
	/// Appends the string to the Text component.
	/// </summary>
	/// <param name="textBoxToAppendTextTo">Text box to append text to.</param>
	/// <param name="stringToAppendToTextBox">String to append to text box.</param>
	public static void AppendText(this Text textBoxToAppendTextTo, string stringToAppendToTextBox)
	{
		textBoxToAppendTextTo.text += stringToAppendToTextBox;
	}

	/*--------------------------------------------------------------------------------------*/
	/* Image component Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the image sprite.
	/// </summary>
	/// <param name="imageToSetSpriteOn">Image to set sprite on.</param>
	/// <param name="newSprite">New sprite.</param>
	public static void SetImageSprite(this Image imageToSetSpriteOn, Sprite newSprite)
	{
		imageToSetSpriteOn.sprite = newSprite;
	}

	/*--------------------------------------------------------------------------------------*/
	/* Color Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the alpha.
	/// </summary>
	/// <param name="colorToSetAlphaOn">Color to set alpha on.</param>
	/// <param name="alpha">Alpha.</param>
	public static void SetAlpha(this Color colorToSetAlphaOn, float alpha)
	{
		colorToSetAlphaOn.a = alpha;
	}

	/*--------------------------------------------------------------------------------------*/
	/* Transform scale Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the scale.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newScale">New scale.</param>
	public static void SetScale(this Transform transformToChangeScaleOn, Vector3 newScale)
	{
		transformToChangeScaleOn.localScale = newScale;
	}

	/// <summary>
	/// Sets the scale X value.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newXValue">New X value.</param>
	public static void SetScaleX (this Transform transformToChangeScaleOn, float newXValue)
	{
		transformToChangeScaleOn.localScale.SetX (newXValue);/* = t.localScale.SetX (newXValue);*/
	}

	/// <summary>
	/// Sets the scale Y value.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetScaleY (this Transform transformToChangeScaleOn, float newYValue)
	{
		transformToChangeScaleOn.localScale.SetY (newYValue);
	}

	/// <summary>
	/// Sets the scale Z value.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetScaleZ (this Transform transformToChangeScaleOn, float newZValue)
	{
		transformToChangeScaleOn.localScale.SetZ (newZValue);
	}

	/// <summary>
	/// Sets the scale X and Y values.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetScaleXY(this Transform transformToChangeScaleOn, float newXValue, float newYValue)
	{
		transformToChangeScaleOn.localScale.SetXY (newXValue, newYValue);
	}

	/// <summary>
	/// Sets the scale Y and Z values.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newYValue">New Y value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetScaleYZ(this Transform transformToChangeScaleOn, float newYValue, float newZValue)
	{
		transformToChangeScaleOn.localScale.SetYZ (newYValue, newZValue);
	}

	/// <summary>
	/// Sets the scale X and Z values.
	/// </summary>
	/// <param name="transformToChangeScaleOn">Transform to change scale on.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetScaleXZ(this Transform transformToChangeScaleOn, float newXValue, float newZValue)
	{
		transformToChangeScaleOn.localScale.SetXZ (newXValue, newZValue);
	}

	/*--------------------------------------------------------------------------------------*/
	/* Transform position Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the position.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newPosition">New position.</param>
	public static void SetPosition(this Transform transformToChangePositionOn, Vector3 newPosition)
	{
		transformToChangePositionOn.position = newPosition;
	}

	/// <summary>
	/// Sets the position X value.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newXValue">New X value.</param>
	public static void SetPositionX(this Transform transformToChangePositionOn, float newXValue)
	{
		transformToChangePositionOn.position.SetX (newXValue);/* = t.position.SetX (newXValue);*/
	}

	/// <summary>
	/// Sets the position Y value.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetPositionY(this Transform transformToChangePositionOn, float newYValue)
	{
		transformToChangePositionOn.position.SetY (newYValue);
	}

	/// <summary>
	/// Sets the position Z value.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetPositionZ(this Transform transformToChangePositionOn, float newZValue)
	{
		transformToChangePositionOn.position.SetZ (newZValue);
	}

	/// <summary>
	/// Sets the position X and Y values.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetPositionXY(this Transform transformToChangePositionOn, float newXValue, float newYValue)
	{
		transformToChangePositionOn.position.SetXY (newXValue, newYValue);
	}

	/// <summary>
	/// Sets the position Y and Z values.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newYValue">New Y value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetPositionYZ(this Transform transformToChangePositionOn, float newYValue, float newZValue)
	{
		transformToChangePositionOn.position.SetYZ (newYValue, newZValue);
	}

	/// <summary>
	/// Sets the position X and Z values.
	/// </summary>
	/// <param name="transformToChangePositionOn">Transform to change position on.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetPositionXZ(this Transform transformToChangePositionOn, float newXValue, float newZValue)
	{
		transformToChangePositionOn.position.SetXZ (newXValue, newZValue);
	}

	/*--------------------------------------------------------------------------------------*/
	/* Transform local position Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the local position.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newLocalPosition">New local position.</param>
	public static void SetLocalPosition(this Transform transformToChangeLocalPositionOn, Vector3 newLocalPosition)
	{
		transformToChangeLocalPositionOn.localPosition = newLocalPosition;
	}

	/// <summary>
	/// Sets the local position X value.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newXValue">New X value.</param>
	public static void SetLocalPositionX(this Transform transformToChangeLocalPositionOn, float newXValue)
	{
		transformToChangeLocalPositionOn.localPosition.SetX (newXValue);/* = t.localPosition.SetX (newXValue);*/
	}

	/// <summary>
	/// Sets the local position Y value.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetLocalPositionY(this Transform transformToChangeLocalPositionOn, float newYValue)
	{
		transformToChangeLocalPositionOn.localPosition.SetY (newYValue);
	}

	/// <summary>
	/// Sets the local position Z value.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetLocalPositionZ(this Transform transformToChangeLocalPositionOn, float newZValue)
	{
		transformToChangeLocalPositionOn.localPosition.SetZ (newZValue);
	}

	/// <summary>
	/// Sets the local position X and Y values.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetLocalPositionXY(this Transform transformToChangeLocalPositionOn, float newXValue, float newYValue)
	{
		transformToChangeLocalPositionOn.localPosition.SetXY (newXValue, newYValue);
	}

	/// <summary>
	/// Sets the local position Y and Z values.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newYValue">New Y value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetLocalPositionYZ(this Transform transformToChangeLocalPositionOn, float newYValue, float newZValue)
	{
		transformToChangeLocalPositionOn.localPosition.SetYZ (newYValue, newZValue);
	}

	/// <summary>
	/// Sets the local position X and Z values.
	/// </summary>
	/// <param name="transformToChangeLocalPositionOn">Transform to change local position on.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetLocalPositionXZ(this Transform transformToChangeLocalPositionOn, float newXValue, float newZValue)
	{
		transformToChangeLocalPositionOn.localPosition.SetXZ (newXValue, newZValue);
	}

	/*--------------------------------------------------------------------------------------*/
	/* Vector 3 Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the X value.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newXValue">New X value.</param>
	public static void SetX(this Vector3 vectorToChange, float newXValue)
	{
		vectorToChange.x = newXValue;
	}

	/// <summary>
	/// Sets the Y value.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetY(this Vector3 vectorToChange, float newYValue)
	{
		vectorToChange.y = newYValue;
	}

	/// <summary>
	/// Sets the Z value.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetZ(this Vector3 vectorToChange, float newZValue)
	{
		vectorToChange.z = newZValue;
	}

	/// <summary>
	/// Sets the X and Y values.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetXY(this Vector3 vectorToChange, float newXValue, float newYValue)
	{
		vectorToChange.x = newXValue;
		vectorToChange.y = newYValue;
	}

	/// <summary>
	/// Sets the X and Z values.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newXValue">New X value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetXZ(this Vector3 vectorToChange, float newXValue, float newZValue)
	{
		vectorToChange.x = newXValue;
		vectorToChange.z = newZValue;
	}

	/// <summary>
	/// Sets the Y and Z values.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newYValue">New Y value.</param>
	/// <param name="newZValue">New Z value.</param>
	public static void SetYZ(this Vector3 vectorToChange, float newYValue, float newZValue)
	{
		vectorToChange.y = newYValue;
		vectorToChange.z = newZValue;
	}

	/*--------------------------------------------------------------------------------------*/
	/* Vector 2 Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Sets the X value.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newXValue">New X value.</param>
	public static void SetX(this Vector2 vectorToChange, float newXValue)
	{
		vectorToChange.x = newXValue;
	}

	/// <summary>
	/// Sets the Y value.
	/// </summary>
	/// <param name="vectorToChange">Vector to change.</param>
	/// <param name="newYValue">New Y value.</param>
	public static void SetY(this Vector2 vectorToChange, float newYValue)
	{
		vectorToChange.y = newYValue;
	}

	/*--------------------------------------------------------------------------------------*/
	/* List Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Shuffles the list.
	/// </summary>
	/// <param name="listToShuffle">List to shuffle.</param>
	public static void ShuffleList<T>(this IList<T> listToShuffle)
	{
		System.Random rng = new System.Random ();
		int n = listToShuffle.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next (n + 1);
			T value = listToShuffle [k];
			listToShuffle [k] = listToShuffle [n];
			listToShuffle [n] = value;
		}
	}

	/// <summary>
	/// Gets a random item from the list.
	/// </summary>
	/// <returns>The random item.</returns>
	/// <param name="listToGetRandomItemFrom">List to get random item from.</param>
	public static T GetRandomItem<T>(this IList<T> listToGetRandomItemFrom)
	{
		if (listToGetRandomItemFrom.Count == 0)
			throw new System.IndexOutOfRangeException ("List contains no items");
		return listToGetRandomItemFrom [UnityEngine.Random.Range (0, listToGetRandomItemFrom.Count)];
	}

	/// <summary>
	/// Removes A random item.
	/// </summary>
	/// <returns>The random item that was removed.</returns>
	/// <param name="listToRemoveRandomItemFrom">List to remove random item from.</param>
	public static T RemoveRandomItem<T>(this IList<T> listToRemoveRandomItemFrom)
	{
		if (listToRemoveRandomItemFrom.Count == 0)
			throw new System.IndexOutOfRangeException ("List contains no items");
		int index = UnityEngine.Random.Range (0, listToRemoveRandomItemFrom.Count);
		T item = listToRemoveRandomItemFrom [index];
		listToRemoveRandomItemFrom.RemoveAt (index);
		return item;
	}

	/*--------------------------------------------------------------------------------------*/
	/* AudioSource Extension methods */
	/*--------------------------------------------------------------------------------------*/

	/// <summary>
	/// Plays the clip.
	/// </summary>
	/// <param name="audioSourceToPlayClipOn">Audio source to play clip on.</param>
	/// <param name="clipToPlay">Clip to play.</param>
	public static void PlayClip(this AudioSource audioSourceToPlayClipOn, AudioClip clipToPlay)
	{
		if(audioSourceToPlayClipOn.clip != clipToPlay)
			audioSourceToPlayClipOn.clip = clipToPlay;
		audioSourceToPlayClipOn.Play ();
	}
}