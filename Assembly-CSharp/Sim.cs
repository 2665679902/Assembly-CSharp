using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000A24 RID: 2596
public static class Sim
{
	// Token: 0x06004E95 RID: 20117 RVA: 0x001BEC0C File Offset: 0x001BCE0C
	public static bool IsRadiationEnabled()
	{
		return DlcManager.FeatureRadiationEnabled();
	}

	// Token: 0x06004E96 RID: 20118 RVA: 0x001BEC13 File Offset: 0x001BCE13
	public static bool IsValidHandle(int h)
	{
		return h != -1 && h != -2;
	}

	// Token: 0x06004E97 RID: 20119 RVA: 0x001BEC23 File Offset: 0x001BCE23
	public static int GetHandleIndex(int h)
	{
		return h & 16777215;
	}

	// Token: 0x06004E98 RID: 20120
	[DllImport("SimDLL")]
	public static extern void SIM_Initialize(Sim.GAME_MessageHandler callback);

	// Token: 0x06004E99 RID: 20121
	[DllImport("SimDLL")]
	public static extern void SIM_Shutdown();

	// Token: 0x06004E9A RID: 20122
	[DllImport("SimDLL")]
	public unsafe static extern IntPtr SIM_HandleMessage(int sim_msg_id, int msg_length, byte* msg);

	// Token: 0x06004E9B RID: 20123
	[DllImport("SimDLL")]
	private unsafe static extern byte* SIM_BeginSave(int* size, int x, int y);

	// Token: 0x06004E9C RID: 20124
	[DllImport("SimDLL")]
	private static extern void SIM_EndSave();

	// Token: 0x06004E9D RID: 20125
	[DllImport("SimDLL")]
	public static extern void SIM_DebugCrash();

