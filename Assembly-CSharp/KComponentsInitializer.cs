using System;

// Token: 0x020007EB RID: 2027
public class KComponentsInitializer : KComponentSpawn
{
	// Token: 0x06003A65 RID: 14949 RVA: 0x00143597 File Offset: 0x00141797
	private void Awake()
	{
		KComponentSpawn.instance = this;
		this.comps = new GameComps();
	}
}
