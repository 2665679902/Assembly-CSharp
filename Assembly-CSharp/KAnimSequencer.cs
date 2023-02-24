using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000417 RID: 1047
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/KAnimSequencer")]
public class KAnimSequencer : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06001632 RID: 5682 RVA: 0x0007269D File Offset: 0x0007089D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.kbac = base.GetComponent<KBatchedAnimController>();
		this.mb = base.GetComponent<MinionBrain>();
		if (this.autoRun)
		{
			this.PlaySequence();
		}
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000726CB File Offset: 0x000708CB
	public void Reset()
	{
		this.currentIndex = 0;
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000726D4 File Offset: 0x000708D4
	public void PlaySequence()
	{
		if (this.sequence != null && this.sequence.Length != 0)
		{
			if (this.mb != null)
			{
				this.mb.Suspend("AnimSequencer");
			}
			this.kbac.onAnimComplete += this.PlayNext;
			this.PlayNext(null);
		}
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x00072734 File Offset: 0x00070934
	private void PlayNext(HashedString name)
	{
		if (this.sequence.Length > this.currentIndex)
		{
			this.kbac.Play(new HashedString(this.sequence[this.currentIndex].anim), this.sequence[this.currentIndex].mode, this.sequence[this.currentIndex].speed, 0f);
			this.currentIndex++;
			return;
		}
		this.kbac.onAnimComplete -= this.PlayNext;
		if (this.mb != null)
		{
			this.mb.Resume("AnimSequencer");
		}
	}

	// Token: 0x04000C57 RID: 3159
	[Serialize]
	public bool autoRun;

	// Token: 0x04000C58 RID: 3160
	[Serialize]
	public KAnimSequencer.KAnimSequence[] sequence = new KAnimSequencer.KAnimSequence[0];

	// Token: 0x04000C59 RID: 3161
	private int currentIndex;

	// Token: 0x04000C5A RID: 3162
	private KBatchedAnimController kbac;

	// Token: 0x04000C5B RID: 3163
	private MinionBrain mb;

	// Token: 0x02001042 RID: 4162
	[SerializationConfig(MemberSerialization.OptOut)]
	[Serializable]
	public class KAnimSequence
	{
		// Token: 0x040056CE RID: 22222
		public string anim;

		// Token: 0x040056CF RID: 22223
		public float speed = 1f;

		// Token: 0x040056D0 RID: 22224
		public KAnim.PlayMode mode = KAnim.PlayMode.Once;
	}
}
