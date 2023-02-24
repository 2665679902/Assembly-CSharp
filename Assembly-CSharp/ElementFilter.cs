using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ElementFilter")]
public class ElementFilter : KMonoBehaviour, ISaveLoadable, ISecondaryOutput
{
	// Token: 0x06002424 RID: 9252 RVA: 0x000C36C3 File Offset: 0x000C18C3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.InitializeStatusItems();
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x000C36D4 File Offset: 0x000C18D4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.inputCell = this.building.GetUtilityInputCell();
		this.outputCell = this.building.GetUtilityOutputCell();
		int num = Grid.PosToCell(base.transform.GetPosition());
		CellOffset rotatedOffset = this.building.GetRotatedOffset(this.portInfo.offset);
		this.filteredCell = Grid.OffsetCell(num, rotatedOffset);
		IUtilityNetworkMgr utilityNetworkMgr = ((this.portInfo.conduitType == ConduitType.Solid) ? SolidConduit.GetFlowManager().networkMgr : Conduit.GetNetworkManager(this.portInfo.conduitType));
		this.itemFilter = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Source, this.filteredCell, base.gameObject);
		utilityNetworkMgr.AddToNetworks(this.filteredCell, this.itemFilter, true);
		if (this.portInfo.conduitType == ConduitType.Gas || this.portInfo.conduitType == ConduitType.Liquid)
		{
			base.GetComponent<ConduitConsumer>().isConsuming = false;
		}
		this.OnFilterChanged(this.filterable.SelectedTag);
		this.filterable.onFilterChanged += this.OnFilterChanged;
		if (this.portInfo.conduitType == ConduitType.Solid)
		{
			SolidConduit.GetFlowManager().AddConduitUpdater(new Action<float>(this.OnConduitTick), ConduitFlowPriority.Default);
		}
		else
		{
			Conduit.GetFlowManager(this.portInfo.conduitType).AddConduitUpdater(new Action<float>(this.OnConduitTick), ConduitFlowPriority.Default);
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, ElementFilter.filterStatusItem, this);
		this.UpdateConduitExistsStatus();
		this.UpdateConduitBlockedStatus();
		ScenePartitionerLayer scenePartitionerLayer = null;
		switch (this.portInfo.conduitType)
		{
		case ConduitType.Gas:
			scenePartitionerLayer = GameScenePartitioner.Instance.gasConduitsLayer;
			break;
		case ConduitType.Liquid:
			scenePartitionerLayer = GameScenePartitioner.Instance.liquidConduitsLayer;
			break;
		case ConduitType.Solid:
			scenePartitionerLayer = GameScenePartitioner.Instance.solidConduitsLayer;
			break;
		}
		if (scenePartitionerLayer != null)
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add("ElementFilterConduitExists", base.gameObject, this.filteredCell, scenePartitionerLayer, delegate(object data)
			{
				this.UpdateConduitExistsStatus();
			});
		}
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x000C38E0 File Offset: 0x000C1AE0
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.portInfo.conduitType).RemoveFromNetworks(this.filteredCell, this.itemFilter, true);
		if (this.portInfo.conduitType == ConduitType.Solid)
		{
			SolidConduit.GetFlowManager().RemoveConduitUpdater(new Action<float>(this.OnConduitTick));
		}
		else
		{
			Conduit.GetFlowManager(this.portInfo.conduitType).RemoveConduitUpdater(new Action<float>(this.OnConduitTick));
		}
		if (this.partitionerEntry.IsValid() && GameScenePartitioner.Instance != null)
		{
			GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		}
		base.OnCleanUp();
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x000C3988 File Offset: 0x000C1B88
	private void OnConduitTick(float dt)
	{
		bool flag = false;
		this.UpdateConduitBlockedStatus();
		if (this.operational.IsOperational)
		{
			if (this.portInfo.conduitType == ConduitType.Gas || this.portInfo.conduitType == ConduitType.Liquid)
			{
				ConduitFlow flowManager = Conduit.GetFlowManager(this.portInfo.conduitType);
				ConduitFlow.ConduitContents contents = flowManager.GetContents(this.inputCell);
				int num = ((contents.element.CreateTag() == this.filterable.SelectedTag) ? this.filteredCell : this.outputCell);
				ConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
				if (contents.mass > 0f && contents2.mass <= 0f)
				{
					flag = true;
					float num2 = flowManager.AddElement(num, contents.element, contents.mass, contents.temperature, contents.diseaseIdx, contents.diseaseCount);
					if (num2 > 0f)
					{
						flowManager.RemoveElement(this.inputCell, num2);
					}
				}
			}
			else
			{
				SolidConduitFlow flowManager2 = SolidConduit.GetFlowManager();
				SolidConduitFlow.ConduitContents contents3 = flowManager2.GetContents(this.inputCell);
				Pickupable pickupable = flowManager2.GetPickupable(contents3.pickupableHandle);
				if (pickupable != null)
				{
					int num3 = ((pickupable.GetComponent<KPrefabID>().PrefabTag == this.filterable.SelectedTag) ? this.filteredCell : this.outputCell);
					SolidConduitFlow.ConduitContents contents4 = flowManager2.GetContents(num3);
					Pickupable pickupable2 = flowManager2.GetPickupable(contents4.pickupableHandle);
					PrimaryElement primaryElement = null;
					if (pickupable2 != null)
					{
						primaryElement = pickupable2.PrimaryElement;
					}
					if (pickupable.PrimaryElement.Mass > 0f && (pickupable2 == null || primaryElement.Mass <= 0f))
					{
						flag = true;
						Pickupable pickupable3 = flowManager2.RemovePickupable(this.inputCell);
						if (pickupable3 != null)
						{
							flowManager2.AddPickupable(num3, pickupable3);
						}
					}
				}
				else
				{
					flowManager2.RemovePickupable(this.inputCell);
				}
			}
		}
		this.operational.SetActive(flag, false);
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x000C3B8C File Offset: 0x000C1D8C
	private void UpdateConduitExistsStatus()
	{
		bool flag = RequireOutputs.IsConnected(this.filteredCell, this.portInfo.conduitType);
		StatusItem statusItem;
		switch (this.portInfo.conduitType)
		{
		case ConduitType.Gas:
			statusItem = Db.Get().BuildingStatusItems.NeedGasOut;
			break;
		case ConduitType.Liquid:
			statusItem = Db.Get().BuildingStatusItems.NeedLiquidOut;
			break;
		case ConduitType.Solid:
			statusItem = Db.Get().BuildingStatusItems.NeedSolidOut;
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
		bool flag2 = this.needsConduitStatusItemGuid != Guid.Empty;
		if (flag == flag2)
		{
			this.needsConduitStatusItemGuid = this.selectable.ToggleStatusItem(statusItem, this.needsConduitStatusItemGuid, !flag, null);
		}
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x000C3C40 File Offset: 0x000C1E40
	private void UpdateConduitBlockedStatus()
	{
		bool flag = Conduit.GetFlowManager(this.portInfo.conduitType).IsConduitEmpty(this.filteredCell);
		StatusItem conduitBlockedMultiples = Db.Get().BuildingStatusItems.ConduitBlockedMultiples;
		bool flag2 = this.conduitBlockedStatusItemGuid != Guid.Empty;
		if (flag == flag2)
		{
			this.conduitBlockedStatusItemGuid = this.selectable.ToggleStatusItem(conduitBlockedMultiples, this.conduitBlockedStatusItemGuid, !flag, null);
		}
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x000C3CAC File Offset: 0x000C1EAC
	private void OnFilterChanged(Tag tag)
	{
		bool flag = !tag.IsValid || tag == GameTags.Void;
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, flag, null);
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x000C3CF0 File Offset: 0x000C1EF0
	private void InitializeStatusItems()
	{
		if (ElementFilter.filterStatusItem == null)
		{
			ElementFilter.filterStatusItem = new StatusItem("Filter", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.LiquidConduits.ID, true, 129022, null);
			ElementFilter.filterStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				ElementFilter elementFilter = (ElementFilter)data;
				if (!elementFilter.filterable.SelectedTag.IsValid || elementFilter.filterable.SelectedTag == GameTags.Void)
				{
					str = string.Format(BUILDINGS.PREFABS.GASFILTER.STATUS_ITEM, BUILDINGS.PREFABS.GASFILTER.ELEMENT_NOT_SPECIFIED);
				}
				else
				{
					str = string.Format(BUILDINGS.PREFABS.GASFILTER.STATUS_ITEM, elementFilter.filterable.SelectedTag.ProperName());
				}
				return str;
			};
			ElementFilter.filterStatusItem.conditionalOverlayCallback = new Func<HashedString, object, bool>(this.ShowInUtilityOverlay);
		}
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x000C3D6C File Offset: 0x000C1F6C
	private bool ShowInUtilityOverlay(HashedString mode, object data)
	{
		bool flag = false;
		switch (((ElementFilter)data).portInfo.conduitType)
		{
		case ConduitType.Gas:
			flag = mode == OverlayModes.GasConduits.ID;
			break;
		case ConduitType.Liquid:
			flag = mode == OverlayModes.LiquidConduits.ID;
			break;
		case ConduitType.Solid:
			flag = mode == OverlayModes.SolidConveyor.ID;
			break;
		}
		return flag;
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x000C3DCB File Offset: 0x000C1FCB
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x000C3DDB File Offset: 0x000C1FDB
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		return this.portInfo.offset;
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x000C3DE8 File Offset: 0x000C1FE8
	public int GetFilteredCell()
	{
		return this.filteredCell;
	}

	// Token: 0x040014C8 RID: 5320
	[SerializeField]
	public ConduitPortInfo portInfo;

	// Token: 0x040014C9 RID: 5321
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040014CA RID: 5322
	[MyCmpReq]
	private Building building;

	// Token: 0x040014CB RID: 5323
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040014CC RID: 5324
	[MyCmpReq]
	private Filterable filterable;

	// Token: 0x040014CD RID: 5325
	private Guid needsConduitStatusItemGuid;

	// Token: 0x040014CE RID: 5326
	private Guid conduitBlockedStatusItemGuid;

	// Token: 0x040014CF RID: 5327
	private int inputCell = -1;

	// Token: 0x040014D0 RID: 5328
	private int outputCell = -1;

	// Token: 0x040014D1 RID: 5329
	private int filteredCell = -1;

	// Token: 0x040014D2 RID: 5330
	private FlowUtilityNetwork.NetworkItem itemFilter;

	// Token: 0x040014D3 RID: 5331
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040014D4 RID: 5332
	private static StatusItem filterStatusItem;
}
