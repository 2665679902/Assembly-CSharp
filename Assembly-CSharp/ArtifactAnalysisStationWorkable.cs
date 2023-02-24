using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000576 RID: 1398
public class ArtifactAnalysisStationWorkable : Workable
{
	// Token: 0x060021C2 RID: 8642 RVA: 0x000B7D00 File Offset: 0x000B5F00
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanStudyArtifact.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.AnalyzingArtifact;
		this.attributeConverter = Db.Get().AttributeConverters.ArtSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_artifact_analysis_kanim") };
		base.SetWorkTime(150f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
		Components.ArtifactAnalysisStations.Add(this);
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x000B7DC9 File Offset: 0x000B5FC9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		this.animController.SetSymbolVisiblity("snapTo_artifact", false);
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x000B7DF3 File Offset: 0x000B5FF3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.ArtifactAnalysisStations.Remove(this);
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x000B7E06 File Offset: 0x000B6006
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.InitialDisplayStoredArtifact();
	}

	// Token: 0x060021C6 RID: 8646 RVA: 0x000B7E15 File Offset: 0x000B6015
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		this.PositionArtifact();
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x000B7E28 File Offset: 0x000B6028
	private void InitialDisplayStoredArtifact()
	{
		GameObject gameObject = base.GetComponent<Storage>().GetItems()[0];
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.GetBatchInstanceData().ClearOverrideTransformMatrix();
		}
		gameObject.transform.SetPosition(new Vector3(base.transform.position.x, base.transform.position.y, Grid.GetLayerZ(Grid.SceneLayer.BuildingBack)));
		gameObject.SetActive(true);
		component.enabled = false;
		component.enabled = true;
		this.PositionArtifact();
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x000B7EB4 File Offset: 0x000B60B4
	private void ReleaseStoredArtifact()
	{
		Storage component = base.GetComponent<Storage>();
		GameObject gameObject = component.GetItems()[0];
		KBatchedAnimController component2 = gameObject.GetComponent<KBatchedAnimController>();
		gameObject.transform.SetPosition(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Grid.GetLayerZ(Grid.SceneLayer.Ore)));
		component2.enabled = false;
		component2.enabled = true;
		component.Drop(gameObject, true);
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x000B7F28 File Offset: 0x000B6128
	private void PositionArtifact()
	{
		GameObject gameObject = base.GetComponent<Storage>().GetItems()[0];
		bool flag;
		Vector3 vector = this.animController.GetSymbolTransform("snapTo_artifact", out flag).GetColumn(3);
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingBack);
		gameObject.transform.SetPosition(vector);
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x000B7F86 File Offset: 0x000B6186
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.ConsumeCharm();
		this.ReleaseStoredArtifact();
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x000B7F9C File Offset: 0x000B619C
	private void ConsumeCharm()
	{
		GameObject gameObject = this.storage.FindFirst(GameTags.CharmedArtifact);
		DebugUtil.DevAssertArgs(gameObject != null, new object[] { "ArtifactAnalysisStation finished studying a charmed artifact but there is not one in its storage" });
		if (gameObject != null)
		{
			this.YieldPayload(gameObject.GetComponent<SpaceArtifact>());
			gameObject.GetComponent<SpaceArtifact>().RemoveCharm();
		}
		if (ArtifactSelector.Instance.RecordArtifactAnalyzed(gameObject.GetComponent<KPrefabID>().PrefabID().ToString()))
		{
			if (gameObject.HasTag(GameTags.TerrestrialArtifact))
			{
				ArtifactSelector.Instance.IncrementAnalyzedTerrestrialArtifacts();
				return;
			}
			ArtifactSelector.Instance.IncrementAnalyzedSpaceArtifacts();
		}
	}

	// Token: 0x060021CC RID: 8652 RVA: 0x000B803C File Offset: 0x000B623C
	private void YieldPayload(SpaceArtifact artifact)
	{
		if (this.nextYeildRoll == -1f)
		{
			this.nextYeildRoll = UnityEngine.Random.Range(0f, 1f);
		}
		if (this.nextYeildRoll <= artifact.GetArtifactTier().payloadDropChance)
		{
			GameUtil.KInstantiate(Assets.GetPrefab("GeneShufflerRecharge"), this.statesInstance.master.transform.position + this.finishedArtifactDropOffset, Grid.SceneLayer.Ore, null, 0).SetActive(true);
		}
		int num = Mathf.FloorToInt(artifact.GetArtifactTier().payloadDropChance * 20f);
		for (int i = 0; i < num; i++)
		{
			GameUtil.KInstantiate(Assets.GetPrefab("OrbitalResearchDatabank"), this.statesInstance.master.transform.position + this.finishedArtifactDropOffset, Grid.SceneLayer.Ore, null, 0).SetActive(true);
		}
		this.nextYeildRoll = UnityEngine.Random.Range(0f, 1f);
	}

	// Token: 0x04001376 RID: 4982
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04001377 RID: 4983
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04001378 RID: 4984
	[SerializeField]
	public Vector3 finishedArtifactDropOffset;

	// Token: 0x04001379 RID: 4985
	private Notification notification;

	// Token: 0x0400137A RID: 4986
	public ArtifactAnalysisStation.StatesInstance statesInstance;

	// Token: 0x0400137B RID: 4987
	private KBatchedAnimController animController;

	// Token: 0x0400137C RID: 4988
	[Serialize]
	private float nextYeildRoll = -1f;
}
