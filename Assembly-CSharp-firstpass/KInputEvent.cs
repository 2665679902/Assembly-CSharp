using System;

// Token: 0x02000030 RID: 48
public class KInputEvent
{
	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000231 RID: 561 RVA: 0x0000C744 File Offset: 0x0000A944
	// (set) Token: 0x06000232 RID: 562 RVA: 0x0000C74C File Offset: 0x0000A94C
	public KInputController Controller { get; private set; }

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000233 RID: 563 RVA: 0x0000C755 File Offset: 0x0000A955
	// (set) Token: 0x06000234 RID: 564 RVA: 0x0000C75D File Offset: 0x0000A95D
	public InputEventType Type { get; private set; }

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000235 RID: 565 RVA: 0x0000C766 File Offset: 0x0000A966
	// (set) Token: 0x06000236 RID: 566 RVA: 0x0000C76E File Offset: 0x0000A96E
	public bool Consumed { get; set; }

	// Token: 0x06000237 RID: 567 RVA: 0x0000C777 File Offset: 0x0000A977
	public KInputEvent(KInputController controller, InputEventType event_type)
	{
		this.Controller = controller;
		this.Type = event_type;
		this.Consumed = false;
	}
}
