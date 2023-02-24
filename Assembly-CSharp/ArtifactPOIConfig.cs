using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class ArtifactPOIConfig : IMultiEntityConfig
{
	// Token: 0x06000B61 RID: 2913 RVA: 0x00041274 File Offset: 0x0003F474
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (ArtifactPOIConfig.ArtifactPOIParams artifactPOIParams in this.GenerateConfigs())
		{
			list.Add(ArtifactPOIConfig.CreateArtifactPOI(artifactPOIParams.id, artifactPOIParams.anim, Strings.Get(artifactPOIParams.nameStringKey), Strings.Get(artifactPOIParams.descStringKey), artifactPOIParams.poiType.idHash));
		}
		return list;
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0004130C File Offset: 0x0003F50C
	public static GameObject CreateArtifactPOI(string id, string anim, string name, string desc, HashedString poiType)
	{
		GameObject gameObject = EntityTemplates.CreateEntity(id, id, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<ArtifactPOIConfigurator>().presetType = poiType;
		ArtifactPOIClusterGridEntity artifactPOIClusterGridEntity = gameObject.AddOrGet<ArtifactPOIClusterGridEntity>();
		artifactPOIClusterGridEntity.m_name = name;
		artifactPOIClusterGridEntity.m_Anim = anim;
		gameObject.AddOrGetDef<ArtifactPOIStates.Def>();
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextSpaceEntry));
		return gameObject;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00041361 File Offset: 0x0003F561
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00041363 File Offset: 0x0003F563
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00041368 File Offset: 0x0003F568
	private List<ArtifactPOIConfig.ArtifactPOIParams> GenerateConfigs()
	{
		List<ArtifactPOIConfig.ArtifactPOIParams> list = new List<ArtifactPOIConfig.ArtifactPOIParams>();
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_1", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation1", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_2", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation2", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_3", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation3", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_4", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation4", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_5", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation5", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_6", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation6", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_7", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation7", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("station_8", new ArtifactPOIConfigurator.ArtifactPOIType("GravitasSpaceStation8", null, false, 30000f, 60000f, "EXPANSION1_ID")));
		list.Add(new ArtifactPOIConfig.ArtifactPOIParams("russels_teapot", new ArtifactPOIConfigurator.ArtifactPOIType("RussellsTeapot", "artifact_TeaPot", true, 30000f, 60000f, "EXPANSION1_ID")));
		list.RemoveAll((ArtifactPOIConfig.ArtifactPOIParams poi) => !poi.poiType.dlcID.IsNullOrWhiteSpace() && !DlcManager.IsContentActive(poi.poiType.dlcID));
		return list;
	}

	// Token: 0x040006C8 RID: 1736
	public const string GravitasSpaceStation1 = "GravitasSpaceStation1";

	// Token: 0x040006C9 RID: 1737
	public const string GravitasSpaceStation2 = "GravitasSpaceStation2";

	// Token: 0x040006CA RID: 1738
	public const string GravitasSpaceStation3 = "GravitasSpaceStation3";

	// Token: 0x040006CB RID: 1739
	public const string GravitasSpaceStation4 = "GravitasSpaceStation4";

	// Token: 0x040006CC RID: 1740
	public const string GravitasSpaceStation5 = "GravitasSpaceStation5";

	// Token: 0x040006CD RID: 1741
	public const string GravitasSpaceStation6 = "GravitasSpaceStation6";

	// Token: 0x040006CE RID: 1742
	public const string GravitasSpaceStation7 = "GravitasSpaceStation7";

	// Token: 0x040006CF RID: 1743
	public const string GravitasSpaceStation8 = "GravitasSpaceStation8";

	// Token: 0x040006D0 RID: 1744
	public const string RussellsTeapot = "RussellsTeapot";

	// Token: 0x02000EE3 RID: 3811
	public struct ArtifactPOIParams
	{
		// Token: 0x06006D58 RID: 27992 RVA: 0x0029A048 File Offset: 0x00298248
		public ArtifactPOIParams(string anim, ArtifactPOIConfigurator.ArtifactPOIType poiType)
		{
			this.id = "ArtifactSpacePOI_" + poiType.id;
			this.anim = anim;
			this.nameStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.ARTIFACT_POI." + poiType.id.ToUpper() + ".NAME");
			this.descStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.ARTIFACT_POI." + poiType.id.ToUpper() + ".DESC");
			this.poiType = poiType;
		}

		// Token: 0x0400529F RID: 21151
		public string id;

		// Token: 0x040052A0 RID: 21152
		public string anim;

		// Token: 0x040052A1 RID: 21153
		public StringKey nameStringKey;

		// Token: 0x040052A2 RID: 21154
		public StringKey descStringKey;

		// Token: 0x040052A3 RID: 21155
		public ArtifactPOIConfigurator.ArtifactPOIType poiType;
	}
}
