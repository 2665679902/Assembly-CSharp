using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020005E0 RID: 1504
public class LogicBroadcastReceiver : KMonoBehaviour, ISimEveryTick
{
	// Token: 0x060025AC RID: 9644 RVA: 0x000CBECC File Offset: 0x000CA0CC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
		this.SetChannel(this.channel.Get());
		this.operational.SetFlag(LogicBroadcastReceiver.spaceVisible, this.IsSpaceVisible());
		this.operational.SetFlag(LogicBroadcastReceiver.validChannelInRange, this.CheckChannelValid() && LogicBroadcastReceiver.CheckRange(this.channel.Get().gameObject, base.gameObject));
		this.wasOperational = !this.operational.IsOperational;
		this.OnOperationalChanged(null);
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x000CBF70 File Offset: 0x000CA170
	public void SimEveryTick(float dt)
	{
		bool flag = this.IsSpaceVisible();
		this.operational.SetFlag(LogicBroadcastReceiver.spaceVisible, flag);
		if (!flag)
		{
			if (this.spaceNotVisibleStatusItem == Guid.Empty)
			{
				this.spaceNotVisibleStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoSurfaceSight, null);
			}
		}
		else if (this.spaceNotVisibleStatusItem != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.spaceNotVisibleStatusItem, false);
			this.spaceNotVisibleStatusItem = Guid.Empty;
		}
		bool flag2 = this.CheckChannelValid() && LogicBroadcastReceiver.CheckRange(this.channel.Get().gameObject, base.gameObject);
		this.operational.SetFlag(LogicBroadcastReceiver.validChannelInRange, flag2);
		if (flag2 && !this.syncToChannelComplete)
		{
			this.SyncWithBroadcast();
		}
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x000CC046 File Offset: 0x000CA246
	public bool IsSpaceVisible()
	{
		return base.gameObject.GetMyWorld().IsModuleInterior || Grid.ExposedToSunlight[Grid.PosToCell(base.gameObject)] > 0;
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x000CC074 File Offset: 0x000CA274
	private bool CheckChannelValid()
	{
		return this.channel.Get() != null && this.channel.Get().GetComponent<LogicPorts>().inputPorts != null;
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x000CC0A3 File Offset: 0x000CA2A3
	public LogicBroadcaster GetChannel()
	{
		return this.channel.Get();
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x000CC0B0 File Offset: 0x000CA2B0
	public void SetChannel(LogicBroadcaster broadcaster)
	{
		this.ClearChannel();
		if (broadcaster == null)
		{
			return;
		}
		this.channel.Set(broadcaster);
		this.syncToChannelComplete = false;
		this.channelEventListeners.Add(this.channel.Get().gameObject.Subscribe(-801688580, new Action<object>(this.OnChannelLogicEvent)));
		this.channelEventListeners.Add(this.channel.Get().gameObject.Subscribe(-592767678, new Action<object>(this.OnChannelLogicEvent)));
		this.SyncWithBroadcast();
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x000CC148 File Offset: 0x000CA348
	private void ClearChannel()
	{
		if (this.CheckChannelValid())
		{
			for (int i = 0; i < this.channelEventListeners.Count; i++)
			{
				this.channel.Get().gameObject.Unsubscribe(this.channelEventListeners[i]);
			}
		}
		this.channelEventListeners.Clear();
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x000CC19F File Offset: 0x000CA39F
	private void OnChannelLogicEvent(object data)
	{
		if (!this.channel.Get().GetComponent<Operational>().IsOperational)
		{
			return;
		}
		this.SyncWithBroadcast();
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x000CC1C0 File Offset: 0x000CA3C0
	private void SyncWithBroadcast()
	{
		if (!this.CheckChannelValid())
		{
			return;
		}
		bool flag = LogicBroadcastReceiver.CheckRange(this.channel.Get().gameObject, base.gameObject);
		this.UpdateRangeStatus(flag);
		if (!flag)
		{
			return;
		}
		base.GetComponent<LogicPorts>().SendSignal(this.PORT_ID, this.channel.Get().GetCurrentValue());
		this.syncToChannelComplete = true;
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x000CC22A File Offset: 0x000CA42A
	public static bool CheckRange(GameObject broadcaster, GameObject receiver)
	{
		return AxialUtil.GetDistance(broadcaster.GetMyWorldLocation(), receiver.GetMyWorldLocation()) <= LogicBroadcaster.RANGE;
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x000CC248 File Offset: 0x000CA448
	private void UpdateRangeStatus(bool inRange)
	{
		if (!inRange && this.rangeStatusItem == Guid.Empty)
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.rangeStatusItem = component.AddStatusItem(Db.Get().BuildingStatusItems.BroadcasterOutOfRange, null);
			return;
		}
		if (this.rangeStatusItem != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.rangeStatusItem, false);
			this.rangeStatusItem = Guid.Empty;
		}
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x000CC2C0 File Offset: 0x000CA4C0
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			if (!this.wasOperational)
			{
				this.wasOperational = true;
				this.animController.Queue("on_pre", KAnim.PlayMode.Once, 1f, 0f);
				this.animController.Queue("on", KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		else if (this.wasOperational)
		{
			this.wasOperational = false;
			this.animController.Queue("on_pst", KAnim.PlayMode.Once, 1f, 0f);
			this.animController.Queue("off", KAnim.PlayMode.Loop, 1f, 0f);
		}
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x000CC37C File Offset: 0x000CA57C
	protected override void OnCleanUp()
	{
		this.ClearChannel();
		base.OnCleanUp();
	}

	// Token: 0x04001600 RID: 5632
	[Serialize]
	private Ref<LogicBroadcaster> channel = new Ref<LogicBroadcaster>();

	// Token: 0x04001601 RID: 5633
	public string PORT_ID = "";

	// Token: 0x04001602 RID: 5634
	private List<int> channelEventListeners = new List<int>();

	// Token: 0x04001603 RID: 5635
	private bool syncToChannelComplete;

	// Token: 0x04001604 RID: 5636
	public static readonly Operational.Flag spaceVisible = new Operational.Flag("spaceVisible", Operational.Flag.Type.Requirement);

	// Token: 0x04001605 RID: 5637
	public static readonly Operational.Flag validChannelInRange = new Operational.Flag("validChannelInRange", Operational.Flag.Type.Requirement);

	// Token: 0x04001606 RID: 5638
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001607 RID: 5639
	private bool wasOperational;

	// Token: 0x04001608 RID: 5640
	[MyCmpGet]
	private KBatchedAnimController animController;

	// Token: 0x04001609 RID: 5641
	private Guid rangeStatusItem = Guid.Empty;

	// Token: 0x0400160A RID: 5642
	private Guid spaceNotVisibleStatusItem = Guid.Empty;
}
