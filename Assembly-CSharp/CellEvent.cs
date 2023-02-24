using System;

// Token: 0x0200075F RID: 1887
public class CellEvent : EventBase
{
	// Token: 0x060033EE RID: 13294 RVA: 0x0011744A File Offset: 0x0011564A
	public CellEvent(string id, string reason, bool is_send, bool enable_logging = true)
		: base(id)
	{
		this.reason = reason;
		this.isSend = is_send;
		this.enableLogging = enable_logging;
	}

	// Token: 0x060033EF RID: 13295 RVA: 0x00117469 File Offset: 0x00115669
	public string GetMessagePrefix()
	{
		if (this.isSend)
		{
			return ">>>: ";
		}
		return "<<<: ";
	}

	// Token: 0x04001FD7 RID: 8151
	public string reason;

	// Token: 0x04001FD8 RID: 8152
	public bool isSend;

	// Token: 0x04001FD9 RID: 8153
	public bool enableLogging;
}
