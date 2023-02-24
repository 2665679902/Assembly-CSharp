using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200081F RID: 2079
public class ConversationMonitor : GameStateMachine<ConversationMonitor, ConversationMonitor.Instance, IStateMachineTarget, ConversationMonitor.Def>
{
	// Token: 0x06003C57 RID: 15447 RVA: 0x00150344 File Offset: 0x0014E544
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.TopicDiscussed, delegate(ConversationMonitor.Instance smi, object obj)
		{
			smi.OnTopicDiscussed(obj);
		}).EventHandler(GameHashes.TopicDiscovered, delegate(ConversationMonitor.Instance smi, object obj)
		{
			smi.OnTopicDiscovered(obj);
		});
	}

	// Token: 0x04002745 RID: 10053
	private const int MAX_RECENT_TOPICS = 5;

	// Token: 0x04002746 RID: 10054
	private const int MAX_FAVOURITE_TOPICS = 5;

	// Token: 0x04002747 RID: 10055
	private const float FAVOURITE_CHANCE = 0.033333335f;

	// Token: 0x04002748 RID: 10056
	private const float LEARN_CHANCE = 0.33333334f;

	// Token: 0x02001586 RID: 5510
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001587 RID: 5511
	[SerializationConfig(MemberSerialization.OptIn)]
	public new class Instance : GameStateMachine<ConversationMonitor, ConversationMonitor.Instance, IStateMachineTarget, ConversationMonitor.Def>.GameInstance
	{
		// Token: 0x06008438 RID: 33848 RVA: 0x002E96AC File Offset: 0x002E78AC
		public Instance(IStateMachineTarget master, ConversationMonitor.Def def)
			: base(master, def)
		{
			this.recentTopics = new Queue<string>();
			this.favouriteTopics = new List<string> { ConversationMonitor.Instance.randomTopics[UnityEngine.Random.Range(0, ConversationMonitor.Instance.randomTopics.Count)] };
			this.personalTopics = new List<string>();
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x002E9704 File Offset: 0x002E7904
		public string GetATopic()
		{
			int num = this.recentTopics.Count + this.favouriteTopics.Count * 2 + this.personalTopics.Count;
			int num2 = UnityEngine.Random.Range(0, num);
			if (num2 < this.recentTopics.Count)
			{
				return this.recentTopics.Dequeue();
			}
			num2 -= this.recentTopics.Count;
			if (num2 < this.favouriteTopics.Count)
			{
				return this.favouriteTopics[num2];
			}
			num2 -= this.favouriteTopics.Count;
			if (num2 < this.favouriteTopics.Count)
			{
				return this.favouriteTopics[num2];
			}
			num2 -= this.favouriteTopics.Count;
			if (num2 < this.personalTopics.Count)
			{
				return this.personalTopics[num2];
			}
			return "";
		}

		// Token: 0x0600843A RID: 33850 RVA: 0x002E97DC File Offset: 0x002E79DC
		public void OnTopicDiscovered(object data)
		{
			string text = (string)data;
			if (!this.recentTopics.Contains(text))
			{
				this.recentTopics.Enqueue(text);
				if (this.recentTopics.Count > 5)
				{
					string text2 = this.recentTopics.Dequeue();
					this.TryMakeFavouriteTopic(text2);
				}
			}
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x002E982C File Offset: 0x002E7A2C
		public void OnTopicDiscussed(object data)
		{
			string text = (string)data;
			if (UnityEngine.Random.value < 0.33333334f)
			{
				this.OnTopicDiscovered(text);
			}
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x002E9854 File Offset: 0x002E7A54
		private void TryMakeFavouriteTopic(string topic)
		{
			if (UnityEngine.Random.value < 0.033333335f)
			{
				if (this.favouriteTopics.Count < 5)
				{
					this.favouriteTopics.Add(topic);
					return;
				}
				this.favouriteTopics[UnityEngine.Random.Range(0, this.favouriteTopics.Count)] = topic;
			}
		}

		// Token: 0x04006701 RID: 26369
		[Serialize]
		private Queue<string> recentTopics;

		// Token: 0x04006702 RID: 26370
		[Serialize]
		private List<string> favouriteTopics;

		// Token: 0x04006703 RID: 26371
		private List<string> personalTopics;

		// Token: 0x04006704 RID: 26372
		private static readonly List<string> randomTopics = new List<string> { "Headquarters" };
	}
}
