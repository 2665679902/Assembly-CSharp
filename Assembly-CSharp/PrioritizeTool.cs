using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D4 RID: 2004
public class PrioritizeTool : FilteredDragTool
{
	// Token: 0x06003989 RID: 14729 RVA: 0x0013E4FC File Offset: 0x0013C6FC
	public static void DestroyInstance()
	{
		PrioritizeTool.Instance = null;
	}

	// Token: 0x0600398A RID: 14730 RVA: 0x0013E504 File Offset: 0x0013C704
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.interceptNumberKeysForPriority = true;
		PrioritizeTool.Instance = this;
		this.visualizer = Util.KInstantiate(this.visualizer, null, null);
		this.viewMode = OverlayModes.Priorities.ID;
		Game.Instance.prioritizableRenderer.currentTool = this;
	}

	// Token: 0x0600398B RID: 14731 RVA: 0x0013E554 File Offset: 0x0013C754
	public override string GetFilterLayerFromGameObject(GameObject input)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (input.GetComponent<Diggable>())
		{
			flag = true;
		}
		if (input.GetComponent<Constructable>() || (input.GetComponent<Deconstructable>() && input.GetComponent<Deconstructable>().IsMarkedForDeconstruction()))
		{
			flag2 = true;
		}
		if (input.GetComponent<Clearable>() || input.GetComponent<Moppable>() || input.GetComponent<StorageLocker>())
		{
			flag3 = true;
		}
		if (flag2)
		{
			return ToolParameterMenu.FILTERLAYERS.CONSTRUCTION;
		}
		if (flag)
		{
			return ToolParameterMenu.FILTERLAYERS.DIG;
		}
		if (flag3)
		{
			return ToolParameterMenu.FILTERLAYERS.CLEAN;
		}
		return ToolParameterMenu.FILTERLAYERS.OPERATE;
	}

	// Token: 0x0600398C RID: 14732 RVA: 0x0013E5E8 File Offset: 0x0013C7E8
	protected override void GetDefaultFilters(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		filters.Add(ToolParameterMenu.FILTERLAYERS.ALL, ToolParameterMenu.ToggleState.On);
		filters.Add(ToolParameterMenu.FILTERLAYERS.CONSTRUCTION, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.DIG, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.CLEAN, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.OPERATE, ToolParameterMenu.ToggleState.Off);
	}

	// Token: 0x0600398D RID: 14733 RVA: 0x0013E628 File Offset: 0x0013C828
	private bool TryPrioritizeGameObject(GameObject target, PrioritySetting priority)
	{
		string filterLayerFromGameObject = this.GetFilterLayerFromGameObject(target);
		if (base.IsActiveLayer(filterLayerFromGameObject))
		{
			Prioritizable component = target.GetComponent<Prioritizable>();
			if (component != null && component.showIcon && component.IsPrioritizable())
			{
				component.SetMasterPriority(priority);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600398E RID: 14734 RVA: 0x0013E670 File Offset: 0x0013C870
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		PrioritySetting lastSelectedPriority = ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority();
		int num = 0;
		for (int i = 0; i < 44; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				if (gameObject.GetComponent<Pickupable>())
				{
					ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem;
					while (objectLayerListItem != null)
					{
						GameObject gameObject2 = objectLayerListItem.gameObject;
						objectLayerListItem = objectLayerListItem.nextItem;
						if (!(gameObject2 == null) && !(gameObject2.GetComponent<MinionIdentity>() != null) && this.TryPrioritizeGameObject(gameObject2, lastSelectedPriority))
						{
							num++;
						}
					}
				}
				else if (this.TryPrioritizeGameObject(gameObject, lastSelectedPriority))
				{
					num++;
				}
			}
		}
		if (num > 0)
		{
			PriorityScreen.PlayPriorityConfirmSound(lastSelectedPriority);
		}
	}

	// Token: 0x0600398F RID: 14735 RVA: 0x0013E72C File Offset: 0x0013C92C
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.ShowDiagram(true);
		ToolMenu.Instance.PriorityScreen.Show(true);
		ToolMenu.Instance.PriorityScreen.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
	}

	// Token: 0x06003990 RID: 14736 RVA: 0x0013E788 File Offset: 0x0013C988
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
		ToolMenu.Instance.PriorityScreen.ShowDiagram(false);
		ToolMenu.Instance.PriorityScreen.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x0013E7E4 File Offset: 0x0013C9E4
	public void Update()
	{
		PrioritySetting lastSelectedPriority = ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority();
		int num = 0;
		if (lastSelectedPriority.priority_class >= PriorityScreen.PriorityClass.high)
		{
			num += 9;
		}
		if (lastSelectedPriority.priority_class >= PriorityScreen.PriorityClass.topPriority)
		{
			num = num;
		}
		num += lastSelectedPriority.priority_value;
		Texture2D texture2D = this.cursors[num - 1];
		MeshRenderer componentInChildren = this.visualizer.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren != null)
		{
			componentInChildren.material.mainTexture = texture2D;
		}
	}

	// Token: 0x04002602 RID: 9730
	public GameObject Placer;

	// Token: 0x04002603 RID: 9731
	public static PrioritizeTool Instance;

	// Token: 0x04002604 RID: 9732
	public Texture2D[] cursors;
}
