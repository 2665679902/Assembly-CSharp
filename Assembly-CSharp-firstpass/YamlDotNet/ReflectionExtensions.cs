using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace YamlDotNet
{
	// Token: 0x02000171 RID: 369
	internal static class ReflectionExtensions
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00036757 File Offset: 0x00034957
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0003675F File Offset: 0x0003495F
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00036767 File Offset: 0x00034967
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003676F File Offset: 0x0003496F
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00036777 File Offset: 0x00034977
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003677F File Offset: 0x0003497F
		public static bool HasDefaultConstructor(this Type type)
		{
			return type.IsValueType || type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null) != null;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000367A0 File Offset: 0x000349A0
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000367A8 File Offset: 0x000349A8
		public static PropertyInfo GetPublicProperty(this Type type, string name)
		{
			return type.GetProperty(name);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000367B4 File Offset: 0x000349B4
		public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
		{
			BindingFlags instancePublic = BindingFlags.Instance | BindingFlags.Public;
			if (!type.IsInterface)
			{
				return type.GetProperties(instancePublic);
			}
			return new Type[] { type }.Concat(type.GetInterfaces()).SelectMany((Type i) => i.GetProperties(instancePublic));
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003680C File Offset: 0x00034A0C
		public static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00036816 File Offset: 0x00034A16
		public static MethodInfo GetPublicStaticMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetMethod(name, BindingFlags.Static | BindingFlags.Public, null, parameterTypes, null);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00036824 File Offset: 0x00034A24
		public static MethodInfo GetPublicInstanceMethod(this Type type, string name)
		{
			return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003682F File Offset: 0x00034A2F
		public static Exception Unwrap(this TargetInvocationException ex)
		{
			Exception innerException = ex.InnerException;
			if (ReflectionExtensions.remoteStackTraceField != null)
			{
				ReflectionExtensions.remoteStackTraceField.SetValue(ex.InnerException, ex.InnerException.StackTrace + "\r\n");
			}
			return innerException;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00036869 File Offset: 0x00034A69
		public static bool IsInstanceOf(this Type type, object o)
		{
			return type.IsInstanceOfType(o);
		}

		// Token: 0x040007D3 RID: 2003
		private static readonly FieldInfo remoteStackTraceField = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
