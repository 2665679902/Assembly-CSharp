using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B45 RID: 2885
public class NotificationHighlightController : KMonoBehaviour
{
	// Token: 0x0600595D RID: 22877 RVA: 0x00205802 File Offset: 0x00203A02
	protected override void OnSpawn()
	{
		this.highlightBox = Util.KInstantiateUI<RectTransform>(this.highlightBoxPrefab.gameObject, base.gameObject, false);
		this.HideBox();
	}

	// Token: 0x0600595E RID: 22878 RVA: 0x00205828 File Offset: 0x00203A28
	[ContextMenu("Force Update")]
	protected void LateUpdate()
	{
		bool flag = false;
		if (this.activeTargetNotification != null)
		{
			foreach (NotificationHighlightTarget notificationHighlightTarget in this.targets)
			{
				if (notificationHighlightTarget.targetKey == this.activeTargetNotification.highlightTarget)
				{
					this.SnapBoxToTarget(notificationHighlightTarget);
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			this.HideBox();
		}
	}

	// Token: 0x0600595F RID: 22879 RVA: 0x002058AC File Offset: 0x00203AAC
	public void AddTarget(NotificationHighlightTarget target)
	{
		this.targets.Add(target);
	}

	// Token: 0x06005960 RID: 22880 RVA: 0x002058BA File Offset: 0x00203ABA
	public void RemoveTarget(NotificationHighlightTarget target)
	{
		this.targets.Remove(target);
	}

	// Token: 0x06005961 RID: 22881 RVA: 0x002058C9 File Offset: 0x00203AC9
	public void SetActiveTarget(ManagementMenuNotification notification)
	{
		this.activeTargetNotification = notification;
	}

	// Token: 0x06005962 RID: 22882 RVA: 0x002058D2 File Offset: 0x00203AD2
	public void ClearActiveTarget(ManagementMenuNotification checkNotification)
	{
		if (checkNotification == this.activeTargetNotification)
		{
			this.activeTargetNotification = null;
		}
	}

	// Token: 0x06005963 RID: 22883 RVA: 0x002058E4 File Offset: 0x00203AE4
	public void ClearActiveTarget()
	{
		this.activeTargetNotification = null;
	}

	// Token: 0x06005964 RID: 22884 RVA: 0x002058ED File Offset: 0x00203AED
	public void TargetViewed(NotificationHighlightTarget target)
	{
		if (this.activeTargetNotification != null && this.activeTargetNotification.highlightTarget == target.targetKey)
		{
			this.activeTargetNotification.View();
		}
	}

	// Token: 0x06005965 RID: 22885 RVA: 0x0020591C File Offset: 0x00203B1C
	private void SnapBoxToTarget(NotificationHighlightTarget target)
	{
		RectTransform rectTransform = target.rectTransform();
		Vector3 position = rectTransform.GetPosition();
		this.highlightBox.sizeDelta = rectTransform.rect.size;
		this.highlightBox.SetPosition(position + new Vector3(rectTransform.rect.position.x, rectTransform.rect.position.y, 0f));
		RectMask2D componentInParent = rectTransform.GetComponentInParent<RectMask2D>();
		if (componentInParent != null)
		{
			RectTransform rectTransform2 = componentInParent.rectTransform();
			Vector3 vector = rectTransform2.TransformPoint(rectTransform2.rect.min);
			Vector3 vector2 = rectTransform2.TransformPoint(rectTransform2.rect.max);
			Vector3 vector3 = this.highlightBox.TransformPoint(this.highlightBox.rect.min);
			Vector3 vector4 = this.highlightBox.TransformPoint(this.highlightBox.rect.max);
			Vector3 vector5 = vector - vector3;
			Vector3 vector6 = vector2 - vector4;
			if (vector5.x > 0f)
			{
				this.highlightBox.anchoredPosition = this.highlightBox.anchoredPosition + new Vector2(vector5.x, 0f);
				this.highlightBox.sizeDelta -= new Vector2(vector5.x, 0f);
			}
			else if (vector5.y > 0f)
			{
				this.highlightBox.anchoredPosition = this.highlightBox.anchoredPosition + new Vector2(0f, vector5.y);
				this.highlightBox.sizeDelta -= new Vector2(0f, vector5.y);
			}
			if (vector6.x < 0f)
			{
				this.highlightBox.sizeDelta += new Vector2(vector6.x, 0f);
			}
			if (vector6.y < 0f)
			{
				this.highlightBox.sizeDelta += new Vector2(0f, vector6.y);
			}
		}
		this.highlightBox.gameObject.SetActive(this.highlightBox.sizeDelta.x > 0f && this.highlightBox.sizeDelta.y > 0f);
	}

	// Token: 0x06005966 RID: 22886 RVA: 0x00205BAB File Offset: 0x00203DAB
	private void HideBox()
	{
		this.highlightBox.gameObject.SetActive(false);
	}

	// Token: 0x04003C65 RID: 15461
	public RectTransform highlightBoxPrefab;

	// Token: 0x04003C66 RID: 15462
	private RectTransform highlightBox;

	// Token: 0x04003C67 RID: 15463
	private List<NotificationHighlightTarget> targets = new List<NotificationHighlightTarget>();

	// Token: 0x04003C68 RID: 15464
	private ManagementMenuNotification activeTargetNotification;
}
