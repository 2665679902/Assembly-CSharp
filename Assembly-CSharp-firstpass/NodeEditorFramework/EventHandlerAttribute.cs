using System;
using System.Reflection;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000482 RID: 1154
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class EventHandlerAttribute : Attribute
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000649BD File Offset: 0x00062BBD
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x000649C5 File Offset: 0x00062BC5
		public EventType? handledEvent { get; private set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000649CE File Offset: 0x00062BCE
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000649D6 File Offset: 0x00062BD6
		public int priority { get; private set; }

		// Token: 0x060031A1 RID: 12705 RVA: 0x000649DF File Offset: 0x00062BDF
		public EventHandlerAttribute(EventType eventType, int priorityValue)
		{
			this.handledEvent = new EventType?(eventType);
			this.priority = priorityValue;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000649FC File Offset: 0x00062BFC
		public EventHandlerAttribute(int priorityValue)
		{
			this.handledEvent = null;
			this.priority = priorityValue;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00064A25 File Offset: 0x00062C25
		public EventHandlerAttribute(EventType eventType)
		{
			this.handledEvent = new EventType?(eventType);
			this.priority = 50;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x00064A44 File Offset: 0x00062C44
		public EventHandlerAttribute()
		{
			this.handledEvent = null;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00064A68 File Offset: 0x00062C68
		internal static bool AssureValidity(MethodInfo method, EventHandlerAttribute attr)
		{
			if (!method.IsGenericMethod && !method.IsGenericMethodDefinition && (method.ReturnType == null || method.ReturnType == typeof(void)))
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 1 && parameters[0].ParameterType == typeof(NodeEditorInputInfo))
				{
					return true;
				}
				global::Debug.LogWarning("Method " + method.Name + " has incorrect signature for EventHandlerAttribute!");
			}
			return false;
		}
	}
}
