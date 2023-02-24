using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000881 RID: 2177
	public sealed class Transaction : Handle
	{
		// Token: 0x06004D62 RID: 19810 RVA: 0x00095727 File Offset: 0x00093927
		public Transaction(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x00095730 File Offset: 0x00093930
		public Result GetTransactionId(StringBuilder outBuffer, ref int inOutBufferLength)
		{
			Result result = Transaction.EOS_Ecom_Transaction_GetTransactionId(base.InnerHandle, outBuffer, ref inOutBufferLength);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x0009575C File Offset: 0x0009395C
		public uint GetEntitlementsCount(TransactionGetEntitlementsCountOptions options)
		{
			TransactionGetEntitlementsCountOptionsInternal transactionGetEntitlementsCountOptionsInternal = Helper.CopyProperties<TransactionGetEntitlementsCountOptionsInternal>(options);
			uint num = Transaction.EOS_Ecom_Transaction_GetEntitlementsCount(base.InnerHandle, ref transactionGetEntitlementsCountOptionsInternal);
			Helper.TryMarshalDispose<TransactionGetEntitlementsCountOptionsInternal>(ref transactionGetEntitlementsCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x00095794 File Offset: 0x00093994
		public Result CopyEntitlementByIndex(TransactionCopyEntitlementByIndexOptions options, out Entitlement outEntitlement)
		{
			TransactionCopyEntitlementByIndexOptionsInternal transactionCopyEntitlementByIndexOptionsInternal = Helper.CopyProperties<TransactionCopyEntitlementByIndexOptionsInternal>(options);
			outEntitlement = Helper.GetDefault<Entitlement>();
			IntPtr zero = IntPtr.Zero;
			Result result = Transaction.EOS_Ecom_Transaction_CopyEntitlementByIndex(base.InnerHandle, ref transactionCopyEntitlementByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<TransactionCopyEntitlementByIndexOptionsInternal>(ref transactionCopyEntitlementByIndexOptionsInternal);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero, out outEntitlement))
			{
				Transaction.EOS_Ecom_Entitlement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x000957EA File Offset: 0x000939EA
		public void Release()
		{
			Transaction.EOS_Ecom_Transaction_Release(base.InnerHandle);
		}

		// Token: 0x06004D67 RID: 19815
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_Entitlement_Release(IntPtr entitlement);

		// Token: 0x06004D68 RID: 19816
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_Transaction_Release(IntPtr transaction);

		// Token: 0x06004D69 RID: 19817
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_Transaction_CopyEntitlementByIndex(IntPtr handle, ref TransactionCopyEntitlementByIndexOptionsInternal options, ref IntPtr outEntitlement);

		// Token: 0x06004D6A RID: 19818
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_Transaction_GetEntitlementsCount(IntPtr handle, ref TransactionGetEntitlementsCountOptionsInternal options);

		// Token: 0x06004D6B RID: 19819
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_Transaction_GetTransactionId(IntPtr handle, StringBuilder outBuffer, ref int inOutBufferLength);
	}
}
