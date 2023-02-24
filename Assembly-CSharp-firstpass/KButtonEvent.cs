using System;

// Token: 0x02000031 RID: 49
public class KButtonEvent : KInputEvent
{
	// Token: 0x06000238 RID: 568 RVA: 0x0000C794 File Offset: 0x0000A994
	public KButtonEvent(KInputController controller, InputEventType event_type, bool[] is_action)
		: base(controller, event_type)
	{
		this.mIsAction = is_action;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
	public KButtonEvent(KInputController controller, InputEventType event_type, global::Action action)
		: base(controller, event_type)
	{
		this.mIsAction = null;
		this.mAction = action;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000C7BD File Offset: 0x0000A9BD
	public bool TryConsume(global::Action action)
	{
		if (base.Consumed)
		{
			return false;
		}
		if (action != global::Action.NumActions && this.IsAction(action))
		{
			base.Consumed = true;
		}
		return base.Consumed;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
	public bool IsAction(global::Action action)
	{
		if (this.mIsAction != null)
		{
			return this.mIsAction[(int)action];
		}
		return this.mAction == action;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000C804 File Offset: 0x0000AA04
	public global::Action GetAction()
	{
		if (this.mIsAction != null)
		{
			for (int i = 0; i < this.mIsAction.Length; i++)
			{
				if (this.mIsAction[i])
				{
					return (global::Action)i;
				}
			}
			return global::Action.NumActions;
		}
		return this.mAction;
	}

	// Token: 0x04000232 RID: 562
	private bool[] mIsAction;

	// Token: 0x04000233 RID: 563
	private global::Action mAction;
}
