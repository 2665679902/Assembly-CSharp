using System;

// Token: 0x02000BA7 RID: 2983
public interface IConfigurableConsumer
{
	// Token: 0x06005DDF RID: 24031
	IConfigurableConsumerOption[] GetSettingOptions();

	// Token: 0x06005DE0 RID: 24032
	IConfigurableConsumerOption GetSelectedOption();

	// Token: 0x06005DE1 RID: 24033
	void SetSelectedOption(IConfigurableConsumerOption option);
}
