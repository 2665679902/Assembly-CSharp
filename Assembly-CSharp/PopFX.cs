using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B5C RID: 2908
[AddComponentMenu("KMonoBehaviour/scripts/PopFX")]
public class PopFX : KMonoBehaviour
{
	// Token: 0x06005AAB RID: 23211 RVA: 0x0020E9E8 File Offset: 0x0020CBE8
	public void Recycle()
	{
		this.icon = null;
		this.text = "";
		this.targetTransform = null;
		this.lifeElapsed = 0f;
		this.trackTarget = false;
		this.startPos = Vector3.zero;
		this.IconDisplay.color = Color.white;
		this.TextDisplay.color = Color.white;
		PopFXManager.Instance.RecycleFX(this);
		this.canvasGroup.alpha = 0f;
		base.gameObject.SetActive(false);
		this.isLive = false;
		this.isActiveWorld = false;
		Game.Instance.Unsubscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
	}

	// Token: 0x06005AAC RID: 23212 RVA: 0x0020EA9C File Offset: 0x0020CC9C
	public void Spawn(Sprite Icon, string Text, Transform TargetTransform, Vector3 Offset, float LifeTime = 1.5f, bool TrackTarget = false)
	{
		this.icon = Icon;
		this.text = Text;
		this.targetTransform = TargetTransform;
		this.trackTarget = TrackTarget;
		this.lifetime = LifeTime;
		this.offset = Offset;
		if (this.targetTransform != null)
		{
			this.startPos = this.targetTransform.GetPosition();
			int num;
			int num2;
			Grid.PosToXY(this.startPos, out num, out num2);
			if (num2 % 2 != 0)
			{
				this.startPos.x = this.startPos.x + 0.5f;
			}
		}
		this.TextDisplay.text = this.text;
		this.IconDisplay.sprite = this.icon;
		this.canvasGroup.alpha = 1f;
		this.isLive = true;
		Game.Instance.Subscribe(1983128072, new Action<object>(this.OnActiveWorldChanged));
		this.SetWorldActive(ClusterManager.Instance.activeWorldId);
		this.Update();
	}

	// Token: 0x06005AAD RID: 23213 RVA: 0x0020EB88 File Offset: 0x0020CD88
	private void OnActiveWorldChanged(object data)
	{
		global::Tuple<int, int> tuple = (global::Tuple<int, int>)data;
		if (this.isLive)
		{
			this.SetWorldActive(tuple.first);
		}
	}

	// Token: 0x06005AAE RID: 23214 RVA: 0x0020EBB0 File Offset: 0x0020CDB0
	private void SetWorldActive(int worldId)
	{
		int num = Grid.PosToCell((this.trackTarget && this.targetTransform != null) ? this.targetTransform.position : (this.startPos + this.offset));
		this.isActiveWorld = !Grid.IsValidCell(num) || (int)Grid.WorldIdx[num] == worldId;
	}

	// Token: 0x06005AAF RID: 23215 RVA: 0x0020EC14 File Offset: 0x0020CE14
	private void Update()
	{
		if (!this.isLive)
		{
			return;
		}
		if (!PopFXManager.Instance.Ready())
		{
			return;
		}
		this.lifeElapsed += Time.unscaledDeltaTime;
		if (this.lifeElapsed >= this.lifetime)
		{
			this.Recycle();
		}
		if (this.trackTarget && this.targetTransform != null)
		{
			Vector3 vector = PopFXManager.Instance.WorldToScreen(this.targetTransform.GetPosition() + this.offset + Vector3.up * this.lifeElapsed * (this.Speed * this.lifeElapsed));
			vector.z = 0f;
			base.gameObject.rectTransform().anchoredPosition = vector;
		}
		else
		{
			Vector3 vector2 = PopFXManager.Instance.WorldToScreen(this.startPos + this.offset + Vector3.up * this.lifeElapsed * (this.Speed * (this.lifeElapsed / 2f)));
			vector2.z = 0f;
			base.gameObject.rectTransform().anchoredPosition = vector2;
		}
		this.canvasGroup.alpha = (this.isActiveWorld ? (1.5f * ((this.lifetime - this.lifeElapsed) / this.lifetime)) : 0f);
	}

	// Token: 0x04003D5B RID: 15707
	private float Speed = 2f;

	// Token: 0x04003D5C RID: 15708
	private Sprite icon;

	// Token: 0x04003D5D RID: 15709
	private string text;

	// Token: 0x04003D5E RID: 15710
	private Transform targetTransform;

	// Token: 0x04003D5F RID: 15711
	private Vector3 offset;

	// Token: 0x04003D60 RID: 15712
	public Image IconDisplay;

	// Token: 0x04003D61 RID: 15713
	public LocText TextDisplay;

	// Token: 0x04003D62 RID: 15714
	public CanvasGroup canvasGroup;

	// Token: 0x04003D63 RID: 15715
	private Camera uiCamera;

	// Token: 0x04003D64 RID: 15716
	private float lifetime;

	// Token: 0x04003D65 RID: 15717
	private float lifeElapsed;

	// Token: 0x04003D66 RID: 15718
	private bool trackTarget;

	// Token: 0x04003D67 RID: 15719
	private Vector3 startPos;

	// Token: 0x04003D68 RID: 15720
	private bool isLive;

	// Token: 0x04003D69 RID: 15721
	private bool isActiveWorld;
}
