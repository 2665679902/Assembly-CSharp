using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000175 RID: 373
	// (Invoke) Token: 0x06000C8B RID: 3211
	public delegate TComponent WrapperFactory<TComponentBase, TComponent>(TComponentBase wrapped) where TComponent : TComponentBase;
}
