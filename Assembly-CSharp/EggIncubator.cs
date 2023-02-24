﻿using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020005AE RID: 1454
[SerializationConfig(MemberSerialization.OptIn)]
public class EggIncubator : SingleEntityReceptacle, ISaveLoadable, ISim1000ms
{
	// Token: 0x060023F5 RID: 9205 RVA: 0x000C28C4 File Offset: 0x000C0AC4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.autoReplaceEntity = true;
		this.choreType = Db.Get().ChoreTypes.RanchingFetch;
		this.statusItemNeed = Db.Get().BuildingStatusItems.NeedEgg;
		this.statusItemNoneAvailable = Db.Get().BuildingStatusItems.NoAvailableEgg;
		this.statusItemAwaitingDelivery = Db.Get().BuildingStatusItems.AwaitingEggDelivery;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanWrangleCreatures.Id;
		this.occupyingObjectRelativePosition = new Vector3(0.5f, 1f, -1f);
		this.synchronizeAnims = false;
		base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("egg_target", false);
		this.meter = new MeterController(this, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		base.Subscribe<EggIncubator>(-905833192, EggIncubator.OnCopySettingsDelegate);
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x000C29A8 File Offset: 0x000C0BA8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (base.occupyingObject)
		{
			if (base.occupyingObject.HasTag(GameTags.Creature))
			{
				this.storage.allowItemRemoval = true;
			}
			this.storage.RenotifyAll();
			this.PositionOccupyingObject();
		}
		base.Subscribe<EggIncubator>(-592767678, EggIncubator.OnOperationalChangedDelegate);
		base.Subscribe<EggIncubator>(-731304873, EggIncubator.OnOccupantChangedDelegate);
		base.Subscribe<EggIncubator>(-1697596308, EggIncubator.OnStorageChangeDelegate);
		this.smi = new EggIncubatorStates.Instance(this);
		this.smi.StartSM();
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x000C2A44 File Offset: 0x000C0C44
	private void OnCopySettings(object data)
	{
		EggIncubator component = ((GameObject)data).GetComponent<EggIncubator>();
		if (component != null)
		{
			this.autoReplaceEntity = component.autoReplaceEntity;
			if (base.occupyingObject == null)
			{
				if (!(this.requestedEntityTag == component.requestedEntityTag) || !(this.requestedEntityAdditionalFilterTag == component.requestedEntityAdditionalFilterTag))
				{
					base.CancelActiveRequest();
				}
				if (this.fetchChore == null)
				{
					Tag requestedEntityTag = component.requestedEntityTag;
					this.CreateOrder(requestedEntityTag, component.requestedEntityAdditionalFilterTag);
				}
			}
			if (base.occupyingObject != null)
			{
				Prioritizable component2 = base.GetComponent<Prioritizable>();
				if (component2 != null)
				{
					Prioritizable component3 = base.occupyingObject.GetComponent<Prioritizable>();
					if (component3 != null)
					{
						component3.SetMasterPriority(component2.GetMasterPriority());
					}
				}
			}
		}
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x000C2B0D File Offset: 0x000C0D0D
	protected override void OnCleanUp()
	{
		this.smi.StopSM("cleanup");
		base.OnCleanUp();
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x000C2B28 File Offset: 0x000C0D28
	protected override void SubscribeToOccupant()
	{
		base.SubscribeToOccupant();
		if (base.occupyingObject != null)
		{
			this.tracker = base.occupyingObject.AddComponent<KBatchedAnimTracker>();
			this.tracker.symbol = "egg_target";
			this.tracker.forceAlwaysVisible = true;
		}
		this.UpdateProgress();
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x000C2B81 File Offset: 0x000C0D81
	protected override void UnsubscribeFromOccupant()
	{
		base.UnsubscribeFromOccupant();
		UnityEngine.Object.Destroy(this.tracker);
		this.tracker = null;
		this.UpdateProgress();
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x000C2BA4 File Offset: 0x000C0DA4
	private void OnOperationalChanged(object data = null)
	{
		if (!base.occupyingObject)
		{
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x000C2BD6 File Offset: 0x000C0DD6
	private void OnOccupantChanged(object data = null)
	{
		if (!base.occupyingObject)
		{
			this.storage.allowItemRemoval = false;
		}
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x000C2BF1 File Offset: 0x000C0DF1
	private void OnStorageChange(object data = null)
	{
		if (base.occupyingObject && !this.storage.items.Contains(base.occupyingObject))
		{
			this.UnsubscribeFromOccupant();
			this.ClearOccupant();
		}
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x000C2C24 File Offset: 0x000C0E24
	protected override void ClearOccupant()
	{
		bool flag = false;
		if (base.occupyingObject != null)
		{
			flag = !base.occupyingObject.HasTag(GameTags.Egg);
		}
		base.ClearOccupant();
		if (this.autoReplaceEntity && flag && this.requestedEntityTag.IsValid)
		{
			this.CreateOrder(this.requestedEntityTag, Tag.Invalid);
		}
	}

	// Token: 0x060023FF RID: 9215 RVA: 0x000C2C84 File Offset: 0x000C0E84
	protected override void PositionOccupyingObject()
	{
		base.PositionOccupyingObject();
		base.occupyingObject.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.BuildingUse);
		KSelectable component = base.occupyingObject.GetComponent<KSelectable>();
		if (component != null)
		{
			component.IsSelectable = true;
		}
	}

	// Token: 0x06002400 RID: 9216 RVA: 0x000C2CC8 File Offset: 0x000C0EC8
	public override void OrderRemoveOccupant()
	{
		UnityEngine.Object.Destroy(this.tracker);
		this.tracker = null;
		this.storage.DropAll(false, false, default(Vector3), true, null);
		base.occupyingObject = null;
		this.ClearOccupant();
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x000C2D0C File Offset: 0x000C0F0C
	public float GetProgress()
	{
		float num = 0f;
		if (base.occupyingObject)
		{
			AmountInstance amountInstance = base.occupyingObject.GetAmounts().Get(Db.Get().Amounts.Incubation);
			if (amountInstance != null)
			{
				num = amountInstance.value / amountInstance.GetMax();
			}
			else
			{
				num = 1f;
			}
		}
		return num;
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x000C2D66 File Offset: 0x000C0F66
	private void UpdateProgress()
	{
		this.meter.SetPositionPercent(this.GetProgress());
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x000C2D79 File Offset: 0x000C0F79
	public void Sim1000ms(float dt)
	{
		this.UpdateProgress();
		this.UpdateChore();
	}

	// Token: 0x06002404 RID: 9220 RVA: 0x000C2D88 File Offset: 0x000C0F88
	public void StoreBaby(GameObject baby)
	{
		this.UnsubscribeFromOccupant();
		this.storage.DropAll(false, false, default(Vector3), true, null);
		this.storage.allowItemRemoval = true;
		this.storage.Store(baby, false, false, true, false);
		base.occupyingObject = baby;
		this.SubscribeToOccupant();
		base.Trigger(-731304873, base.occupyingObject);
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x000C2DF0 File Offset: 0x000C0FF0
	private void UpdateChore()
	{
		if (this.operational.IsOperational && this.EggNeedsAttention())
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<EggIncubatorWorkable>(Db.Get().ChoreTypes.EggSing, this.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				return;
			}
		}
		else if (this.chore != null)
		{
			this.chore.Cancel("now is not the time for song");
			this.chore = null;
		}
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x000C2E6C File Offset: 0x000C106C
	private bool EggNeedsAttention()
	{
		if (!base.Occupant)
		{
			return false;
		}
		IncubationMonitor.Instance instance = base.Occupant.GetSMI<IncubationMonitor.Instance>();
		return instance != null && !instance.HasSongBuff();
	}

	// Token: 0x040014AA RID: 5290
	[MyCmpAdd]
	private EggIncubatorWorkable workable;

	// Token: 0x040014AB RID: 5291
	[MyCmpAdd]
	private CopyBuildingSettings copySettings;

	// Token: 0x040014AC RID: 5292
	private Chore chore;

	// Token: 0x040014AD RID: 5293
	private EggIncubatorStates.Instance smi;

	// Token: 0x040014AE RID: 5294
	private KBatchedAnimTracker tracker;

	// Token: 0x040014AF RID: 5295
	private MeterController meter;

	// Token: 0x040014B0 RID: 5296
	private static readonly EventSystem.IntraObjectHandler<EggIncubator> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<EggIncubator>(delegate(EggIncubator component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x040014B1 RID: 5297
	private static readonly EventSystem.IntraObjectHandler<EggIncubator> OnOccupantChangedDelegate = new EventSystem.IntraObjectHandler<EggIncubator>(delegate(EggIncubator component, object data)
	{
		component.OnOccupantChanged(data);
	});

	// Token: 0x040014B2 RID: 5298
	private static readonly EventSystem.IntraObjectHandler<EggIncubator> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<EggIncubator>(delegate(EggIncubator component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x040014B3 RID: 5299
	private static readonly EventSystem.IntraObjectHandler<EggIncubator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<EggIncubator>(delegate(EggIncubator component, object data)
	{
		component.OnCopySettings(data);
	});
}
