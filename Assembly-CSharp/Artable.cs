using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Database;
using Klei.AI;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200044C RID: 1100
[AddComponentMenu("KMonoBehaviour/Workable/Artable")]
public class Artable : Workable
{
	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060017BC RID: 6076 RVA: 0x0007C364 File Offset: 0x0007A564
	public string CurrentStage
	{
		get
		{
			return this.currentStage;
		}
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x0007C36C File Offset: 0x0007A56C
	protected Artable()
	{
		this.faceTargetWhenWorking = true;
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x0007C388 File Offset: 0x0007A588
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Arting;
		this.attributeConverter = Db.Get().AttributeConverters.ArtSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Art.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanArt.Id;
		base.SetWorkTime(80f);
	}

	// Token: 0x060017BF RID: 6079 RVA: 0x0007C41C File Offset: 0x0007A61C
	protected override void OnSpawn()
	{
		base.GetComponent<KPrefabID>().PrefabID();
		if (string.IsNullOrEmpty(this.currentStage) || this.currentStage == this.defaultArtworkId)
		{
			this.SetDefault();
		}
		else
		{
			this.SetStage(this.currentStage, true);
		}
		this.shouldShowSkillPerkStatusItem = false;
		base.OnSpawn();
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x0007C478 File Offset: 0x0007A678
	[OnDeserialized]
	public void OnDeserialized()
	{
		if (Db.GetArtableStages().TryGet(this.currentStage) == null && this.currentStage != this.defaultArtworkId)
		{
			string text = string.Format("{0}_{1}", base.GetComponent<KPrefabID>().PrefabID().ToString(), this.currentStage);
			if (Db.GetArtableStages().TryGet(text) == null)
			{
				global::Debug.LogError("Failed up to update " + this.currentStage + " to ArtableStages");
				return;
			}
			this.currentStage = text;
		}
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x0007C504 File Offset: 0x0007A704
	protected override void OnCompleteWork(Worker worker)
	{
		if (string.IsNullOrEmpty(this.userChosenTargetStage))
		{
			Db db = Db.Get();
			Tag tag = base.GetComponent<KPrefabID>().PrefabID();
			List<ArtableStage> prefabStages = Db.GetArtableStages().GetPrefabStages(tag);
			ArtableStatusItem artist_skill = db.ArtableStatuses.LookingUgly;
			MinionResume component = worker.GetComponent<MinionResume>();
			if (component != null)
			{
				if (component.HasPerk(db.SkillPerks.CanArtGreat.Id))
				{
					artist_skill = db.ArtableStatuses.LookingGreat;
				}
				else if (component.HasPerk(db.SkillPerks.CanArtOkay.Id))
				{
					artist_skill = db.ArtableStatuses.LookingOkay;
				}
			}
			prefabStages.RemoveAll((ArtableStage stage) => stage.statusItem.StatusType > artist_skill.StatusType || stage.statusItem.StatusType == ArtableStatuses.ArtableStatusType.AwaitingArting);
			prefabStages.Sort((ArtableStage x, ArtableStage y) => y.statusItem.StatusType.CompareTo(x.statusItem.StatusType));
			ArtableStatuses.ArtableStatusType highest_type = prefabStages[0].statusItem.StatusType;
			prefabStages.RemoveAll((ArtableStage stage) => stage.statusItem.StatusType < highest_type);
			prefabStages.RemoveAll((ArtableStage stage) => !stage.IsUnlocked());
			prefabStages.Shuffle<ArtableStage>();
			this.SetStage(prefabStages[0].id, false);
			if (prefabStages[0].cheerOnComplete)
			{
				new EmoteChore(worker.GetComponent<ChoreProvider>(), db.ChoreTypes.EmoteHighPriority, db.Emotes.Minion.Cheer, 1, null);
			}
			else
			{
				new EmoteChore(worker.GetComponent<ChoreProvider>(), db.ChoreTypes.EmoteHighPriority, db.Emotes.Minion.Disappointed, 1, null);
			}
		}
		else
		{
			this.SetStage(this.userChosenTargetStage, false);
			this.userChosenTargetStage = null;
		}
		this.shouldShowSkillPerkStatusItem = false;
		this.UpdateStatusItem(null);
		Prioritizable.RemoveRef(base.gameObject);
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x0007C6FC File Offset: 0x0007A8FC
	public void SetDefault()
	{
		this.currentStage = this.defaultArtworkId;
		base.GetComponent<KBatchedAnimController>().SwapAnims(base.GetComponent<Building>().Def.AnimFiles);
		base.GetComponent<KAnimControllerBase>().Play(this.defaultAnimName, KAnim.PlayMode.Once, 1f, 0f);
		KSelectable component = base.GetComponent<KSelectable>();
		BuildingDef def = base.GetComponent<Building>().Def;
		component.SetName(def.Name);
		component.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().ArtableStatuses.AwaitingArting, this);
		this.GetAttributes().Remove(this.artQualityDecorModifier);
		this.shouldShowSkillPerkStatusItem = false;
		this.UpdateStatusItem(null);
		if (this.currentStage == this.defaultArtworkId)
		{
			this.shouldShowSkillPerkStatusItem = true;
			Prioritizable.AddRef(base.gameObject);
			this.chore = new WorkChore<Artable>(Db.Get().ChoreTypes.Art, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, this.requiredSkillPerk);
		}
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x0007C820 File Offset: 0x0007AA20
	public virtual void SetStage(string stage_id, bool skip_effect)
	{
		ArtableStage artableStage = Db.GetArtableStages().Get(stage_id);
		if (artableStage == null)
		{
			global::Debug.LogError("Missing stage: " + stage_id);
			return;
		}
		this.currentStage = artableStage.id;
		base.GetComponent<KBatchedAnimController>().SwapAnims(new KAnimFile[] { Assets.GetAnim(artableStage.animFile) });
		base.GetComponent<KAnimControllerBase>().Play(artableStage.anim, KAnim.PlayMode.Once, 1f, 0f);
		this.GetAttributes().Remove(this.artQualityDecorModifier);
		if (artableStage.decor != 0)
		{
			this.artQualityDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, (float)artableStage.decor, "Art Quality", false, false, true);
			this.GetAttributes().Add(this.artQualityDecorModifier);
		}
		KSelectable component = base.GetComponent<KSelectable>();
		component.SetName(artableStage.Name);
		component.SetStatusItem(Db.Get().StatusItemCategories.Main, artableStage.statusItem, this);
		this.shouldShowSkillPerkStatusItem = false;
		this.UpdateStatusItem(null);
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x0007C932 File Offset: 0x0007AB32
	public void SetUserChosenTargetState(string stageID)
	{
		this.SetDefault();
		this.userChosenTargetStage = stageID;
	}

	// Token: 0x04000D26 RID: 3366
	[Serialize]
	private string currentStage;

	// Token: 0x04000D27 RID: 3367
	[Serialize]
	private string userChosenTargetStage;

	// Token: 0x04000D28 RID: 3368
	private AttributeModifier artQualityDecorModifier;

	// Token: 0x04000D29 RID: 3369
	private string defaultArtworkId = "Default";

	// Token: 0x04000D2A RID: 3370
	public string defaultAnimName;

	// Token: 0x04000D2B RID: 3371
	private WorkChore<Artable> chore;
}
