using System;

// Token: 0x020003FD RID: 1021
public abstract class StateEvent
{
	// Token: 0x06001510 RID: 5392 RVA: 0x0006E075 File Offset: 0x0006C275
	public StateEvent(string name)
	{
		this.name = name;
		this.debugName = "(Event)" + name;
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x0006E095 File Offset: 0x0006C295
	public virtual StateEvent.Context Subscribe(StateMachine.Instance smi)
	{
		return new StateEvent.Context(this);
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x0006E09D File Offset: 0x0006C29D
	public virtual void Unsubscribe(StateMachine.Instance smi, StateEvent.Context context)
	{
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x0006E09F File Offset: 0x0006C29F
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x0006E0A7 File Offset: 0x0006C2A7
	public string GetDebugName()
	{
		return this.debugName;
	}

	// Token: 0x04000BD0 RID: 3024
	protected string name;

	// Token: 0x04000BD1 RID: 3025
	private string debugName;

	// Token: 0x02001013 RID: 4115
	public struct Context
	{
		// Token: 0x060071F5 RID: 29173 RVA: 0x002AC30D File Offset: 0x002AA50D
		public Context(StateEvent state_event)
		{
			this.stateEvent = state_event;
			this.data = 0;
		}

		// Token: 0x04005654 RID: 22100
		public StateEvent stateEvent;

		// Token: 0x04005655 RID: 22101
		public int data;
	}
}
