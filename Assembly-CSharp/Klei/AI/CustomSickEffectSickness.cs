using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D81 RID: 3457
	public class CustomSickEffectSickness : Sickness.SicknessComponent
	{
		// Token: 0x06006972 RID: 26994 RVA: 0x002909DE File Offset: 0x0028EBDE
		public CustomSickEffectSickness(string effect_kanim, string effect_anim_name)
		{
			this.kanim = effect_kanim;
			this.animName = effect_anim_name;
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x002909F4 File Offset: 0x0028EBF4
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(this.kanim, go.transform.GetPosition() + new Vector3(0f, 0f, -0.1f), go.transform, true, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play(this.animName, KAnim.PlayMode.Loop, 1f, 0f);
			return kbatchedAnimController;
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x00290A56 File Offset: 0x0028EC56
		public override void OnCure(GameObject go, object instance_data)
		{
			((KAnimControllerBase)instance_data).gameObject.DeleteObject();
		}

		// Token: 0x04004F49 RID: 20297
		private string kanim;

		// Token: 0x04004F4A RID: 20298
		private string animName;
	}
}
