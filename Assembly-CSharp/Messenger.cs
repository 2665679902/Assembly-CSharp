using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B1D RID: 2845
[AddComponentMenu("KMonoBehaviour/scripts/Messenger")]
public class Messenger : KMonoBehaviour
{
	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x060057B7 RID: 22455 RVA: 0x001FD165 File Offset: 0x001FB365
	public int Count
	{
		get
		{
			return this.messages.Count;
		}
	}

	// Token: 0x060057B8 RID: 22456 RVA: 0x001FD172 File Offset: 0x001FB372
	public IEnumerator<Message> GetEnumerator()
	{
		return this.messages.GetEnumerator();
	}

	// Token: 0x060057B9 RID: 22457 RVA: 0x001FD17F File Offset: 0x001FB37F
	public static void DestroyInstance()
	{
		Messenger.Instance = null;
	}

	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x060057BA RID: 22458 RVA: 0x001FD187 File Offset: 0x001FB387
	public SerializedList<Message> Messages
	{
		get
		{
			return this.messages;
		}
	}

	// Token: 0x060057BB RID: 22459 RVA: 0x001FD18F File Offset: 0x001FB38F
	protected override void OnPrefabInit()
	{
		Messenger.Instance = this;
	}

	// Token: 0x060057BC RID: 22460 RVA: 0x001FD198 File Offset: 0x001FB398
	protected override void OnSpawn()
	{
		int i = 0;
		while (i < this.messages.Count)
		{
			if (this.messages[i].IsValid())
			{
				i++;
			}
			else
			{
				this.messages.RemoveAt(i);
			}
		}
		base.Trigger(-599791736, null);
	}

	// Token: 0x060057BD RID: 22461 RVA: 0x001FD1E8 File Offset: 0x001FB3E8
	public void QueueMessage(Message message)
	{
		this.messages.Add(message);
		base.Trigger(1558809273, message);
	}

	// Token: 0x060057BE RID: 22462 RVA: 0x001FD204 File Offset: 0x001FB404
	public Message DequeueMessage()
	{
		Message message = null;
		if (this.messages.Count > 0)
		{
			message = this.messages[0];
			this.messages.RemoveAt(0);
		}
		return message;
	}

	// Token: 0x060057BF RID: 22463 RVA: 0x001FD23C File Offset: 0x001FB43C
	public void ClearAllMessages()
	{
		for (int i = this.messages.Count - 1; i >= 0; i--)
		{
			this.messages.RemoveAt(i);
		}
	}

	// Token: 0x060057C0 RID: 22464 RVA: 0x001FD26D File Offset: 0x001FB46D
	public void RemoveMessage(Message m)
	{
		this.messages.Remove(m);
		base.Trigger(-599791736, null);
	}

	// Token: 0x04003B64 RID: 15204
	[Serialize]
	private SerializedList<Message> messages = new SerializedList<Message>();

	// Token: 0x04003B65 RID: 15205
	public static Messenger Instance;
}
