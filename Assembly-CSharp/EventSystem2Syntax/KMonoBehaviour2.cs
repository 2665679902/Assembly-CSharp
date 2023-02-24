using System;

namespace EventSystem2Syntax
{
	// Token: 0x02000C58 RID: 3160
	internal class KMonoBehaviour2
	{
		// Token: 0x06006477 RID: 25719 RVA: 0x0025A960 File Offset: 0x00258B60
		protected virtual void OnPrefabInit()
		{
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x0025A962 File Offset: 0x00258B62
		public void Subscribe(int evt, Action<object> cb)
		{
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x0025A964 File Offset: 0x00258B64
		public void Trigger(int evt, object data)
		{
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x0025A966 File Offset: 0x00258B66
		public void Subscribe<ListenerType, EventType>(Action<ListenerType, EventType> cb) where EventType : IEventData
		{
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x0025A968 File Offset: 0x00258B68
		public void Trigger<EventType>(EventType evt) where EventType : IEventData
		{
		}
	}
}
