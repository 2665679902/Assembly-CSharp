using System;
using System.Diagnostics;

// Token: 0x02000763 RID: 1891
public class CellSolidEvent : CellEvent
{
	// Token: 0x060033F9 RID: 13305 RVA: 0x00117DA0 File Offset: 0x00115FA0
	public CellSolidEvent(string id, string reason, bool is_send, bool enable_logging = true)
		: base(id, reason, is_send, enable_logging)
	{
	}

	// Token: 0x060033FA RID: 13306 RVA: 0x00117DB0 File Offset: 0x00115FB0
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, bool solid)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, solid ? 1 : 0, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x060033FB RID: 13307 RVA: 0x00117DE4 File Offset: 0x00115FE4
	public override string GetDescription(EventInstanceBase ev)
	{
		if ((ev as CellEventInstance).data == 1)
		{
			return base.GetMessagePrefix() + "Solid=true (" + this.reason + ")";
		}
		return base.GetMessagePrefix() + "Solid=false (" + this.reason + ")";
	}
}
