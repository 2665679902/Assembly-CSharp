using System;
using System.Collections.Generic;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200018E RID: 398
	public interface ITypeInspector
	{
		// Token: 0x06000D13 RID: 3347
		IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container);

		// Token: 0x06000D14 RID: 3348
		IPropertyDescriptor GetProperty(Type type, object container, string name, bool ignoreUnmatched);
	}
}
