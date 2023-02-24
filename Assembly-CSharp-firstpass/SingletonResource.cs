using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public static class SingletonResource<T> where T : ResourceFile
{
	// Token: 0x0600089A RID: 2202 RVA: 0x00022C1C File Offset: 0x00020E1C
	public static T Get()
	{
		if (SingletonResource<T>.StaticInstance == null)
		{
			SingletonResource<T>.StaticInstance = Resources.Load<T>(typeof(T).Name);
			SingletonResource<T>.StaticInstance.Initialize();
		}
		return SingletonResource<T>.StaticInstance;
	}

	// Token: 0x0400065C RID: 1628
	private static T StaticInstance;
}
