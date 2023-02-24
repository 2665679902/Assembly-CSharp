using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000651 RID: 1617
[AddComponentMenu("KMonoBehaviour/scripts/SweepBotStation")]
public class SweepBotStation : KMonoBehaviour
{
	// Token: 0x06002B35 RID: 11061 RVA: 0x000E3D4B File Offset: 0x000E1F4B
	public void SetStorages(Storage botMaterialStorage, Storage sweepStorage)
	{
		this.botMaterialStorage = botMaterialStorage;
		this.sweepStorage = sweepStorage;
	}

	// Token: 0x06002B36 RID: 11062 RVA: 0x000E3D5B File Offset: 0x000E1F5B
	protected override void OnPrefabInit()
	{
		this.Initialize(false);
		base.Subscribe<SweepBotStation>(-592767678, SweepBotStation.OnOperationalChangedDelegate);
	}

	// Token: 0x06002B37 RID: 11063 RVA: 0x000E3D75 File Offset: 0x000E1F75
	protected void Initialize(bool use_logic_meter)
	{
		base.OnPrefabInit();
		base.GetComponent<Operational>().SetFlag(this.dockedRobot, false);
	}

	// Token: 0x06002B38 RID: 11064 RVA: 0x000E3D90 File Offset: 0x000E1F90
	protected override void OnSpawn()
	{
		base.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_frame", "meter_level" });
		if (this.sweepBot == null || this.sweepBot.Get() == null)
		{
			this.RequestNewSweepBot(null);
		}
		else
		{
			StorageUnloadMonitor.Instance smi = this.sweepBot.Get().GetSMI<StorageUnloadMonitor.Instance>();
			smi.sm.sweepLocker.Set(this.sweepStorage, smi, false);
			this.RefreshSweepBotSubscription();
		}
		this.UpdateMeter();
		this.UpdateNameDisplay();
	}

	// Token: 0x06002B39 RID: 11065 RVA: 0x000E3E50 File Offset: 0x000E2050
	private void RequestNewSweepBot(object data = null)
	{
		if (this.botMaterialStorage.FindFirstWithMass(GameTags.RefinedMetal, SweepBotConfig.MASS) == null)
		{
			FetchList2 fetchList = new FetchList2(this.botMaterialStorage, Db.Get().ChoreTypes.Fetch);
			fetchList.Add(GameTags.RefinedMetal, null, SweepBotConfig.MASS, Operational.State.None);
			fetchList.Submit(null, true);
			return;
		}
		this.MakeNewSweepBot(null);
	}

