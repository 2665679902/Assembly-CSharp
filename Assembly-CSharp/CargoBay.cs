using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000939 RID: 2361
[AddComponentMenu("KMonoBehaviour/scripts/CargoBay")]
public class CargoBay : KMonoBehaviour
{
	// Token: 0x06004525 RID: 17701 RVA: 0x00185DA0 File Offset: 0x00183FA0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		base.Subscribe<CargoBay>(-1277991738, CargoBay.OnLaunchDelegate);
		base.Subscribe<CargoBay>(-887025858, CargoBay.OnLandDelegate);
		base.Subscribe<CargoBay>(493375141, CargoBay.OnRefreshUserMenuDelegate);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
		this.OnStorageChange(null);
		base.Subscribe<CargoBay>(-1697596308, CargoBay.OnStorageChangeDelegate);
	}

	// Token: 0x06004526 RID: 17702 RVA: 0x00185E78 File Offset: 0x00184078
	private void OnRefreshUserMenu(object data)
	{
		KIconButtonMenu.ButtonInfo buttonInfo = new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME, delegate
		{
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP, true);
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06004527 RID: 17703 RVA: 0x00185ED4 File Offset: 0x001840D4
	private void OnStorageChange(object data)
	{
		this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.Capacity());
	}

	// Token: 0x06004528 RID: 17704 RVA: 0x00185EF8 File Offset: 0x001840F8
	public void SpawnResources(object data)
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			return;
		}
		ILaunchableRocket component = base.GetComponent<RocketModule>().conditionManager.GetComponent<ILaunchableRocket>();
		if (component.registerType == LaunchableRocketRegisterType.Clustercraft)
		{
			return;
		}
		SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(SpacecraftManager.instance.GetSpacecraftID(component));
		int num = Grid.PosToCell(base.gameObject);
		foreach (KeyValuePair<SimHashes, float> keyValuePair in spacecraftDestination.GetMissionResourceResult(this.storage.RemainingCapacity(), this.reservedResources, this.storageType == CargoBay.CargoType.Solids, this.storageType == CargoBay.CargoType.Liquids, this.storageType == CargoBay.CargoType.Gasses))
		{
			Element element = ElementLoader.FindElementByHash(keyValuePair.Key);
			if (this.storageType == CargoBay.CargoType.Solids && element.IsSolid)
			{
				GameObject gameObject = Scenario.SpawnPrefab(num, 0, 0, element.tag.Name, Grid.SceneLayer.Ore);
				gameObject.GetComponent<PrimaryElement>().Mass = keyValuePair.Value;
				gameObject.GetComponent<PrimaryElement>().Temperature = ElementLoader.FindElementByHash(keyValuePair.Key).defaultValues.temperature;
				gameObject.SetActive(true);
				this.storage.Store(gameObject, false, false, true, false);
			}
			else if (this.storageType == CargoBay.CargoType.Liquids && element.IsLiquid)
			{
				this.storage.AddLiquid(keyValuePair.Key, keyValuePair.Value, ElementLoader.FindElementByHash(keyValuePair.Key).defaultValues.temperature, byte.MaxValue, 0, false, true);
			}
			else if (this.storageType == CargoBay.CargoType.Gasses && element.IsGas)
			{
				this.storage.AddGasChunk(keyValuePair.Key, keyValuePair.Value, ElementLoader.FindElementByHash(keyValuePair.Key).defaultValues.temperature, byte.MaxValue, 0, false, true);
			}
		}
		if (this.storageType == CargoBay.CargoType.Entities)
		{
			foreach (KeyValuePair<Tag, int> keyValuePair2 in spacecraftDestination.GetMissionEntityResult())
			{
				GameObject prefab = Assets.GetPrefab(keyValuePair2.Key);
				if (prefab == null)
				{
					KCrashReporter.Assert(false, "Missing prefab: " + keyValuePair2.Key.Name);
				}
				else
				{
					for (int i = 0; i < keyValuePair2.Value; i++)
					{
						GameObject gameObject2 = Util.KInstantiate(prefab, base.transform.position);
						gameObject2.SetActive(true);
						this.storage.Store(gameObject2, false, false, true, false);
						Baggable component2 = gameObject2.GetComponent<Baggable>();
						if (component2 != null)
						{
							component2.SetWrangled();
						}
					}
				}
			}
		}
	}

	// Token: 0x06004529 RID: 17705 RVA: 0x001861DC File Offset: 0x001843DC
	public void OnLaunch(object data)
	{
		this.ReserveResources();
		ConduitDispenser component = base.GetComponent<ConduitDispenser>();
		if (component != null)
		{
			component.conduitType = ConduitType.None;
		}
	}

	// Token: 0x0600452A RID: 17706 RVA: 0x00186208 File Offset: 0x00184408
	private void ReserveResources()
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			return;
		}
		ILaunchableRocket component = base.GetComponent<RocketModule>().conditionManager.GetComponent<ILaunchableRocket>();
		if (component.registerType == LaunchableRocketRegisterType.Clustercraft)
		{
			return;
		}
		int spacecraftID = SpacecraftManager.instance.GetSpacecraftID(component);
		SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(spacecraftID);
		this.reservedResources = spacecraftDestination.ReserveResources(this);
	}

	// Token: 0x0600452B RID: 17707 RVA: 0x00186260 File Offset: 0x00184460
	public void OnLand(object data)
	{
		this.SpawnResources(data);
		ConduitDispenser component = base.GetComponent<ConduitDispenser>();
		if (component != null)
		{
			CargoBay.CargoType cargoType = this.storageType;
			if (cargoType == CargoBay.CargoType.Liquids)
			{
				component.conduitType = ConduitType.Liquid;
				return;
			}
			if (cargoType == CargoBay.CargoType.Gasses)
			{
				component.conduitType = ConduitType.Gas;
				return;
			}
			component.conduitType = ConduitType.None;
		}
	}

	// Token: 0x04002E16 RID: 11798
	public Storage storage;

	// Token: 0x04002E17 RID: 11799
	private MeterController meter;

	// Token: 0x04002E18 RID: 11800
	[Serialize]
	public float reservedResources;

	// Token: 0x04002E19 RID: 11801
	public CargoBay.CargoType storageType;

	// Token: 0x04002E1A RID: 11802
	public static Dictionary<Element.State, CargoBay.CargoType> ElementStateToCargoTypes = new Dictionary<Element.State, CargoBay.CargoType>
	{
		{
			Element.State.Gas,
			CargoBay.CargoType.Gasses
		},
		{
			Element.State.Liquid,
			CargoBay.CargoType.Liquids
		},
		{
			Element.State.Solid,
			CargoBay.CargoType.Solids
		}
	};

	// Token: 0x04002E1B RID: 11803
	private static readonly EventSystem.IntraObjectHandler<CargoBay> OnLaunchDelegate = new EventSystem.IntraObjectHandler<CargoBay>(delegate(CargoBay component, object data)
	{
		component.OnLaunch(data);
	});

	// Token: 0x04002E1C RID: 11804
	private static readonly EventSystem.IntraObjectHandler<CargoBay> OnLandDelegate = new EventSystem.IntraObjectHandler<CargoBay>(delegate(CargoBay component, object data)
	{
		component.OnLand(data);
	});

	// Token: 0x04002E1D RID: 11805
	private static readonly EventSystem.IntraObjectHandler<CargoBay> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<CargoBay>(delegate(CargoBay component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04002E1E RID: 11806
	private static readonly EventSystem.IntraObjectHandler<CargoBay> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<CargoBay>(delegate(CargoBay component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001724 RID: 5924
	public enum CargoType
	{
		// Token: 0x04006C23 RID: 27683
		Solids,
		// Token: 0x04006C24 RID: 27684
		Liquids,
		// Token: 0x04006C25 RID: 27685
		Gasses,
		// Token: 0x04006C26 RID: 27686
		Entities
	}
}
