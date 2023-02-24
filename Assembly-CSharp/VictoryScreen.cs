using System;
using UnityEngine;

// Token: 0x02000C20 RID: 3104
public class VictoryScreen : KModalScreen
{
	// Token: 0x06006242 RID: 25154 RVA: 0x002448B3 File Offset: 0x00242AB3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Init();
	}

	// Token: 0x06006243 RID: 25155 RVA: 0x002448C1 File Offset: 0x00242AC1
	private void Init()
	{
		if (this.DismissButton)
		{
			this.DismissButton.onClick += delegate
			{
				this.Dismiss();
			};
		}
	}

	// Token: 0x06006244 RID: 25156 RVA: 0x002448E7 File Offset: 0x00242AE7
	private void Retire()
	{
		if (RetireColonyUtility.SaveColonySummaryData())
		{
			this.Show(false);
		}
	}

	// Token: 0x06006245 RID: 25157 RVA: 0x002448F7 File Offset: 0x00242AF7
	private void Dismiss()
	{
		this.Show(false);
	}

	// Token: 0x06006246 RID: 25158 RVA: 0x00244900 File Offset: 0x00242B00
	public void SetAchievements(string[] achievementIDs)
	{
		string text = "";
		for (int i = 0; i < achievementIDs.Length; i++)
		{
			if (i > 0)
			{
				text += "\n";
			}
			text += GameUtil.ApplyBoldString(Db.Get().ColonyAchievements.Get(achievementIDs[i]).Name);
			text = text + "\n" + Db.Get().ColonyAchievements.Get(achievementIDs[i]).description;
		}
		this.descriptionText.text = text;
	}

	// Token: 0x040043F8 RID: 17400
	[SerializeField]
	private KButton DismissButton;

	// Token: 0x040043F9 RID: 17401
	[SerializeField]
	private LocText descriptionText;
}
