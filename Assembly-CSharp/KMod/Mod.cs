using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Klei;
using Newtonsoft.Json;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000D13 RID: 3347
	[JsonObject(MemberSerialization.OptIn)]
	[DebuggerDisplay("{title}")]
	public class Mod
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x0027E654 File Offset: 0x0027C854
		// (set) Token: 0x06006770 RID: 26480 RVA: 0x0027E65C File Offset: 0x0027C85C
		public Content available_content { get; private set; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x0027E665 File Offset: 0x0027C865
		// (set) Token: 0x06006772 RID: 26482 RVA: 0x0027E66D File Offset: 0x0027C86D
		[JsonProperty]
		public string staticID { get; private set; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06006773 RID: 26483 RVA: 0x0027E676 File Offset: 0x0027C876
		// (set) Token: 0x06006774 RID: 26484 RVA: 0x0027E67E File Offset: 0x0027C87E
		public LocString manage_tooltip { get; private set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06006775 RID: 26485 RVA: 0x0027E687 File Offset: 0x0027C887
		// (set) Token: 0x06006776 RID: 26486 RVA: 0x0027E68F File Offset: 0x0027C88F
		public System.Action on_managed { get; private set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06006777 RID: 26487 RVA: 0x0027E698 File Offset: 0x0027C898
		public bool is_managed
		{
			get
			{
				return this.manage_tooltip != null;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06006778 RID: 26488 RVA: 0x0027E6A3 File Offset: 0x0027C8A3
		public string title
		{
			get
			{
				return this.label.title;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06006779 RID: 26489 RVA: 0x0027E6B0 File Offset: 0x0027C8B0
		// (set) Token: 0x0600677A RID: 26490 RVA: 0x0027E6B8 File Offset: 0x0027C8B8
		public string description { get; private set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600677B RID: 26491 RVA: 0x0027E6C1 File Offset: 0x0027C8C1
		// (set) Token: 0x0600677C RID: 26492 RVA: 0x0027E6C9 File Offset: 0x0027C8C9
		public Content loaded_content { get; private set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x0027E6D2 File Offset: 0x0027C8D2
		// (set) Token: 0x0600677E RID: 26494 RVA: 0x0027E6DA File Offset: 0x0027C8DA
		public bool DevModCrashTriggered { get; private set; }

		// Token: 0x0600677F RID: 26495 RVA: 0x0027E6E3 File Offset: 0x0027C8E3
		[JsonConstructor]
		public Mod()
		{
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x0027E6F8 File Offset: 0x0027C8F8
		public void CopyPersistentDataTo(Mod other_mod)
		{
			other_mod.status = this.status;
			other_mod.enabledForDlc = ((this.enabledForDlc != null) ? new List<string>(this.enabledForDlc) : new List<string>());
			other_mod.crash_count = this.crash_count;
			other_mod.loaded_content = this.loaded_content;
			other_mod.loaded_mod_data = this.loaded_mod_data;
			other_mod.reinstall_path = this.reinstall_path;
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x0027E764 File Offset: 0x0027C964
		public Mod(Label label, string staticID, string description, IFileSource file_source, LocString manage_tooltip, System.Action on_managed)
		{
			this.label = label;
			this.status = Mod.Status.NotInstalled;
			this.staticID = staticID;
			this.description = description;
			this.file_source = file_source;
			this.manage_tooltip = manage_tooltip;
			this.on_managed = on_managed;
			this.loaded_content = (Content)0;
			this.available_content = (Content)0;
			this.ScanContent();
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x0027E7CA File Offset: 0x0027C9CA
		public bool IsEnabledForActiveDlc()
		{
			return this.IsEnabledForDlc(DlcManager.GetHighestActiveDlcId());
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x0027E7D7 File Offset: 0x0027C9D7
		public bool IsEnabledForDlc(string dlcId)
		{
			return this.enabledForDlc != null && this.enabledForDlc.Contains(dlcId);
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x0027E7EF File Offset: 0x0027C9EF
		public void SetEnabledForActiveDlc(bool enabled)
		{
			this.SetEnabledForDlc(DlcManager.GetHighestActiveDlcId(), enabled);
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x0027E800 File Offset: 0x0027CA00
		public void SetEnabledForDlc(string dlcId, bool set_enabled)
		{
			if (this.enabledForDlc == null)
			{
				this.enabledForDlc = new List<string>();
			}
			bool flag = this.enabledForDlc.Contains(dlcId);
			if (set_enabled && !flag)
			{
				this.enabledForDlc.Add(dlcId);
				return;
			}
			if (!set_enabled && flag)
			{
				this.enabledForDlc.Remove(dlcId);
			}
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x0027E858 File Offset: 0x0027CA58
		public void ScanContent()
		{
			this.ModDevLog(string.Format("{0} ({1}): Setting up mod.", this.label, this.label.id));
			this.available_content = (Content)0;
			if (this.file_source == null)
			{
				if (this.label.id.EndsWith(".zip"))
				{
					DebugUtil.DevAssert(false, "Does this actually get used ever?", null);
					this.file_source = new ZipFile(this.label.install_path);
				}
				else
				{
					this.file_source = new Directory(this.label.install_path);
				}
			}
			if (!this.file_source.Exists())
			{
				global::Debug.LogWarning(string.Format("{0}: File source does not appear to be valid, skipping. ({1})", this.label, this.label.install_path));
				return;
			}
			KModHeader header = KModUtil.GetHeader(this.file_source, this.label.defaultStaticID, this.label.title, this.description, this.IsDev);
			if (this.label.title != header.title)
			{
				global::Debug.Log(string.Concat(new string[]
				{
					"\t",
					this.label.title,
					" has a mod.yaml with the title `",
					header.title,
					"`, using that from now on."
				}));
			}
			if (this.label.defaultStaticID != header.staticID)
			{
				global::Debug.Log(string.Concat(new string[]
				{
					"\t",
					this.label.title,
					" has a mod.yaml with a staticID `",
					header.staticID,
					"`, using that from now on."
				}));
			}
			this.label.title = header.title;
			this.staticID = header.staticID;
			this.description = header.description;
			Mod.ArchivedVersion mostSuitableArchive = this.GetMostSuitableArchive();
			if (mostSuitableArchive == null)
			{
				global::Debug.LogWarning(string.Format("{0}: No archive supports this game version, skipping content.", this.label));
				this.contentCompatability = ModContentCompatability.DoesntSupportDLCConfig;
				this.available_content = (Content)0;
				this.SetEnabledForActiveDlc(false);
				return;
			}
			this.packagedModInfo = mostSuitableArchive.info;
			Content content;
			this.ScanContentFromSource(mostSuitableArchive.relativePath, out content);
			if (content == (Content)0)
			{
				global::Debug.LogWarning(string.Format("{0}: No supported content for mod, skipping content.", this.label));
				this.contentCompatability = ModContentCompatability.NoContent;
				this.available_content = (Content)0;
				this.SetEnabledForActiveDlc(false);
				return;
			}
			bool flag = mostSuitableArchive.info.APIVersion == 2;
			if ((content & Content.DLL) != (Content)0 && !flag)
			{
				global::Debug.LogWarning(string.Format("{0}: DLLs found but not using the correct API version.", this.label));
				this.contentCompatability = ModContentCompatability.OldAPI;
				this.available_content = (Content)0;
				this.SetEnabledForActiveDlc(false);
				return;
			}
			this.contentCompatability = ModContentCompatability.OK;
			this.available_content = content;
			this.relative_root = mostSuitableArchive.relativePath;
			global::Debug.Assert(this.content_source == null);
			this.content_source = new Directory(this.ContentPath);
			string text = (string.IsNullOrEmpty(this.relative_root) ? "root" : this.relative_root);
			global::Debug.Log(string.Format("{0}: Successfully loaded from path '{1}' with content '{2}'.", this.label, text, this.available_content.ToString()));
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x0027EB88 File Offset: 0x0027CD88
		private Mod.ArchivedVersion GetMostSuitableArchive()
		{
			Mod.PackagedModInfo packagedModInfo = this.GetModInfoForFolder("");
			if (packagedModInfo == null)
			{
				packagedModInfo = new Mod.PackagedModInfo
				{
					supportedContent = "vanilla_id",
					minimumSupportedBuild = 0
				};
				if (this.ScanContentFromSourceForTranslationsOnly(""))
				{
					this.ModDevLogWarning(string.Format("{0}: No mod_info.yaml found, but since it contains a translation, default its supported content to 'ALL'", this.label));
					packagedModInfo.supportedContent = "all";
				}
				else
				{
					this.ModDevLogWarning(string.Format("{0}: No mod_info.yaml found, default its supported content to 'VANILLA_ID'", this.label));
				}
			}
			Mod.ArchivedVersion archivedVersion = new Mod.ArchivedVersion
			{
				relativePath = "",
				info = packagedModInfo
			};
			if (!this.file_source.Exists("archived_versions"))
			{
				this.ModDevLog(string.Format("\t{0}: No archived_versions for this mod, using root version directly.", this.label));
				if (!this.DoesModSupportCurrentContent(packagedModInfo))
				{
					return null;
				}
				return archivedVersion;
			}
			else
			{
				List<FileSystemItem> list = new List<FileSystemItem>();
				this.file_source.GetTopLevelItems(list, "archived_versions");
				if (list.Count == 0)
				{
					this.ModDevLog(string.Format("\t{0}: No archived_versions for this mod, using root version directly.", this.label));
					if (!this.DoesModSupportCurrentContent(packagedModInfo))
					{
						return null;
					}
					return archivedVersion;
				}
				else
				{
					List<Mod.ArchivedVersion> list2 = new List<Mod.ArchivedVersion>();
					list2.Add(archivedVersion);
					foreach (FileSystemItem fileSystemItem in list)
					{
						string text = Path.Combine("archived_versions", fileSystemItem.name);
						Mod.PackagedModInfo modInfoForFolder = this.GetModInfoForFolder(text);
						if (modInfoForFolder != null)
						{
							list2.Add(new Mod.ArchivedVersion
							{
								relativePath = text,
								info = modInfoForFolder
							});
						}
					}
					list2 = list2.Where((Mod.ArchivedVersion v) => this.DoesModSupportCurrentContent(v.info)).ToList<Mod.ArchivedVersion>();
					list2 = list2.Where((Mod.ArchivedVersion v) => v.info.APIVersion == 2 || v.info.APIVersion == 0).ToList<Mod.ArchivedVersion>();
					Mod.ArchivedVersion archivedVersion2 = (from v in list2
						where (long)v.info.minimumSupportedBuild <= 544519L
						orderby v.info.minimumSupportedBuild descending
						select v).FirstOrDefault<Mod.ArchivedVersion>();
					if (archivedVersion2 == null)
					{
						return null;
					}
					return archivedVersion2;
				}
			}
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x0027EDC8 File Offset: 0x0027CFC8
		private Mod.PackagedModInfo GetModInfoForFolder(string relative_root)
		{
			List<FileSystemItem> list = new List<FileSystemItem>();
			this.file_source.GetTopLevelItems(list, relative_root);
			bool flag = false;
			foreach (FileSystemItem fileSystemItem in list)
			{
				if (fileSystemItem.type == FileSystemItem.ItemType.File && fileSystemItem.name.ToLower() == "mod_info.yaml")
				{
					flag = true;
					break;
				}
			}
			string text = (string.IsNullOrEmpty(relative_root) ? "root" : relative_root);
			if (!flag)
			{
				this.ModDevLogWarning(string.Concat(new string[] { "\t", this.title, ": has no mod_info.yaml in folder '", text, "'" }));
				return null;
			}
			string text2 = this.file_source.Read(Path.Combine(relative_root, "mod_info.yaml"));
			if (string.IsNullOrEmpty(text2))
			{
				this.ModDevLogError(string.Format("\t{0}: Failed to read {1} in folder '{2}', skipping", this.label, "mod_info.yaml", text));
				return null;
			}
			YamlIO.ErrorHandler errorHandler = delegate(YamlIO.Error e, bool force_warning)
			{
				YamlIO.LogError(e, !this.IsDev);
			};
			Mod.PackagedModInfo packagedModInfo = YamlIO.Parse<Mod.PackagedModInfo>(text2, default(FileHandle), errorHandler, null);
			if (packagedModInfo == null)
			{
				this.ModDevLogError(string.Format("\t{0}: Failed to parse {1} in folder '{2}', text is {3}", new object[] { this.label, "mod_info.yaml", text, text2 }));
				return null;
			}
			if (packagedModInfo.supportedContent == null)
			{
				this.ModDevLogError(string.Format("\t{0}: {1} in folder '{2}' does not specify supportedContent. Make sure you spelled it correctly in your mod_info!", this.label, "mod_info.yaml", text));
				return null;
			}
			if (packagedModInfo.lastWorkingBuild != 0)
			{
				this.ModDevLogError(string.Format("\t{0}: {1} in folder '{2}' is using `{3}`, please upgrade this to `{4}`", new object[] { this.label, "mod_info.yaml", text, "lastWorkingBuild", "minimumSupportedBuild" }));
				if (packagedModInfo.minimumSupportedBuild == 0)
				{
					packagedModInfo.minimumSupportedBuild = packagedModInfo.lastWorkingBuild;
				}
			}
			this.ModDevLog(string.Format("\t{0}: Found valid mod_info.yaml in folder '{1}': {2} at {3}", new object[] { this.label, text, packagedModInfo.supportedContent, packagedModInfo.minimumSupportedBuild }));
			return packagedModInfo;
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x0027F008 File Offset: 0x0027D208
		private bool DoesModSupportCurrentContent(Mod.PackagedModInfo mod_info)
		{
			string text = DlcManager.GetHighestActiveDlcId();
			if (text == "")
			{
				text = "vanilla_id";
			}
			text = text.ToLower();
			string text2 = mod_info.supportedContent.ToLower();
			return text2.Contains(text) || text2.Contains("all");
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x0027F058 File Offset: 0x0027D258
		private bool ScanContentFromSourceForTranslationsOnly(string relativeRoot)
		{
			this.available_content = (Content)0;
			List<FileSystemItem> list = new List<FileSystemItem>();
			this.file_source.GetTopLevelItems(list, relativeRoot);
			foreach (FileSystemItem fileSystemItem in list)
			{
				if (fileSystemItem.type == FileSystemItem.ItemType.File && fileSystemItem.name.ToLower().EndsWith(".po"))
				{
					this.available_content |= Content.Translation;
				}
			}
			return this.available_content > (Content)0;
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x0027F0F0 File Offset: 0x0027D2F0
		private bool ScanContentFromSource(string relativeRoot, out Content available)
		{
			available = (Content)0;
			List<FileSystemItem> list = new List<FileSystemItem>();
			this.file_source.GetTopLevelItems(list, relativeRoot);
			foreach (FileSystemItem fileSystemItem in list)
			{
				if (fileSystemItem.type == FileSystemItem.ItemType.Directory)
				{
					string text = fileSystemItem.name.ToLower();
					available |= this.AddDirectory(text);
				}
				else
				{
					string text2 = fileSystemItem.name.ToLower();
					available |= this.AddFile(text2);
				}
			}
			return available > (Content)0;
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600678C RID: 26508 RVA: 0x0027F190 File Offset: 0x0027D390
		public string ContentPath
		{
			get
			{
				return Path.Combine(this.label.install_path, this.relative_root);
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x0027F1A8 File Offset: 0x0027D3A8
		public bool IsEmpty()
		{
			return this.available_content == (Content)0;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x0027F1B4 File Offset: 0x0027D3B4
		private Content AddDirectory(string directory)
		{
			Content content = (Content)0;
			string text = directory.TrimEnd(new char[] { '/' });
			if (text != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1519694028U)
				{
					if (num != 948591336U)
					{
						if (num != 1318520008U)
						{
							if (num == 1519694028U)
							{
								if (text == "elements")
								{
									content |= Content.LayerableFiles;
								}
							}
						}
						else if (text == "buildingfacades")
						{
							content |= Content.Animation;
						}
					}
					else if (text == "templates")
					{
						content |= Content.LayerableFiles;
					}
				}
				else if (num <= 3037049615U)
				{
					if (num != 2960291089U)
					{
						if (num == 3037049615U)
						{
							if (text == "worldgen")
							{
								content |= Content.LayerableFiles;
							}
						}
					}
					else if (text == "strings")
					{
						content |= Content.Strings;
					}
				}
				else if (num != 3319670096U)
				{
					if (num == 3570262116U)
					{
						if (text == "codex")
						{
							content |= Content.LayerableFiles;
						}
					}
				}
				else if (text == "anim")
				{
					content |= Content.Animation;
				}
			}
			return content;
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x0027F2D4 File Offset: 0x0027D4D4
		private Content AddFile(string file)
		{
			Content content = (Content)0;
			if (file.EndsWith(".dll"))
			{
				content |= Content.DLL;
			}
			if (file.EndsWith(".po"))
			{
				content |= Content.Translation;
			}
			return content;
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x0027F306 File Offset: 0x0027D506
		private static void AccumulateExtensions(Content content, List<string> extensions)
		{
			if ((content & Content.DLL) != (Content)0)
			{
				extensions.Add(".dll");
			}
			if ((content & (Content.Strings | Content.Translation)) != (Content)0)
			{
				extensions.Add(".po");
			}
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x0027F32C File Offset: 0x0027D52C
		[Conditional("DEBUG")]
		private void Assert(bool condition, string failure_message)
		{
			if (string.IsNullOrEmpty(this.title))
			{
				DebugUtil.Assert(condition, string.Format("{2}\n\t{0}\n\t{1}", this.title, this.label.ToString(), failure_message));
				return;
			}
			DebugUtil.Assert(condition, string.Format("{1}\n\t{0}", this.label.ToString(), failure_message));
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x0027F394 File Offset: 0x0027D594
		public void Install()
		{
			if (this.IsLocal)
			{
				this.status = Mod.Status.Installed;
				return;
			}
			this.status = Mod.Status.ReinstallPending;
			if (this.file_source == null)
			{
				return;
			}
			if (!FileUtil.DeleteDirectory(this.label.install_path, 0))
			{
				return;
			}
			if (!FileUtil.CreateDirectory(this.label.install_path, 0))
			{
				return;
			}
			this.file_source.CopyTo(this.label.install_path, null);
			this.file_source = new Directory(this.label.install_path);
			this.status = Mod.Status.Installed;
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x0027F424 File Offset: 0x0027D624
		public bool Uninstall()
		{
			this.SetEnabledForActiveDlc(false);
			if (this.loaded_content != (Content)0)
			{
				global::Debug.Log(string.Format("Can't uninstall {0}: still has loaded content: {1}", this.label.ToString(), this.loaded_content.ToString()));
				this.status = Mod.Status.UninstallPending;
				return false;
			}
			if (!this.IsLocal && !FileUtil.DeleteDirectory(this.label.install_path, 0))
			{
				global::Debug.Log(string.Format("Can't uninstall {0}: directory deletion failed", this.label.ToString()));
				this.status = Mod.Status.UninstallPending;
				return false;
			}
			this.status = Mod.Status.NotInstalled;
			return true;
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x0027F4CC File Offset: 0x0027D6CC
		private bool LoadStrings()
		{
			string text = FileSystem.Normalize(Path.Combine(this.ContentPath, "strings"));
			if (!Directory.Exists(text))
			{
				return false;
			}
			int num = 0;
			foreach (FileInfo fileInfo in new DirectoryInfo(text).GetFiles())
			{
				if (!(fileInfo.Extension.ToLower() != ".po"))
				{
					num++;
					Localization.OverloadStrings(Localization.LoadStringsFile(fileInfo.FullName, false));
				}
			}
			return true;
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x0027F549 File Offset: 0x0027D749
		private bool LoadTranslations()
		{
			return false;
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x0027F54C File Offset: 0x0027D74C
		private bool LoadAnimation()
		{
			string text = FileSystem.Normalize(Path.Combine(this.ContentPath, "anim"));
			if (!Directory.Exists(text))
			{
				return false;
			}
			int num = 0;
			DirectoryInfo[] directories = new DirectoryInfo(text).GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				foreach (DirectoryInfo directoryInfo in directories[i].GetDirectories())
				{
					KAnimFile.Mod mod = new KAnimFile.Mod();
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						if (fileInfo.Extension == ".png")
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							Texture2D texture2D = new Texture2D(2, 2);
							texture2D.LoadImage(array);
							mod.textures.Add(texture2D);
						}
						else if (fileInfo.Extension == ".bytes")
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
							byte[] array2 = File.ReadAllBytes(fileInfo.FullName);
							if (fileNameWithoutExtension.EndsWith("_anim"))
							{
								mod.anim = array2;
							}
							else if (fileNameWithoutExtension.EndsWith("_build"))
							{
								mod.build = array2;
							}
							else
							{
								DebugUtil.LogWarningArgs(new object[] { string.Format("Unhandled TextAsset ({0})...ignoring", fileInfo.FullName) });
							}
						}
						else
						{
							DebugUtil.LogWarningArgs(new object[] { string.Format("Unhandled asset ({0})...ignoring", fileInfo.FullName) });
						}
					}
					string text2 = directoryInfo.Name + "_kanim";
					if (mod.IsValid() && ModUtil.AddKAnimMod(text2, mod))
					{
						num++;
					}
				}
			}
			return true;
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x0027F710 File Offset: 0x0027D910
		public void Load(Content content)
		{
			content &= this.available_content & ~this.loaded_content;
			if (content > (Content)0)
			{
				global::Debug.Log(string.Format("Loading mod content {2} [{0}:{1}] (provides {3})", new object[]
				{
					this.title,
					this.label.id,
					content.ToString(),
					this.available_content.ToString()
				}));
			}
			if ((content & Content.Strings) != (Content)0 && this.LoadStrings())
			{
				this.loaded_content |= Content.Strings;
			}
			if ((content & Content.Translation) != (Content)0 && this.LoadTranslations())
			{
				this.loaded_content |= Content.Translation;
			}
			if ((content & Content.DLL) != (Content)0)
			{
				this.loaded_mod_data = DLLLoader.LoadDLLs(this, this.staticID, this.ContentPath, this.IsDev);
				if (this.loaded_mod_data != null)
				{
					this.loaded_content |= Content.DLL;
				}
			}
			if ((content & Content.LayerableFiles) != (Content)0)
			{
				global::Debug.Assert(this.content_source != null, "Attempting to Load layerable files with content_source not initialized");
				FileSystem.file_sources.Insert(0, this.content_source.GetFileSystem());
				this.loaded_content |= Content.LayerableFiles;
			}
			if ((content & Content.Animation) != (Content)0 && this.LoadAnimation())
			{
				this.loaded_content |= Content.Animation;
			}
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x0027F84F File Offset: 0x0027DA4F
		public void PostLoad(IReadOnlyList<Mod> mods)
		{
			if ((this.loaded_content & Content.DLL) != (Content)0 && this.loaded_mod_data != null)
			{
				DLLLoader.PostLoadDLLs(this.staticID, this.loaded_mod_data, mods);
			}
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x0027F875 File Offset: 0x0027DA75
		public void Unload(Content content)
		{
			content &= this.loaded_content;
			if ((content & Content.LayerableFiles) != (Content)0)
			{
				FileSystem.file_sources.Remove(this.content_source.GetFileSystem());
				this.loaded_content &= ~Content.LayerableFiles;
			}
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x0027F8AE File Offset: 0x0027DAAE
		private void SetCrashCount(int new_crash_count)
		{
			this.crash_count = MathUtil.Clamp(0, 3, new_crash_count);
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600679B RID: 26523 RVA: 0x0027F8BE File Offset: 0x0027DABE
		public bool IsDev
		{
			get
			{
				return this.label.distribution_platform == Label.DistributionPlatform.Dev;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600679C RID: 26524 RVA: 0x0027F8CE File Offset: 0x0027DACE
		public bool IsLocal
		{
			get
			{
				return this.label.distribution_platform == Label.DistributionPlatform.Dev || this.label.distribution_platform == Label.DistributionPlatform.Local;
			}
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x0027F8EE File Offset: 0x0027DAEE
		public void SetCrashed()
		{
			this.SetCrashCount(this.crash_count + 1);
			if (!this.IsDev)
			{
				this.SetEnabledForActiveDlc(false);
			}
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x0027F90D File Offset: 0x0027DB0D
		public void Uncrash()
		{
			this.SetCrashCount(this.IsDev ? (this.crash_count - 1) : 0);
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x0027F928 File Offset: 0x0027DB28
		public bool IsActive()
		{
			return this.loaded_content > (Content)0;
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x0027F933 File Offset: 0x0027DB33
		public bool AllActive(Content content)
		{
			return (this.loaded_content & content) == content;
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x0027F940 File Offset: 0x0027DB40
		public bool AllActive()
		{
			return (this.loaded_content & this.available_content) == this.available_content;
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x0027F957 File Offset: 0x0027DB57
		public bool AnyActive(Content content)
		{
			return (this.loaded_content & content) > (Content)0;
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x0027F964 File Offset: 0x0027DB64
		public bool HasContent()
		{
			return this.available_content > (Content)0;
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x0027F96F File Offset: 0x0027DB6F
		public bool HasAnyContent(Content content)
		{
			return (this.available_content & content) > (Content)0;
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x0027F97C File Offset: 0x0027DB7C
		public bool HasOnlyTranslationContent()
		{
			return this.available_content == Content.Translation;
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x0027F988 File Offset: 0x0027DB88
		public Texture2D GetPreviewImage()
		{
			string text = null;
			foreach (string text2 in Mod.PREVIEW_FILENAMES)
			{
				if (Directory.Exists(this.ContentPath) && File.Exists(Path.Combine(this.ContentPath, text2)))
				{
					text = text2;
					break;
				}
			}
			if (text == null)
			{
				return null;
			}
			Texture2D texture2D2;
			try
			{
				byte[] array = File.ReadAllBytes(Path.Combine(this.ContentPath, text));
				Texture2D texture2D = new Texture2D(2, 2);
				texture2D.LoadImage(array);
				texture2D2 = texture2D;
			}
			catch
			{
				global::Debug.LogWarning(string.Format("Mod {0} seems to have a preview.png but it didn't load correctly.", this.label));
				texture2D2 = null;
			}
			return texture2D2;
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x0027FA54 File Offset: 0x0027DC54
		public void ModDevLog(string msg)
		{
			if (this.IsDev)
			{
				global::Debug.Log(msg);
			}
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x0027FA64 File Offset: 0x0027DC64
		public void ModDevLogWarning(string msg)
		{
			if (this.IsDev)
			{
				global::Debug.LogWarning(msg);
			}
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x0027FA74 File Offset: 0x0027DC74
		public void ModDevLogError(string msg)
		{
			if (this.IsDev)
			{
				this.DevModCrashTriggered = true;
				global::Debug.LogError(msg);
			}
		}

		// Token: 0x04004BEE RID: 19438
		public const int MOD_API_VERSION_NONE = 0;

		// Token: 0x04004BEF RID: 19439
		public const int MOD_API_VERSION_HARMONY1 = 1;

		// Token: 0x04004BF0 RID: 19440
		public const int MOD_API_VERSION_HARMONY2 = 2;

		// Token: 0x04004BF1 RID: 19441
		public const int MOD_API_VERSION = 2;

		// Token: 0x04004BF2 RID: 19442
		[JsonProperty]
		public Label label;

		// Token: 0x04004BF3 RID: 19443
		[JsonProperty]
		public Mod.Status status;

		// Token: 0x04004BF4 RID: 19444
		[JsonProperty]
		public bool enabled;

		// Token: 0x04004BF5 RID: 19445
		[JsonProperty]
		public List<string> enabledForDlc;

		// Token: 0x04004BF7 RID: 19447
		[JsonProperty]
		public int crash_count;

		// Token: 0x04004BF8 RID: 19448
		[JsonProperty]
		public string reinstall_path;

		// Token: 0x04004BFA RID: 19450
		public bool foundInStackTrace;

		// Token: 0x04004BFB RID: 19451
		public string relative_root = "";

		// Token: 0x04004BFC RID: 19452
		public Mod.PackagedModInfo packagedModInfo;

		// Token: 0x04004C01 RID: 19457
		public LoadedModData loaded_mod_data;

		// Token: 0x04004C02 RID: 19458
		public IFileSource file_source;

		// Token: 0x04004C03 RID: 19459
		public IFileSource content_source;

		// Token: 0x04004C04 RID: 19460
		public bool is_subscribed;

		// Token: 0x04004C06 RID: 19462
		private const string VANILLA_ID = "vanilla_id";

		// Token: 0x04004C07 RID: 19463
		private const string ALL_ID = "all";

		// Token: 0x04004C08 RID: 19464
		private const string ARCHIVED_VERSIONS_FOLDER = "archived_versions";

		// Token: 0x04004C09 RID: 19465
		private const string MOD_INFO_FILENAME = "mod_info.yaml";

		// Token: 0x04004C0A RID: 19466
		public ModContentCompatability contentCompatability;

		// Token: 0x04004C0B RID: 19467
		public const int MAX_CRASH_COUNT = 3;

		// Token: 0x04004C0C RID: 19468
		private static readonly List<string> PREVIEW_FILENAMES = new List<string> { "preview.png", "Preview.png", "PREVIEW.PNG" };

		// Token: 0x02001B46 RID: 6982
		public enum Status
		{
			// Token: 0x04007B14 RID: 31508
			NotInstalled,
			// Token: 0x04007B15 RID: 31509
			Installed,
			// Token: 0x04007B16 RID: 31510
			UninstallPending,
			// Token: 0x04007B17 RID: 31511
			ReinstallPending
		}

		// Token: 0x02001B47 RID: 6983
		public class ArchivedVersion
		{
			// Token: 0x04007B18 RID: 31512
			public string relativePath;

			// Token: 0x04007B19 RID: 31513
			public Mod.PackagedModInfo info;
		}

		// Token: 0x02001B48 RID: 6984
		public class PackagedModInfo
		{
			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x060095EA RID: 38378 RVA: 0x00321ED6 File Offset: 0x003200D6
			// (set) Token: 0x060095EB RID: 38379 RVA: 0x00321EDE File Offset: 0x003200DE
			public string supportedContent { get; set; }

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x060095EC RID: 38380 RVA: 0x00321EE7 File Offset: 0x003200E7
			// (set) Token: 0x060095ED RID: 38381 RVA: 0x00321EEF File Offset: 0x003200EF
			[Obsolete("Use minimumSupportedBuild instead!")]
			public int lastWorkingBuild { get; set; }

			// Token: 0x170009EB RID: 2539
			// (get) Token: 0x060095EE RID: 38382 RVA: 0x00321EF8 File Offset: 0x003200F8
			// (set) Token: 0x060095EF RID: 38383 RVA: 0x00321F00 File Offset: 0x00320100
			public int minimumSupportedBuild { get; set; }

			// Token: 0x170009EC RID: 2540
			// (get) Token: 0x060095F0 RID: 38384 RVA: 0x00321F09 File Offset: 0x00320109
			// (set) Token: 0x060095F1 RID: 38385 RVA: 0x00321F11 File Offset: 0x00320111
			public int APIVersion { get; set; }

			// Token: 0x170009ED RID: 2541
			// (get) Token: 0x060095F2 RID: 38386 RVA: 0x00321F1A File Offset: 0x0032011A
			// (set) Token: 0x060095F3 RID: 38387 RVA: 0x00321F22 File Offset: 0x00320122
			public string version { get; set; }
		}
	}
}
