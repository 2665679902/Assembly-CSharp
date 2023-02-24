using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000056 RID: 86
[AddComponentMenu("KMonoBehaviour/Plugins/KBasicToggle")]
public class KBasicToggle : KMonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000356 RID: 854 RVA: 0x00011C9C File Offset: 0x0000FE9C
	// (remove) Token: 0x06000357 RID: 855 RVA: 0x00011CD4 File Offset: 0x0000FED4
	public event System.Action onClick;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000358 RID: 856 RVA: 0x00011D0C File Offset: 0x0000FF0C
	// (remove) Token: 0x06000359 RID: 857 RVA: 0x00011D44 File Offset: 0x0000FF44
	public event System.Action onDoubleClick;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x0600035A RID: 858 RVA: 0x00011D7C File Offset: 0x0000FF7C
	// (remove) Token: 0x0600035B RID: 859 RVA: 0x00011DB4 File Offset: 0x0000FFB4
	public event System.Action onPointerEnter;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x0600035C RID: 860 RVA: 0x00011DEC File Offset: 0x0000FFEC
	// (remove) Token: 0x0600035D RID: 861 RVA: 0x00011E24 File Offset: 0x00010024
	public event System.Action onPointerExit;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x0600035E RID: 862 RVA: 0x00011E5C File Offset: 0x0001005C
	// (remove) Token: 0x0600035F RID: 863 RVA: 0x00011E94 File Offset: 0x00010094
	public event Action<bool> onValueChanged;

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000360 RID: 864 RVA: 0x00011EC9 File Offset: 0x000100C9
	// (set) Token: 0x06000361 RID: 865 RVA: 0x00011ED1 File Offset: 0x000100D1
	public bool isOn
	{
		get
		{
			return this._isOn;
		}
		set
		{
			this._isOn = value;
			if (this.onValueChanged != null)
			{
				this.onValueChanged(value);
			}
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00011EEE File Offset: 0x000100EE
	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.doubleClickCoroutine != null && this.onDoubleClick != null)
		{
			this.onDoubleClick();
			this.didDoubleClick = true;
			return;
		}
		this.doubleClickCoroutine = this.DoubleClickTimer(eventData);
		base.StartCoroutine(this.doubleClickCoroutine);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00011F2D File Offset: 0x0001012D
	private IEnumerator DoubleClickTimer(PointerEventData eventData)
	{
		float startTime = Time.unscaledTime;
		while (Time.unscaledTime - startTime < 0.15f && !this.didDoubleClick)
		{
			yield return null;
		}
		if (!this.didDoubleClick && this.onClick != null)
		{
			this.isOn = !this.isOn;
			this.onClick();
			this.onValueChanged(this.isOn);
		}
		this.doubleClickCoroutine = null;
		this.didDoubleClick = false;
		yield break;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00011F3C File Offset: 0x0001013C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.onPointerEnter != null)
		{
			this.onPointerEnter();
		}
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00011F51 File Offset: 0x00010151
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.onPointerExit != null)
		{
			this.onPointerExit();
		}
	}

	// Token: 0x0400040F RID: 1039
	private const float DoubleClickTime = 0.15f;

	// Token: 0x04000410 RID: 1040
	private bool _isOn;

	// Token: 0x04000411 RID: 1041
	private bool didDoubleClick;

	// Token: 0x04000412 RID: 1042
	private IEnumerator doubleClickCoroutine;
}
