using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RCF
{
	/// <summary>
	/// FPS counter.
	/// </summary>
	public class FPSCounter : MonoBehaviour 
	{
		/// <summary>
		/// Reference to text label, used to display the FPS
		/// </summary>
		public Text fpsCounter;
		private float deltaTime = 0.0f;

		void Update () 
		{
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			fpsCounter.SetText (string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps));
		}
	}
}