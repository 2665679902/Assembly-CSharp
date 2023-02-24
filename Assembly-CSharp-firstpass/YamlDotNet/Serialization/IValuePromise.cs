using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000191 RID: 401
	public interface IValuePromise
	{
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000D17 RID: 3351
		// (remove) Token: 0x06000D18 RID: 3352
		event Action<object> ValueAvailable;
	}
}
