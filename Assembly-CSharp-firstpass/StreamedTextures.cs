using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200004A RID: 74
public static class StreamedTextures
{
	// Token: 0x06000312 RID: 786 RVA: 0x00010C64 File Offset: 0x0000EE64
	public static Texture2D GetTexture(string tex_name)
	{
		Texture2D texture2D;
		StreamedTextures.TexLookup.TryGetValue(tex_name, out texture2D);
		return texture2D;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00010C80 File Offset: 0x0000EE80
	public static void SetBundlesLoaded(bool load)
	{
		StreamedTextures.ShouldBeLoaded = load;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00010C88 File Offset: 0x0000EE88
	public static bool AreBundlesLoaded()
	{
		return StreamedTextures.ShouldBeLoaded;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00010C90 File Offset: 0x0000EE90
	public static void UpdateRequests()
	{
		for (int i = 0; i < StreamedTextures.ActiveRequests.Count; i++)
		{
			StreamedTextures.LoadRequest loadRequest = StreamedTextures.ActiveRequests[i];
			AssetBundleCreateRequest request = loadRequest.Request;
			if (request.isDone)
			{
				string bundleName = loadRequest.BundleName;
				AssetBundle assetBundle = request.assetBundle;
				global::Debug.Assert(assetBundle != null, "Failed to load bundle: " + bundleName);
				TextureBundle component = assetBundle.LoadAsset<GameObject>(bundleName).GetComponent<TextureBundle>();
				global::Debug.Assert(component != null, "Bundle format mismatch");
				foreach (Texture2D texture2D in component.HiResTextures)
				{
					string text = texture2D.name.Replace("_hires_", "_");
					DebugUtil.DevAssert(!StreamedTextures.TexLookup.ContainsKey(text), "Texture already loaded!", null);
					StreamedTextures.TexLookup[text] = texture2D;
				}
				StreamedTextures.LoadedBundle loadedBundle = default(StreamedTextures.LoadedBundle);
				loadedBundle.Name = bundleName;
				loadedBundle.AssetBundle = assetBundle;
				StreamedTextures.LoadedBundles.Add(loadedBundle);
				StreamedTextures.ActiveRequests.RemoveAt(i);
				i--;
			}
		}
		if (StreamedTextures.ShouldBeLoaded && StreamedTextures.ActiveRequests.Count == 0 && StreamedTextures.LoadedBundles.Count == 0)
		{
			for (int k = 0; k < StreamedTextures.VanillaBundles.Length; k++)
			{
				string text2 = StreamedTextures.VanillaBundles[k];
				string text3 = Path.Combine(Application.streamingAssetsPath, text2);
				StreamedTextures.LoadRequest loadRequest2 = default(StreamedTextures.LoadRequest);
				loadRequest2.BundleName = text2;
				loadRequest2.Request = AssetBundle.LoadFromFileAsync(text3);
				StreamedTextures.ActiveRequests.Add(loadRequest2);
			}
			if (DlcManager.IsExpansion1Active())
			{
				for (int l = 0; l < StreamedTextures.ExpansionBundles.Length; l++)
				{
					string text4 = StreamedTextures.ExpansionBundles[l];
					string text5 = Path.Combine(Application.streamingAssetsPath, text4);
					StreamedTextures.LoadRequest loadRequest3 = default(StreamedTextures.LoadRequest);
					loadRequest3.BundleName = text4;
					loadRequest3.Request = AssetBundle.LoadFromFileAsync(text5);
					StreamedTextures.ActiveRequests.Add(loadRequest3);
				}
			}
		}
		if (!StreamedTextures.ShouldBeLoaded && StreamedTextures.LoadedBundles.Count > 0)
		{
			while (StreamedTextures.LoadedBundles.Count > 0)
			{
				StreamedTextures.LoadedBundles[0].AssetBundle.Unload(true);
				StreamedTextures.LoadedBundles.RemoveAt(0);
			}
			StreamedTextures.TexLookup.Clear();
		}
	}

	// Token: 0x040003CE RID: 974
	private static string[] VanillaBundles = new string[] { "hires_base_bundle" };

	// Token: 0x040003CF RID: 975
	private static string[] ExpansionBundles = new string[] { "hires_expansion1_bundle" };

	// Token: 0x040003D0 RID: 976
	private static bool ShouldBeLoaded = false;

	// Token: 0x040003D1 RID: 977
	private static Dictionary<string, Texture2D> TexLookup = new Dictionary<string, Texture2D>();

	// Token: 0x040003D2 RID: 978
	private static List<StreamedTextures.LoadedBundle> LoadedBundles = new List<StreamedTextures.LoadedBundle>();

	// Token: 0x040003D3 RID: 979
	private static List<StreamedTextures.LoadRequest> ActiveRequests = new List<StreamedTextures.LoadRequest>();

	// Token: 0x020009A3 RID: 2467
	private struct LoadedBundle
	{
		// Token: 0x04002168 RID: 8552
		public string Name;

		// Token: 0x04002169 RID: 8553
		public AssetBundle AssetBundle;
	}

	// Token: 0x020009A4 RID: 2468
	private struct LoadRequest
	{
		// Token: 0x0400216A RID: 8554
		public string BundleName;

		// Token: 0x0400216B RID: 8555
		public AssetBundleCreateRequest Request;
	}
}
