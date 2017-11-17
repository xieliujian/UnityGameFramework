using UnityEngine;
using Util;
using System.Collections.Generic;
using System;
using Proto;
using System.IO;
using Google.Protobuf;

namespace Net
{
    public delegate void TocHandler(object data);

    public class NetManager : SingletonMonoBehaviour<NetManager>
    {
        #region 变量

        /// <summary>
        /// Socket
        /// </summary>
        private SocketClient mSocketClient = new SocketClient();

        /// <summary>
        /// 回调消息表
        /// </summary>
        private Dictionary<Type, TocHandler> mHandlerDict = new Dictionary<Type, TocHandler>();

        /// <summary>
        /// 事件队列
        /// </summary>
        private static Queue<KeyValuePair<Type, object>> mEventQueue = new Queue<KeyValuePair<Type, object>>();

        /// <summary>
        /// Socket
        /// </summary>
        public SocketClient SocketClient
        {
            get
            {
                return mSocketClient;
            }
        }

        #endregion

        #region 内置函数

        void Start()
        {
            Init();
        }

        void Update()
        {
            UpdateEventQueue();
        }

        #endregion

        #region 函数

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            mSocketClient.OnRegister();
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect()
        {
            mSocketClient.SendConnect();
        }

        /// <summary>
        /// 关闭网络
        /// </summary>
        public void OnRemove()
        {
            mSocketClient.OnRemove();
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(ByteBuffer buffer)
        {
            mSocketClient.SendMessage(buffer);
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(IMessage obj)
        {
            if (!mSocketClient.IsConnected())
                return;

            if (!ProtoDic.ContainProtoType(obj.GetType()))
            {
                Debug.LogError("不存协议类型");
                return;
            }

            ByteBuffer buff = new ByteBuffer();
            int protoId = ProtoDic.GetProtoIdByProtoType(obj.GetType());

            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                obj.WriteTo(ms);
                result = ms.ToArray();
            }

            UInt16 lengh = (UInt16)(result.Length + 2);
            //Debug.Log("lengh" + lengh + ",protoId" + protoId);
            buff.WriteShort((UInt16)lengh);
            buff.WriteShort((UInt16)protoId);
            buff.WriteBytes(result);
            SendMessage(buff);
        }

        /// <summary>
        /// 连接 
        /// </summary>
        public void OnConnect()
        {
            Debug.Log("======连接========");
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void OnDisConnect()
        {
            Debug.Log("======断开连接========");
        }

        /// <summary>
        /// 派发协议
        /// </summary>
        /// <param name="protoId"></param>
        /// <param name="buff"></param>
        public void DispatchProto(int protoId, byte[] buff)
        {
            if (!ProtoDic.ContainProtoId(protoId))
            {
                Debug.LogError("未知协议号");
                return;
            }

            Type protoType = ProtoDic.GetProtoTypeByProtoId(protoId);
            try
            {
                MessageParser messageParser = ProtoDic.GetMessageParser(protoType.TypeHandle);
                object toc = messageParser.ParseFrom(buff);
                mEventQueue.Enqueue(new KeyValuePair<Type, object>(protoType, toc));
            }
            catch
            {
                Debug.Log("DispatchProto Error:" + protoType.ToString());
            }
        }

        /// <summary>
        /// 增加消息回调
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        public void AddHandler(Type type, TocHandler handler)
        {
            if (mHandlerDict.ContainsKey(type))
            {
                mHandlerDict[type] += handler;
            }
            else
            {
                mHandlerDict.Add(type, handler);
            }
        }

        /// <summary>
        /// 刷新事件队列
        /// </summary>
        private void UpdateEventQueue()
        {
            if (mEventQueue.Count <= 0)
                return;

            while (mEventQueue.Count > 0)
            {
                KeyValuePair<Type, object> _event = mEventQueue.Dequeue();
                if (mHandlerDict.ContainsKey(_event.Key))
                {
                    mHandlerDict[_event.Key](_event.Value);
                }
            }
        }

        #endregion
    } 
}
