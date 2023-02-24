using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200000D RID: 13
public class DragMe : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06000033 RID: 51 RVA: 0x00002DF4 File Offset: 0x00000FF4
	public void OnBeginDrag(PointerEventData eventData)
	{
		Canvas canvas = DragMe.FindInParents<Canvas>(base.gameObject);
		if (canvas == null)
		{
			return;
		}
		this.m_DraggingIcon = UnityEngine.Object.Instantiate<GameObject>(base.gameObject);
		GraphicRaycaster component = this.m_DraggingIcon.GetComponent<GraphicRaycaster>();
		if (component != null)
		{
			component.enabled = false;
		}
		this.m_DraggingIcon.name = "dragObj";
		this.m_DraggingIcon.transform.SetParent(canvas.transform, false);
		this.m_DraggingIcon.transform.SetAsLastSibling();
		this.m_DraggingIcon.GetComponent<RectTransform>().pivot = Vector2.zero;
		if (this.dragOnSurfaces)
		{
			this.m_DraggingPlane = base.transform as RectTransform;
		}
		else
		{
			this.m_DraggingPlane = canvas.transform as RectTransform;
		}
		this.SetDraggedPosition(eventData);
		this.listener.OnBeginDrag(eventData.position);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002ED4 File Offset: 0x000010D4
	public void OnDrag(PointerEventData data)
	{
		if (this.m_DraggingIcon != null)
		{
			this.SetDraggedPosition(data);
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002EEC File Offset: 0x000010EC
	private void SetDraggedPosition(PointerEventData data)
	{
		if (this.dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
		{
			this.m_DraggingPlane = data.pointerEnter.transform as RectTransform;
		}
		RectTransform component = this.m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 vector;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_DraggingPlane, data.position, data.pressEventCamera, out vector))
		{
			component.position = vector;
			component.rotation = this.m_DraggingPlane.rotation;
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002F7C File Offset: 0x0000117C
	public void OnEndDrag(PointerEventData eventData)
	{
		this.listener.OnEndDrag(eventData.position);
		if (this.m_DraggingIcon != null)
		{
			UnityEngine.Object.Destroy(this.m_DraggingIcon);
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002FA8 File Offset: 0x000011A8
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return default(T);
		}
		T t = default(T);
		Transform transform = go.transform.parent;
		while (transform != null && t == null)
		{
			t = transform.gameObject.GetComponent<T>();
			transform = transform.parent;
		}
		return t;
	}

	// Token: 0x0400002B RID: 43
	public bool dragOnSurfaces = true;

	// Token: 0x0400002C RID: 44
	private GameObject m_DraggingIcon;

	// Token: 0x0400002D RID: 45
	private RectTransform m_DraggingPlane;

	// Token: 0x0400002E RID: 46
	public DragMe.IDragListener listener;

	// Token: 0x02000DB7 RID: 3511
	public interface IDragListener
	{
		// Token: 0x06006A9F RID: 27295
		void OnBeginDrag(Vector2 position);

		// Token: 0x06006AA0 RID: 27296
		void OnEndDrag(Vector2 position);
	}
}
