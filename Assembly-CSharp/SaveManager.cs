using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KSerialization;
using UnityEngine;

// Token: 0x020004C8 RID: 1224
[AddComponentMenu("KMonoBehaviour/scripts/SaveManager")]
public class SaveManager : KMonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06001C5D RID: 7261 RVA: 0x0009723C File Offset: 0x0009543C
	// (remove) Token: 0x06001C5E RID: 7262 RVA: 0x00097274 File Offset: 0x00095474
	public event Action<SaveLoadRoot> onRegister;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06001C5F RID: 7263 RVA: 0x000972AC File Offset: 0x000954AC
	// (remove) Token: 0x06001C60 RID: 7264 RVA: 0x000972E4 File Offset: 0x000954E4
	public event Action<SaveLoadRoot> onUnregister;

	// Token: 0x06001C61 RID: 7265 RVA: 0x00097319 File Offset: 0x00095519
	protected override void OnPrefabInit()
	{
		Assets.RegisterOnAddPrefab(new Action<KPrefabID>(this.OnAddPrefab));
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x0009732C File Offset: 0x0009552C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Assets.UnregisterOnAddPrefab(new Action<KPrefabID>(this.OnAddPrefab));
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x00097348 File Offset: 0x00095548
	private void OnAddPrefab(KPrefabID prefab)
	{
		if (prefab == null)
		{
			return;
		}
		Tag saveLoadTag = prefab.GetSaveLoadTag();
		this.prefabMap[saveLoadTag] = prefab.gameObject;
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x00097378 File Offset: 0x00095578
	public Dictionary<Tag, List<SaveLoadRoot>> GetLists()
	{
		return this.sceneObjects;
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x00097380 File Offset: 0x00095580
	private List<SaveLoadRoot> GetSaveLoadRootList(SaveLoadRoot saver)
	{
		KPrefabID component = saver.GetComponent<KPrefabID>();
		if (component == null)
		{
			DebugUtil.LogErrorArgs(saver.gameObject, new object[]
			{
				"All savers must also have a KPrefabID on them but",
				saver.gameObject.name,
				"does not have one."
			});
			return null;
		}
		List<SaveLoadRoot> list;
		if (!this.sceneObjects.TryGetValue(component.GetSaveLoadTag(), out list))
		{
			list = new List<SaveLoadRoot>();
			this.sceneObjects[component.GetSaveLoadTag()] = list;
		}
		return list;
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x000973FC File Offset: 0x000955FC
	public void Register(SaveLoadRoot root)
	{
		List<SaveLoadRoot> saveLoadRootList = this.GetSaveLoadRootList(root);
		if (saveLoadRootList == null)
		{
			return;
		}
		saveLoadRootList.Add(root);
		if (this.onRegister != null)
		{
			this.onRegister(root);
		}
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x00097430 File Offset: 0x00095630
	public void Unregister(SaveLoadRoot root)
	{
		if (this.onRegister != null)
		{
			this.onUnregister(root);
		}
		List<SaveLoadRoot> saveLoadRootList = this.GetSaveLoadRootList(root);
		if (saveLoadRootList == null)
		{
			return;
		}
		saveLoadRootList.Remove(root);
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x00097468 File Offset: 0x00095668
	public GameObject GetPrefab(Tag tag)
	{
		GameObject gameObject = null;
		if (this.prefabMap.TryGetValue(tag, out gameObject))
		{
			return gameObject;
		}
		DebugUtil.LogArgs(new object[]
		{
			"Item not found in prefabMap",
			"[" + tag.Name + "]"
		});
		return null;
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x000974B8 File Offset: 0x000956B8
	public void Save(BinaryWriter writer)
	{
		writer.Write(SaveManager.SAVE_HEADER);
		writer.Write(7);
		writer.Write(31);
		int num = 0;
		foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in this.sceneObjects)
		{
			if (keyValuePair.Value.Count > 0)
			{
				num++;
			}
		}
		writer.Write(num);
		this.orderedKeys.Clear();
		this.orderedKeys.AddRange(this.sceneObjects.Keys);
		this.orderedKeys.Remove(SaveGame.Instance.PrefabID());
		this.orderedKeys = this.orderedKeys.OrderBy((Tag a) => a.Name == "StickerBomb").ToList<Tag>();
		this.orderedKeys = this.orderedKeys.OrderBy((Tag a) => a.Name.Contains("UnderConstruction")).ToList<Tag>();
		this.Write(SaveGame.Instance.PrefabID(), new List<SaveLoadRoot>(new SaveLoadRoot[] { SaveGame.Instance.GetComponent<SaveLoadRoot>() }), writer);
		foreach (Tag tag in this.orderedKeys)
		{
			List<SaveLoadRoot> list = this.sceneObjects[tag];
			if (list.Count > 0)
			{
				foreach (SaveLoadRoot saveLoadRoot in list)
				{
					if (!(saveLoadRoot == null) && saveLoadRoot.GetComponent<SimCellOccupier>() != null)
					{
						this.Write(tag, list, writer);
						break;
					}
				}
			}
		}
		foreach (Tag tag2 in this.orderedKeys)
		{
			List<SaveLoadRoot> list2 = this.sceneObjects[tag2];
			if (list2.Count > 0)
			{
				foreach (SaveLoadRoot saveLoadRoot2 in list2)
				{
					if (!(saveLoadRoot2 == null) && saveLoadRoot2.GetComponent<SimCellOccupier>() == null)
					{
						this.Write(tag2, list2, writer);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x0009776C File Offset: 0x0009596C
	private void Write(Tag key, List<SaveLoadRoot> value, BinaryWriter writer)
	{
		int count = value.Count;
		Tag tag = key;
		writer.WriteKleiString(tag.Name);
		writer.Write(count);
		long position = writer.BaseStream.Position;
		int num = -1;
		writer.Write(num);
		long position2 = writer.BaseStream.Position;
		foreach (SaveLoadRoot saveLoadRoot in value)
		{
			if (saveLoadRoot != null)
			{
				saveLoadRoot.Save(writer);
			}
			else
			{
				DebugUtil.LogWarningArgs(new object[] { "Null game object when saving" });
			}
		}
		long position3 = writer.BaseStream.Position;
		long num2 = position3 - position2;
		writer.BaseStream.Position = position;
		writer.Write((int)num2);
		writer.BaseStream.Position = position3;
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x00097854 File Offset: 0x00095A54
	public bool Load(IReader reader)
	{
		char[] array = reader.ReadChars(SaveManager.SAVE_HEADER.Length);
		if (array == null || array.Length != SaveManager.SAVE_HEADER.Length)
		{
			return false;
		}
		for (int i = 0; i < SaveManager.SAVE_HEADER.Length; i++)
		{
			if (array[i] != SaveManager.SAVE_HEADER[i])
			{
				return false;
			}
		}
		int num = reader.ReadInt32();
		int num2 = reader.ReadInt32();
		if (num != 7 || num2 > 31)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("SAVE FILE VERSION MISMATCH! Expected {0}.{1} but got {2}.{3}", new object[] { 7, 31, num, num2 }) });
			return false;
		}
		this.ClearScene();
		try
		{
			int num3 = reader.ReadInt32();
			for (int j = 0; j < num3; j++)
			{
				string text = reader.ReadKleiString();
				int num4 = reader.ReadInt32();
				int num5 = reader.ReadInt32();
				Tag tag = TagManager.Create(text);
				GameObject gameObject;
				if (!this.prefabMap.TryGetValue(tag, out gameObject))
				{
					DebugUtil.LogWarningArgs(new object[] { "Could not find prefab '" + text + "'" });
					reader.SkipBytes(num5);
				}
				else
				{
					List<SaveLoadRoot> list = new List<SaveLoadRoot>(num4);
					this.sceneObjects[tag] = list;
					for (int k = 0; k < num4; k++)
					{
						SaveLoadRoot saveLoadRoot = SaveLoadRoot.Load(gameObject, reader);
						if (SaveManager.DEBUG_OnlyLoadThisCellsObjects == -1 && saveLoadRoot == null)
						{
							global::Debug.LogError("Error loading data [" + text + "]");
							return false;
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				"Error deserializing prefabs\n\n",
				ex.ToString()
			});
			throw ex;
		}
		return true;
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x00097A18 File Offset: 0x00095C18
	private void ClearScene()
	{
		foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in this.sceneObjects)
		{
			foreach (SaveLoadRoot saveLoadRoot in keyValuePair.Value)
			{
				UnityEngine.Object.Destroy(saveLoadRoot.gameObject);
			}
		}
		this.sceneObjects.Clear();
	}

	// Token: 0x04000FE2 RID: 4066
	public const int SAVE_MAJOR_VERSION_LAST_UNDOCUMENTED = 7;

	// Token: 0x04000FE3 RID: 4067
	public const int SAVE_MAJOR_VERSION = 7;

	// Token: 0x04000FE4 RID: 4068
	public const int SAVE_MINOR_VERSION_EXPLICIT_VALUE_TYPES = 4;

	// Token: 0x04000FE5 RID: 4069
	public const int SAVE_MINOR_VERSION_LAST_UNDOCUMENTED = 7;

	// Token: 0x04000FE6 RID: 4070
	public const int SAVE_MINOR_VERSION_MOD_IDENTIFIER = 8;

	// Token: 0x04000FE7 RID: 4071
	public const int SAVE_MINOR_VERSION_FINITE_SPACE_RESOURCES = 9;

	// Token: 0x04000FE8 RID: 4072
	public const int SAVE_MINOR_VERSION_COLONY_REQ_ACHIEVEMENTS = 10;

	// Token: 0x04000FE9 RID: 4073
	public const int SAVE_MINOR_VERSION_TRACK_NAV_DISTANCE = 11;

	// Token: 0x04000FEA RID: 4074
	public const int SAVE_MINOR_VERSION_EXPANDED_WORLD_INFO = 12;

	// Token: 0x04000FEB RID: 4075
	public const int SAVE_MINOR_VERSION_BASIC_COMFORTS_FIX = 13;

	// Token: 0x04000FEC RID: 4076
	public const int SAVE_MINOR_VERSION_PLATFORM_TRAIT_NAMES = 14;

	// Token: 0x04000FED RID: 4077
	public const int SAVE_MINOR_VERSION_ADD_JOY_REACTIONS = 15;

	// Token: 0x04000FEE RID: 4078
	public const int SAVE_MINOR_VERSION_NEW_AUTOMATION_WARNING = 16;

	// Token: 0x04000FEF RID: 4079
	public const int SAVE_MINOR_VERSION_ADD_GUID_TO_HEADER = 17;

	// Token: 0x04000FF0 RID: 4080
	public const int SAVE_MINOR_VERSION_EXPANSION_1_INTRODUCED = 20;

	// Token: 0x04000FF1 RID: 4081
	public const int SAVE_MINOR_VERSION_CONTENT_SETTINGS = 21;

	// Token: 0x04000FF2 RID: 4082
	public const int SAVE_MINOR_VERSION_COLONY_REQ_REMOVE_SERIALIZATION = 22;

	// Token: 0x04000FF3 RID: 4083
	public const int SAVE_MINOR_VERSION_ROTTABLE_TUNING = 23;

	// Token: 0x04000FF4 RID: 4084
	public const int SAVE_MINOR_VERSION_LAUNCH_PAD_SOLIDITY = 24;

	// Token: 0x04000FF5 RID: 4085
	public const int SAVE_MINOR_VERSION_BASE_GAME_MERGEDOWN = 25;

	// Token: 0x04000FF6 RID: 4086
	public const int SAVE_MINOR_VERSION_FALLING_WATER_WORLDIDX_SERIALIZATION = 26;

	// Token: 0x04000FF7 RID: 4087
	public const int SAVE_MINOR_VERSION_ROCKET_RANGE_REBALANCE = 27;

	// Token: 0x04000FF8 RID: 4088
	public const int SAVE_MINOR_VERSION_ENTITIES_WRONG_LAYER = 28;

	// Token: 0x04000FF9 RID: 4089
	public const int SAVE_MINOR_VERSION_TAGBITS_REWORK = 29;

	// Token: 0x04000FFA RID: 4090
	public const int SAVE_MINOR_VERSION_ACCESSORY_SLOT_UPGRADE = 30;

	// Token: 0x04000FFB RID: 4091
	public const int SAVE_MINOR_VERSION_GEYSER_CAN_BE_RENAMED = 31;

	// Token: 0x04000FFC RID: 4092
	public const int SAVE_MINOR_VERSION = 31;

	// Token: 0x04000FFD RID: 4093
	private Dictionary<Tag, GameObject> prefabMap = new Dictionary<Tag, GameObject>();

	// Token: 0x04000FFE RID: 4094
	private Dictionary<Tag, List<SaveLoadRoot>> sceneObjects = new Dictionary<Tag, List<SaveLoadRoot>>();

	// Token: 0x04001001 RID: 4097
	public static int DEBUG_OnlyLoadThisCellsObjects = -1;

	// Token: 0x04001002 RID: 4098
	private static readonly char[] SAVE_HEADER = new char[] { 'K', 'S', 'A', 'V' };

	// Token: 0x04001003 RID: 4099
	private List<Tag> orderedKeys = new List<Tag>();

	// Token: 0x0200110E RID: 4366
	private enum BoundaryTag : uint
	{
		// Token: 0x040059A9 RID: 22953
		Component = 3735928559U,
		// Token: 0x040059AA RID: 22954
		Prefab = 3131961357U,
		// Token: 0x040059AB RID: 22955
		Complete = 3735929054U
	}
}
