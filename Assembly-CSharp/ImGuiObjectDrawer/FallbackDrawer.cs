using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C62 RID: 3170
	public sealed class FallbackDrawer : SimpleDrawer
	{
		// Token: 0x060064D5 RID: 25813 RVA: 0x0025AF93 File Offset: 0x00259193
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return true;
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x0025AF96 File Offset: 0x00259196
		public override bool CanDrawAtDepth(int depth)
		{
			return true;
		}
	}
}
