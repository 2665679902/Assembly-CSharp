using System;
using System.Diagnostics;

// Token: 0x02000764 RID: 1892
public class CellSolidFilterEvent : CellEvent
{
	// Token: 0x060033FC RID: 13308 RVA: 0x00117E36 File Offset: 0x00116036
	public CellSolidFilterEvent(string id, bool enable_logging = true)
		: base(id, "filtered", false, enable_logging)
	{
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x00117E48 File Offset: 0x00116048
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

	// Token: 0x060033FE RID: 13310 RVA: 0x00117E7C File Offset: 0x0011607C
	public override string GetDescription(EventInstanceBase ev)
	{
		CellEventInstance cellEventInstance = ev as CellEventInstance;
		return base.GetMessagePrefix() + "Filtered Solid Event solid=" + cellEventInstance.data.ToString();
	}
}
