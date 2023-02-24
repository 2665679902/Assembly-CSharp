using System;
using FMODUnity;
using UnityEngine;

// Token: 0x020004A2 RID: 1186
[AddComponentMenu("KMonoBehaviour/scripts/MiningSounds")]
public class MiningSounds : KMonoBehaviour
{
	// Token: 0x06001AA7 RID: 6823 RVA: 0x0008E74B File Offset: 0x0008C94B
	protected override void OnPrefabInit()
	{
		base.Subscribe<MiningSounds>(-1762453998, MiningSounds.OnStartMiningSoundDelegate);
		base.Subscribe<MiningSounds>(939543986, MiningSounds.OnStopMiningSoundDelegate);
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x0008E770 File Offset: 0x0008C970
	private void OnStartMiningSound(object data)
	{
		if (this.miningSound == null)
		{
			Element element = data as Element;
			if (element != null)
			{
				string text = element.substance.GetMiningSound();
				if (text == null || text == "")
				{
					return;
				}
				text = "Mine_" + text;
				string sound = GlobalAssets.GetSound(text, false);
				this.miningSoundEvent = RuntimeManager.PathToEventReference(sound);
				if (!this.miningSoundEvent.IsNull)
				{
					this.loopingSounds.StartSound(this.miningSoundEvent);
				}
			}
		}
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x0008E7F1 File Offset: 0x0008C9F1
	private void OnStopMiningSound(object data)
	{
		if (!this.miningSoundEvent.IsNull)
		{
			this.loopingSounds.StopSound(this.miningSoundEvent);
			this.miningSound = null;
		}
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x0008E818 File Offset: 0x0008CA18
	public void SetPercentComplete(float progress)
	{
		if (!this.miningSoundEvent.IsNull)
		{
			this.loopingSounds.SetParameter(this.miningSoundEvent, MiningSounds.HASH_PERCENTCOMPLETE, progress);
		}
	}

	// Token: 0x04000EC8 RID: 3784
	private static HashedString HASH_PERCENTCOMPLETE = "percentComplete";

	// Token: 0x04000EC9 RID: 3785
	[MyCmpGet]
	private LoopingSounds loopingSounds;

	// Token: 0x04000ECA RID: 3786
	private FMODAsset miningSound;

	// Token: 0x04000ECB RID: 3787
	private EventReference miningSoundEvent;

	// Token: 0x04000ECC RID: 3788
	private static readonly EventSystem.IntraObjectHandler<MiningSounds> OnStartMiningSoundDelegate = new EventSystem.IntraObjectHandler<MiningSounds>(delegate(MiningSounds component, object data)
	{
		component.OnStartMiningSound(data);
	});

	// Token: 0x04000ECD RID: 3789
	private static readonly EventSystem.IntraObjectHandler<MiningSounds> OnStopMiningSoundDelegate = new EventSystem.IntraObjectHandler<MiningSounds>(delegate(MiningSounds component, object data)
	{
		component.OnStopMiningSound(data);
	});
}
