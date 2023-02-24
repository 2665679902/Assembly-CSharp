using System;

// Token: 0x0200064E RID: 1614
public class StorageLockerSmart : StorageLocker
{
	// Token: 0x06002AF9 RID: 11001 RVA: 0x000E2B6D File Offset: 0x000E0D6D
	protected override void OnPrefabInit()
	{
		base.Initialize(true);
	}

	// Token: 0x06002AFA RID: 11002 RVA: 0x000E2B78 File Offset: 0x000E0D78
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ports = base.gameObject.GetComponent<LogicPorts>();
		base.Subscribe<StorageLockerSmart>(-1697596308, StorageLockerSmart.UpdateLogicCircuitCBDelegate);
		base.Subscribe<StorageLockerSmart>(-592767678, StorageLockerSmart.UpdateLogicCircuitCBDelegate);
		this.UpdateLogicAndActiveState();
	}

	// Token: 0x06002AFB RID: 11003 RVA: 0x000E2BC4 File Offset: 0x000E0DC4
	private void UpdateLogicCircuitCB(object data)
	{
		this.UpdateLogicAndActiveState();
	}

	// Token: 0x06002AFC RID: 11004 RVA: 0x000E2BCC File Offset: 0x000E0DCC
	private void UpdateLogicAndActiveState()
	{
		bool flag = this.filteredStorage.IsFull();
		bool isOperational = this.operational.IsOperational;
		bool flag2 = flag && isOperational;
		this.ports.SendSignal(FilteredStorage.FULL_PORT_ID, flag2 ? 1 : 0);
		this.filteredStorage.SetLogicMeter(flag2);
		this.operational.SetActive(isOperational, false);
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000E2C23 File Offset: 0x000E0E23
	// (set) Token: 0x06002AFE RID: 11006 RVA: 0x000E2C2B File Offset: 0x000E0E2B
	public override float UserMaxCapacity
	{
		get
		{
			return base.UserMaxCapacity;
		}
		set
		{
			base.UserMaxCapacity = value;
			this.UpdateLogicAndActiveState();
		}
	}

	// Token: 0x04001988 RID: 6536
	[MyCmpGet]
	private LogicPorts ports;

	// Token: 0x04001989 RID: 6537
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400198A RID: 6538
	private static readonly EventSystem.IntraObjectHandler<StorageLockerSmart> UpdateLogicCircuitCBDelegate = new EventSystem.IntraObjectHandler<StorageLockerSmart>(delegate(StorageLockerSmart component, object data)
	{
		component.UpdateLogicCircuitCB(data);
	});
}
