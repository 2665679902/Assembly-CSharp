using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000748 RID: 1864
[AddComponentMenu("KMonoBehaviour/scripts/EntityConfigManager")]
public class EntityConfigManager : KMonoBehaviour
{
	// Token: 0x0600335F RID: 13151 RVA: 0x00114A07 File Offset: 0x00112C07
	public static void DestroyInstance()
	{
		EntityConfigManager.Instance = null;
	}

	// Token: 0x06003360 RID: 13152 RVA: 0x00114A0F File Offset: 0x00112C0F
	protected override void OnPrefabInit()
	{
		EntityConfigManager.Instance = this;
	}

	// Token: 0x06003361 RID: 13153 RVA: 0x00114A18 File Offset: 0x00112C18
	private static int GetSortOrder(Type type)
	{
		foreach (Attribute attribute in type.GetCustomAttributes(true))
		{
			if (attribute.GetType() == typeof(EntityConfigOrder))
			{
				return (attribute as EntityConfigOrder).sortOrder;
			}
		}
		return 0;
	}

	// Token: 0x06003362 RID: 13154 RVA: 0x00114A68 File Offset: 0x00112C68
	public void LoadGeneratedEntities(List<Type> types)
	{
		Type typeFromHandle = typeof(IEntityConfig);
		Type typeFromHandle2 = typeof(IMultiEntityConfig);
		List<EntityConfigManager.ConfigEntry> list = new List<EntityConfigManager.ConfigEntry>();
		foreach (Type type in types)
		{
			if ((typeFromHandle.IsAssignableFrom(type) || typeFromHandle2.IsAssignableFrom(type)) && !type.IsAbstract && !type.IsInterface)
			{
				int sortOrder = EntityConfigManager.GetSortOrder(type);
				EntityConfigManager.ConfigEntry configEntry = new EntityConfigManager.ConfigEntry
				{
					type = type,
					sortOrder = sortOrder
				};
				list.Add(configEntry);
			}
		}
		list.Sort((EntityConfigManager.ConfigEntry x, EntityConfigManager.ConfigEntry y) => x.sortOrder.CompareTo(y.sortOrder));
		foreach (EntityConfigManager.ConfigEntry configEntry2 in list)
		{
			object obj = Activator.CreateInstance(configEntry2.type);
			if (obj is IEntityConfig && DlcManager.IsDlcListValidForCurrentContent((obj as IEntityConfig).GetDlcIds()))
			{
				this.RegisterEntity(obj as IEntityConfig);
			}
			if (obj is IMultiEntityConfig)
			{
				this.RegisterEntities(obj as IMultiEntityConfig);
			}
		}
	}

	// Token: 0x06003363 RID: 13155 RVA: 0x00114BC8 File Offset: 0x00112DC8
	public void RegisterEntity(IEntityConfig config)
	{
		KPrefabID component = config.CreatePrefab().GetComponent<KPrefabID>();
		component.prefabInitFn += config.OnPrefabInit;
		component.prefabSpawnFn += config.OnSpawn;
		Assets.AddPrefab(component);
	}

	// Token: 0x06003364 RID: 13156 RVA: 0x00114C00 File Offset: 0x00112E00
	public void RegisterEntities(IMultiEntityConfig config)
	{
		foreach (GameObject gameObject in config.CreatePrefabs())
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			component.prefabInitFn += config.OnPrefabInit;
			component.prefabSpawnFn += config.OnSpawn;
			Assets.AddPrefab(component);
		}
	}

	// Token: 0x04001F86 RID: 8070
	public static EntityConfigManager Instance;

	// Token: 0x0200144E RID: 5198
	private struct ConfigEntry
	{
		// Token: 0x04006316 RID: 25366
		public Type type;

		// Token: 0x04006317 RID: 25367
		public int sortOrder;
	}
}
