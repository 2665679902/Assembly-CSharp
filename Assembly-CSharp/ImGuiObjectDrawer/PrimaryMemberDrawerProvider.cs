using System;
using System.Collections.Generic;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C5E RID: 3166
	public class PrimaryMemberDrawerProvider : IMemberDrawerProvider
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060064C7 RID: 25799 RVA: 0x0025AE76 File Offset: 0x00259076
		public int Priority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x0025AE7C File Offset: 0x0025907C
		public void AppendDrawersTo(List<MemberDrawer> drawers)
		{
			drawers.AddRange(new MemberDrawer[]
			{
				new NullDrawer(),
				new SimpleDrawer(),
				new LocStringDrawer(),
				new EnumDrawer(),
				new HashedStringDrawer(),
				new KAnimHashedStringDrawer(),
				new Vector2Drawer(),
				new Vector3Drawer(),
				new Vector4Drawer(),
				new UnityObjectDrawer(),
				new ArrayDrawer(),
				new IDictionaryDrawer(),
				new IEnumerableDrawer(),
				new PlainCSharpObjectDrawer(),
				new FallbackDrawer()
			});
		}
	}
}
