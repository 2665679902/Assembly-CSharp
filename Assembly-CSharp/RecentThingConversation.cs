using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B6 RID: 1718
public class RecentThingConversation : ConversationType
{
	// Token: 0x06002EBD RID: 11965 RVA: 0x000F70CB File Offset: 0x000F52CB
	public RecentThingConversation()
	{
		this.id = "RecentThingConversation";
	}

	// Token: 0x06002EBE RID: 11966 RVA: 0x000F70E0 File Offset: 0x000F52E0
	public override void NewTarget(MinionIdentity speaker)
	{
		ConversationMonitor.Instance smi = speaker.GetSMI<ConversationMonitor.Instance>();
		this.target = smi.GetATopic();
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x000F7100 File Offset: 0x000F5300
	public override Conversation.Topic GetNextTopic(MinionIdentity speaker, Conversation.Topic lastTopic)
	{
		if (string.IsNullOrEmpty(this.target))
		{
			return null;
		}
		List<Conversation.ModeType> list;
		if (lastTopic == null)
		{
			list = new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.Statement,
				Conversation.ModeType.Musing
			};
		}
		else
		{
			list = RecentThingConversation.transitions[lastTopic.mode];
		}
		Conversation.ModeType modeType = list[UnityEngine.Random.Range(0, list.Count)];
		return new Conversation.Topic(this.target, modeType);
	}

	// Token: 0x06002EC0 RID: 11968 RVA: 0x000F716C File Offset: 0x000F536C
	public override Sprite GetSprite(string topic)
	{
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(topic, "ui", true);
		if (uisprite != null)
		{
			return uisprite.first;
		}
		return null;
	}

	// Token: 0x04001C32 RID: 7218
	public static Dictionary<Conversation.ModeType, List<Conversation.ModeType>> transitions = new Dictionary<Conversation.ModeType, List<Conversation.ModeType>>
	{
		{
			Conversation.ModeType.Query,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Agreement,
				Conversation.ModeType.Disagreement,
				Conversation.ModeType.Musing
			}
		},
		{
			Conversation.ModeType.Statement,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Agreement,
				Conversation.ModeType.Disagreement,
				Conversation.ModeType.Query,
				Conversation.ModeType.Segue
			}
		},
		{
			Conversation.ModeType.Agreement,
			new List<Conversation.ModeType> { Conversation.ModeType.Satisfaction }
		},
		{
			Conversation.ModeType.Disagreement,
			new List<Conversation.ModeType> { Conversation.ModeType.Dissatisfaction }
		},
		{
			Conversation.ModeType.Musing,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.Statement,
				Conversation.ModeType.Segue
			}
		},
		{
			Conversation.ModeType.Satisfaction,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Segue,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Nominal,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Segue,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Dissatisfaction,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Segue,
				Conversation.ModeType.End
			}
		}
	};
}
