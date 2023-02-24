using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000432 RID: 1074
[AddComponentMenu("KMonoBehaviour/scripts/AudioEventManager")]
public class AudioEventManager : KMonoBehaviour
{
	// Token: 0x06001721 RID: 5921 RVA: 0x000782C0 File Offset: 0x000764C0
	public static AudioEventManager Get()
	{
		if (AudioEventManager.instance == null)
		{
			if (App.IsExiting)
			{
				return null;
			}
			GameObject gameObject = GameObject.Find("/AudioEventManager");
			if (gameObject == null)
			{
				gameObject = new GameObject();
				gameObject.name = "AudioEventManager";
			}
			AudioEventManager.instance = gameObject.GetComponent<AudioEventManager>();
			if (AudioEventManager.instance == null)
			{
				AudioEventManager.instance = gameObject.AddComponent<AudioEventManager>();
			}
		}
		return AudioEventManager.instance;
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x00078330 File Offset: 0x00076530
	protected override void OnSpawn()
	{
		base.OnPrefabInit();
		this.spatialSplats.Reset(Grid.WidthInCells, Grid.HeightInCells, 16, 16);
	}

	// Token: 0x06001723 RID: 5923 RVA: 0x00078351 File Offset: 0x00076551
	public static float LoudnessToDB(float loudness)
	{
		if (loudness <= 0f)
		{
			return 0f;
		}
		return 10f * Mathf.Log10(loudness);
	}

	// Token: 0x06001724 RID: 5924 RVA: 0x0007836D File Offset: 0x0007656D
	public static float DBToLoudness(float src_db)
	{
		return Mathf.Pow(10f, src_db / 10f);
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x00078380 File Offset: 0x00076580
	public float GetDecibelsAtCell(int cell)
	{
		return Mathf.Round(AudioEventManager.LoudnessToDB(Grid.Loudness[cell]) * 2f) / 2f;
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000783A0 File Offset: 0x000765A0
	public static string GetLoudestNoisePolluterAtCell(int cell)
	{
		float negativeInfinity = float.NegativeInfinity;
		string text = null;
		AudioEventManager audioEventManager = AudioEventManager.Get();
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2 vector = new Vector2((float)vector2I.x, (float)vector2I.y);
		foreach (object obj in audioEventManager.spatialSplats.GetAllIntersecting(vector))
		{
			NoiseSplat noiseSplat = (NoiseSplat)obj;
			if (noiseSplat.GetLoudness(cell) > negativeInfinity)
			{
				text = noiseSplat.GetProvider().GetName();
			}
		}
		return text;
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x00078444 File Offset: 0x00076644
	public void ClearNoiseSplat(NoiseSplat splat)
	{
		if (this.splats.Contains(splat))
		{
			this.splats.Remove(splat);
			this.spatialSplats.Remove(splat);
		}
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x0007846D File Offset: 0x0007666D
	public void AddSplat(NoiseSplat splat)
	{
		this.splats.Add(splat);
		this.spatialSplats.Add(splat);
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x00078488 File Offset: 0x00076688
	public NoiseSplat CreateNoiseSplat(Vector2 pos, int dB, int radius, string name, GameObject go)
	{
		Polluter polluter = this.GetPolluter(radius);
		polluter.SetAttributes(pos, dB, go, name);
		NoiseSplat noiseSplat = new NoiseSplat(polluter, 0f);
		polluter.SetSplat(noiseSplat);
		return noiseSplat;
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x000784BC File Offset: 0x000766BC
	public List<AudioEventManager.PolluterDisplay> GetPollutersForCell(int cell)
	{
		this.polluters.Clear();
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2 vector = new Vector2((float)vector2I.x, (float)vector2I.y);
		foreach (object obj in this.spatialSplats.GetAllIntersecting(vector))
		{
			NoiseSplat noiseSplat = (NoiseSplat)obj;
			float loudness = noiseSplat.GetLoudness(cell);
			if (loudness > 0f)
			{
				AudioEventManager.PolluterDisplay polluterDisplay = default(AudioEventManager.PolluterDisplay);
				polluterDisplay.name = noiseSplat.GetName();
				polluterDisplay.value = AudioEventManager.LoudnessToDB(loudness);
				polluterDisplay.provider = noiseSplat.GetProvider();
				this.polluters.Add(polluterDisplay);
			}
		}
		return this.polluters;
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x00078594 File Offset: 0x00076794
	private void RemoveExpiredSplats()
	{
		if (this.removeTime.Count > 1)
		{
			this.removeTime.Sort((Pair<float, NoiseSplat> a, Pair<float, NoiseSplat> b) => a.first.CompareTo(b.first));
		}
		int num = -1;
		int num2 = 0;
		while (num2 < this.removeTime.Count && this.removeTime[num2].first <= Time.time)
		{
			NoiseSplat second = this.removeTime[num2].second;
			if (second != null)
			{
				IPolluter provider = second.GetProvider();
				this.FreePolluter(provider as Polluter);
			}
			num = num2;
			num2++;
		}
		for (int i = num; i >= 0; i--)
		{
			this.removeTime.RemoveAt(i);
		}
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x00078650 File Offset: 0x00076850
	private void Update()
	{
		this.RemoveExpiredSplats();
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x00078658 File Offset: 0x00076858
	private Polluter GetPolluter(int radius)
	{
		if (!this.freePool.ContainsKey(radius))
		{
			this.freePool.Add(radius, new List<Polluter>());
		}
		Polluter polluter;
		if (this.freePool[radius].Count > 0)
		{
			polluter = this.freePool[radius][0];
			this.freePool[radius].RemoveAt(0);
		}
		else
		{
			polluter = new Polluter(radius);
		}
		if (!this.inusePool.ContainsKey(radius))
		{
			this.inusePool.Add(radius, new List<Polluter>());
		}
		this.inusePool[radius].Add(polluter);
		return polluter;
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000786FC File Offset: 0x000768FC
	private void FreePolluter(Polluter pol)
	{
		if (pol != null)
		{
			pol.Clear();
			global::Debug.Assert(this.inusePool[pol.radius].Contains(pol));
			this.inusePool[pol.radius].Remove(pol);
			this.freePool[pol.radius].Add(pol);
		}
	}

	// Token: 0x0600172F RID: 5935 RVA: 0x00078760 File Offset: 0x00076960
	public void PlayTimedOnceOff(Vector2 pos, int dB, int radius, string name, GameObject go, float time = 1f)
	{
		if (dB > 0 && radius > 0 && time > 0f)
		{
			Polluter polluter = this.GetPolluter(radius);
			polluter.SetAttributes(pos, dB, go, name);
			this.AddTimedInstance(polluter, time);
		}
	}

	// Token: 0x06001730 RID: 5936 RVA: 0x0007879C File Offset: 0x0007699C
	private void AddTimedInstance(Polluter p, float time)
	{
		NoiseSplat noiseSplat = new NoiseSplat(p, time + Time.time);
		p.SetSplat(noiseSplat);
		this.removeTime.Add(new Pair<float, NoiseSplat>(time + Time.time, noiseSplat));
	}

	// Token: 0x06001731 RID: 5937 RVA: 0x000787D6 File Offset: 0x000769D6
	private static void SoundLog(long itemId, string message)
	{
		global::Debug.Log(" [" + itemId.ToString() + "] \t" + message);
	}

	// Token: 0x04000CD2 RID: 3282
	public const float NO_NOISE_EFFECTORS = 0f;

	// Token: 0x04000CD3 RID: 3283
	public const float MIN_LOUDNESS_THRESHOLD = 1f;

	// Token: 0x04000CD4 RID: 3284
	private static AudioEventManager instance;

	// Token: 0x04000CD5 RID: 3285
	private List<Pair<float, NoiseSplat>> removeTime = new List<Pair<float, NoiseSplat>>();

	// Token: 0x04000CD6 RID: 3286
	private Dictionary<int, List<Polluter>> freePool = new Dictionary<int, List<Polluter>>();

	// Token: 0x04000CD7 RID: 3287
	private Dictionary<int, List<Polluter>> inusePool = new Dictionary<int, List<Polluter>>();

	// Token: 0x04000CD8 RID: 3288
	private HashSet<NoiseSplat> splats = new HashSet<NoiseSplat>();

	// Token: 0x04000CD9 RID: 3289
	private UniformGrid<NoiseSplat> spatialSplats = new UniformGrid<NoiseSplat>();

	// Token: 0x04000CDA RID: 3290
	private List<AudioEventManager.PolluterDisplay> polluters = new List<AudioEventManager.PolluterDisplay>();

	// Token: 0x02001053 RID: 4179
	public enum NoiseEffect
	{
		// Token: 0x04005720 RID: 22304
		Peaceful,
		// Token: 0x04005721 RID: 22305
		Quiet = 36,
		// Token: 0x04005722 RID: 22306
		TossAndTurn = 45,
		// Token: 0x04005723 RID: 22307
		WakeUp = 60,
		// Token: 0x04005724 RID: 22308
		Passive = 80,
		// Token: 0x04005725 RID: 22309
		Active = 106
	}

	// Token: 0x02001054 RID: 4180
	public struct PolluterDisplay
	{
		// Token: 0x04005726 RID: 22310
		public string name;

		// Token: 0x04005727 RID: 22311
		public float value;

		// Token: 0x04005728 RID: 22312
		public IPolluter provider;
	}
}
