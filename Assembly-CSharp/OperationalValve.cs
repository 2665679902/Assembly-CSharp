using System;
using KSerialization;

// Token: 0x0200061B RID: 1563
[SerializationConfig(MemberSerialization.OptIn)]
public class OperationalValve : ValveBase
{
	// Token: 0x060028F5 RID: 10485 RVA: 0x000D8B03 File Offset: 0x000D6D03
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OperationalValve>(-592767678, OperationalValve.OnOperationalChangedDelegate);
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x000D8B1C File Offset: 0x000D6D1C
	protected override void OnSpawn()
	{
		this.OnOperationalChanged(this.operational.IsOperational);
		base.OnSpawn();
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x000D8B3A File Offset: 0x000D6D3A
	protected override void OnCleanUp()
	{
		base.Unsubscribe<OperationalValve>(-592767678, OperationalValve.OnOperationalChangedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x000D8B54 File Offset: 0x000D6D54
	private void OnOperationalChanged(object data)
	{
		bool flag = (bool)data;
		if (flag)
		{
			base.CurrentFlow = base.MaxFlow;
		}
		else
		{
			base.CurrentFlow = 0f;
		}
		this.operational.SetActive(flag, false);
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x000D8B91 File Offset: 0x000D6D91
	protected override void OnMassTransfer(float amount)
	{
		this.isDispensing = amount > 0f;
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x000D8BA4 File Offset: 0x000D6DA4
	public override void UpdateAnim()
	{
		if (!this.operational.IsOperational)
		{
			this.controller.Queue("off", KAnim.PlayMode.Once, 1f, 0f);
			return;
		}
		if (this.isDispensing)
		{
			this.controller.Queue("on_flow", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		this.controller.Queue("on", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001815 RID: 6165
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001816 RID: 6166
	private bool isDispensing;

	// Token: 0x04001817 RID: 6167
	private static readonly EventSystem.IntraObjectHandler<OperationalValve> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<OperationalValve>(delegate(OperationalValve component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
