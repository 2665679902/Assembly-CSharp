using System;

// Token: 0x02000524 RID: 1316
public class DevToolObjectViewer<T> : DevTool
{
	// Token: 0x06001FA0 RID: 8096 RVA: 0x000AA21B File Offset: 0x000A841B
	public DevToolObjectViewer(Func<T> getValue)
	{
		this.getValue = getValue;
		this.Name = typeof(T).Name;
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x000AA240 File Offset: 0x000A8440
	protected override void RenderTo(DevPanel panel)
	{
		T t = this.getValue();
		this.Name = t.GetType().Name;
		ImGuiEx.DrawObject(t, null);
	}

	// Token: 0x0400120E RID: 4622
	private Func<T> getValue;
}
