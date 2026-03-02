using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// An Encrypted version of player prefs.
/// </summary>
public static class EncryptedPlayerPrefs
{
	/// <summary>
	/// Set false if you don't want to use encrypt/decrypt value
	/// </summary>
	public static bool useSecure = true;

	const int Iterations = 555;

	static string strPassword = "IamZETOwow!123";
	static string strSalt = "IvmD123A12";
	static bool hasSetPassword = false;

	/// <summary>
	/// Deletes all PlayerPrefs.
	/// </summary>
	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	/// <summary>
	/// Deletes the value stored with the given key.
	/// </summary>
	/// <param name="key">Key to delete.</param>
	public static void DeleteKey(string key)
	{
		PlayerPrefs.DeleteKey(Encrypt(key, strPassword));
	}

	/// <summary>
	/// Gets the bool that was stored with the provided key.
	/// </summary>
	/// <returns>the bool</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">returns the default value if the key doesn't exist</param>
	public static bool GetBool (string key, bool defaultValue = false)
	{
		return (1==GetInt(key, defaultValue?1:0));
	}

	/// <summary>
	/// Gets the float that was stored with the provided key.
	/// </summary>
	/// <returns>The float.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static float GetFloat(string key, float defaultValue = 0.0f)
	{
		float retValue = defaultValue;

		string strValue = GetString(key);

		if (float.TryParse(strValue, out retValue))
		{
			return retValue;
		}
		else
		{
			return defaultValue;
		}
	}

	/// <summary>
	/// Gets the int that was stored with the provided key.
	/// </summary>
	/// <returns>The int.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static int GetInt(string key, int defaultValue = 0)
	{
		int retValue = defaultValue;

		string strValue = GetString(key);

		if (int.TryParse(strValue, out retValue))
		{
			return retValue;
		}
		else
		{
			return defaultValue;
		}
	}

	/// <summary>
	/// Gets the string that was stored with the provided key.
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value if the key doesn't exist.</param>
	public static string GetString(string key, string defaultValue = "")
	{
		string strEncryptValue = GetStoredEncryptedString(key, defaultValue);
		return Decrypt(strEncryptValue, strPassword);
	}

	/// <summary>
	/// Gets the stored Encrypted string.
	/// </summary>
	/// <returns>The Encrypted string.</returns>
	/// <param name="key">Key that was used.</param>
	/// <param name="defaultValue">Default value.</param>
	static string GetStoredEncryptedString(string key, string defaultValue)
	{
		CheckPasswordSet();
		//Returns the encrypted version of the key with the set password
		string strEncryptKey = Encrypt(key, strPassword);
		//Returns the encrypted version of the defaultValue with the set password
		string strEncryptDefaultValue = Encrypt(defaultValue, strPassword);
		//Returns the value(value is encrypted) stored with the encrypted key 
		//or returns the default if the strEncryptKey does not exist
		string strEncryptValue = PlayerPrefs.GetString(strEncryptKey, strEncryptDefaultValue);

		return strEncryptValue;
	}

	/// <summary>
	/// Determines if a value exists that was stored with the given key.
	/// </summary>
	/// <returns><c>true</c> if the key exists; otherwise, <c>false</c>.</returns>
	/// <param name="key">Key to check.</param>
	public static bool HasKey(string key)
	{
		CheckPasswordSet();
		return PlayerPrefs.HasKey(Encrypt(key, strPassword));
	}

