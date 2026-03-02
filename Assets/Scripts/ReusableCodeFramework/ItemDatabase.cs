using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic Item database.
/// </summary>
public class ItemDatabase 
{
	private static ItemDatabase instance;

	protected ItemDatabase()
	{
		LoadDatabase ();
	}

	public static ItemDatabase Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new ItemDatabase();
			}
			return instance;
		}
	}

	/// <summary>
	/// Creates the item database.
	/// </summary>
	protected virtual void LoadDatabase()
	{
		
	}

	/// <summary>
	/// Loads the sprite resource.
	/// </summary>
	/// <returns>The resource.</returns>
	/// <param name="fileName">File name of the resource.</param>
	protected T LoadResource<T>(string fileName) where T : UnityEngine.Object
	{
		return Resources.Load<T> (fileName);
	}
}