using System;
using UnityEngine;

// Token: 0x0200086A RID: 2154
public class Polluter : IPolluter
{
	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x0015935C File Offset: 0x0015755C
	// (set) Token: 0x06003DCA RID: 15818 RVA: 0x00159364 File Offset: 0x00157564
	public int radius
	{
		get
		{
			return this._radius;
		}
		private set
		{
			this._radius = value;
			if (this._radius == 0)
			{
				global::Debug.LogFormat("[{0}] has a 0 radius noise, this will disable it", new object[] { this.GetName() });
				return;
			}
		}
	}

	// Token: 0x06003DCB RID: 15819 RVA: 0x0015938F File Offset: 0x0015758F
	public void SetAttributes(Vector2 pos, int dB, GameObject go, string name)
	{
		this.position = pos;
		this.sourceName = name;
		this.decibels = dB;
		this.gameObject = go;
	}

	// Token: 0x06003DCC RID: 15820 RVA: 0x001593AE File Offset: 0x001575AE
	public string GetName()
	{
		return this.sourceName;
	}

	// Token: 0x06003DCD RID: 15821 RVA: 0x001593B6 File Offset: 0x001575B6
	public int GetRadius()
	{
		return this.radius;
	}

	// Token: 0x06003DCE RID: 15822 RVA: 0x001593BE File Offset: 0x001575BE
	public int GetNoise()
	{
		return this.decibels;
	}

	// Token: 0x06003DCF RID: 15823 RVA: 0x001593C6 File Offset: 0x001575C6
	public GameObject GetGameObject()
	{
		return this.gameObject;
	}

	// Token: 0x06003DD0 RID: 15824 RVA: 0x001593CE File Offset: 0x001575CE
	public Polluter(int radius)
	{
		this.radius = radius;
	}

	// Token: 0x06003DD1 RID: 15825 RVA: 0x001593DD File Offset: 0x001575DD
	public void SetSplat(NoiseSplat new_splat)
	{
		if (new_splat == null && this.splat != null)
		{
			this.Clear();
		}
		this.splat = new_splat;
		if (this.splat != null)
		{
			AudioEventManager.Get().AddSplat(this.splat);
		}
	}

	// Token: 0x06003DD2 RID: 15826 RVA: 0x0015940F File Offset: 0x0015760F
	public void Clear()
	{
		if (this.splat != null)
		{
			AudioEventManager.Get().ClearNoiseSplat(this.splat);
			this.splat.Clear();
			this.splat = null;
		}
	}

	// Token: 0x06003DD3 RID: 15827 RVA: 0x0015943B File Offset: 0x0015763B
	public Vector2 GetPosition()
	{
		return this.position;
	}

	// Token: 0x04002879 RID: 10361
	private int _radius;

	// Token: 0x0400287A RID: 10362
	private int decibels;

	// Token: 0x0400287B RID: 10363
	private Vector2 position;

	// Token: 0x0400287C RID: 10364
	private string sourceName;

	// Token: 0x0400287D RID: 10365
	private GameObject gameObject;

	// Token: 0x0400287E RID: 10366
	private NoiseSplat splat;
}
