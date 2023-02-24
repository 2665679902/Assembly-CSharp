using System;

namespace Klei.Actions
{
	// Token: 0x02000DB1 RID: 3505
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class ActionTypeAttribute : Attribute
	{
		// Token: 0x06006A8D RID: 27277 RVA: 0x002954FB File Offset: 0x002936FB
		public ActionTypeAttribute(string groupName, string typeName, bool generateConfig = true)
		{
			this.TypeName = typeName;
			this.GroupName = groupName;
			this.GenerateConfig = generateConfig;
		}

		// Token: 0x06006A8E RID: 27278 RVA: 0x00295518 File Offset: 0x00293718
		public static bool operator ==(ActionTypeAttribute lhs, ActionTypeAttribute rhs)
		{
			bool flag = object.Equals(lhs, null);
			bool flag2 = object.Equals(rhs, null);
			if (flag || flag2)
			{
				return flag == flag2;
			}
			return lhs.TypeName == rhs.TypeName && lhs.GroupName == rhs.GroupName;
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x00295565 File Offset: 0x00293765
		public static bool operator !=(ActionTypeAttribute lhs, ActionTypeAttribute rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x00295571 File Offset: 0x00293771
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x0029557A File Offset: 0x0029377A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04005000 RID: 20480
		public readonly string TypeName;

		// Token: 0x04005001 RID: 20481
		public readonly string GroupName;

		// Token: 0x04005002 RID: 20482
		public readonly bool GenerateConfig;
	}
}
