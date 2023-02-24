using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
[AddComponentMenu("KMonoBehaviour/scripts/LightColorMenu")]
public class LightColorMenu : KMonoBehaviour
{
	// Token: 0x06001A51 RID: 6737 RVA: 0x0008C27A File Offset: 0x0008A47A
	protected override void OnPrefabInit()
	{
		base.Subscribe<LightColorMenu>(493375141, LightColorMenu.OnRefreshUserMenuDelegate);
		this.SetColor(0);
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x0008C294 File Offset: 0x0008A494
	private void OnRefreshUserMenu(object data)
	{
		if (this.lightColors.Length != 0)
		{
			int num = this.lightColors.Length;
			for (int i = 0; i < num; i++)
			{
				if (i != this.currentColor)
				{
					int new_color = i;
					Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo(this.lightColors[i].name, this.lightColors[i].name, delegate
					{
						this.SetColor(new_color);
					}, global::Action.NumActions, null, null, null, "", true), 1f);
				}
			}
		}
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x0008C33C File Offset: 0x0008A53C
	private void SetColor(int color_index)
	{
		if (this.lightColors.Length != 0 && color_index < this.lightColors.Length)
		{
			Light2D[] componentsInChildren = base.GetComponentsInChildren<Light2D>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Color = this.lightColors[color_index].color;
			}
			MeshRenderer[] componentsInChildren2 = base.GetComponentsInChildren<MeshRenderer>(true);
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				foreach (Material material in componentsInChildren2[i].materials)
				{
					if (material.name.StartsWith("matScriptedGlow01"))
					{
						material.color = this.lightColors[color_index].color;
					}
				}
			}
		}
		this.currentColor = color_index;
	}

	// Token: 0x04000EA4 RID: 3748
	public LightColorMenu.LightColor[] lightColors;

	// Token: 0x04000EA5 RID: 3749
	private int currentColor;

	// Token: 0x04000EA6 RID: 3750
	private static readonly EventSystem.IntraObjectHandler<LightColorMenu> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<LightColorMenu>(delegate(LightColorMenu component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x020010D3 RID: 4307
	[Serializable]
	public struct LightColor
	{
		// Token: 0x06007492 RID: 29842 RVA: 0x002B3710 File Offset: 0x002B1910
		public LightColor(string name, Color color)
		{
			this.name = name;
			this.color = color;
		}

		// Token: 0x040058C9 RID: 22729
		public string name;

		// Token: 0x040058CA RID: 22730
		public Color color;
	}
}
