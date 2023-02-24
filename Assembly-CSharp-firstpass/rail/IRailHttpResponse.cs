using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200036A RID: 874
	public interface IRailHttpResponse : IRailComponent
	{
		// Token: 0x06002EC9 RID: 11977
		int GetHttpResponseCode();

		// Token: 0x06002ECA RID: 11978
		RailResult GetResponseHeaderKeys(List<string> header_keys);

		// Token: 0x06002ECB RID: 11979
		string GetResponseHeaderValue(string header_key);

		// Token: 0x06002ECC RID: 11980
		string GetResponseBodyData();

		// Token: 0x06002ECD RID: 11981
		uint GetContentLength();

		// Token: 0x06002ECE RID: 11982
		string GetContentType();

		// Token: 0x06002ECF RID: 11983
		string GetContentRange();

		// Token: 0x06002ED0 RID: 11984
		string GetContentLanguage();

		// Token: 0x06002ED1 RID: 11985
		string GetContentEncoding();

		// Token: 0x06002ED2 RID: 11986
		string GetLastModified();
	}
}
