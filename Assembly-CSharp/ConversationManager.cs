using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006B2 RID: 1714
[AddComponentMenu("KMonoBehaviour/scripts/ConversationManager")]
public class ConversationManager : KMonoBehaviour, ISim200ms
{
	// Token: 0x06002EA6 RID: 11942 RVA: 0x000F65A6 File Offset: 0x000F47A6
	protected override void OnPrefabInit()
	{
		this.activeSetups = new List<Conversation>();
		this.lastConvoTimeByMinion = new Dictionary<MinionIdentity, float>();
		this.simRenderLoadBalance = true;
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x000F65C8 File Offset: 0x000F47C8
	public void Sim200ms(float dt)
	{
		for (int i = this.activeSetups.Count - 1; i >= 0; i--)
		{
			Conversation conversation = this.activeSetups[i];
			for (int j = conversation.minions.Count - 1; j >= 0; j--)
			{
				if (!this.ValidMinionTags(conversation.minions[j]) || !this.MinionCloseEnoughToConvo(conversation.minions[j], conversation))
				{
					conversation.minions.RemoveAt(j);
				}
				else
				{
					this.setupsByMinion[conversation.minions[j]] = conversation;
				}
			}
			if (conversation.minions.Count <= 1)
			{
				this.activeSetups.RemoveAt(i);
			}
			else
			{
				bool flag = true;
				bool flag2 = conversation.minions.Find((MinionIdentity match) => !match.HasTag(GameTags.Partying)) == null;
				if ((conversation.numUtterances == 0 && flag2 && GameClock.Instance.GetTime() > conversation.lastTalkedTime) || GameClock.Instance.GetTime() > conversation.lastTalkedTime + TuningData<ConversationManager.Tuning>.Get().delayBeforeStart)
				{
					MinionIdentity minionIdentity = conversation.minions[UnityEngine.Random.Range(0, conversation.minions.Count)];
					conversation.conversationType.NewTarget(minionIdentity);
					flag = this.DoTalking(conversation, minionIdentity);
				}
				else if (conversation.numUtterances > 0 && conversation.numUtterances < TuningData<ConversationManager.Tuning>.Get().maxUtterances && ((flag2 && GameClock.Instance.GetTime() > conversation.lastTalkedTime + TuningData<ConversationManager.Tuning>.Get().speakTime / 4f) || GameClock.Instance.GetTime() > conversation.lastTalkedTime + TuningData<ConversationManager.Tuning>.Get().speakTime + TuningData<ConversationManager.Tuning>.Get().delayBetweenUtterances))
				{
					int num = (conversation.minions.IndexOf(conversation.lastTalked) + UnityEngine.Random.Range(1, conversation.minions.Count)) % conversation.minions.Count;
					MinionIdentity minionIdentity2 = conversation.minions[num];
					flag = this.DoTalking(conversation, minionIdentity2);
				}
				else if (conversation.numUtterances >= TuningData<ConversationManager.Tuning>.Get().maxUtterances)
				{
					flag = false;
				}
				if (!flag)
				{
					this.activeSetups.RemoveAt(i);
				}
			}
		}
		foreach (MinionIdentity minionIdentity3 in Components.LiveMinionIdentities.Items)
		{
			if (this.ValidMinionTags(minionIdentity3) && !this.setupsByMinion.ContainsKey(minionIdentity3) && !this.MinionOnCooldown(minionIdentity3))
			{
				foreach (MinionIdentity minionIdentity4 in Components.LiveMinionIdentities.Items)
				{
					if (!(minionIdentity4 == minionIdentity3) && this.ValidMinionTags(minionIdentity4))
					{
						if (this.setupsByMinion.ContainsKey(minionIdentity4))
						{
							Conversation conversation2 = this.setupsByMinion[minionIdentity4];
							if (conversation2.minions.Count < TuningData<ConversationManager.Tuning>.Get().maxDupesPerConvo && (this.GetCentroid(conversation2) - minionIdentity3.transform.GetPosition()).magnitude < TuningData<ConversationManager.Tuning>.Get().maxDistance * 0.5f)
							{
								conversation2.minions.Add(minionIdentity3);
								this.setupsByMinion[minionIdentity3] = conversation2;
								break;
							}
						}
						else if (!this.MinionOnCooldown(minionIdentity4) && (minionIdentity4.transform.GetPosition() - minionIdentity3.transform.GetPosition()).magnitude < TuningData<ConversationManager.Tuning>.Get().maxDistance)
						{
							Conversation conversation3 = new Conversation();
							conversation3.minions.Add(minionIdentity3);
							conversation3.minions.Add(minionIdentity4);
							Type type = this.convoTypes[UnityEngine.Random.Range(0, this.convoTypes.Count)];
							conversation3.conversationType = (ConversationType)Activator.CreateInstance(type);
							conversation3.lastTalkedTime = GameClock.Instance.GetTime();
							this.activeSetups.Add(conversation3);
							this.setupsByMinion[minionIdentity3] = conversation3;
							this.setupsByMinion[minionIdentity4] = conversation3;
							break;
						}
					}
				}
			}
		}
		this.setupsByMinion.Clear();
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x000F6A78 File Offset: 0x000F4C78
	private bool DoTalking(Conversation setup, MinionIdentity new_speaker)
	{
		DebugUtil.Assert(setup != null, "setup was null");
		DebugUtil.Assert(new_speaker != null, "new_speaker was null");
		if (setup.lastTalked != null)
		{
			setup.lastTalked.Trigger(25860745, setup.lastTalked.gameObject);
		}
		DebugUtil.Assert(setup.conversationType != null, "setup.conversationType was null");
		Conversation.Topic nextTopic = setup.conversationType.GetNextTopic(new_speaker, setup.lastTopic);
		if (nextTopic == null || nextTopic.mode == Conversation.ModeType.End || nextTopic.mode == Conversation.ModeType.Segue)
		{
			return false;
		}
		Thought thoughtForTopic = this.GetThoughtForTopic(setup, nextTopic);
		if (thoughtForTopic == null)
		{
			return false;
		}
		ThoughtGraph.Instance smi = new_speaker.GetSMI<ThoughtGraph.Instance>();
		if (smi == null)
		{
			return false;
		}
		smi.AddThought(thoughtForTopic);
		setup.lastTopic = nextTopic;
		setup.lastTalked = new_speaker;
		setup.lastTalkedTime = GameClock.Instance.GetTime();
		DebugUtil.Assert(this.lastConvoTimeByMinion != null, "lastConvoTimeByMinion was null");
		this.lastConvoTimeByMinion[setup.lastTalked] = GameClock.Instance.GetTime();
		Effects component = setup.lastTalked.GetComponent<Effects>();
		DebugUtil.Assert(component != null, "effects was null");
		component.Add("GoodConversation", true);
		Conversation.Mode mode = Conversation.Topic.Modes[(int)nextTopic.mode];
		DebugUtil.Assert(mode != null, "mode was null");
		ConversationManager.StartedTalkingEvent startedTalkingEvent = new ConversationManager.StartedTalkingEvent
		{
			talker = new_speaker.gameObject,
			anim = mode.anim
		};
		foreach (MinionIdentity minionIdentity in setup.minions)
		{
			if (!minionIdentity)
			{
				DebugUtil.DevAssert(false, "minion in setup.minions was null", null);
			}
			else
			{
				minionIdentity.Trigger(-594200555, startedTalkingEvent);
			}
		}
		setup.numUtterances++;
		return true;
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x000F6C54 File Offset: 0x000F4E54
	public bool TryGetConversation(MinionIdentity minion, out Conversation conversation)
	{
		return this.setupsByMinion.TryGetValue(minion, out conversation);
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x000F6C64 File Offset: 0x000F4E64
	private Vector3 GetCentroid(Conversation setup)
	{
		Vector3 vector = Vector3.zero;
		foreach (MinionIdentity minionIdentity in setup.minions)
		{
			if (!(minionIdentity == null))
			{
				vector += minionIdentity.transform.GetPosition();
			}
		}
		return vector / (float)setup.minions.Count;
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x000F6CE4 File Offset: 0x000F4EE4
	private Thought GetThoughtForTopic(Conversation setup, Conversation.Topic topic)
	{
		if (string.IsNullOrEmpty(topic.topic))
		{
			DebugUtil.DevAssert(false, "topic.topic was null", null);
			return null;
		}
		Sprite sprite = setup.conversationType.GetSprite(topic.topic);
		if (sprite != null)
		{
			Conversation.Mode mode = Conversation.Topic.Modes[(int)topic.mode];
			return new Thought("Topic_" + topic.topic, null, sprite, mode.icon, mode.voice, "bubble_chatter", mode.mouth, DUPLICANTS.THOUGHTS.CONVERSATION.TOOLTIP, true, TuningData<ConversationManager.Tuning>.Get().speakTime);
		}
		return null;
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x000F6D78 File Offset: 0x000F4F78
	private bool ValidMinionTags(MinionIdentity minion)
	{
		return !(minion == null) && !minion.GetComponent<KPrefabID>().HasAnyTags(ConversationManager.invalidConvoTags);
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x000F6D98 File Offset: 0x000F4F98
	private bool MinionCloseEnoughToConvo(MinionIdentity minion, Conversation setup)
	{
		return (this.GetCentroid(setup) - minion.transform.GetPosition()).magnitude < TuningData<ConversationManager.Tuning>.Get().maxDistance * 0.5f;
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x000F6DD8 File Offset: 0x000F4FD8
	private bool MinionOnCooldown(MinionIdentity minion)
	{
		return !minion.GetComponent<KPrefabID>().HasTag(GameTags.AlwaysConverse) && ((this.lastConvoTimeByMinion.ContainsKey(minion) && GameClock.Instance.GetTime() < this.lastConvoTimeByMinion[minion] + TuningData<ConversationManager.Tuning>.Get().minionCooldownTime) || GameClock.Instance.GetTime() / 600f < TuningData<ConversationManager.Tuning>.Get().cyclesBeforeFirstConversation);
	}

	// Token: 0x04001C24 RID: 7204
	private List<Conversation> activeSetups;

	// Token: 0x04001C25 RID: 7205
	private Dictionary<MinionIdentity, float> lastConvoTimeByMinion;

	// Token: 0x04001C26 RID: 7206
	private Dictionary<MinionIdentity, Conversation> setupsByMinion = new Dictionary<MinionIdentity, Conversation>();

	// Token: 0x04001C27 RID: 7207
	private List<Type> convoTypes = new List<Type>
	{
		typeof(RecentThingConversation),
		typeof(AmountStateConversation),
		typeof(CurrentJobConversation)
	};

	// Token: 0x04001C28 RID: 7208
	private static readonly Tag[] invalidConvoTags = new Tag[]
	{
		GameTags.Asleep,
		GameTags.HoldingBreath,
		GameTags.Dead
	};

	// Token: 0x02001388 RID: 5000
	public class Tuning : TuningData<ConversationManager.Tuning>
	{
		// Token: 0x040060F8 RID: 24824
		public float cyclesBeforeFirstConversation;

		// Token: 0x040060F9 RID: 24825
		public float maxDistance;

		// Token: 0x040060FA RID: 24826
		public int maxDupesPerConvo;

		// Token: 0x040060FB RID: 24827
		public float minionCooldownTime;

		// Token: 0x040060FC RID: 24828
		public float speakTime;

		// Token: 0x040060FD RID: 24829
		public float delayBetweenUtterances;

		// Token: 0x040060FE RID: 24830
		public float delayBeforeStart;

		// Token: 0x040060FF RID: 24831
		public int maxUtterances;
	}

	// Token: 0x02001389 RID: 5001
	public class StartedTalkingEvent
	{
		// Token: 0x04006100 RID: 24832
		public GameObject talker;

		// Token: 0x04006101 RID: 24833
		public string anim;
	}
}
