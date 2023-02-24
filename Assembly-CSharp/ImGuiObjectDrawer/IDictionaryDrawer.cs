using System;
using System.Collections;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C6C RID: 3180
	public sealed class IDictionaryDrawer : CollectionDrawer
	{
		// Token: 0x060064F9 RID: 25849 RVA: 0x0025B3EA File Offset: 0x002595EA
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.CanAssignToType<IDictionary>();
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x0025B3F2 File Offset: 0x002595F2
		public override bool IsEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			return ((IDictionary)member.value).Count == 0;
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x0025B408 File Offset: 0x00259608
		protected override void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member)
		{
			IDictionary dictionary = (IDictionary)member.value;
			int num = 0;
			using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DictionaryEntry kvp = (DictionaryEntry)enumerator.Current;
					visit(context, new CollectionDrawer.Element(num, delegate
					{
						DrawerUtil.Tooltip(string.Format("{0} -> {1}", kvp.Key.GetType(), kvp.Value.GetType()));
					}, () => new
					{
						key = kvp.Key,
						value = kvp.Value
					}));
					num++;
				}
			}
		}
	}
}
