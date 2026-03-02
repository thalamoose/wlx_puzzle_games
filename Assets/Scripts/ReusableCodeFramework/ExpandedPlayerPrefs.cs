using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An Expanded version of player prefs that includes Encryption from EncryptedPlayerPrefs.cs
/// and supports unencrypted storage of more data types than Unity PlayerPrefs.
/// </summary>
public class ExpandedPlayerPrefs
{
	static private int endianDiff1;
	static private int endianDiff2;
	static private int idx;
	static private byte [] byteBlock;

	/// <summary>
	/// Enum representing the Array type.
	/// </summary>
	enum ArrayType {Float, Int32, Bool, String, Vector2, Vector3, Quaternion, Color}

	/// <summary>
	/// Deletes all PlayerPrefs.
	/// </summary>
	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	/// <summary>
	/// Deletes the key.
	/// </summary>
	/// <param name="key">Key to delete.</param>
	/// <param name="valueWasEncrypted">If set to <c>true</c> then assumes the key is encrypted and will use the appropriate delete function.</param>
	public static void DeleteKey(string key, bool valueWasEncrypted = false)
	{
		if (valueWasEncrypted)
			EncryptedPlayerPrefs.DeleteKey (key);
		else
			PlayerPrefs.DeleteKey(key);
	}

	/// <summary>
	/// Determines if the specified key exists in PlayerPrefs.
	/// </summary>
	/// <returns><c>true</c> if the specified key exists; otherwise, <c>false</c>.</returns>
	/// <param name="key">Key to check.</param>
	/// <param name="valueWasEncrypted">If set to <c>true</c> then use the Encrypted version of HasKey(key).</param>
	public static bool HasKey(string key, bool valueWasEncrypted)
	{
		if (valueWasEncrypted)
			return EncryptedPlayerPrefs.HasKey (key);
		else
			return PlayerPrefs.HasKey (key);
	}

	/// <summary>
	/// Save the PlayerPrefs.
	/// </summary>
	public static void Save()
	{
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Stores the provided string with the given key.
	/// </summary>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	/// <param name="shouldEncryptValue">If set to <c>true</c> then use the Encrypted version of SetString().</param>
	public static void SetString(string key, string value, bool shouldEncryptValue = false)
	{
		if (shouldEncryptValue)
			EncryptedPlayerPrefs.SetString (key, value);
		else
			PlayerPrefs.SetString (key, value);
	}

	/// <summary>
	/// Gets the string that was stored with the provided key.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	/// <param name="valueWasEncrypted">If set to <c>true</c> then use the Encrypted version of GetString().</param>
	public static string GetString(string key, string defaultValue = "", bool valueWasEncrypted = false)
	{
		if (valueWasEncrypted)
			return EncryptedPlayerPrefs.GetString (key, defaultValue);
		else
			return PlayerPrefs.GetString (key, defaultValue);
	}

	/// <summary>
	/// Stores the provided float with the given key.
	/// </summary>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	/// <param name="shouldEncryptValue">If set to <c>true</c> then use the Encrypted version of SetFloat().</param>
	public static void SetFloat(string key, float value, bool shouldEncryptValue = false)
	{
		if (shouldEncryptValue)
			EncryptedPlayerPrefs.SetFloat (key, value);
		else
			PlayerPrefs.SetFloat (key, value);
	}

	/// <summary>
	/// Gets the float that was stored with the provided key.
	/// </summary>
	/// <returns>The float.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	/// <param name="valueWasEncrypted">If set to <c>true</c> then use the Encrypted version of GetFloat().</param>
	public static float GetFloat(string key, float defaultValue = 0, bool valueWasEncrypted = false)
	{
		if (valueWasEncrypted)
			return EncryptedPlayerPrefs.GetFloat (key, defaultValue);
		else
			return PlayerPrefs.GetFloat (key, defaultValue);
	}

	/// <summary>
	/// Stores the provided int with the given key.
	/// </summary>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	/// <param name="shouldEncryptValue">If set to <c>true</c> then use the Encrypted version of SetInt().</param>
	public static void SetInt(string key, int value, bool shouldEncryptValue = false)
	{
		if (shouldEncryptValue)
			EncryptedPlayerPrefs.SetInt (key, value);
		else
			PlayerPrefs.SetInt (key, value);
	}

	/// <summary>
	/// Gets the int that was stored with the provided key.
	/// </summary>
	/// <returns>The int.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	/// <param name="valueWasEncrypted">If set to <c>true</c> then use the Encrypted version of GetInt().</param>
	public static int GetInt(string key, int defaultValue = 0, bool valueWasEncrypted = false)
	{
		if (valueWasEncrypted)
			return EncryptedPlayerPrefs.GetInt (key, defaultValue);
		else
			return PlayerPrefs.GetInt (key, defaultValue);
	}

	/// <summary>
	/// Stores the provided bool with the given key.
	/// </summary>
	/// <returns><c>true</c>, if retrieved a bool succesfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	/// <param name="shouldEncryptValue">If set to <c>true</c> then use the Encrypted version of SetBool().</param>
	public static bool SetBool (string key, bool value, bool shouldEncryptValue = false)
	{
		try
		{
			if(shouldEncryptValue)
				EncryptedPlayerPrefs.SetBool(key,value);
			else
				PlayerPrefs.SetInt(key, value? 1 : 0);
		}
		catch
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Gets the bool that was stored with the provided key.
	/// </summary>
	/// <returns>The bool.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	/// <param name="valueWasEncrypted">If set to <c>true</c> then use the Encrypted version of GetBool().</param>
	public static bool GetBool (string key, bool defaultValue = false, bool valueWasEncrypted = false)
	{
		if (valueWasEncrypted)
			return EncryptedPlayerPrefs.GetBool (key, defaultValue);
		else
			return (1==PlayerPrefs.GetInt(key, defaultValue?1:0));
	}

	/// <summary>
	/// Stores the provided vector2 with the given key.
	/// </summary>
	/// <returns><c>true</c>, if vector2 was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="vector">Vector to store.</param>
	public static bool SetVector2 (string key, Vector2 vector)
	{
		return SetFloatArray(key, new float[]{vector.x, vector.y});
	}

	/// <summary>
	/// Gets the vector2 that was stored with the provided key.
	/// </summary>
	/// <returns>The vector2.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static Vector2 GetVector2 (string key, Vector2 defaultValue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetVector2(key);
		}
		return defaultValue;
	}

