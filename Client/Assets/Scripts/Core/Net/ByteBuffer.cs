using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

namespace Net
{
    public class ByteBuffer
    {
        #region 变量

        MemoryStream mStream = null;
        BinaryWriter mWriter = null;
        BinaryReader mReader = null;

        #endregion

        #region 函数

        public ByteBuffer()
        {
            mStream = new MemoryStream();
            mWriter = new BinaryWriter(mStream);
        }

        public ByteBuffer(byte[] data)
        {
            if (data != null)
            {
                mStream = new MemoryStream(data);
                mReader = new BinaryReader(mStream);
            }
            else
            {
                mStream = new MemoryStream();
                mWriter = new BinaryWriter(mStream);
            }
        }

        public void Close()
        {
            if (mWriter != null) mWriter.Close();
            if (mReader != null) mReader.Close();

            mStream.Close();
            mWriter = null;
            mReader = null;
            mStream = null;
        }

        public void WriteByte(byte v)
        {
            mWriter.Write(v);
        }

        public void WriteInt(int v)
        {
            mWriter.Write((int)v);
        }

        public void WriteShort(UInt16 v)
        {
            mWriter.Write((UInt16)v);
        }

        public void WriteLong(long v)
        {
            mWriter.Write((long)v);
        }

        public void WriteFloat(float v)
        {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            mWriter.Write(BitConverter.ToSingle(temp, 0));
        }

        public void WriteDouble(double v)
        {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            mWriter.Write(BitConverter.ToDouble(temp, 0));
        }

        public void WriteString(string v)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            mWriter.Write((ushort)bytes.Length);
            mWriter.Write(bytes);
        }

        public void WriteBytes(byte[] v)
        {
            //Debug.Log("WriteBytes with uint 16 len" + v.Length);
            //writer.Write((UInt16)(v.Length));
            mWriter.Write(v);
        }

        public byte ReadByte()
        {
            return mReader.ReadByte();
        }

        public int ReadInt()
        {
            return (int)mReader.ReadInt32();
        }

        public ushort ReadShort()
        {
            return (ushort)mReader.ReadInt16();
        }

        public long ReadLong()
        {
            return (long)mReader.ReadInt64();
        }

        public float ReadFloat()
        {
            byte[] temp = BitConverter.GetBytes(mReader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        public double ReadDouble()
        {
            byte[] temp = BitConverter.GetBytes(mReader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }

        public string ReadString()
        {
            ushort len = ReadShort();
            byte[] buffer = new byte[len];
            buffer = mReader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }

        public byte[] ReadBytes()
        {
            int len = ReadInt();
            return mReader.ReadBytes(len);
        }

        public byte[] ReadBytes(int len)
        {
            return mReader.ReadBytes(len);
        }

        public byte[] ToBytes()
        {
            mWriter.Flush();
            return mStream.ToArray();
        }

        public void Flush()
        {
            mWriter.Flush();
        }

        #endregion
    }
}