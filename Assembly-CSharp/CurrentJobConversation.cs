using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B5 RID: 1717
public class CurrentJobConversation : ConversationType
{
	// Token: 0x06002EB6 RID: 11958 RVA: 0x000F6EF5 File Offset: 0x000F50F5
	public CurrentJobConversation()
	{
		this.id = "CurrentJobConversation";
	}

	// Token: 0x06002EB7 RID: 11959 RVA: 0x000F6F08 File Offset: 0x000F5108
	public override void NewTarget(MinionIdentity speaker)
	{
		this.target = "hows_role";
	}

	// Token: 0x06002EB8 RID: 11960 RVA: 0x000F6F18 File Offset: 0x000F5118
	public override Conversation.Topic GetNextTopic(MinionIdentity speaker, Conversation.Topic lastTopic)
	{
		if (lastTopic == null)
		{
			return new Conversation.Topic(this.target, Conversation.ModeType.Query);
		}
		List<Conversation.ModeType> list = CurrentJobConversation.transitions[lastTopic.mode];
		Conversation.ModeType modeType = list[UnityEngine.Random.Range(0, list.Count)];
		if (modeType == Conversation.ModeType.Statement)
		{
			this.target = this.GetRoleForSpeaker(speaker);
			Conversation.ModeType modeForRole = this.GetModeForRole(speaker, this.target);
			return new Conversation.Topic(this.target, modeForRole);
		}
		return new Conversation.Topic(this.target, modeType);
	}

	// Token: 0x06002EB9 RID: 11961 RVA: 0x000F6F94 File Offset: 0x000F5194
	public override Sprite GetSprite(string topic)
	{
		if (topic == "hows_role")
		{
			return Assets.GetSprite("crew_state_role");
		}
		if (Db.Get().Skills.TryGet(topic) != null)
		{
			return Assets.GetSprite(Db.Get().Skills.Get(topic).hat);
		}
		return null;
	}

	// Token: 0x06002EBA RID: 11962 RVA: 0x000F6FF1 File Offset: 0x000F51F1
	private Conversation.ModeType GetModeForRole(MinionIdentity speaker, string roleId)
	{
		return Conversation.ModeType.Nominal;
	}

	// Token: 0x06002EBB RID: 11963 RVA: 0x000F6FF4 File Offset: 0x000F51F4
	private string GetRoleForSpeaker(MinionIdentity speaker)
	{
		return speaker.GetComponent<MinionResume>().CurrentRole;
	}

	// Token: 0x04001C31 RID: 7217
	public static Dictionary<Conversation.ModeType, List<Conversation.ModeType>> transitions = new Dictionary<Conversation.ModeType, List<Conversation.ModeType>>
	{
		{
			Conversation.ModeType.Query,
			new List<Conversation.ModeType> { Conversation.ModeType.Statement }
		},
		{
			Conversation.ModeType.Satisfaction,
			new List<Conversation.ModeType> { Conversation.ModeType.Agreement }
		},
		{
			Conversation.ModeType.Nominal,
			new List<Conversation.ModeType> { Conversation.ModeType.Musing }
		},
		{
			Conversation.ModeType.Dissatisfaction,
			new List<Conversation.ModeType> { Conversation.ModeType.Disagreement }
		},
		{
			Conversation.ModeType.Stressing,
			new List<Conversation.ModeType> { Conversation.ModeType.Disagreement }
		},
		{
			Conversation.ModeType.Agreement,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Disagreement,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.End
			}
		},
		{
			Conversation.ModeType.Musing,
			new List<Conversation.ModeType>
			{
				Conversation.ModeType.Query,
				Conversation.ModeType.End
			}
		}
	};
}
