using System;
using UnityEngine;

// Token: 0x0200062A RID: 1578
[AddComponentMenu("KMonoBehaviour/scripts/Pump")]
public class Pump : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06002971 RID: 10609 RVA: 0x000DAB77 File Offset: 0x000D8D77
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.consumer.EnableConsumption(false);
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x000DAB8B File Offset: 0x000D8D8B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.elapsedTime = 0f;
		this.pumpable = this.UpdateOperational();
		this.dispenser.GetConduitManager().AddConduitUpdater(new Action<float>(this.OnConduitUpdate), ConduitFlowPriority.LastPostUpdate);
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x000DABC8 File Offset: 0x000D8DC8
	protected override void OnCleanUp()
	{
		this.dispenser.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.OnConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x000DABEC File Offset: 0x000D8DEC
	public void Sim1000ms(float dt)
	{
		this.elapsedTime += dt;
		if (this.elapsedTime >= 1f)
		{
			this.pumpable = this.UpdateOperational();
			this.elapsedTime = 0f;
		}
		if (this.operational.IsOperational && this.pumpable)
		{
			this.operational.SetActive(true, false);
			return;
		}
		this.operational.SetActive(false, false);
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x000DAC5C File Offset: 0x000D8E5C
	private bool UpdateOperational()
	{
		Element.State state = Element.State.Vacuum;
		ConduitType conduitType = this.dispenser.conduitType;
		if (conduitType != ConduitType.Gas)
		{
			if (conduitType == ConduitType.Liquid)
			{
				state = Element.State.Liquid;
			}
		}
		else
		{
			state = Element.State.Gas;
		}
		bool flag = this.IsPumpable(state, (int)this.consumer.consumptionRadius);
		StatusItem statusItem = ((state == Element.State.Gas) ? Db.Get().BuildingStatusItems.NoGasElementToPump : Db.Get().BuildingStatusItems.NoLiquidElementToPump);
		this.noElementStatusGuid = this.selectable.ToggleStatusItem(statusItem, this.noElementStatusGuid, !flag, null);
		this.operational.SetFlag(Pump.PumpableFlag, !this.storage.IsFull() && flag);
		return flag;
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x000DAD00 File Offset: 0x000D8F00
	private bool IsPumpable(Element.State expected_state, int radius)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		for (int i = 0; i < (int)this.consumer.consumptionRadius; i++)
		{
			for (int j = 0; j < (int)this.consumer.consumptionRadius; j++)
			{
				int num2 = num + j + Grid.WidthInCells * i;
				if (Grid.Element[num2].IsState(expected_state))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x000DAD68 File Offset: 0x000D8F68
	private void OnConduitUpdate(float dt)
	{
		this.conduitBlockedStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.ConduitBlocked, this.conduitBlockedStatusGuid, this.dispenser.blocked, null);
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06002978 RID: 10616 RVA: 0x000DAD9C File Offset: 0x000D8F9C
	public ConduitType conduitType
	{
		get
		{
			return this.dispenser.conduitType;
		}
	}

	// Token: 0x04001866 RID: 6246
	public static readonly Operational.Flag PumpableFlag = new Operational.Flag("vent", Operational.Flag.Type.Requirement);

	// Token: 0x04001867 RID: 6247
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001868 RID: 6248
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x04001869 RID: 6249
	[MyCmpGet]
	private ElementConsumer consumer;

	// Token: 0x0400186A RID: 6250
	[MyCmpGet]
	private ConduitDispenser dispenser;

	// Token: 0x0400186B RID: 6251
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400186C RID: 6252
	private const float OperationalUpdateInterval = 1f;

	// Token: 0x0400186D RID: 6253
	private float elapsedTime;

	// Token: 0x0400186E RID: 6254
	private bool pumpable;

	// Token: 0x0400186F RID: 6255
	private Guid conduitBlockedStatusGuid;

	// Token: 0x04001870 RID: 6256
	private Guid noElementStatusGuid;
}
