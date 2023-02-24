using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B9D RID: 2973
public class CheckboxListGroupSideScreen : SideScreenContent
{
	// Token: 0x06005D78 RID: 23928 RVA: 0x00222033 File Offset: 0x00220233
	private CheckboxListGroupSideScreen.CheckboxContainer InstantiateCheckboxContainer()
	{
		return new CheckboxListGroupSideScreen.CheckboxContainer(Util.KInstantiateUI(this.checkboxGroupPrefab, this.groupParent.gameObject, true).GetComponent<HierarchyReferences>());
	}

	// Token: 0x06005D79 RID: 23929 RVA: 0x00222056 File Offset: 0x00220256
	private GameObject InstantiateCheckbox()
	{
		return Util.KInstantiateUI(this.checkboxPrefab, this.checkboxParent.gameObject, false);
	}

	// Token: 0x06005D7A RID: 23930 RVA: 0x0022206F File Offset: 0x0022026F
	protected override void OnSpawn()
	{
		this.checkboxPrefab.SetActive(false);
		this.checkboxGroupPrefab.SetActive(false);
		base.OnSpawn();
	}

	// Token: 0x06005D7B RID: 23931 RVA: 0x00222090 File Offset: 0x00220290
	public override bool IsValidForTarget(GameObject target)
	{
		ICheckboxListGroupControl[] components = target.GetComponents<ICheckboxListGroupControl>();
		if (components != null)
		{
			ICheckboxListGroupControl[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].SidescreenEnabled())
				{
					return true;
				}
			}
		}
		using (List<ICheckboxListGroupControl>.Enumerator enumerator = target.GetAllSMI<ICheckboxListGroupControl>().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.SidescreenEnabled())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06005D7C RID: 23932 RVA: 0x00222114 File Offset: 0x00220314
	public override int GetSideScreenSortOrder()
	{
		if (this.targets == null)
		{
			return 20;
		}
		return this.targets[0].CheckboxSideScreenSortOrder();
	}

	// Token: 0x06005D7D RID: 23933 RVA: 0x00222134 File Offset: 0x00220334
	public override void SetTarget(GameObject target)
	{
		if (target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.targets = target.GetAllSMI<ICheckboxListGroupControl>();
		this.targets.AddRange(target.GetComponents<ICheckboxListGroupControl>());
		this.Rebuild(target);
		this.uiRefreshSubHandle = this.currentBuildTarget.Subscribe(1980521255, new Action<object>(this.Refresh));
	}

	// Token: 0x06005D7E RID: 23934 RVA: 0x0022219C File Offset: 0x0022039C
	public override void ClearTarget()
	{
		if (this.uiRefreshSubHandle != -1 && this.currentBuildTarget != null)
		{
			this.currentBuildTarget.Unsubscribe(this.uiRefreshSubHandle);
			this.uiRefreshSubHandle = -1;
		}
		this.ReleaseContainers(this.activeChecklistGroups.Count);
	}

	// Token: 0x06005D7F RID: 23935 RVA: 0x002221E9 File Offset: 0x002203E9
	public override string GetTitle()
	{
		if (this.targets != null && this.targets.Count > 0 && this.targets[0] != null)
		{
			return this.targets[0].Title;
		}
		return base.GetTitle();
	}

	// Token: 0x06005D80 RID: 23936 RVA: 0x00222228 File Offset: 0x00220428
	private void Rebuild(GameObject buildTarget)
	{
		if (this.checkboxContainerPool == null)
		{
			this.checkboxContainerPool = new ObjectPool<CheckboxListGroupSideScreen.CheckboxContainer>(new Func<CheckboxListGroupSideScreen.CheckboxContainer>(this.InstantiateCheckboxContainer), 0);
			this.checkboxPool = new GameObjectPool(new Func<GameObject>(this.InstantiateCheckbox), 0);
		}
		if (buildTarget == this.currentBuildTarget)
		{
			this.Refresh(null);
			return;
		}
		this.currentBuildTarget = buildTarget;
		this.descriptionLabel.enabled = !this.targets[0].Description.IsNullOrWhiteSpace();
		if (!this.targets[0].Description.IsNullOrWhiteSpace())
		{
			this.descriptionLabel.SetText(this.targets[0].Description);
		}
		foreach (ICheckboxListGroupControl checkboxListGroupControl in this.targets)
		{
			foreach (ICheckboxListGroupControl.ListGroup listGroup in checkboxListGroupControl.GetData())
			{
				CheckboxListGroupSideScreen.CheckboxContainer instance = this.checkboxContainerPool.GetInstance();
				this.InitContainer(checkboxListGroupControl, listGroup, instance);
			}
		}
	}

	// Token: 0x06005D81 RID: 23937 RVA: 0x00222358 File Offset: 0x00220558
	[ContextMenu("Force refresh")]
	private void Test()
	{
		this.Refresh(null);
	}

	// Token: 0x06005D82 RID: 23938 RVA: 0x00222364 File Offset: 0x00220564
	private void Refresh(object data = null)
	{
		int num = 0;
		foreach (ICheckboxListGroupControl checkboxListGroupControl in this.targets)
		{
			foreach (ICheckboxListGroupControl.ListGroup listGroup in checkboxListGroupControl.GetData())
			{
				if (++num > this.activeChecklistGroups.Count)
				{
					this.InitContainer(checkboxListGroupControl, listGroup, this.checkboxContainerPool.GetInstance());
				}
				CheckboxListGroupSideScreen.CheckboxContainer checkboxContainer = this.activeChecklistGroups[num - 1];
				if (listGroup.resolveTitleCallback != null)
				{
					checkboxContainer.container.GetReference<LocText>("Text").SetText(listGroup.resolveTitleCallback(listGroup.title));
				}
				int num2 = 0;
				foreach (ICheckboxListGroupControl.CheckboxItem checkboxItem in listGroup.checkboxItems)
				{
					num2++;
					checkboxContainer.checkboxUIItems[num2 - 1].GetReference<Image>("Check").enabled = checkboxItem.isOn;
				}
			}
		}
		this.ReleaseContainers(this.activeChecklistGroups.Count - num);
	}

	// Token: 0x06005D83 RID: 23939 RVA: 0x002224C0 File Offset: 0x002206C0
	private void ReleaseContainers(int count)
	{
		int count2 = this.activeChecklistGroups.Count;
		for (int i = 1; i <= count; i++)
		{
			int num = count2 - i;
			CheckboxListGroupSideScreen.CheckboxContainer checkboxContainer = this.activeChecklistGroups[num];
			this.activeChecklistGroups.RemoveAt(num);
			for (int j = checkboxContainer.checkboxUIItems.Count - 1; j >= 0; j--)
			{
				GameObject gameObject = checkboxContainer.checkboxUIItems[j].gameObject;
				checkboxContainer.checkboxUIItems.RemoveAt(j);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(this.checkboxParent);
				this.checkboxPool.ReleaseInstance(gameObject);
			}
			checkboxContainer.container.gameObject.SetActive(false);
			this.checkboxContainerPool.ReleaseInstance(checkboxContainer);
		}
	}

	// Token: 0x06005D84 RID: 23940 RVA: 0x0022258C File Offset: 0x0022078C
	private void InitContainer(ICheckboxListGroupControl target, ICheckboxListGroupControl.ListGroup group, CheckboxListGroupSideScreen.CheckboxContainer groupUI)
	{
		this.activeChecklistGroups.Add(groupUI);
		groupUI.container.gameObject.SetActive(true);
		string text = group.title;
		if (group.resolveTitleCallback != null)
		{
			text = group.resolveTitleCallback(text);
		}
		groupUI.container.GetReference<LocText>("Text").SetText(text);
		ICheckboxListGroupControl.CheckboxItem[] checkboxItems = group.checkboxItems;
		for (int i = 0; i < checkboxItems.Length; i++)
		{
			ICheckboxListGroupControl.CheckboxItem item = checkboxItems[i];
			HierarchyReferences component = this.checkboxPool.GetInstance().GetComponent<HierarchyReferences>();
			groupUI.checkboxUIItems.Add(component);
			component.transform.SetParent(groupUI.container.transform);
			component.gameObject.SetActive(true);
			component.GetReference<LocText>("Text").SetText(item.text);
			component.GetReference<Image>("Check").enabled = item.isOn;
			ToolTip reference = component.GetReference<ToolTip>("Tooltip");
			reference.SetSimpleTooltip(item.tooltip);
			reference.refreshWhileHovering = item.resolveTooltipCallback != null;
			reference.OnToolTip = delegate
			{
				if (item.resolveTooltipCallback == null)
				{
					return item.tooltip;
				}
				return item.resolveTooltipCallback(item.tooltip, target);
			};
		}
	}

	// Token: 0x04003FDC RID: 16348
	public const int DefaultCheckboxListSideScreenSortOrder = 20;

	// Token: 0x04003FDD RID: 16349
	private ObjectPool<CheckboxListGroupSideScreen.CheckboxContainer> checkboxContainerPool;

	// Token: 0x04003FDE RID: 16350
	private GameObjectPool checkboxPool;

	// Token: 0x04003FDF RID: 16351
	[SerializeField]
	private GameObject checkboxGroupPrefab;

	// Token: 0x04003FE0 RID: 16352
	[SerializeField]
	private GameObject checkboxPrefab;

	// Token: 0x04003FE1 RID: 16353
	[SerializeField]
	private RectTransform groupParent;

	// Token: 0x04003FE2 RID: 16354
	[SerializeField]
	private RectTransform checkboxParent;

	// Token: 0x04003FE3 RID: 16355
	[SerializeField]
	private LocText descriptionLabel;

	// Token: 0x04003FE4 RID: 16356
	private List<ICheckboxListGroupControl> targets;

	// Token: 0x04003FE5 RID: 16357
	private GameObject currentBuildTarget;

	// Token: 0x04003FE6 RID: 16358
	private int uiRefreshSubHandle = -1;

	// Token: 0x04003FE7 RID: 16359
	private List<CheckboxListGroupSideScreen.CheckboxContainer> activeChecklistGroups = new List<CheckboxListGroupSideScreen.CheckboxContainer>();

	// Token: 0x02001A52 RID: 6738
	public class CheckboxContainer
	{
		// Token: 0x060092C0 RID: 37568 RVA: 0x003184E6 File Offset: 0x003166E6
		public CheckboxContainer(HierarchyReferences container)
		{
			this.container = container;
		}

		// Token: 0x0400773B RID: 30523
		public HierarchyReferences container;

		// Token: 0x0400773C RID: 30524
		public List<HierarchyReferences> checkboxUIItems = new List<HierarchyReferences>();
	}
}
