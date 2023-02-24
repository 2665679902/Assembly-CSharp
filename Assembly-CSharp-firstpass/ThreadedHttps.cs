using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

// Token: 0x0200003E RID: 62
public class ThreadedHttps<T> where T : class, new()
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000F621 File Offset: 0x0000D821
	public static T Instance
	{
		get
		{
			return ThreadedHttps<T>.Singleton.instance;
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000F628 File Offset: 0x0000D828
	public bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		if (this.certFail)
		{
			return false;
		}
		this.certFail = true;
		string text = "";
		if (sslPolicyErrors == SslPolicyErrors.None)
		{
			this.certFail = false;
		}
		else if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
		{
			this.certFail = false;
			for (int i = 0; i < chain.ChainStatus.Length; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					"[",
					i.ToString(),
					"] ",
					chain.ChainStatus[i].Status.ToString(),
					"\n"
				});
				if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
				{
					chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
					chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
					chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
					chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
					if (!chain.Build((X509Certificate2)certificate))
					{
						this.certFail = true;
					}
				}
			}
		}
		else
		{
			this.certFail = true;
		}
		if (this.certFail)
		{
			X509Certificate2 x509Certificate = new X509Certificate2(certificate);
			Debug.LogWarning(string.Concat(new string[]
			{
				this.serviceName,
				": ",
				sslPolicyErrors.ToString(),
				"\n",
				text,
				"\n",
				x509Certificate.ToString()
			}));
		}
		return !this.certFail;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000F7AC File Offset: 0x0000D9AC
	public void Start()
	{
		if (this.updateThread != null)
		{
			this.End();
		}
		if (this.certFail)
		{
			return;
		}
		this.packets = new List<byte[]>();
		this.shouldQuit = false;
		this.updateThread = new Thread(new ThreadStart(this.SendData));
		this.updateThread.Start();
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000F804 File Offset: 0x0000DA04
	public void End()
	{
		this.Quit();
		if (this.updateThread == null)
		{
			return;
		}
		if (this.updateThread != Thread.CurrentThread && !this.updateThread.Join(TimeSpan.FromSeconds(2.0)))
		{
			this.updateThread.Abort();
		}
		this.updateThread = null;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000F85A File Offset: 0x0000DA5A
	protected virtual void OnReplyRecieved(WebResponse response)
	{
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000F85C File Offset: 0x0000DA5C
	protected string Send(byte[] byteArray, bool isForce = false)
	{
		ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(this.RemoteCertificateValidationCallback));
		string text = "";
		int num = 0;
		for (;;)
		{
			try
			{
				string text2 = "https://" + this.LIVE_ENDPOINT;
				Stream stream = null;
				WebResponse webResponse = null;
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text2);
				httpWebRequest.AllowAutoRedirect = false;
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				httpWebRequest.ContentLength = (long)byteArray.Length;
				try
				{
					stream = httpWebRequest.GetRequestStream();
				}
				catch (WebException ex)
				{
					string message = ex.Message;
					text = string.Concat(new string[]
					{
						DateTime.Now.ToLongTimeString(),
						" ",
						this.serviceName,
						": Exception getting Request Stream:",
						message
					});
					Debug.LogWarning(text);
					throw;
				}
				try
				{
					stream.Write(byteArray, 0, byteArray.Length);
				}
				catch (WebException ex2)
				{
					string message2 = ex2.Message;
					text = string.Concat(new string[]
					{
						DateTime.Now.ToLongTimeString(),
						" ",
						this.serviceName,
						": Exception writing data to Stream:",
						message2
					});
					Debug.LogWarning(text);
					throw;
				}
				stream.Close();
				try
				{
					webResponse = httpWebRequest.GetResponse();
				}
				catch (WebException ex3)
				{
					string message3 = ex3.Message;
					WebResponse response = ex3.Response;
					if (response != null)
					{
						using (Stream responseStream = response.GetResponseStream())
						{
							text = new StreamReader(responseStream).ReadToEnd();
							goto IL_170;
						}
					}
					text = " -- we.Response is NULL";
					IL_170:
					text = string.Concat(new string[]
					{
						DateTime.Now.ToLongTimeString(),
						" ",
						this.serviceName,
						": Exception getting response:",
						message3,
						text
					});
					Debug.LogWarning(text);
					throw;
				}
				text = ((HttpWebResponse)webResponse).StatusDescription;
				if (text != "OK")
				{
					stream = webResponse.GetResponseStream();
					StreamReader streamReader = new StreamReader(stream);
					string text3 = streamReader.ReadToEnd();
					streamReader.Close();
					stream.Close();
					text = string.Concat(new string[] { this.serviceName, ": Server Responded with Status: [", text, "] Response: ", text3 });
				}
				else
				{
					this.OnReplyRecieved(webResponse);
				}
				webResponse.Close();
			}
			catch (Exception ex4)
			{
				if (!this.shouldQuit)
				{
					if (this.certFail)
					{
						Debug.LogWarning(this.serviceName + ": Cert fail, quitting");
						try
						{
							this.OnReplyRecieved(null);
						}
						catch
						{
						}
						this.QuitOnError();
						break;
					}
					num++;
					if (num > 3)
					{
						text = string.Concat(new string[]
						{
							DateTime.Now.ToLongTimeString(),
							" ",
							this.serviceName,
							": Max Retries (",
							3.ToString(),
							") reached. Disabling ",
							this.serviceName,
							"..."
						});
						Debug.LogWarning(text);
						try
						{
							this.OnReplyRecieved(null);
						}
						catch
						{
						}
						this.QuitOnError();
						break;
					}
					string message4 = ex4.Message;
					string stackTrace = ex4.StackTrace;
					TimeSpan timeSpan = TimeSpan.FromSeconds(Math.Pow(2.0, (double)(num + 3)));
					text = string.Concat(new string[]
					{
						DateTime.Now.ToLongTimeString(),
						" ",
						this.serviceName,
						": Exception (retrying in ",
						timeSpan.TotalSeconds.ToString(),
						" seconds): ",
						message4,
						"\n",
						stackTrace
					});
					Debug.LogWarning(text);
					if (isForce)
					{
						Debug.LogWarning(ex4.StackTrace);
						break;
					}
					Thread.Sleep(timeSpan);
				}
				continue;
			}
			break;
		}
		ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(this.RemoteCertificateValidationCallback));
		return text;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000FCDC File Offset: 0x0000DEDC
	protected bool ShouldQuit()
	{
		bool flag = false;
		object quitLock = this._quitLock;
		lock (quitLock)
		{
			flag = (this.shouldQuit && this.packets.Count == 0) || this.quitOnError;
		}
		return flag;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000FD38 File Offset: 0x0000DF38
	protected void QuitOnError()
	{
		object quitLock = this._quitLock;
		lock (quitLock)
		{
			this.quitOnError = true;
			this.shouldQuit = true;
		}
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000FD80 File Offset: 0x0000DF80
	protected void Quit()
	{
		object quitLock = this._quitLock;
		lock (quitLock)
		{
			this.shouldQuit = true;
		}
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
	protected byte[] GetPacket()
	{
		byte[] array = null;
		List<byte[]> list = this.packets;
		lock (list)
		{
			if (this.packets.Count > 0)
			{
				array = this.packets[0];
				this.packets.RemoveAt(0);
			}
		}
		return array;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0000FE28 File Offset: 0x0000E028
	protected void PutPacket(byte[] packet, bool infront = false)
	{
		List<byte[]> list = this.packets;
		lock (list)
		{
			if (infront)
			{
				this.packets.Insert(0, packet);
			}
			else
			{
				this.packets.Add(packet);
				this._waitHandle.Set();
			}
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0000FE8C File Offset: 0x0000E08C
	public void ForceSendData()
	{
		for (byte[] array = this.GetPacket(); array != null; array = this.GetPacket())
		{
			if (this.Send(array, true) != "OK")
			{
				this.PutPacket(array, true);
				return;
			}
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0000FECC File Offset: 0x0000E0CC
	protected void SendData()
	{
		while (!this.ShouldQuit())
		{
			byte[] packet = this.GetPacket();
			if (packet != null)
			{
				if (this.Send(packet, false) != "OK")
				{
					this.PutPacket(packet, true);
				}
			}
			else
			{
				this._waitHandle.WaitOne();
			}
			if (this.singleSend)
			{
				return;
			}
		}
	}

	// Token: 0x04000398 RID: 920
	protected string serviceName;

	// Token: 0x04000399 RID: 921
	protected string CLIENT_KEY;

	// Token: 0x0400039A RID: 922
	protected string LIVE_ENDPOINT;

	// Token: 0x0400039B RID: 923
	private bool certFail;

	// Token: 0x0400039C RID: 924
	private const int retryCount = 3;

	// Token: 0x0400039D RID: 925
	protected Thread updateThread;

	// Token: 0x0400039E RID: 926
	protected List<byte[]> packets = new List<byte[]>();

	// Token: 0x0400039F RID: 927
	private EventWaitHandle _waitHandle = new AutoResetEvent(false);

	// Token: 0x040003A0 RID: 928
	protected bool shouldQuit;

	// Token: 0x040003A1 RID: 929
	protected bool quitOnError;

	// Token: 0x040003A2 RID: 930
	private object _quitLock = new object();

	// Token: 0x040003A3 RID: 931
	protected bool singleSend;

	// Token: 0x02000999 RID: 2457
	private class Singleton
	{
		// Token: 0x0400215B RID: 8539
		internal static readonly T instance = new T();
	}
}
