using System;

// Token: 0x02000522 RID: 1314
public class DevToolMenuNodeAction : IMenuNode
{
	// Token: 0x06001F97 RID: 8087 RVA: 0x000A9E10 File Offset: 0x000A8010
	public DevToolMenuNodeAction(string name, System.Action onClickFn)
	{
		this.name = name;
		this.onClickFn = onClickFn;
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x000A9E26 File Offset: 0x000A8026
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x000A9E2E File Offset: 0x000A802E
	public void Draw()
	{
		if (ImGuiEx.MenuItem(this.name, this.isEnabledFn == null || this.isEnabledFn()))
		{
			this.onClickFn();
		}
	}

	// Token: 0x04001203 RID: 4611
	public string name;

	// Token: 0x04001204 RID: 4612
	public System.Action onClickFn;

	// Token: 0x04001205 RID: 4613
	public Func<bool> isEnabledFn;
}
