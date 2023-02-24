using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008B4 RID: 2228
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/AccessControl")]
public class AccessControl : KMonoBehaviour, ISaveLoadable, IGameObjectEffectDescriptor
{
	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x0600400F RID: 16399 RVA: 0x00165A97 File Offset: 0x00163C97
	// (set) Token: 0x06004010 RID: 16400 RVA: 0x00165A9F File Offset: 0x00163C9F
	public AccessControl.Permission DefaultPermission
	{
		get
		{
			return this._defaultPermission;
		}
		set
		{
			this._defaultPermission = value;
			this.SetStatusItem();
			this.SetGridRestrictions(null, this._defaultPermission);
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06004011 RID: 16401 RVA: 0x00165ABB File Offset: 0x00163CBB
	public bool Online
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06004012 RID: 16402 RVA: 0x00165AC0 File Offset: 0x00163CC0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (AccessControl.accessControlActive == null)
		{
			AccessControl.accessControlActive = new StatusItem("accessControlActive", BUILDING.STATUSITEMS.ACCESS_CONTROL.ACTIVE.NAME, BUILDING.STATUSITEMS.ACCESS_CONTROL.ACTIVE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, 129022, true, null);
		}
		base.Subscribe<AccessControl>(279163026, AccessControl.OnControlStateChangedDelegate);
		base.Subscribe<AccessControl>(-905833192, AccessControl.OnCopySettingsDelegate);
	}

	// Token: 0x06004013 RID: 16403 RVA: 0x00165B34 File Offset: 0x00163D34
	private void CheckForBadData()
	{
		List<KeyValuePair<Ref<KPrefabID>, AccessControl.Permission>> list = new List<KeyValuePair<Ref<KPrefabID>, AccessControl.Permission>>();
		foreach (KeyValuePair<Ref<KPrefabID>, AccessControl.Permission> keyValuePair in this.savedPermissions)
		{
			if (keyValuePair.Key.Get() == null)
			{
				list.Add(keyValuePair);
			}
		}
		foreach (KeyValuePair<Ref<KPrefabID>, AccessControl.Permission> keyValuePair2 in list)
		{
			this.savedPermissions.Remove(keyValuePair2);
		}
	}

	// Token: 0x06004014 RID: 16404 RVA: 0x00165BE4 File Offset: 0x00163DE4
	protected override void OnSpawn()
	{
		this.isTeleporter = base.GetComponent<NavTeleporter>() != null;
		base.OnSpawn();
		if (this.savedPermissions.Count > 0)
		{
			this.CheckForBadData();
		}
		if (this.registered)
		{
			this.RegisterInGrid(true);
			this.RestorePermissions();
		}
		ListPool<global::Tuple<MinionAssignablesProxy, AccessControl.Permission>, AccessControl>.PooledList pooledList = ListPool<global::Tuple<MinionAssignablesProxy, AccessControl.Permission>, AccessControl>.Allocate();
		for (int i = this.savedPermissions.Count - 1; i >= 0; i--)
		{
			KPrefabID kprefabID = this.savedPermissions[i].Key.Get();
			if (kprefabID != null)
			{
				MinionIdentity component = kprefabID.GetComponent<MinionIdentity>();
				if (component != null)
				{
					pooledList.Add(new global::Tuple<MinionAssignablesProxy, AccessControl.Permission>(component.assignableProxy.Get(), this.savedPermissions[i].Value));
					this.savedPermissions.RemoveAt(i);
					this.ClearGridRestrictions(kprefabID);
				}
			}
		}
		foreach (global::Tuple<MinionAssignablesProxy, AccessControl.Permission> tuple in pooledList)
		{
			this.SetPermission(tuple.first, tuple.second);
		}
		pooledList.Recycle();
		this.SetStatusItem();
	}

	// Token: 0x06004015 RID: 16405 RVA: 0x00165D20 File Offset: 0x00163F20
	protected override void OnCleanUp()
	{
		this.RegisterInGrid(false);
		base.OnCleanUp();
	}

	// Token: 0x06004016 RID: 16406 RVA: 0x00165D2F File Offset: 0x00163F2F
	private void OnControlStateChanged(object data)
	{
		this.overrideAccess = (Door.ControlState)data;
	}

	// Token: 0x06004017 RID: 16407 RVA: 0x00165D40 File Offset: 0x00163F40
	private void OnCopySettings(object data)
	{
		AccessControl component = ((GameObject)data).GetComponent<AccessControl>();
		if (component != null)
		{
			this.savedPermissions.Clear();
			foreach (KeyValuePair<Ref<KPrefabID>, AccessControl.Permission> keyValuePair in component.savedPermissions)
			{
				if (keyValuePair.Key.Get() != null)
				{
					this.SetPermission(keyValuePair.Key.Get().GetComponent<MinionAssignablesProxy>(), keyValuePair.Value);
				}
			}
			this._defaultPermission = component._defaultPermission;
			this.SetGridRestrictions(null, this.DefaultPermission);
		}
	}

	// Token: 0x06004018 RID: 16408 RVA: 0x00165DFC File Offset: 0x00163FFC
	public void SetRegistered(bool newRegistered)
	{
		if (newRegistered && !this.registered)
		{
			this.RegisterInGrid(true);
			this.RestorePermissions();
			return;
		}
		if (!newRegistered && this.registered)
		{
			this.RegisterInGrid(false);
		}
	}

	// Token: 0x06004019 RID: 16409 RVA: 0x00165E2C File Offset: 0x0016402C
	public void SetPermission(MinionAssignablesProxy key, AccessControl.Permission permission)
	{
		KPrefabID component = key.GetComponent<KPrefabID>();
		if (component == null)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < this.savedPermissions.Count; i++)
		{
			if (this.savedPermissions[i].Key.GetId() == component.InstanceID)
			{
				flag = true;
				KeyValuePair<Ref<KPrefabID>, AccessControl.Permission> keyValuePair = this.savedPermissions[i];
				this.savedPermissions[i] = new KeyValuePair<Ref<KPrefabID>, AccessControl.Permission>(keyValuePair.Key, permission);
				break;
			}
		}
		if (!flag)
		{
			this.savedPermissions.Add(new KeyValuePair<Ref<KPrefabID>, AccessControl.Permission>(new Ref<KPrefabID>(component), permission));
		}
		this.SetStatusItem();
		this.SetGridRestrictions(component, permission);
	}

