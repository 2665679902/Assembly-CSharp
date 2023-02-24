using System;

// Token: 0x02000111 RID: 273
public class TuningData<TuningType>
{
	// Token: 0x06000938 RID: 2360 RVA: 0x0002497C File Offset: 0x00022B7C
	public static TuningType Get()
	{
		TuningSystem.Init();
		return TuningData<TuningType>._TuningData;
	}

	// Token: 0x04000686 RID: 1670
	public static TuningType _TuningData;
}
