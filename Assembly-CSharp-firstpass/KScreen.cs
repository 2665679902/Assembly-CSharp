using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000062 RID: 98
[AddComponentMenu("KMonoBehaviour/Plugins/KScreen")]
public class KScreen : KMonoBehaviour, IInputHandler, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060003CB RID: 971 RVA: 0x000139CA File Offset: 0x00011BCA
	public string handlerName
	{
		get
		{
			return base.gameObject.name;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060003CC RID: 972 RVA: 0x000139D7 File Offset: 0x00011BD7
	// (set) Token: 0x060003CD RID: 973 RVA: 0x000139DF File Offset: 0x00011BDF
	public KInputHandler inputHandler { get; set; }

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060003CE RID: 974 RVA: 0x000139E8 File Offset: 0x00011BE8
	public virtual bool HasFocus
	{
		get
		{
			return this.hasFocus;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060003CF RID: 975 RVA: 0x000139F0 File Offset: 0x00011BF0
	// (set) Token: 0x060003D0 RID: 976 RVA: 0x000139F8 File Offset: 0x00011BF8
	protected bool isEditing
	{
		get
		{
			return this._isEditing;
		}
		set
		{
			this._isEditing = value;
			KScreenManager.Instance.RefreshStack();
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00013A0B File Offset: 0x00011C0B
	public void SetIsEditing(bool state)
	{
		this.isEditing = state;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00013A14 File Offset: 0x00011C14
	public virtual float GetSortKey()
	{
		if (this.isEditing)
		{
			return 50f;
		}
		return 0f;
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060003D3 RID: 979 RVA: 0x00013A29 File Offset: 0x00011C29
	public Canvas canvas
	{
		get
		{
			return this._canvas;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060003D4 RID: 980 RVA: 0x00013A31 File Offset: 0x00011C31
	// (set) Token: 0x060003D5 RID: 981 RVA: 0x00013A39 File Offset: 0x00011C39
	public string screenName { get; private set; }

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060003D6 RID: 982 RVA: 0x00013A42 File Offset: 0x00011C42
	public bool GetMouseOver
	{
		get
		{
			return this.mouseOver;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060003D7 RID: 983 RVA: 0x00013A4A File Offset: 0x00011C4A
	// (set) Token: 0x060003D8 RID: 984 RVA: 0x00013A52 File Offset: 0x00011C52
	public bool ConsumeMouseScroll { get; set; }

	// Token: 0x060003D9 RID: 985 RVA: 0x00013A5B File Offset: 0x00011C5B
	public virtual void SetHasFocus(bool has_focus)
	{
		this.hasFocus = has_focus;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00013A64 File Offset: 0x00011C64
	public KScreen()
	{
		this.screenName = base.GetType().ToString();
		if (this.displayName == null || this.displayName == "")
		{
			this.displayName = this.screenName;
		}
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00013AA3 File Offset: 0x00011CA3
	protected override void OnPrefabInit()
	{
		if (this.fadeIn)
		{
			this.InitWidgetTransition();
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00013AB3 File Offset: 0x00011CB3
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver = true;
		if (this.pointerEnterActions != null)
		{
			this.pointerEnterActions(eventData);
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00013AD0 File Offset: 0x00011CD0
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		this.mouseOver = false;
		if (this.pointerExitActions != null)
		{
			this.pointerExitActions(eventData);
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00013AED File Offset: 0x00011CED
	public virtual void OnDrag(PointerEventData eventData)
	{
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00013AEF File Offset: 0x00011CEF
	public virtual void OnBeginDrag(PointerEventData eventData)
	{
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00013AF1 File Offset: 0x00011CF1
	public virtual void OnEndDrag(PointerEventData eventData)
	{
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00013AF4 File Offset: 0x00011CF4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this._canvas = base.GetComponentInParent<Canvas>();
		if (this._canvas != null)
		{
			this._rectTransform = this._canvas.GetComponentInParent<RectTransform>();
		}
		if (this.activateOnSpawn && KScreenManager.Instance != null && !this.isActive)
		{
			this.Activate();
		}
		if (this.ConsumeMouseScroll && !this.IsActive())
		{
			global::Debug.LogWarning("ConsumeMouseScroll is true on" + base.gameObject.name + " , but screen has not been activated. Mouse scrolling might not work properly on this screen.");
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00013B84 File Offset: 0x00011D84
	public virtual void OnKeyDown(KButtonEvent e)
	{
		if (this.isEditing)
		{
			e.Consumed = true;
		}
		if (!e.Consumed)
		{
			this.child_scroll_rects = base.GetComponentsInChildren<KScrollRect>();
		}
		if (this.mouseOver && this.ConsumeMouseScroll)
		{
			if (KInputManager.currentControllerIsGamepad && !e.Consumed)
			{
				foreach (KScrollRect kscrollRect in this.child_scroll_rects)
				{
					Vector2 vector = kscrollRect.rectTransform().InverseTransformPoint(KInputManager.GetMousePos());
					if (kscrollRect.rectTransform().rect.Contains(vector))
					{
						kscrollRect.mouseIsOver = true;
					}
					else
					{
						kscrollRect.mouseIsOver = false;
					}
					kscrollRect.OnKeyDown(e);
					if (e.Consumed)
					{
						break;
					}
				}
			}
			if (!e.Consumed && !e.TryConsume(global::Action.ZoomIn))
			{
				e.TryConsume(global::Action.ZoomOut);
			}
		}
		if (!e.Consumed)
		{
			foreach (KScrollRect kscrollRect2 in this.child_scroll_rects)
			{
				Vector2 vector2 = kscrollRect2.rectTransform().InverseTransformPoint(KInputManager.GetMousePos());
				if (kscrollRect2.rectTransform().rect.Contains(vector2))
				{
					kscrollRect2.mouseIsOver = true;
				}
				else
				{
					kscrollRect2.mouseIsOver = false;
				}
				kscrollRect2.OnKeyDown(e);
				if (e.Consumed)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00013CCC File Offset: 0x00011ECC
	public virtual void OnKeyUp(KButtonEvent e)
	{
		if (!e.Consumed)
		{
			KScrollRect[] componentsInChildren = base.GetComponentsInChildren<KScrollRect>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].OnKeyUp(e);
				if (e.Consumed)
				{
					break;
				}
			}
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00013D07 File Offset: 0x00011F07
	public virtual bool IsModal()
	{
		return false;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00013D0A File Offset: 0x00011F0A
	public virtual void ScreenUpdate(bool topLevel)
	{
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00013D0C File Offset: 0x00011F0C
	public bool IsActive()
	{
		return this.isActive;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00013D14 File Offset: 0x00011F14
	public void Activate()
	{
		base.gameObject.SetActive(true);
		KScreenManager.Instance.PushScreen(this);
		this.OnActivate();
		this.isActive = true;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00013D3A File Offset: 0x00011F3A
	protected virtual void OnActivate()
	{
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00013D3C File Offset: 0x00011F3C
	public virtual void Deactivate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.OnDeactivate();
		this.isActive = false;
		KScreenManager.Instance.PopScreen(this);
		if (this != null && base.gameObject != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00013D8C File Offset: 0x00011F8C
	protected override void OnCleanUp()
	{
		if (this.isActive)
		{
			this.Deactivate();
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00013D9C File Offset: 0x00011F9C
	protected virtual void OnDeactivate()
	{
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00013D9E File Offset: 0x00011F9E
	public string Name()
	{
		return this.screenName;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00013DA8 File Offset: 0x00011FA8
	public Vector3 WorldToScreen(Vector3 pos)
	{
		if (this._rectTransform == null)
		{
			global::Debug.LogWarning("Hey you are calling this function too early!");
			return Vector3.zero;
		}
		Camera main = Camera.main;
		Vector3 vector = main.WorldToViewportPoint(pos);
		vector.y = vector.y * main.rect.height + main.rect.y;
		return new Vector2((vector.x - 0.5f) * this._rectTransform.sizeDelta.x, (vector.y - 0.5f) * this._rectTransform.sizeDelta.y);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00013E50 File Offset: 0x00012050
	protected virtual void OnShow(bool show)
	{
		this.child_scroll_rects = base.GetComponentsInChildren<KScrollRect>();
		if (show && this.fadeIn)
		{
			base.gameObject.FindOrAddUnityComponent<WidgetTransition>().StartTransition();
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00013E79 File Offset: 0x00012079
	public virtual void Show(bool show = true)
	{
		this.mouseOver = false;
		base.gameObject.SetActive(show);
		this.OnShow(show);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00013E95 File Offset: 0x00012095
	public void SetShouldFadeIn(bool bShouldFade)
	{
		this.fadeIn = bShouldFade;
		this.InitWidgetTransition();
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00013EA4 File Offset: 0x000120A4
	private void InitWidgetTransition()
	{
		base.gameObject.FindOrAddUnityComponent<WidgetTransition>().SetTransitionType(this.transitionType);
	}

	// Token: 0x04000453 RID: 1107
	[SerializeField]
	public bool activateOnSpawn;

	// Token: 0x04000454 RID: 1108
	private bool _isEditing;

	// Token: 0x04000455 RID: 1109
	public const float MODAL_SCREEN_SORT_KEY = 100f;

	// Token: 0x04000456 RID: 1110
	public const float EDITING_SCREEN_SORT_KEY = 50f;

	// Token: 0x04000457 RID: 1111
	public const float LOCKER_SORT_KEY = 40f;

	// Token: 0x04000458 RID: 1112
	public const float PAUSE_MENU_SORT_KEY = 30f;

	// Token: 0x04000459 RID: 1113
	public const float FULLSCREEN_SCREEN_SORT_KEY = 20f;

	// Token: 0x0400045A RID: 1114
	private Canvas _canvas;

	// Token: 0x0400045B RID: 1115
	private RectTransform _rectTransform;

	// Token: 0x0400045D RID: 1117
	private bool isActive;

	// Token: 0x0400045E RID: 1118
	protected bool mouseOver;

	// Token: 0x04000460 RID: 1120
	public WidgetTransition.TransitionType transitionType;

	// Token: 0x04000461 RID: 1121
	public bool fadeIn;

	// Token: 0x04000462 RID: 1122
	public string displayName;

	// Token: 0x04000463 RID: 1123
	public KScreen.PointerEnterActions pointerEnterActions;

	// Token: 0x04000464 RID: 1124
	public KScreen.PointerExitActions pointerExitActions;

	// Token: 0x04000465 RID: 1125
	private KScrollRect[] child_scroll_rects;

	// Token: 0x04000466 RID: 1126
	private bool hasFocus;

	// Token: 0x04000467 RID: 1127
	public bool isHiddenButActive;

	// Token: 0x020009B1 RID: 2481
	// (Invoke) Token: 0x0600534E RID: 21326
	public delegate void PointerEnterActions(PointerEventData eventData);

	// Token: 0x020009B2 RID: 2482
	// (Invoke) Token: 0x06005352 RID: 21330
	public delegate void PointerExitActions(PointerEventData eventData);
}
