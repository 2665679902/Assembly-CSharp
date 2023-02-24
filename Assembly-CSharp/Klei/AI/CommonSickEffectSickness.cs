using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D80 RID: 3456
	public class CommonSickEffectSickness : Sickness.SicknessComponent
	{
		// Token: 0x0600696F RID: 26991 RVA: 0x00290964 File Offset: 0x0028EB64
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("contaminated_crew_fx_kanim", go.transform.GetPosition() + new Vector3(0f, 0f, -0.1f), go.transform, true, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play("fx_loop", KAnim.PlayMode.Loop, 1f, 0f);
			return kbatchedAnimController;
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x002909C4 File Offset: 0x0028EBC4
		public override void OnCure(GameObject go, object instance_data)
		{
			((KAnimControllerBase)instance_data).gameObject.DeleteObject();
		}
	}
}
