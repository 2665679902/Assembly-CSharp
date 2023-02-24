using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C5F RID: 3167
	public abstract class InlineDrawer : MemberDrawer
	{
		// Token: 0x060064CA RID: 25802 RVA: 0x0025AF1C File Offset: 0x0025911C
		public sealed override MemberDrawType GetDrawType(in MemberDrawContext context, in MemberDetails member)
		{
			return MemberDrawType.Inline;
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x0025AF1F File Offset: 0x0025911F
		protected sealed override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			this.DrawInline(context, member);
		}
	}
}
