using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used for showing a high score screen to the player
/// </summary>
public class HighScorePanel : MonoBehaviour 
{
	/// <summary>
	/// The number of high score tables in the game.
	/// </summary>
	public int numberOfHighScoreTables;
	/// <summary>
	/// The number of scores to show per high score table.
	/// </summary>
	public int numberOfScoresPerTable;
	/// <summary>
	/// The high score table title that displays the current selected high score table.
	/// </summary>
	public Text highScoreTableTitleLabel;
	/// <summary>
	/// The potential high score table titles.
	/// </summary>
	public List<string> highScoreTableTitles;
	/// <summary>
	/// The index of the current high score table.
	/// </summary>
	protected int currentHighScoreTableIndex;

	void OnEnable () 
	{
		currentHighScoreTableIndex = 0;
		OnHighScoreTableChanged ();
	}

	/// <summary>
	/// Move to the next high score table.
	/// </summary>
	public virtual void NextHighScoreTable()
	{
		currentHighScoreTableIndex++;
		if (currentHighScoreTableIndex >= numberOfHighScoreTables)
		{
			currentHighScoreTableIndex = 0;
		}
		OnHighScoreTableChanged ();
	}

	/// <summary>
	/// Move to the previous high score table.
	/// </summary>
	public virtual void PrevHighScoreTable()
	{
		currentHighScoreTableIndex--;
		if (currentHighScoreTableIndex < 0)
		{
			currentHighScoreTableIndex = numberOfHighScoreTables - 1;
		}
		OnHighScoreTableChanged ();
	}

	/// <summary>
	/// Gets called when the current high score table changes.
	/// </summary>
	private void OnHighScoreTableChanged()
	{
		OnHighScoreTableModeChanged ();
		for (int i = 0; i < numberOfScoresPerTable; i++)
		{
			SetHighScoreUIElementsAt (i);
		}
	}

	/// <summary>
	/// Changes the text label to show the current selected high score table.
	/// </summary>
	protected virtual void OnHighScoreTableModeChanged()
	{
		if (highScoreTableTitleLabel != null)
			highScoreTableTitleLabel.SetText (highScoreTableTitles [currentHighScoreTableIndex]);
	}

	/// <summary>
	/// Sets the relevant high score ui elements for the specific highScorePosition, i.e 5th, 4th, 3rd, 2nd , 1st, etc.
	/// Override this with your own implementation of what ui elements to set.
	/// </summary>
	/// <param name="highScorePosition">High score position.</param>
	protected virtual void SetHighScoreUIElementsAt(int highScorePosition)
	{

	}
}