	/// <summary>
	/// Save the PlayerPrefs.
	/// </summary>
	public static void Save()
	{
		CheckPasswordSet();
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Sets the provided bool with the given key.
	/// </summary>
	/// <returns><c>true</c>, if it works, <c>false</c> if it fails.</returns>
	/// <param name="key">Key to use.</param>
	/// <param name="value">value to store.</param>
	public static bool SetBool (string key, bool value)
	{
		try
		{
			SetInt(key, value? 1 : 0);
		}
		catch
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Sets the provided float with the given key.
	/// </summary>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	public static void SetFloat(string key, float value)
	{
		string strValue = System.Convert.ToString(value);
		SetString(key, strValue);
	}

	/// <summary>
	/// Sets the provided int with the given key.
	/// </summary>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	public static void SetInt(string key, int value)
	{
		string strValue = System.Convert.ToString(value);
		SetString(key, strValue);
	}

	/// <summary>
	/// Sets the provided string with the given key.
	/// </summary>
	/// <param name="key">Key to use.</param>
	/// <param name="value">Value to store.</param>
	public static void SetString(string key, string value)
	{
		CheckPasswordSet();
		PlayerPrefs.SetString(Encrypt(key, strPassword), Encrypt(value, strPassword));
	}

	/// <summary>
	/// Initialize the specified strPassword and strSalt with the provided values.
	/// </summary>
	/// <param name="newPassword">New password to use for encryption.</param>
	/// <param name="newSalt">New salt to use for encryption.</param>
	public static void Initialize(string newPassword, string newSalt)
	{
		strPassword = newPassword;
		strSalt = newSalt;

		hasSetPassword = true;
	}

	/// <summary>
	/// Checks if the password has been set.
	/// </summary>
	static void CheckPasswordSet()
	{
		if (!hasSetPassword)
		{
			Initialize ("RPGFunky01!Pass!WordFinger", "FUNKYfInGeR!!Saltingword");
			if(!hasSetPassword)
				Debug.LogWarning("Set Your Own Password & Salt!!!");
		}
	}

	/// <summary>
	/// Gets the IV(Initialization Vector).
	/// </summary>
	/// <returns>The IV.</returns>
	static byte[] GetIV()
	{
		byte[] IV = Encoding.UTF8.GetBytes(strSalt);
		return IV;
	}

	/// <summary>
	/// Encrypt the specified strPlain with the provided password.
	/// </summary>
	/// <param name="strPlain">unencrypted form of the string.</param>
	/// <param name="password">Password to encrypt with.</param>
	static string Encrypt(string strPlain, string password)
	{
		if (!useSecure)
			return strPlain;

		try
		{
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();

			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, GetIV(), Iterations);

			byte[] key = rfc2898DeriveBytes.GetBytes(8);

			using (var memoryStream = new MemoryStream())
			using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, GetIV()), CryptoStreamMode.Write))
			{
				memoryStream.Write(GetIV(), 0, GetIV().Length);

				byte[] plainTextBytes = Encoding.UTF8.GetBytes(strPlain);

				cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
				cryptoStream.FlushFinalBlock();

				return Convert.ToBase64String(memoryStream.ToArray());
			}
		}
		catch (Exception e)
		{
			Debug.LogWarning("Encrypt Exception: " + e);
			return strPlain;
		}
	}

	/// <summary>
	/// Decrypt the specified strEncrypt with the provided password.
	/// </summary>
	/// <param name="strEncrypt">String encrypt.</param>
	/// <param name="password">Password.</param>
	static string Decrypt(string strEncrypt, string password)
	{
		if (!useSecure)
			return strEncrypt;

		try
		{
			byte[] cipherBytes = Convert.FromBase64String(strEncrypt);

			using (var memoryStream = new MemoryStream(cipherBytes))
			{
				DESCryptoServiceProvider des = new DESCryptoServiceProvider();

				byte[] iv = GetIV();
				memoryStream.Read(iv, 0, iv.Length);

				// use derive bytes to generate key from password and IV
				var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, iv, Iterations);

				byte[] key = rfc2898DeriveBytes.GetBytes(8);

				using (var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
				using (var streamReader = new StreamReader(cryptoStream))
				{
					string strPlain = streamReader.ReadToEnd();
					return strPlain;
				}
			}
		}
		catch (Exception e)
		{
			Debug.LogWarning("Decrypt Exception: " + e);
			return strEncrypt;
		}
	}
}