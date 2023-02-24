using System;
using System.Collections.Generic;

// Token: 0x020006B3 RID: 1715
public class Conversation
{
	// Token: 0x04001C29 RID: 7209
	public List<MinionIdentity> minions = new List<MinionIdentity>();

	// Token: 0x04001C2A RID: 7210
	public MinionIdentity lastTalked;

	// Token: 0x04001C2B RID: 7211
	public ConversationType conversationType;

	// Token: 0x04001C2C RID: 7212
	public float lastTalkedTime;

	// Token: 0x04001C2D RID: 7213
	public Conversation.Topic lastTopic;

	// Token: 0x04001C2E RID: 7214
	public int numUtterances;

	// Token: 0x0200138B RID: 5003
	public enum ModeType
	{
		// Token: 0x04006105 RID: 24837
		Query,
		// Token: 0x04006106 RID: 24838
		Statement,
		// Token: 0x04006107 RID: 24839
		Agreement,
		// Token: 0x04006108 RID: 24840
		Disagreement,
		// Token: 0x04006109 RID: 24841
		Musing,
		// Token: 0x0400610A RID: 24842
		Satisfaction,
		// Token: 0x0400610B RID: 24843
		Nominal,
		// Token: 0x0400610C RID: 24844
		Dissatisfaction,
		// Token: 0x0400610D RID: 24845
		Stressing,
		// Token: 0x0400610E RID: 24846
		Segue,
		// Token: 0x0400610F RID: 24847
		End
	}

	// Token: 0x0200138C RID: 5004
	public class Mode
	{
		// Token: 0x06007E4B RID: 32331 RVA: 0x002D8CEB File Offset: 0x002D6EEB
		public Mode(Conversation.ModeType type, string voice, string icon, string mouth, string anim, bool newTopic = false)
		{
			this.type = type;
			this.voice = voice;
			this.mouth = mouth;
			this.anim = anim;
			this.icon = icon;
			this.newTopic = newTopic;
		}

		// Token: 0x04006110 RID: 24848
		public Conversation.ModeType type;

		// Token: 0x04006111 RID: 24849
		public string voice;

		// Token: 0x04006112 RID: 24850
		public string mouth;

		// Token: 0x04006113 RID: 24851
		public string anim;

		// Token: 0x04006114 RID: 24852
		public string icon;

		// Token: 0x04006115 RID: 24853
		public bool newTopic;
	}

	// Token: 0x0200138D RID: 5005
	public class Topic
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06007E4C RID: 32332 RVA: 0x002D8D20 File Offset: 0x002D6F20
		public static Dictionary<int, Conversation.Mode> Modes
		{
			get
			{
				if (Conversation.Topic._modes == null)
				{
					Conversation.Topic._modes = new Dictionary<int, Conversation.Mode>();
					foreach (Conversation.Mode mode in Conversation.Topic.modeList)
					{
						Conversation.Topic._modes[(int)mode.type] = mode;
					}
				}
				return Conversation.Topic._modes;
			}
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x002D8D94 File Offset: 0x002D6F94
		public Topic(string topic, Conversation.ModeType mode)
		{
			this.topic = topic;
			this.mode = mode;
		}

		// Token: 0x04006116 RID: 24854
		public static List<Conversation.Mode> modeList = new List<Conversation.Mode>
		{
			new Conversation.Mode(Conversation.ModeType.Query, "conversation_question", "mode_query", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Statement, "conversation_answer", "mode_statement", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Agreement, "conversation_answer", "mode_agreement", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Disagreement, "conversation_answer", "mode_disagreement", SpeechMonitor.PREFIX_SAD, "unhappy", false),
			new Conversation.Mode(Conversation.ModeType.Musing, "conversation_short", "mode_musing", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Satisfaction, "conversation_short", "mode_satisfaction", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Nominal, "conversation_short", "mode_nominal", SpeechMonitor.PREFIX_HAPPY, "happy", false),
			new Conversation.Mode(Conversation.ModeType.Dissatisfaction, "conversation_short", "mode_dissatisfaction", SpeechMonitor.PREFIX_SAD, "unhappy", false),
			new Conversation.Mode(Conversation.ModeType.Stressing, "conversation_short", "mode_stressing", SpeechMonitor.PREFIX_SAD, "unhappy", false),
			new Conversation.Mode(Conversation.ModeType.Segue, "conversation_question", "mode_segue", SpeechMonitor.PREFIX_HAPPY, "happy", true)
		};

		// Token: 0x04006117 RID: 24855
		private static Dictionary<int, Conversation.Mode> _modes;

		// Token: 0x04006118 RID: 24856
		public string topic;

		// Token: 0x04006119 RID: 24857
		public Conversation.ModeType mode;
	}
}
