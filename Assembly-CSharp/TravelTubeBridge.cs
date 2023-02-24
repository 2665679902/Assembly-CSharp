using System;
using UnityEngine;

// Token: 0x02000662 RID: 1634
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/TravelTubeBridge")]
public class TravelTubeBridge : KMonoBehaviour, ITravelTubePiece
{
	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000E673A File Offset: 0x000E493A
	public Vector3 Position
	{
		get
		{
			return base.transform.GetPosition();
		}
	}

	// Token: 0x06002BE2 RID: 11234 RVA: 0x000E6748 File Offset: 0x000E4948
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Grid.HasTube[Grid.PosToCell(this)] = true;
		Components.ITravelTubePieces.Add(this);
		base.Subscribe<TravelTubeBridge>(774203113, TravelTubeBridge.OnBuildingBrokenDelegate);
		base.Subscribe<TravelTubeBridge>(-1735440190, TravelTubeBridge.OnBuildingFullyRepairedDelegate);
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x000E679C File Offset: 0x000E499C
	protected override void OnCleanUp()
	{
		base.Unsubscribe<TravelTubeBridge>(774203113, TravelTubeBridge.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<TravelTubeBridge>(-1735440190, TravelTubeBridge.OnBuildingFullyRepairedDelegate, false);
		Grid.HasTube[Grid.PosToCell(this)] = false;
		Components.ITravelTubePieces.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x000E67ED File Offset: 0x000E49ED
	private void OnBuildingBroken(object data)
	{
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x000E67EF File Offset: 0x000E49EF
	private void OnBuildingFullyRepaired(object data)
	{
	}

	// Token: 0x040019F7 RID: 6647
	private static readonly EventSystem.IntraObjectHandler<TravelTubeBridge> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<TravelTubeBridge>(delegate(TravelTubeBridge component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x040019F8 RID: 6648
	private static readonly EventSystem.IntraObjectHandler<TravelTubeBridge> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<TravelTubeBridge>(delegate(TravelTubeBridge component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});
}
