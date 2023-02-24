using System;
using KSerialization;

// Token: 0x020005E1 RID: 1505
public class LogicBroadcaster : KMonoBehaviour, ISimEveryTick
{
	// Token: 0x17000209 RID: 521
	// (get) Token: 0x060025BB RID: 9659 RVA: 0x000CC3EB File Offset: 0x000CA5EB
	// (set) Token: 0x060025BC RID: 9660 RVA: 0x000CC3F3 File Offset: 0x000CA5F3
	public int BroadCastChannelID
	{
		get
		{
			return this.broadcastChannelID;
		}
		private set
		{
			this.broadcastChannelID = value;
		}
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x000CC3FC File Offset: 0x000CA5FC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.LogicBroadcasters.Add(this);
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000CC40F File Offset: 0x000CA60F
	protected override void OnCleanUp()
	{
		Components.LogicBroadcasters.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x000CC424 File Offset: 0x000CA624
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<LogicBroadcaster>(-801688580, LogicBroadcaster.OnLogicValueChangedDelegate);
		base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
		this.operational.SetFlag(LogicBroadcaster.spaceVisible, this.IsSpaceVisible());
		this.wasOperational = !this.operational.IsOperational;
		this.OnOperationalChanged(null);
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x000CC491 File Offset: 0x000CA691
	public bool IsSpaceVisible()
	{
		return base.gameObject.GetMyWorld().IsModuleInterior || Grid.ExposedToSunlight[Grid.PosToCell(base.gameObject)] > 0;
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x000CC4BF File Offset: 0x000CA6BF
	public int GetCurrentValue()
	{
		return base.GetComponent<LogicPorts>().GetInputValue(this.PORT_ID);
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x000CC4D7 File Offset: 0x000CA6D7
	private void OnLogicValueChanged(object data)
	{
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x000CC4DC File Offset: 0x000CA6DC
	public void SimEveryTick(float dt)
	{
		bool flag = this.IsSpaceVisible();
		this.operational.SetFlag(LogicBroadcaster.spaceVisible, flag);
		if (!flag)
		{
			if (this.spaceNotVisibleStatusItem == Guid.Empty)
			{
				this.spaceNotVisibleStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.NoSurfaceSight, null);
				return;
			}
		}
		else if (this.spaceNotVisibleStatusItem != Guid.Empty)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(this.spaceNotVisibleStatusItem, false);
			this.spaceNotVisibleStatusItem = Guid.Empty;
		}
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x000CC568 File Offset: 0x000CA768
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

	// Token: 0x0400160B RID: 5643
	public static int RANGE = 5;

	// Token: 0x0400160C RID: 5644
	private static int INVALID_CHANNEL_ID = -1;

	// Token: 0x0400160D RID: 5645
	public string PORT_ID = "";

	// Token: 0x0400160E RID: 5646
	private bool wasOperational;

	// Token: 0x0400160F RID: 5647
	[Serialize]
	private int broadcastChannelID = LogicBroadcaster.INVALID_CHANNEL_ID;

	// Token: 0x04001610 RID: 5648
	private static readonly EventSystem.IntraObjectHandler<LogicBroadcaster> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicBroadcaster>(delegate(LogicBroadcaster component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001611 RID: 5649
	public static readonly Operational.Flag spaceVisible = new Operational.Flag("spaceVisible", Operational.Flag.Type.Requirement);

	// Token: 0x04001612 RID: 5650
	private Guid spaceNotVisibleStatusItem = Guid.Empty;

	// Token: 0x04001613 RID: 5651
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001614 RID: 5652
	[MyCmpGet]
	private KBatchedAnimController animController;
}
