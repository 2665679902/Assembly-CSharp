using System;
using System.Reflection;

namespace YamlDotNet
{
	// Token: 0x02000173 RID: 371
	internal static class PropertyInfoExtensions
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x000368B2 File Offset: 0x00034AB2
		public static object ReadValue(this PropertyInfo property, object target)
		{
			return property.GetGetMethod().Invoke(target, null);
		}
	}
}
