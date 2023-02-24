using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000B17 RID: 2839
public class GenericMessage : Message
{
	// Token: 0x06005789 RID: 22409 RVA: 0x001FCBB2 File Offset: 0x001FADB2
	public GenericMessage(string _title, string _body, string _tooltip, KMonoBehaviour click_focus = null)
	{
		this.title = _title;
		this.body = _body;
		this.tooltip = _tooltip;
		this.clickFocus.Set(click_focus);
	}

	// Token: 0x0600578A RID: 22410 RVA: 0x001FCBE7 File Offset: 0x001FADE7
	public GenericMessage()
	{
	}

	// Token: 0x0600578B RID: 22411 RVA: 0x001FCBFA File Offset: 0x001FADFA
	public override string GetSound()
	{
		return null;
	}

	// Token: 0x0600578C RID: 22412 RVA: 0x001FCBFD File Offset: 0x001FADFD
	public override string GetMessageBody()
	{
		return this.body;
	}

	// Token: 0x0600578D RID: 22413 RVA: 0x001FCC05 File Offset: 0x001FAE05
	public override string GetTooltip()
	{
		return this.tooltip;
	}

	// Token: 0x0600578E RID: 22414 RVA: 0x001FCC0D File Offset: 0x001FAE0D
	public override string GetTitle()
	{
		return this.title;
	}

	// Token: 0x0600578F RID: 22415 RVA: 0x001FCC18 File Offset: 0x001FAE18
	public override void OnClick()
	{
		KMonoBehaviour kmonoBehaviour = this.clickFocus.Get();
		if (kmonoBehaviour == null)
		{
			return;
		}
		Transform transform = kmonoBehaviour.transform;
		if (transform == null)
		{
			return;
		}
		Vector3 position = transform.GetPosition();
		position.z = -40f;
		CameraController.Instance.SetTargetPos(position, 8f, true);
		if (transform.GetComponent<KSelectable>() != null)
		{
			SelectTool.Instance.Select(transform.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x04003B55 RID: 15189
	[Serialize]
	private string title;

	// Token: 0x04003B56 RID: 15190
	[Serialize]
	private string tooltip;

	// Token: 0x04003B57 RID: 15191
	[Serialize]
	private string body;

	// Token: 0x04003B58 RID: 15192
	[Serialize]
	private Ref<KMonoBehaviour> clickFocus = new Ref<KMonoBehaviour>();
}
