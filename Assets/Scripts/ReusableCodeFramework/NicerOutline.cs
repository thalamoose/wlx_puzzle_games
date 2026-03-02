//Author: Melang http://forum.unity3d.com/members/melang.593409/
using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	/// <summary>
	/// Nicer outline effect.
	/// An outline that looks a bit nicer than the default one. It has less "holes" in the outline by drawing more copies of the effect
	/// </summary>
	[AddComponentMenu ("UI/Effects/NicerOutline", 15)]
	public class NicerOutline : BaseMeshEffect
	{
		[SerializeField]
		private Color m_EffectColor = new Color (0f, 0f, 0f, 0.5f);
		
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2 (1f, -1f);
		
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty ();
				}
			}
		}
		
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				value.x = Mathf.Clamp (value.x, -600, 600);
				value.y = Mathf.Clamp (value.y, -600, 600);

				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty ();
				}
			}
		}
		
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty ();
				}
			}
		}

		protected void ApplyShadow (List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			UIVertex vt;

			int requiredCapacity = verts.Count * 2;
			if (verts.Capacity < requiredCapacity)
				verts.Capacity = requiredCapacity;
			
			for (int i = start; i < end; ++i)
			{
				vt = verts [i];
				verts.Add (vt);

				Vector3 position = vt.position;
				position.x += x;
				position.y += y;
				vt.position = position;
				Color32 newColor = color;
				if (m_UseGraphicAlpha)
					newColor.a = (byte)(newColor.a * verts [i].color.a / 255);
				vt.color = newColor;
				verts [i] = vt;
			}
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
				return;
			
			List<UIVertex> vertexList = new List<UIVertex>();
			vh.GetUIVertexStream(vertexList);
			
			ModifyVertices(vertexList);

			vh.Clear();
			vh.AddUIVertexTriangleStream(vertexList);
		}
		
		public void ModifyVertices (List<UIVertex> verts)
		{
			Text foundtext = GetComponent<Text>();
			
			float best_fit_adjustment = 1f;
			
			if (foundtext && foundtext.resizeTextForBestFit)  
			{
				best_fit_adjustment = (float)foundtext.cachedTextGenerator.fontSizeUsedForBestFit / (foundtext.resizeTextMaxSize - 1); //max size seems to be exclusive 

			}

			float distanceX = this.effectDistance.x * best_fit_adjustment;
			float distanceY = this.effectDistance.y * best_fit_adjustment;

			int start = 0;
			int count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, distanceX, distanceY);
			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, distanceX, -distanceY);
			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, -distanceX, distanceY);
			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, -distanceX, -distanceY);

			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, distanceX, 0);
			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, -distanceX, 0);

			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, 0, distanceY);
			start = count;
			count = verts.Count;
			this.ApplyShadow (verts, this.effectColor, start, verts.Count, 0, -distanceY);
		}

#if UNITY_EDITOR
		protected override void OnValidate ()
		{
			this.effectDistance = this.m_EffectDistance;
			base.OnValidate ();
		}
#endif
	}
}