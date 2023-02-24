using System;

namespace Klei
{
	// Token: 0x02000D52 RID: 3410
	public struct CallbackInfo
	{
		// Token: 0x06006851 RID: 26705 RVA: 0x0028A535 File Offset: 0x00288735
		public CallbackInfo(HandleVector<Game.CallbackInfo>.Handle h)
		{
			this.handle = h;
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x0028A540 File Offset: 0x00288740
		public void Release()
		{
			if (this.handle.IsValid())
			{
				Game.CallbackInfo item = Game.Instance.callbackManager.GetItem(this.handle);
				System.Action cb = item.cb;
				if (!item.manuallyRelease)
				{
					Game.Instance.callbackManager.Release(this.handle);
				}
				cb();
			}
		}

		// Token: 0x04004E78 RID: 20088
		private HandleVector<Game.CallbackInfo>.Handle handle;
	}
}
