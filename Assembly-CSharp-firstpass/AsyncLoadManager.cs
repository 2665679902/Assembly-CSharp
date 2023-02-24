using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x0200007F RID: 127
public static class AsyncLoadManager<AsyncLoaderType>
{
	// Token: 0x06000501 RID: 1281 RVA: 0x000188E8 File Offset: 0x00016AE8
	public static void Run()
	{
		List<AsyncLoader> list = new List<AsyncLoader>();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			foreach (Type type in assemblies[i].GetTypes())
			{
				if (!type.IsAbstract && typeof(AsyncLoaderType).IsAssignableFrom(type))
				{
					AsyncLoader asyncLoader = (AsyncLoader)Activator.CreateInstance(type);
					list.Add(asyncLoader);
					AsyncLoadManager<AsyncLoaderType>.loaders[type] = asyncLoader;
					asyncLoader.CollectLoaders(list);
				}
			}
		}
		if (AsyncLoadManager<AsyncLoaderType>.loaders.Count > 0)
		{
			WorkItemCollection<AsyncLoadManager<AsyncLoaderType>.RunLoader, object> workItemCollection = new WorkItemCollection<AsyncLoadManager<AsyncLoaderType>.RunLoader, object>();
			workItemCollection.Reset(null);
			foreach (AsyncLoader asyncLoader2 in list)
			{
				workItemCollection.Add(new AsyncLoadManager<AsyncLoaderType>.RunLoader
				{
					loader = asyncLoader2
				});
			}
			GlobalJobManager.Run(workItemCollection);
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x000189F8 File Offset: 0x00016BF8
	public static AsyncLoader GetLoader(Type type)
	{
		return AsyncLoadManager<AsyncLoaderType>.loaders[type];
	}

	// Token: 0x0400051A RID: 1306
	private static Dictionary<Type, AsyncLoader> loaders = new Dictionary<Type, AsyncLoader>();

	// Token: 0x020009C9 RID: 2505
	public abstract class AsyncLoader<LoaderType> : AsyncLoader where LoaderType : class
	{
		// Token: 0x06005378 RID: 21368 RVA: 0x0009BD4C File Offset: 0x00099F4C
		public static LoaderType Get()
		{
			return AsyncLoadManager<AsyncLoaderType>.GetLoader(typeof(LoaderType)) as LoaderType;
		}
	}

	// Token: 0x020009CA RID: 2506
	private struct RunLoader : IWorkItem<object>
	{
		// Token: 0x0600537A RID: 21370 RVA: 0x0009BD6F File Offset: 0x00099F6F
		public void Run(object shared_data)
		{
			this.loader.Run();
		}

		// Token: 0x040021E9 RID: 8681
		public AsyncLoader loader;
	}
}
