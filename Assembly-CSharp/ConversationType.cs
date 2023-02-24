using System;
using UnityEngine;

// Token: 0x020006B4 RID: 1716
public class ConversationType
{
	// Token: 0x06002EB2 RID: 11954 RVA: 0x000F6EE5 File Offset: 0x000F50E5
	public virtual void NewTarget(MinionIdentity speaker)
	{
	}

	// Token: 0x06002EB3 RID: 11955 RVA: 0x000F6EE7 File Offset: 0x000F50E7
	public virtual Conversation.Topic GetNextTopic(MinionIdentity speaker, Conversation.Topic lastTopic)
	{
		return null;
	}

	// Token: 0x06002EB4 RID: 11956 RVA: 0x000F6EEA File Offset: 0x000F50EA
	public virtual Sprite GetSprite(string topic)
	{
		return null;
	}

	// Token: 0x04001C2F RID: 7215
	public string id;

	// Token: 0x04001C30 RID: 7216
	public string target;
}
