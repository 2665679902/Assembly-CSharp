using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020008AD RID: 2221
[SerializationConfig(MemberSerialization.OptIn)]
public class QuestManager : KMonoBehaviour
{
	// Token: 0x06003FE2 RID: 16354 RVA: 0x00165224 File Offset: 0x00163424
	protected override void OnPrefabInit()
	{
		if (QuestManager.instance != null)
		{
			UnityEngine.Object.Destroy(QuestManager.instance);
			return;
		}
		QuestManager.instance = this;
		base.OnPrefabInit();
	}

	// Token: 0x06003FE3 RID: 16355 RVA: 0x0016524C File Offset: 0x0016344C
	public static QuestInstance InitializeQuest(Tag ownerId, Quest quest)
	{
		QuestInstance questInstance;
		if (!QuestManager.TryGetQuest(ownerId.GetHash(), quest, out questInstance))
		{
			questInstance = (QuestManager.instance.ownerToQuests[ownerId.GetHash()][quest.IdHash] = new QuestInstance(quest));
		}
		questInstance.Initialize(quest);
		return questInstance;
	}

	// Token: 0x06003FE4 RID: 16356 RVA: 0x001652A0 File Offset: 0x001634A0
	public static QuestInstance InitializeQuest(HashedString ownerId, Quest quest)
	{
		QuestInstance questInstance;
		if (!QuestManager.TryGetQuest(ownerId.HashValue, quest, out questInstance))
		{
			questInstance = (QuestManager.instance.ownerToQuests[ownerId.HashValue][quest.IdHash] = new QuestInstance(quest));
		}
		questInstance.Initialize(quest);
		return questInstance;
	}

	// Token: 0x06003FE5 RID: 16357 RVA: 0x001652F4 File Offset: 0x001634F4
	public static QuestInstance GetInstance(Tag ownerId, Quest quest)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.GetHash(), quest, out questInstance);
		return questInstance;
	}

	// Token: 0x06003FE6 RID: 16358 RVA: 0x00165314 File Offset: 0x00163514
	public static QuestInstance GetInstance(HashedString ownerId, Quest quest)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.HashValue, quest, out questInstance);
		return questInstance;
	}

	// Token: 0x06003FE7 RID: 16359 RVA: 0x00165334 File Offset: 0x00163534
	public static bool CheckState(HashedString ownerId, Quest quest, Quest.State state)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.HashValue, quest, out questInstance);
		return questInstance != null && questInstance.CurrentState == state;
	}

	// Token: 0x06003FE8 RID: 16360 RVA: 0x00165360 File Offset: 0x00163560
	public static bool CheckState(Tag ownerId, Quest quest, Quest.State state)
	{
		QuestInstance questInstance;
		QuestManager.TryGetQuest(ownerId.GetHash(), quest, out questInstance);
		return questInstance != null && questInstance.CurrentState == state;
	}

	// Token: 0x06003FE9 RID: 16361 RVA: 0x0016538C File Offset: 0x0016358C
	private static bool TryGetQuest(int ownerId, Quest quest, out QuestInstance qInst)
	{
		qInst = null;
		Dictionary<HashedString, QuestInstance> dictionary;
		if (!QuestManager.instance.ownerToQuests.TryGetValue(ownerId, out dictionary))
		{
			dictionary = (QuestManager.instance.ownerToQuests[ownerId] = new Dictionary<HashedString, QuestInstance>());
		}
		return dictionary.TryGetValue(quest.IdHash, out qInst);
	}

	// Token: 0x040029E1 RID: 10721
	private static QuestManager instance;

	// Token: 0x040029E2 RID: 10722
	[Serialize]
	private Dictionary<int, Dictionary<HashedString, QuestInstance>> ownerToQuests = new Dictionary<int, Dictionary<HashedString, QuestInstance>>();
}
