using System;
using KSerialization;

// Token: 0x0200061A RID: 1562
[SerializationConfig(MemberSerialization.OptIn)]
public class OperationalControlledSwitch : CircuitSwitch
{
	// Token: 0x060028F0 RID: 10480 RVA: 0x000D8A99 File Offset: 0x000D6C99
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.manuallyControlled = false;
	}

	// Token: 0x060028F1 RID: 10481 RVA: 0x000D8AA8 File Offset: 0x000D6CA8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<OperationalControlledSwitch>(-592767678, OperationalControlledSwitch.OnOperationalChangedDelegate);
	}

	// Token: 0x060028F2 RID: 10482 RVA: 0x000D8AC4 File Offset: 0x000D6CC4
	private void OnOperationalChanged(object data)
	{
		bool flag = (bool)data;
		this.SetState(flag);
	}

	// Token: 0x04001814 RID: 6164
	private static readonly EventSystem.IntraObjectHandler<OperationalControlledSwitch> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<OperationalControlledSwitch>(delegate(OperationalControlledSwitch component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
