using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000504 RID: 1284
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Workable")]
public class Workable : KMonoBehaviour, ISaveLoadable, IApproachable
{
	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06001E5D RID: 7773 RVA: 0x000A2CCE File Offset: 0x000A0ECE
	// (set) Token: 0x06001E5E RID: 7774 RVA: 0x000A2CD6 File Offset: 0x000A0ED6
	public Worker worker { get; protected set; }

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06001E5F RID: 7775 RVA: 0x000A2CDF File Offset: 0x000A0EDF
	// (set) Token: 0x06001E60 RID: 7776 RVA: 0x000A2CE7 File Offset: 0x000A0EE7
	public float WorkTimeRemaining
	{
		get
		{
			return this.workTimeRemaining;
		}
		set
		{
			this.workTimeRemaining = value;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000A2CF0 File Offset: 0x000A0EF0
	// (set) Token: 0x06001E62 RID: 7778 RVA: 0x000A2CF8 File Offset: 0x000A0EF8
	public bool preferUnreservedCell { get; set; }

	// Token: 0x06001E63 RID: 7779 RVA: 0x000A2D01 File Offset: 0x000A0F01
	public virtual float GetWorkTime()
	{
		return this.workTime;
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x000A2D09 File Offset: 0x000A0F09
	public Worker GetWorker()
	{
		return this.worker;
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x000A2D11 File Offset: 0x000A0F11
	public virtual float GetPercentComplete()
	{
		if (this.workTimeRemaining > this.workTime)
		{
			return -1f;
		}
		return 1f - this.workTimeRemaining / this.workTime;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x000A2D3A File Offset: 0x000A0F3A
	public void ConfigureMultitoolContext(HashedString context, Tag hitEffectTag)
	{
		this.multitoolContext = context;
		this.multitoolHitEffectTag = hitEffectTag;
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x000A2D4C File Offset: 0x000A0F4C
	public virtual Workable.AnimInfo GetAnim(Worker worker)
	{
		Workable.AnimInfo animInfo = default(Workable.AnimInfo);
		if (this.overrideAnims != null && this.overrideAnims.Length != 0)
		{
			BuildingFacade buildingFacade = this.GetBuildingFacade();
			bool flag = false;
			if (buildingFacade != null && !buildingFacade.IsOriginal)
			{
				flag = buildingFacade.interactAnims.TryGetValue(base.name, out animInfo.overrideAnims);
			}
			if (!flag)
			{
				animInfo.overrideAnims = this.overrideAnims;
			}
		}
		if (this.multitoolContext.IsValid && this.multitoolHitEffectTag.IsValid)
		{
			animInfo.smi = new MultitoolController.Instance(this, worker, this.multitoolContext, Assets.GetPrefab(this.multitoolHitEffectTag));
		}
		return animInfo;
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x000A2DEF File Offset: 0x000A0FEF
	public virtual HashedString[] GetWorkAnims(Worker worker)
	{
		return this.workAnims;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x000A2DF7 File Offset: 0x000A0FF7
	public virtual KAnim.PlayMode GetWorkAnimPlayMode()
	{
		return this.workAnimPlayMode;
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x000A2DFF File Offset: 0x000A0FFF
	public virtual HashedString[] GetWorkPstAnims(Worker worker, bool successfully_completed)
	{
		if (successfully_completed)
		{
			return this.workingPstComplete;
		}
		return this.workingPstFailed;
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x000A2E11 File Offset: 0x000A1011
	public virtual Vector3 GetWorkOffset()
	{
		return Vector3.zero;
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x000A2E18 File Offset: 0x000A1018
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().MiscStatusItems.Using;
		this.workingStatusItem = Db.Get().MiscStatusItems.Operating;
		this.readyForSkillWorkStatusItem = Db.Get().BuildingStatusItems.RequiresSkillPerk;
		this.workTime = this.GetWorkTime();
		this.workTimeRemaining = Mathf.Min(this.workTimeRemaining, this.workTime);
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x000A2E90 File Offset: 0x000A1090
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.shouldShowSkillPerkStatusItem && !string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			if (this.skillsUpdateHandle != -1)
			{
				Game.Instance.Unsubscribe(this.skillsUpdateHandle);
			}
			this.skillsUpdateHandle = Game.Instance.Subscribe(-1523247426, new Action<object>(this.UpdateStatusItem));
		}
		if (this.requireMinionToWork && this.minionUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.minionUpdateHandle);
		}
		this.minionUpdateHandle = Game.Instance.Subscribe(586301400, new Action<object>(this.UpdateStatusItem));
		base.GetComponent<KPrefabID>().AddTag(GameTags.HasChores, false);
		if (base.gameObject.HasTag(this.laboratoryEfficiencyBonusTagRequired))
		{
			this.useLaboratoryEfficiencyBonus = true;
			base.Subscribe<Workable>(144050788, Workable.OnUpdateRoomDelegate);
		}
		this.ShowProgressBar(this.alwaysShowProgressBar && this.workTimeRemaining < this.GetWorkTime());
		this.UpdateStatusItem(null);
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x000A2F98 File Offset: 0x000A1198
	private void RefreshRoom()
	{
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(base.gameObject));
		if (cavityForCell != null && cavityForCell.room != null)
		{
			this.OnUpdateRoom(cavityForCell.room);
			return;
		}
		this.OnUpdateRoom(null);
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x000A2FE0 File Offset: 0x000A11E0
	private void OnUpdateRoom(object data)
	{
		if (this.worker == null)
		{
			return;
		}
		Room room = (Room)data;
		if (room != null && room.roomType == Db.Get().RoomTypes.Laboratory)
		{
			this.currentlyInLaboratory = true;
			if (this.laboratoryEfficiencyBonusStatusItemHandle == Guid.Empty)
			{
				this.laboratoryEfficiencyBonusStatusItemHandle = this.worker.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.LaboratoryWorkEfficiencyBonus, this);
				return;
			}
		}
		else
		{
			this.currentlyInLaboratory = false;
			if (this.laboratoryEfficiencyBonusStatusItemHandle != Guid.Empty)
			{
				this.laboratoryEfficiencyBonusStatusItemHandle = this.worker.GetComponent<KSelectable>().RemoveStatusItem(this.laboratoryEfficiencyBonusStatusItemHandle, false);
			}
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x000A3094 File Offset: 0x000A1294
	protected virtual void UpdateStatusItem(object data = null)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (component == null)
		{
			return;
		}
		component.RemoveStatusItem(this.workStatusItemHandle, false);
		if (this.worker == null)
		{
			if (this.requireMinionToWork && Components.LiveMinionIdentities.GetWorldItems(this.GetMyWorldId(), false).Count == 0)
			{
				this.workStatusItemHandle = component.AddStatusItem(Db.Get().BuildingStatusItems.WorkRequiresMinion, null);
				return;
			}
			if (this.shouldShowSkillPerkStatusItem && !string.IsNullOrEmpty(this.requiredSkillPerk))
			{
				if (!MinionResume.AnyMinionHasPerk(this.requiredSkillPerk, this.GetMyWorldId()))
				{
					StatusItem statusItem = (DlcManager.FeatureClusterSpaceEnabled() ? Db.Get().BuildingStatusItems.ClusterColonyLacksRequiredSkillPerk : Db.Get().BuildingStatusItems.ColonyLacksRequiredSkillPerk);
					this.workStatusItemHandle = component.AddStatusItem(statusItem, this.requiredSkillPerk);
					return;
				}
				this.workStatusItemHandle = component.AddStatusItem(this.readyForSkillWorkStatusItem, this.requiredSkillPerk);
				return;
			}
		}
		else if (this.workingStatusItem != null)
		{
			this.workStatusItemHandle = component.AddStatusItem(this.workingStatusItem, this);
		}
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x000A31AC File Offset: 0x000A13AC
	protected override void OnLoadLevel()
	{
		this.overrideAnims = null;
		base.OnLoadLevel();
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x000A31BB File Offset: 0x000A13BB
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x000A31C4 File Offset: 0x000A13C4
	public void StartWork(Worker worker_to_start)
	{
		global::Debug.Assert(worker_to_start != null, "How did we get a null worker?");
		this.worker = worker_to_start;
		this.UpdateStatusItem(null);
		if (this.showProgressBar)
		{
			this.ShowProgressBar(true);
		}
		if (this.useLaboratoryEfficiencyBonus)
		{
			this.RefreshRoom();
		}
		this.OnStartWork(this.worker);
		if (this.worker != null)
		{
			string conversationTopic = this.GetConversationTopic();
			if (conversationTopic != null)
			{
				this.worker.Trigger(937885943, conversationTopic);
			}
		}
		if (this.OnWorkableEventCB != null)
		{
			this.OnWorkableEventCB(this, Workable.WorkableEvent.WorkStarted);
		}
		this.numberOfUses++;
		if (this.worker != null)
		{
			if (base.gameObject.GetComponent<KSelectable>() != null && base.gameObject.GetComponent<KSelectable>().IsSelected && this.worker.gameObject.GetComponent<LoopingSounds>() != null)
			{
				this.worker.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(true);
			}
			else if (this.worker.gameObject.GetComponent<KSelectable>() != null && this.worker.gameObject.GetComponent<KSelectable>().IsSelected && base.gameObject.GetComponent<LoopingSounds>() != null)
			{
				base.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(true);
			}
		}
		base.gameObject.Trigger(853695848, this);
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x000A3330 File Offset: 0x000A1530
	public bool WorkTick(Worker worker, float dt)
	{
		bool flag = false;
		if (dt > 0f)
		{
			this.workTimeRemaining -= dt;
			flag = this.OnWorkTick(worker, dt);
		}
		return flag || this.workTimeRemaining < 0f;
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x000A3370 File Offset: 0x000A1570
	public virtual float GetEfficiencyMultiplier(Worker worker)
	{
		float num = 1f;
		if (this.attributeConverter != null)
		{
			AttributeConverterInstance converter = worker.GetComponent<AttributeConverters>().GetConverter(this.attributeConverter.Id);
			num += converter.Evaluate();
		}
		if (this.lightEfficiencyBonus)
		{
			int num2 = Grid.PosToCell(worker.gameObject);
			if (Grid.IsValidCell(num2))
			{
				if (Grid.LightIntensity[num2] > 0)
				{
					this.currentlyLit = true;
					num += 0.15f;
					if (this.lightEfficiencyBonusStatusItemHandle == Guid.Empty)
					{
						this.lightEfficiencyBonusStatusItemHandle = worker.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.LightWorkEfficiencyBonus, this);
					}
				}
				else
				{
					this.currentlyLit = false;
					if (this.lightEfficiencyBonusStatusItemHandle != Guid.Empty)
					{
						worker.GetComponent<KSelectable>().RemoveStatusItem(this.lightEfficiencyBonusStatusItemHandle, false);
					}
				}
			}
		}
		if (this.useLaboratoryEfficiencyBonus && this.currentlyInLaboratory)
		{
			num += 0.1f;
		}
		return Mathf.Max(num, this.minimumAttributeMultiplier);
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x000A346C File Offset: 0x000A166C
	public virtual Klei.AI.Attribute GetWorkAttribute()
	{
		if (this.attributeConverter != null)
		{
			return this.attributeConverter.attribute;
		}
		return null;
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x000A3484 File Offset: 0x000A1684
	public virtual string GetConversationTopic()
	{
		KPrefabID component = base.GetComponent<KPrefabID>();
		if (!component.HasTag(GameTags.NotConversationTopic))
		{
			return component.PrefabTag.Name;
		}
		return null;
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x000A34B2 File Offset: 0x000A16B2
	public float GetAttributeExperienceMultiplier()
	{
		return this.attributeExperienceMultiplier;
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x000A34BA File Offset: 0x000A16BA
	public string GetSkillExperienceSkillGroup()
	{
		return this.skillExperienceSkillGroup;
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x000A34C2 File Offset: 0x000A16C2
	public float GetSkillExperienceMultiplier()
	{
		return this.skillExperienceMultiplier;
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x000A34CA File Offset: 0x000A16CA
	protected virtual bool OnWorkTick(Worker worker, float dt)
	{
		return false;
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x000A34D0 File Offset: 0x000A16D0
	public void StopWork(Worker workerToStop, bool aborted)
	{
		if (this.worker == workerToStop && aborted)
		{
			this.OnAbortWork(workerToStop);
		}
		if (this.shouldTransferDiseaseWithWorker)
		{
			this.TransferDiseaseWithWorker(workerToStop);
		}
		if (this.OnWorkableEventCB != null)
		{
			this.OnWorkableEventCB(this, Workable.WorkableEvent.WorkStopped);
		}
		this.OnStopWork(workerToStop);
		if (this.resetProgressOnStop)
		{
			this.workTimeRemaining = this.GetWorkTime();
		}
		this.ShowProgressBar(this.alwaysShowProgressBar && this.workTimeRemaining < this.GetWorkTime());
		if (this.lightEfficiencyBonusStatusItemHandle != Guid.Empty)
		{
			this.lightEfficiencyBonusStatusItemHandle = workerToStop.GetComponent<KSelectable>().RemoveStatusItem(this.lightEfficiencyBonusStatusItemHandle, false);
		}
		if (this.laboratoryEfficiencyBonusStatusItemHandle != Guid.Empty)
		{
			this.laboratoryEfficiencyBonusStatusItemHandle = this.worker.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.LaboratoryWorkEfficiencyBonus, false);
		}
		if (base.gameObject.GetComponent<KSelectable>() != null && !base.gameObject.GetComponent<KSelectable>().IsSelected && base.gameObject.GetComponent<LoopingSounds>() != null)
		{
			base.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(false);
		}
		else if (workerToStop.gameObject.GetComponent<KSelectable>() != null && !workerToStop.gameObject.GetComponent<KSelectable>().IsSelected && workerToStop.gameObject.GetComponent<LoopingSounds>() != null)
		{
			workerToStop.gameObject.GetComponent<LoopingSounds>().UpdateObjectSelection(false);
		}
		this.worker = null;
		base.gameObject.Trigger(679550494, this);
		this.UpdateStatusItem(null);
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x000A3667 File Offset: 0x000A1867
	public virtual StatusItem GetWorkerStatusItem()
	{
		return this.workerStatusItem;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x000A366F File Offset: 0x000A186F
	public void SetWorkerStatusItem(StatusItem item)
	{
		this.workerStatusItem = item;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x000A3678 File Offset: 0x000A1878
	public void CompleteWork(Worker worker)
	{
		if (this.shouldTransferDiseaseWithWorker)
		{
			this.TransferDiseaseWithWorker(worker);
		}
		this.OnCompleteWork(worker);
		if (this.OnWorkableEventCB != null)
		{
			this.OnWorkableEventCB(this, Workable.WorkableEvent.WorkCompleted);
		}
		this.workTimeRemaining = this.GetWorkTime();
		this.ShowProgressBar(false);
		base.gameObject.Trigger(-2011693419, this);
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x000A36D4 File Offset: 0x000A18D4
	public void SetReportType(ReportManager.ReportType report_type)
	{
		this.reportType = report_type;
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x000A36DD File Offset: 0x000A18DD
	public ReportManager.ReportType GetReportType()
	{
		return this.reportType;
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x000A36E5 File Offset: 0x000A18E5
	protected virtual void OnStartWork(Worker worker)
	{
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x000A36E7 File Offset: 0x000A18E7
	protected virtual void OnStopWork(Worker worker)
	{
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x000A36E9 File Offset: 0x000A18E9
	protected virtual void OnCompleteWork(Worker worker)
	{
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x000A36EB File Offset: 0x000A18EB
	protected virtual void OnAbortWork(Worker worker)
	{
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x000A36ED File Offset: 0x000A18ED
	public virtual void OnPendingCompleteWork(Worker worker)
	{
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x000A36EF File Offset: 0x000A18EF
	public void SetOffsets(CellOffset[] offsets)
	{
		if (this.offsetTracker != null)
		{
			this.offsetTracker.Clear();
		}
		this.offsetTracker = new StandardOffsetTracker(offsets);
	}

	// Token: 0x06001E88 RID: 7816 RVA: 0x000A3710 File Offset: 0x000A1910
	public void SetOffsetTable(CellOffset[][] offset_table)
	{
		if (this.offsetTracker != null)
		{
			this.offsetTracker.Clear();
		}
		this.offsetTracker = new OffsetTableTracker(offset_table, this);
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x000A3732 File Offset: 0x000A1932
	public virtual CellOffset[] GetOffsets(int cell)
	{
		if (this.offsetTracker == null)
		{
			this.offsetTracker = new StandardOffsetTracker(new CellOffset[1]);
		}
		return this.offsetTracker.GetOffsets(cell);
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x000A3759 File Offset: 0x000A1959
	public CellOffset[] GetOffsets()
	{
		return this.GetOffsets(Grid.PosToCell(this));
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x000A3767 File Offset: 0x000A1967
	public void SetWorkTime(float work_time)
	{
		this.workTime = work_time;
		this.workTimeRemaining = work_time;
	}

	// Token: 0x06001E8C RID: 7820 RVA: 0x000A3777 File Offset: 0x000A1977
	public bool ShouldFaceTargetWhenWorking()
	{
		return this.faceTargetWhenWorking;
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x000A377F File Offset: 0x000A197F
	public virtual Vector3 GetFacingTarget()
	{
		return base.transform.GetPosition();
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x000A378C File Offset: 0x000A198C
	public void ShowProgressBar(bool show)
	{
		if (show)
		{
			if (this.progressBar == null)
			{
				this.progressBar = ProgressBar.CreateProgressBar(base.gameObject, new Func<float>(this.GetPercentComplete));
			}
			this.progressBar.gameObject.SetActive(true);
			return;
		}
		if (this.progressBar != null)
		{
			this.progressBar.gameObject.DeleteObject();
			this.progressBar = null;
		}
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x000A3800 File Offset: 0x000A1A00
	protected override void OnCleanUp()
	{
		this.ShowProgressBar(false);
		if (this.offsetTracker != null)
		{
			this.offsetTracker.Clear();
		}
		if (this.skillsUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.skillsUpdateHandle);
		}
		if (this.minionUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.minionUpdateHandle);
		}
		base.OnCleanUp();
		this.OnWorkableEventCB = null;
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x000A3868 File Offset: 0x000A1A68
	public virtual Vector3 GetTargetPoint()
	{
		Vector3 vector = base.transform.GetPosition();
		float num = vector.y + 0.65f;
		KBoxCollider2D component = base.GetComponent<KBoxCollider2D>();
		if (component != null)
		{
			vector = component.bounds.center;
		}
		vector.y = num;
		vector.z = 0f;
		return vector;
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x000A38C2 File Offset: 0x000A1AC2
	public int GetNavigationCost(Navigator navigator, int cell)
	{
		return navigator.GetNavigationCost(cell, this.GetOffsets(cell));
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x000A38D2 File Offset: 0x000A1AD2
	public int GetNavigationCost(Navigator navigator)
	{
		return this.GetNavigationCost(navigator, Grid.PosToCell(this));
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x000A38E1 File Offset: 0x000A1AE1
	private void TransferDiseaseWithWorker(Worker worker)
	{
		if (this == null || worker == null)
		{
			return;
		}
		Workable.TransferDiseaseWithWorker(base.gameObject, worker.gameObject);
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x000A3908 File Offset: 0x000A1B08
	public static void TransferDiseaseWithWorker(GameObject workable, GameObject worker)
	{
		if (workable == null || worker == null)
		{
			return;
		}
		PrimaryElement component = workable.GetComponent<PrimaryElement>();
		if (component == null)
		{
			return;
		}
		PrimaryElement component2 = worker.GetComponent<PrimaryElement>();
		if (component2 == null)
		{
			return;
		}
		SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
		invalid.idx = component2.DiseaseIdx;
		invalid.count = (int)((float)component2.DiseaseCount * 0.33f);
		SimUtil.DiseaseInfo invalid2 = SimUtil.DiseaseInfo.Invalid;
		invalid2.idx = component.DiseaseIdx;
		invalid2.count = (int)((float)component.DiseaseCount * 0.33f);
		component2.ModifyDiseaseCount(-invalid.count, "Workable.TransferDiseaseWithWorker");
		component.ModifyDiseaseCount(-invalid2.count, "Workable.TransferDiseaseWithWorker");
		if (invalid.count > 0)
		{
			component.AddDisease(invalid.idx, invalid.count, "Workable.TransferDiseaseWithWorker");
		}
		if (invalid2.count > 0)
		{
			component2.AddDisease(invalid2.idx, invalid2.count, "Workable.TransferDiseaseWithWorker");
		}
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x000A3A00 File Offset: 0x000A1C00
	public virtual bool InstantlyFinish(Worker worker)
	{
		float num = worker.workable.WorkTimeRemaining;
		if (!float.IsInfinity(num))
		{
			worker.Work(num);
			return true;
		}
		DebugUtil.DevAssert(false, this.ToString() + " was asked to instantly finish but it has infinite work time! Override InstantlyFinish in your workable!", null);
		return false;
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x000A3A44 File Offset: 0x000A1C44
	public virtual List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.trackUses)
		{
			Descriptor descriptor = new Descriptor(string.Format(BUILDING.DETAILS.USE_COUNT, this.numberOfUses), string.Format(BUILDING.DETAILS.USE_COUNT_TOOLTIP, this.numberOfUses), Descriptor.DescriptorType.Detail, false);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x000A3AA4 File Offset: 0x000A1CA4
	public virtual BuildingFacade GetBuildingFacade()
	{
		return base.GetComponent<BuildingFacade>();
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x000A3AAC File Offset: 0x000A1CAC
	[ContextMenu("Refresh Reachability")]
	public void RefreshReachability()
	{
		if (this.offsetTracker != null)
		{
			this.offsetTracker.ForceRefresh();
		}
	}

	// Token: 0x04001102 RID: 4354
	public float workTime;

	// Token: 0x04001103 RID: 4355
	public Vector3 AnimOffset = Vector3.zero;

	// Token: 0x04001104 RID: 4356
	protected bool showProgressBar = true;

	// Token: 0x04001105 RID: 4357
	public bool alwaysShowProgressBar;

	// Token: 0x04001106 RID: 4358
	protected bool lightEfficiencyBonus = true;

	// Token: 0x04001107 RID: 4359
	protected Guid lightEfficiencyBonusStatusItemHandle;

	// Token: 0x04001108 RID: 4360
	public bool currentlyLit;

	// Token: 0x04001109 RID: 4361
	public Tag laboratoryEfficiencyBonusTagRequired = RoomConstraints.ConstraintTags.ScienceBuilding;

	// Token: 0x0400110A RID: 4362
	private bool useLaboratoryEfficiencyBonus;

	// Token: 0x0400110B RID: 4363
	protected Guid laboratoryEfficiencyBonusStatusItemHandle;

	// Token: 0x0400110C RID: 4364
	private bool currentlyInLaboratory;

	// Token: 0x0400110D RID: 4365
	protected StatusItem workerStatusItem;

	// Token: 0x0400110E RID: 4366
	protected StatusItem workingStatusItem;

	// Token: 0x0400110F RID: 4367
	protected Guid workStatusItemHandle;

	// Token: 0x04001110 RID: 4368
	protected OffsetTracker offsetTracker;

	// Token: 0x04001111 RID: 4369
	[SerializeField]
	protected string attributeConverterId;

	// Token: 0x04001112 RID: 4370
	protected AttributeConverter attributeConverter;

	// Token: 0x04001113 RID: 4371
	protected float minimumAttributeMultiplier = 0.5f;

	// Token: 0x04001114 RID: 4372
	public bool resetProgressOnStop;

	// Token: 0x04001115 RID: 4373
	protected bool shouldTransferDiseaseWithWorker = true;

	// Token: 0x04001116 RID: 4374
	[SerializeField]
	protected float attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;

	// Token: 0x04001117 RID: 4375
	[SerializeField]
	protected string skillExperienceSkillGroup;

	// Token: 0x04001118 RID: 4376
	[SerializeField]
	protected float skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;

	// Token: 0x04001119 RID: 4377
	public bool triggerWorkReactions = true;

	// Token: 0x0400111A RID: 4378
	public ReportManager.ReportType reportType = ReportManager.ReportType.WorkTime;

	// Token: 0x0400111B RID: 4379
	[SerializeField]
	[Tooltip("What layer does the dupe switch to when interacting with the building")]
	public Grid.SceneLayer workLayer = Grid.SceneLayer.Move;

	// Token: 0x0400111C RID: 4380
	[SerializeField]
	[Serialize]
	protected float workTimeRemaining = float.PositiveInfinity;

	// Token: 0x0400111D RID: 4381
	[SerializeField]
	public KAnimFile[] overrideAnims;

	// Token: 0x0400111E RID: 4382
	[SerializeField]
	protected HashedString multitoolContext;

	// Token: 0x0400111F RID: 4383
	[SerializeField]
	protected Tag multitoolHitEffectTag;

	// Token: 0x04001120 RID: 4384
	[SerializeField]
	[Tooltip("Whether to user the KAnimSynchronizer or not")]
	public bool synchronizeAnims = true;

	// Token: 0x04001121 RID: 4385
	[SerializeField]
	[Tooltip("Whether to display number of uses in the details panel")]
	public bool trackUses;

	// Token: 0x04001122 RID: 4386
	[Serialize]
	protected int numberOfUses;

	// Token: 0x04001123 RID: 4387
	public Action<Workable, Workable.WorkableEvent> OnWorkableEventCB;

	// Token: 0x04001124 RID: 4388
	private int skillsUpdateHandle = -1;

	// Token: 0x04001125 RID: 4389
	private int minionUpdateHandle = -1;

	// Token: 0x04001126 RID: 4390
	public string requiredSkillPerk;

	// Token: 0x04001127 RID: 4391
	[SerializeField]
	protected bool shouldShowSkillPerkStatusItem = true;

	// Token: 0x04001128 RID: 4392
	[SerializeField]
	public bool requireMinionToWork;

	// Token: 0x04001129 RID: 4393
	protected StatusItem readyForSkillWorkStatusItem;

	// Token: 0x0400112A RID: 4394
	public HashedString[] workAnims = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x0400112B RID: 4395
	public HashedString[] workingPstComplete = new HashedString[] { "working_pst" };

	// Token: 0x0400112C RID: 4396
	public HashedString[] workingPstFailed = new HashedString[] { "working_pst" };

	// Token: 0x0400112D RID: 4397
	public KAnim.PlayMode workAnimPlayMode;

	// Token: 0x0400112E RID: 4398
	public bool faceTargetWhenWorking;

	// Token: 0x0400112F RID: 4399
	private static readonly EventSystem.IntraObjectHandler<Workable> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<Workable>(delegate(Workable component, object data)
	{
		component.OnUpdateRoom(data);
	});

	// Token: 0x04001130 RID: 4400
	protected ProgressBar progressBar;

	// Token: 0x0200113E RID: 4414
	public enum WorkableEvent
	{
		// Token: 0x04005A57 RID: 23127
		WorkStarted,
		// Token: 0x04005A58 RID: 23128
		WorkCompleted,
		// Token: 0x04005A59 RID: 23129
		WorkStopped
	}

	// Token: 0x0200113F RID: 4415
	public struct AnimInfo
	{
		// Token: 0x04005A5A RID: 23130
		public KAnimFile[] overrideAnims;

		// Token: 0x04005A5B RID: 23131
		public StateMachine.Instance smi;
	}
}