	/// <summary>
	/// Gets the vector2 that was stored with the provided key.
	/// </summary>
	/// <returns>The vector2.</returns>
	/// <param name="key">Key that was used.</param>
	public static Vector2 GetVector2 (string key)
	{
		var floatArray = GetFloatArray(key);
		if (floatArray.Length < 2)
		{
			return Vector2.zero;
		}
		return new Vector2(floatArray[0], floatArray[1]);
	}

	/// <summary>
	/// Stores the provided vector3 with the given key.
	/// </summary>
	/// <returns><c>true</c>, if vector3 was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="vector">Vector to store.</param>
	public static bool SetVector3 (string key, Vector3 vector)
	{
		return SetFloatArray(key, new float []{vector.x, vector.y, vector.z});
	}

	/// <summary>
	/// Gets the vector3 that was stored with the provided key.
	/// </summary>
	/// <returns>The vector3.</returns>
	/// <param name="key">Key that was used.</param>
	public static Vector3 GetVector3 (string key)
	{
		var floatArray = GetFloatArray(key);
		if (floatArray.Length < 3)
		{
			return Vector3.zero;
		}
		return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}

	/// <summary>
	/// Gets the vector3 that was stored with the provided key.
	/// </summary>
	/// <returns>The vector3.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static Vector3 GetVector3 (string key, Vector3 defaultValue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetVector3(key);
		}
		return defaultValue;
	}

	/// <summary>
	/// Stores the provided quaternion with the given key.
	/// </summary>
	/// <returns><c>true</c>, if quaternion was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="quat">Quaternion to store.</param>
	public static bool SetQuaternion (string key, Quaternion quat)
	{
		return SetFloatArray(key, new float[]{quat.x, quat.y, quat.z, quat.w});
	}

	/// <summary>
	/// Gets the quaternion that was stored with the provided key.
	/// </summary>
	/// <returns>The quaternion.</returns>
	/// <param name="key">Key that was used.</param>
	public static Quaternion GetQuaternion (string key)
	{
		var floatArray = GetFloatArray(key);
		if (floatArray.Length < 4)
		{
			return Quaternion.identity;
		}
		return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
	}

	/// <summary>
	/// Gets the quaternion that was stored with the provided key.
	/// </summary>
	/// <returns>The quaternion.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static Quaternion GetQuaternion (string key, Quaternion defaultValue )
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetQuaternion(key);
		}
		return defaultValue;
	}

	/// <summary>
	/// Stores the provided color with the given key.
	/// </summary>
	/// <returns><c>true</c>, if color was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="color">Color to store.</param>
	public static bool SetColor (string key, Color color)
	{
		return SetFloatArray(key, new float[]{color.r, color.g, color.b, color.a});
	}

	/// <summary>
	/// Gets the color that was stored with the provided key.
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="key">Key that was used.</param>
	public static Color GetColor (string key)
	{
		var floatArray = GetFloatArray(key);
		if (floatArray.Length < 4)
		{
			return new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}
		return new Color(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
	}

	/// <summary>
	/// Gets the color that was stored with the provided key.
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static Color GetColor (string key , Color defaultValue )
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetColor(key);
		}
		return defaultValue;
	}

	/// <summary>
	/// Stores the provided bool array with given key.
	/// </summary>
	/// <returns><c>true</c>, if bool array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="boolArray">Bool array to store.</param>
	public static bool SetBoolArray (string key, bool[] boolArray)
	{
		// Make a byte array that's a multiple of 8 in length, plus 5 bytes to store the number of entries as an int32 (+ identifier)
		// We have to store the number of entries, since the boolArray length might not be a multiple of 8, so there could be some padded zeroes
		var bytes = new byte[(boolArray.Length + 7)/8 + 5];
		bytes[0] = System.Convert.ToByte (ArrayType.Bool);	// Identifier
		var bits = new BitArray(boolArray);
		bits.CopyTo (bytes, 5);
		Initialize();
		ConvertInt32ToBytes (boolArray.Length, bytes); // The number of entries in the boolArray goes in the first 4 bytes

		return SaveBytes (key, bytes);	
	}

	/// <summary>
	/// Gets the bool array that was stored with the provided key.
	/// </summary>
	/// <returns>The bool array.</returns>
	/// <param name="key">Key that was used.</param>
	public static bool[] GetBoolArray (string key)
	{
		if (PlayerPrefs.HasKey(key))
		{
			var bytes = System.Convert.FromBase64String (PlayerPrefs.GetString(key));
			if (bytes.Length < 5)
			{
				Debug.LogError ("Corrupt preference file for " + key);
				return new bool[0];
			}
			if ((ArrayType)bytes[0] != ArrayType.Bool)
			{
				Debug.LogError (key + " is not a boolean array");
				return new bool[0];
			}
			Initialize();

			// Make a new bytes array that doesn't include the number of entries + identifier (first 5 bytes) and turn that into a BitArray
			var bytes2 = new byte[bytes.Length-5];
			System.Array.Copy(bytes, 5, bytes2, 0, bytes2.Length);
			var bits = new BitArray(bytes2);
			// Get the number of entries from the first 4 bytes after the identifier and resize the BitArray to that length, then convert it to a boolean array
			bits.Length = ConvertBytesToInt32 (bytes);
			var boolArray = new bool[bits.Count];
			bits.CopyTo (boolArray, 0);

			return boolArray;
		}
		return new bool[0];
	}

	/// <summary>
	/// Gets the bool array that was stored with the provided key.
	/// </summary>
	/// <returns>The bool array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">default value if the key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static bool[] GetBoolArray (string key, bool defaultValue, int defaultSize) 
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetBoolArray(key);
		}
		var boolArray = new bool[defaultSize];
		for(int i=0;i<defaultSize;i++)
		{
			boolArray[i] = defaultValue;
		}
		return boolArray;
	}

	/// <summary>
	/// Stores the provided string array with given key.
	/// </summary>
	/// <returns><c>true</c>, if string array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="stringArray">String array to store.</param>
	public static bool SetStringArray (string key, string[] stringArray)
	{
		var bytes = new byte[stringArray.Length + 1];
		bytes[0] = System.Convert.ToByte (ArrayType.String);	// Identifier
		Initialize();

		// Store the length of each string that's in stringArray, so we can extract the correct strings in GetStringArray
		for (var i = 0; i < stringArray.Length; i++)
		{
			if (stringArray[i] == null)
			{
				Debug.LogError ("Can't save null entries in the string array when setting " + key);
				return false;
			}
			if (stringArray[i].Length > 255)
			{
				Debug.LogError ("Strings cannot be longer than 255 characters when setting " + key);
				return false;
			}
			bytes[idx++] = (byte)stringArray[i].Length;
		}

		try
		{
			PlayerPrefs.SetString (key, System.Convert.ToBase64String (bytes) + "|" + String.Join("", stringArray));
		}
		catch
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Gets the string array that was stored with the provided key.
	/// </summary>
	/// <returns>The string array.</returns>
	/// <param name="key">Key that was used.</param>
	public static string[] GetStringArray (string key)
	{
		if (PlayerPrefs.HasKey(key)) {
			var completeString = PlayerPrefs.GetString(key);
			var separatorIndex = completeString.IndexOf("|"[0]);
			if (separatorIndex < 4) {
				Debug.LogError ("Corrupt preference file for " + key);
				return new string[0];
			}
			var bytes = System.Convert.FromBase64String (completeString.Substring(0, separatorIndex));
			if ((ArrayType)bytes[0] != ArrayType.String) {
				Debug.LogError (key + " is not a string array");
				return new string[0];
			}
			Initialize();

			var numberOfEntries = bytes.Length-1;
			var stringArray = new string[numberOfEntries];
			var stringIndex = separatorIndex + 1;
			for (var i = 0; i < numberOfEntries; i++)
			{
				int stringLength = bytes[idx++];
				if (stringIndex + stringLength > completeString.Length)
				{
					Debug.LogError ("Corrupt preference file for " + key);
					return new string[0];
				}
				stringArray[i] = completeString.Substring(stringIndex, stringLength);
				stringIndex += stringLength;
			}

			return stringArray;
		}
		return new string[0];
	}

	/// <summary>
	/// Gets the string array that was stored with the provided key.
	/// </summary>
	/// <returns>The string array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static string[] GetStringArray (string key, string defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetStringArray(key);
		}
		var stringArray = new string[defaultSize];
		for(int i=0;i<defaultSize;i++)
		{
			stringArray[i] = defaultValue;
		}
		return stringArray;
	}

	/// <summary>
	/// Stores the provided int array with given key.
	/// </summary>
	/// <returns><c>true</c>, if int array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="intArray">Int array to store.</param>
	public static bool SetIntArray (string key, int[] intArray)
	{
		return SetValue (key, intArray, ArrayType.Int32, 1, ConvertFromInt);
	}

	/// <summary>
	/// Stores the provided float array with given key.
	/// </summary>
	/// <returns><c>true</c>, if float array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="floatArray">Float array to store.</param>
	public static bool SetFloatArray (string key, float[] floatArray)
	{
		return SetValue (key, floatArray, ArrayType.Float, 1, ConvertFromFloat);
	}

	/// <summary>
	/// Stores the provided Vector2 array with given key.
	/// </summary>
	/// <returns><c>true</c>, if Vector2 array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="vector2Array">Vector2 array to store.</param>
	public static bool SetVector2Array (string key, Vector2[] vector2Array )
	{
		return SetValue (key, vector2Array, ArrayType.Vector2, 2, ConvertFromVector2);
	}

	/// <summary>
	/// Stores the provided Vector3 array with given key.
	/// </summary>
	/// <returns><c>true</c>, if Vector3 array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="vector3Array">Vector3 array to store.</param>
	public static bool SetVector3Array (string key, Vector3[] vector3Array)
	{
		return SetValue (key, vector3Array, ArrayType.Vector3, 3, ConvertFromVector3);
	}

	/// <summary>
	/// Stores the provided quaternion array with given key.
	/// </summary>
	/// <returns><c>true</c>, if quaternion array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="quaternionArray">quaternion array to store.</param>
	public static bool SetQuaternionArray (string key, Quaternion[] quaternionArray )
	{
		return SetValue (key, quaternionArray, ArrayType.Quaternion, 4, ConvertFromQuaternion);
	}

	/// <summary>
	/// Stores the provided color array with given key.
	/// </summary>
	/// <returns><c>true</c>, if color array was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="colorArray">color array to store.</param>
	public static bool SetColorArray (string key, Color[] colorArray)
	{
		return SetValue (key, colorArray, ArrayType.Color, 4, ConvertFromColor);
	}

	/// <summary>
	/// Stores the provided value array with the given key.
	/// </summary>
	/// <returns><c>true</c>, if value was stored successfully, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="array">Array to store.</param>
	/// <param name="arrayType">Array type.</param>
	/// <param name="vectorNumber">number of values per vector.</param>
	/// <param name="convert">Conversion method.</param>
	private static bool SetValue<T> (string key, T array, ArrayType arrayType, int vectorNumber, Action<T, byte[],int> convert) where T : IList
	{
		var bytes = new byte[(4*array.Count)*vectorNumber + 1];
		bytes[0] = System.Convert.ToByte (arrayType);	// Identifier
		Initialize();

		for (var i = 0; i < array.Count; i++) {
			convert (array, bytes, i);	
		}
		return SaveBytes (key, bytes);
	}

	private static void ConvertFromInt (int[] array, byte[] bytes, int i)
	{
		ConvertInt32ToBytes (array[i], bytes);
	}

	private static void ConvertFromFloat (float[] array, byte[] bytes, int i)
	{
		ConvertFloatToBytes (array[i], bytes);
	}

	private static void ConvertFromVector2 (Vector2[] array, byte[] bytes, int i)
	{
		ConvertFloatToBytes (array[i].x, bytes);
		ConvertFloatToBytes (array[i].y, bytes);
	}

	private static void ConvertFromVector3 (Vector3[] array, byte[] bytes, int i)
	{
		ConvertFloatToBytes (array[i].x, bytes);
		ConvertFloatToBytes (array[i].y, bytes);
		ConvertFloatToBytes (array[i].z, bytes);
	}

	private static void ConvertFromQuaternion (Quaternion[] array, byte[] bytes, int i)
	{
		ConvertFloatToBytes (array[i].x, bytes);
		ConvertFloatToBytes (array[i].y, bytes);
		ConvertFloatToBytes (array[i].z, bytes);
		ConvertFloatToBytes (array[i].w, bytes);
	}

	private static void ConvertFromColor (Color[] array, byte[] bytes, int i)
	{
		ConvertFloatToBytes (array[i].r, bytes);
		ConvertFloatToBytes (array[i].g, bytes);
		ConvertFloatToBytes (array[i].b, bytes);
		ConvertFloatToBytes (array[i].a, bytes);
	}

	/// <summary>
	/// Gets the int array that was stored with the provided key.
	/// </summary>
	/// <returns>The int array.</returns>
	/// <param name="key">Key that was used.</param>
	public static int[] GetIntArray (string key)
	{
		var intList = new List<int>();
		GetValue (key, intList, ArrayType.Int32, 1, ConvertToInt);
		return intList.ToArray();
	}

	/// <summary>
	/// Gets the int array that was stored with the provided key.
	/// </summary>
	/// <returns>The int array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static int[] GetIntArray (string key, int defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetIntArray(key);
		}
		var intArray = new int[defaultSize];
		for (int i=0; i<defaultSize; i++)
		{
			intArray[i] = defaultValue;
		}
		return intArray;
	}

	/// <summary>
	/// Gets the float array that was stored with the provided key.
	/// </summary>
	/// <returns>The float array.</returns>
	/// <param name="key">Key that was used.</param>
	public static float[] GetFloatArray (string key)
	{
		var floatList = new List<float>();
		GetValue (key, floatList, ArrayType.Float, 1, ConvertToFloat);
		return floatList.ToArray();
	}

	/// <summary>
	/// Gets the float array that was stored with the provided key.
	/// </summary>
	/// <returns>The float array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static float[] GetFloatArray (string key, float defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetFloatArray(key);
		}
		var floatArray = new float[defaultSize];
		for (int i=0; i<defaultSize; i++)
		{
			floatArray[i] = defaultValue;
		}
		return floatArray;
	}

	/// <summary>
	/// Gets the vector2 array that was stored with the provided key.
	/// </summary>
	/// <returns>The vector2 array.</returns>
	/// <param name="key">Key that was used.</param>
	public static Vector2[] GetVector2Array (string key)
	{
		var vector2List = new List<Vector2>();
		GetValue (key, vector2List, ArrayType.Vector2, 2, ConvertToVector2);
		return vector2List.ToArray();
	}

	/// <summary>
	/// Gets the vector2 array that was stored with the provided key.
	/// </summary>
	/// <returns>The vector2 array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static Vector2[] GetVector2Array (string key, Vector2 defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetVector2Array(key);
		}
		var vector2Array = new Vector2[defaultSize];
		for(int i=0; i< defaultSize;i++)
		{
			vector2Array[i] = defaultValue;
		}
		return vector2Array;
	}

	/// <summary>
	/// Gets the vector3 array that was stored with the provided key.
	/// </summary>
	/// <returns>The vector3 array.</returns>
	/// <param name="key">Key that was used.</param>
	public static Vector3[] GetVector3Array (string key)
	{
		var vector3List = new List<Vector3>();
		GetValue (key, vector3List, ArrayType.Vector3, 3, ConvertToVector3);
		return vector3List.ToArray();
	}

	/// <summary>
	/// Gets the vector3 array that was stored with the provided key.
	/// </summary>
	/// <returns>The vector3 array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static Vector3[] GetVector3Array (string key, Vector3 defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key))

		{
			return GetVector3Array(key);
		}
		var vector3Array = new Vector3[defaultSize];
		for (int i=0; i<defaultSize;i++)
		{
			vector3Array[i] = defaultValue;
		}
		return vector3Array;
	}

	/// <summary>
	/// Gets the quaternion array that was stored with the provided key.
	/// </summary>
	/// <returns>The quaternion array.</returns>
	/// <param name="key">Key that was used.</param>
	public static Quaternion[] GetQuaternionArray (string key)
	{
		var quaternionList = new List<Quaternion>();
		GetValue (key, quaternionList, ArrayType.Quaternion, 4, ConvertToQuaternion);
		return quaternionList.ToArray();
	}

	/// <summary>
	/// Gets the quaternion array that was stored with the provided key.
	/// </summary>
	/// <returns>The quaternion array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static Quaternion[] GetQuaternionArray (string key, Quaternion defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return GetQuaternionArray(key);
		}
		var quaternionArray = new Quaternion[defaultSize];
		for(int i=0;i<defaultSize;i++)
		{
			quaternionArray[i] = defaultValue;
		}
		return quaternionArray;
	}

	/// <summary>
	/// Gets the color array that was stored with the provided key.
	/// </summary>
	/// <returns>The color array.</returns>
	/// <param name="key">Key that was used.</param>
	public static Color[] GetColorArray (string key)
	{
		var colorList = new List<Color>();
		GetValue (key, colorList, ArrayType.Color, 4, ConvertToColor);
		return colorList.ToArray();
	}

	/// <summary>
	/// Gets the color array that was stored with the provided key.
	/// </summary>
	/// <returns>The color array.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if key doesn't exist.</param>
	/// <param name="defaultSize">Default size for the default array.</param>
	public static Color[] GetColorArray (string key, Color defaultValue, int defaultSize)
	{
		if (PlayerPrefs.HasKey(key)) {
			return GetColorArray(key);
		}
		var colorArray = new Color[defaultSize];
		for(int i=0;i<defaultSize;i++)
		{
			colorArray[i] = defaultValue;
		}
		return colorArray;
	}

	/// <summary>
	/// Gets the value that was stored with the provided key.
	/// </summary>
	/// <param name="key">Key that was used.</param>
	/// <param name="list">List to store array in.</param>
	/// <param name="arrayType">Array type.</param>
	/// <param name="vectorNumber">number of values per vector.</param>
	/// <param name="convert">Conversion method.</param>
	private static void GetValue<T> (string key, T list, ArrayType arrayType, int vectorNumber, Action<T, byte[]> convert) where T : IList
	{
		if (PlayerPrefs.HasKey(key))
		{
			var bytes = System.Convert.FromBase64String (PlayerPrefs.GetString(key));
			if ((bytes.Length-1) % (vectorNumber*4) != 0)
			{
				Debug.LogError ("Corrupt preference file for " + key);
				return;
			}
			if ((ArrayType)bytes[0] != arrayType)
			{
				Debug.LogError (key + " is not a " + arrayType.ToString() + " array");
				return;
			}
			Initialize();

			var end = (bytes.Length-1) / (vectorNumber*4);
			for (var i = 0; i < end; i++)
			{
				convert (list, bytes);
			}
		}
	}

	private static void ConvertToInt (List<int> list, byte[] bytes)
	{
		list.Add (ConvertBytesToInt32(bytes));
	}

	private static void ConvertToFloat (List<float> list, byte[] bytes)
	{
		list.Add (ConvertBytesToFloat(bytes));
	}

	private static void ConvertToVector2 (List<Vector2> list, byte[] bytes)
	{
		list.Add (new Vector2(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
	}

	private static void ConvertToVector3 (List<Vector3> list, byte[] bytes)
	{
		list.Add (new Vector3(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
	}

	private static void ConvertToQuaternion (List<Quaternion> list,byte[] bytes)
	{
		list.Add (new Quaternion(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
	}

	private static void ConvertToColor (List<Color> list, byte[] bytes)
	{
		list.Add (new Color(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
	}

	public static void ShowArrayType (string key)
	{
		var bytes = System.Convert.FromBase64String (PlayerPrefs.GetString(key));
		if (bytes.Length > 0)
		{
			ArrayType arrayType = (ArrayType)bytes[0];
			Debug.Log (key + " is a " + arrayType.ToString() + " array");
		}
	}

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	private static void Initialize ()
	{
		if (System.BitConverter.IsLittleEndian)
		{
			endianDiff1 = 0;
			endianDiff2 = 0;
		}
		else
		{
			endianDiff1 = 3;
			endianDiff2 = 1;
		}
		if (byteBlock == null)
		{
			byteBlock = new byte[4];
		}
		idx = 1;
	}

	/// <summary>
	/// Stores the bytes to PlayerPrefs with given key.
	/// </summary>
	/// <returns><c>true</c>, if bytes were saved, <c>false</c> otherwise.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="bytes">Bytes to store.</param>
	private static bool SaveBytes (string key, byte[] bytes)
	{
		try
		{
			PlayerPrefs.SetString (key, System.Convert.ToBase64String (bytes));
		}
		catch
		{
			return false;
		}
		return true;
	}

	private static void ConvertFloatToBytes (float f, byte[] bytes)
	{
		byteBlock = System.BitConverter.GetBytes (f);
		ConvertTo4Bytes (bytes);
	}

	private static float ConvertBytesToFloat (byte[] bytes)
	{
		ConvertFrom4Bytes (bytes);
		return System.BitConverter.ToSingle (byteBlock, 0);
	}

	private static void ConvertInt32ToBytes (int i, byte[] bytes)
	{
		byteBlock = System.BitConverter.GetBytes (i);
		ConvertTo4Bytes (bytes);
	}

	private static int ConvertBytesToInt32 (byte[] bytes)
	{
		ConvertFrom4Bytes (bytes);
		return System.BitConverter.ToInt32 (byteBlock, 0);
	}

	private static void ConvertTo4Bytes (byte[] bytes)
	{
		bytes[idx  ] = byteBlock[    endianDiff1];
		bytes[idx+1] = byteBlock[1 + endianDiff2];
		bytes[idx+2] = byteBlock[2 - endianDiff2];
		bytes[idx+3] = byteBlock[3 - endianDiff1];
		idx += 4;
	}

	private static void ConvertFrom4Bytes (byte[] bytes)
	{
		byteBlock[    endianDiff1] = bytes[idx  ];
		byteBlock[1 + endianDiff2] = bytes[idx+1];
		byteBlock[2 - endianDiff2] = bytes[idx+2];
		byteBlock[3 - endianDiff1] = bytes[idx+3];
		idx += 4;
	}
}