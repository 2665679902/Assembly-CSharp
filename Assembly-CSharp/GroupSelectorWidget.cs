using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C2C RID: 3116
public class GroupSelectorWidget : MonoBehaviour
{
	// Token: 0x06006290 RID: 25232 RVA: 0x0024604F File Offset: 0x0024424F
	public void Initialize(object widget_id, IList<GroupSelectorWidget.ItemData> options, GroupSelectorWidget.ItemCallbacks item_callbacks)
	{
		this.widgetID = widget_id;
		this.options = options;
		this.itemCallbacks = item_callbacks;
		this.addItemButton.onClick += this.OnAddItemClicked;
	}

	// Token: 0x06006291 RID: 25233 RVA: 0x00246080 File Offset: 0x00244280
	public void Reconfigure(IList<int> selected_option_indices)
	{
		this.selectedOptionIndices.Clear();
		this.selectedOptionIndices.AddRange(selected_option_indices);
		this.selectedOptionIndices.Sort();
		this.addItemButton.isInteractable = this.selectedOptionIndices.Count < this.options.Count;
		this.RebuildSelectedVisualizers();
	}

	// Token: 0x06006292 RID: 25234 RVA: 0x002460D8 File Offset: 0x002442D8
	private void OnAddItemClicked()
	{
		if (!this.IsSubPanelOpen())
		{
			if (this.RebuildSubPanelOptions() > 0)
			{
				this.unselectedItemsPanel.GetComponent<GridLayoutGroup>().constraintCount = Mathf.Min(this.numExpectedPanelColumns, this.unselectedItemsPanel.childCount);
				this.unselectedItemsPanel.gameObject.SetActive(true);
				this.unselectedItemsPanel.GetComponent<Selectable>().Select();
				return;
			}
		}
		else
		{
			this.CloseSubPanel();
		}
	}

	// Token: 0x06006293 RID: 25235 RVA: 0x00246144 File Offset: 0x00244344
	private void OnItemAdded(int option_idx)
	{
		if (this.itemCallbacks.onItemAdded != null)
		{
			this.itemCallbacks.onItemAdded(this.widgetID, this.options[option_idx].userData);
			this.RebuildSubPanelOptions();
		}
	}

	// Token: 0x06006294 RID: 25236 RVA: 0x00246181 File Offset: 0x00244381
	private void OnItemRemoved(int option_idx)
	{
		if (this.itemCallbacks.onItemRemoved != null)
		{
			this.itemCallbacks.onItemRemoved(this.widgetID, this.options[option_idx].userData);
		}
	}

	// Token: 0x06006295 RID: 25237 RVA: 0x002461B8 File Offset: 0x002443B8
	private void RebuildSelectedVisualizers()
	{
		foreach (GameObject gameObject in this.selectedVisualizers)
		{
			Util.KDestroyGameObject(gameObject);
		}
		this.selectedVisualizers.Clear();
		foreach (int num in this.selectedOptionIndices)
		{
			GameObject gameObject2 = this.CreateItem(num, new Action<int>(this.OnItemRemoved), this.selectedItemsPanel.gameObject, true);
			this.selectedVisualizers.Add(gameObject2);
		}
	}

	// Token: 0x06006296 RID: 25238 RVA: 0x0024627C File Offset: 0x0024447C
	private GameObject CreateItem(int idx, Action<int> on_click, GameObject parent, bool is_selected_item)
	{
		GameObject gameObject = Util.KInstantiateUI(this.itemTemplate, parent, true);
		KButton component = gameObject.GetComponent<KButton>();
		component.onClick += delegate
		{
			on_click(idx);
		};
		component.fgImage.sprite = this.options[idx].sprite;
		if (parent == this.selectedItemsPanel.gameObject)
		{
			HierarchyReferences component2 = component.GetComponent<HierarchyReferences>();
			if (component2 != null)
			{
				Component reference = component2.GetReference("CancelImg");
				if (reference != null)
				{
					reference.gameObject.SetActive(true);
				}
			}
		}
		gameObject.GetComponent<ToolTip>().OnToolTip = () => this.itemCallbacks.getItemHoverText(this.widgetID, this.options[idx].userData, is_selected_item);
		return gameObject;
	}

	// Token: 0x06006297 RID: 25239 RVA: 0x0024634E File Offset: 0x0024454E
	public bool IsSubPanelOpen()
	{
		return this.unselectedItemsPanel.gameObject.activeSelf;
	}

	// Token: 0x06006298 RID: 25240 RVA: 0x00246360 File Offset: 0x00244560
	public void CloseSubPanel()
	{
		this.ClearSubPanelOptions();
		this.unselectedItemsPanel.gameObject.SetActive(false);
	}

	// Token: 0x06006299 RID: 25241 RVA: 0x0024637C File Offset: 0x0024457C
	private void ClearSubPanelOptions()
	{
		foreach (object obj in this.unselectedItemsPanel.transform)
		{
			Util.KDestroyGameObject(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0600629A RID: 25242 RVA: 0x002463DC File Offset: 0x002445DC
	private int RebuildSubPanelOptions()
	{
		IList<int> list = this.itemCallbacks.getSubPanelDisplayIndices(this.widgetID);
		if (list.Count > 0)
		{
			this.ClearSubPanelOptions();
			using (IEnumerator<int> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					if (!this.selectedOptionIndices.Contains(num))
					{
						this.CreateItem(num, new Action<int>(this.OnItemAdded), this.unselectedItemsPanel.gameObject, false);
					}
				}
				goto IL_7E;
			}
		}
		this.CloseSubPanel();
		IL_7E:
		return list.Count;
	}

	// Token: 0x04004449 RID: 17481
	[SerializeField]
	private GameObject itemTemplate;

	// Token: 0x0400444A RID: 17482
	[SerializeField]
	private RectTransform selectedItemsPanel;

	// Token: 0x0400444B RID: 17483
	[SerializeField]
	private RectTransform unselectedItemsPanel;

	// Token: 0x0400444C RID: 17484
	[SerializeField]
	private KButton addItemButton;

	// Token: 0x0400444D RID: 17485
	[SerializeField]
	private int numExpectedPanelColumns = 3;

	// Token: 0x0400444E RID: 17486
	private object widgetID;

	// Token: 0x0400444F RID: 17487
	private GroupSelectorWidget.ItemCallbacks itemCallbacks;

	// Token: 0x04004450 RID: 17488
	private IList<GroupSelectorWidget.ItemData> options;

	// Token: 0x04004451 RID: 17489
	private List<int> selectedOptionIndices = new List<int>();

	// Token: 0x04004452 RID: 17490
	private List<GameObject> selectedVisualizers = new List<GameObject>();

	// Token: 0x02001ABA RID: 6842
	[Serializable]
	public struct ItemData
	{
		// Token: 0x060093FC RID: 37884 RVA: 0x0031AFC5 File Offset: 0x003191C5
		public ItemData(Sprite sprite, object user_data)
		{
			this.sprite = sprite;
			this.userData = user_data;
		}

		// Token: 0x04007888 RID: 30856
		public Sprite sprite;

		// Token: 0x04007889 RID: 30857
		public object userData;
	}

	// Token: 0x02001ABB RID: 6843
	public struct ItemCallbacks
	{
		// Token: 0x0400788A RID: 30858
		public Func<object, IList<int>> getSubPanelDisplayIndices;

		// Token: 0x0400788B RID: 30859
		public Action<object, object> onItemAdded;

		// Token: 0x0400788C RID: 30860
		public Action<object, object> onItemRemoved;

		// Token: 0x0400788D RID: 30861
		public Func<object, object, bool, string> getItemHoverText;
	}
}
