using System;
using System.Collections.Generic;

// Token: 0x02000513 RID: 1299
public class DevPanelList
{
	// Token: 0x06001F3E RID: 7998 RVA: 0x000A6014 File Offset: 0x000A4214
	public DevPanel AddPanelFor<T>() where T : DevTool, new()
	{
		return this.AddPanelFor(new T());
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x000A6028 File Offset: 0x000A4228
	public DevPanel AddPanelFor(DevTool devTool)
	{
		DevPanel devPanel = new DevPanel(devTool, this);
		this.activePanels.Add(devPanel);
		return devPanel;
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000A604C File Offset: 0x000A424C
	public Option<T> GetDevTool<T>() where T : DevTool
	{
		foreach (DevPanel devPanel in this.activePanels)
		{
			T t = devPanel.GetCurrentDevTool() as T;
			if (t != null)
			{
				return t;
			}
		}
		return Option.None;
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x000A60C4 File Offset: 0x000A42C4
	public T AddOrGetDevTool<T>() where T : DevTool, new()
	{
		bool flag;
		T t;
		this.GetDevTool<T>().Deconstruct(out flag, out t);
		bool flag2 = flag;
		T t2 = t;
		if (!flag2)
		{
			t2 = new T();
			this.AddPanelFor(t2);
		}
		return t2;
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x000A60FC File Offset: 0x000A42FC
	public void ClosePanel(DevPanel panel)
	{
		if (this.activePanels.Remove(panel))
		{
			panel.Internal_Uninit();
		}
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x000A6114 File Offset: 0x000A4314
	public void Render()
	{
		if (this.activePanels.Count == 0)
		{
			return;
		}
		using (ListPool<DevPanel, DevPanelList>.PooledList pooledList = ListPool<DevPanel, DevPanelList>.Allocate())
		{
			for (int i = 0; i < this.activePanels.Count; i++)
			{
				DevPanel devPanel = this.activePanels[i];
				devPanel.RenderPanel();
				if (devPanel.isRequestingToClose)
				{
					pooledList.Add(devPanel);
				}
			}
			foreach (DevPanel devPanel2 in pooledList)
			{
				this.ClosePanel(devPanel2);
			}
		}
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x000A61C8 File Offset: 0x000A43C8
	public void Internal_InitPanelId(Type initialDevToolType, out string panelId, out uint idPostfixNumber)
	{
		idPostfixNumber = this.Internal_GetUniqueIdPostfix(initialDevToolType);
		panelId = initialDevToolType.Name + idPostfixNumber.ToString();
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x000A61E8 File Offset: 0x000A43E8
	public uint Internal_GetUniqueIdPostfix(Type initialDevToolType)
	{
		uint num3;
		using (HashSetPool<uint, DevPanelList>.PooledHashSet pooledHashSet = HashSetPool<uint, DevPanelList>.Allocate())
		{
			foreach (DevPanel devPanel in this.activePanels)
			{
				if (!(devPanel.initialDevToolType != initialDevToolType))
				{
					pooledHashSet.Add(devPanel.idPostfixNumber);
				}
			}
			for (uint num = 0U; num < 100U; num += 1U)
			{
				if (!pooledHashSet.Contains(num))
				{
					return num;
				}
			}
			Debug.Assert(false, "Something went wrong, this should only assert if there's over 100 of the same type of debug window");
			uint num2 = this.fallbackUniqueIdPostfixNumber;
			this.fallbackUniqueIdPostfixNumber = num2 + 1U;
			num3 = num2;
		}
		return num3;
	}

	// Token: 0x040011B7 RID: 4535
	private List<DevPanel> activePanels = new List<DevPanel>();

	// Token: 0x040011B8 RID: 4536
	private uint fallbackUniqueIdPostfixNumber = 300U;
}
