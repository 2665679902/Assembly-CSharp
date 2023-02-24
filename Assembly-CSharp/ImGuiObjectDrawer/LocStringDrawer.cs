using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C63 RID: 3171
	public sealed class LocStringDrawer : InlineDrawer
	{
		// Token: 0x060064D8 RID: 25816 RVA: 0x0025AFA1 File Offset: 0x002591A1
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.CanAssignToType<LocString>();
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x0025AFA9 File Offset: 0x002591A9
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, string.Format("{0}({1})", member.value, ((LocString)member.value).text));
		}
	}
}
