using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000901 RID: 2305
public class SceneInitializer : MonoBehaviour
{
	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x0600430C RID: 17164 RVA: 0x0017B1D0 File Offset: 0x001793D0
	// (set) Token: 0x0600430D RID: 17165 RVA: 0x0017B1D7 File Offset: 0x001793D7
	public static SceneInitializer Instance { get; private set; }

	// Token: 0x0600430E RID: 17166 RVA: 0x0017B1E0 File Offset: 0x001793E0
	private void Awake()
	{
		Localization.SwapToLocalizedFont();
		string environmentVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
		string text = Application.dataPath + Path.DirectorySeparatorChar.ToString() + "Plugins";
		if (!environmentVariable.Contains(text))
		{
			Environment.SetEnvironmentVariable("PATH", environmentVariable + Path.PathSeparator.ToString() + text, EnvironmentVariableTarget.Process);
		}
		SceneInitializer.Instance = this;
		this.PreLoadPrefabs();
	}

	// Token: 0x0600430F RID: 17167 RVA: 0x0017B24F File Offset: 0x0017944F
	private void OnDestroy()
	{
		SceneInitializer.Instance = null;
	}

	// Token: 0x06004310 RID: 17168 RVA: 0x0017B258 File Offset: 0x00179458
	private void PreLoadPrefabs()
	{
		foreach (GameObject gameObject in this.preloadPrefabs)
		{
			if (gameObject != null)
			{
				Util.KInstantiate(gameObject, gameObject.transform.GetPosition(), Quaternion.identity, base.gameObject, null, true, 0);
			}
		}
	}

	// Token: 0x06004311 RID: 17169 RVA: 0x0017B2D0 File Offset: 0x001794D0
	public void NewSaveGamePrefab()
	{
		if (this.prefab_NewSaveGame != null && SaveGame.Instance == null)
		{
			Util.KInstantiate(this.prefab_NewSaveGame, base.gameObject, null);
		}
	}

	// Token: 0x06004312 RID: 17170 RVA: 0x0017B300 File Offset: 0x00179500
	public void PostLoadPrefabs()
	{
		foreach (GameObject gameObject in this.prefabs)
		{
			if (gameObject != null)
			{
				Util.KInstantiate(gameObject, base.gameObject, null);
			}
		}
	}

	// Token: 0x04002CB2 RID: 11442
	public const int MAXDEPTH = -30000;

	// Token: 0x04002CB3 RID: 11443
	public const int SCREENDEPTH = -1000;

	// Token: 0x04002CB5 RID: 11445
	public GameObject prefab_NewSaveGame;

	// Token: 0x04002CB6 RID: 11446
	public List<GameObject> preloadPrefabs = new List<GameObject>();

	// Token: 0x04002CB7 RID: 11447
	public List<GameObject> prefabs = new List<GameObject>();
}
