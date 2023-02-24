using System;
using ImGuiNET;

// Token: 0x02000514 RID: 1300
public abstract class DevTool
{
	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06001F47 RID: 8007 RVA: 0x000A62CC File Offset: 0x000A44CC
	// (remove) Token: 0x06001F48 RID: 8008 RVA: 0x000A6304 File Offset: 0x000A4504
	public event System.Action OnInit;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06001F49 RID: 8009 RVA: 0x000A633C File Offset: 0x000A453C
	// (remove) Token: 0x06001F4A RID: 8010 RVA: 0x000A6374 File Offset: 0x000A4574
	public event System.Action OnUninit;

	// Token: 0x06001F4B RID: 8011 RVA: 0x000A63A9 File Offset: 0x000A45A9
	public DevTool()
	{
		this.Name = DevToolUtil.GenerateDevToolName(this);
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x000A63BD File Offset: 0x000A45BD
	public void DoImGui(DevPanel panel)
	{
		if (this.RequiresGameRunning && Game.Instance == null)
		{
			ImGui.Text("Game must be loaded to use this devtool.");
			return;
		}
		this.RenderTo(panel);
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x000A63E6 File Offset: 0x000A45E6
	public void ClosePanel()
	{
		this.isRequestingToClosePanel = true;
	}

	// Token: 0x06001F4E RID: 8014
	protected abstract void RenderTo(DevPanel panel);

	// Token: 0x06001F4F RID: 8015 RVA: 0x000A63EF File Offset: 0x000A45EF
	public void Internal_TryInit()
	{
		if (this.didInit)
		{
			return;
		}
		this.didInit = true;
		if (this.OnInit != null)
		{
			this.OnInit();
		}
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x000A6414 File Offset: 0x000A4614
	public void Internal_Uninit()
	{
		if (this.OnUninit != null)
		{
			this.OnUninit();
		}
	}

	// Token: 0x040011B9 RID: 4537
	public string Name;

	// Token: 0x040011BA RID: 4538
	public bool RequiresGameRunning;

	// Token: 0x040011BB RID: 4539
	public bool isRequestingToClosePanel;

	// Token: 0x040011BC RID: 4540
	public ImGuiWindowFlags drawFlags;

	// Token: 0x040011BF RID: 4543
	private bool didInit;
}
