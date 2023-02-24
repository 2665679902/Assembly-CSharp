using System;
using System.Diagnostics;

// Token: 0x02000762 RID: 1890
public class CellModifyMassEvent : CellEvent
{
	// Token: 0x060033F6 RID: 13302 RVA: 0x00117CDF File Offset: 0x00115EDF
	public CellModifyMassEvent(string id, string reason, bool enable_logging = false)
		: base(id, reason, true, enable_logging)
	{
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x00117CEC File Offset: 0x00115EEC
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, SimHashes element, float amount)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, (int)element, (int)(amount * 1000f), this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x060033F8 RID: 13304 RVA: 0x00117D20 File Offset: 0x00115F20
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
