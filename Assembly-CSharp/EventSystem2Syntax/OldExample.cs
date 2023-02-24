using System;

namespace EventSystem2Syntax
{
	// Token: 0x02000C56 RID: 3158
	internal class OldExample : KMonoBehaviour2
	{
		// Token: 0x06006471 RID: 25713 RVA: 0x0025A8D0 File Offset: 0x00258AD0
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			base.Subscribe(0, new Action<object>(this.OnObjectDestroyed));
			bool flag = false;
			base.Trigger(0, flag);
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x0025A905 File Offset: 0x00258B05
		private void OnObjectDestroyed(object data)
		{
			Debug.Log((bool)data);
		}
	}
}