	// Token: 0x0600401A RID: 16410 RVA: 0x00165ED8 File Offset: 0x001640D8
	private void RestorePermissions()
	{
		this.SetGridRestrictions(null, this.DefaultPermission);
		foreach (KeyValuePair<Ref<KPrefabID>, AccessControl.Permission> keyValuePair in this.savedPermissions)
		{
			KPrefabID kprefabID = keyValuePair.Key.Get();
			if (kprefabID == null)
			{
				DebugUtil.Assert(kprefabID == null, "Tried to set a duplicant-specific access restriction with a null key! This will result in an invisible default permission!");
			}
			this.SetGridRestrictions(keyValuePair.Key.Get(), keyValuePair.Value);
		}
	}

	// Token: 0x0600401B RID: 16411 RVA: 0x00165F74 File Offset: 0x00164174
	private void RegisterInGrid(bool register)
	{
		Building component = base.GetComponent<Building>();
		OccupyArea component2 = base.GetComponent<OccupyArea>();
		if (component2 == null && component == null)
		{
			return;
		}
		if (register)
		{
			Rotatable component3 = base.GetComponent<Rotatable>();
			Grid.Restriction.Orientation orientation;
			if (!this.isTeleporter)
			{
				orientation = ((component3 == null || component3.GetOrientation() == Orientation.Neutral) ? Grid.Restriction.Orientation.Vertical : Grid.Restriction.Orientation.Horizontal);
			}
			else
			{
				orientation = Grid.Restriction.Orientation.SingleCell;
			}
			if (component != null)
			{
				this.registeredBuildingCells = component.PlacementCells;
				int[] array = this.registeredBuildingCells;
				for (int i = 0; i < array.Length; i++)
				{
					Grid.RegisterRestriction(array[i], orientation);
				}
			}
			else
			{
				foreach (CellOffset cellOffset in component2.OccupiedCellsOffsets)
				{
					Grid.RegisterRestriction(Grid.OffsetCell(Grid.PosToCell(component2), cellOffset), orientation);
				}
			}
			if (this.isTeleporter)
			{
				Grid.RegisterRestriction(base.GetComponent<NavTeleporter>().GetCell(), orientation);
			}
		}
		else
		{
			if (component != null)
			{
				if (component.GetMyWorldId() != (int)ClusterManager.INVALID_WORLD_IDX && this.registeredBuildingCells != null)
				{
					int[] array = this.registeredBuildingCells;
					for (int i = 0; i < array.Length; i++)
					{
						Grid.UnregisterRestriction(array[i]);
					}
					this.registeredBuildingCells = null;
				}
			}
			else
			{
				foreach (CellOffset cellOffset2 in component2.OccupiedCellsOffsets)
				{
					Grid.UnregisterRestriction(Grid.OffsetCell(Grid.PosToCell(component2), cellOffset2));
				}
			}
			if (this.isTeleporter)
			{
				int cell = base.GetComponent<NavTeleporter>().GetCell();
				if (cell != Grid.InvalidCell)
				{
					Grid.UnregisterRestriction(cell);
				}
			}
		}
		this.registered = register;
	}

