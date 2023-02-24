using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;

// Token: 0x02000408 RID: 1032
public class StateMachineSerializer
{
	// Token: 0x0600156C RID: 5484 RVA: 0x0006F000 File Offset: 0x0006D200
	public void Serialize(List<StateMachine.Instance> state_machines, BinaryWriter writer)
	{
		writer.Write(StateMachineSerializer.SERIALIZER_VERSION);
		long position = writer.BaseStream.Position;
		writer.Write(0);
		long position2 = writer.BaseStream.Position;
		try
		{
			int num = (int)writer.BaseStream.Position;
			int num2 = 0;
			writer.Write(num2);
			using (List<StateMachine.Instance>.Enumerator enumerator = state_machines.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (StateMachineSerializer.Entry.TrySerialize(enumerator.Current, writer))
					{
						num2++;
					}
				}
			}
			int num3 = (int)writer.BaseStream.Position;
			writer.BaseStream.Position = (long)num;
			writer.Write(num2);
			writer.BaseStream.Position = (long)num3;
		}
		catch (Exception ex)
		{
			Debug.Log("StateMachines: ");
			foreach (StateMachine.Instance instance in state_machines)
			{
				Debug.Log(instance.ToString());
			}
			Debug.LogError(ex);
		}
		long position3 = writer.BaseStream.Position;
		long num4 = position3 - position2;
		writer.BaseStream.Position = position;
		writer.Write((int)num4);
		writer.BaseStream.Position = position3;
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x0006F160 File Offset: 0x0006D360
	public void Deserialize(IReader reader)
	{
		int num = reader.ReadInt32();
		int num2 = reader.ReadInt32();
		if (num < 10)
		{
			Debug.LogWarning(string.Concat(new string[]
			{
				"State machine serializer version mismatch: ",
				num.ToString(),
				"!=",
				StateMachineSerializer.SERIALIZER_VERSION.ToString(),
				"\nDiscarding data."
			}));
			reader.SkipBytes(num2);
			return;
		}
		if (num < 12)
		{
			this.entries = StateMachineSerializer.OldEntryV11.DeserializeOldEntries(reader, num);
			return;
		}
		int num3 = reader.ReadInt32();
		this.entries = new List<StateMachineSerializer.Entry>(num3);
		for (int i = 0; i < num3; i++)
		{
			StateMachineSerializer.Entry entry = StateMachineSerializer.Entry.Deserialize(reader, num);
			if (entry != null)
			{
				this.entries.Add(entry);
			}
		}
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x0006F214 File Offset: 0x0006D414
	private static string TrimAssemblyInfo(string type_name)
	{
		int num = type_name.IndexOf("[[");
		if (num != -1)
		{
			return type_name.Substring(0, num);
		}
		return type_name;
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x0006F23C File Offset: 0x0006D43C
	public bool Restore(StateMachine.Instance instance)
	{
		Type type = instance.GetType();
		for (int i = 0; i < this.entries.Count; i++)
		{
			StateMachineSerializer.Entry entry = this.entries[i];
			if (entry.type == type && instance.serializationSuffix == entry.typeSuffix)
			{
				this.entries.RemoveAt(i);
				return entry.Restore(instance);
			}
		}
		return false;
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x0006F2A9 File Offset: 0x0006D4A9
	private static bool DoesVersionHaveTypeSuffix(int version)
	{
		return version >= 20 || version == 11;
	}

	// Token: 0x04000BED RID: 3053
	public const int SERIALIZER_PRE_DLC1 = 10;

	// Token: 0x04000BEE RID: 3054
	public const int SERIALIZER_TYPE_SUFFIX = 11;

	// Token: 0x04000BEF RID: 3055
	public const int SERIALIZER_OPTIMIZE_BUFFERS = 12;

	// Token: 0x04000BF0 RID: 3056
	public const int SERIALIZER_EXPANSION1 = 20;

	// Token: 0x04000BF1 RID: 3057
	private static int SERIALIZER_VERSION = 20;

	// Token: 0x04000BF2 RID: 3058
	private const string TargetParameterName = "TargetParameter";

	// Token: 0x04000BF3 RID: 3059
	private List<StateMachineSerializer.Entry> entries = new List<StateMachineSerializer.Entry>();

	// Token: 0x02001034 RID: 4148
	private class Entry
	{
		// Token: 0x0600727F RID: 29311 RVA: 0x002AD958 File Offset: 0x002ABB58
		public static bool TrySerialize(StateMachine.Instance smi, BinaryWriter writer)
		{
			if (!smi.IsRunning())
			{
				return false;
			}
			int num = (int)writer.BaseStream.Position;
			writer.Write(0);
			writer.WriteKleiString(smi.GetType().FullName);
			writer.WriteKleiString(smi.serializationSuffix);
			writer.WriteKleiString(smi.GetCurrentState().name);
			int num2 = (int)writer.BaseStream.Position;
			writer.Write(0);
			int num3 = (int)writer.BaseStream.Position;
			Serializer.SerializeTypeless(smi, writer);
			if (smi.GetStateMachine().serializable == StateMachine.SerializeType.ParamsOnly || smi.GetStateMachine().serializable == StateMachine.SerializeType.Both_DEPRECATED)
			{
				StateMachine.Parameter.Context[] parameterContexts = smi.GetParameterContexts();
				writer.Write(parameterContexts.Length);
				foreach (StateMachine.Parameter.Context context in parameterContexts)
				{
					long num4 = (long)((int)writer.BaseStream.Position);
					writer.Write(0);
					long num5 = (long)((int)writer.BaseStream.Position);
					writer.WriteKleiString(context.GetType().FullName);
					writer.WriteKleiString(context.parameter.name);
					context.Serialize(writer);
					long num6 = (long)((int)writer.BaseStream.Position);
					writer.BaseStream.Position = num4;
					long num7 = num6 - num5;
					writer.Write((int)num7);
					writer.BaseStream.Position = num6;
				}
			}
			int num8 = (int)writer.BaseStream.Position - num3;
			if (num8 > 0)
			{
				int num9 = (int)writer.BaseStream.Position;
				writer.BaseStream.Position = (long)num2;
				writer.Write(num8);
				writer.BaseStream.Position = (long)num9;
				return true;
			}
			writer.BaseStream.Position = (long)num;
			writer.BaseStream.SetLength((long)num);
			return false;
		}

		// Token: 0x06007280 RID: 29312 RVA: 0x002ADB18 File Offset: 0x002ABD18
		public static StateMachineSerializer.Entry Deserialize(IReader reader, int serializerVersion)
		{
			StateMachineSerializer.Entry entry = new StateMachineSerializer.Entry();
			reader.ReadInt32();
			entry.version = serializerVersion;
			string text = reader.ReadKleiString();
			entry.type = Type.GetType(text);
			entry.typeSuffix = (StateMachineSerializer.DoesVersionHaveTypeSuffix(serializerVersion) ? reader.ReadKleiString() : null);
			entry.currentState = reader.ReadKleiString();
			int num = reader.ReadInt32();
			entry.entryData = new FastReader(reader.ReadBytes(num));
			if (entry.type == null)
			{
				return null;
			}
			return entry;
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x002ADB9C File Offset: 0x002ABD9C
		public bool Restore(StateMachine.Instance smi)
		{
			if (Manager.HasDeserializationMapping(smi.GetType()))
			{
				Deserializer.DeserializeTypeless(smi, this.entryData);
			}
			StateMachine.SerializeType serializable = smi.GetStateMachine().serializable;
			if (serializable == StateMachine.SerializeType.Never)
			{
				return false;
			}
			if ((serializable == StateMachine.SerializeType.Both_DEPRECATED || serializable == StateMachine.SerializeType.ParamsOnly) && !this.entryData.IsFinished)
			{
				StateMachine.Parameter.Context[] parameterContexts = smi.GetParameterContexts();
				int num = this.entryData.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					int num2 = this.entryData.ReadInt32();
					int position = this.entryData.Position;
					string text = this.entryData.ReadKleiString();
					text = text.Replace("Version=2.0.0.0", "Version=4.0.0.0");
					string text2 = this.entryData.ReadKleiString();
					foreach (StateMachine.Parameter.Context context in parameterContexts)
					{
						if (context.parameter.name == text2 && (this.version > 10 || !(context.parameter.GetType().Name == "TargetParameter")) && context.GetType().FullName == text)
						{
							context.Deserialize(this.entryData, smi);
							break;
						}
					}
					this.entryData.SkipBytes(num2 - (this.entryData.Position - position));
				}
			}
			if (serializable == StateMachine.SerializeType.Both_DEPRECATED || serializable == StateMachine.SerializeType.CurrentStateOnly_DEPRECATED)
			{
				StateMachine.BaseState state = smi.GetStateMachine().GetState(this.currentState);
				if (state != null)
				{
					smi.GoTo(state);
					return true;
				}
			}
			return false;
		}

		// Token: 0x040056A2 RID: 22178
		public int version;

		// Token: 0x040056A3 RID: 22179
		public Type type;

		// Token: 0x040056A4 RID: 22180
		public string typeSuffix;

		// Token: 0x040056A5 RID: 22181
		public string currentState;

		// Token: 0x040056A6 RID: 22182
		public FastReader entryData;
	}

	// Token: 0x02001035 RID: 4149
	private class OldEntryV11
	{
		// Token: 0x06007283 RID: 29315 RVA: 0x002ADD27 File Offset: 0x002ABF27
		public OldEntryV11(int version, int dataPos, Type type, string typeSuffix, string currentState)
		{
			this.version = version;
			this.dataPos = dataPos;
			this.type = type;
			this.typeSuffix = typeSuffix;
			this.currentState = currentState;
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x002ADD54 File Offset: 0x002ABF54
		public static List<StateMachineSerializer.Entry> DeserializeOldEntries(IReader reader, int serializerVersion)
		{
			Debug.Assert(serializerVersion < 12);
			List<StateMachineSerializer.OldEntryV11> list = StateMachineSerializer.OldEntryV11.ReadEntries(reader, serializerVersion);
			byte[] array = StateMachineSerializer.OldEntryV11.ReadEntryData(reader);
			List<StateMachineSerializer.Entry> list2 = new List<StateMachineSerializer.Entry>(list.Count);
			foreach (StateMachineSerializer.OldEntryV11 oldEntryV in list)
			{
				StateMachineSerializer.Entry entry = new StateMachineSerializer.Entry();
				entry.version = serializerVersion;
				entry.type = oldEntryV.type;
				entry.typeSuffix = oldEntryV.typeSuffix;
				entry.currentState = oldEntryV.currentState;
				entry.entryData = new FastReader(array);
				entry.entryData.SkipBytes(oldEntryV.dataPos);
				list2.Add(entry);
			}
			return list2;
		}

		// Token: 0x06007285 RID: 29317 RVA: 0x002ADE1C File Offset: 0x002AC01C
		private static StateMachineSerializer.OldEntryV11 Deserialize(IReader reader, int serializerVersion)
		{
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			string text = reader.ReadKleiString();
			string text2 = (StateMachineSerializer.DoesVersionHaveTypeSuffix(serializerVersion) ? reader.ReadKleiString() : null);
			string text3 = reader.ReadKleiString();
			Type type = Type.GetType(text);
			if (type == null)
			{
				return null;
			}
			return new StateMachineSerializer.OldEntryV11(num, num2, type, text2, text3);
		}

		// Token: 0x06007286 RID: 29318 RVA: 0x002ADE74 File Offset: 0x002AC074
		private static List<StateMachineSerializer.OldEntryV11> ReadEntries(IReader reader, int serializerVersion)
		{
			List<StateMachineSerializer.OldEntryV11> list = new List<StateMachineSerializer.OldEntryV11>();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				StateMachineSerializer.OldEntryV11 oldEntryV = StateMachineSerializer.OldEntryV11.Deserialize(reader, serializerVersion);
				if (oldEntryV != null)
				{
					list.Add(oldEntryV);
				}
			}
			return list;
		}

		// Token: 0x06007287 RID: 29319 RVA: 0x002ADEB0 File Offset: 0x002AC0B0
		private static byte[] ReadEntryData(IReader reader)
		{
			int num = reader.ReadInt32();
			return reader.ReadBytes(num);
		}

		// Token: 0x040056A7 RID: 22183
		public int version;

		// Token: 0x040056A8 RID: 22184
		public int dataPos;

		// Token: 0x040056A9 RID: 22185
		public Type type;

		// Token: 0x040056AA RID: 22186
		public string typeSuffix;

		// Token: 0x040056AB RID: 22187
		public string currentState;
	}
}
