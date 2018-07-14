using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using Net;
using UnityDebuger;

public class SocketClient
{
    #region 常量和枚举

    public enum DisType
    {
        Exception,
        Disconnect,
    }

    private const int MAX_READ = 8192;

    #endregion

    #region 变量

    /// <summary>
    /// tcp client
    /// </summary>
    private TcpClient mClient = null;

    /// <summary>
    /// network stream
    /// </summary>
    private NetworkStream mNetStream = null;

    /// <summary>
    /// memory stream
    /// </summary>
    private MemoryStream mMemStream = null;

    /// <summary>
    /// reader
    /// </summary>
    private BinaryReader mReader = null;
    
    /// <summary>
    /// 网络接收的数据
    /// </summary>
    private byte[] mByteBuffer = new byte[MAX_READ];

    #endregion

    #region 函数

    // Use this for initialization
    public SocketClient()
    {
    }

    /// <summary>
    /// 注册代理
    /// </summary>
    public void OnRegister()
    {
        mMemStream = new MemoryStream();
        mReader = new BinaryReader(mMemStream);
    }

    /// <summary>
    /// 移除代理
    /// </summary>
    public void OnRemove()
    {
        Close();
        mReader.Close();
        mMemStream.Close();
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    void ConnectServer(string host, int port)
    {
        mClient = null;
        mClient = new TcpClient();
        mClient.SendTimeout = 1000;
        mClient.ReceiveTimeout = 1000;
        mClient.NoDelay = true;

        try
        {
            mClient.BeginConnect(host, port, new AsyncCallback(OnConnect), null);
        }
        catch (Exception e)
        {
            Close();
            Debuger.LogError(e.Message);
        }
    }

    /// <summary>
    /// 连接上服务器
    /// </summary>
    void OnConnect(IAsyncResult asr)
    {
        mNetStream = mClient.GetStream();
        mNetStream.BeginRead(mByteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
        NetManager.Instance.OnConnect();
    }

    /// <summary>
    /// 写数据
    /// </summary>
    void WriteMessage(byte[] message)
    {
        MemoryStream ms = null;
        using (ms = new MemoryStream())
        {
            ms.Position = 0;
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(message);
            writer.Flush();
            if (mClient != null && mClient.Connected)
            {
                byte[] payload = ms.ToArray();
                mNetStream.BeginWrite(payload, 0, payload.Length, new AsyncCallback(OnWrite), null);
            }
            else
            {
                Debuger.LogError("client.connected----->>false");
            }
        }
    }

    /// <summary>
    /// 读取消息
    /// </summary>
    void OnRead(IAsyncResult asr)
    {
        int bytesRead = 0;
        try
        {
            lock (mClient.GetStream())
            {       
                //读取字节流到缓冲区
                bytesRead = mClient.GetStream().EndRead(asr);
            }

            if (bytesRead < 1)
            {       
                //包尺寸有问题，断线处理
                OnDisconnected(DisType.Disconnect, "bytesRead < 1");
                return;
            }

            //分析数据包内容，抛给逻辑层
            OnReceive(mByteBuffer, bytesRead);

            lock (mClient.GetStream())
            {         
                //分析完，再次监听服务器发过来的新消息
                Array.Clear(mByteBuffer, 0, mByteBuffer.Length);   //清空数组
                mClient.GetStream().BeginRead(mByteBuffer, 0, MAX_READ, new AsyncCallback(OnRead), null);
            }
        }
        catch (Exception ex)
        {
            //PrintBytes();
            OnDisconnected(DisType.Exception, ex.Message);
        }
    }

    /// <summary>
    /// 丢失链接
    /// </summary>
    void OnDisconnected(DisType dis, string msg)
    {
        Debuger.Log("OnDisconnected" + msg);
        Close();   //关掉客户端链接
        NetManager.Instance.OnDisConnect();
    }

    /// <summary>
    /// 打印字节
    /// </summary>
    /// <param name="bytes"></param>
    void PrintBytes()
    {
        string returnStr = string.Empty;
        for (int i = 0; i < mByteBuffer.Length; i++)
        {
            returnStr += mByteBuffer[i].ToString("X2");
        }

        Debuger.LogError(returnStr);
    }

    /// <summary>
    /// 向链接写入数据流
    /// </summary>
    void OnWrite(IAsyncResult r)
    {
        try
        {
            mNetStream.EndWrite(r);
        }
        catch (Exception ex)
        {
            Debuger.LogError("OnWrite--->>>" + ex.Message);
        }
    }

    /// <summary>
    /// 接收到消息
    /// </summary>
    void OnReceive(byte[] bytes, int length)
    {
        mMemStream.Seek(0, SeekOrigin.End);
        mMemStream.Write(bytes, 0, length);

        //Reset to beginning
        mMemStream.Seek(0, SeekOrigin.Begin);
        while (RemainingBytes() > 2)
        {
            ushort messageLen = mReader.ReadUInt16();
            if (RemainingBytes() >= messageLen)
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(ms);
                writer.Write(mReader.ReadBytes(messageLen));
                ms.Seek(0, SeekOrigin.Begin);
                OnReceivedMessage(ms);
            }
            else
            {
                mMemStream.Position = mMemStream.Position - 2;
                break;
            }
        }

        byte[] leftover = mReader.ReadBytes((int)RemainingBytes());
        mMemStream.SetLength(0);
        mMemStream.Write(leftover, 0, leftover.Length);
    }

    /// <summary>
    /// 剩余的字节
    /// </summary>
    private long RemainingBytes()
    {
        return mMemStream.Length - mMemStream.Position;
    }

    /// <summary>
    /// 接收到消息
    /// </summary>
    /// <param name="ms"></param>
    void OnReceivedMessage(MemoryStream ms)
    {
        BinaryReader r = new BinaryReader(ms);
        byte[] message = r.ReadBytes((int)(ms.Length - ms.Position));
        ByteBuffer buffer = new ByteBuffer(message);
        int mainId = buffer.ReadShort();
        int pbDataLen = message.Length - 2;
        byte[] pbData = buffer.ReadBytes(pbDataLen);
        NetManager.Instance.DispatchProto(mainId, pbData);
    }

    /// <summary>
    /// 会话发送
    /// </summary>
    void SessionSend(byte[] bytes)
    {
        WriteMessage(bytes);
    }

    /// <summary>
    /// 关闭链接
    /// </summary>
    public void Close()
    {
        if (mClient != null)
        {
            if (mClient.Connected)
                mClient.Close();

            mClient = null;
        }
    }

    /// <summary>
    /// 发送连接请求
    /// </summary>
    public void SendConnect()
    {
        ConnectServer(AppConst.SocketAddress, AppConst.SocketPort);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public void SendMessage(ByteBuffer buffer)
    {
        SessionSend(buffer.ToBytes());
        buffer.Close();
    }

    /// <summary>
    /// 是否连接
    /// </summary>
    /// <returns></returns>
    public bool IsConnected()
    {
        return (mClient != null && mClient.Connected);
    }

    #endregion
}
