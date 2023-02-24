using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000521 RID: 1313
	public class Handle : IEquatable<Handle>
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x0007F489 File Offset: 0x0007D689
		// (set) Token: 0x06003807 RID: 14343 RVA: 0x0007F491 File Offset: 0x0007D691
		public IntPtr InnerHandle { get; private set; }

		// Token: 0x06003808 RID: 14344 RVA: 0x0007F49A File Offset: 0x0007D69A
		public Handle(IntPtr innerHandle)
		{
			this.InnerHandle = innerHandle;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x0007F4A9 File Offset: 0x0007D6A9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Handle);
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x0007F4B8 File Offset: 0x0007D6B8
		public override int GetHashCode()
		{
			return (int)(65536L + this.InnerHandle.ToInt64());
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x0007F4DB File Offset: 0x0007D6DB
		public bool Equals(Handle other)
		{
			return other != null && (this == other || (!(base.GetType() != other.GetType()) && this.InnerHandle == other.InnerHandle));
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x0007F50E File Offset: 0x0007D70E
		public static bool operator ==(Handle lhs, Handle rhs)
		{
			if (lhs == null)
			{
				return rhs == null;
			}
			return lhs.Equals(rhs);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x0007F521 File Offset: 0x0007D721
		public static bool operator !=(Handle lhs, Handle rhs)
		{
			return !(lhs == rhs);
		}
	}
}
