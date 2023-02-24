using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020006AA RID: 1706
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ConduitDispenser")]
public class ConduitDispenser : KMonoBehaviour, ISaveLoadable, IConduitDispenser
{
	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06002E47 RID: 11847 RVA: 0x000F41FB File Offset: 0x000F23FB
	public Storage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000F4203 File Offset: 0x000F2403
	public ConduitType ConduitType
	{
		get
		{
			return this.conduitType;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000F420B File Offset: 0x000F240B
	public ConduitFlow.ConduitContents ConduitContents
	{
		get
		{
			return this.GetConduitManager().GetContents(this.utilityCell);
		}
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x000F421E File Offset: 0x000F241E
	public void SetConduitData(ConduitType type)
	{
		this.conduitType = type;
	}

	// Token: 0x06002E4B RID: 11851 RVA: 0x000F4228 File Offset: 0x000F2428
	public ConduitFlow GetConduitManager()
	{
		ConduitType conduitType = this.conduitType;
		if (conduitType == ConduitType.Gas)
		{
			return Game.Instance.gasConduitFlow;
		}
		if (conduitType != ConduitType.Liquid)
		{
			return null;
		}
		return Game.Instance.liquidConduitFlow;
	}

	// Token: 0x06002E4C RID: 11852 RVA: 0x000F425D File Offset: 0x000F245D
	private void OnConduitConnectionChanged(object data)
	{
		base.Trigger(-2094018600, this.IsConnected);
	}

	// Token: 0x06002E4D RID: 11853 RVA: 0x000F4278 File Offset: 0x000F2478
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("PlumbingTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Plumbing, true);
		}, null, null);
		ConduitFlow conduitManager = this.GetConduitManager();
		this.utilityCell = this.GetOutputCell(conduitManager.conduitType);
		ScenePartitionerLayer scenePartitionerLayer = GameScenePartitioner.Instance.objectLayers[(this.conduitType == ConduitType.Gas) ? 12 : 16];
		this.partitionerEntry = GameScenePartitioner.Instance.Add("ConduitConsumer.OnSpawn", base.gameObject, this.utilityCell, scenePartitionerLayer, new Action<object>(this.OnConduitConnectionChanged));
		this.GetConduitManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Dispense);
		this.OnConduitConnectionChanged(null);
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x000F4343 File Offset: 0x000F2543
	protected override void OnCleanUp()
	{
		this.GetConduitManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002E4F RID: 11855 RVA: 0x000F4372 File Offset: 0x000F2572
	public void SetOnState(bool onState)
	{
		this.isOn = onState;
	}

	// Token: 0x06002E50 RID: 11856 RVA: 0x000F437B File Offset: 0x000F257B
	private void ConduitUpdate(float dt)
	{
		this.operational.SetFlag(ConduitDispenser.outputConduitFlag, this.IsConnected);
		this.blocked = false;
		if (this.isOn)
		{
			this.Dispense(dt);
		}
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x000F43AC File Offset: 0x000F25AC
	private void Dispense(float dt)
	{
		if (this.operational.IsOperational || this.alwaysDispense)
		{
			if (this.building.Def.CanMove)
			{
				this.utilityCell = this.GetOutputCell(this.GetConduitManager().conduitType);
			}
			PrimaryElement primaryElement = this.FindSuitableElement();
			if (primaryElement != null)
			{
				this.empty = false;
				float num = this.GetConduitManager().AddElement(this.utilityCell, primaryElement.ElementID, primaryElement.Mass, primaryElement.Temperature, primaryElement.DiseaseIdx, primaryElement.DiseaseCount);
				if (num > 0f)
				{
					int num2 = (int)(num / primaryElement.Mass * (float)primaryElement.DiseaseCount);
					primaryElement.ModifyDiseaseCount(-num2, "ConduitDispenser.ConduitUpdate");
					primaryElement.Mass -= num;
					this.storage.Trigger(-1697596308, primaryElement.gameObject);
					return;
				}
				this.blocked = true;
				return;
			}
			else
			{
				this.empty = true;
			}
		}
	}

	// Token: 0x06002E52 RID: 11858 RVA: 0x000F44A0 File Offset: 0x000F26A0
	private PrimaryElement FindSuitableElement()
	{
		List<GameObject> items = this.storage.items;
		int count = items.Count;
		for (int i = 0; i < count; i++)
		{
			int num = (i + this.elementOutputOffset) % count;
			PrimaryElement component = items[num].GetComponent<PrimaryElement>();
			if (component != null && component.Mass > 0f && ((this.conduitType == ConduitType.Liquid) ? component.Element.IsLiquid : component.Element.IsGas) && (this.elementFilter == null || this.elementFilter.Length == 0 || (!this.invertElementFilter && this.IsFilteredElement(component.ElementID)) || (this.invertElementFilter && !this.IsFilteredElement(component.ElementID))))
			{
				this.elementOutputOffset = (this.elementOutputOffset + 1) % count;
				return component;
			}
		}
		return null;
	}

	// Token: 0x06002E53 RID: 11859 RVA: 0x000F4580 File Offset: 0x000F2780
	private bool IsFilteredElement(SimHashes element)
	{
		for (int num = 0; num != this.elementFilter.Length; num++)
		{
			if (this.elementFilter[num] == element)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000F45B0 File Offset: 0x000F27B0
	public bool IsConnected
	{
		get
		{
			GameObject gameObject = Grid.Objects[this.utilityCell, (this.conduitType == ConduitType.Gas) ? 12 : 16];
			return gameObject != null && gameObject.GetComponent<BuildingComplete>() != null;
		}
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x000F45F4 File Offset: 0x000F27F4
	private int GetOutputCell(ConduitType outputConduitType)
	{
		Building component = base.GetComponent<Building>();
		if (this.useSecondaryOutput)
		{
			ISecondaryOutput[] components = base.GetComponents<ISecondaryOutput>();
			foreach (ISecondaryOutput secondaryOutput in components)
			{
				if (secondaryOutput.HasSecondaryConduitType(outputConduitType))
				{
					return Grid.OffsetCell(component.NaturalBuildingCell(), secondaryOutput.GetSecondaryConduitOffset(outputConduitType));
				}
			}
			return Grid.OffsetCell(component.NaturalBuildingCell(), components[0].GetSecondaryConduitOffset(outputConduitType));
		}
		return component.GetUtilityOutputCell();
	}

	// Token: 0x04001BEE RID: 7150
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04001BEF RID: 7151
	[SerializeField]
	public SimHashes[] elementFilter;

	// Token: 0x04001BF0 RID: 7152
	[SerializeField]
	public bool invertElementFilter;

	// Token: 0x04001BF1 RID: 7153
	[SerializeField]
	public bool alwaysDispense;

	// Token: 0x04001BF2 RID: 7154
	[SerializeField]
	public bool isOn = true;

	// Token: 0x04001BF3 RID: 7155
	[SerializeField]
	public bool blocked;

	// Token: 0x04001BF4 RID: 7156
	[SerializeField]
	public bool empty = true;

	// Token: 0x04001BF5 RID: 7157
	[SerializeField]
	public bool useSecondaryOutput;

	// Token: 0x04001BF6 RID: 7158
	private static readonly Operational.Flag outputConduitFlag = new Operational.Flag("output_conduit", Operational.Flag.Type.Functional);

	// Token: 0x04001BF7 RID: 7159
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001BF8 RID: 7160
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04001BF9 RID: 7161
	[MyCmpReq]
	private Building building;

	// Token: 0x04001BFA RID: 7162
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001BFB RID: 7163
	private int utilityCell = -1;

	// Token: 0x04001BFC RID: 7164
	private int elementOutputOffset;
}
