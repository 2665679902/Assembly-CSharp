﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x020008E7 RID: 2279
public static class RetireColonyUtility
{
	// Token: 0x0600419B RID: 16795 RVA: 0x0016F810 File Offset: 0x0016DA10
	public static bool SaveColonySummaryData()
	{
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		string text = Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName());
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string text2 = RetireColonyUtility.StripInvalidCharacters(SaveGame.Instance.BaseName);
		string text3 = Path.Combine(text, text2);
		if (!Directory.Exists(text3))
		{
			Directory.CreateDirectory(text3);
		}
		string text4 = Path.Combine(text3, text2 + ".json");
		string text5 = JsonConvert.SerializeObject(RetireColonyUtility.GetCurrentColonyRetiredColonyData());
		if (DlcManager.IsExpansion1Active())
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (worldContainer.IsDiscovered && !worldContainer.IsModuleInterior)
				{
					string name = worldContainer.GetComponent<ClusterGridEntity>().Name;
					string text6 = Path.Combine(text3, name);
					string text7 = Path.Combine(text3, worldContainer.id.ToString("D5"));
					if (Directory.Exists(text6))
					{
						bool flag = Directory.GetFiles(text6).Length != 0;
						if (!Directory.Exists(text7))
						{
							Directory.CreateDirectory(text7);
						}
						foreach (string text8 in Directory.GetFiles(text6))
						{
							try
							{
								File.Copy(text8, text8.Replace(text6, text7), true);
								File.Delete(text8);
							}
							catch (Exception ex)
							{
								flag = false;
								global::Debug.LogWarning("Error occurred trying to migrate screenshot: " + text8);
								global::Debug.LogWarning(ex);
							}
						}
						if (flag)
						{
							Directory.Delete(text6);
						}
					}
				}
			}
		}
		bool flag2 = false;
		int num = 0;
		while (!flag2 && num < 5)
		{
			try
			{
				Thread.Sleep(num * 100);
				using (FileStream fileStream = File.Open(text4, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
				{
					flag2 = true;
					byte[] bytes = Encoding.UTF8.GetBytes(text5);
					fileStream.Write(bytes, 0, bytes.Length);
				}
			}
			catch (Exception ex2)
			{
				global::Debug.LogWarningFormat("SaveColonySummaryData failed attempt {0}: {1}", new object[]
				{
					num + 1,
					ex2.ToString()
				});
			}
			num++;
		}
		return flag2;
	}

	// Token: 0x0600419C RID: 16796 RVA: 0x0016FA70 File Offset: 0x0016DC70
	public static RetiredColonyData GetCurrentColonyRetiredColonyData()
	{
		List<MinionAssignablesProxy> list = new List<MinionAssignablesProxy>();
		for (int i = 0; i < Components.MinionAssignablesProxy.Count; i++)
		{
			if (Components.MinionAssignablesProxy[i] != null)
			{
				list.Add(Components.MinionAssignablesProxy[i]);
			}
		}
		List<string> list2 = new List<string>();
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in SaveGame.Instance.GetComponent<ColonyAchievementTracker>().achievements)
		{
			if (keyValuePair.Value.success)
			{
				list2.Add(keyValuePair.Key);
			}
		}
		BuildingComplete[] array = new BuildingComplete[Components.BuildingCompletes.Count];
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = Components.BuildingCompletes[j];
		}
		string text = null;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer.IsDiscovered && !worldContainer.IsModuleInterior)
			{
				dictionary.Add(worldContainer.id.ToString("D5"), worldContainer.worldName);
				if (worldContainer.IsStartWorld)
				{
					text = worldContainer.id.ToString("D5");
				}
			}
		}
		return new RetiredColonyData(SaveGame.Instance.BaseName, GameClock.Instance.GetCycle(), System.DateTime.Now.ToShortDateString(), list2.ToArray(), list.ToArray(), array, text, dictionary);
	}

	// Token: 0x0600419D RID: 16797 RVA: 0x0016FC2C File Offset: 0x0016DE2C
	private static RetiredColonyData LoadRetiredColony(string file, bool skipStats, Encoding enc)
	{
		RetiredColonyData retiredColonyData = new RetiredColonyData();
		using (FileStream fileStream = File.Open(file, FileMode.Open))
		{
			using (StreamReader streamReader = new StreamReader(fileStream, enc))
			{
				using (JsonReader jsonReader = new JsonTextReader(streamReader))
				{
					string text = string.Empty;
					List<string> list = new List<string>();
					List<global::Tuple<string, int>> list2 = new List<global::Tuple<string, int>>();
					List<RetiredColonyData.RetiredDuplicantData> list3 = new List<RetiredColonyData.RetiredDuplicantData>();
					List<RetiredColonyData.RetiredColonyStatistic> list4 = new List<RetiredColonyData.RetiredColonyStatistic>();
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					while (jsonReader.Read())
					{
						JsonToken jsonToken = jsonReader.TokenType;
						if (jsonToken == JsonToken.PropertyName)
						{
							text = jsonReader.Value.ToString();
						}
						if (jsonToken == JsonToken.String && text == "colonyName")
						{
							retiredColonyData.colonyName = jsonReader.Value.ToString();
						}
						if (jsonToken == JsonToken.String && text == "date")
						{
							retiredColonyData.date = jsonReader.Value.ToString();
						}
						if (jsonToken == JsonToken.Integer && text == "cycleCount")
						{
							retiredColonyData.cycleCount = int.Parse(jsonReader.Value.ToString());
						}
						if (jsonToken == JsonToken.String && text == "achievements")
						{
							list.Add(jsonReader.Value.ToString());
						}
						if (jsonToken == JsonToken.StartObject && text == "Duplicants")
						{
							string text2 = null;
							RetiredColonyData.RetiredDuplicantData retiredDuplicantData = new RetiredColonyData.RetiredDuplicantData();
							retiredDuplicantData.accessories = new Dictionary<string, string>();
							while (jsonReader.Read())
							{
								jsonToken = jsonReader.TokenType;
								if (jsonToken == JsonToken.EndObject)
								{
									break;
								}
								if (jsonToken == JsonToken.PropertyName)
								{
									text2 = jsonReader.Value.ToString();
								}
								if (text2 == "name" && jsonToken == JsonToken.String)
								{
									retiredDuplicantData.name = jsonReader.Value.ToString();
								}
								if (text2 == "age" && jsonToken == JsonToken.Integer)
								{
									retiredDuplicantData.age = int.Parse(jsonReader.Value.ToString());
								}
								if (text2 == "skillPointsGained" && jsonToken == JsonToken.Integer)
								{
									retiredDuplicantData.skillPointsGained = int.Parse(jsonReader.Value.ToString());
								}
								if (text2 == "accessories")
								{
									string text3 = null;
									while (jsonReader.Read())
									{
										jsonToken = jsonReader.TokenType;
										if (jsonToken == JsonToken.EndObject)
										{
											break;
										}
										if (jsonToken == JsonToken.PropertyName)
										{
											text3 = jsonReader.Value.ToString();
										}
										if (text3 != null && jsonReader.Value != null && jsonToken == JsonToken.String)
										{
											string text4 = jsonReader.Value.ToString();
											retiredDuplicantData.accessories.Add(text3, text4);
										}
									}
								}
							}
							list3.Add(retiredDuplicantData);
						}
						if (jsonToken == JsonToken.StartObject && text == "buildings")
						{
							string text5 = null;
							string text6 = null;
							int num = 0;
							while (jsonReader.Read())
							{
								jsonToken = jsonReader.TokenType;
								if (jsonToken == JsonToken.EndObject)
								{
									break;
								}
								if (jsonToken == JsonToken.PropertyName)
								{
									text5 = jsonReader.Value.ToString();
								}
								if (text5 == "first" && jsonToken == JsonToken.String)
								{
									text6 = jsonReader.Value.ToString();
								}
								if (text5 == "second" && jsonToken == JsonToken.Integer)
								{
									num = int.Parse(jsonReader.Value.ToString());
								}
							}
							global::Tuple<string, int> tuple = new global::Tuple<string, int>(text6, num);
							list2.Add(tuple);
						}
						if (jsonToken == JsonToken.StartObject && text == "Stats")
						{
							if (skipStats)
							{
								break;
							}
							string text7 = null;
							RetiredColonyData.RetiredColonyStatistic retiredColonyStatistic = new RetiredColonyData.RetiredColonyStatistic();
							List<global::Tuple<float, float>> list5 = new List<global::Tuple<float, float>>();
							while (jsonReader.Read())
							{
								jsonToken = jsonReader.TokenType;
								if (jsonToken == JsonToken.EndObject)
								{
									break;
								}
								if (jsonToken == JsonToken.PropertyName)
								{
									text7 = jsonReader.Value.ToString();
								}
								if (text7 == "id" && jsonToken == JsonToken.String)
								{
									retiredColonyStatistic.id = jsonReader.Value.ToString();
								}
								if (text7 == "name" && jsonToken == JsonToken.String)
								{
									retiredColonyStatistic.name = jsonReader.Value.ToString();
								}
								if (text7 == "nameX" && jsonToken == JsonToken.String)
								{
									retiredColonyStatistic.nameX = jsonReader.Value.ToString();
								}
								if (text7 == "nameY" && jsonToken == JsonToken.String)
								{
									retiredColonyStatistic.nameY = jsonReader.Value.ToString();
								}
								if (text7 == "value" && jsonToken == JsonToken.StartObject)
								{
									string text8 = null;
									float num2 = 0f;
									float num3 = 0f;
									while (jsonReader.Read())
									{
										jsonToken = jsonReader.TokenType;
										if (jsonToken == JsonToken.EndObject)
										{
											break;
										}
										if (jsonToken == JsonToken.PropertyName)
										{
											text8 = jsonReader.Value.ToString();
										}
										if (text8 == "first" && (jsonToken == JsonToken.Float || jsonToken == JsonToken.Integer))
										{
											num2 = float.Parse(jsonReader.Value.ToString());
										}
										if (text8 == "second" && (jsonToken == JsonToken.Float || jsonToken == JsonToken.Integer))
										{
											num3 = float.Parse(jsonReader.Value.ToString());
										}
									}
									global::Tuple<float, float> tuple2 = new global::Tuple<float, float>(num2, num3);
									list5.Add(tuple2);
								}
							}
							retiredColonyStatistic.value = list5.ToArray();
							list4.Add(retiredColonyStatistic);
						}
						if (jsonToken == JsonToken.StartObject && text == "worldIdentities")
						{
							string text9 = null;
							while (jsonReader.Read())
							{
								jsonToken = jsonReader.TokenType;
								if (jsonToken == JsonToken.EndObject)
								{
									break;
								}
								if (jsonToken == JsonToken.PropertyName)
								{
									text9 = jsonReader.Value.ToString();
								}
								if (text9 != null && jsonReader.Value != null && jsonToken == JsonToken.String)
								{
									string text10 = jsonReader.Value.ToString();
									dictionary.Add(text9, text10);
								}
							}
						}
						if (jsonToken == JsonToken.String && text == "startWorld")
						{
							retiredColonyData.startWorld = jsonReader.Value.ToString();
						}
					}
					retiredColonyData.Duplicants = list3.ToArray();
					retiredColonyData.Stats = list4.ToArray();
					retiredColonyData.achievements = list.ToArray();
					retiredColonyData.buildings = list2;
					retiredColonyData.worldIdentities = dictionary;
				}
			}
		}
		return retiredColonyData;
	}

	// Token: 0x0600419E RID: 16798 RVA: 0x0017023C File Offset: 0x0016E43C
	public static RetiredColonyData[] LoadRetiredColonies(bool skipStats = false)
	{
		List<RetiredColonyData> list = new List<RetiredColonyData>();
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		string text = Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName());
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		text = Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName());
		string[] directories = Directory.GetDirectories(text);
		for (int i = 0; i < directories.Length; i++)
		{
			foreach (string text2 in Directory.GetFiles(directories[i]))
			{
				if (text2.EndsWith(".json"))
				{
					for (int k = 0; k < RetireColonyUtility.attempt_encodings.Length; k++)
					{
						Encoding encoding = RetireColonyUtility.attempt_encodings[k];
						try
						{
							RetiredColonyData retiredColonyData = RetireColonyUtility.LoadRetiredColony(text2, skipStats, encoding);
							if (retiredColonyData != null)
							{
								if (retiredColonyData.colonyName == null)
								{
									throw new Exception("data.colonyName was null");
								}
								list.Add(retiredColonyData);
							}
							break;
						}
						catch (Exception ex)
						{
							global::Debug.LogWarningFormat("LoadRetiredColonies failed load {0} [{1}]: {2}", new object[]
							{
								encoding,
								text2,
								ex.ToString()
							});
						}
					}
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x0600419F RID: 16799 RVA: 0x00170374 File Offset: 0x0016E574
	public static string[] LoadColonySlideshowFiles(string colonyName, string world_name)
	{
		string text = RetireColonyUtility.StripInvalidCharacters(colonyName);
		string text2 = Path.Combine(Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName()), text);
		if (!world_name.IsNullOrWhiteSpace())
		{
			text2 = Path.Combine(text2, world_name);
		}
		List<string> list = new List<string>();
		if (Directory.Exists(text2))
		{
			foreach (string text3 in Directory.GetFiles(text2))
			{
				if (text3.EndsWith(".png"))
				{
					list.Add(text3);
				}
			}
		}
		else
		{
			global::Debug.LogWarningFormat("LoadColonySlideshow path does not exist or is not directory [{0}]", new object[] { text2 });
		}
		return list.ToArray();
	}

	// Token: 0x060041A0 RID: 16800 RVA: 0x00170410 File Offset: 0x0016E610
	public static Sprite[] LoadColonySlideshow(string colonyName)
	{
		string text = RetireColonyUtility.StripInvalidCharacters(colonyName);
		string text2 = Path.Combine(Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName()), text);
		List<Sprite> list = new List<Sprite>();
		if (Directory.Exists(text2))
		{
			foreach (string text3 in Directory.GetFiles(text2))
			{
				if (text3.EndsWith(".png"))
				{
					Texture2D texture2D = new Texture2D(512, 768);
					texture2D.filterMode = FilterMode.Point;
					texture2D.LoadImage(File.ReadAllBytes(text3));
					list.Add(Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2((float)texture2D.width, (float)texture2D.height)), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect));
				}
			}
		}
		else
		{
			global::Debug.LogWarningFormat("LoadColonySlideshow path does not exist or is not directory [{0}]", new object[] { text2 });
		}
		return list.ToArray();
	}

	// Token: 0x060041A1 RID: 16801 RVA: 0x00170504 File Offset: 0x0016E704
	public static Sprite LoadRetiredColonyPreview(string colonyName, string startName = null)
	{
		try
		{
			string text = RetireColonyUtility.StripInvalidCharacters(colonyName);
			string text2 = Path.Combine(Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName()), text);
			if (!startName.IsNullOrWhiteSpace())
			{
				text2 = Path.Combine(text2, startName);
			}
			List<string> list = new List<string>();
			if (Directory.Exists(text2))
			{
				foreach (string text3 in Directory.GetFiles(text2))
				{
					if (text3.EndsWith(".png"))
					{
						list.Add(text3);
					}
				}
			}
			if (list.Count > 0)
			{
				Texture2D texture2D = new Texture2D(512, 768);
				string text4 = list[list.Count - 1];
				if (!texture2D.LoadImage(File.ReadAllBytes(text4)))
				{
					return null;
				}
				if (texture2D.width > SystemInfo.maxTextureSize || texture2D.height > SystemInfo.maxTextureSize)
				{
					return null;
				}
				if (texture2D.width == 0 || texture2D.height == 0)
				{
					return null;
				}
				return Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2((float)texture2D.width, (float)texture2D.height)), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect);
			}
		}
		catch (Exception ex)
		{
			global::Debug.Log("Loading timelapse preview failed! reason: " + ex.Message);
		}
		return null;
	}

	// Token: 0x060041A2 RID: 16802 RVA: 0x0017067C File Offset: 0x0016E87C
	public static Sprite LoadColonyPreview(string savePath, string colonyName, bool fallbackToTimelapse = false)
	{
		string text = Path.ChangeExtension(savePath, ".png");
		if (File.Exists(text))
		{
			try
			{
				Texture2D texture2D = new Texture2D(512, 768);
				if (!texture2D.LoadImage(File.ReadAllBytes(text)))
				{
					return null;
				}
				if (texture2D.width > SystemInfo.maxTextureSize || texture2D.height > SystemInfo.maxTextureSize)
				{
					return null;
				}
				if (texture2D.width == 0 || texture2D.height == 0)
				{
					return null;
				}
				return Sprite.Create(texture2D, new Rect(Vector2.zero, new Vector2((float)texture2D.width, (float)texture2D.height)), new Vector2(0.5f, 0.5f), 100f, 0U, SpriteMeshType.FullRect);
			}
			catch (Exception ex)
			{
				string text2 = "failed to load preview image!? ";
				Exception ex2 = ex;
				global::Debug.Log(text2 + ((ex2 != null) ? ex2.ToString() : null));
			}
		}
		if (!fallbackToTimelapse)
		{
			return null;
		}
		try
		{
			return RetireColonyUtility.LoadRetiredColonyPreview(colonyName, null);
		}
		catch (Exception ex3)
		{
			global::Debug.Log(string.Format("failed to load fallback timelapse image!? {0}", ex3));
		}
		return null;
	}

	// Token: 0x060041A3 RID: 16803 RVA: 0x0017079C File Offset: 0x0016E99C
	public static string StripInvalidCharacters(string source)
	{
		foreach (char c in RetireColonyUtility.invalidCharacters)
		{
			source = source.Replace(c, '_');
		}
		source = source.Trim();
		return source;
	}

	// Token: 0x04002BBE RID: 11198
	private const int FILE_IO_RETRY_ATTEMPTS = 5;

	// Token: 0x04002BBF RID: 11199
	private static char[] invalidCharacters = "<>:\"\\/|?*.".ToCharArray();

	// Token: 0x04002BC0 RID: 11200
	private static Encoding[] attempt_encodings = new Encoding[]
	{
		new UTF8Encoding(false, true),
		new UnicodeEncoding(false, true, true),
		Encoding.ASCII
	};
}
