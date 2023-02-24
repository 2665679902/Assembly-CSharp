using System;
using UnityEngine;

// Token: 0x02000C09 RID: 3081
public class SwapUIAnimationController : MonoBehaviour
{
	// Token: 0x060061AB RID: 25003 RVA: 0x00241190 File Offset: 0x0023F390
	public void SetState(bool Primary)
	{
		this.AnimationControllerObject_Primary.SetActive(Primary);
		if (!Primary)
		{
			this.AnimationControllerObject_Alternate.GetComponent<KAnimControllerBase>().TintColour = new Color(1f, 1f, 1f, 0.5f);
			this.AnimationControllerObject_Primary.GetComponent<KAnimControllerBase>().TintColour = Color.clear;
		}
		this.AnimationControllerObject_Alternate.SetActive(!Primary);
		if (Primary)
		{
			this.AnimationControllerObject_Primary.GetComponent<KAnimControllerBase>().TintColour = Color.white;
			this.AnimationControllerObject_Alternate.GetComponent<KAnimControllerBase>().TintColour = Color.clear;
		}
	}

	// Token: 0x04004387 RID: 17287
	public GameObject AnimationControllerObject_Primary;

	// Token: 0x04004388 RID: 17288
	public GameObject AnimationControllerObject_Alternate;
}
