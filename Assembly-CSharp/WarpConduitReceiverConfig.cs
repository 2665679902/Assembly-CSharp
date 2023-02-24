using System;
using TUNING;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class WarpConduitReceiverConfig : IBuildingConfig
{
	// Token: 0x060010DB RID: 4315 RVA: 0x0005B408 File Offset: 0x00059608
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0005B410 File Offset: 0x00059610
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WarpConduitReceiver";
		int num = 4;
		int num2 = 3;
		string text2 = "warp_conduit_receiver_kanim";
		int num3 = 250;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.DefaultAnimState = "off";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ShowInBuildMenu = false;
		buildingDef.Disinfectable = false;
		buildingDef.Invincible = true;
		buildingDef.Repairable = false;
		return buildingDef;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0005B48E File Offset: 0x0005968E
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.liquidOutputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.gasOutputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.solidOutputPort;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0005B4C4 File Offset: 0x000596C4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		WarpConduitReceiver warpConduitReceiver = go.AddOrGet<WarpConduitReceiver>();
		warpConduitReceiver.liquidPortInfo = this.liquidOutputPort;
		warpConduitReceiver.gasPortInfo = this.gasOutputPort;
		warpConduitReceiver.solidPortInfo = this.solidOutputPort;
		Activatable activatable = go.AddOrGet<Activatable>();
		activatable.synchronizeAnims = true;
		activatable.workAnims = new HashedString[] { "touchpanel_interact_pre", "touchpanel_interact_loop" };
		activatable.workingPstComplete = new HashedString[] { "touchpanel_interact_pst" };
		activatable.workingPstFailed = new HashedString[] { "touchpanel_interact_pst" };
		activatable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_warp_conduit_receiver_kanim") };
		activatable.SetWorkTime(30f);
		go.AddComponent<ConduitSecondaryOutput>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0005B5D3 File Offset: 0x000597D3
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<BuildingCellVisualizer>();
		go.GetComponent<Deconstructable>().SetAllowDeconstruction(false);
		go.GetComponent<Activatable>().requiredSkillPerk = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0005B607 File Offset: 0x00059807
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0005B61F File Offset: 0x0005981F
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x04000920 RID: 2336
	public const string ID = "WarpConduitReceiver";

	// Token: 0x04000921 RID: 2337
	private ConduitPortInfo liquidOutputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(0, 1));

	// Token: 0x04000922 RID: 2338
	private ConduitPortInfo gasOutputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(-1, 1));

	// Token: 0x04000923 RID: 2339
	private ConduitPortInfo solidOutputPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(1, 1));
}
