using System;

// Token: 0x020000FA RID: 250
public abstract class Singleton<T> where T : class, new()
{
	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000893 RID: 2195 RVA: 0x00022B28 File Offset: 0x00020D28
	public static T Instance
	{
		get
		{
			object @lock = Singleton<T>._lock;
			T instance;
			lock (@lock)
			{
				instance = Singleton<T>._instance;
			}
			return instance;
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x00022B68 File Offset: 0x00020D68
	public static void CreateInstance()
	{
		object @lock = Singleton<T>._lock;
		lock (@lock)
		{
			if (Singleton<T>._instance == null)
			{
				Singleton<T>._instance = new T();
			}
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00022BB8 File Offset: 0x00020DB8
	public static void DestroyInstance()
	{
		object @lock = Singleton<T>._lock;
		lock (@lock)
		{
			Singleton<T>._instance = default(T);
		}
	}

	// Token: 0x0400065A RID: 1626
	private static T _instance;

	// Token: 0x0400065B RID: 1627
	private static object _lock = new object();
}
