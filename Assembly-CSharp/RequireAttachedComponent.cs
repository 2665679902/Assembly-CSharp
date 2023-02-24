using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000988 RID: 2440
[SerializationConfig(MemberSerialization.OptIn)]
public class RequireAttachedComponent : ProcessCondition
{
	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x06004850 RID: 18512 RVA: 0x00195F1C File Offset: 0x0019411C
	// (set) Token: 0x06004851 RID: 18513 RVA: 0x00195F24 File Offset: 0x00194124
	public Type RequiredType
	{
		get
		{
			return this.requiredType;
		}
		set
		{
			this.requiredType = value;
			this.typeNameString = this.requiredType.Name;
		}
	}

	// Token: 0x06004852 RID: 18514 RVA: 0x00195F3E File Offset: 0x0019413E
	public RequireAttachedComponent(AttachableBuilding myAttachable, Type required_type, string type_name_string)
	{
		this.myAttachable = myAttachable;
		this.requiredType = required_type;
		this.typeNameString = type_name_string;
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x00195F5C File Offset: 0x0019415C
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.myAttachable != null)
		{
			using (List<GameObject>.Enumerator enumerator = AttachableBuilding.GetAttachedNetwork(this.myAttachable).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetComponent(this.requiredType))
					{
						return ProcessCondition.Status.Ready;
					}
				}
			}
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06004854 RID: 18516 RVA: 0x00195FD4 File Offset: 0x001941D4
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return this.typeNameString;
	}

	// Token: 0x06004855 RID: 18517 RVA: 0x00195FE0 File Offset: 0x001941E0
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return string.Format(UI.STARMAP.LAUNCHCHECKLIST.INSTALLED_TOOLTIP, this.typeNameString.ToLower());
		}
		return string.Format(UI.STARMAP.LAUNCHCHECKLIST.MISSING_TOOLTIP, this.typeNameString.ToLower());
	}

	// Token: 0x06004856 RID: 18518 RVA: 0x0019601B File Offset: 0x0019421B
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F90 RID: 12176
	private string typeNameString;

	// Token: 0x04002F91 RID: 12177
	private Type requiredType;

	// Token: 0x04002F92 RID: 12178
	private AttachableBuilding myAttachable;
}
