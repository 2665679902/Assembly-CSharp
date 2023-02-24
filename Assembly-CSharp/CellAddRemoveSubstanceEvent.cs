using System;
using System.Diagnostics;

// Token: 0x0200075B RID: 1883
public class CellAddRemoveSubstanceEvent : CellEvent
{
	// Token: 0x060033E2 RID: 13282 RVA: 0x00117231 File Offset: 0x00115431
	public CellAddRemoveSubstanceEvent(string id, string reason, bool enable_logging = false)
		: base(id, reason, true, enable_logging)
	{
	}

	// Token: 0x060033E3 RID: 13283 RVA: 0x00117240 File Offset: 0x00115440
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, SimHashes element, float amount, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, (int)element, (int)(amount * 1000f), this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x060033E4 RID: 13284 RVA: 0x00117274 File Offset: 0x00115474
	public override string GetDescription(EventInstanceBase ev)
	{
		CellEventInstance cellEventInstance = ev as CellEventInstance;
		SimHashes data = (SimHashes)cellEventInstance.data;
		return string.Concat(new string[]
		{
			base.GetMessagePrefix(),
			"Element=",
			data.ToString(),
			", Mass=",
			((float)cellEventInstance.data2 / 1000f).ToString(),
			" (",
			this.reason,
			")"
		});
	}
}
