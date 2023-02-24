using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Klei;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class TuningSystem
{
	// Token: 0x0600093A RID: 2362 RVA: 0x00024990 File Offset: 0x00022B90
	public static void Init()
	{
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00024992 File Offset: 0x00022B92
	static TuningSystem()
	{
		TuningSystem.InitializeTuning();
		TuningSystem.ListenForFileChanges();
		TuningSystem.Load();
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x000249C8 File Offset: 0x00022BC8
	private static void ListenForFileChanges()
	{
		string directoryName = Path.GetDirectoryName(TuningSystem._TuningPath);
		try
		{
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
			fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
			fileSystemWatcher.Changed += TuningSystem.OnFileChanged;
			fileSystemWatcher.Path = directoryName;
			fileSystemWatcher.Filter = Path.GetFileName(TuningSystem._TuningPath);
			fileSystemWatcher.EnableRaisingEvents = true;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Error when attempting to monitor path: " + directoryName + "\n" + ex.ToString());
		}
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00024A4C File Offset: 0x00022C4C
	private static void OnFileChanged(object source, FileSystemEventArgs e)
	{
		TuningSystem.Load();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00024A54 File Offset: 0x00022C54
	private static void InitializeTuning()
	{
		TuningSystem._TuningValues = new Dictionary<Type, object>();
		foreach (Type type in App.GetCurrentDomainTypes())
		{
			Type baseType = type.BaseType;
			if (!(baseType == null) && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(TuningData<>))
			{
				object obj = Activator.CreateInstance(baseType.GetGenericArguments()[0]);
				TuningSystem._TuningValues[obj.GetType()] = obj;
				baseType.GetField("_TuningData", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).SetValue(null, obj);
			}
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00024B0C File Offset: 0x00022D0C
	private static void Save()
	{
		if (TuningSystem._TuningValues == null)
		{
			return;
		}
		JsonSerializer jsonSerializer = JsonSerializer.Create(TuningSystem._SerializationSettings);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (KeyValuePair<Type, object> keyValuePair in TuningSystem._TuningValues)
		{
			dictionary[keyValuePair.Value.GetType().FullName] = keyValuePair.Value;
		}
		using (StreamWriter streamWriter = File.CreateText(TuningSystem._TuningPath))
		{
			jsonSerializer.Serialize(streamWriter, dictionary);
		}
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00024BC0 File Offset: 0x00022DC0
	private static void Load()
	{
		string[] array = new string[]
		{
			TuningSystem._TuningPath,
			string.Empty
		};
		if (Thread.CurrentThread == KProfiler.main_thread)
		{
			array[1] = Path.Combine(Application.dataPath, "Tuning.json");
		}
		foreach (string text in array)
		{
			if (!string.IsNullOrEmpty(text) && FileSystem.FileExists(text))
			{
				foreach (KeyValuePair<string, object> keyValuePair in JsonConvert.DeserializeObject<Dictionary<string, object>>(FileSystem.ConvertToText(FileSystem.ReadBytes(text))))
				{
					Type type = null;
					Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
					for (int j = 0; j < assemblies.Length; j++)
					{
						type = assemblies[j].GetType(keyValuePair.Key);
						if (type != null)
						{
							break;
						}
					}
					if (type != null)
					{
						if (TuningSystem._TuningValues.ContainsKey(type))
						{
							JsonConvert.PopulateObject(keyValuePair.Value.ToString(), TuningSystem._TuningValues[type]);
						}
						else
						{
							object obj = JsonConvert.DeserializeObject(keyValuePair.Value.ToString(), type);
							TuningSystem._TuningValues[type] = obj;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00024D1C File Offset: 0x00022F1C
	private static bool IsLoaded()
	{
		return TuningSystem._TuningValues != null;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00024D26 File Offset: 0x00022F26
	public static Dictionary<Type, object> GetAllTuningValues()
	{
		return TuningSystem._TuningValues;
	}

	// Token: 0x04000687 RID: 1671
	private static JsonSerializerSettings _SerializationSettings = new JsonSerializerSettings
	{
		Formatting = Formatting.Indented
	};

	// Token: 0x04000688 RID: 1672
	private static Dictionary<Type, object> _TuningValues;

	// Token: 0x04000689 RID: 1673
	private static string _TuningPath = Path.Combine(Application.streamingAssetsPath, "Tuning.json");
}
