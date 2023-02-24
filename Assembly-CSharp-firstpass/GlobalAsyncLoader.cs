using System;

// Token: 0x0200007D RID: 125
public abstract class GlobalAsyncLoader<LoaderType> : AsyncLoadManager<IGlobalAsyncLoader>.AsyncLoader<LoaderType>, IGlobalAsyncLoader where LoaderType : class
{
}
