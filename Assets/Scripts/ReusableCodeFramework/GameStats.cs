using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used for storing some generic stats for the game
/// </summary>
public class GameStats 
{
	/// <summary>
	/// A static reference to the GameStats class
	/// </summary>
	protected static GameStats instance;

	public static GameStats Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GameStats();
			}
			return instance;
		}
	}

	protected GameStats()
	{
		InitializeGameStats ();
	}
		
	/// <summary>
	/// Set the starting values for the game stats
	/// </summary>
	protected virtual void InitializeGameStats()
	{

	}

	/// <summary>
	/// Prints the game stats.
	/// </summary>
	/// <returns>The game stats.</returns>
	public virtual string PrintGameStats()
	{
		string gameStats = "";
		return gameStats;
	}

	/// <summary>
	/// Resets the game stats.
	/// </summary>
	public virtual void ResetStats()
	{
		
	}
}