	// Token: 0x0600401C RID: 16412 RVA: 0x00166118 File Offset: 0x00164318
	private void SetGridRestrictions(KPrefabID kpid, AccessControl.Permission permission)
	{
		if (!this.registered || !base.isSpawned)
		{
			return;
		}
		Building component = base.GetComponent<Building>();
		OccupyArea component2 = base.GetComponent<OccupyArea>();
		if (component2 == null && component == null)
		{
			return;
		}
		int num = ((kpid != null) ? kpid.InstanceID : (-1));
		Grid.Restriction.Directions directions = (Grid.Restriction.Directions)0;
		switch (permission)
		{
		case AccessControl.Permission.Both:
			directions = (Grid.Restriction.Directions)0;
			break;
		case AccessControl.Permission.GoLeft:
			directions = Grid.Restriction.Directions.Right;
			break;
		case AccessControl.Permission.GoRight:
			directions = Grid.Restriction.Directions.Left;
			break;
		case AccessControl.Permission.Neither:
			directions = Grid.Restriction.Directions.Left | Grid.Restriction.Directions.Right;
			break;
		}
		if (this.isTeleporter)
		{
			if (directions != (Grid.Restriction.Directions)0)
			{
				directions = Grid.Restriction.Directions.Teleport;
			}
			else
			{
				directions = (Grid.Restriction.Directions)0;
			}
		}
		if (component != null)
		{
			int[] array = this.registeredBuildingCells;
			for (int i = 0; i < array.Length; i++)
			{
				Grid.SetRestriction(array[i], num, directions);
			}
		}
		else
		{
			foreach (CellOffset cellOffset in component2.OccupiedCellsOffsets)
			{
				Grid.SetRestriction(Grid.OffsetCell(Grid.PosToCell(component2), cellOffset), num, directions);
			}
		}
		if (this.isTeleporter)
		{
			Grid.SetRestriction(base.GetComponent<NavTeleporter>().GetCell(), num, directions);
		}
	}

	// Token: 0x0600401D RID: 16413 RVA: 0x0016622C File Offset: 0x0016442C
	private void ClearGridRestrictions(KPrefabID kpid)
	{
		Building component = base.GetComponent<Building>();
		OccupyArea component2 = base.GetComponent<OccupyArea>();
		if (component2 == null && component == null)
		{
			return;
		}
		int num = ((kpid != null) ? kpid.InstanceID : (-1));
		if (component != null)
		{
			int[] array = this.registeredBuildingCells;
			for (int i = 0; i < array.Length; i++)
			{
				Grid.ClearRestriction(array[i], num);
			}
			return;
		}
		foreach (CellOffset cellOffset in component2.OccupiedCellsOffsets)
		{
			Grid.ClearRestriction(Grid.OffsetCell(Grid.PosToCell(component2), cellOffset), num);
		}
	}

	// Token: 0x0600401E RID: 16414 RVA: 0x001662D4 File Offset: 0x001644D4
	public AccessControl.Permission GetPermission(Navigator minion)
	{
		Door.ControlState controlState = this.overrideAccess;
		if (controlState == Door.ControlState.Opened)
		{
			return AccessControl.Permission.Both;
		}
		if (controlState == Door.ControlState.Locked)
		{
			return AccessControl.Permission.Neither;
		}
		return this.GetSetPermission(this.GetKeyForNavigator(minion));
	}

	// Token: 0x0600401F RID: 16415 RVA: 0x00166301 File Offset: 0x00164501
	private MinionAssignablesProxy GetKeyForNavigator(Navigator minion)
	{
		return minion.GetComponent<MinionIdentity>().assignableProxy.Get();
	}

