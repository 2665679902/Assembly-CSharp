using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A57 RID: 2647
public class ClusterNameDisplayScreen : KScreen
{
	// Token: 0x0600507D RID: 20605 RVA: 0x001CD5F6 File Offset: 0x001CB7F6
	public static void DestroyInstance()
	{
		ClusterNameDisplayScreen.Instance = null;
	}

	// Token: 0x0600507E RID: 20606 RVA: 0x001CD5FE File Offset: 0x001CB7FE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ClusterNameDisplayScreen.Instance = this;
	}

	// Token: 0x0600507F RID: 20607 RVA: 0x001CD60C File Offset: 0x001CB80C
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06005080 RID: 20608 RVA: 0x001CD614 File Offset: 0x001CB814
	public void AddNewEntry(ClusterGridEntity representedObject)
	{
		if (this.GetEntry(representedObject) != null)
		{
			return;
		}
		ClusterNameDisplayScreen.Entry entry = new ClusterNameDisplayScreen.Entry();
		entry.grid_entity = representedObject;
		GameObject gameObject = Util.KInstantiateUI(this.nameAndBarsPrefab, base.gameObject, true);
		entry.display_go = gameObject;
		gameObject.name = representedObject.name + " cluster overlay";
		entry.Name = representedObject.name;
		entry.refs = gameObject.GetComponent<HierarchyReferences>();
		entry.bars_go = entry.refs.GetReference<RectTransform>("Bars").gameObject;
		this.m_entries.Add(entry);
		if (representedObject.GetComponent<KSelectable>() != null)
		{
			this.UpdateName(representedObject);
			this.UpdateBars(representedObject);
		}
	}

	// Token: 0x06005081 RID: 20609 RVA: 0x001CD6C4 File Offset: 0x001CB8C4
	private void LateUpdate()
	{
		if (App.isLoading || App.IsExiting)
		{
			return;
		}
		int num = this.m_entries.Count;
		int i = 0;
		while (i < num)
		{
			if (this.m_entries[i].grid_entity != null && ClusterMapScreen.GetRevealLevel(this.m_entries[i].grid_entity) == ClusterRevealLevel.Visible)
			{
				Transform gridEntityNameTarget = ClusterMapScreen.Instance.GetGridEntityNameTarget(this.m_entries[i].grid_entity);
				if (gridEntityNameTarget != null)
				{
					Vector3 position = gridEntityNameTarget.GetPosition();
					this.m_entries[i].display_go.GetComponent<RectTransform>().SetPositionAndRotation(position, Quaternion.identity);
					this.m_entries[i].display_go.SetActive(this.m_entries[i].grid_entity.IsVisible && this.m_entries[i].grid_entity.ShowName());
				}
				else if (this.m_entries[i].display_go.activeSelf)
				{
					this.m_entries[i].display_go.SetActive(false);
				}
				this.UpdateBars(this.m_entries[i].grid_entity);
				if (this.m_entries[i].bars_go != null)
				{
					this.m_entries[i].bars_go.GetComponentsInChildren<KCollider2D>(false, this.workingList);
					foreach (KCollider2D kcollider2D in this.workingList)
					{
						kcollider2D.MarkDirty(false);
					}
				}
				i++;
			}
			else
			{
				UnityEngine.Object.Destroy(this.m_entries[i].display_go);
				num--;
				this.m_entries[i] = this.m_entries[num];
			}
		}
		this.m_entries.RemoveRange(num, this.m_entries.Count - num);
	}

	// Token: 0x06005082 RID: 20610 RVA: 0x001CD8DC File Offset: 0x001CBADC
	public void UpdateName(ClusterGridEntity representedObject)
	{
		ClusterNameDisplayScreen.Entry entry = this.GetEntry(representedObject);
		if (entry == null)
		{
			return;
		}
		KSelectable component = representedObject.GetComponent<KSelectable>();
		entry.display_go.name = component.GetProperName() + " cluster overlay";
		LocText componentInChildren = entry.display_go.GetComponentInChildren<LocText>();
		if (componentInChildren != null)
		{
			componentInChildren.text = component.GetProperName();
		}
	}

	// Token: 0x06005083 RID: 20611 RVA: 0x001CD938 File Offset: 0x001CBB38
	private void UpdateBars(ClusterGridEntity representedObject)
	{
		ClusterNameDisplayScreen.Entry entry = this.GetEntry(representedObject);
		if (entry == null)
		{
			return;
		}
		GenericUIProgressBar componentInChildren = entry.bars_go.GetComponentInChildren<GenericUIProgressBar>(true);
		if (entry.grid_entity.ShowProgressBar())
		{
			if (!componentInChildren.gameObject.activeSelf)
			{
				componentInChildren.gameObject.SetActive(true);
			}
			componentInChildren.SetFillPercentage(entry.grid_entity.GetProgress());
			return;
		}
		if (componentInChildren.gameObject.activeSelf)
		{
			componentInChildren.gameObject.SetActive(false);
		}
	}

	// Token: 0x06005084 RID: 20612 RVA: 0x001CD9B0 File Offset: 0x001CBBB0
	private ClusterNameDisplayScreen.Entry GetEntry(ClusterGridEntity entity)
	{
		return this.m_entries.Find((ClusterNameDisplayScreen.Entry entry) => entry.grid_entity == entity);
	}

	// Token: 0x04003629 RID: 13865
	public static ClusterNameDisplayScreen Instance;

	// Token: 0x0400362A RID: 13866
	public GameObject nameAndBarsPrefab;

	// Token: 0x0400362B RID: 13867
	[SerializeField]
	private Color selectedColor;

	// Token: 0x0400362C RID: 13868
	[SerializeField]
	private Color defaultColor;

	// Token: 0x0400362D RID: 13869
	private List<ClusterNameDisplayScreen.Entry> m_entries = new List<ClusterNameDisplayScreen.Entry>();

	// Token: 0x0400362E RID: 13870
	private List<KCollider2D> workingList = new List<KCollider2D>();

	// Token: 0x020018E3 RID: 6371
	private class Entry
	{
		// Token: 0x040072A9 RID: 29353
		public string Name;

		// Token: 0x040072AA RID: 29354
		public ClusterGridEntity grid_entity;

		// Token: 0x040072AB RID: 29355
		public GameObject display_go;

		// Token: 0x040072AC RID: 29356
		public GameObject bars_go;

		// Token: 0x040072AD RID: 29357
		public HierarchyReferences refs;
	}
}
