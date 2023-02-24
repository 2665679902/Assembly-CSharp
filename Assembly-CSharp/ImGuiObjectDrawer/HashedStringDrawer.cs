using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C65 RID: 3173
	public sealed class HashedStringDrawer : InlineDrawer
	{
		// Token: 0x060064DE RID: 25822 RVA: 0x0025B00B File Offset: 0x0025920B
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is HashedString;
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x0025B01C File Offset: 0x0025921C
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			HashedString hashedString = (HashedString)member.value;
			string text = hashedString.ToString();
			string text2 = "0x" + hashedString.HashValue.ToString("X");
			ImGuiEx.SimpleField(member.name, text + " (" + text2 + ")");
		}
	}
}
