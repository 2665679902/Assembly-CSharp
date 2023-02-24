using System;
using UnityEngine;

// Token: 0x0200040A RID: 1034
[Serializable]
public class AnimEvent
{
	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06001574 RID: 5492 RVA: 0x0006F307 File Offset: 0x0006D507
	// (set) Token: 0x06001575 RID: 5493 RVA: 0x0006F30F File Offset: 0x0006D50F
	[SerializeField]
	public string name { get; private set; }

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06001576 RID: 5494 RVA: 0x0006F318 File Offset: 0x0006D518
	// (set) Token: 0x06001577 RID: 5495 RVA: 0x0006F320 File Offset: 0x0006D520
	[SerializeField]
	public string file { get; private set; }

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06001578 RID: 5496 RVA: 0x0006F329 File Offset: 0x0006D529
	// (set) Token: 0x06001579 RID: 5497 RVA: 0x0006F331 File Offset: 0x0006D531
	[SerializeField]
	public int frame { get; private set; }

	// Token: 0x0600157A RID: 5498 RVA: 0x0006F33A File Offset: 0x0006D53A
	public AnimEvent()
	{
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x0006F344 File Offset: 0x0006D544
	public AnimEvent(string file, string name, int frame)
	{
		this.file = ((file == "") ? null : file);
		if (this.file != null)
		{
			this.fileHash = new KAnimHashedString(this.file);
		}
		this.name = name;
		this.frame = frame;
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x0006F398 File Offset: 0x0006D598
	public void Play(AnimEventManager.EventPlayerData behaviour)
	{
		if (this.IsFilteredOut(behaviour))
		{
			return;
		}
		if (behaviour.previousFrame < behaviour.currentFrame)
		{
			if (behaviour.previousFrame < this.frame && behaviour.currentFrame >= this.frame)
			{
				this.OnPlay(behaviour);
				return;
			}
		}
		else if (behaviour.previousFrame > behaviour.currentFrame && (behaviour.previousFrame < this.frame || this.frame <= behaviour.currentFrame))
		{
			this.OnPlay(behaviour);
		}
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x0006F41A File Offset: 0x0006D61A
	private void DebugAnimEvent(string ev_name, AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x0006F41C File Offset: 0x0006D61C
	public virtual void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x0006F41E File Offset: 0x0006D61E
	public virtual void OnUpdate(AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x0006F420 File Offset: 0x0006D620
	public virtual void Stop(AnimEventManager.EventPlayerData behaviour)
	{
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x0006F422 File Offset: 0x0006D622
	protected bool IsFilteredOut(AnimEventManager.EventPlayerData behaviour)
	{
		return this.file != null && !behaviour.controller.HasAnimationFile(this.fileHash);
	}

	// Token: 0x04000BF6 RID: 3062
	[SerializeField]
	private KAnimHashedString fileHash;

	// Token: 0x04000BF8 RID: 3064
	public bool OnExit;
}
