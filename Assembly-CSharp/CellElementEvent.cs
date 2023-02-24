using System;
using System.Diagnostics;

// Token: 0x0200075E RID: 1886
public class CellElementEvent : CellEvent
{
	// Token: 0x060033EB RID: 13291 RVA: 0x001173B2 File Offset: 0x001155B2
	public CellElementEvent(string id, string reason, bool is_send, bool enable_logging = true)
		: base(id, reason, is_send, enable_logging)
	{
	}

	// Token: 0x060033EC RID: 13292 RVA: 0x001173C0 File Offset: 0x001155C0
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, SimHashes element, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, (int)element, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x060033ED RID: 13293 RVA: 0x001173EC File Offset: 0x001155EC
	public override string GetDescription(EventInstanceBase ev)
	{
		SimHashes data = (SimHashes)(ev as CellEventInstance).data;
		return string.Concat(new string[]
		{
			base.GetMessagePrefix(),
			"Element=",
			data.ToString(),
			" (",
			this.reason,
			")"
		});
	}
}
