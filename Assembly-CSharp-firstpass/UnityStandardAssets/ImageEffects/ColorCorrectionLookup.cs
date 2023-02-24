using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200013D RID: 317
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (3D Lookup Texture)")]
	public class ColorCorrectionLookup : PostEffectsBase
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x000287AE File Offset: 0x000269AE
		private void Awake()
		{
			this.supports3dTextures = SystemInfo.supports3DTextures;
			base.CheckSupport(false);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000287C3 File Offset: 0x000269C3
		public override bool CheckResources()
		{
			this.material = base.CheckShaderAndCreateMaterial(this.shader, this.material);
			if (!this.isSupported || !this.supports3dTextures)
			{
				base.ReportAutoDisable();
			}
			return this.isSupported;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000287F9 File Offset: 0x000269F9
		private void OnDisable()
		{
			if (this.material)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
				this.material = null;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002881A File Offset: 0x00026A1A
		private void OnDestroy()
		{
			if (this.converted3DLut)
			{
				UnityEngine.Object.DestroyImmediate(this.converted3DLut);
			}
			if (this.converted3DLut2)
			{
				UnityEngine.Object.DestroyImmediate(this.converted3DLut2);
			}
			this.converted3DLut = null;
			this.converted3DLut2 = null;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002885A File Offset: 0x00026A5A
		public void SetIdentityLut()
		{
			this.SetIdentityLut(ref this.converted3DLut);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00028868 File Offset: 0x00026A68
		public void SetIdentityLut2()
		{
			this.SetIdentityLut(ref this.converted3DLut);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00028878 File Offset: 0x00026A78
		private void SetIdentityLut(ref Texture3D target)
		{
			int num = 16;
			Color[] array = new Color[num * num * num];
			float num2 = 1f / (1f * (float)num - 1f);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
					}
				}
			}
			if (target)
			{
				UnityEngine.Object.DestroyImmediate(target);
			}
			target = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
			target.SetPixels(array);
			target.Apply();
			this.basedOnTempTex = "";
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00028947 File Offset: 0x00026B47
		public bool ValidDimensions(Texture2D tex2d)
		{
			return tex2d && tex2d.height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002896F File Offset: 0x00026B6F
		public void Convert(Texture2D temp2DTex, string path)
		{
			this.Convert(temp2DTex, path, ref this.converted3DLut);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002897F File Offset: 0x00026B7F
		public void Convert2(Texture2D temp2DTex, string path)
		{
			this.Convert(temp2DTex, path, ref this.converted3DLut2);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00028990 File Offset: 0x00026B90
		private void Convert(Texture2D temp2DTex, string path, ref Texture3D target)
		{
			if (!temp2DTex)
			{
				global::Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
				return;
			}
			int num = temp2DTex.width * temp2DTex.height;
			num = temp2DTex.height;
			if (!this.ValidDimensions(temp2DTex))
			{
				global::Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
				this.basedOnTempTex = "";
				return;
			}
			Color[] pixels = temp2DTex.GetPixels();
			Color[] array = new Color[pixels.Length];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						int num2 = num - j - 1;
						array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
					}
				}
			}
			if (target)
			{
				UnityEngine.Object.DestroyImmediate(target);
			}
			target = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
			target.SetPixels(array);
			target.Apply();
			this.basedOnTempTex = path;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00028A94 File Offset: 0x00026C94
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!this.CheckResources() || !this.supports3dTextures)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.converted3DLut == null)
			{
				this.SetIdentityLut();
			}
			if (this.converted3DLut2 == null)
			{
				this.SetIdentityLut2();
			}
			int width = this.converted3DLut.width;
			this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			this.material.SetFloat("_Scale", (float)(width - 1) / (1f * (float)width));
			this.material.SetFloat("_Offset", 1f / (2f * (float)width));
			this.material.SetTexture("_ClutTex", this.converted3DLut);
			this.material.SetTexture("_ClutTex2", this.converted3DLut2);
			Graphics.Blit(source, destination, this.material, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 1 : 0);
		}

		// Token: 0x040006E9 RID: 1769
		public Shader shader;

		// Token: 0x040006EA RID: 1770
		private Material material;

		// Token: 0x040006EB RID: 1771
		public Texture3D converted3DLut;

		// Token: 0x040006EC RID: 1772
		public Texture3D converted3DLut2;

		// Token: 0x040006ED RID: 1773
		public string basedOnTempTex = "";

		// Token: 0x040006EE RID: 1774
		private bool supports3dTextures;
	}
}
