using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006BB RID: 1723
public class Staterpillar : KMonoBehaviour
{
	// Token: 0x06002EE4 RID: 12004 RVA: 0x000F83E0 File Offset: 0x000F65E0
	protected override void OnPrefabInit()
	{
		this.dummyElement = new List<Tag> { SimHashes.Unobtanium.CreateTag() };
		this.connectorDef = Assets.GetBuildingDef(this.connectorDefId);
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x000F840E File Offset: 0x000F660E
	public void SpawnConnectorBuilding(int targetCell)
	{
		if (this.conduitLayer == ObjectLayer.Wire)
		{
			this.SpawnGenerator(targetCell);
			return;
		}
		this.SpawnConduitConnector(targetCell);
	}

	// Token: 0x06002EE6 RID: 12006 RVA: 0x000F842C File Offset: 0x000F662C
	public void DestroyOrphanedConnectorBuilding()
	{
		KPrefabID building = this.GetConnectorBuilding();
		if (building != null)
		{
			this.connectorRef.Set(null);
			this.cachedGenerator = null;
			this.cachedConduitDispenser = null;
			GameScheduler.Instance.ScheduleNextFrame("Destroy Staterpillar Connector building", delegate(object o)
			{
				if (building != null)
				{
					Util.KDestroyGameObject(building.gameObject);
				}
			}, null, null);
		}
	}

	// Token: 0x06002EE7 RID: 12007 RVA: 0x000F8491 File Offset: 0x000F6691
	public void EnableConnector()
	{
		if (this.conduitLayer == ObjectLayer.Wire)
		{
			this.EnableGenerator();
			return;
		}
		this.EnableConduitConnector();
	}

	// Token: 0x06002EE8 RID: 12008 RVA: 0x000F84AA File Offset: 0x000F66AA
	public bool IsConnectorBuildingSpawned()
	{
		return this.GetConnectorBuilding() != null;
	}

	// Token: 0x06002EE9 RID: 12009 RVA: 0x000F84B8 File Offset: 0x000F66B8
	public bool IsConnected()
	{
		if (this.conduitLayer == ObjectLayer.Wire)
		{
			return this.GetGenerator().CircuitID != ushort.MaxValue;
		}
		return this.GetConduitDispenser().IsConnected;
	}

	// Token: 0x06002EEA RID: 12010 RVA: 0x000F84E5 File Offset: 0x000F66E5
	public KPrefabID GetConnectorBuilding()
	{
		return this.connectorRef.Get();
	}

	// Token: 0x06002EEB RID: 12011 RVA: 0x000F84F4 File Offset: 0x000F66F4
	private void SpawnConduitConnector(int targetCell)
	{
		if (this.GetConduitDispenser() == null)
		{
			GameObject gameObject = this.connectorDef.Build(targetCell, Orientation.R180, null, this.dummyElement, base.gameObject.GetComponent<PrimaryElement>().Temperature, true, -1f);
			this.connectorRef = new Ref<KPrefabID>(gameObject.GetComponent<KPrefabID>());
			gameObject.SetActive(true);
			gameObject.GetComponent<BuildingCellVisualizer>().enabled = false;
		}
	}

	// Token: 0x06002EEC RID: 12012 RVA: 0x000F855E File Offset: 0x000F675E
	private void EnableConduitConnector()
	{
		ConduitDispenser conduitDispenser = this.GetConduitDispenser();
		conduitDispenser.GetComponent<BuildingCellVisualizer>().enabled = true;
		conduitDispenser.storage = base.GetComponent<Storage>();
		conduitDispenser.SetOnState(true);
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x000F8584 File Offset: 0x000F6784
	public ConduitDispenser GetConduitDispenser()
	{
		if (this.cachedConduitDispenser == null)
		{
			KPrefabID kprefabID = this.connectorRef.Get();
			if (kprefabID != null)
			{
				this.cachedConduitDispenser = kprefabID.GetComponent<ConduitDispenser>();
			}
		}
		return this.cachedConduitDispenser;
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x000F85C8 File Offset: 0x000F67C8
	private void DestroyOrphanedConduitDispenserBuilding()
	{
		ConduitDispenser dispenser = this.GetConduitDispenser();
		if (dispenser != null)
		{
			this.connectorRef.Set(null);
			GameScheduler.Instance.ScheduleNextFrame("Destroy Staterpillar Dispenser", delegate(object o)
			{
				if (dispenser != null)
				{
					Util.KDestroyGameObject(dispenser.gameObject);
				}
			}, null, null);
		}
	}

	// Token: 0x06002EEF RID: 12015 RVA: 0x000F8620 File Offset: 0x000F6820
	private void SpawnGenerator(int targetCell)
	{
		StaterpillarGenerator generator = this.GetGenerator();
		GameObject gameObject = null;
		if (generator != null)
		{
			gameObject = generator.gameObject;
		}
		if (!gameObject)
		{
			gameObject = this.connectorDef.Build(targetCell, Orientation.R180, null, this.dummyElement, base.gameObject.GetComponent<PrimaryElement>().Temperature, true, -1f);
			StaterpillarGenerator component = gameObject.GetComponent<StaterpillarGenerator>();
			component.parent = new Ref<Staterpillar>(this);
			this.connectorRef = new Ref<KPrefabID>(component.GetComponent<KPrefabID>());
			gameObject.SetActive(true);
			gameObject.GetComponent<BuildingCellVisualizer>().enabled = false;
			component.enabled = false;
		}
		Attributes attributes = gameObject.gameObject.GetAttributes();
		bool flag = base.gameObject.GetSMI<WildnessMonitor.Instance>().wildness.value > 0f;
		if (flag)
		{
			attributes.Add(this.wildMod);
		}
		bool flag2 = base.gameObject.GetComponent<Effects>().HasEffect("Unhappy");
		CreatureCalorieMonitor.Instance smi = base.gameObject.GetSMI<CreatureCalorieMonitor.Instance>();
		if (smi.IsHungry() || flag2)
		{
			float calories0to = smi.GetCalories0to1();
			float num = 1f;
			if (calories0to <= 0f)
			{
				num = (flag ? 0.1f : 0.025f);
			}
			else if (calories0to <= 0.3f)
			{
				num = 0.5f;
			}
			else if (calories0to <= 0.5f)
			{
				num = 0.75f;
			}
			if (num < 1f)
			{
				float num2;
				if (flag)
				{
					num2 = Mathf.Lerp(0f, 25f, 1f - num);
				}
				else
				{
					num2 = (1f - num) * 100f;
				}
				AttributeModifier attributeModifier = new AttributeModifier(Db.Get().Attributes.GeneratorOutput.Id, -num2, BUILDINGS.PREFABS.STATERPILLARGENERATOR.MODIFIERS.HUNGRY, false, false, true);
				attributes.Add(attributeModifier);
			}
		}
	}

	// Token: 0x06002EF0 RID: 12016 RVA: 0x000F87DE File Offset: 0x000F69DE
	private void EnableGenerator()
	{
		StaterpillarGenerator generator = this.GetGenerator();
		generator.enabled = true;
		generator.GetComponent<BuildingCellVisualizer>().enabled = true;
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x000F87F8 File Offset: 0x000F69F8
	public StaterpillarGenerator GetGenerator()
	{
		if (this.cachedGenerator == null)
		{
			KPrefabID kprefabID = this.connectorRef.Get();
			if (kprefabID != null)
			{
				this.cachedGenerator = kprefabID.GetComponent<StaterpillarGenerator>();
			}
		}
		return this.cachedGenerator;
	}

	// Token: 0x04001C39 RID: 7225
	public ObjectLayer conduitLayer;

	// Token: 0x04001C3A RID: 7226
	public string connectorDefId;

	// Token: 0x04001C3B RID: 7227
	private IList<Tag> dummyElement;

	// Token: 0x04001C3C RID: 7228
	private BuildingDef connectorDef;

	// Token: 0x04001C3D RID: 7229
	[Serialize]
	private Ref<KPrefabID> connectorRef = new Ref<KPrefabID>();

	// Token: 0x04001C3E RID: 7230
	private AttributeModifier wildMod = new AttributeModifier(Db.Get().Attributes.GeneratorOutput.Id, -75f, BUILDINGS.PREFABS.STATERPILLARGENERATOR.MODIFIERS.WILD, false, false, true);

	// Token: 0x04001C3F RID: 7231
	private ConduitDispenser cachedConduitDispenser;

	// Token: 0x04001C40 RID: 7232
	private StaterpillarGenerator cachedGenerator;
}
