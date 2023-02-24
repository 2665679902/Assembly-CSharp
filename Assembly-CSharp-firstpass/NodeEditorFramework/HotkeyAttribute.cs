using System;
using System.Reflection;
using UnityEngine;

namespace NodeEditorFramework
{
	// Token: 0x02000483 RID: 1155
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class HotkeyAttribute : Attribute
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x00064AED File Offset: 0x00062CED
		// (set) Token: 0x060031A7 RID: 12711 RVA: 0x00064AF5 File Offset: 0x00062CF5
		public KeyCode handledHotKey { get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x00064AFE File Offset: 0x00062CFE
		// (set) Token: 0x060031A9 RID: 12713 RVA: 0x00064B06 File Offset: 0x00062D06
		public EventModifiers? modifiers { get; private set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x00064B0F File Offset: 0x00062D0F
		// (set) Token: 0x060031AB RID: 12715 RVA: 0x00064B17 File Offset: 0x00062D17
		public EventType? limitingEventType { get; private set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x00064B20 File Offset: 0x00062D20
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x00064B28 File Offset: 0x00062D28
		public int priority { get; private set; }

		// Token: 0x060031AE RID: 12718 RVA: 0x00064B34 File Offset: 0x00062D34
		public HotkeyAttribute(KeyCode handledKey)
		{
			this.handledHotKey = handledKey;
			this.modifiers = null;
			this.limitingEventType = null;
			this.priority = 50;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x00064B74 File Offset: 0x00062D74
		public HotkeyAttribute(KeyCode handledKey, EventModifiers eventModifiers)
		{
			this.handledHotKey = handledKey;
			this.modifiers = new EventModifiers?(eventModifiers);
			this.limitingEventType = null;
			this.priority = 50;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x00064BB4 File Offset: 0x00062DB4
		public HotkeyAttribute(KeyCode handledKey, EventType LimitEventType)
		{
			this.handledHotKey = handledKey;
			this.modifiers = null;
			this.limitingEventType = new EventType?(LimitEventType);
			this.priority = 50;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x00064BF4 File Offset: 0x00062DF4
		public HotkeyAttribute(KeyCode handledKey, EventType LimitEventType, int priorityValue)
		{
			this.handledHotKey = handledKey;
			this.modifiers = null;
			this.limitingEventType = new EventType?(LimitEventType);
			this.priority = priorityValue;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x00064C30 File Offset: 0x00062E30
		public HotkeyAttribute(KeyCode handledKey, EventModifiers eventModifiers, EventType LimitEventType)
		{
			this.handledHotKey = handledKey;
			this.modifiers = new EventModifiers?(eventModifiers);
			this.limitingEventType = new EventType?(LimitEventType);
			this.priority = 50;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00064C60 File Offset: 0x00062E60
		internal static bool AssureValidity(MethodInfo method, HotkeyAttribute attr)
		{
			if (!method.IsGenericMethod && !method.IsGenericMethodDefinition && (method.ReturnType == null || method.ReturnType == typeof(void)))
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(typeof(NodeEditorInputInfo)))
				{
					return true;
				}
				global::Debug.LogWarning("Method " + method.Name + " has incorrect signature for HotkeyAttribute!");
			}
			return false;
		}
	}
}
