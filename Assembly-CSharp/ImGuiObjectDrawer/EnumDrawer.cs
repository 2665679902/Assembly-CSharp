using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C64 RID: 3172
	public sealed class EnumDrawer : InlineDrawer
	{
		// Token: 0x060064DB RID: 25819 RVA: 0x0025AFDE File Offset: 0x002591DE
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.type.IsEnum;
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x0025AFEB File Offset: 0x002591EB
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, member.value.ToString());
		}
	}
}
