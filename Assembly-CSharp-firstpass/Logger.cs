using System;

// Token: 0x020000CC RID: 204
public abstract class Logger
{
	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001FFAD File Offset: 0x0001E1AD
	// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001FFB5 File Offset: 0x0001E1B5
	public bool enableConsoleLogging { get; set; }

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001FFBE File Offset: 0x0001E1BE
	// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001FFC6 File Offset: 0x0001E1C6
	public bool breakOnLog { get; set; }

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060007CC RID: 1996
	public abstract int Count { get; }

	// Token: 0x060007CD RID: 1997 RVA: 0x0001FFCF File Offset: 0x0001E1CF
	public Logger(string name)
	{
		this.name = name;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0001FFDE File Offset: 0x0001E1DE
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0001FFE6 File Offset: 0x0001E1E6
	public void SetName(string name)
	{
		this.name = name;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0001FFEF File Offset: 0x0001E1EF
	public virtual void DebugDisplayLog()
	{
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0001FFF1 File Offset: 0x0001E1F1
	public virtual void DebugDevTool()
	{
	}

	// Token: 0x04000613 RID: 1555
	public static uint NextIdx;

	// Token: 0x04000614 RID: 1556
	protected string name;
}
