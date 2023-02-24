using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public static class EntityTemplateExtensions
{
	// Token: 0x0600020C RID: 524 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
	public static DefType AddOrGetDef<DefType>(this GameObject go) where DefType : StateMachine.BaseDef
	{
		StateMachineController stateMachineController = go.AddOrGet<StateMachineController>();
		DefType defType = stateMachineController.GetDef<DefType>();
		if (defType == null)
		{
			defType = Activator.CreateInstance<DefType>();
			stateMachineController.AddDef(defType);
			defType.Configure(go);
		}
		return defType;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
	public static ComponentType AddOrGet<ComponentType>(this GameObject go) where ComponentType : Component
	{
		ComponentType componentType = go.GetComponent<ComponentType>();
		if (componentType == null)
		{
			componentType = go.AddComponent<ComponentType>();
		}
		return componentType;
	}
}
