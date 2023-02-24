using System;
using KSerialization;

// Token: 0x02000766 RID: 1894
[SerializationConfig(MemberSerialization.OptIn)]
public class EventInstanceBase : ISaveLoadable
{
	// Token: 0x06003401 RID: 13313 RVA: 0x00117EC8 File Offset: 0x001160C8
	public EventInstanceBase(EventBase ev)
	{
		this.frame = GameClock.Instance.GetFrame();
		this.eventHash = ev.hash;
		this.ev = ev;
	}

	// Token: 0x06003402 RID: 13314 RVA: 0x00117EF4 File Offset: 0x001160F4
	public override string ToString()
	{
		string text = "[" + this.frame.ToString() + "] ";
		if (this.ev != null)
		{
			return text + this.ev.GetDescription(this);
		}
		return text + "Unknown event";
	}

	// Token: 0x0400201E RID: 8222
	[Serialize]
	public int frame;

	// Token: 0x0400201F RID: 8223
	[Serialize]
	public int eventHash;

	// Token: 0x04002020 RID: 8224
	public EventBase ev;
}
