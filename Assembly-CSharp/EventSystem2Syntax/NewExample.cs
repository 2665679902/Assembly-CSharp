using System;

namespace EventSystem2Syntax
{
	// Token: 0x02000C57 RID: 3159
	internal class NewExample : KMonoBehaviour2
	{
		// Token: 0x06006474 RID: 25716 RVA: 0x0025A920 File Offset: 0x00258B20
		protected override void OnPrefabInit()
		{
			base.Subscribe<NewExample, NewExample.ObjectDestroyedEvent>(new Action<NewExample, NewExample.ObjectDestroyedEvent>(NewExample.OnObjectDestroyed));
			base.Trigger<NewExample.ObjectDestroyedEvent>(new NewExample.ObjectDestroyedEvent
			{
				parameter = false
			});
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x0025A956 File Offset: 0x00258B56
		private static void OnObjectDestroyed(NewExample example, NewExample.ObjectDestroyedEvent evt)
		{
		}

		// Token: 0x02001B04 RID: 6916
		private struct ObjectDestroyedEvent : IEventData
		{
			// Token: 0x0400796C RID: 31084
			public bool parameter;
		}
	}
}
