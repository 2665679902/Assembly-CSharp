using System;
using UnityEngine;

// Token: 0x0200092C RID: 2348
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/SolidConduitConsumer")]
public class SolidConduitConsumer : KMonoBehaviour, IConduitConsumer
{
	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x060044A0 RID: 17568 RVA: 0x0018309F File Offset: 0x0018129F
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x060044A1 RID: 17569 RVA: 0x001830A7 File Offset: 0x001812A7
	public ConduitType ConduitType
	{
		get
		{
			return ConduitType.Solid;
		}
	}

	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x060044A2 RID: 17570 RVA: 0x001830AA File Offset: 0x001812AA
	public bool IsConsuming
	{
		get
		{
			return this.consuming;
		}
	}

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x060044A3 RID: 17571 RVA: 0x001830B4 File Offset: 0x001812B4
	public bool IsConnected
	{
		get
		{
			GameObject gameObject = Grid.Objects[this.utilityCell, 20];
			return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
		}
	}

	// Token: 0x060044A4 RID: 17572 RVA: 0x001830EB File Offset: 0x001812EB
	private SolidConduitFlow GetConduitFlow()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x060044A5 RID: 17573 RVA: 0x001830F8 File Offset: 0x001812F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.utilityCell = this.GetInputCell();
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[20];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("SolidConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitFlow().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x060044A6 RID: 17574 RVA: 0x00183172 File Offset: 0x00181372
	protected override void OnCleanUp()
	{
		this.GetConduitFlow().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060044A7 RID: 17575 RVA: 0x001831A1 File Offset: 0x001813A1
	private void OnConduitConnectionChanged(object data)
	{
		this.consuming = this.consuming && this.IsConnected;
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x060044A8 RID: 17576 RVA: 0x001831D0 File Offset: 0x001813D0
	private void ConduitUpdate(float dt)
	{
		bool flag = false;
		SolidConduitFlow conduitFlow = this.GetConduitFlow();
		if (this.IsConnected)
		{
			SolidConduitFlow.ConduitContents contents = conduitFlow.GetContents(this.utilityCell);
			if (contents.pickupableHandle.IsValid() && (this.alwaysConsume || this.operational.IsOperational))
			{
				float num = ((this.capacityTag != GameTags.Any) ? this.storage.GetMassAvailable(this.capacityTag) : this.storage.MassStored());
				float num2 = Mathf.Min(this.storage.capacityKg, this.capacityKG);
				float num3 = Mathf.Max(0f, num2 - num);
				if (num3 > 0f)
				{
					Pickupable pickupable = conduitFlow.GetPickupable(contents.pickupableHandle);
					if (pickupable.PrimaryElement.Mass <= num3 || pickupable.PrimaryElement.Mass > num2)
					{
						Pickupable pickupable2 = conduitFlow.RemovePickupable(this.utilityCell);
						if (pickupable2)
						{
							this.storage.Store(pickupable2.gameObject, true, false, true, false);
							flag = true;
						}
					}
				}
			}
		}
		if (this.storage != null)
		{
			this.storage.storageNetworkID = this.GetConnectedNetworkID();
		}
		this.consuming = flag;
	}

	// Token: 0x060044A9 RID: 17577 RVA: 0x00183310 File Offset: 0x00181510
	private int GetConnectedNetworkID()
	{
		GameObject gameObject = Grid.Objects[this.utilityCell, 20];
		SolidConduit solidConduit = ((gameObject != null) ? gameObject.GetComponent<SolidConduit>() : null);
		UtilityNetwork utilityNetwork = ((solidConduit != null) ? solidConduit.GetNetwork() : null);
		if (utilityNetwork == null)
		{
			return -1;
		}
		return utilityNetwork.id;
	}

	// Token: 0x060044AA RID: 17578 RVA: 0x00183364 File Offset: 0x00181564
	private int GetInputCell()
	{
		if (this.useSecondaryInput)
		{
			foreach (ISecondaryInput secondaryInput in base.GetComponents<ISecondaryInput>())
			{
				if (secondaryInput.HasSecondaryConduitType(ConduitType.Solid))
				{
					return Grid.OffsetCell(this.building.NaturalBuildingCell(), secondaryInput.GetSecondaryConduitOffset(ConduitType.Solid));
				}
			}
			return Grid.OffsetCell(this.building.NaturalBuildingCell(), CellOffset.none);
		}
		return this.building.GetUtilityInputCell();
	}

	// Token: 0x04002DC1 RID: 11713
	[SerializeField]
	public Tag capacityTag = GameTags.Any;

	// Token: 0x04002DC2 RID: 11714
	[SerializeField]
	public float capacityKG = float.PositiveInfinity;

	// Token: 0x04002DC3 RID: 11715
	[SerializeField]
	public bool alwaysConsume;

	// Token: 0x04002DC4 RID: 11716
	[SerializeField]
	public bool useSecondaryInput;

	// Token: 0x04002DC5 RID: 11717
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002DC6 RID: 11718
	[MyCmpReq]
	private Building building;

	// Token: 0x04002DC7 RID: 11719
	[MyCmpGet]
	public Storage storage;

	// Token: 0x04002DC8 RID: 11720
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002DC9 RID: 11721
	private int utilityCell = -1;

	// Token: 0x04002DCA RID: 11722
	private bool consuming;
}
