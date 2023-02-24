using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeEditorFramework.Utilities
{
	// Token: 0x02000495 RID: 1173
	public static class ResourceManager
	{
		// Token: 0x06003298 RID: 12952 RVA: 0x00068BA7 File Offset: 0x00066DA7
		public static void SetDefaultResourcePath(string defaultResourcePath)
		{
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x00068BAC File Offset: 0x00066DAC
		public static string PreparePath(string path)
		{
			path = path.Replace(Application.dataPath, "Assets");
			if (path.Contains("Resources"))
			{
				path = path.Substring(path.LastIndexOf("Resources") + 10);
			}
			return path.Substring(0, path.LastIndexOf('.'));
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x00068BFD File Offset: 0x00066DFD
		public static T[] LoadResources<T>(string path) where T : UnityEngine.Object
		{
			path = ResourceManager.PreparePath(path);
			throw new NotImplementedException("Currently it is not possible to load subAssets at runtime!");
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x00068C11 File Offset: 0x00066E11
		public static T LoadResource<T>(string path) where T : UnityEngine.Object
		{
			path = ResourceManager.PreparePath(path);
			return Resources.Load<T>(path);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x00068C24 File Offset: 0x00066E24
		public static Texture2D LoadTexture(string texPath)
		{
			if (string.IsNullOrEmpty(texPath))
			{
				return null;
			}
			int num = ResourceManager.loadedTextures.FindIndex((ResourceManager.MemoryTexture memTex) => memTex.path == texPath);
			if (num != -1)
			{
				if (!(ResourceManager.loadedTextures[num].texture == null))
				{
					return ResourceManager.loadedTextures[num].texture;
				}
				ResourceManager.loadedTextures.RemoveAt(num);
			}
			Texture2D texture2D = ResourceManager.LoadResource<Texture2D>(texPath);
			ResourceManager.AddTextureToMemory(texPath, texture2D, Array.Empty<string>());
			return texture2D;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x00068CBC File Offset: 0x00066EBC
		public static Texture2D GetTintedTexture(string texPath, Color col)
		{
			string text = "Tint:" + col.ToString();
			Texture2D texture2D = ResourceManager.GetTexture(texPath, new string[] { text });
			if (texture2D == null)
			{
				texture2D = ResourceManager.LoadTexture(texPath);
				ResourceManager.AddTextureToMemory(texPath, texture2D, Array.Empty<string>());
				texture2D = RTEditorGUI.Tint(texture2D, col);
				ResourceManager.AddTextureToMemory(texPath, texture2D, new string[] { text });
			}
			return texture2D;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x00068D28 File Offset: 0x00066F28
		public static void AddTextureToMemory(string texturePath, Texture2D texture, params string[] modifications)
		{
			if (texture == null)
			{
				return;
			}
			ResourceManager.loadedTextures.Add(new ResourceManager.MemoryTexture(texturePath, texture, modifications));
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x00068D48 File Offset: 0x00066F48
		public static ResourceManager.MemoryTexture FindInMemory(Texture2D tex)
		{
			int num = ResourceManager.loadedTextures.FindIndex((ResourceManager.MemoryTexture memTex) => memTex.texture == tex);
			if (num == -1)
			{
				return null;
			}
			return ResourceManager.loadedTextures[num];
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x00068D8C File Offset: 0x00066F8C
		public static bool HasInMemory(string texturePath, params string[] modifications)
		{
			int num = ResourceManager.loadedTextures.FindIndex((ResourceManager.MemoryTexture memTex) => memTex.path == texturePath);
			return num != -1 && ResourceManager.EqualModifications(ResourceManager.loadedTextures[num].modifications, modifications);
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x00068DDC File Offset: 0x00066FDC
		public static ResourceManager.MemoryTexture GetMemoryTexture(string texturePath, params string[] modifications)
		{
			List<ResourceManager.MemoryTexture> list = ResourceManager.loadedTextures.FindAll((ResourceManager.MemoryTexture memTex) => memTex.path == texturePath);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			foreach (ResourceManager.MemoryTexture memoryTexture in list)
			{
				if (ResourceManager.EqualModifications(memoryTexture.modifications, modifications))
				{
					return memoryTexture;
				}
			}
			return null;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x00068E6C File Offset: 0x0006706C
		public static Texture2D GetTexture(string texturePath, params string[] modifications)
		{
			ResourceManager.MemoryTexture memoryTexture = ResourceManager.GetMemoryTexture(texturePath, modifications);
			if (memoryTexture != null)
			{
				return memoryTexture.texture;
			}
			return null;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x00068E8C File Offset: 0x0006708C
		private static bool EqualModifications(string[] modsA, string[] modsB)
		{
			return modsA.Length == modsB.Length && Array.TrueForAll<string>(modsA, (string mod) => modsB.Count((string oMod) => mod == oMod) == modsA.Count((string oMod) => mod == oMod));
		}

		// Token: 0x0400118D RID: 4493
		private static List<ResourceManager.MemoryTexture> loadedTextures = new List<ResourceManager.MemoryTexture>();

		// Token: 0x02000AC8 RID: 2760
		public class MemoryTexture
		{
			// Token: 0x06005763 RID: 22371 RVA: 0x000A325D File Offset: 0x000A145D
			public MemoryTexture(string texPath, Texture2D tex, params string[] mods)
			{
				this.path = texPath;
				this.texture = tex;
				this.modifications = mods;
			}

			// Token: 0x040024F4 RID: 9460
			public string path;

			// Token: 0x040024F5 RID: 9461
			public Texture2D texture;

			// Token: 0x040024F6 RID: 9462
			public string[] modifications;
		}
	}
}
