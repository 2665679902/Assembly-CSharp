using System;
using System.Collections.Generic;

// Token: 0x02000680 RID: 1664
public class Chatty : KMonoBehaviour, ISimEveryTick
{
	// Token: 0x06002CD4 RID: 11476 RVA: 0x000EB3EA File Offset: 0x000E95EA
	protected override void OnPrefabInit()
	{
		base.GetComponent<KPrefabID>().AddTag(GameTags.AlwaysConverse, false);
		base.Subscribe(-594200555, new Action<object>(this.OnStartedTalking));
		this.identity = base.GetComponent<MinionIdentity>();
	}

	// Token: 0x06002CD5 RID: 11477 RVA: 0x000EB424 File Offset: 0x000E9624
	private void OnStartedTalking(object data)
	{
		MinionIdentity minionIdentity = data as MinionIdentity;
		if (minionIdentity == null)
		{
			return;
		}
		this.conversationPartners.Add(minionIdentity);
	}

	// Token: 0x06002CD6 RID: 11478 RVA: 0x000EB450 File Offset: 0x000E9650
	public void SimEveryTick(float dt)
	{
		if (this.conversationPartners.Count == 0)
		{
			return;
		}
		for (int i = this.conversationPartners.Count - 1; i >= 0; i--)
		{
			MinionIdentity minionIdentity = this.conversationPartners[i];
			this.conversationPartners.RemoveAt(i);
			if (!(minionIdentity == this.identity))
			{
				minionIdentity.AddTag(GameTags.PleasantConversation);
			}
		}
		base.gameObject.AddTag(GameTags.PleasantConversation);
	}

	// Token: 0x04001AC8 RID: 6856
	private MinionIdentity identity;

	// Token: 0x04001AC9 RID: 6857
	private List<MinionIdentity> conversationPartners = new List<MinionIdentity>();
}
