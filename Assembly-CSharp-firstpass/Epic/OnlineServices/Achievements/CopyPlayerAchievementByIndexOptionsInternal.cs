using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000921 RID: 2337
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerAchievementByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x0009941C File Offset: 0x0009761C
		// (set) Token: 0x06005132 RID: 20786 RVA: 0x0009943E File Offset: 0x0009763E
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x00099450 File Offset: 0x00097650
		// (set) Token: 0x06005134 RID: 20788 RVA: 0x00099472 File Offset: 0x00097672
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06005135 RID: 20789 RVA: 0x00099484 File Offset: 0x00097684
		// (set) Token: 0x06005136 RID: 20790 RVA: 0x000994A6 File Offset: 0x000976A6
		public uint AchievementIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_AchievementIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_AchievementIndex, value);
			}
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x000994B5 File Offset: 0x000976B5
		public void Dispose()
		{
		}

		// Token: 0x04001F92 RID: 8082
		private int m_ApiVersion;

		// Token: 0x04001F93 RID: 8083
		private IntPtr m_UserId;

		// Token: 0x04001F94 RID: 8084
		private uint m_AchievementIndex;
	}
}
