using System;
using KSerialization;

// Token: 0x02000402 RID: 1026
[SerializationConfig(MemberSerialization.OptIn)]
public class StateMachineComponent<StateMachineInstanceType> : StateMachineComponent, ISaveLoadable where StateMachineInstanceType : StateMachine.Instance
{
	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06001532 RID: 5426 RVA: 0x0006E57C File Offset: 0x0006C77C
	public StateMachineInstanceType smi
	{
		get
		{
			if (this._smi == null)
			{
				this._smi = (StateMachineInstanceType)((object)Activator.CreateInstance(typeof(StateMachineInstanceType), new object[] { this }));
			}
			return this._smi;
		}
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x0006E5B5 File Offset: 0x0006C7B5
	public override StateMachine.Instance GetSMI()
	{
		return this._smi;
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x0006E5C2 File Offset: 0x0006C7C2
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this._smi != null)
		{
			this._smi.StopSM("StateMachineComponent.OnCleanUp");
			this._smi = default(StateMachineInstanceType);
		}
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x0006E5F8 File Offset: 0x0006C7F8
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (base.isSpawned)
		{
			this.smi.StartSM();
		}
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x0006E618 File Offset: 0x0006C818
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this._smi != null)
		{
			this._smi.StopSM("StateMachineComponent.OnDisable");
		}
	}

	// Token: 0x04000BE0 RID: 3040
	private StateMachineInstanceType _smi;
}
