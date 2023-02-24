using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000176 RID: 374
	// (Invoke) Token: 0x06000C8F RID: 3215
	public delegate TComponent WrapperFactory<TArgument, TComponentBase, TComponent>(TComponentBase wrapped, TArgument argument) where TComponent : TComponentBase;
}
