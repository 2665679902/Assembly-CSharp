using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C28 RID: 3112
public class ClippyPanel : KScreen
{
	// Token: 0x06006282 RID: 25218 RVA: 0x00245BDC File Offset: 0x00243DDC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06006283 RID: 25219 RVA: 0x00245BE4 File Offset: 0x00243DE4
	protected override void OnActivate()
	{
		base.OnActivate();
		SpeedControlScreen.Instance.Pause(true, false);
		Game.Instance.Trigger(1634669191, null);
	}

	// Token: 0x06006284 RID: 25220 RVA: 0x00245C08 File Offset: 0x00243E08
	public void OnOk()
	{
		SpeedControlScreen.Instance.Unpause(true);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04004437 RID: 17463
	public Text title;

	// Token: 0x04004438 RID: 17464
	public Text detailText;

	// Token: 0x04004439 RID: 17465
	public Text flavorText;

	// Token: 0x0400443A RID: 17466
	public Image topicIcon;

	// Token: 0x0400443B RID: 17467
	private KButton okButton;
}
