using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020003FC RID: 1020
public class MySmi : MyAttributeManager<StateMachine.Instance>
{
	// Token: 0x0600150C RID: 5388 RVA: 0x0006DF8C File Offset: 0x0006C18C
	public static void Init()
	{
		MyAttributes.Register(new MySmi(new Dictionary<Type, MethodInfo>
		{
			{
				typeof(MySmiGet),
				typeof(MySmi).GetMethod("FindSmi")
			},
			{
				typeof(MySmiReq),
				typeof(MySmi).GetMethod("RequireSmi")
			}
		}));
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
	public MySmi(Dictionary<Type, MethodInfo> attributeMap)
		: base(attributeMap, null)
	{
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x0006DFFC File Offset: 0x0006C1FC
	public static StateMachine.Instance FindSmi<T>(KMonoBehaviour c, bool isStart) where T : StateMachine.Instance
	{
		StateMachineController component = c.GetComponent<StateMachineController>();
		if (component != null)
		{
			return component.GetSMI<T>();
		}
		return null;
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x0006E028 File Offset: 0x0006C228
	public static StateMachine.Instance RequireSmi<T>(KMonoBehaviour c, bool isStart) where T : StateMachine.Instance
	{
		if (isStart)
		{
			StateMachine.Instance instance = MySmi.FindSmi<T>(c, isStart);
			Debug.Assert(instance != null, string.Format("{0} '{1}' requires a StateMachineInstance of type {2}!", c.GetType().ToString(), c.name, typeof(T)));
			return instance;
		}
		return MySmi.FindSmi<T>(c, isStart);
	}
}
