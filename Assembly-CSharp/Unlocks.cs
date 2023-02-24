using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x020009B2 RID: 2482
[AddComponentMenu("KMonoBehaviour/scripts/Unlocks")]
public class Unlocks : KMonoBehaviour
{
	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x060049AC RID: 18860 RVA: 0x0019C653 File Offset: 0x0019A853
	private static string UnlocksFilename
	{
		get
		{
			return System.IO.Path.Combine(global::Util.RootFolder(), "unlocks.json");
		}
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x0019C664 File Offset: 0x0019A864
	protected override void OnPrefabInit()
	{
		this.LoadUnlocks();
	}

	// Token: 0x060049AE RID: 18862 RVA: 0x0019C66C File Offset: 0x0019A86C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UnlockCycleCodexes();
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDay));
		base.Subscribe<Unlocks>(-1277991738, Unlocks.OnLaunchRocketDelegate);
		base.Subscribe<Unlocks>(282337316, Unlocks.OnDuplicantDiedDelegate);
		base.Subscribe<Unlocks>(-818188514, Unlocks.OnDiscoveredSpaceDelegate);
		Components.LiveMinionIdentities.OnAdd += this.OnNewDupe;
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x0019C6EA File Offset: 0x0019A8EA
	public bool IsUnlocked(string unlockID)
	{
		return !string.IsNullOrEmpty(unlockID) && (DebugHandler.InstantBuildMode || this.unlocked.Contains(unlockID));
	}

	// Token: 0x060049B0 RID: 18864 RVA: 0x0019C70B File Offset: 0x0019A90B
	public IReadOnlyList<string> GetAllUnlockedIds()
	{
		return this.unlocked;
	}

	// Token: 0x060049B1 RID: 18865 RVA: 0x0019C713 File Offset: 0x0019A913
	public void Lock(string unlockID)
	{
		if (this.unlocked.Contains(unlockID))
		{
			this.unlocked.Remove(unlockID);
			this.SaveUnlocks();
			Game.Instance.Trigger(1594320620, unlockID);
		}
	}

	// Token: 0x060049B2 RID: 18866 RVA: 0x0019C748 File Offset: 0x0019A948
	public void Unlock(string unlockID, bool shouldTryShowCodexNotification = true)
	{
		if (string.IsNullOrEmpty(unlockID))
		{
			DebugUtil.DevAssert(false, "Unlock called with null or empty string", null);
			return;
		}
		if (!this.unlocked.Contains(unlockID))
		{
			this.unlocked.Add(unlockID);
			this.SaveUnlocks();
			Game.Instance.Trigger(1594320620, unlockID);
			if (shouldTryShowCodexNotification)
			{
				MessageNotification messageNotification = this.GenerateCodexUnlockNotification(unlockID);
				if (messageNotification != null)
				{
					base.GetComponent<Notifier>().Add(messageNotification, "");
				}
			}
		}
		this.EvalMetaCategories();
	}

	// Token: 0x060049B3 RID: 18867 RVA: 0x0019C7C0 File Offset: 0x0019A9C0
	private void EvalMetaCategories()
	{
		foreach (Unlocks.MetaUnlockCategory metaUnlockCategory in this.MetaUnlockCategories)
		{
			string metaCollectionID = metaUnlockCategory.metaCollectionID;
			string mesaCollectionID = metaUnlockCategory.mesaCollectionID;
			int mesaUnlockCount = metaUnlockCategory.mesaUnlockCount;
			int num = 0;
			foreach (string text in this.lockCollections[mesaCollectionID])
			{
				if (this.IsUnlocked(text))
				{
					num++;
				}
			}
			if (num >= mesaUnlockCount)
			{
				this.UnlockNext(metaCollectionID);
			}
		}
	}

	// Token: 0x060049B4 RID: 18868 RVA: 0x0019C868 File Offset: 0x0019AA68
	private void SaveUnlocks()
	{
		if (!Directory.Exists(global::Util.RootFolder()))
		{
			Directory.CreateDirectory(global::Util.RootFolder());
		}
		string text = JsonConvert.SerializeObject(this.unlocked);
		bool flag = false;
		int num = 0;
		while (!flag && num < 5)
		{
			try
			{
				Thread.Sleep(num * 100);
				using (FileStream fileStream = File.Open(Unlocks.UnlocksFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
				{
					flag = true;
					byte[] bytes = new ASCIIEncoding().GetBytes(text);
					fileStream.Write(bytes, 0, bytes.Length);
				}
			}
			catch (Exception ex)
			{
				global::Debug.LogWarningFormat("Failed to save Unlocks attempt {0}: {1}", new object[]
				{
					num + 1,
					ex.ToString()
				});
			}
			num++;
		}
	}

	// Token: 0x060049B5 RID: 18869 RVA: 0x0019C930 File Offset: 0x0019AB30
	public void LoadUnlocks()
	{
		this.unlocked.Clear();
		if (!File.Exists(Unlocks.UnlocksFilename))
		{
			return;
		}
		string text = "";
		bool flag = false;
		int num = 0;
		while (!flag && num < 5)
		{
			try
			{
				Thread.Sleep(num * 100);
				using (FileStream fileStream = File.Open(Unlocks.UnlocksFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					flag = true;
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					byte[] array = new byte[fileStream.Length];
					if ((long)fileStream.Read(array, 0, array.Length) == fileStream.Length)
					{
						text += asciiencoding.GetString(array);
					}
				}
			}
			catch (Exception ex)
			{
				global::Debug.LogWarningFormat("Failed to load Unlocks attempt {0}: {1}", new object[]
				{
					num + 1,
					ex.ToString()
				});
			}
			num++;
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		try
		{
			foreach (string text2 in JsonConvert.DeserializeObject<string[]>(text))
			{
				if (!string.IsNullOrEmpty(text2) && !this.unlocked.Contains(text2))
				{
					this.unlocked.Add(text2);
				}
			}
		}
		catch (Exception ex2)
		{
			global::Debug.LogErrorFormat("Error parsing unlocks file [{0}]: {1}", new object[]
			{
				Unlocks.UnlocksFilename,
				ex2.ToString()
			});
		}
	}

	// Token: 0x060049B6 RID: 18870 RVA: 0x0019CAA0 File Offset: 0x0019ACA0
	public string UnlockNext(string collectionID)
	{
		foreach (string text in this.lockCollections[collectionID])
		{
			if (string.IsNullOrEmpty(text))
			{
				DebugUtil.DevAssertArgs(false, new object[] { "Found null/empty string in Unlocks collection: ", collectionID });
			}
			else if (!this.IsUnlocked(text))
			{
				this.Unlock(text, true);
				return text;
			}
		}
		return null;
	}

	// Token: 0x060049B7 RID: 18871 RVA: 0x0019CB04 File Offset: 0x0019AD04
	private MessageNotification GenerateCodexUnlockNotification(string lockID)
	{
		string entryForLock = CodexCache.GetEntryForLock(lockID);
		if (string.IsNullOrEmpty(entryForLock))
		{
			return null;
		}
		string text = null;
		if (CodexCache.FindSubEntry(lockID) != null)
		{
			text = CodexCache.FindSubEntry(lockID).title;
		}
		else if (CodexCache.FindSubEntry(entryForLock) != null)
		{
			text = CodexCache.FindSubEntry(entryForLock).title;
		}
		else if (CodexCache.FindEntry(entryForLock) != null)
		{
			text = CodexCache.FindEntry(entryForLock).title;
		}
		string text2 = UI.FormatAsLink(Strings.Get(text), entryForLock);
		if (!string.IsNullOrEmpty(text))
		{
			ContentContainer contentContainer = CodexCache.FindEntry(entryForLock).contentContainers.Find((ContentContainer match) => match.lockID == lockID);
			if (contentContainer != null)
			{
				foreach (ICodexWidget codexWidget in contentContainer.content)
				{
					CodexText codexText = codexWidget as CodexText;
					if (codexText != null)
					{
						text2 = text2 + "\n\n" + codexText.text;
					}
				}
			}
			return new MessageNotification(new CodexUnlockedMessage(lockID, text2));
		}
		return null;
	}

	// Token: 0x060049B8 RID: 18872 RVA: 0x0019CC30 File Offset: 0x0019AE30
	private void UnlockCycleCodexes()
	{
		foreach (KeyValuePair<int, string> keyValuePair in this.cycleLocked)
		{
			if (GameClock.Instance.GetCycle() + 1 >= keyValuePair.Key)
			{
				this.Unlock(keyValuePair.Value, true);
			}
		}
	}

	// Token: 0x060049B9 RID: 18873 RVA: 0x0019CCA0 File Offset: 0x0019AEA0
	private void OnNewDay(object data)
	{
		this.UnlockCycleCodexes();
	}

	// Token: 0x060049BA RID: 18874 RVA: 0x0019CCA8 File Offset: 0x0019AEA8
	private void OnLaunchRocket(object data)
	{
		this.Unlock("surfacebreach", true);
		this.Unlock("firstrocketlaunch", true);
	}

	// Token: 0x060049BB RID: 18875 RVA: 0x0019CCC2 File Offset: 0x0019AEC2
	private void OnDuplicantDied(object data)
	{
		this.Unlock("duplicantdeath", true);
		if (Components.LiveMinionIdentities.Count == 1)
		{
			this.Unlock("onedupeleft", true);
		}
	}

	// Token: 0x060049BC RID: 18876 RVA: 0x0019CCE9 File Offset: 0x0019AEE9
	private void OnNewDupe(MinionIdentity minion_identity)
	{
		if (Components.LiveMinionIdentities.Count >= Db.Get().Personalities.GetAll(true, false).Count)
		{
			this.Unlock("fulldupecolony", true);
		}
	}

	// Token: 0x060049BD RID: 18877 RVA: 0x0019CD19 File Offset: 0x0019AF19
	private void OnDiscoveredSpace(object data)
	{
		this.Unlock("surfacebreach", true);
	}

	// Token: 0x060049BE RID: 18878 RVA: 0x0019CD28 File Offset: 0x0019AF28
	public void Sim4000ms(float dt)
	{
		int num = int.MinValue;
		int num2 = int.MinValue;
		int num3 = int.MaxValue;
		int num4 = int.MaxValue;
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
		{
			if (!(minionIdentity == null))
			{
				int num5 = Grid.PosToCell(minionIdentity);
				if (Grid.IsValidCell(num5))
				{
					int num6;
					int num7;
					Grid.CellToXY(num5, out num6, out num7);
					if (num7 > num2)
					{
						num2 = num7;
						num = num6;
					}
					if (num7 < num4)
					{
						num3 = num6;
						num4 = num7;
					}
				}
			}
		}
		if (num2 != -2147483648)
		{
			int num8 = num2;
			for (int i = 0; i < 30; i++)
			{
				num8++;
				int num9 = Grid.XYToCell(num, num8);
				if (!Grid.IsValidCell(num9))
				{
					break;
				}
				if (global::World.Instance.zoneRenderData.GetSubWorldZoneType(num9) == SubWorld.ZoneType.Space)
				{
					this.Unlock("nearingsurface", true);
					break;
				}
			}
		}
		if (num4 != 2147483647)
		{
			int num10 = num4;
			for (int j = 0; j < 30; j++)
			{
				num10--;
				int num11 = Grid.XYToCell(num3, num10);
				if (!Grid.IsValidCell(num11))
				{
					break;
				}
				if (global::World.Instance.zoneRenderData.GetSubWorldZoneType(num11) == SubWorld.ZoneType.ToxicJungle && Grid.Element[num11].id == SimHashes.Magma)
				{
					this.Unlock("nearingmagma", true);
					return;
				}
			}
		}
	}

	// Token: 0x04003066 RID: 12390
	private const int FILE_IO_RETRY_ATTEMPTS = 5;

	// Token: 0x04003067 RID: 12391
	private List<string> unlocked = new List<string>();

	// Token: 0x04003068 RID: 12392
	private List<Unlocks.MetaUnlockCategory> MetaUnlockCategories = new List<Unlocks.MetaUnlockCategory>
	{
		new Unlocks.MetaUnlockCategory("dimensionalloreMeta", "dimensionallore", 4)
	};

	// Token: 0x04003069 RID: 12393
	public Dictionary<string, string[]> lockCollections = new Dictionary<string, string[]>
	{
		{
			"emails",
			new string[]
			{
				"email_thermodynamiclaws", "email_security2", "email_pens2", "email_atomiconrecruitment", "email_devonsblog", "email_researchgiant", "email_thejanitor", "email_newemployee", "email_timeoffapproved", "email_security3",
				"email_preliminarycalculations", "email_hollandsdog", "email_temporalbowupdate", "email_retemporalbowupdate", "email_memorychip", "email_arthistoryrequest", "email_AIcontrol", "email_AIcontrol2", "email_friendlyemail", "email_AIcontrol3",
				"email_AIcontrol4", "email_engineeringcandidate", "email_missingnotes", "email_journalistrequest", "email_journalistrequest2"
			}
		},
		{
			"journals",
			new string[]
			{
				"journal_timesarrowthoughts", "journal_A046_1", "journal_B835_1", "journal_sunflowerseeds", "journal_B327_1", "journal_B556_1", "journal_employeeprocessing", "journal_B327_2", "journal_A046_2", "journal_elliesbirthday1",
				"journal_B835_2", "journal_ants", "journal_pipedream", "journal_B556_2", "journal_movedrats", "journal_B835_3", "journal_A046_3", "journal_B556_3", "journal_B327_3", "journal_B835_4",
				"journal_cleanup", "journal_A046_4", "journal_B327_4", "journal_revisitednumbers", "journal_B556_4", "journal_B835_5", "journal_elliesbirthday2", "journal_B111_1", "journal_revisitednumbers2", "journal_timemusings",
				"journal_evil", "journal_timesorder", "journal_inspace", "journal_mysteryaward", "journal_courier"
			}
		},
		{
			"researchnotes",
			new string[]
			{
				"notes_clonedrats", "notes_agriculture1", "notes_husbandry1", "notes_hibiscus3", "notes_husbandry2", "notes_agriculture2", "notes_geneticooze", "notes_agriculture3", "notes_husbandry3", "notes_memoryimplantation",
				"notes_husbandry4", "notes_agriculture4", "notes_neutronium", "notes_firstsuccess", "notes_neutroniumapplications", "notes_teleportation", "notes_AI", "cryotank_warning"
			}
		},
		{
			"misc",
			new string[] { "misc_newsecurity", "misc_mailroometiquette", "misc_unattendedcultures", "misc_politerequest", "misc_casualfriday", "misc_dishbot" }
		},
		{
			"dimensionallore",
			new string[] { "notes_clonedrabbits", "notes_clonedraccoons", "journal_movedrabbits", "journal_movedraccoons", "journal_strawberries", "journal_shrimp" }
		},
		{
			"dimensionalloreMeta",
			new string[] { "log9" }
		},
		{
			"space",
			new string[] { "display_spaceprop1", "notice_pilot", "journal_inspace", "notes_firstcolony" }
		},
		{
			"storytraits",
			new string[] { "story_trait_critter_manipulator_initial", "story_trait_critter_manipulator_complete", "storytrait_crittermanipulator_workiversary", "story_trait_mega_brain_tank_initial", "story_trait_mega_brain_tank_competed" }
		}
	};

	// Token: 0x0400306A RID: 12394
	public Dictionary<int, string> cycleLocked = new Dictionary<int, string>
	{
		{ 0, "log1" },
		{ 3, "log2" },
		{ 15, "log3" },
		{ 1000, "log4" },
		{ 1500, "log4b" },
		{ 2000, "log5" },
		{ 2500, "log5b" },
		{ 3000, "log6" },
		{ 3500, "log6b" },
		{ 4000, "log7" },
		{ 4001, "log8" }
	};

	// Token: 0x0400306B RID: 12395
	private static readonly EventSystem.IntraObjectHandler<Unlocks> OnLaunchRocketDelegate = new EventSystem.IntraObjectHandler<Unlocks>(delegate(Unlocks component, object data)
	{
		component.OnLaunchRocket(data);
	});

	// Token: 0x0400306C RID: 12396
	private static readonly EventSystem.IntraObjectHandler<Unlocks> OnDuplicantDiedDelegate = new EventSystem.IntraObjectHandler<Unlocks>(delegate(Unlocks component, object data)
	{
		component.OnDuplicantDied(data);
	});

	// Token: 0x0400306D RID: 12397
	private static readonly EventSystem.IntraObjectHandler<Unlocks> OnDiscoveredSpaceDelegate = new EventSystem.IntraObjectHandler<Unlocks>(delegate(Unlocks component, object data)
	{
		component.OnDiscoveredSpace(data);
	});

	// Token: 0x020017B5 RID: 6069
	private class MetaUnlockCategory
	{
		// Token: 0x06008BA5 RID: 35749 RVA: 0x003002A8 File Offset: 0x002FE4A8
		public MetaUnlockCategory(string metaCollectionID, string mesaCollectionID, int mesaUnlockCount)
		{
			this.metaCollectionID = metaCollectionID;
			this.mesaCollectionID = mesaCollectionID;
			this.mesaUnlockCount = mesaUnlockCount;
		}

		// Token: 0x04006DD0 RID: 28112
		public string metaCollectionID;

		// Token: 0x04006DD1 RID: 28113
		public string mesaCollectionID;

		// Token: 0x04006DD2 RID: 28114
		public int mesaUnlockCount;
	}
}
