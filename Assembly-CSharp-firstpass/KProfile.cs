using System;

// Token: 0x020000C7 RID: 199
public class KProfile : IDisposable
{
	// Token: 0x06000791 RID: 1937 RVA: 0x0001F708 File Offset: 0x0001D908
	public KProfile(string name, string group = "Game")
	{
		this.name = name;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0001F717 File Offset: 0x0001D917
	public void Dispose()
	{
	}

	// Token: 0x04000600 RID: 1536
	private string name;
}