	// Token: 0x06004E9E RID: 20126 RVA: 0x001BEC2C File Offset: 0x001BCE2C
	public unsafe static IntPtr HandleMessage(SimMessageHashes sim_msg_id, int msg_length, byte[] msg)
	{
		IntPtr intPtr;
		fixed (byte[] array = msg)
		{
			byte* ptr;
			if (msg == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			intPtr = Sim.SIM_HandleMessage((int)sim_msg_id, msg_length, ptr);
		}
		return intPtr;
	}

	// Token: 0x06004E9F RID: 20127 RVA: 0x001BEC5C File Offset: 0x001BCE5C
	public unsafe static void Save(BinaryWriter writer, int x, int y)
	{
		int num;
		void* ptr = (void*)Sim.SIM_BeginSave(&num, x, y);
		byte[] array = new byte[num];
		Marshal.Copy((IntPtr)ptr, array, 0, num);
		Sim.SIM_EndSave();
		writer.Write(num);
		writer.Write(array);
	}

	// Token: 0x06004EA0 RID: 20128 RVA: 0x001BEC9C File Offset: 0x001BCE9C
	public unsafe static int LoadWorld(IReader reader)
	{
		int num = reader.ReadInt32();
		byte[] array;
		byte* ptr;
		if ((array = reader.ReadBytes(num)) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		IntPtr intPtr = Sim.SIM_HandleMessage(-672538170, num, ptr);
		array = null;
		if (intPtr == IntPtr.Zero)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06004EA1 RID: 20129 RVA: 0x001BECEC File Offset: 0x001BCEEC
	public static void AllocateCells(int width, int height, bool headless = false)
	{
		using (MemoryStream memoryStream = new MemoryStream(8))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				binaryWriter.Write(width);
				binaryWriter.Write(height);
				bool flag = Sim.IsRadiationEnabled();
				binaryWriter.Write(flag);
				binaryWriter.Write(headless);
				binaryWriter.Flush();
				Sim.HandleMessage(SimMessageHashes.AllocateCells, (int)memoryStream.Length, memoryStream.GetBuffer());
			}
		}
	}

	// Token: 0x06004EA2 RID: 20130 RVA: 0x001BED7C File Offset: 0x001BCF7C
	public unsafe static int Load(IReader reader)
	{
		int num = reader.ReadInt32();
		byte[] array;
		byte* ptr;
		if ((array = reader.ReadBytes(num)) == null || array.Length == 0)
		{
			ptr = null;
		}
		else
		{
			ptr = &array[0];
		}
		IntPtr intPtr = Sim.SIM_HandleMessage(-672538170, num, ptr);
		array = null;
		if (intPtr == IntPtr.Zero)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06004EA3 RID: 20131 RVA: 0x001BEDCC File Offset: 0x001BCFCC
	public unsafe static void Start()
	{
		Sim.GameDataUpdate* ptr = (Sim.GameDataUpdate*)(void*)Sim.SIM_HandleMessage(-931446686, 0, null);
		Grid.elementIdx = ptr->elementIdx;
		Grid.temperature = ptr->temperature;
		Grid.radiation = ptr->radiation;
		Grid.mass = ptr->mass;
		Grid.properties = ptr->properties;
		Grid.strengthInfo = ptr->strengthInfo;
		Grid.insulation = ptr->insulation;
		Grid.diseaseIdx = ptr->diseaseIdx;
		Grid.diseaseCount = ptr->diseaseCount;
		Grid.AccumulatedFlowValues = ptr->accumulatedFlow;
		PropertyTextures.externalFlowTex = ptr->propertyTextureFlow;
		PropertyTextures.externalLiquidTex = ptr->propertyTextureLiquid;
		PropertyTextures.externalExposedToSunlight = ptr->propertyTextureExposedToSunlight;
		Grid.InitializeCells();
	}

	// Token: 0x06004EA4 RID: 20132 RVA: 0x001BEE80 File Offset: 0x001BD080
	public static void Shutdown()
	{
		Sim.SIM_Shutdown();
		Grid.mass = null;
	}

	// Token: 0x06004EA5 RID: 20133
	[DllImport("SimDLL")]
	public unsafe static extern char* SYSINFO_Acquire();

	// Token: 0x06004EA6 RID: 20134
	[DllImport("SimDLL")]
	public static extern void SYSINFO_Release();

	// Token: 0x06004EA7 RID: 20135 RVA: 0x001BEE90 File Offset: 0x001BD090
	public unsafe static int DLL_MessageHandler(int message_id, IntPtr data)
	{
		if (message_id == 0)
		{
			Sim.DLLExceptionHandlerMessage* ptr = (Sim.DLLExceptionHandlerMessage*)(void*)data;
			string text = Marshal.PtrToStringAnsi(ptr->callstack);
			string text2 = Marshal.PtrToStringAnsi(ptr->dmpFilename);
			KCrashReporter.ReportSimDLLCrash("SimDLL Crash Dump", text, text2);
			return 0;
		}
		if (message_id == 1)
		{
			Sim.DLLReportMessageMessage* ptr2 = (Sim.DLLReportMessageMessage*)(void*)data;
			string text3 = "SimMessage: " + Marshal.PtrToStringAnsi(ptr2->message);
			string text4;
			if (ptr2->callstack != IntPtr.Zero)
			{
				text4 = Marshal.PtrToStringAnsi(ptr2->callstack);
			}
			else
			{
				string text5 = Marshal.PtrToStringAnsi(ptr2->file);
				int line = ptr2->line;
				text4 = text5 + ":" + line.ToString();
			}
			KCrashReporter.ReportSimDLLCrash(text3, text4, null);
			return 0;
		}
		return -1;
	}

	// Token: 0x04003400 RID: 13312
	public const int InvalidHandle = -1;

	// Token: 0x04003401 RID: 13313
	public const int QueuedRegisterHandle = -2;

	// Token: 0x04003402 RID: 13314
	public const byte InvalidDiseaseIdx = 255;

	// Token: 0x04003403 RID: 13315
	public const ushort InvalidElementIdx = 65535;

	// Token: 0x04003404 RID: 13316
	public const byte SpaceZoneID = 255;

	// Token: 0x04003405 RID: 13317
	public const byte SolidZoneID = 0;

	// Token: 0x04003406 RID: 13318
	public const int ChunkEdgeSize = 32;

	// Token: 0x04003407 RID: 13319
	public const float StateTransitionEnergy = 3f;

	// Token: 0x04003408 RID: 13320
	public const float ZeroDegreesCentigrade = 273.15f;

	// Token: 0x04003409 RID: 13321
	public const float StandardTemperature = 293.15f;

	// Token: 0x0400340A RID: 13322
	public const float StandardPressure = 101.3f;

	// Token: 0x0400340B RID: 13323
	public const float Epsilon = 0.0001f;

	// Token: 0x0400340C RID: 13324
	public const float MaxTemperature = 10000f;

	// Token: 0x0400340D RID: 13325
	public const float MinTemperature = 0f;

	// Token: 0x0400340E RID: 13326
	public const float MaxRadiation = 9000000f;

	// Token: 0x0400340F RID: 13327
	public const float MinRadiation = 0f;

	// Token: 0x04003410 RID: 13328
	public const float MaxMass = 10000f;

	// Token: 0x04003411 RID: 13329
	public const float MinMass = 1.0001f;

	// Token: 0x04003412 RID: 13330
	private const int PressureUpdateInterval = 1;

	// Token: 0x04003413 RID: 13331
	private const int TemperatureUpdateInterval = 1;

	// Token: 0x04003414 RID: 13332
	private const int LiquidUpdateInterval = 1;

	// Token: 0x04003415 RID: 13333
	private const int LifeUpdateInterval = 1;

	// Token: 0x04003416 RID: 13334
	public const byte ClearSkyGridValue = 253;

	// Token: 0x04003417 RID: 13335
	public const int PACKING_ALIGNMENT = 4;

	// Token: 0x02001862 RID: 6242
	// (Invoke) Token: 0x06008E3A RID: 36410
	public delegate int GAME_MessageHandler(int message_id, IntPtr data);

	// Token: 0x02001863 RID: 6243
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DLLExceptionHandlerMessage
	{
		// Token: 0x04007053 RID: 28755
		public IntPtr callstack;

		// Token: 0x04007054 RID: 28756
		public IntPtr dmpFilename;
	}

	// Token: 0x02001864 RID: 6244
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DLLReportMessageMessage
	{
		// Token: 0x04007055 RID: 28757
		public IntPtr callstack;

		// Token: 0x04007056 RID: 28758
		public IntPtr message;

		// Token: 0x04007057 RID: 28759
		public IntPtr file;

		// Token: 0x04007058 RID: 28760
		public int line;
	}

	// Token: 0x02001865 RID: 6245
	private enum GameHandledMessages
	{
		// Token: 0x0400705A RID: 28762
		ExceptionHandler,
		// Token: 0x0400705B RID: 28763
		ReportMessage
	}

	// Token: 0x02001866 RID: 6246
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PhysicsData
	{
		// Token: 0x06008E3D RID: 36413 RVA: 0x0030C453 File Offset: 0x0030A653
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.temperature);
			writer.Write(this.mass);
			writer.Write(this.pressure);
		}

		// Token: 0x0400705C RID: 28764
		public float temperature;

		// Token: 0x0400705D RID: 28765
		public float mass;

		// Token: 0x0400705E RID: 28766
		public float pressure;
	}

