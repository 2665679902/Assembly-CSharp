using System;
using System.Diagnostics;

// Token: 0x0200075D RID: 1885
public class CellDigEvent : CellEvent
{
	// Token: 0x060033E8 RID: 13288 RVA: 0x0011735F File Offset: 0x0011555F
	public CellDigEvent(bool enable_logging = true)
		: base("Dig", "Dig", true, enable_logging)
	{
	}

	// Token: 0x060033E9 RID: 13289 RVA: 0x00117374 File Offset: 0x00115574
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, 0, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x001173A0 File Offset: 0x001155A0
	public override string GetDescription(EventInstanceBase ev)
	{
		return base.GetMessagePrefix() + "Dig=true";
	}
}
