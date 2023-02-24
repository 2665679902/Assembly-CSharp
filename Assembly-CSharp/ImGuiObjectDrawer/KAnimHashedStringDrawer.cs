using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C66 RID: 3174
	public sealed class KAnimHashedStringDrawer : InlineDrawer
	{
		// Token: 0x060064E1 RID: 25825 RVA: 0x0025B086 File Offset: 0x00259286
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is KAnimHashedString;
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x0025B098 File Offset: 0x00259298
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			KAnimHashedString kanimHashedString = (KAnimHashedString)member.value;
			string text = kanimHashedString.ToString();
			string text2 = "0x" + kanimHashedString.HashValue.ToString("X");
			ImGuiEx.SimpleField(member.name, text + " (" + text2 + ")");
		}
	}
}
