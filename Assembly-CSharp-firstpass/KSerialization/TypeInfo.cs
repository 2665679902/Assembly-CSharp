using System;

namespace KSerialization
{
	// Token: 0x020004FD RID: 1277
	public class TypeInfo
	{
		// Token: 0x060036F6 RID: 14070 RVA: 0x000792C8 File Offset: 0x000774C8
		public void BuildGenericArgs()
		{
			if (this.type.IsGenericType)
			{
				Type genericTypeDefinition = this.type.GetGenericTypeDefinition();
				this.genericTypeArgs = this.type.GetGenericArguments();
				this.genericInstantiationType = genericTypeDefinition.MakeGenericType(this.genericTypeArgs);
			}
			if (this.subTypes != null)
			{
				for (int i = 0; i < this.subTypes.Length; i++)
				{
					this.subTypes[i].BuildGenericArgs();
				}
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x0007933C File Offset: 0x0007753C
		public override bool Equals(object obj)
		{
			if (obj != null && obj is TypeInfo)
			{
				TypeInfo typeInfo = (TypeInfo)obj;
				return this.Equals(typeInfo);
			}
			return false;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x00079364 File Offset: 0x00077564
		public bool Equals(TypeInfo other)
		{
			if (this.info != other.info)
			{
				return false;
			}
			if (this.subTypes == null || other.subTypes == null)
			{
				return this.subTypes == null && other.subTypes == null && this.type == other.type;
			}
			if (this.subTypes.Length == other.subTypes.Length)
			{
				for (int i = 0; i < this.subTypes.Length; i++)
				{
					if (!this.subTypes[i].Equals(other.subTypes[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000793F6 File Offset: 0x000775F6
		public override int GetHashCode()
		{
			return this.type.GetHashCode();
		}

		// Token: 0x040013B2 RID: 5042
		public Type type;

		// Token: 0x040013B3 RID: 5043
		public SerializationTypeInfo info;

		// Token: 0x040013B4 RID: 5044
		public TypeInfo[] subTypes;

		// Token: 0x040013B5 RID: 5045
		public Type genericInstantiationType;

		// Token: 0x040013B6 RID: 5046
		public Type[] genericTypeArgs;
	}
}
