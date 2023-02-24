using System;
using System.Collections.Generic;
using Klei.Actions;
using UnityEngine;

namespace Klei.Input
{
	// Token: 0x02000DAC RID: 3500
	[CreateAssetMenu(fileName = "InterfaceToolConfig", menuName = "Klei/Interface Tools/Config")]
	public class InterfaceToolConfig : ScriptableObject
	{
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0029534B File Offset: 0x0029354B
		public DigAction DigAction
		{
			get
			{
				return ActionFactory<DigToolActionFactory, DigAction, DigToolActionFactory.Actions>.GetOrCreateAction(this.digAction);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06006A7C RID: 27260 RVA: 0x00295358 File Offset: 0x00293558
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06006A7D RID: 27261 RVA: 0x00295360 File Offset: 0x00293560
		public global::Action InputAction
		{
			get
			{
				return (global::Action)Enum.Parse(typeof(global::Action), this.inputAction);
			}
		}

		// Token: 0x04004FFC RID: 20476
		[SerializeField]
		private DigToolActionFactory.Actions digAction;

		// Token: 0x04004FFD RID: 20477
		public static InterfaceToolConfig.Comparer ConfigComparer = new InterfaceToolConfig.Comparer();

		// Token: 0x04004FFE RID: 20478
		[SerializeField]
		[Tooltip("Defines which config will take priority should multiple configs be activated\n0 is the lower bound for this value.")]
		private int priority;

		// Token: 0x04004FFF RID: 20479
		[SerializeField]
		[Tooltip("This will serve as a key for activating different configs. Currently, these Actionsare how we indicate that different input modes are desired.\nAssigning Action.Invalid to this field will indicate that this is the \"default\" config")]
		private string inputAction = global::Action.Invalid.ToString();

		// Token: 0x02001E7E RID: 7806
		public class Comparer : IComparer<InterfaceToolConfig>
		{
			// Token: 0x06009BE7 RID: 39911 RVA: 0x00339C43 File Offset: 0x00337E43
			public int Compare(InterfaceToolConfig lhs, InterfaceToolConfig rhs)
			{
				if (lhs.Priority == rhs.Priority)
				{
					return 0;
				}
				if (lhs.Priority <= rhs.Priority)
				{
					return -1;
				}
				return 1;
			}
		}
	}
}
