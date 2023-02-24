using System;
using UnityEngine;

// Token: 0x020004C9 RID: 1225
public class Sculpture : Artable
{
	// Token: 0x06001C6F RID: 7279 RVA: 0x00097AFB File Offset: 0x00095CFB
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (Sculpture.sculptureOverrides == null)
		{
			Sculpture.sculptureOverrides = new KAnimFile[] { Assets.GetAnim("anim_interacts_sculpture_kanim") };
		}
		this.overrideAnims = Sculpture.sculptureOverrides;
		this.synchronizeAnims = false;
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x00097B3C File Offset: 0x00095D3C
	public override void SetStage(string stage_id, bool skip_effect)
	{
		base.SetStage(stage_id, skip_effect);
		if (!skip_effect && base.CurrentStage != "Default")
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("sculpture_fx_kanim", base.transform.GetPosition(), base.transform, false, Grid.SceneLayer.Front, false);
			kbatchedAnimController.destroyOnAnimComplete = true;
			kbatchedAnimController.transform.SetLocalPosition(Vector3.zero);
			kbatchedAnimController.Play("poof", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x04001004 RID: 4100
	private static KAnimFile[] sculptureOverrides;
}
