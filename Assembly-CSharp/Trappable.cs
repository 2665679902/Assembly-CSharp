using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020009AF RID: 2479
[AddComponentMenu("KMonoBehaviour/scripts/Trappable")]
public class Trappable : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06004995 RID: 18837 RVA: 0x0019C3C8 File Offset: 0x0019A5C8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
		this.OnCellChange();
	}

	// Token: 0x06004996 RID: 18838 RVA: 0x0019C3DC File Offset: 0x0019A5DC
	protected override void OnCleanUp()
	{
		this.Unregister();
		base.OnCleanUp();
	}

	// Token: 0x06004997 RID: 18839 RVA: 0x0019C3EC File Offset: 0x0019A5EC
	private void OnCellChange()
	{
		int num = Grid.PosToCell(this);
		GameScenePartitioner.Instance.TriggerEvent(num, GameScenePartitioner.Instance.trapsLayer, this);
	}

	// Token: 0x06004998 RID: 18840 RVA: 0x0019C416 File Offset: 0x0019A616
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.Register();
	}

	// Token: 0x06004999 RID: 18841 RVA: 0x0019C424 File Offset: 0x0019A624
	protected override void OnCmpDisable()
	{
		this.Unregister();
		base.OnCmpDisable();
	}

	// Token: 0x0600499A RID: 18842 RVA: 0x0019C434 File Offset: 0x0019A634
	private void Register()
	{
		if (this.registered)
		{
			return;
		}
		base.Subscribe<Trappable>(856640610, Trappable.OnStoreDelegate);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "Trappable.Register");
		this.registered = true;
	}

	// Token: 0x0600499B RID: 18843 RVA: 0x0019C484 File Offset: 0x0019A684
	private void Unregister()
	{
		if (!this.registered)
		{
			return;
		}
		base.Unsubscribe<Trappable>(856640610, Trappable.OnStoreDelegate, false);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		this.registered = false;
	}

	// Token: 0x0600499C RID: 18844 RVA: 0x0019C4C3 File Offset: 0x0019A6C3
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.BUILDINGEFFECTS.CAPTURE_METHOD_TRAP, UI.BUILDINGEFFECTS.TOOLTIPS.CAPTURE_METHOD_TRAP, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x0600499D RID: 18845 RVA: 0x0019C4EC File Offset: 0x0019A6EC
	public void OnStore(object data)
	{
		Storage storage = data as Storage;
		if (storage ? storage.GetComponent<Trap>() : null)
		{
			base.gameObject.AddTag(GameTags.Trapped);
			return;
		}
		base.gameObject.RemoveTag(GameTags.Trapped);
	}

	// Token: 0x04003061 RID: 12385
	private bool registered;

	// Token: 0x04003062 RID: 12386
	private static readonly EventSystem.IntraObjectHandler<Trappable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Trappable>(delegate(Trappable component, object data)
	{
		component.OnStore(data);
	});
}
