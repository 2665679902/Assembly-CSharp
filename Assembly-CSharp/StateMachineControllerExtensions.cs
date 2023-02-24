using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000404 RID: 1028
public static class StateMachineControllerExtensions
{
	// Token: 0x06001550 RID: 5456 RVA: 0x0006EBB0 File Offset: 0x0006CDB0
	public static StateMachineInstanceType GetSMI<StateMachineInstanceType>(this StateMachine.Instance smi) where StateMachineInstanceType : StateMachine.Instance
	{
		return smi.gameObject.GetSMI<StateMachineInstanceType>();
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x0006EBBD File Offset: 0x0006CDBD
	public static DefType GetDef<DefType>(this Component cmp) where DefType : StateMachine.BaseDef
	{
		return cmp.gameObject.GetDef<DefType>();
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x0006EBCC File Offset: 0x0006CDCC
	public static DefType GetDef<DefType>(this GameObject go) where DefType : StateMachine.BaseDef
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component == null)
		{
			return default(DefType);
		}
		return component.GetDef<DefType>();
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x0006EBF9 File Offset: 0x0006CDF9
	public static StateMachineInstanceType GetSMI<StateMachineInstanceType>(this Component cmp) where StateMachineInstanceType : class
	{
		return cmp.gameObject.GetSMI<StateMachineInstanceType>();
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x0006EC08 File Offset: 0x0006CE08
	public static StateMachineInstanceType GetSMI<StateMachineInstanceType>(this GameObject go) where StateMachineInstanceType : class
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			return component.GetSMI<StateMachineInstanceType>();
		}
		return default(StateMachineInstanceType);
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x0006EC35 File Offset: 0x0006CE35
	public static List<StateMachineInstanceType> GetAllSMI<StateMachineInstanceType>(this Component cmp) where StateMachineInstanceType : class
	{
		return cmp.gameObject.GetAllSMI<StateMachineInstanceType>();
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x0006EC44 File Offset: 0x0006CE44
	public static List<StateMachineInstanceType> GetAllSMI<StateMachineInstanceType>(this GameObject go) where StateMachineInstanceType : class
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			return component.GetAllSMI<StateMachineInstanceType>();
		}
		return new List<StateMachineInstanceType>();
	}
}
