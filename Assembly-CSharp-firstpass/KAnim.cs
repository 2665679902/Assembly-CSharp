using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class KAnim
{
	// Token: 0x02000954 RID: 2388
	public enum PlayMode
	{
		// Token: 0x0400203A RID: 8250
		Loop,
		// Token: 0x0400203B RID: 8251
		Once,
		// Token: 0x0400203C RID: 8252
		Paused
	}

	// Token: 0x02000955 RID: 2389
	public enum LayerFlags
	{
		// Token: 0x0400203E RID: 8254
		FG = 1
	}

	// Token: 0x02000956 RID: 2390
	public enum SymbolFlags
	{
		// Token: 0x04002040 RID: 8256
		Bloom = 1,
		// Token: 0x04002041 RID: 8257
		OnLight,
		// Token: 0x04002042 RID: 8258
		SnapTo = 4,
		// Token: 0x04002043 RID: 8259
		FG = 8
	}

	// Token: 0x02000957 RID: 2391
	[Serializable]
	public struct AnimHashTable
	{
		// Token: 0x04002044 RID: 8260
		public KAnimHashedString[] hashes;
	}

	// Token: 0x02000958 RID: 2392
	[DebuggerDisplay("{id} {animFile}")]
	[Serializable]
	public class Anim
	{
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x0009A977 File Offset: 0x00098B77
		// (set) Token: 0x060052A4 RID: 21156 RVA: 0x0009A97F File Offset: 0x00098B7F
		public int index { get; private set; }

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x0009A988 File Offset: 0x00098B88
		// (set) Token: 0x060052A6 RID: 21158 RVA: 0x0009A990 File Offset: 0x00098B90
		public KAnimFileData animFile { get; private set; }

		// Token: 0x060052A7 RID: 21159 RVA: 0x0009A999 File Offset: 0x00098B99
		public Anim(KAnimFileData anim_file, int idx)
		{
			this.animFile = anim_file;
			this.index = idx;
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0009A9BC File Offset: 0x00098BBC
		public int GetFrameIdx(KAnim.PlayMode mode, float elapsedSeconds)
		{
			if (this.numFrames <= 0)
			{
				return -1;
			}
			int num = 0;
			if (mode != KAnim.PlayMode.Loop)
			{
				if (mode != KAnim.PlayMode.Once)
				{
				}
			}
			else
			{
				elapsedSeconds %= this.totalTime;
			}
			if (elapsedSeconds > 0f)
			{
				float num2 = elapsedSeconds * this.frameRate + 0.49999997f;
				num = Math.Min(this.numFrames - 1, (int)num2);
			}
			return num;
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0009AA14 File Offset: 0x00098C14
		private static KBatchGroupData GetAnimBatchGroupData(KAnimFileData animFile)
		{
			if (!animFile.batchTag.IsValid)
			{
				global::Debug.LogErrorFormat("Invalid batchTag for anim [{0}]", new object[] { animFile.name });
			}
			global::Debug.Assert(animFile.batchTag.IsValid, "Invalid batch tag");
			KAnimGroupFile.Group group = KAnimGroupFile.GetGroup(animFile.batchTag);
			if (group == null)
			{
				global::Debug.LogErrorFormat("Null group for tag [{0}]", new object[] { animFile.batchTag });
			}
			HashedString hashedString = animFile.batchTag;
			if (group.renderType == KAnimBatchGroup.RendererType.DontRender || group.renderType == KAnimBatchGroup.RendererType.AnimOnly)
			{
				if (!group.swapTarget.IsValid)
				{
					global::Debug.LogErrorFormat("Invalid swap target for group [{0}]", new object[] { group.id });
				}
				hashedString = group.swapTarget;
			}
			KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(hashedString);
			if (batchGroupData == null)
			{
				global::Debug.LogErrorFormat("Null batch group for tag [{0}]", new object[] { hashedString });
			}
			return batchGroupData;
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0009AB00 File Offset: 0x00098D00
		public KAnim.Anim.Frame GetFrame(KAnimFileData animFile, KAnim.PlayMode mode, float t)
		{
			int frameIdx = this.GetFrameIdx(mode, t);
			KAnim.Anim.Frame frame;
			if (frameIdx >= 0 && animFile.batchTag.IsValid && animFile.batchTag != KAnimBatchManager.NO_BATCH)
			{
				frame = KAnim.Anim.GetAnimBatchGroupData(animFile).GetFrame(this.firstFrameIdx + frameIdx);
			}
			else
			{
				frame = KAnim.Anim.Frame.InvalidFrame;
			}
			return frame;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0009AB56 File Offset: 0x00098D56
		public KAnim.Anim.Frame GetFrame(HashedString batchTag, int idx)
		{
			return KAnimBatchManager.Instance().GetBatchGroupData(batchTag).GetFrame(idx + this.firstFrameIdx);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0009AB70 File Offset: 0x00098D70
		public KAnim.Anim Copy()
		{
			return new KAnim.Anim(this.animFile, this.index)
			{
				name = this.name,
				id = this.id,
				hash = this.hash,
				rootSymbol = this.rootSymbol,
				frameRate = this.frameRate,
				firstFrameIdx = this.firstFrameIdx,
				numFrames = this.numFrames,
				totalTime = this.totalTime,
				scaledBoundingRadius = this.scaledBoundingRadius,
				unScaledSize = this.unScaledSize
			};
		}

		// Token: 0x04002045 RID: 8261
		public string name;

		// Token: 0x04002046 RID: 8262
		public HashedString id;

		// Token: 0x04002048 RID: 8264
		public float frameRate;

		// Token: 0x04002049 RID: 8265
		public int firstFrameIdx;

		// Token: 0x0400204A RID: 8266
		public int numFrames;

		// Token: 0x0400204B RID: 8267
		public HashedString rootSymbol;

		// Token: 0x0400204C RID: 8268
		public HashedString hash;

		// Token: 0x0400204D RID: 8269
		public float totalTime;

		// Token: 0x0400204E RID: 8270
		public float scaledBoundingRadius;

		// Token: 0x0400204F RID: 8271
		public Vector2 unScaledSize = Vector2.zero;

		// Token: 0x02000B3C RID: 2876
		[Serializable]
		public struct Frame
		{
			// Token: 0x06005899 RID: 22681 RVA: 0x000A49D2 File Offset: 0x000A2BD2
			public bool IsValid()
			{
				return this.idx != -1;
			}

			// Token: 0x0600589A RID: 22682 RVA: 0x000A49E0 File Offset: 0x000A2BE0
			public static bool operator ==(KAnim.Anim.Frame a, KAnim.Anim.Frame b)
			{
				return a.idx == b.idx;
			}

			// Token: 0x0600589B RID: 22683 RVA: 0x000A49F0 File Offset: 0x000A2BF0
			public static bool operator !=(KAnim.Anim.Frame a, KAnim.Anim.Frame b)
			{
				return a.idx != b.idx;
			}

			// Token: 0x0600589C RID: 22684 RVA: 0x000A4A04 File Offset: 0x000A2C04
			public override bool Equals(object obj)
			{
				KAnim.Anim.Frame frame = (KAnim.Anim.Frame)obj;
				return this.idx == frame.idx;
			}

			// Token: 0x0600589D RID: 22685 RVA: 0x000A4A26 File Offset: 0x000A2C26
			public override int GetHashCode()
			{
				return this.idx;
			}

			// Token: 0x04002664 RID: 9828
			public AABB3 bbox;

			// Token: 0x04002665 RID: 9829
			public int firstElementIdx;

			// Token: 0x04002666 RID: 9830
			public int idx;

			// Token: 0x04002667 RID: 9831
			public int numElements;

			// Token: 0x04002668 RID: 9832
			public static readonly KAnim.Anim.Frame InvalidFrame = new KAnim.Anim.Frame
			{
				idx = -1
			};
		}

		// Token: 0x02000B3D RID: 2877
		[Serializable]
		public struct FrameElement
		{
			// Token: 0x04002669 RID: 9833
			public KAnimHashedString fileHash;

			// Token: 0x0400266A RID: 9834
			public KAnimHashedString symbol;

			// Token: 0x0400266B RID: 9835
			public int symbolIdx;

			// Token: 0x0400266C RID: 9836
			public KAnimHashedString folder;

			// Token: 0x0400266D RID: 9837
			public int frame;

			// Token: 0x0400266E RID: 9838
			public Matrix2x3 transform;

			// Token: 0x0400266F RID: 9839
			public Color multColour;
		}
	}

	// Token: 0x02000959 RID: 2393
	[Serializable]
	public class Build : ISerializationCallbackReceiver
	{
		// Token: 0x060052AD RID: 21165 RVA: 0x0009AC06 File Offset: 0x00098E06
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0009AC08 File Offset: 0x00098E08
		public void OnAfterDeserialize()
		{
			if (this.symbols != null)
			{
				for (int i = 0; i < this.symbols.Length; i++)
				{
					this.symbols[i].build = this;
				}
			}
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0009AC3E File Offset: 0x00098E3E
		public KAnim.Build.Symbol GetSymbolByIndex(uint index)
		{
			if ((ulong)index >= (ulong)((long)this.symbols.Length))
			{
				return null;
			}
			return this.symbols[(int)index];
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0009AC58 File Offset: 0x00098E58
		public Texture2D GetTexture(int index)
		{
			if (index < 0 || index >= this.textureCount)
			{
				global::Debug.LogError("Invalid texture index:" + index.ToString());
			}
			return KAnimBatchManager.Instance().GetBatchGroupData(this.batchTag).GetTexure(this.textureStartIdx + index);
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0009ACA8 File Offset: 0x00098EA8
		public int GetSymbolOffset(KAnimHashedString symbol_name)
		{
			for (int i = 0; i < this.symbols.Length; i++)
			{
				if (this.symbols[i].hash == symbol_name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0009ACE0 File Offset: 0x00098EE0
		public KAnim.Build.Symbol GetSymbol(KAnimHashedString symbol_name)
		{
			for (int i = 0; i < this.symbols.Length; i++)
			{
				if (this.symbols[i].hash == symbol_name)
				{
					return this.symbols[i];
				}
			}
			return null;
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x0009AD1F File Offset: 0x00098F1F
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04002051 RID: 8273
		public KAnimHashedString fileHash;

		// Token: 0x04002052 RID: 8274
		public int index;

		// Token: 0x04002053 RID: 8275
		public string name;

		// Token: 0x04002054 RID: 8276
		public HashedString batchTag;

		// Token: 0x04002055 RID: 8277
		public int textureStartIdx;

		// Token: 0x04002056 RID: 8278
		public int textureCount;

		// Token: 0x04002057 RID: 8279
		public KAnim.Build.Symbol[] symbols;

		// Token: 0x04002058 RID: 8280
		public KAnim.Build.SymbolFrame[] frames;

		// Token: 0x02000B3E RID: 2878
		[Serializable]
		public class SymbolFrame : IComparable<KAnim.Build.SymbolFrame>
		{
			// Token: 0x0600589F RID: 22687 RVA: 0x000A4A53 File Offset: 0x000A2C53
			public int CompareTo(KAnim.Build.SymbolFrame obj)
			{
				return this.sourceFrameNum.CompareTo(obj.sourceFrameNum);
			}

			// Token: 0x04002670 RID: 9840
			public int sourceFrameNum;

			// Token: 0x04002671 RID: 9841
			public int duration;

			// Token: 0x04002672 RID: 9842
			public KAnimHashedString fileNameHash;

			// Token: 0x04002673 RID: 9843
			public Vector2 uvMin;

			// Token: 0x04002674 RID: 9844
			public Vector2 uvMax;

			// Token: 0x04002675 RID: 9845
			public Vector2 bboxMin;

			// Token: 0x04002676 RID: 9846
			public Vector2 bboxMax;
		}

		// Token: 0x02000B3F RID: 2879
		public struct SymbolFrameInstance
		{
			// Token: 0x04002677 RID: 9847
			public KAnim.Build.SymbolFrame symbolFrame;

			// Token: 0x04002678 RID: 9848
			public int buildImageIdx;

			// Token: 0x04002679 RID: 9849
			public int symbolIdx;
		}

		// Token: 0x02000B40 RID: 2880
		[DebuggerDisplay("{hash} {path} {folder} {colourChannel}")]
		[Serializable]
		public class Symbol : IComparable
		{
			// Token: 0x060058A1 RID: 22689 RVA: 0x000A4A70 File Offset: 0x000A2C70
			public int GetFrameIdx(int frame)
			{
				if (this.frameLookup == null)
				{
					global::Debug.LogErrorFormat("Cant get frame [{2}] because Symbol [{0}] for build [{1}] batch [{3}] has no frameLookup", new object[]
					{
						this.hash.ToString(),
						this.build.name,
						frame,
						this.build.batchTag.ToString()
					});
				}
				if (this.frameLookup.Length == 0 || frame >= this.frameLookup.Length)
				{
					return -1;
				}
				frame = Math.Min(frame, this.frameLookup.Length - 1);
				return this.frameLookup[frame];
			}

			// Token: 0x060058A2 RID: 22690 RVA: 0x000A4B09 File Offset: 0x000A2D09
			public bool HasFrame(int frame)
			{
				return this.GetFrameIdx(frame) >= 0;
			}

			// Token: 0x060058A3 RID: 22691 RVA: 0x000A4B18 File Offset: 0x000A2D18
			public KAnim.Build.SymbolFrameInstance GetFrame(int frame)
			{
				int frameIdx = this.GetFrameIdx(frame);
				return KAnimBatchManager.Instance().GetBatchGroupData(this.build.batchTag).GetSymbolFrameInstance(frameIdx);
			}

			// Token: 0x060058A4 RID: 22692 RVA: 0x000A4B48 File Offset: 0x000A2D48
			public int CompareTo(object obj)
			{
				if (obj == null)
				{
					return 1;
				}
				if (obj.GetType() == typeof(HashedString))
				{
					HashedString hashedString = (HashedString)obj;
					return this.hash.HashValue.CompareTo(hashedString.HashValue);
				}
				KAnim.Build.Symbol symbol = (KAnim.Build.Symbol)obj;
				return this.hash.HashValue.CompareTo(symbol.hash.HashValue);
			}

			// Token: 0x060058A5 RID: 22693 RVA: 0x000A4BB8 File Offset: 0x000A2DB8
			public bool HasFlag(KAnim.SymbolFlags flag)
			{
				return (this.flags & (int)flag) != 0;
			}

			// Token: 0x060058A6 RID: 22694 RVA: 0x000A4BC8 File Offset: 0x000A2DC8
			public KAnim.Build.Symbol Copy()
			{
				KAnim.Build.Symbol symbol = new KAnim.Build.Symbol();
				symbol.hash = this.hash;
				symbol.path = this.path;
				symbol.folder = this.folder;
				symbol.colourChannel = this.colourChannel;
				symbol.flags = this.flags;
				symbol.firstFrameIdx = this.firstFrameIdx;
				symbol.numFrames = this.numFrames;
				symbol.numLookupFrames = this.numLookupFrames;
				symbol.frameLookup = new int[this.frameLookup.Length];
				symbol.symbolIndexInSourceBuild = this.symbolIndexInSourceBuild;
				Array.Copy(this.frameLookup, symbol.frameLookup, symbol.frameLookup.Length);
				return symbol;
			}

			// Token: 0x0400267A RID: 9850
			[NonSerialized]
			public KAnim.Build build;

			// Token: 0x0400267B RID: 9851
			public KAnimHashedString hash;

			// Token: 0x0400267C RID: 9852
			public KAnimHashedString path;

			// Token: 0x0400267D RID: 9853
			public KAnimHashedString folder;

			// Token: 0x0400267E RID: 9854
			public KAnimHashedString colourChannel;

			// Token: 0x0400267F RID: 9855
			public int flags;

			// Token: 0x04002680 RID: 9856
			public int firstFrameIdx;

			// Token: 0x04002681 RID: 9857
			public int numFrames;

			// Token: 0x04002682 RID: 9858
			public int numLookupFrames;

			// Token: 0x04002683 RID: 9859
			public int[] frameLookup;

			// Token: 0x04002684 RID: 9860
			public int symbolIndexInSourceBuild;
		}
	}
}
