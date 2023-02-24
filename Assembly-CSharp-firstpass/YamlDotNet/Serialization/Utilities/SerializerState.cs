using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization.Utilities
{
	// Token: 0x020001AD RID: 429
	public sealed class SerializerState : IDisposable
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0003902C File Offset: 0x0003722C
		public T Get<T>() where T : class, new()
		{
			object obj;
			if (!this.items.TryGetValue(typeof(T), out obj))
			{
				obj = new T();
				this.items.Add(typeof(T), obj);
			}
			return (T)((object)obj);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003907C File Offset: 0x0003727C
		public void OnDeserialization()
		{
			foreach (IPostDeserializationCallback postDeserializationCallback in this.items.Values.OfType<IPostDeserializationCallback>())
			{
				postDeserializationCallback.OnDeserialization();
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000390D0 File Offset: 0x000372D0
		public void Dispose()
		{
			foreach (IDisposable disposable in this.items.Values.OfType<IDisposable>())
			{
				disposable.Dispose();
			}
		}

		// Token: 0x0400081A RID: 2074
		private readonly IDictionary<Type, object> items = new Dictionary<Type, object>();
	}
}
