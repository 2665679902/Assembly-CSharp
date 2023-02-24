using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020006FD RID: 1789
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/DecorProvider")]
public class DecorProvider : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x060030E0 RID: 12512 RVA: 0x0010366C File Offset: 0x0010186C
	private void AddDecor()
	{
		this.currDecor = 0f;
		if (this.decor != null)
		{
			this.currDecor = this.decor.GetTotalValue();
		}
		if (base.gameObject.HasTag(GameTags.Stored))
		{
			this.currDecor = 0f;
		}
		int num = Grid.PosToCell(base.gameObject);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		if (!Grid.Transparent[num] && Grid.Solid[num] && this.simCellOccupier == null)
		{
			this.currDecor = 0f;
		}
		if (this.currDecor == 0f)
		{
			return;
		}
		this.cellCount = 0;
		int num2 = 5;
		if (this.decorRadius != null)
		{
			num2 = (int)this.decorRadius.GetTotalValue();
		}
		Orientation orientation = Orientation.Neutral;
		if (this.rotatable)
		{
			orientation = this.rotatable.GetOrientation();
		}
		Extents extents = this.occupyArea.GetExtents(orientation);
		extents.x = Mathf.Max(extents.x - num2, 0);
		extents.y = Mathf.Max(extents.y - num2, 0);
		extents.width = Mathf.Min(extents.width + num2 * 2, Grid.WidthInCells - 1);
		extents.height = Mathf.Min(extents.height + num2 * 2, Grid.HeightInCells - 1);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("DecorProvider.SplatCollectDecorProviders", base.gameObject, extents, GameScenePartitioner.Instance.decorProviderLayer, this.onCollectDecorProvidersCallback);
		this.solidChangedPartitionerEntry = GameScenePartitioner.Instance.Add("DecorProvider.SplatSolidCheck", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, this.refreshPartionerCallback);
		int num3 = extents.x + extents.width;
		int num4 = extents.y + extents.height;
		int num5 = extents.x;
		int num6 = extents.y;
		int num7;
		int num8;
		Grid.CellToXY(num, out num7, out num8);
		num3 = Math.Min(num3, Grid.WidthInCells);
		num4 = Math.Min(num4, Grid.HeightInCells);
		num5 = Math.Max(0, num5);
		num6 = Math.Max(0, num6);
		int num9 = (num3 - num5) * (num4 - num6);
		if (this.cells == null || this.cells.Length != num9)
		{
			this.cells = new int[num9];
		}
		for (int i = num5; i < num3; i++)
		{
			for (int j = num6; j < num4; j++)
			{
				if (Grid.VisibilityTest(num7, num8, i, j, false))
				{
					int num10 = Grid.XYToCell(i, j);
					if (Grid.IsValidCell(num10))
					{
						Grid.Decor[num10] += this.currDecor;
						int[] array = this.cells;
						int num11 = this.cellCount;
						this.cellCount = num11 + 1;
						array[num11] = num10;
					}
				}
			}
		}
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x00103923 File Offset: 0x00101B23
	public void Clear()
	{
		if (this.currDecor == 0f)
		{
			return;
		}
		this.RemoveDecor();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x0010395C File Offset: 0x00101B5C
	private void RemoveDecor()
	{
		if (this.currDecor == 0f)
		{
			return;
		}
		for (int i = 0; i < this.cellCount; i++)
		{
			int num = this.cells[i];
			if (Grid.IsValidCell(num))
			{
				Grid.Decor[num] -= this.currDecor;
			}
		}
	}

	// Token: 0x060030E3 RID: 12515 RVA: 0x001039B0 File Offset: 0x00101BB0
	public void Refresh()
	{
		this.Clear();
		this.AddDecor();
		KPrefabID component = base.GetComponent<KPrefabID>();
		bool flag = component.HasTag(RoomConstraints.ConstraintTags.Decor20);
		bool flag2 = this.decor.GetTotalValue() >= 20f;
		if (flag != flag2)
		{
			if (flag2)
			{
				component.AddTag(RoomConstraints.ConstraintTags.Decor20, false);
			}
			else
			{
				component.RemoveTag(RoomConstraints.ConstraintTags.Decor20);
			}
			int num = Grid.PosToCell(this);
			if (Grid.IsValidCell(num))
			{
				Game.Instance.roomProber.SolidChangedEvent(num, true);
			}
		}
	}

	// Token: 0x060030E4 RID: 12516 RVA: 0x00103A30 File Offset: 0x00101C30
	public float GetDecorForCell(int cell)
	{
		for (int i = 0; i < this.cellCount; i++)
		{
			if (this.cells[i] == cell)
			{
				return this.currDecor;
			}
		}
		return 0f;
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x00103A65 File Offset: 0x00101C65
	public void SetValues(EffectorValues values)
	{
		this.baseDecor = (float)values.amount;
		this.baseRadius = (float)values.radius;
		if (base.IsInitialized())
		{
			this.UpdateBaseDecorModifiers();
		}
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x00103A90 File Offset: 0x00101C90
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.decor = this.GetAttributes().Add(Db.Get().BuildingAttributes.Decor);
		this.decorRadius = this.GetAttributes().Add(Db.Get().BuildingAttributes.DecorRadius);
		this.UpdateBaseDecorModifiers();
	}

	// Token: 0x060030E7 RID: 12519 RVA: 0x00103AEC File Offset: 0x00101CEC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.refreshCallback = new System.Action(this.Refresh);
		this.refreshPartionerCallback = delegate(object data)
		{
			this.Refresh();
		};
		this.onCollectDecorProvidersCallback = new Action<object>(this.OnCollectDecorProviders);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "DecorProvider.OnSpawn");
		AttributeInstance attributeInstance = this.decor;
		attributeInstance.OnDirty = (System.Action)Delegate.Combine(attributeInstance.OnDirty, this.refreshCallback);
		AttributeInstance attributeInstance2 = this.decorRadius;
		attributeInstance2.OnDirty = (System.Action)Delegate.Combine(attributeInstance2.OnDirty, this.refreshCallback);
		this.Refresh();
	}

	// Token: 0x060030E8 RID: 12520 RVA: 0x00103BA0 File Offset: 0x00101DA0
	private void UpdateBaseDecorModifiers()
	{
		Attributes attributes = this.GetAttributes();
		if (this.baseDecorModifier != null)
		{
			attributes.Remove(this.baseDecorModifier);
			attributes.Remove(this.baseDecorRadiusModifier);
			this.baseDecorModifier = null;
			this.baseDecorRadiusModifier = null;
		}
		if (this.baseDecor != 0f)
		{
			this.baseDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, this.baseDecor, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			this.baseDecorRadiusModifier = new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, this.baseRadius, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			attributes.Add(this.baseDecorModifier);
			attributes.Add(this.baseDecorRadiusModifier);
		}
	}

	// Token: 0x060030E9 RID: 12521 RVA: 0x00103C6B File Offset: 0x00101E6B
	private void OnCellChange()
	{
		this.Refresh();
	}

	// Token: 0x060030EA RID: 12522 RVA: 0x00103C73 File Offset: 0x00101E73
	private void OnCollectDecorProviders(object data)
	{
		((List<DecorProvider>)data).Add(this);
	}

	// Token: 0x060030EB RID: 12523 RVA: 0x00103C81 File Offset: 0x00101E81
	public string GetName()
	{
		if (string.IsNullOrEmpty(this.overrideName))
		{
			return base.GetComponent<KSelectable>().GetName();
		}
		return this.overrideName;
	}

	// Token: 0x060030EC RID: 12524 RVA: 0x00103CA4 File Offset: 0x00101EA4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (base.isSpawned)
		{
			AttributeInstance attributeInstance = this.decor;
			attributeInstance.OnDirty = (System.Action)Delegate.Remove(attributeInstance.OnDirty, this.refreshCallback);
			AttributeInstance attributeInstance2 = this.decorRadius;
			attributeInstance2.OnDirty = (System.Action)Delegate.Remove(attributeInstance2.OnDirty, this.refreshCallback);
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		}
		this.Clear();
	}

	// Token: 0x060030ED RID: 12525 RVA: 0x00103D24 File Offset: 0x00101F24
	public List<Descriptor> GetEffectDescriptions()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.decor != null && this.decorRadius != null)
		{
			float totalValue = this.decor.GetTotalValue();
			float totalValue2 = this.decorRadius.GetTotalValue();
			string text = ((this.baseDecor > 0f) ? "produced" : "consumed");
			string text2 = ((this.baseDecor > 0f) ? UI.BUILDINGEFFECTS.TOOLTIPS.DECORPROVIDED : UI.BUILDINGEFFECTS.TOOLTIPS.DECORDECREASED);
			text2 = text2 + "\n\n" + this.decor.GetAttributeValueTooltip();
			string text3 = GameUtil.AddPositiveSign(totalValue.ToString(), totalValue > 0f);
			Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.DECORPROVIDED, text, text3, totalValue2), string.Format(text2, text3, totalValue2), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else if (this.baseDecor != 0f)
		{
			string text4 = ((this.baseDecor >= 0f) ? "produced" : "consumed");
			string text5 = ((this.baseDecor >= 0f) ? UI.BUILDINGEFFECTS.TOOLTIPS.DECORPROVIDED : UI.BUILDINGEFFECTS.TOOLTIPS.DECORDECREASED);
			string text6 = GameUtil.AddPositiveSign(this.baseDecor.ToString(), this.baseDecor > 0f);
			Descriptor descriptor2 = new Descriptor(string.Format(UI.BUILDINGEFFECTS.DECORPROVIDED, text4, text6, this.baseRadius), string.Format(text5, text6, this.baseRadius), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x060030EE RID: 12526 RVA: 0x00103EB9 File Offset: 0x001020B9
	public static int GetLightDecorBonus(int cell)
	{
		if (Grid.LightIntensity[cell] > 0)
		{
			return DECOR.LIT_BONUS;
		}
		return 0;
	}

	// Token: 0x060030EF RID: 12527 RVA: 0x00103ED0 File Offset: 0x001020D0
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.GetEffectDescriptions();
	}

	// Token: 0x04001D7F RID: 7551
	public const string ID = "DecorProvider";

	// Token: 0x04001D80 RID: 7552
	public float baseRadius;

	// Token: 0x04001D81 RID: 7553
	public float baseDecor;

	// Token: 0x04001D82 RID: 7554
	public string overrideName;

	// Token: 0x04001D83 RID: 7555
	public System.Action refreshCallback;

	// Token: 0x04001D84 RID: 7556
	public Action<object> refreshPartionerCallback;

	// Token: 0x04001D85 RID: 7557
	public Action<object> onCollectDecorProvidersCallback;

	// Token: 0x04001D86 RID: 7558
	public AttributeInstance decor;

	// Token: 0x04001D87 RID: 7559
	public AttributeInstance decorRadius;

	// Token: 0x04001D88 RID: 7560
	private AttributeModifier baseDecorModifier;

	// Token: 0x04001D89 RID: 7561
	private AttributeModifier baseDecorRadiusModifier;

	// Token: 0x04001D8A RID: 7562
	[MyCmpReq]
	public OccupyArea occupyArea;

	// Token: 0x04001D8B RID: 7563
	[MyCmpGet]
	public Rotatable rotatable;

	// Token: 0x04001D8C RID: 7564
	[MyCmpGet]
	public SimCellOccupier simCellOccupier;

	// Token: 0x04001D8D RID: 7565
	private int[] cells;

	// Token: 0x04001D8E RID: 7566
	private int cellCount;

	// Token: 0x04001D8F RID: 7567
	public float currDecor;

	// Token: 0x04001D90 RID: 7568
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001D91 RID: 7569
	private HandleVector<int>.Handle solidChangedPartitionerEntry;
}
