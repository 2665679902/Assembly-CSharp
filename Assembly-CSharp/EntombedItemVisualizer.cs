using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200074B RID: 1867
[AddComponentMenu("KMonoBehaviour/scripts/EntombedItemVisualizer")]
public class EntombedItemVisualizer : KMonoBehaviour
{
	// Token: 0x06003377 RID: 13175 RVA: 0x001153D4 File Offset: 0x001135D4
	public void Clear()
	{
		this.cellEntombedCounts.Clear();
	}

	// Token: 0x06003378 RID: 13176 RVA: 0x001153E1 File Offset: 0x001135E1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.entombedItemPool = new GameObjectPool(new Func<GameObject>(this.InstantiateEntombedObject), 32);
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x00115404 File Offset: 0x00113604
	public bool AddItem(int cell)
	{
		bool flag = false;
		if (Grid.Objects[cell, 9] == null)
		{
			flag = true;
			EntombedItemVisualizer.Data data;
			this.cellEntombedCounts.TryGetValue(cell, out data);
			if (data.refCount == 0)
			{
				GameObject instance = this.entombedItemPool.GetInstance();
				instance.transform.SetPosition(Grid.CellToPosCCC(cell, Grid.SceneLayer.FXFront));
				instance.transform.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.value * 360f);
				KBatchedAnimController component = instance.GetComponent<KBatchedAnimController>();
				int num = UnityEngine.Random.Range(0, EntombedItemVisualizer.EntombedVisualizerAnims.Length);
				string text = EntombedItemVisualizer.EntombedVisualizerAnims[num];
				component.initialAnim = text;
				instance.SetActive(true);
				component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
				data.controller = component;
			}
			data.refCount++;
			this.cellEntombedCounts[cell] = data;
		}
		return flag;
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x001154F4 File Offset: 0x001136F4
	public void RemoveItem(int cell)
	{
		EntombedItemVisualizer.Data data;
		if (this.cellEntombedCounts.TryGetValue(cell, out data))
		{
			data.refCount--;
			if (data.refCount == 0)
			{
				this.ReleaseVisualizer(cell, data);
				return;
			}
			this.cellEntombedCounts[cell] = data;
		}
	}

	// Token: 0x0600337B RID: 13179 RVA: 0x0011553C File Offset: 0x0011373C
	public void ForceClear(int cell)
	{
		EntombedItemVisualizer.Data data;
		if (this.cellEntombedCounts.TryGetValue(cell, out data))
		{
			this.ReleaseVisualizer(cell, data);
		}
	}

	// Token: 0x0600337C RID: 13180 RVA: 0x00115564 File Offset: 0x00113764
	private void ReleaseVisualizer(int cell, EntombedItemVisualizer.Data data)
	{
		if (data.controller != null)
		{
			data.controller.gameObject.SetActive(false);
			this.entombedItemPool.ReleaseInstance(data.controller.gameObject);
		}
		this.cellEntombedCounts.Remove(cell);
	}

	// Token: 0x0600337D RID: 13181 RVA: 0x001155B3 File Offset: 0x001137B3
	public bool IsEntombedItem(int cell)
	{
		return this.cellEntombedCounts.ContainsKey(cell) && this.cellEntombedCounts[cell].refCount > 0;
	}

	// Token: 0x0600337E RID: 13182 RVA: 0x001155D9 File Offset: 0x001137D9
	private GameObject InstantiateEntombedObject()
	{
		GameObject gameObject = GameUtil.KInstantiate(this.entombedItemPrefab, Grid.SceneLayer.FXFront, null, 0);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x04001F91 RID: 8081
	[SerializeField]
	private GameObject entombedItemPrefab;

	// Token: 0x04001F92 RID: 8082
	private static readonly string[] EntombedVisualizerAnims = new string[] { "idle1", "idle2", "idle3", "idle4" };

	// Token: 0x04001F93 RID: 8083
	private GameObjectPool entombedItemPool;

	// Token: 0x04001F94 RID: 8084
	private Dictionary<int, EntombedItemVisualizer.Data> cellEntombedCounts = new Dictionary<int, EntombedItemVisualizer.Data>();

	// Token: 0x02001451 RID: 5201
	private struct Data
	{
		// Token: 0x04006320 RID: 25376
		public int refCount;

		// Token: 0x04006321 RID: 25377
		public KBatchedAnimController controller;
	}
}
