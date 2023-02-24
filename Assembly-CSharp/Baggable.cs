using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200055A RID: 1370
[AddComponentMenu("KMonoBehaviour/scripts/Baggable")]
public class Baggable : KMonoBehaviour
{
	// Token: 0x060020F7 RID: 8439 RVA: 0x000B39C4 File Offset: 0x000B1BC4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.minionAnimOverride = Assets.GetAnim("anim_restrain_creature_kanim");
		Pickupable pickupable = base.gameObject.AddOrGet<Pickupable>();
		pickupable.workAnims = new HashedString[]
		{
			new HashedString("capture"),
			new HashedString("pickup")
		};
		pickupable.workAnimPlayMode = KAnim.PlayMode.Once;
		pickupable.workingPstComplete = null;
		pickupable.workingPstFailed = null;
		pickupable.overrideAnims = new KAnimFile[] { this.minionAnimOverride };
		pickupable.trackOnPickup = false;
		pickupable.useGunforPickup = this.useGunForPickup;
		pickupable.synchronizeAnims = false;
		pickupable.SetWorkTime(3f);
		if (this.mustStandOntopOfTrapForPickup)
		{
			pickupable.SetOffsets(new CellOffset[]
			{
				default(CellOffset),
				new CellOffset(0, -1)
			});
		}
		base.Subscribe<Baggable>(856640610, Baggable.OnStoreDelegate);
		if (base.transform.parent != null)
		{
			if (base.transform.parent.GetComponent<Trap>() != null)
			{
				base.GetComponent<KBatchedAnimController>().enabled = true;
			}
			if (base.transform.parent.GetComponent<EggIncubator>() != null)
			{
				this.wrangled = true;
			}
		}
		if (this.wrangled)
		{
			this.SetWrangled();
		}
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x000B3B0C File Offset: 0x000B1D0C
	private void OnStore(object data)
	{
		Storage storage = data as Storage;
		if (storage != null || (data != null && (bool)data))
		{
			base.gameObject.AddTag(GameTags.Creatures.Bagged);
			if (storage && storage.IsPrefabID(GameTags.Minion))
			{
				this.SetVisible(false);
				return;
			}
		}
		else
		{
			this.Free();
		}
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x000B3B70 File Offset: 0x000B1D70
	private void SetVisible(bool visible)
	{
		KAnimControllerBase component = base.gameObject.GetComponent<KAnimControllerBase>();
		if (component != null && component.enabled != visible)
		{
			component.enabled = visible;
		}
		KSelectable component2 = base.gameObject.GetComponent<KSelectable>();
		if (component2 != null && component2.enabled != visible)
		{
			component2.enabled = visible;
		}
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000B3BC8 File Offset: 0x000B1DC8
	public void SetWrangled()
	{
		this.wrangled = true;
		Navigator component = base.GetComponent<Navigator>();
		if (component && component.IsValidNavType(NavType.Floor))
		{
			component.SetCurrentNavType(NavType.Floor);
		}
		base.gameObject.AddTag(GameTags.Creatures.Bagged);
		base.GetComponent<KAnimControllerBase>().Play("trussed", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000B3C2B File Offset: 0x000B1E2B
	public void Free()
	{
		base.gameObject.RemoveTag(GameTags.Creatures.Bagged);
		this.wrangled = false;
		this.SetVisible(true);
	}

	// Token: 0x040012F3 RID: 4851
	[SerializeField]
	private KAnimFile minionAnimOverride;

	// Token: 0x040012F4 RID: 4852
	public bool mustStandOntopOfTrapForPickup;

	// Token: 0x040012F5 RID: 4853
	[Serialize]
	public bool wrangled;

	// Token: 0x040012F6 RID: 4854
	public bool useGunForPickup;

	// Token: 0x040012F7 RID: 4855
	private static readonly EventSystem.IntraObjectHandler<Baggable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Baggable>(delegate(Baggable component, object data)
	{
		component.OnStore(data);
	});
}
