using System;
using System.IO;
using UnityEngine;

// Token: 0x02000675 RID: 1653
public class BundledAssetsLoader : KMonoBehaviour
{
	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000E9D7D File Offset: 0x000E7F7D
	// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000E9D85 File Offset: 0x000E7F85
	public BundledAssets Expansion1Assets { get; private set; }

	// Token: 0x06002C96 RID: 11414 RVA: 0x000E9D90 File Offset: 0x000E7F90
	protected override void OnPrefabInit()
	{
		BundledAssetsLoader.instance = this;
		global::Debug.Log("Expansion1: " + DlcManager.IsExpansion1Active().ToString());
		if (DlcManager.IsExpansion1Active())
		{
			global::Debug.Log("Loading Expansion1 assets from bundle");
			AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, DlcManager.GetContentBundleName("EXPANSION1_ID")));
			global::Debug.Assert(assetBundle != null, "Expansion1 is Active but its asset bundle failed to load");
			GameObject gameObject = assetBundle.LoadAsset<GameObject>("Expansion1Assets");
			global::Debug.Assert(gameObject != null, "Could not load the Expansion1Assets prefab");
			this.Expansion1Assets = Util.KInstantiate(gameObject, base.gameObject, null).GetComponent<BundledAssets>();
		}
	}

	// Token: 0x04001A85 RID: 6789
	public static BundledAssetsLoader instance;
}
