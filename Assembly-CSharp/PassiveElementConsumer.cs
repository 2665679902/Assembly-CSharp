using System;

// Token: 0x02000887 RID: 2183
public class PassiveElementConsumer : ElementConsumer, IGameObjectEffectDescriptor
{
	// Token: 0x06003EA4 RID: 16036 RVA: 0x0015E3B6 File Offset: 0x0015C5B6
	protected override bool IsActive()
	{
		return true;
	}
}
