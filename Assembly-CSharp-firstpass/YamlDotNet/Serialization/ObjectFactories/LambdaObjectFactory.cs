using System;

namespace YamlDotNet.Serialization.ObjectFactories
{
	// Token: 0x020001C1 RID: 449
	public sealed class LambdaObjectFactory : IObjectFactory
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x0003A413 File Offset: 0x00038613
		public LambdaObjectFactory(Func<Type, object> factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			this._factory = factory;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003A430 File Offset: 0x00038630
		public object Create(Type type)
		{
			return this._factory(type);
		}

		// Token: 0x04000832 RID: 2098
		private readonly Func<Type, object> _factory;
	}
}
