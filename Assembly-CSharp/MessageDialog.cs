using System;

// Token: 0x02000B1A RID: 2842
public abstract class MessageDialog : KMonoBehaviour
{
	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x060057A1 RID: 22433 RVA: 0x001FCD7E File Offset: 0x001FAF7E
	public virtual bool CanDontShowAgain
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060057A2 RID: 22434
	public abstract bool CanDisplay(Message message);

	// Token: 0x060057A3 RID: 22435
	public abstract void SetMessage(Message message);

	// Token: 0x060057A4 RID: 22436
	public abstract void OnClickAction();

	// Token: 0x060057A5 RID: 22437 RVA: 0x001FCD81 File Offset: 0x001FAF81
	public virtual void OnDontShowAgain()
	{
	}
}
