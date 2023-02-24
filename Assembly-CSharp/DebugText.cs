using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006FB RID: 1787
[AddComponentMenu("KMonoBehaviour/scripts/DebugText")]
public class DebugText : KMonoBehaviour
{
	// Token: 0x060030D9 RID: 12505 RVA: 0x00103464 File Offset: 0x00101664
	public static void DestroyInstance()
	{
		DebugText.Instance = null;
	}

	// Token: 0x060030DA RID: 12506 RVA: 0x0010346C File Offset: 0x0010166C
	protected override void OnPrefabInit()
	{
		DebugText.Instance = this;
	}

	// Token: 0x060030DB RID: 12507 RVA: 0x00103474 File Offset: 0x00101674
	public void Draw(string text, Vector3 pos, Color color)
	{
		DebugText.Entry entry = new DebugText.Entry
		{
			text = text,
			pos = pos,
			color = color
		};
		this.entries.Add(entry);
	}

	// Token: 0x060030DC RID: 12508 RVA: 0x001034B0 File Offset: 0x001016B0
	private void LateUpdate()
	{
		foreach (Text text in this.texts)
		{
			UnityEngine.Object.Destroy(text.gameObject);
		}
		this.texts.Clear();
		foreach (DebugText.Entry entry in this.entries)
		{
			GameObject gameObject = new GameObject();
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(GameScreenManager.Instance.worldSpaceCanvas.GetComponent<RectTransform>());
			gameObject.transform.SetPosition(entry.pos);
			rectTransform.localScale = new Vector3(0.02f, 0.02f, 1f);
			Text text2 = gameObject.AddComponent<Text>();
			text2.font = Assets.DebugFont;
			text2.text = entry.text;
			text2.color = entry.color;
			text2.horizontalOverflow = HorizontalWrapMode.Overflow;
			text2.verticalOverflow = VerticalWrapMode.Overflow;
			text2.alignment = TextAnchor.MiddleCenter;
			this.texts.Add(text2);
		}
		this.entries.Clear();
	}

	// Token: 0x04001D7C RID: 7548
	public static DebugText Instance;

	// Token: 0x04001D7D RID: 7549
	private List<DebugText.Entry> entries = new List<DebugText.Entry>();

	// Token: 0x04001D7E RID: 7550
	private List<Text> texts = new List<Text>();

	// Token: 0x0200141B RID: 5147
	private struct Entry
	{
		// Token: 0x0400628D RID: 25229
		public string text;

		// Token: 0x0400628E RID: 25230
		public Vector3 pos;

		// Token: 0x0400628F RID: 25231
		public Color color;
	}
}
