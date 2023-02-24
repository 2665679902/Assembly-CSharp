using System;
using System.IO;
using Klei;

namespace KMod
{
	// Token: 0x02000D09 RID: 3337
	public class KModUtil
	{
		// Token: 0x06006745 RID: 26437 RVA: 0x0027D9B8 File Offset: 0x0027BBB8
		public static KModHeader GetHeader(IFileSource file_source, string defaultStaticID, string defaultTitle, string defaultDescription, bool devMod)
		{
			string text = "mod.yaml";
			string text2 = file_source.Read(text);
			YamlIO.ErrorHandler errorHandler = delegate(YamlIO.Error e, bool force_warning)
			{
				YamlIO.LogError(e, !devMod);
			};
			KModHeader kmodHeader = ((!string.IsNullOrEmpty(text2)) ? YamlIO.Parse<KModHeader>(text2, new FileHandle
			{
				full_path = Path.Combine(file_source.GetRoot(), text)
			}, errorHandler, null) : null);
			if (kmodHeader == null)
			{
				kmodHeader = new KModHeader
				{
					title = defaultTitle,
					description = defaultDescription,
					staticID = defaultStaticID
				};
			}
			if (string.IsNullOrEmpty(kmodHeader.staticID))
			{
				kmodHeader.staticID = defaultStaticID;
			}
			if (kmodHeader.title == null)
			{
				kmodHeader.title = defaultTitle;
			}
			if (kmodHeader.description == null)
			{
				kmodHeader.description = defaultDescription;
			}
			return kmodHeader;
		}
	}
}
