using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A9E RID: 2718
public class FrontEndBackground : UIDupeRandomizer
{
	// Token: 0x06005348 RID: 21320 RVA: 0x001E37BC File Offset: 0x001E19BC
	protected override void Start()
	{
		this.tuning = TuningData<FrontEndBackground.Tuning>.Get();
		base.Start();
		for (int i = 0; i < this.anims.Length; i++)
		{
			int minionIndex = i;
			KBatchedAnimController kbatchedAnimController = this.anims[i].minions[0];
			if (kbatchedAnimController.gameObject.activeInHierarchy)
			{
				kbatchedAnimController.onAnimComplete += delegate(HashedString name)
				{
					this.WaitForABit(minionIndex, name);
				};
				this.WaitForABit(i, HashedString.Invalid);
			}
		}
		this.dreckoController = base.transform.GetChild(0).Find("startmenu_drecko").GetComponent<KBatchedAnimController>();
		if (this.dreckoController.gameObject.activeInHierarchy)
		{
			this.dreckoController.enabled = false;
			this.nextDreckoTime = UnityEngine.Random.Range(this.tuning.minFirstDreckoInterval, this.tuning.maxFirstDreckoInterval) + Time.unscaledTime;
		}
	}

	// Token: 0x06005349 RID: 21321 RVA: 0x001E38AA File Offset: 0x001E1AAA
	protected override void Update()
	{
		base.Update();
		this.UpdateDrecko();
	}

	// Token: 0x0600534A RID: 21322 RVA: 0x001E38B8 File Offset: 0x001E1AB8
	private void UpdateDrecko()
	{
		if (this.dreckoController.gameObject.activeInHierarchy && Time.unscaledTime > this.nextDreckoTime)
		{
			this.dreckoController.enabled = true;
			this.dreckoController.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
			this.nextDreckoTime = UnityEngine.Random.Range(this.tuning.minDreckoInterval, this.tuning.maxDreckoInterval) + Time.unscaledTime;
		}
	}

	// Token: 0x0600534B RID: 21323 RVA: 0x001E3937 File Offset: 0x001E1B37
	private void WaitForABit(int minion_idx, HashedString name)
	{
		base.StartCoroutine(this.WaitForTime(minion_idx));
	}

	// Token: 0x0600534C RID: 21324 RVA: 0x001E3947 File Offset: 0x001E1B47
	private IEnumerator WaitForTime(int minion_idx)
	{
		this.anims[minion_idx].lastWaitTime = UnityEngine.Random.Range(this.anims[minion_idx].minSecondsBetweenAction, this.anims[minion_idx].maxSecondsBetweenAction);
		yield return new WaitForSecondsRealtime(this.anims[minion_idx].lastWaitTime);
		base.GetNewBody(minion_idx);
		using (List<KBatchedAnimController>.Enumerator enumerator = this.anims[minion_idx].minions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KBatchedAnimController kbatchedAnimController = enumerator.Current;
				kbatchedAnimController.ClearQueue();
				kbatchedAnimController.Play(this.anims[minion_idx].anim_name, KAnim.PlayMode.Once, 1f, 0f);
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x04003874 RID: 14452
	private KBatchedAnimController dreckoController;

	// Token: 0x04003875 RID: 14453
	private float nextDreckoTime;

	// Token: 0x04003876 RID: 14454
	private FrontEndBackground.Tuning tuning;

	// Token: 0x02001924 RID: 6436
	public class Tuning : TuningData<FrontEndBackground.Tuning>
	{
		// Token: 0x04007371 RID: 29553
		public float minDreckoInterval;

		// Token: 0x04007372 RID: 29554
		public float maxDreckoInterval;

		// Token: 0x04007373 RID: 29555
		public float minFirstDreckoInterval;

		// Token: 0x04007374 RID: 29556
		public float maxFirstDreckoInterval;
	}
}
