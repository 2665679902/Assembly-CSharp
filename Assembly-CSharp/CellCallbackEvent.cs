using System;
using System.Diagnostics;

// Token: 0x0200075C RID: 1884
public class CellCallbackEvent : CellEvent
{
	// Token: 0x060033E5 RID: 13285 RVA: 0x001172F4 File Offset: 0x001154F4
	public CellCallbackEvent(string id, bool is_send, bool enable_logging = true)
		: base(id, "Callback", is_send, enable_logging)
	{
	}

	// Token: 0x060033E6 RID: 13286 RVA: 0x00117304 File Offset: 0x00115504
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, callback_id, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x060033E7 RID: 13287 RVA: 0x00117330 File Offset: 0x00115530
	public override string GetDescription(EventInstanceBase ev)
	{
		CellEventInstance cellEventInstance = ev as CellEventInstance;
		return base.GetMessagePrefix() + "Callback=" + cellEventInstance.data.ToString();
	}
}
