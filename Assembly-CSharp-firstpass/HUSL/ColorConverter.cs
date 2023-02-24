using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace HUSL
{
	// Token: 0x0200051E RID: 1310
	public class ColorConverter
	{
		// Token: 0x060037DC RID: 14300 RVA: 0x0007E6D0 File Offset: 0x0007C8D0
		protected static IList<double[]> GetBounds(double L)
		{
			List<double[]> list = new List<double[]>();
			double num = Math.Pow(L + 16.0, 3.0) / 1560896.0;
			double num2 = ((num > ColorConverter.Epsilon) ? num : (L / ColorConverter.Kappa));
			for (int i = 0; i < 3; i++)
			{
				double num3 = ColorConverter.M[i][0];
				double num4 = ColorConverter.M[i][1];
				double num5 = ColorConverter.M[i][2];
				for (int j = 0; j < 2; j++)
				{
					double num6 = (284517.0 * num3 - 94839.0 * num5) * num2;
					double num7 = (838422.0 * num5 + 769860.0 * num4 + 731718.0 * num3) * L * num2 - (double)(769860 * j) * L;
					double num8 = (632260.0 * num5 - 126452.0 * num4) * num2 + (double)(126452 * j);
					list.Add(new double[]
					{
						num6 / num8,
						num7 / num8
					});
				}
			}
			return list;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0007E7FC File Offset: 0x0007C9FC
		protected static double IntersectLineLine(IList<double> lineA, IList<double> lineB)
		{
			return (lineA[1] - lineB[1]) / (lineB[0] - lineA[0]);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x0007E81D File Offset: 0x0007CA1D
		protected static double DistanceFromPole(IList<double> point)
		{
			return Math.Sqrt(Math.Pow(point[0], 2.0) + Math.Pow(point[1], 2.0));
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0007E84F File Offset: 0x0007CA4F
		protected static bool LengthOfRayUntilIntersect(double theta, IList<double> line, out double length)
		{
			length = line[1] / (Math.Sin(theta) - line[0] * Math.Cos(theta));
			return length >= 0.0;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x0007E880 File Offset: 0x0007CA80
		protected static double MaxSafeChromaForL(double L)
		{
			IList<double[]> bounds = ColorConverter.GetBounds(L);
			double num = double.MaxValue;
			for (int i = 0; i < 2; i++)
			{
				double num2 = bounds[i][0];
				double num3 = bounds[i][1];
				IList<double> list = new double[] { num2, num3 };
				double[] array = new double[2];
				array[0] = -1.0 / num2;
				double num4 = ColorConverter.IntersectLineLine(list, array);
				double num5 = ColorConverter.DistanceFromPole(new double[]
				{
					num4,
					num3 + num4 * num2
				});
				num = Math.Min(num, num5);
			}
			return num;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x0007E910 File Offset: 0x0007CB10
		protected static double MaxChromaForLH(double L, double H)
		{
			double num = H / 360.0 * 3.141592653589793 * 2.0;
			IEnumerable<double[]> bounds = ColorConverter.GetBounds(L);
			double num2 = double.MaxValue;
			foreach (double[] array in bounds)
			{
				double num3;
				if (ColorConverter.LengthOfRayUntilIntersect(num, array, out num3))
				{
					num2 = Math.Min(num2, num3);
				}
			}
			return num2;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x0007E998 File Offset: 0x0007CB98
		protected static double DotProduct(IList<double> a, IList<double> b)
		{
			double num = 0.0;
			for (int i = 0; i < a.Count; i++)
			{
				num += a[i] * b[i];
			}
			return num;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0007E9D4 File Offset: 0x0007CBD4
		protected static double Round(double value, int places)
		{
			double num = Math.Pow(10.0, (double)places);
			return Math.Round(value * num) / num;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0007E9FC File Offset: 0x0007CBFC
		protected static double FromLinear(double c)
		{
			if (c <= 0.0031308)
			{
				return 12.92 * c;
			}
			return 1.055 * Math.Pow(c, 0.4166666666666667) - 0.055;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x0007EA39 File Offset: 0x0007CC39
		protected static double ToLinear(double c)
		{
			if (c > 0.04045)
			{
				return Math.Pow((c + 0.055) / 1.055, 2.4);
			}
			return c / 12.92;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x0007EA78 File Offset: 0x0007CC78
		protected static IList<int> RGBPrepare(IList<double> tuple)
		{
			for (int i = 0; i < tuple.Count; i++)
			{
				tuple[i] = ColorConverter.Round(tuple[i], 3);
			}
			for (int j = 0; j < tuple.Count; j++)
			{
				double num = tuple[j];
				if (num < -0.0001 || num > 1.0001)
				{
					throw new Exception("Illegal rgb value: " + num.ToString());
				}
			}
			int[] array = new int[tuple.Count];
			for (int k = 0; k < tuple.Count; k++)
			{
				array[k] = (int)Math.Round(tuple[k] * 255.0);
			}
			return array;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0007EB30 File Offset: 0x0007CD30
		protected static double YToL(double Y)
		{
			if (Y <= ColorConverter.Epsilon)
			{
				return Y / ColorConverter.RefY * ColorConverter.Kappa;
			}
			return 116.0 * Math.Pow(Y / ColorConverter.RefY, 0.3333333333333333) - 16.0;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0007EB7C File Offset: 0x0007CD7C
		protected static double LToY(double L)
		{
			if (L <= 8.0)
			{
				return ColorConverter.RefY * L / ColorConverter.Kappa;
			}
			return ColorConverter.RefY * Math.Pow((L + 16.0) / 116.0, 3.0);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x0007EBCC File Offset: 0x0007CDCC
		public static IList<double> XYZToRGB(IList<double> tuple)
		{
			return new double[]
			{
				ColorConverter.FromLinear(ColorConverter.DotProduct(ColorConverter.M[0], tuple)),
				ColorConverter.FromLinear(ColorConverter.DotProduct(ColorConverter.M[1], tuple)),
				ColorConverter.FromLinear(ColorConverter.DotProduct(ColorConverter.M[2], tuple))
			};
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0007EC20 File Offset: 0x0007CE20
		public static IList<double> RGBToXYZ(IList<double> tuple)
		{
			double[] array = new double[]
			{
				ColorConverter.ToLinear(tuple[0]),
				ColorConverter.ToLinear(tuple[1]),
				ColorConverter.ToLinear(tuple[2])
			};
			return new double[]
			{
				ColorConverter.DotProduct(ColorConverter.MInv[0], array),
				ColorConverter.DotProduct(ColorConverter.MInv[1], array),
				ColorConverter.DotProduct(ColorConverter.MInv[2], array)
			};
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0007EC98 File Offset: 0x0007CE98
		public static IList<double> XYZToLUV(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			double num4 = 4.0 * num / (num + 15.0 * num2 + 3.0 * num3);
			double num5 = 9.0 * num2 / (num + 15.0 * num2 + 3.0 * num3);
			double num6 = ColorConverter.YToL(num2);
			if (num6 == 0.0)
			{
				return new double[3];
			}
			double num7 = 13.0 * num6 * (num4 - ColorConverter.RefU);
			double num8 = 13.0 * num6 * (num5 - ColorConverter.RefV);
			return new double[] { num6, num7, num8 };
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x0007ED68 File Offset: 0x0007CF68
		public static IList<double> LUVToXYZ(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			if (num == 0.0)
			{
				return new double[3];
			}
			double num4 = num2 / (13.0 * num) + ColorConverter.RefU;
			double num5 = num3 / (13.0 * num) + ColorConverter.RefV;
			double num6 = ColorConverter.LToY(num);
			double num7 = 0.0 - 9.0 * num6 * num4 / ((num4 - 4.0) * num5 - num4 * num5);
			double num8 = (9.0 * num6 - 15.0 * num5 * num6 - num5 * num7) / (3.0 * num5);
			return new double[] { num7, num6, num8 };
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0007EE48 File Offset: 0x0007D048
		public static IList<double> LUVToLCH(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			double num4 = Math.Pow(Math.Pow(num2, 2.0) + Math.Pow(num3, 2.0), 0.5);
			double num5 = Math.Atan2(num3, num2) * 180.0 / 3.141592653589793;
			if (num5 < 0.0)
			{
				num5 = 360.0 + num5;
			}
			return new double[] { num, num4, num5 };
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0007EEE8 File Offset: 0x0007D0E8
		public static IList<double> LCHToLUV(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2] / 360.0 * 2.0 * 3.141592653589793;
			double num4 = Math.Cos(num3) * num2;
			double num5 = Math.Sin(num3) * num2;
			return new double[] { num, num4, num5 };
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x0007EF50 File Offset: 0x0007D150
		public static IList<double> HUSLToLCH(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			if (num3 > 99.9999999)
			{
				return new double[] { 100.0, 0.0, num };
			}
			if (num3 < 1E-08)
			{
				return new double[] { 0.0, 0.0, num };
			}
			double num4 = ColorConverter.MaxChromaForLH(num3, num) / 100.0 * num2;
			return new double[] { num3, num4, num };
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x0007EFD8 File Offset: 0x0007D1D8
		public static IList<double> LCHToHUSL(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			if (num > 99.9999999)
			{
				return new double[] { num3, 0.0, 100.0 };
			}
			if (num < 1E-08)
			{
				double[] array = new double[3];
				array[0] = num3;
				return array;
			}
			double num4 = ColorConverter.MaxChromaForLH(num, num3);
			double num5 = num2 / num4 * 100.0;
			return new double[] { num3, num5, num };
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x0007F064 File Offset: 0x0007D264
		public static IList<double> HUSLPToLCH(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			if (num3 > 99.9999999)
			{
				return new double[] { 100.0, 0.0, num };
			}
			if (num3 < 1E-08)
			{
				return new double[] { 0.0, 0.0, num };
			}
			double num4 = ColorConverter.MaxSafeChromaForL(num3) / 100.0 * num2;
			return new double[] { num3, num4, num };
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x0007F0E8 File Offset: 0x0007D2E8
		public static IList<double> LCHToHUSLP(IList<double> tuple)
		{
			double num = tuple[0];
			double num2 = tuple[1];
			double num3 = tuple[2];
			if (num > 99.9999999)
			{
				return new double[] { num3, 0.0, 100.0 };
			}
			if (num < 1E-08)
			{
				double[] array = new double[3];
				array[0] = num3;
				return array;
			}
			double num4 = ColorConverter.MaxSafeChromaForL(num);
			double num5 = num2 / num4 * 100.0;
			return new double[] { num3, num5, num };
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0007F170 File Offset: 0x0007D370
		public static string RGBToHex(IList<double> tuple)
		{
			IList<int> list = ColorConverter.RGBPrepare(tuple);
			return string.Format("#{0}{1}{2}", list[0].ToString("x2"), list[1].ToString("x2"), list[2].ToString("x2"));
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x0007F1CC File Offset: 0x0007D3CC
		public static IList<double> HexToRGB(string hex)
		{
			return new double[]
			{
				(double)int.Parse(hex.Substring(1, 2), NumberStyles.HexNumber) / 255.0,
				(double)int.Parse(hex.Substring(3, 2), NumberStyles.HexNumber) / 255.0,
				(double)int.Parse(hex.Substring(5, 2), NumberStyles.HexNumber) / 255.0
			};
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x0007F23F File Offset: 0x0007D43F
		public static IList<double> LCHToRGB(IList<double> tuple)
		{
			return ColorConverter.XYZToRGB(ColorConverter.LUVToXYZ(ColorConverter.LCHToLUV(tuple)));
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x0007F251 File Offset: 0x0007D451
		public static IList<double> RGBToLCH(IList<double> tuple)
		{
			return ColorConverter.LUVToLCH(ColorConverter.XYZToLUV(ColorConverter.RGBToXYZ(tuple)));
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x0007F263 File Offset: 0x0007D463
		public static IList<double> HUSLToRGB(IList<double> tuple)
		{
			return ColorConverter.LCHToRGB(ColorConverter.HUSLToLCH(tuple));
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0007F270 File Offset: 0x0007D470
		public static IList<double> RGBToHUSL(IList<double> tuple)
		{
			return ColorConverter.LCHToHUSL(ColorConverter.RGBToLCH(tuple));
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x0007F27D File Offset: 0x0007D47D
		public static IList<double> HUSLPToRGB(IList<double> tuple)
		{
			return ColorConverter.LCHToRGB(ColorConverter.HUSLPToLCH(tuple));
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x0007F28A File Offset: 0x0007D48A
		public static IList<double> RGBToHUSLP(IList<double> tuple)
		{
			return ColorConverter.LCHToHUSLP(ColorConverter.RGBToLCH(tuple));
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x0007F297 File Offset: 0x0007D497
		public static string HUSLToHex(IList<double> tuple)
		{
			return ColorConverter.RGBToHex(ColorConverter.HUSLToRGB(tuple));
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x0007F2A4 File Offset: 0x0007D4A4
		public static string HUSLPToHex(IList<double> tuple)
		{
			return ColorConverter.RGBToHex(ColorConverter.HUSLPToRGB(tuple));
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x0007F2B1 File Offset: 0x0007D4B1
		public static IList<double> HexToHUSL(string s)
		{
			return ColorConverter.RGBToHUSL(ColorConverter.HexToRGB(s));
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0007F2BE File Offset: 0x0007D4BE
		public static IList<double> HexToHUSLP(string s)
		{
			return ColorConverter.RGBToHUSLP(ColorConverter.HexToRGB(s));
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x0007F2CC File Offset: 0x0007D4CC
		public static Color HUSLToColor(float h, float s, float l)
		{
			IList<double> list = ColorConverter.HUSLToRGB(new List<double>(new double[]
			{
				(double)h,
				(double)s,
				(double)l
			}));
			return new Color((float)list[0], (float)list[1], (float)list[2]);
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x0007F318 File Offset: 0x0007D518
		public static Color HUSLPToColor(float h, float s, float l)
		{
			IList<double> list = ColorConverter.HUSLPToRGB(new List<double>(new double[]
			{
				(double)h,
				(double)s,
				(double)l
			}));
			return new Color((float)list[0], (float)list[1], (float)list[2]);
		}

		// Token: 0x04001433 RID: 5171
		protected static double[][] M = new double[][]
		{
			new double[] { 3.240969941904521, -1.537383177570093, -0.498610760293 },
			new double[] { -0.96924363628087, 1.87596750150772, 0.041555057407175 },
			new double[] { 0.055630079696993, -0.20397695888897, 1.056971514242878 }
		};

		// Token: 0x04001434 RID: 5172
		protected static double[][] MInv = new double[][]
		{
			new double[] { 0.41239079926595, 0.35758433938387, 0.18048078840183 },
			new double[] { 0.21263900587151, 0.71516867876775, 0.072192315360733 },
			new double[] { 0.019330818715591, 0.11919477979462, 0.95053215224966 }
		};

		// Token: 0x04001435 RID: 5173
		protected static double RefX = 0.95045592705167;

		// Token: 0x04001436 RID: 5174
		protected static double RefY = 1.0;

		// Token: 0x04001437 RID: 5175
		protected static double RefZ = 1.089057750759878;

		// Token: 0x04001438 RID: 5176
		protected static double RefU = 0.19783000664283;

		// Token: 0x04001439 RID: 5177
		protected static double RefV = 0.46831999493879;

		// Token: 0x0400143A RID: 5178
		protected static double Kappa = 903.2962962;

		// Token: 0x0400143B RID: 5179
		protected static double Epsilon = 0.0088564516;
	}
}
