using System;

// Token: 0x02000516 RID: 1302
public class DevToolAnimFile : DevTool
{
	// Token: 0x06001F55 RID: 8021 RVA: 0x000A6750 File Offset: 0x000A4950
	public DevToolAnimFile(KAnimFile animFile)
	{
		this.animFile = animFile;
		this.Name = "Anim File: \"" + animFile.name + "\"";
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x000A677C File Offset: 0x000A497C
	protected override void RenderTo(DevPanel panel)
	{
		ImGuiEx.DrawObject(this.animFile, null);
		ImGuiEx.DrawObject(this.animFile.GetData(), null);
	}

	// Token: 0x040011C0 RID: 4544
	private KAnimFile animFile;
}
