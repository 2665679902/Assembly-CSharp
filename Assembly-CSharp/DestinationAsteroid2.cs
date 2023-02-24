using System;
using ProcGen;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A89 RID: 2697
[AddComponentMenu("KMonoBehaviour/scripts/DestinationAsteroid2")]
public class DestinationAsteroid2 : KMonoBehaviour
{
	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06005286 RID: 21126 RVA: 0x001DD218 File Offset: 0x001DB418
	// (remove) Token: 0x06005287 RID: 21127 RVA: 0x001DD250 File Offset: 0x001DB450
	public event Action<ColonyDestinationAsteroidBeltData> OnClicked;

	// Token: 0x06005288 RID: 21128 RVA: 0x001DD285 File Offset: 0x001DB485
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.button.onClick += this.OnClickInternal;
	}

	// Token: 0x06005289 RID: 21129 RVA: 0x001DD2A4 File Offset: 0x001DB4A4
	public void SetAsteroid(ColonyDestinationAsteroidBeltData newAsteroidData)
	{
		if (this.asteroidData == null || newAsteroidData.beltPath != this.asteroidData.beltPath)
		{
			this.asteroidData = newAsteroidData;
			ProcGen.World getStartWorld = newAsteroidData.GetStartWorld;
			KAnimFile kanimFile;
			Assets.TryGetAnim(getStartWorld.asteroidIcon.IsNullOrWhiteSpace() ? AsteroidGridEntity.DEFAULT_ASTEROID_ICON_ANIM : getStartWorld.asteroidIcon, out kanimFile);
			if (DlcManager.FeatureClusterSpaceEnabled() && kanimFile != null)
			{
				this.asteroidImage.gameObject.SetActive(false);
				this.animController.AnimFiles = new KAnimFile[] { kanimFile };
				this.animController.initialMode = KAnim.PlayMode.Loop;
				this.animController.initialAnim = "idle_loop";
				this.animController.gameObject.SetActive(true);
				if (this.animController.HasAnimation(this.animController.initialAnim))
				{
					this.animController.Play(this.animController.initialAnim, KAnim.PlayMode.Loop, 1f, 0f);
					return;
				}
			}
			else
			{
				this.animController.gameObject.SetActive(false);
				this.asteroidImage.gameObject.SetActive(true);
				this.asteroidImage.sprite = this.asteroidData.sprite;
			}
		}
	}

	// Token: 0x0600528A RID: 21130 RVA: 0x001DD3EE File Offset: 0x001DB5EE
	private void OnClickInternal()
	{
		DebugUtil.LogArgs(new object[]
		{
			"Clicked asteroid belt",
			this.asteroidData.beltPath
		});
		this.OnClicked(this.asteroidData);
	}

	// Token: 0x040037C2 RID: 14274
	[SerializeField]
	private Image asteroidImage;

	// Token: 0x040037C3 RID: 14275
	[SerializeField]
	private KButton button;

	// Token: 0x040037C4 RID: 14276
	[SerializeField]
	private KBatchedAnimController animController;

	// Token: 0x040037C6 RID: 14278
	private ColonyDestinationAsteroidBeltData asteroidData;
}
