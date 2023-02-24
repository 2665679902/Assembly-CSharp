using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000A7D RID: 2685
[SerializationConfig(MemberSerialization.OptIn)]
public class CreatureLure : StateMachineComponent<CreatureLure.StatesInstance>
{
	// Token: 0x06005224 RID: 21028 RVA: 0x001DA8A9 File Offset: 0x001D8AA9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.operational = base.GetComponent<Operational>();
		base.Subscribe<CreatureLure>(-905833192, CreatureLure.OnCopySettingsDelegate);
	}

	// Token: 0x06005225 RID: 21029 RVA: 0x001DA8D0 File Offset: 0x001D8AD0
	private void OnCopySettings(object data)
	{
		CreatureLure component = ((GameObject)data).GetComponent<CreatureLure>();
		if (component != null)
		{
			this.ChangeBaitSetting(component.activeBaitSetting);
		}
	}

	// Token: 0x06005226 RID: 21030 RVA: 0x001DA900 File Offset: 0x001D8B00
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		if (this.activeBaitSetting == Tag.Invalid)
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoLureElementSelected, null);
		}
		else
		{
			this.ChangeBaitSetting(this.activeBaitSetting);
			this.OnStorageChange(null);
		}
		base.Subscribe<CreatureLure>(-1697596308, CreatureLure.OnStorageChangeDelegate);
	}

	// Token: 0x06005227 RID: 21031 RVA: 0x001DA974 File Offset: 0x001D8B74
	private void OnStorageChange(object data = null)
	{
		bool flag = this.baitStorage.GetAmountAvailable(this.activeBaitSetting) > 0f;
		this.operational.SetFlag(this.baited, flag);
	}

	// Token: 0x06005228 RID: 21032 RVA: 0x001DA9AC File Offset: 0x001D8BAC
	public void ChangeBaitSetting(Tag baitSetting)
	{
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("SwitchedResource");
		}
		if (baitSetting != this.activeBaitSetting)
		{
			this.activeBaitSetting = baitSetting;
			this.baitStorage.DropAll(false, false, default(Vector3), true, null);
		}
		base.smi.GoTo(base.smi.sm.idle);
		this.baitStorage.storageFilters = new List<Tag> { this.activeBaitSetting };
		if (baitSetting != Tag.Invalid)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.NoLureElementSelected, false);
			if (base.smi.master.baitStorage.IsEmpty())
			{
				this.CreateFetchChore();
				return;
			}
		}
		else
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoLureElementSelected, null);
		}
	}

	// Token: 0x06005229 RID: 21033 RVA: 0x001DAA98 File Offset: 0x001D8C98
	protected void CreateFetchChore()
	{
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("Overwrite");
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.AwaitingBaitDelivery, false);
		if (this.activeBaitSetting == Tag.Invalid)
		{
			return;
		}
		this.fetchChore = new FetchChore(Db.Get().ChoreTypes.RanchingFetch, this.baitStorage, 100f, new HashSet<Tag> { this.activeBaitSetting }, FetchChore.MatchCriteria.MatchID, Tag.Invalid, null, null, true, null, null, null, Operational.State.None, 0);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.AwaitingBaitDelivery, null);
	}

	// Token: 0x0400375D RID: 14173
	public static float CONSUMPTION_RATE = 1f;

	// Token: 0x0400375E RID: 14174
	[Serialize]
	public Tag activeBaitSetting;

	// Token: 0x0400375F RID: 14175
	public List<Tag> baitTypes;

	// Token: 0x04003760 RID: 14176
	public Storage baitStorage;

	// Token: 0x04003761 RID: 14177
	protected FetchChore fetchChore;

	// Token: 0x04003762 RID: 14178
	private Operational operational;

	// Token: 0x04003763 RID: 14179
	private Operational.Flag baited = new Operational.Flag("Baited", Operational.Flag.Type.Requirement);

	// Token: 0x04003764 RID: 14180
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04003765 RID: 14181
	private static readonly EventSystem.IntraObjectHandler<CreatureLure> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<CreatureLure>(delegate(CreatureLure component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04003766 RID: 14182
	private static readonly EventSystem.IntraObjectHandler<CreatureLure> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<CreatureLure>(delegate(CreatureLure component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001908 RID: 6408
	public class StatesInstance : GameStateMachine<CreatureLure.States, CreatureLure.StatesInstance, CreatureLure, object>.GameInstance
	{
		// Token: 0x06008F0C RID: 36620 RVA: 0x0030F80D File Offset: 0x0030DA0D
		public StatesInstance(CreatureLure master)
			: base(master)
		{
		}
	}

	// Token: 0x02001909 RID: 6409
	public class States : GameStateMachine<CreatureLure.States, CreatureLure.StatesInstance, CreatureLure>
	{
		// Token: 0x06008F0D RID: 36621 RVA: 0x0030F818 File Offset: 0x0030DA18
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("off", KAnim.PlayMode.Loop).Enter(delegate(CreatureLure.StatesInstance smi)
			{
				if (smi.master.activeBaitSetting != Tag.Invalid)
				{
					if (smi.master.baitStorage.IsEmpty())
					{
						smi.master.CreateFetchChore();
						return;
					}
					if (smi.master.operational.IsOperational)
					{
						smi.GoTo(this.working);
					}
				}
			}).EventTransition(GameHashes.OnStorageChange, this.working, (CreatureLure.StatesInstance smi) => !smi.master.baitStorage.IsEmpty() && smi.master.activeBaitSetting != Tag.Invalid && smi.master.operational.IsOperational)
				.EventTransition(GameHashes.OperationalChanged, this.working, (CreatureLure.StatesInstance smi) => !smi.master.baitStorage.IsEmpty() && smi.master.activeBaitSetting != Tag.Invalid && smi.master.operational.IsOperational);
			this.working.Enter(delegate(CreatureLure.StatesInstance smi)
			{
				smi.master.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.AwaitingBaitDelivery, false);
				HashedString batchTag = ElementLoader.FindElementByName(smi.master.activeBaitSetting.ToString()).substance.anim.batchTag;
				KAnim.Build build = ElementLoader.FindElementByName(smi.master.activeBaitSetting.ToString()).substance.anim.GetData().build;
				KAnim.Build.Symbol symbol = build.GetSymbol(new KAnimHashedString(build.name));
				HashedString hashedString = "slime_mold";
				SymbolOverrideController component = smi.GetComponent<SymbolOverrideController>();
				component.TryRemoveSymbolOverride(hashedString, 0);
				component.AddSymbolOverride(hashedString, symbol, 0);
				smi.GetSMI<Lure.Instance>().SetActiveLures(new Tag[] { smi.master.activeBaitSetting });
			}).Exit(new StateMachine<CreatureLure.States, CreatureLure.StatesInstance, CreatureLure, object>.State.Callback(CreatureLure.States.ClearBait)).QueueAnim("working_pre", false, null)
				.QueueAnim("working_loop", true, null)
				.EventTransition(GameHashes.OnStorageChange, this.empty, (CreatureLure.StatesInstance smi) => smi.master.baitStorage.IsEmpty() && smi.master.activeBaitSetting != Tag.Invalid)
				.EventTransition(GameHashes.OperationalChanged, this.idle, (CreatureLure.StatesInstance smi) => !smi.master.operational.IsOperational && !smi.master.baitStorage.IsEmpty());
			this.empty.QueueAnim("working_pst", false, null).QueueAnim("off", false, null).Enter(delegate(CreatureLure.StatesInstance smi)
			{
				smi.master.CreateFetchChore();
			})
				.EventTransition(GameHashes.OnStorageChange, this.working, (CreatureLure.StatesInstance smi) => !smi.master.baitStorage.IsEmpty() && smi.master.operational.IsOperational)
				.EventTransition(GameHashes.OperationalChanged, this.working, (CreatureLure.StatesInstance smi) => !smi.master.baitStorage.IsEmpty() && smi.master.operational.IsOperational);
		}

		// Token: 0x06008F0E RID: 36622 RVA: 0x0030FA01 File Offset: 0x0030DC01
		private static void ClearBait(StateMachine.Instance smi)
		{
			if (smi.GetSMI<Lure.Instance>() != null)
			{
				smi.GetSMI<Lure.Instance>().SetActiveLures(null);
			}
		}

		// Token: 0x04007321 RID: 29473
		public GameStateMachine<CreatureLure.States, CreatureLure.StatesInstance, CreatureLure, object>.State idle;

		// Token: 0x04007322 RID: 29474
		public GameStateMachine<CreatureLure.States, CreatureLure.StatesInstance, CreatureLure, object>.State working;

		// Token: 0x04007323 RID: 29475
		public GameStateMachine<CreatureLure.States, CreatureLure.StatesInstance, CreatureLure, object>.State empty;
	}
}
