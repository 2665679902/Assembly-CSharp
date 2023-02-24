using System;
using System.Reflection;

namespace NodeEditorFramework
{
	// Token: 0x02000485 RID: 1157
	[AttributeUsage(AttributeTargets.Method)]
	public class ContextEntryAttribute : Attribute
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x00064CE5 File Offset: 0x00062EE5
		// (set) Token: 0x060031B5 RID: 12725 RVA: 0x00064CED File Offset: 0x00062EED
		public ContextType contextType { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x00064CF6 File Offset: 0x00062EF6
		// (set) Token: 0x060031B7 RID: 12727 RVA: 0x00064CFE File Offset: 0x00062EFE
		public string contextPath { get; private set; }

		// Token: 0x060031B8 RID: 12728 RVA: 0x00064D07 File Offset: 0x00062F07
		public ContextEntryAttribute(ContextType type, string path)
		{
			this.contextType = type;
			this.contextPath = path;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00064D20 File Offset: 0x00062F20
		internal static bool AssureValidity(MethodInfo method, ContextEntryAttribute attr)
		{
			if (!method.IsGenericMethod && !method.IsGenericMethodDefinition && (method.ReturnType == null || method.ReturnType == typeof(void)))
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 1 && parameters[0].ParameterType == typeof(NodeEditorInputInfo))
				{
					return true;
				}
				Debug.LogWarning("Method " + method.Name + " has incorrect signature for ContextAttribute!");
			}
			return false;
		}
	}
}
