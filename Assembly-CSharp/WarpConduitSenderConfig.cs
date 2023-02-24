using System;
using TUNING;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class WarpConduitSenderConfig : IBuildingConfig
{
	// Token: 0x060010E3 RID: 4323 RVA: 0x0005B684 File Offset: 0x00059884
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0005B68C File Offset: 0x0005988C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WarpConduitSender";
		int num = 4;
		int num2 = 3;
		string text2 = "warp_conduit_sender_kanim";
		int num3 = 250;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ShowInBuildMenu = false;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0005B6FC File Offset: 0x000598FC
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.liquidInputPort;
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.gasInputPort;
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.solidInputPort;
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0005B734 File Offset: 0x00059934
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		WarpConduitSender warpConduitSender = go.AddOrGet<WarpConduitSender>();
		warpConduitSender.liquidPortInfo = this.liquidInputPort;
		warpConduitSender.gasPortInfo = this.gasInputPort;
		warpConduitSender.solidPortInfo = this.solidInputPort;
		warpConduitSender.gasStorage = go.AddComponent<Storage>();
		warpConduitSender.gasStorage.showInUI = false;
		warpConduitSender.gasStorage.capacityKg = 1f;
		warpConduitSender.liquidStorage = go.AddComponent<Storage>();
		warpConduitSender.liquidStorage.showInUI = false;
		warpConduitSender.liquidStorage.capacityKg = 10f;
		warpConduitSender.solidStorage = go.AddComponent<Storage>();
		warpConduitSender.solidStorage.showInUI = false;
		warpConduitSender.solidStorage.capacityKg = 100f;
		Activatable activatable = go.AddOrGet<Activatable>();
		activatable.synchronizeAnims = true;
		activatable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_warp_conduit_sender_kanim") };
		activatable.workAnims = new HashedString[] { "sending_pre", "sending_loop" };
		activatable.workingPstComplete = new HashedString[] { "sending_pst" };
		activatable.workingPstFailed = new HashedString[] { "sending_pre" };
		activatable.SetWorkTime(30f);
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0005B8B4 File Offset: 0x00059AB4
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<BuildingCellVisualizer>();
		go.GetComponent<Deconstructable>().SetAllowDeconstruction(false);
		go.GetComponent<Activatable>().requiredSkillPerk = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0005B8E8 File Offset: 0x00059AE8
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0005B900 File Offset: 0x00059B00
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x04000924 RID: 2340
	public const string ID = "WarpConduitSender";

	// Token: 0x04000925 RID: 2341
	private ConduitPortInfo gasInputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(0, 1));

	// Token: 0x04000926 RID: 2342
	private ConduitPortInfo liquidInputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(1, 1));

	// Token: 0x04000927 RID: 2343
	private ConduitPortInfo solidInputPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(2, 1));
}