	// Token: 0x02001867 RID: 6247
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Cell
	{
		// Token: 0x06008E3E RID: 36414 RVA: 0x0030C47C File Offset: 0x0030A67C
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.elementIdx);
			writer.Write(0);
			writer.Write(this.insulation);
			writer.Write(0);
			writer.Write(this.pad0);
			writer.Write(this.pad1);
			writer.Write(this.pad2);
			writer.Write(this.temperature);
			writer.Write(this.mass);
		}

		// Token: 0x06008E3F RID: 36415 RVA: 0x0030C4ED File Offset: 0x0030A6ED
		public void SetValues(global::Element elem, List<global::Element> elements)
		{
			this.SetValues(elem, elem.defaultValues, elements);
		}

		// Token: 0x06008E40 RID: 36416 RVA: 0x0030C500 File Offset: 0x0030A700
		public void SetValues(global::Element elem, Sim.PhysicsData pd, List<global::Element> elements)
		{
			this.elementIdx = (ushort)elements.IndexOf(elem);
			this.temperature = pd.temperature;
			this.mass = pd.mass;
			this.insulation = byte.MaxValue;
			DebugUtil.Assert(this.temperature > 0f || this.mass == 0f, "A non-zero mass cannot have a <= 0 temperature");
		}

		// Token: 0x06008E41 RID: 36417 RVA: 0x0030C568 File Offset: 0x0030A768
		public void SetValues(ushort new_elem_idx, float new_temperature, float new_mass)
		{
			this.elementIdx = new_elem_idx;
			this.temperature = new_temperature;
			this.mass = new_mass;
			this.insulation = byte.MaxValue;
			DebugUtil.Assert(this.temperature > 0f || this.mass == 0f, "A non-zero mass cannot have a <= 0 temperature");
		}

		// Token: 0x0400705F RID: 28767
		public ushort elementIdx;

		// Token: 0x04007060 RID: 28768
		public byte properties;

		// Token: 0x04007061 RID: 28769
		public byte insulation;

		// Token: 0x04007062 RID: 28770
		public byte strengthInfo;

		// Token: 0x04007063 RID: 28771
		public byte pad0;

		// Token: 0x04007064 RID: 28772
		public byte pad1;

		// Token: 0x04007065 RID: 28773
		public byte pad2;

		// Token: 0x04007066 RID: 28774
		public float temperature;

		// Token: 0x04007067 RID: 28775
		public float mass;

		// Token: 0x020020F7 RID: 8439
		public enum Properties
		{
			// Token: 0x040092B4 RID: 37556
			GasImpermeable = 1,
			// Token: 0x040092B5 RID: 37557
			LiquidImpermeable,
			// Token: 0x040092B6 RID: 37558
			SolidImpermeable = 4,
			// Token: 0x040092B7 RID: 37559
			Unbreakable = 8,
			// Token: 0x040092B8 RID: 37560
			Transparent = 16,
			// Token: 0x040092B9 RID: 37561
			Opaque = 32,
			// Token: 0x040092BA RID: 37562
			NotifyOnMelt = 64,
			// Token: 0x040092BB RID: 37563
			ConstructedTile = 128
		}
	}

	// Token: 0x02001868 RID: 6248
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Element
	{
		// Token: 0x06008E42 RID: 36418 RVA: 0x0030C5BC File Offset: 0x0030A7BC
		public Element(global::Element e, List<global::Element> elements)
		{
			this.id = e.id;
			this.state = (byte)e.state;
			if (e.HasTag(GameTags.Unstable))
			{
				this.state |= 8;
			}
			int num = elements.FindIndex((global::Element ele) => ele.id == e.lowTempTransitionTarget);
			int num2 = elements.FindIndex((global::Element ele) => ele.id == e.highTempTransitionTarget);
			this.lowTempTransitionIdx = (ushort)((num >= 0) ? num : 65535);
			this.highTempTransitionIdx = (ushort)((num2 >= 0) ? num2 : 65535);
			this.elementsTableIdx = (ushort)elements.IndexOf(e);
			this.specificHeatCapacity = e.specificHeatCapacity;
			this.thermalConductivity = e.thermalConductivity;
			this.solidSurfaceAreaMultiplier = e.solidSurfaceAreaMultiplier;
			this.liquidSurfaceAreaMultiplier = e.liquidSurfaceAreaMultiplier;
			this.gasSurfaceAreaMultiplier = e.gasSurfaceAreaMultiplier;
			this.molarMass = e.molarMass;
			this.strength = e.strength;
			this.flow = e.flow;
			this.viscosity = e.viscosity;
			this.minHorizontalFlow = e.minHorizontalFlow;
			this.minVerticalFlow = e.minVerticalFlow;
			this.maxMass = e.maxMass;
			this.lowTemp = e.lowTemp;
			this.highTemp = e.highTemp;
			this.highTempTransitionOreID = e.highTempTransitionOreID;
			this.highTempTransitionOreMassConversion = e.highTempTransitionOreMassConversion;
			this.lowTempTransitionOreID = e.lowTempTransitionOreID;
			this.lowTempTransitionOreMassConversion = e.lowTempTransitionOreMassConversion;
			this.sublimateIndex = (ushort)elements.FindIndex((global::Element ele) => ele.id == e.sublimateId);
			this.convertIndex = (ushort)elements.FindIndex((global::Element ele) => ele.id == e.convertId);
			this.pack0 = 0;
			if (e.substance == null)
			{
				this.colour = 0U;
			}
			else
			{
				Color32 color = e.substance.colour;
				this.colour = (uint)(((int)color.a << 24) | ((int)color.b << 16) | ((int)color.g << 8) | (int)color.r);
			}
			this.sublimateFX = e.sublimateFX;
			this.sublimateRate = e.sublimateRate;
			this.sublimateEfficiency = e.sublimateEfficiency;
			this.sublimateProbability = e.sublimateProbability;
			this.offGasProbability = e.offGasPercentage;
			this.lightAbsorptionFactor = e.lightAbsorptionFactor;
			this.radiationAbsorptionFactor = e.radiationAbsorptionFactor;
			this.radiationPer1000Mass = e.radiationPer1000Mass;
			this.defaultValues = e.defaultValues;
		}

		// Token: 0x06008E43 RID: 36419 RVA: 0x0030C8CC File Offset: 0x0030AACC
		public void Write(BinaryWriter writer)
		{
			writer.Write((int)this.id);
			writer.Write(this.lowTempTransitionIdx);
			writer.Write(this.highTempTransitionIdx);
			writer.Write(this.elementsTableIdx);
			writer.Write(this.state);
			writer.Write(this.pack0);
			writer.Write(this.specificHeatCapacity);
			writer.Write(this.thermalConductivity);
			writer.Write(this.molarMass);
			writer.Write(this.solidSurfaceAreaMultiplier);
			writer.Write(this.liquidSurfaceAreaMultiplier);
			writer.Write(this.gasSurfaceAreaMultiplier);
			writer.Write(this.flow);
			writer.Write(this.viscosity);
			writer.Write(this.minHorizontalFlow);
			writer.Write(this.minVerticalFlow);
			writer.Write(this.maxMass);
			writer.Write(this.lowTemp);
			writer.Write(this.highTemp);
			writer.Write(this.strength);
			writer.Write((int)this.lowTempTransitionOreID);
			writer.Write(this.lowTempTransitionOreMassConversion);
			writer.Write((int)this.highTempTransitionOreID);
			writer.Write(this.highTempTransitionOreMassConversion);
			writer.Write(this.sublimateIndex);
			writer.Write(this.convertIndex);
			writer.Write(this.colour);
			writer.Write((int)this.sublimateFX);
			writer.Write(this.sublimateRate);
			writer.Write(this.sublimateEfficiency);
			writer.Write(this.sublimateProbability);
			writer.Write(this.offGasProbability);
			writer.Write(this.lightAbsorptionFactor);
			writer.Write(this.radiationAbsorptionFactor);
			writer.Write(this.radiationPer1000Mass);
			this.defaultValues.Write(writer);
		}

		// Token: 0x04007068 RID: 28776
		public SimHashes id;

		// Token: 0x04007069 RID: 28777
		public ushort lowTempTransitionIdx;

		// Token: 0x0400706A RID: 28778
		public ushort highTempTransitionIdx;

		// Token: 0x0400706B RID: 28779
		public ushort elementsTableIdx;

		// Token: 0x0400706C RID: 28780
		public byte state;

		// Token: 0x0400706D RID: 28781
		public byte pack0;

		// Token: 0x0400706E RID: 28782
		public float specificHeatCapacity;

		// Token: 0x0400706F RID: 28783
		public float thermalConductivity;

		// Token: 0x04007070 RID: 28784
		public float molarMass;

		// Token: 0x04007071 RID: 28785
		public float solidSurfaceAreaMultiplier;

		// Token: 0x04007072 RID: 28786
		public float liquidSurfaceAreaMultiplier;

		// Token: 0x04007073 RID: 28787
		public float gasSurfaceAreaMultiplier;

		// Token: 0x04007074 RID: 28788
		public float flow;

		// Token: 0x04007075 RID: 28789
		public float viscosity;

		// Token: 0x04007076 RID: 28790
		public float minHorizontalFlow;

		// Token: 0x04007077 RID: 28791
		public float minVerticalFlow;

		// Token: 0x04007078 RID: 28792
		public float maxMass;

		// Token: 0x04007079 RID: 28793
		public float lowTemp;

		// Token: 0x0400707A RID: 28794
		public float highTemp;

		// Token: 0x0400707B RID: 28795
		public float strength;

		// Token: 0x0400707C RID: 28796
		public SimHashes lowTempTransitionOreID;

		// Token: 0x0400707D RID: 28797
		public float lowTempTransitionOreMassConversion;

		// Token: 0x0400707E RID: 28798
		public SimHashes highTempTransitionOreID;

		// Token: 0x0400707F RID: 28799
		public float highTempTransitionOreMassConversion;

		// Token: 0x04007080 RID: 28800
		public ushort sublimateIndex;

		// Token: 0x04007081 RID: 28801
		public ushort convertIndex;

		// Token: 0x04007082 RID: 28802
		public uint colour;

		// Token: 0x04007083 RID: 28803
		public SpawnFXHashes sublimateFX;

		// Token: 0x04007084 RID: 28804
		public float sublimateRate;

		// Token: 0x04007085 RID: 28805
		public float sublimateEfficiency;

		// Token: 0x04007086 RID: 28806
		public float sublimateProbability;

		// Token: 0x04007087 RID: 28807
		public float offGasProbability;

		// Token: 0x04007088 RID: 28808
		public float lightAbsorptionFactor;

		// Token: 0x04007089 RID: 28809
		public float radiationAbsorptionFactor;

		// Token: 0x0400708A RID: 28810
		public float radiationPer1000Mass;

		// Token: 0x0400708B RID: 28811
		public Sim.PhysicsData defaultValues;
	}

	// Token: 0x02001869 RID: 6249
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseCell
	{
		// Token: 0x06008E44 RID: 36420 RVA: 0x0030CAA0 File Offset: 0x0030ACA0
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.diseaseIdx);
			writer.Write(this.reservedInfestationTickCount);
			writer.Write(this.pad1);
			writer.Write(this.pad2);
			writer.Write(this.elementCount);
			writer.Write(this.reservedAccumulatedError);
		}

		// Token: 0x0400708C RID: 28812
		public byte diseaseIdx;

		// Token: 0x0400708D RID: 28813
		private byte reservedInfestationTickCount;

		// Token: 0x0400708E RID: 28814
		private byte pad1;

		// Token: 0x0400708F RID: 28815
		private byte pad2;

		// Token: 0x04007090 RID: 28816
		public int elementCount;

		// Token: 0x04007091 RID: 28817
		private float reservedAccumulatedError;

		// Token: 0x04007092 RID: 28818
		public static readonly Sim.DiseaseCell Invalid = new Sim.DiseaseCell
		{
			diseaseIdx = byte.MaxValue,
			elementCount = 0
		};
	}

	// Token: 0x0200186A RID: 6250
	// (Invoke) Token: 0x06008E47 RID: 36423
	public delegate void GAME_Callback();

	// Token: 0x0200186B RID: 6251
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SolidInfo
	{
		// Token: 0x04007093 RID: 28819
		public int cellIdx;

		// Token: 0x04007094 RID: 28820
		public int isSolid;
	}

	// Token: 0x0200186C RID: 6252
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct LiquidChangeInfo
	{
		// Token: 0x04007095 RID: 28821
		public int cellIdx;
	}

	// Token: 0x0200186D RID: 6253
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SolidSubstanceChangeInfo
	{
		// Token: 0x04007096 RID: 28822
		public int cellIdx;
	}

	// Token: 0x0200186E RID: 6254
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SubstanceChangeInfo
	{
		// Token: 0x04007097 RID: 28823
		public int cellIdx;

		// Token: 0x04007098 RID: 28824
		public ushort oldElemIdx;

		// Token: 0x04007099 RID: 28825
		public ushort newElemIdx;
	}

	// Token: 0x0200186F RID: 6255
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CallbackInfo
	{
		// Token: 0x0400709A RID: 28826
		public int callbackIdx;
	}

	// Token: 0x02001870 RID: 6256
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameDataUpdate
	{
		// Token: 0x0400709B RID: 28827
		public int numFramesProcessed;

		// Token: 0x0400709C RID: 28828
		public unsafe ushort* elementIdx;

		// Token: 0x0400709D RID: 28829
		public unsafe float* temperature;

		// Token: 0x0400709E RID: 28830
		public unsafe float* mass;

		// Token: 0x0400709F RID: 28831
		public unsafe byte* properties;

		// Token: 0x040070A0 RID: 28832
		public unsafe byte* insulation;

		// Token: 0x040070A1 RID: 28833
		public unsafe byte* strengthInfo;

		// Token: 0x040070A2 RID: 28834
		public unsafe float* radiation;

		// Token: 0x040070A3 RID: 28835
		public unsafe byte* diseaseIdx;

		// Token: 0x040070A4 RID: 28836
		public unsafe int* diseaseCount;

		// Token: 0x040070A5 RID: 28837
		public int numSolidInfo;

		// Token: 0x040070A6 RID: 28838
		public unsafe Sim.SolidInfo* solidInfo;

		// Token: 0x040070A7 RID: 28839
		public int numLiquidChangeInfo;

		// Token: 0x040070A8 RID: 28840
		public unsafe Sim.LiquidChangeInfo* liquidChangeInfo;

		// Token: 0x040070A9 RID: 28841
		public int numSolidSubstanceChangeInfo;

		// Token: 0x040070AA RID: 28842
		public unsafe Sim.SolidSubstanceChangeInfo* solidSubstanceChangeInfo;

		// Token: 0x040070AB RID: 28843
		public int numSubstanceChangeInfo;

		// Token: 0x040070AC RID: 28844
		public unsafe Sim.SubstanceChangeInfo* substanceChangeInfo;

		// Token: 0x040070AD RID: 28845
		public int numCallbackInfo;

		// Token: 0x040070AE RID: 28846
		public unsafe Sim.CallbackInfo* callbackInfo;

		// Token: 0x040070AF RID: 28847
		public int numSpawnFallingLiquidInfo;

		// Token: 0x040070B0 RID: 28848
		public unsafe Sim.SpawnFallingLiquidInfo* spawnFallingLiquidInfo;

		// Token: 0x040070B1 RID: 28849
		public int numDigInfo;

		// Token: 0x040070B2 RID: 28850
		public unsafe Sim.SpawnOreInfo* digInfo;

		// Token: 0x040070B3 RID: 28851
		public int numSpawnOreInfo;

		// Token: 0x040070B4 RID: 28852
		public unsafe Sim.SpawnOreInfo* spawnOreInfo;

		// Token: 0x040070B5 RID: 28853
		public int numSpawnFXInfo;

		// Token: 0x040070B6 RID: 28854
		public unsafe Sim.SpawnFXInfo* spawnFXInfo;

		// Token: 0x040070B7 RID: 28855
		public int numUnstableCellInfo;

		// Token: 0x040070B8 RID: 28856
		public unsafe Sim.UnstableCellInfo* unstableCellInfo;

		// Token: 0x040070B9 RID: 28857
		public int numWorldDamageInfo;

		// Token: 0x040070BA RID: 28858
		public unsafe Sim.WorldDamageInfo* worldDamageInfo;

		// Token: 0x040070BB RID: 28859
		public int numBuildingTemperatures;

		// Token: 0x040070BC RID: 28860
		public unsafe Sim.BuildingTemperatureInfo* buildingTemperatures;

		// Token: 0x040070BD RID: 28861
		public int numMassConsumedCallbacks;

		// Token: 0x040070BE RID: 28862
		public unsafe Sim.MassConsumedCallback* massConsumedCallbacks;

		// Token: 0x040070BF RID: 28863
		public int numMassEmittedCallbacks;

		// Token: 0x040070C0 RID: 28864
		public unsafe Sim.MassEmittedCallback* massEmittedCallbacks;

		// Token: 0x040070C1 RID: 28865
		public int numDiseaseConsumptionCallbacks;

		// Token: 0x040070C2 RID: 28866
		public unsafe Sim.DiseaseConsumptionCallback* diseaseConsumptionCallbacks;

		// Token: 0x040070C3 RID: 28867
		public int numComponentStateChangedMessages;

		// Token: 0x040070C4 RID: 28868
		public unsafe Sim.ComponentStateChangedMessage* componentStateChangedMessages;

		// Token: 0x040070C5 RID: 28869
		public int numRemovedMassEntries;

		// Token: 0x040070C6 RID: 28870
		public unsafe Sim.ConsumedMassInfo* removedMassEntries;

		// Token: 0x040070C7 RID: 28871
		public int numEmittedMassEntries;

		// Token: 0x040070C8 RID: 28872
		public unsafe Sim.EmittedMassInfo* emittedMassEntries;

		// Token: 0x040070C9 RID: 28873
		public int numElementChunkInfos;

		// Token: 0x040070CA RID: 28874
		public unsafe Sim.ElementChunkInfo* elementChunkInfos;

		// Token: 0x040070CB RID: 28875
		public int numElementChunkMeltedInfos;

		// Token: 0x040070CC RID: 28876
		public unsafe Sim.MeltedInfo* elementChunkMeltedInfos;

		// Token: 0x040070CD RID: 28877
		public int numBuildingOverheatInfos;

		// Token: 0x040070CE RID: 28878
		public unsafe Sim.MeltedInfo* buildingOverheatInfos;

		// Token: 0x040070CF RID: 28879
		public int numBuildingNoLongerOverheatedInfos;

		// Token: 0x040070D0 RID: 28880
		public unsafe Sim.MeltedInfo* buildingNoLongerOverheatedInfos;

		// Token: 0x040070D1 RID: 28881
		public int numBuildingMeltedInfos;

		// Token: 0x040070D2 RID: 28882
		public unsafe Sim.MeltedInfo* buildingMeltedInfos;

		// Token: 0x040070D3 RID: 28883
		public int numCellMeltedInfos;

		// Token: 0x040070D4 RID: 28884
		public unsafe Sim.CellMeltedInfo* cellMeltedInfos;

		// Token: 0x040070D5 RID: 28885
		public int numDiseaseEmittedInfos;

		// Token: 0x040070D6 RID: 28886
		public unsafe Sim.DiseaseEmittedInfo* diseaseEmittedInfos;

		// Token: 0x040070D7 RID: 28887
		public int numDiseaseConsumedInfos;

		// Token: 0x040070D8 RID: 28888
		public unsafe Sim.DiseaseConsumedInfo* diseaseConsumedInfos;

		// Token: 0x040070D9 RID: 28889
		public int numRadiationConsumedCallbacks;

		// Token: 0x040070DA RID: 28890
		public unsafe Sim.ConsumedRadiationCallback* radiationConsumedCallbacks;

		// Token: 0x040070DB RID: 28891
		public unsafe float* accumulatedFlow;

		// Token: 0x040070DC RID: 28892
		public IntPtr propertyTextureFlow;

		// Token: 0x040070DD RID: 28893
		public IntPtr propertyTextureLiquid;

		// Token: 0x040070DE RID: 28894
		public IntPtr propertyTextureExposedToSunlight;
	}

	// Token: 0x02001871 RID: 6257
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SpawnFallingLiquidInfo
	{
		// Token: 0x040070DF RID: 28895
		public int cellIdx;

		// Token: 0x040070E0 RID: 28896
		public ushort elemIdx;

		// Token: 0x040070E1 RID: 28897
		public byte diseaseIdx;

		// Token: 0x040070E2 RID: 28898
		public byte pad0;

		// Token: 0x040070E3 RID: 28899
		public float mass;

		// Token: 0x040070E4 RID: 28900
		public float temperature;

		// Token: 0x040070E5 RID: 28901
		public int diseaseCount;
	}

	// Token: 0x02001872 RID: 6258
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SpawnOreInfo
	{
		// Token: 0x040070E6 RID: 28902
		public int cellIdx;

		// Token: 0x040070E7 RID: 28903
		public ushort elemIdx;

		// Token: 0x040070E8 RID: 28904
		public byte diseaseIdx;

		// Token: 0x040070E9 RID: 28905
		private byte pad0;

		// Token: 0x040070EA RID: 28906
		public float mass;

		// Token: 0x040070EB RID: 28907
		public float temperature;

		// Token: 0x040070EC RID: 28908
		public int diseaseCount;
	}

	// Token: 0x02001873 RID: 6259
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct SpawnFXInfo
	{
		// Token: 0x040070ED RID: 28909
		public int cellIdx;

		// Token: 0x040070EE RID: 28910
		public int fxHash;

		// Token: 0x040070EF RID: 28911
		public float rotation;
	}

	// Token: 0x02001874 RID: 6260
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct UnstableCellInfo
	{
		// Token: 0x040070F0 RID: 28912
		public int cellIdx;

		// Token: 0x040070F1 RID: 28913
		public ushort elemIdx;

		// Token: 0x040070F2 RID: 28914
		public byte fallingInfo;

		// Token: 0x040070F3 RID: 28915
		public byte diseaseIdx;

		// Token: 0x040070F4 RID: 28916
		public float mass;

		// Token: 0x040070F5 RID: 28917
		public float temperature;

		// Token: 0x040070F6 RID: 28918
		public int diseaseCount;

		// Token: 0x020020F9 RID: 8441
		public enum FallingInfo
		{
			// Token: 0x040092BE RID: 37566
			StartedFalling,
			// Token: 0x040092BF RID: 37567
			StoppedFalling
		}
	}

	// Token: 0x02001875 RID: 6261
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct NewGameFrame
	{
		// Token: 0x040070F7 RID: 28919
		public float elapsedSeconds;

		// Token: 0x040070F8 RID: 28920
		public int minX;

		// Token: 0x040070F9 RID: 28921
		public int minY;

		// Token: 0x040070FA RID: 28922
		public int maxX;

		// Token: 0x040070FB RID: 28923
		public int maxY;

		// Token: 0x040070FC RID: 28924
		public float currentSunlightIntensity;

		// Token: 0x040070FD RID: 28925
		public float currentCosmicRadiationIntensity;
	}

	// Token: 0x02001876 RID: 6262
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct WorldDamageInfo
	{
		// Token: 0x040070FE RID: 28926
		public int gameCell;

		// Token: 0x040070FF RID: 28927
		public int damageSourceOffset;
	}

	// Token: 0x02001877 RID: 6263
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PipeTemperatureChange
	{
		// Token: 0x04007100 RID: 28928
		public int cellIdx;

		// Token: 0x04007101 RID: 28929
		public float temperature;
	}

	// Token: 0x02001878 RID: 6264
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct MassConsumedCallback
	{
		// Token: 0x04007102 RID: 28930
		public int callbackIdx;

		// Token: 0x04007103 RID: 28931
		public ushort elemIdx;

		// Token: 0x04007104 RID: 28932
		public byte diseaseIdx;

		// Token: 0x04007105 RID: 28933
		private byte pad0;

		// Token: 0x04007106 RID: 28934
		public float mass;

		// Token: 0x04007107 RID: 28935
		public float temperature;

		// Token: 0x04007108 RID: 28936
		public int diseaseCount;
	}

	// Token: 0x02001879 RID: 6265
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct MassEmittedCallback
	{
		// Token: 0x04007109 RID: 28937
		public int callbackIdx;

		// Token: 0x0400710A RID: 28938
		public ushort elemIdx;

		// Token: 0x0400710B RID: 28939
		public byte suceeded;

		// Token: 0x0400710C RID: 28940
		public byte diseaseIdx;

		// Token: 0x0400710D RID: 28941
		public float mass;

		// Token: 0x0400710E RID: 28942
		public float temperature;

		// Token: 0x0400710F RID: 28943
		public int diseaseCount;
	}

	// Token: 0x0200187A RID: 6266
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseConsumptionCallback
	{
		// Token: 0x04007110 RID: 28944
		public int callbackIdx;

		// Token: 0x04007111 RID: 28945
		public byte diseaseIdx;

		// Token: 0x04007112 RID: 28946
		private byte pad0;

		// Token: 0x04007113 RID: 28947
		private byte pad1;

		// Token: 0x04007114 RID: 28948
		private byte pad2;

		// Token: 0x04007115 RID: 28949
		public int diseaseCount;
	}

	// Token: 0x0200187B RID: 6267
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ComponentStateChangedMessage
	{
		// Token: 0x04007116 RID: 28950
		public int callbackIdx;

		// Token: 0x04007117 RID: 28951
		public int simHandle;
	}

	// Token: 0x0200187C RID: 6268
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DebugProperties
	{
		// Token: 0x04007118 RID: 28952
		public float buildingTemperatureScale;

		// Token: 0x04007119 RID: 28953
		public float buildingToBuildingTemperatureScale;

		// Token: 0x0400711A RID: 28954
		public float biomeTemperatureLerpRate;

		// Token: 0x0400711B RID: 28955
		public byte isDebugEditing;

		// Token: 0x0400711C RID: 28956
		public byte pad0;

		// Token: 0x0400711D RID: 28957
		public byte pad1;

		// Token: 0x0400711E RID: 28958
		public byte pad2;
	}

	// Token: 0x0200187D RID: 6269
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct EmittedMassInfo
	{
		// Token: 0x0400711F RID: 28959
		public ushort elemIdx;

		// Token: 0x04007120 RID: 28960
		public byte diseaseIdx;

		// Token: 0x04007121 RID: 28961
		public byte pad0;

		// Token: 0x04007122 RID: 28962
		public float mass;

		// Token: 0x04007123 RID: 28963
		public float temperature;

		// Token: 0x04007124 RID: 28964
		public int diseaseCount;
	}

	// Token: 0x0200187E RID: 6270
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ConsumedMassInfo
	{
		// Token: 0x04007125 RID: 28965
		public int simHandle;

		// Token: 0x04007126 RID: 28966
		public ushort removedElemIdx;

		// Token: 0x04007127 RID: 28967
		public byte diseaseIdx;

		// Token: 0x04007128 RID: 28968
		private byte pad0;

		// Token: 0x04007129 RID: 28969
		public float mass;

		// Token: 0x0400712A RID: 28970
		public float temperature;

		// Token: 0x0400712B RID: 28971
		public int diseaseCount;
	}

	// Token: 0x0200187F RID: 6271
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ConsumedDiseaseInfo
	{
		// Token: 0x0400712C RID: 28972
		public int simHandle;

		// Token: 0x0400712D RID: 28973
		public byte diseaseIdx;

		// Token: 0x0400712E RID: 28974
		private byte pad0;

		// Token: 0x0400712F RID: 28975
		private byte pad1;

		// Token: 0x04007130 RID: 28976
		private byte pad2;

		// Token: 0x04007131 RID: 28977
		public int diseaseCount;
	}

	// Token: 0x02001880 RID: 6272
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ElementChunkInfo
	{
		// Token: 0x04007132 RID: 28978
		public float temperature;

		// Token: 0x04007133 RID: 28979
		public float deltaKJ;
	}

	// Token: 0x02001881 RID: 6273
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct MeltedInfo
	{
		// Token: 0x04007134 RID: 28980
		public int handle;
	}

	// Token: 0x02001882 RID: 6274
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CellMeltedInfo
	{
		// Token: 0x04007135 RID: 28981
		public int gameCell;
	}

	// Token: 0x02001883 RID: 6275
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct BuildingTemperatureInfo
	{
		// Token: 0x04007136 RID: 28982
		public int handle;

		// Token: 0x04007137 RID: 28983
		public float temperature;
	}

	// Token: 0x02001884 RID: 6276
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct BuildingConductivityData
	{
		// Token: 0x04007138 RID: 28984
		public float temperature;

		// Token: 0x04007139 RID: 28985
		public float heatCapacity;

		// Token: 0x0400713A RID: 28986
		public float thermalConductivity;
	}

	// Token: 0x02001885 RID: 6277
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseEmittedInfo
	{
		// Token: 0x0400713B RID: 28987
		public byte diseaseIdx;

		// Token: 0x0400713C RID: 28988
		private byte pad0;

		// Token: 0x0400713D RID: 28989
		private byte pad1;

		// Token: 0x0400713E RID: 28990
		private byte pad2;

		// Token: 0x0400713F RID: 28991
		public int count;
	}

	// Token: 0x02001886 RID: 6278
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct DiseaseConsumedInfo
	{
		// Token: 0x04007140 RID: 28992
		public byte diseaseIdx;

		// Token: 0x04007141 RID: 28993
		private byte pad0;

		// Token: 0x04007142 RID: 28994
		private byte pad1;

		// Token: 0x04007143 RID: 28995
		private byte pad2;

		// Token: 0x04007144 RID: 28996
		public int count;
	}

	// Token: 0x02001887 RID: 6279
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ConsumedRadiationCallback
	{
		// Token: 0x04007145 RID: 28997
		public int callbackIdx;

		// Token: 0x04007146 RID: 28998
		public int gameCell;

		// Token: 0x04007147 RID: 28999
		public float radiation;
	}
}
