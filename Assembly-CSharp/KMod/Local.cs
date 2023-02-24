using System;
using System.IO;
using Klei;
using STRINGS;

namespace KMod
{
	// Token: 0x02000D0A RID: 3338
	public class Local : IDistributionPlatform
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x0027DA76 File Offset: 0x0027BC76
		// (set) Token: 0x06006748 RID: 26440 RVA: 0x0027DA7E File Offset: 0x0027BC7E
		public string folder { get; private set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x0027DA87 File Offset: 0x0027BC87
		// (set) Token: 0x0600674A RID: 26442 RVA: 0x0027DA8F File Offset: 0x0027BC8F
		public Label.DistributionPlatform distribution_platform { get; private set; }

		// Token: 0x0600674B RID: 26443 RVA: 0x0027DA98 File Offset: 0x0027BC98
		public string GetDirectory()
		{
			return FileSystem.Normalize(Path.Combine(Manager.GetDirectory(), this.folder));
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x0027DAB0 File Offset: 0x0027BCB0
		private void Subscribe(string directoryName, long timestamp, IFileSource file_source, bool isDevMod)
		{
			Label label = new Label
			{
				id = directoryName,
				distribution_platform = this.distribution_platform,
				version = (long)directoryName.GetHashCode(),
				title = directoryName
			};
			KModHeader header = KModUtil.GetHeader(file_source, label.defaultStaticID, directoryName, directoryName, isDevMod);
			label.title = header.title;
			Mod mod = new Mod(label, header.staticID, header.description, file_source, UI.FRONTEND.MODS.TOOLTIPS.MANAGE_LOCAL_MOD, delegate
			{
				App.OpenWebURL("file://" + file_source.GetRoot());
			});
			if (file_source.GetType() == typeof(Directory))
			{
				mod.status = Mod.Status.Installed;
			}
			Global.Instance.modManager.Subscribe(mod, this);
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x0027DB84 File Offset: 0x0027BD84
		public Local(string folder, Label.DistributionPlatform distribution_platform, bool isDevFolder)
		{
			this.folder = folder;
			this.distribution_platform = distribution_platform;
			DirectoryInfo directoryInfo = new DirectoryInfo(this.GetDirectory());
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string name = directoryInfo2.Name;
				this.Subscribe(name, directoryInfo2.LastWriteTime.ToFileTime(), new Directory(directoryInfo2.FullName), isDevFolder);
			}
		}
	}
}
