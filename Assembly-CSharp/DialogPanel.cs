using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000C2A RID: 3114
public class DialogPanel : MonoBehaviour, IDeselectHandler, IEventSystemHandler
{
	// Token: 0x06006289 RID: 25225 RVA: 0x00245C6C File Offset: 0x00243E6C
	public void OnDeselect(BaseEventData eventData)
	{
		if (this.destroyOnDeselect)
		{
			foreach (object obj in base.transform)
			{
				Util.KDestroyGameObject(((Transform)obj).gameObject);
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400443E RID: 17470
	public bool destroyOnDeselect = true;
}
