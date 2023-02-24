using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class MyCmp : MyAttributeManager<Component>
{
	// Token: 0x06000833 RID: 2099 RVA: 0x000213D0 File Offset: 0x0001F5D0
	public static void Init()
	{
		MyAttributes.Register(new MyCmp(new Dictionary<Type, MethodInfo>
		{
			{
				typeof(MyCmpAdd),
				typeof(MyCmp).GetMethod("FindOrAddComponent")
			},
			{
				typeof(MyCmpGet),
				typeof(MyCmp).GetMethod("FindComponent")
			},
			{
				typeof(MyCmpReq),
				typeof(MyCmp).GetMethod("RequireComponent")
			}
		}, new Action<Component>(Util.SpawnComponent)));
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00021464 File Offset: 0x0001F664
	public MyCmp(Dictionary<Type, MethodInfo> attributeMap, Action<Component> spawnFunc)
		: base(attributeMap, spawnFunc)
	{
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0002146E File Offset: 0x0001F66E
	public static Component FindComponent<T>(KMonoBehaviour c, bool isStart) where T : Component
	{
		return c.FindComponent<T>();
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002147B File Offset: 0x0001F67B
	public static Component RequireComponent<T>(KMonoBehaviour c, bool isStart) where T : Component
	{
		if (isStart)
		{
			return c.RequireComponent<T>();
		}
		return c.FindComponent<T>();
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00021497 File Offset: 0x0001F697
	public static Component FindOrAddComponent<T>(KMonoBehaviour c, bool isStart) where T : Component
	{
		return c.FindOrAddComponent<T>();
	}
}
