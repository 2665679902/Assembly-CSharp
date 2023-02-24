using System;
using UnityEngine;

// Token: 0x02000727 RID: 1831
public class DreamBubble : KMonoBehaviour
{
	// Token: 0x17000398 RID: 920
	// (get) Token: 0x0600321C RID: 12828 RVA: 0x0010C0E6 File Offset: 0x0010A2E6
	// (set) Token: 0x0600321B RID: 12827 RVA: 0x0010C0DD File Offset: 0x0010A2DD
	public bool IsVisible { get; private set; }

	// Token: 0x0600321D RID: 12829 RVA: 0x0010C0EE File Offset: 0x0010A2EE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.dreamBackgroundComponent.SetSymbolVisiblity(this.snapToPivotSymbol, false);
		this.SetVisibility(false);
	}

	// Token: 0x0600321E RID: 12830 RVA: 0x0010C114 File Offset: 0x0010A314
	public void Tick(float dt)
	{
		if (this._currentDream != null && this._currentDream.Icons.Length != 0)
		{
			float num = this._timePassedSinceDreamStarted / this._currentDream.secondPerImage;
			int num2 = Mathf.FloorToInt(num);
			float num3 = num - (float)num2;
			int num4 = (int)Mathf.Repeat((float)Mathf.FloorToInt(num), (float)this._currentDream.Icons.Length);
			if (this.dreamContentComponent.sprite != this._currentDream.Icons[num4])
			{
				this.dreamContentComponent.sprite = this._currentDream.Icons[num4];
			}
			this.dreamContentComponent.rectTransform.localScale = Vector3.one * num3;
			this._color.a = (Mathf.Sin(num3 * 6.2831855f - 1.5707964f) + 1f) * 0.5f;
			this.dreamContentComponent.color = this._color;
			this._timePassedSinceDreamStarted += dt;
		}
	}

	// Token: 0x0600321F RID: 12831 RVA: 0x0010C210 File Offset: 0x0010A410
	public void SetDream(Dream dream)
	{
		this._currentDream = dream;
		this.dreamBackgroundComponent.Stop();
		this.dreamBackgroundComponent.AnimFiles = new KAnimFile[] { Assets.GetAnim(dream.BackgroundAnim) };
		this.dreamContentComponent.color = this._color;
		this.dreamContentComponent.enabled = dream != null && dream.Icons != null && dream.Icons.Length != 0;
		this._timePassedSinceDreamStarted = 0f;
		this._color.a = 0f;
	}

	// Token: 0x06003220 RID: 12832 RVA: 0x0010C2A4 File Offset: 0x0010A4A4
	public void SetVisibility(bool visible)
	{
		this.IsVisible = visible;
		this.dreamBackgroundComponent.SetVisiblity(visible);
		this.dreamContentComponent.gameObject.SetActive(visible);
		if (visible)
		{
			if (this._currentDream != null)
			{
				this.dreamBackgroundComponent.Play("dream_loop", KAnim.PlayMode.Loop, 1f, 0f);
			}
			this.dreamBubbleBorderKanim.Play("dream_bubble_loop", KAnim.PlayMode.Loop, 1f, 0f);
			this.maskKanim.Play("dream_bubble_mask", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		this.dreamBackgroundComponent.Stop();
		this.maskKanim.Stop();
		this.dreamBubbleBorderKanim.Stop();
	}

	// Token: 0x06003221 RID: 12833 RVA: 0x0010C362 File Offset: 0x0010A562
	public void StopDreaming()
	{
		this._currentDream = null;
		this.SetVisibility(false);
	}

	// Token: 0x04001E68 RID: 7784
	public KBatchedAnimController dreamBackgroundComponent;

	// Token: 0x04001E69 RID: 7785
	public KBatchedAnimController maskKanim;

	// Token: 0x04001E6A RID: 7786
	public KBatchedAnimController dreamBubbleBorderKanim;

	// Token: 0x04001E6B RID: 7787
	public KImage dreamContentComponent;

	// Token: 0x04001E6C RID: 7788
	private const string dreamBackgroundAnimationName = "dream_loop";

	// Token: 0x04001E6D RID: 7789
	private const string dreamMaskAnimationName = "dream_bubble_mask";

	// Token: 0x04001E6E RID: 7790
	private const string dreamBubbleBorderAnimationName = "dream_bubble_loop";

	// Token: 0x04001E6F RID: 7791
	private HashedString snapToPivotSymbol = new HashedString("snapto_pivot");

	// Token: 0x04001E71 RID: 7793
	private Dream _currentDream;

	// Token: 0x04001E72 RID: 7794
	private float _timePassedSinceDreamStarted;

	// Token: 0x04001E73 RID: 7795
	private Color _color = Color.white;

	// Token: 0x04001E74 RID: 7796
	private const float PI_2 = 6.2831855f;

	// Token: 0x04001E75 RID: 7797
	private const float HALF_PI = 1.5707964f;
}
