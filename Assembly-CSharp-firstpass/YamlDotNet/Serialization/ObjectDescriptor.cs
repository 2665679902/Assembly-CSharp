using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200019B RID: 411
	public sealed class ObjectDescriptor : IObjectDescriptor
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00037B49 File Offset: 0x00035D49
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00037B51 File Offset: 0x00035D51
		public object Value { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00037B5A File Offset: 0x00035D5A
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x00037B62 File Offset: 0x00035D62
		public Type Type { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00037B6B File Offset: 0x00035D6B
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x00037B73 File Offset: 0x00035D73
		public Type StaticType { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00037B7C File Offset: 0x00035D7C
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00037B84 File Offset: 0x00035D84
		public ScalarStyle ScalarStyle { get; private set; }

		// Token: 0x06000D43 RID: 3395 RVA: 0x00037B8D File Offset: 0x00035D8D
		public ObjectDescriptor(object value, Type type, Type staticType)
			: this(value, type, staticType, ScalarStyle.Any)
		{
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00037B9C File Offset: 0x00035D9C
		public ObjectDescriptor(object value, Type type, Type staticType, ScalarStyle scalarStyle)
		{
			this.Value = value;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.Type = type;
			if (staticType == null)
			{
				throw new ArgumentNullException("staticType");
			}
			this.StaticType = staticType;
			this.ScalarStyle = scalarStyle;
		}
	}
}
