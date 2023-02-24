using System;
using System.Collections.Generic;

namespace YamlDotNet.Serialization.Utilities
{
	// Token: 0x020001AC RID: 428
	internal static class ReflectionUtility
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00038FB8 File Offset: 0x000371B8
		public static Type GetImplementedGenericInterface(Type type, Type genericInterfaceType)
		{
			foreach (Type type2 in ReflectionUtility.GetImplementedInterfaces(type))
			{
				if (type2.IsGenericType() && type2.GetGenericTypeDefinition() == genericInterfaceType)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003901C File Offset: 0x0003721C
		public static IEnumerable<Type> GetImplementedInterfaces(Type type)
		{
			if (type.IsInterface())
			{
				yield return type;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				yield return type2;
			}
			Type[] array = null;
			yield break;
		}
	}
}
