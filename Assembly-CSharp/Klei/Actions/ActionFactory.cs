using System;
using System.Collections.Generic;

namespace Klei.Actions
{
	// Token: 0x02000DB3 RID: 3507
	public class ActionFactory<ActionFactoryType, ActionType, EnumType> where ActionFactoryType : ActionFactory<ActionFactoryType, ActionType, EnumType>
	{
		// Token: 0x06006A93 RID: 27283 RVA: 0x00295594 File Offset: 0x00293794
		public static ActionType GetOrCreateAction(EnumType actionType)
		{
			ActionType actionType2;
			if (!ActionFactory<ActionFactoryType, ActionType, EnumType>.actionInstances.TryGetValue(actionType, out actionType2))
			{
				ActionFactory<ActionFactoryType, ActionType, EnumType>.EnsureFactoryInstance();
				actionType2 = (ActionFactory<ActionFactoryType, ActionType, EnumType>.actionInstances[actionType] = ActionFactory<ActionFactoryType, ActionType, EnumType>.actionFactory.CreateAction(actionType));
			}
			return actionType2;
		}

		// Token: 0x06006A94 RID: 27284 RVA: 0x002955D3 File Offset: 0x002937D3
		private static void EnsureFactoryInstance()
		{
			if (ActionFactory<ActionFactoryType, ActionType, EnumType>.actionFactory != null)
			{
				return;
			}
			ActionFactory<ActionFactoryType, ActionType, EnumType>.actionFactory = Activator.CreateInstance(typeof(ActionFactoryType)) as ActionFactoryType;
		}

		// Token: 0x06006A95 RID: 27285 RVA: 0x00295600 File Offset: 0x00293800
		protected virtual ActionType CreateAction(EnumType actionType)
		{
			throw new InvalidOperationException("Can not call InterfaceToolActionFactory<T1, T2>.CreateAction()! This function must be called from a deriving class!");
		}

		// Token: 0x04005004 RID: 20484
		private static Dictionary<EnumType, ActionType> actionInstances = new Dictionary<EnumType, ActionType>();

		// Token: 0x04005005 RID: 20485
		private static ActionFactoryType actionFactory = default(ActionFactoryType);
	}
}
