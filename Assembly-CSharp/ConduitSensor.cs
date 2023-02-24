using System;

// Token: 0x02000599 RID: 1433
public abstract class ConduitSensor : Switch
{
	// Token: 0x06002328 RID: 9000
	protected abstract void ConduitUpdate(float dt);

	// Token: 0x06002329 RID: 9001 RVA: 0x000BE84C File Offset: 0x000BCA4C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			Conduit.GetFlowManager(this.conduitType).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
			return;
		}
		SolidConduit.GetFlowManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x000BE8E0 File Offset: 0x000BCAE0
	protected override void OnCleanUp()
	{
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			Conduit.GetFlowManager(this.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		}
		else
		{
			SolidConduit.GetFlowManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		}
		base.OnCleanUp();
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x000BE93B File Offset: 0x000BCB3B
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x0600232C RID: 9004 RVA: 0x000BE94A File Offset: 0x000BCB4A
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x000BE968 File Offset: 0x000BCB68
	protected virtual void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				this.animController.Play(ConduitSensor.ON_ANIMS, KAnim.PlayMode.Loop);
				return;
			}
			this.animController.Play(ConduitSensor.OFF_ANIMS, KAnim.PlayMode.Once);
		}
	}

	// Token: 0x0400143D RID: 5181
	public ConduitType conduitType;

	// Token: 0x0400143E RID: 5182
	protected bool wasOn;

	// Token: 0x0400143F RID: 5183
	protected KBatchedAnimController animController;

	// Token: 0x04001440 RID: 5184
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on" };

	// Token: 0x04001441 RID: 5185
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "on_pst", "off" };
}
