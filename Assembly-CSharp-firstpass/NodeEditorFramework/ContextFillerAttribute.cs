using System;
using System.Reflection;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework
{
	// Token: 0x02000486 RID: 1158
	[AttributeUsage(AttributeTargets.Method)]
	public class ContextFillerAttribute : Attribute
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x00064DA5 File Offset: 0x00062FA5
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x00064DAD File Offset: 0x00062FAD
		public ContextType contextType { get; private set; }

		// Token: 0x060031BC RID: 12732 RVA: 0x00064DB6 File Offset: 0x00062FB6
		public ContextFillerAttribute(ContextType type)
		{
			this.contextType = type;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x00064DC8 File Offset: 0x00062FC8
		internal static bool AssureValidity(MethodInfo method, ContextFillerAttribute attr)
		{
			if (!method.IsGenericMethod && !method.IsGenericMethodDefinition && (method.ReturnType == null || method.ReturnType == typeof(void)))
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 2 && parameters[0].ParameterType == typeof(NodeEditorInputInfo) && parameters[1].ParameterType == typeof(GenericMenu))
				{
					return true;
				}
				Debug.LogWarning("Method " + method.Name + " has incorrect signature for ContextAttribute!");
			}
			return false;
		}
	}
}
