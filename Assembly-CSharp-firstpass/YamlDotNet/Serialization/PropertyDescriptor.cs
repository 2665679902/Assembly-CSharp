using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200019C RID: 412
	public sealed class PropertyDescriptor : IPropertyDescriptor
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00037BF4 File Offset: 0x00035DF4
		public PropertyDescriptor(IPropertyDescriptor baseDescriptor)
		{
			this.baseDescriptor = baseDescriptor;
			this.Name = baseDescriptor.Name;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00037C0F File Offset: 0x00035E0F
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00037C17 File Offset: 0x00035E17
		public string Name { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00037C20 File Offset: 0x00035E20
		public Type Type
		{
			get
			{
				return this.baseDescriptor.Type;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00037C2D File Offset: 0x00035E2D
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00037C3A File Offset: 0x00035E3A
		public Type TypeOverride
		{
			get
			{
				return this.baseDescriptor.TypeOverride;
			}
			set
			{
				this.baseDescriptor.TypeOverride = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00037C48 File Offset: 0x00035E48
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00037C50 File Offset: 0x00035E50
		public int Order { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00037C59 File Offset: 0x00035E59
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x00037C66 File Offset: 0x00035E66
		public ScalarStyle ScalarStyle
		{
			get
			{
				return this.baseDescriptor.ScalarStyle;
			}
			set
			{
				this.baseDescriptor.ScalarStyle = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00037C74 File Offset: 0x00035E74
		public bool CanWrite
		{
			get
			{
				return this.baseDescriptor.CanWrite;
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00037C81 File Offset: 0x00035E81
		public void Write(object target, object value)
		{
			this.baseDescriptor.Write(target, value);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00037C90 File Offset: 0x00035E90
		public T GetCustomAttribute<T>() where T : Attribute
		{
			return this.baseDescriptor.GetCustomAttribute<T>();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00037C9D File Offset: 0x00035E9D
		public IObjectDescriptor Read(object target)
		{
			return this.baseDescriptor.Read(target);
		}

		// Token: 0x040007F8 RID: 2040
		private readonly IPropertyDescriptor baseDescriptor;
	}
}