	// Token: 0x06004020 RID: 16416 RVA: 0x00166313 File Offset: 0x00164513
	public AccessControl.Permission GetSetPermission(MinionAssignablesProxy key)
	{
		return this.GetSetPermission(key.GetComponent<KPrefabID>());
	}

	// Token: 0x06004021 RID: 16417 RVA: 0x00166324 File Offset: 0x00164524
	private AccessControl.Permission GetSetPermission(KPrefabID kpid)
	{
		AccessControl.Permission permission = this.DefaultPermission;
		if (kpid != null)
		{
			for (int i = 0; i < this.savedPermissions.Count; i++)
			{
				if (this.savedPermissions[i].Key.GetId() == kpid.InstanceID)
				{
					permission = this.savedPermissions[i].Value;
					break;
				}
			}
		}
		return permission;
	}

	// Token: 0x06004022 RID: 16418 RVA: 0x00166390 File Offset: 0x00164590
	public void ClearPermission(MinionAssignablesProxy key)
	{
		KPrefabID component = key.GetComponent<KPrefabID>();
		if (component != null)
		{
			for (int i = 0; i < this.savedPermissions.Count; i++)
			{
				if (this.savedPermissions[i].Key.GetId() == component.InstanceID)
				{
					this.savedPermissions.RemoveAt(i);
					break;
				}
			}
		}
		this.SetStatusItem();
		this.ClearGridRestrictions(component);
	}

	// Token: 0x06004023 RID: 16419 RVA: 0x00166400 File Offset: 0x00164600
	public bool IsDefaultPermission(MinionAssignablesProxy key)
	{
		bool flag = false;
		KPrefabID component = key.GetComponent<KPrefabID>();
		if (component != null)
		{
			for (int i = 0; i < this.savedPermissions.Count; i++)
			{
				if (this.savedPermissions[i].Key.GetId() == component.InstanceID)
				{
					flag = true;
					break;
				}
			}
		}
		return !flag;
	}

	// Token: 0x06004024 RID: 16420 RVA: 0x00166460 File Offset: 0x00164660
	private void SetStatusItem()
	{
		if (this._defaultPermission != AccessControl.Permission.Both || this.savedPermissions.Count > 0)
		{
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.AccessControl, AccessControl.accessControlActive, null);
			return;
		}
		this.selectable.SetStatusItem(Db.Get().StatusItemCategories.AccessControl, null, null);
	}

	// Token: 0x06004025 RID: 16421 RVA: 0x001664C4 File Offset: 0x001646C4
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.ACCESS_CONTROL, UI.BUILDINGEFFECTS.TOOLTIPS.ACCESS_CONTROL, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x040029FF RID: 10751
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002A00 RID: 10752
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002A01 RID: 10753
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04002A02 RID: 10754
	private bool isTeleporter;

	// Token: 0x04002A03 RID: 10755
	private int[] registeredBuildingCells;

	// Token: 0x04002A04 RID: 10756
	[Serialize]
	private List<KeyValuePair<Ref<KPrefabID>, AccessControl.Permission>> savedPermissions = new List<KeyValuePair<Ref<KPrefabID>, AccessControl.Permission>>();

	// Token: 0x04002A05 RID: 10757
	[Serialize]
	private AccessControl.Permission _defaultPermission;

	// Token: 0x04002A06 RID: 10758
	[Serialize]
	public bool registered = true;

	// Token: 0x04002A07 RID: 10759
	[Serialize]
	public bool controlEnabled;

	// Token: 0x04002A08 RID: 10760
	public Door.ControlState overrideAccess;

	// Token: 0x04002A09 RID: 10761
	private static StatusItem accessControlActive;

	// Token: 0x04002A0A RID: 10762
	private static readonly EventSystem.IntraObjectHandler<AccessControl> OnControlStateChangedDelegate = new EventSystem.IntraObjectHandler<AccessControl>(delegate(AccessControl component, object data)
	{
		component.OnControlStateChanged(data);
	});

	// Token: 0x04002A0B RID: 10763
	private static readonly EventSystem.IntraObjectHandler<AccessControl> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<AccessControl>(delegate(AccessControl component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001688 RID: 5768
	public enum Permission
	{
		// Token: 0x04006A15 RID: 27157
		Both,
		// Token: 0x04006A16 RID: 27158
		GoLeft,
		// Token: 0x04006A17 RID: 27159
		GoRight,
		// Token: 0x04006A18 RID: 27160
		Neither
	}
}
