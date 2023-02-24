using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200013E RID: 318
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class PostEffectsBase : MonoBehaviour
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x00028B8C File Offset: 0x00026D8C
		protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
		{
			if (!s)
			{
				global::Debug.Log("Missing shader in " + this.ToString());
				base.enabled = false;
				return null;
			}
			if (s.isSupported && m2Create && m2Create.shader == s)
			{
				return m2Create;
			}
			if (!s.isSupported)
			{
				this.NotSupported();
				global::Debug.Log(string.Concat(new string[]
				{
					"The shader ",
					s.ToString(),
					" on effect ",
					this.ToString(),
					" is not supported on this platform!"
				}));
				return null;
			}
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			if (m2Create)
			{
				return m2Create;
			}
			return null;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00028C44 File Offset: 0x00026E44
		protected Material CreateMaterial(Shader s, Material m2Create)
		{
			if (!s)
			{
				global::Debug.Log("Missing shader in " + this.ToString());
				return null;
			}
			if (m2Create && m2Create.shader == s && s.isSupported)
			{
				return m2Create;
			}
			if (!s.isSupported)
			{
				return null;
			}
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			if (m2Create)
			{
				return m2Create;
			}
			return null;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00028CB5 File Offset: 0x00026EB5
		private void OnEnable()
		{
			this.isSupported = true;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00028CBE File Offset: 0x00026EBE
		protected bool CheckSupport()
		{
			return this.CheckSupport(false);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00028CC7 File Offset: 0x00026EC7
		public virtual bool CheckResources()
		{
			global::Debug.LogWarning("CheckResources () for " + this.ToString() + " should be overwritten.");
			return this.isSupported;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00028CE9 File Offset: 0x00026EE9
		protected void Start()
		{
			this.CheckResources();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00028CF4 File Offset: 0x00026EF4
		protected bool CheckSupport(bool needDepth)
		{
			this.isSupported = true;
			this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
			this.supportDX11 = SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;
			if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
			{
				this.NotSupported();
				return false;
			}
			if (needDepth)
			{
				base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
			}
			return true;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00028D55 File Offset: 0x00026F55
		protected bool CheckSupport(bool needDepth, bool needHdr)
		{
			if (!this.CheckSupport(needDepth))
			{
				return false;
			}
			if (needHdr && !this.supportHDRTextures)
			{
				this.NotSupported();
				return false;
			}
			return true;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00028D76 File Offset: 0x00026F76
		public bool Dx11Support()
		{
			return this.supportDX11;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00028D7E File Offset: 0x00026F7E
		protected void ReportAutoDisable()
		{
			global::Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00028D9C File Offset: 0x00026F9C
		private bool CheckShader(Shader s)
		{
			global::Debug.Log(string.Concat(new string[]
			{
				"The shader ",
				s.ToString(),
				" on effect ",
				this.ToString(),
				" is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package."
			}));
			if (!s.isSupported)
			{
				this.NotSupported();
				return false;
			}
			return false;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00028DF4 File Offset: 0x00026FF4
		protected void NotSupported()
		{
			base.enabled = false;
			this.isSupported = false;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00028E04 File Offset: 0x00027004
		protected void DrawBorder(RenderTexture dest, Material material)
		{
			RenderTexture.active = dest;
			bool flag = true;
			GL.PushMatrix();
			GL.LoadOrtho();
			for (int i = 0; i < material.passCount; i++)
			{
				material.SetPass(i);
				float num;
				float num2;
				if (flag)
				{
					num = 1f;
					num2 = 0f;
				}
				else
				{
					num = 0f;
					num2 = 1f;
				}
				float num3 = 0f;
				float num4 = 0f + 1f / ((float)dest.width * 1f);
				float num5 = 0f;
				float num6 = 1f;
				GL.Begin(7);
				GL.TexCoord2(0f, num);
				GL.Vertex3(num3, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num3, num6, 0.1f);
				float num7 = 1f - 1f / ((float)dest.width * 1f);
				num4 = 1f;
				num5 = 0f;
				num6 = 1f;
				GL.TexCoord2(0f, num);
				GL.Vertex3(num7, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num7, num6, 0.1f);
				float num8 = 0f;
				num4 = 1f;
				num5 = 0f;
				num6 = 0f + 1f / ((float)dest.height * 1f);
				GL.TexCoord2(0f, num);
				GL.Vertex3(num8, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num8, num6, 0.1f);
				float num9 = 0f;
				num4 = 1f;
				num5 = 1f - 1f / ((float)dest.height * 1f);
				num6 = 1f;
				GL.TexCoord2(0f, num);
				GL.Vertex3(num9, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num9, num6, 0.1f);
				GL.End();
			}
			GL.PopMatrix();
		}

		// Token: 0x040006EF RID: 1775
		protected bool supportHDRTextures = true;

		// Token: 0x040006F0 RID: 1776
		protected bool supportDX11;

		// Token: 0x040006F1 RID: 1777
		protected bool isSupported = true;
	}
}
