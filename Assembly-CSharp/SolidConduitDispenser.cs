using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200092D RID: 2349
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SolidConduitDispenser")]
public class SolidConduitDispenser : KMonoBehaviour, ISaveLoadable, IConduitDispenser
{
	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x060044AC RID: 17580 RVA: 0x001833F9 File Offset: 0x001815F9
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x060044AD RID: 17581 RVA: 0x00183401 File Offset: 0x00181601
	public ConduitType ConduitType
	{
		get
		{
			return ConduitType.Solid;
		}
	}

	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x060044AE RID: 17582 RVA: 0x00183404 File Offset: 0x00181604
	public SolidConduitFlow.ConduitContents ConduitContents
	{
		get
		{
			return this.GetConduitFlow().GetContents(this.utilityCell);
		}
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x060044AF RID: 17583 RVA: 0x00183417 File Offset: 0x00181617
	public bool IsDispensing
	{
		get
		{
			return this.dispensing;
		}
	}

	// Token: 0x060044B0 RID: 17584 RVA: 0x0018341F File Offset: 0x0018161F
	public SolidConduitFlow GetConduitFlow()
	{
		return Game.Instance.solidConduitFlow;
	}

	// Token: 0x060044B1 RID: 17585 RVA: 0x0018342C File Offset: 0x0018162C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.utilityCell = this.GetOutputCell();
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[20];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("SolidConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitFlow().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Dispense);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x060044B2 RID: 17586 RVA: 0x001834A7 File Offset: 0x001816A7
	protected override void OnCleanUp()
	{
		this.GetConduitFlow().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060044B3 RID: 17587 RVA: 0x001834D6 File Offset: 0x001816D6
	private void OnConduitConnectionChanged(object data)
	{
		this.dispensing = this.dispensing && this.IsConnected;
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x060044B4 RID: 17588 RVA: 0x00183508 File Offset: 0x00181708
	private void ConduitUpdate(float dt)
	{
		bool flag = false;
		this.operational.SetFlag(SolidConduitDispenser.outputConduitFlag, this.IsConnected);
		if (this.operational.IsOperational || this.alwaysDispense)
		{
			SolidConduitFlow conduitFlow = this.GetConduitFlow();
			if (conduitFlow.HasConduit(this.utilityCell) && conduitFlow.IsConduitEmpty(this.utilityCell))
			{
				Pickupable pickupable = this.FindSuitableItem();
				if (pickupable)
				{
					if (pickupable.PrimaryElement.Mass > 20f)
					{
						pickupable = pickupable.Take(20f);
					}
					conduitFlow.AddPickupable(this.utilityCell, pickupable);
					flag = true;
				}
			}
		}
		this.storage.storageNetworkID = this.GetConnectedNetworkID();
		this.dispensing = flag;
	}

	// Token: 0x060044B5 RID: 17589 RVA: 0x001835BC File Offset: 0x001817BC
	private bool isSolid(GameObject o)
	{
		PrimaryElement component = o.GetComponent<PrimaryElement>();
		return component == null || component.Element.IsLiquid || component.Element.IsGas;
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x001835F4 File Offset: 0x001817F4
	private Pickupable FindSuitableItem()
	{
		List<GameObject> list = this.storage.items;
		if (this.solidOnly)
		{
			List<GameObject> list2 = new List<GameObject>(list);
			list2.RemoveAll(new Predicate<GameObject>(this.isSolid));
			list = list2;
		}
		if (list.Count < 1)
		{
			return null;
		}
		this.round_robin_index %= list.Count;
		GameObject gameObject = list[this.round_robin_index];
		this.round_robin_index++;
		if (!gameObject)
		{
			return null;
		}
		return gameObject.GetComponent<Pickupable>();
	}

	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x060044B7 RID: 17591 RVA: 0x00183678 File Offset: 0x00181878
	public bool IsConnected
	{
		get
		{
			GameObject gameObject = Grid.Objects[this.utilityCell, 20];
			return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
		}
	}

	// Token: 0x060044B8 RID: 17592 RVA: 0x001836B0 File Offset: 0x001818B0
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

	// Token: 0x060044B9 RID: 17593 RVA: 0x00183704 File Offset: 0x00181904
	private int GetOutputCell()
	{
		Building component = base.GetComponent<Building>();
		if (this.useSecondaryOutput)
		{
			foreach (ISecondaryOutput secondaryOutput in base.GetComponents<ISecondaryOutput>())
			{
				if (secondaryOutput.HasSecondaryConduitType(ConduitType.Solid))
				{
					return Grid.OffsetCell(component.NaturalBuildingCell(), secondaryOutput.GetSecondaryConduitOffset(ConduitType.Solid));
				}
			}
			return Grid.OffsetCell(component.NaturalBuildingCell(), CellOffset.none);
		}
		return component.GetUtilityOutputCell();
	}

	// Token: 0x04002DCB RID: 11723
	[SerializeField]
	public SimHashes[] elementFilter;

	// Token: 0x04002DCC RID: 11724
	[SerializeField]
	public bool invertElementFilter;

	// Token: 0x04002DCD RID: 11725
	[SerializeField]
	public bool alwaysDispense;

	// Token: 0x04002DCE RID: 11726
	[SerializeField]
	public bool useSecondaryOutput;

	// Token: 0x04002DCF RID: 11727
	[SerializeField]
	public bool solidOnly;

	// Token: 0x04002DD0 RID: 11728
	private static readonly Operational.Flag outputConduitFlag = new Operational.Flag("output_conduit", Operational.Flag.Type.Functional);

	// Token: 0x04002DD1 RID: 11729
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002DD2 RID: 11730
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04002DD3 RID: 11731
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002DD4 RID: 11732
	private int utilityCell = -1;

	// Token: 0x04002DD5 RID: 11733
	private bool dispensing;

	// Token: 0x04002DD6 RID: 11734
	private int round_robin_index;
}