	// Token: 0x06002B3A RID: 11066 RVA: 0x000E3EB8 File Offset: 0x000E20B8
	private void MakeNewSweepBot(object data = null)
	{
		if (this.newSweepyHandle.IsValid)
		{
			return;
		}
		if (this.botMaterialStorage.GetAmountAvailable(GameTags.RefinedMetal) < SweepBotConfig.MASS)
		{
			return;
		}
		PrimaryElement primaryElement = this.botMaterialStorage.FindFirstWithMass(GameTags.RefinedMetal, SweepBotConfig.MASS);
		if (primaryElement == null)
		{
			return;
		}
		SimHashes sweepBotMaterial = primaryElement.ElementID;
		primaryElement.Mass -= SweepBotConfig.MASS;
		this.UpdateMeter();
		this.newSweepyHandle = GameScheduler.Instance.Schedule("MakeSweepy", 2f, delegate(object obj)
		{
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("SweepBot"), Grid.CellToPos(Grid.CellRight(Grid.PosToCell(this.gameObject))), Grid.SceneLayer.Creatures, null, 0);
			gameObject.SetActive(true);
			this.sweepBot = new Ref<KSelectable>(gameObject.GetComponent<KSelectable>());
			if (!string.IsNullOrEmpty(this.storedName))
			{
				this.sweepBot.Get().GetComponent<UserNameable>().SetName(this.storedName);
			}
			this.UpdateNameDisplay();
			StorageUnloadMonitor.Instance smi = gameObject.GetSMI<StorageUnloadMonitor.Instance>();
			smi.sm.sweepLocker.Set(this.sweepStorage, smi, false);
			this.sweepBot.Get().GetComponent<PrimaryElement>().ElementID = sweepBotMaterial;
			this.RefreshSweepBotSubscription();
			this.newSweepyHandle.ClearScheduler();
		}, null, null);
		base.GetComponent<KBatchedAnimController>().Play("newsweepy", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002B3B RID: 11067 RVA: 0x000E3F84 File Offset: 0x000E2184
	private void RefreshSweepBotSubscription()
	{
		if (this.refreshSweepbotHandle != -1)
		{
			this.sweepBot.Get().Unsubscribe(this.refreshSweepbotHandle);
			this.sweepBot.Get().Unsubscribe(this.sweepBotNameChangeHandle);
		}
		this.refreshSweepbotHandle = this.sweepBot.Get().Subscribe(1969584890, new Action<object>(this.RequestNewSweepBot));
		this.sweepBotNameChangeHandle = this.sweepBot.Get().Subscribe(1102426921, new Action<object>(this.UpdateStoredName));
	}

	// Token: 0x06002B3C RID: 11068 RVA: 0x000E4014 File Offset: 0x000E2214
	private void UpdateStoredName(object data)
	{
		this.storedName = (string)data;
		this.UpdateNameDisplay();
	}

	// Token: 0x06002B3D RID: 11069 RVA: 0x000E4028 File Offset: 0x000E2228
	private void UpdateNameDisplay()
	{
		if (string.IsNullOrEmpty(this.storedName))
		{
			base.GetComponent<KSelectable>().SetName(string.Format(BUILDINGS.PREFABS.SWEEPBOTSTATION.NAMEDSTATION, ROBOTS.MODELS.SWEEPBOT.NAME));
		}
		else
		{
			base.GetComponent<KSelectable>().SetName(string.Format(BUILDINGS.PREFABS.SWEEPBOTSTATION.NAMEDSTATION, this.storedName));
		}
		NameDisplayScreen.Instance.UpdateName(base.gameObject);
	}

	// Token: 0x06002B3E RID: 11070 RVA: 0x000E4093 File Offset: 0x000E2293
	public void DockRobot(bool docked)
	{
		base.GetComponent<Operational>().SetFlag(this.dockedRobot, docked);
	}

	// Token: 0x06002B3F RID: 11071 RVA: 0x000E40A8 File Offset: 0x000E22A8
	public void StartCharging()
	{
		base.GetComponent<KBatchedAnimController>().Queue("sleep_pre", KAnim.PlayMode.Once, 1f, 0f);
		base.GetComponent<KBatchedAnimController>().Queue("sleep_idle", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06002B40 RID: 11072 RVA: 0x000E40F5 File Offset: 0x000E22F5
	public void StopCharging()
	{
		base.GetComponent<KBatchedAnimController>().Play("sleep_pst", KAnim.PlayMode.Once, 1f, 0f);
		this.UpdateNameDisplay();
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x000E4120 File Offset: 0x000E2320
	protected override void OnCleanUp()
	{
		if (this.newSweepyHandle.IsValid)
		{
			this.newSweepyHandle.ClearScheduler();
		}
		if (this.refreshSweepbotHandle != -1 && this.sweepBot.Get() != null)
		{
			this.sweepBot.Get().Unsubscribe(this.refreshSweepbotHandle);
		}
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x000E4178 File Offset: 0x000E2378
	private void UpdateMeter()
	{
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float num = Mathf.Clamp01(this.GetAmountStored() / maxCapacityMinusStorageMargin);
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x000E41B0 File Offset: 0x000E23B0
	private void OnStorageChanged(object data)
	{
		this.UpdateMeter();
		if (this.sweepBot == null || this.sweepBot.Get() == null)
		{
			this.RequestNewSweepBot(null);
		}
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component.currentFrame >= component.GetCurrentNumFrames())
		{
			base.GetComponent<KBatchedAnimController>().Play("remove", KAnim.PlayMode.Once, 1f, 0f);
		}
		for (int i = 0; i < this.sweepStorage.Count; i++)
		{
			this.sweepStorage[i].GetComponent<Clearable>().MarkForClear(false, true);
		}
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x000E4248 File Offset: 0x000E2448
	private void OnOperationalChanged(object data)
	{
		Operational component = base.GetComponent<Operational>();
		if (component.Flags.ContainsValue(false))
		{
			component.SetActive(false, false);
		}
		else
		{
			component.SetActive(true, false);
		}
		if (this.sweepBot == null || this.sweepBot.Get() == null)
		{
			this.RequestNewSweepBot(null);
		}
	}

	// Token: 0x06002B45 RID: 11077 RVA: 0x000E429E File Offset: 0x000E249E
	private float GetMaxCapacityMinusStorageMargin()
	{
		return this.sweepStorage.Capacity() - this.sweepStorage.storageFullMargin;
	}

	// Token: 0x06002B46 RID: 11078 RVA: 0x000E42B7 File Offset: 0x000E24B7
	private float GetAmountStored()
	{
		return this.sweepStorage.MassStored();
	}

	// Token: 0x0400199E RID: 6558
	[Serialize]
	public Ref<KSelectable> sweepBot;

	// Token: 0x0400199F RID: 6559
	[Serialize]
	public string storedName;

	// Token: 0x040019A0 RID: 6560
	private Operational.Flag dockedRobot = new Operational.Flag("dockedRobot", Operational.Flag.Type.Functional);

	// Token: 0x040019A1 RID: 6561
	private MeterController meter;

	// Token: 0x040019A2 RID: 6562
	[SerializeField]
	private Storage botMaterialStorage;

	// Token: 0x040019A3 RID: 6563
	[SerializeField]
	private Storage sweepStorage;

	// Token: 0x040019A4 RID: 6564
	private SchedulerHandle newSweepyHandle;

	// Token: 0x040019A5 RID: 6565
	private static readonly EventSystem.IntraObjectHandler<SweepBotStation> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<SweepBotStation>(delegate(SweepBotStation component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x040019A6 RID: 6566
	private int refreshSweepbotHandle = -1;

	// Token: 0x040019A7 RID: 6567
	private int sweepBotNameChangeHandle = -1;
}
