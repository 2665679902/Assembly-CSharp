using System;
using UnityEngine;

// Token: 0x02000A44 RID: 2628
public class SizePulse : MonoBehaviour
{
	// Token: 0x06004FB3 RID: 20403 RVA: 0x001C6004 File Offset: 0x001C4204
	private void Start()
	{
		if (base.GetComponents<SizePulse>().Length > 1)
		{
			UnityEngine.Object.Destroy(this);
		}
		RectTransform rectTransform = (RectTransform)base.transform;
		this.from = rectTransform.localScale;
		this.cur = this.from;
		this.to = this.from * this.multiplier;
	}

	// Token: 0x06004FB4 RID: 20404 RVA: 0x001C6064 File Offset: 0x001C4264
	private void Update()
	{
		float num = (this.updateWhenPaused ? Time.unscaledDeltaTime : Time.deltaTime);
		num *= this.speed;
		SizePulse.State state = this.state;
		if (state != SizePulse.State.Up)
		{
			if (state == SizePulse.State.Down)
			{
				this.cur = Vector2.Lerp(this.cur, this.from, num);
				if ((this.from - this.cur).sqrMagnitude < 0.0001f)
				{
					this.cur = this.from;
					this.state = SizePulse.State.Finished;
					if (this.onComplete != null)
					{
						this.onComplete();
					}
				}
			}
		}
		else
		{
			this.cur = Vector2.Lerp(this.cur, this.to, num);
			if ((this.to - this.cur).sqrMagnitude < 0.0001f)
			{
				this.cur = this.to;
				this.state = SizePulse.State.Down;
			}
		}
		((RectTransform)base.transform).localScale = new Vector3(this.cur.x, this.cur.y, 1f);
	}

	// Token: 0x0400355E RID: 13662
	public System.Action onComplete;

	// Token: 0x0400355F RID: 13663
	public Vector2 from = Vector2.one;

	// Token: 0x04003560 RID: 13664
	public Vector2 to = Vector2.one;

	// Token: 0x04003561 RID: 13665
	public float multiplier = 1.25f;

	// Token: 0x04003562 RID: 13666
	public float speed = 1f;

	// Token: 0x04003563 RID: 13667
	public bool updateWhenPaused;

	// Token: 0x04003564 RID: 13668
	private Vector2 cur;

	// Token: 0x04003565 RID: 13669
	private SizePulse.State state;

	// Token: 0x020018D2 RID: 6354
	private enum State
	{
		// Token: 0x04007278 RID: 29304
		Up,
		// Token: 0x04007279 RID: 29305
		Down,
		// Token: 0x0400727A RID: 29306
		Finished
	}
}
