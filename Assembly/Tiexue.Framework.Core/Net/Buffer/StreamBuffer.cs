﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Tiexue.Framework.Net
{
    [Serializable, ComVisible(true)]
    public class StreamBuffer : IDisposable
    {
        #region Fields
        public static readonly StreamBuffer Null = new StreamBuffer();
        private byte[] _buffer;
        private byte[] _orginBuffer;
        private Encoder _encoder;
        private Encoding _encoding;
        protected Stream BaseStream;

        #endregion

        #region Methods

        public StreamBuffer()
        {
            this.BaseStream = new MemoryStream();
            this._buffer = new byte[0x10];
            this._encoding = Encoding.UTF8;//(false, true);
            this._encoder = this._encoding.GetEncoder();
        }

        public StreamBuffer(Encoding encoding)
        {
            this.BaseStream = new MemoryStream();
            this._buffer = new byte[0x10];
            this._encoding = encoding;
            this._encoder = this._encoding.GetEncoder();
        }

        public StreamBuffer(byte[] buffer)
            : this(buffer, 0, buffer.Length)
        {
            _orginBuffer = buffer;
        }

        public StreamBuffer(byte[] buffer, int start, int count)
        {

            this.BaseStream = new MemoryStream(buffer, start, count, false);
            this.BaseStream.Position = 0;
            this._buffer = new byte[0x10];
            this._encoding = Encoding.UTF8;
            this._encoder = this._encoding.GetEncoder();
        }

        public StreamBuffer(byte[] buffer, Encoding encoding)
        {
            this.BaseStream = new MemoryStream(buffer);
            this.BaseStream.Position = 0;
            this._buffer = new byte[0x10];
            this._encoding = encoding;
            this._encoder = this._encoding.GetEncoder();
        }

        public void Initialize()
        {
            Close();
            this.BaseStream = new MemoryStream();
        }

        public StreamBuffer Clone()
        {
            return this;
        }


        public virtual void Close()
        {
            this.Dispose(true);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                BaseStream.Close();
            }
        }


        public virtual void Flush()
        {
            this.BaseStream.Flush();
        }


        public virtual long Seek(int offset, SeekOrigin origin)
        {
            return this.BaseStream.Seek((long)offset, origin);
        }


        public virtual void SetLength(long value)
        {
            this.BaseStream.SetLength(value);
        }


        public bool Peek()
        {
            return BaseStream.Position >= BaseStream.Length ? false : true;
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }


        public byte[] ToByteArray()
        {

            long offset = BaseStream.Position;
            int numBytesToRead = (int)(BaseStream.Length - offset);
            if (numBytesToRead == 0)
                return new byte[0];
            byte[] bytes = new byte[numBytesToRead];
            BaseStream.Read(bytes, 0, numBytesToRead - 1);
            return bytes;

        }

        public byte[] ToByteArrays()
        {
            long org = BaseStream.Position;
            BaseStream.Position = 0;
            byte[] ret = new byte[BaseStream.Length];
            BaseStream.Read(ret, 0, ret.Length);
            BaseStream.Position = org;
            return ret;

        }

        public void Put(bool value)
        {
            this._buffer[0] = value ? (byte)1 : (byte)0;
            this.BaseStream.Write(_buffer, 0, 1);
        }


        public void Put(Byte value)
        {
            this.BaseStream.WriteByte(value);
        }

        public void Put(int index, byte value)
        {
            int pos = (int)this.BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            Put(value);
            Seek(pos, SeekOrigin.Begin);
        }

        public void Put(Byte[] value)
        {
            if (value == null)
            {
                return;
            }
            this.BaseStream.Write(value, 0, value.Length);
        }


        public void Put(Byte[] value, int index, int count)
        {
            this.BaseStream.Write(value, index, count);
        }

        public void Put(char ch)
        {
            PutUShort((ushort)ch);
        }


        public void PutChar(int index, char ch)
        {
            int pos = (int)this.BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            PutUShort(ch);
            Seek(pos, SeekOrigin.Begin);
        }

        public void Put(ushort value)
        {
            PutUShort(value);
        }

        private void PutUShort(ushort value)
        {
            this._buffer[0] = (byte)(value >> 8);
            this._buffer[1] = (byte)value;
            this.BaseStream.Write(this._buffer, 0, 2);
        }

        public void PutUShort(int index, ushort value)
        {
            int pos = (int)this.BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            PutUShort(value);
            Seek(pos, SeekOrigin.Begin);
        }


        public void Put(int value)
        {
            PutInt((uint)value);
        }

        private void PutInt(uint value)
        {
            this._buffer[0] = (byte)(value >> 0x18);
            this._buffer[1] = (byte)(value >> 0x10);
            this._buffer[2] = (byte)(value >> 8);
            this._buffer[3] = (byte)value;
            this.BaseStream.Write(this._buffer, 0, 4);
        }

        public void PutInt(int index, uint value)
        {
            int pos = (int)this.BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            PutInt(value);
            Seek(pos, SeekOrigin.Begin);
        }


        public void Put(long value)
        {
            this._buffer[0] = (byte)(value >> 0x38);
            this._buffer[1] = (byte)(value >> 0x30);
            this._buffer[2] = (byte)(value >> 0x28);
            this._buffer[3] = (byte)(value >> 0x20);
            this._buffer[4] = (byte)(value >> 0x18);
            this._buffer[5] = (byte)(value >> 0x10);
            this._buffer[6] = (byte)(value >> 8);
            this._buffer[7] = (byte)value;
            this.BaseStream.Write(this._buffer, 0, 8);
        }

        public void PutLong(int index, long value)
        {
            int pos = (int)this.BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            Put(value);
            Seek(pos, SeekOrigin.Begin);
        }

        public bool GetBoolean()
        {
            return Get() == 0 ? false : true;
        }


        public byte Get()
        {
            return (byte)BaseStream.ReadByte();
        }

        public byte Get(int index)
        {
            int current = (int)this.BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            byte ret = Get();
            Seek(current, SeekOrigin.Begin);
            return ret;
        }

        public byte[] GetByteArray(uint count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            byte[] buffer = new byte[count];
            int num = BaseStream.Read(buffer, 0, (int)count);
            return buffer;
        }



        public char GetChar()
        {
            return (char)GetUShort();
        }

        public char GetChar(int index)
        {
            int current = (int)BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            char c = GetChar();
            Seek(current, SeekOrigin.Begin);
            return c;
        }


        public ushort GetUShort()
        {
            ushort ret = (ushort)(Get() << 8 | Get());
            return ret;
        }

        public ushort GetUShort(int index)
        {
            int current = (int)BaseStream.Position;
            Seek(index, SeekOrigin.Begin);
            ushort ret = GetUShort();
            Seek(current, SeekOrigin.Begin);
            return ret;
        }

        public int GetInt()
        {
            int ret = (int)(Get() << 0x18 | Get() << 0x10 | Get() << 8 | Get());
            return ret;
        }

        public uint GetUInt()
        {
            return (uint)GetInt();
        }


        public long GetLong()
        {
            uint num1 = (uint)GetInt();
            uint num2 = (uint)GetInt();
            return (long)((num1 << 0x20) | num2);
        }


        public int Length
        {
            get
            {
                return (int)this.BaseStream.Length;
            }
        }


        public int Position
        {
            get
            {
                return (int)this.BaseStream.Position;
            }
            set
            {
                this.BaseStream.Position = value;
            }
        }


        public int Remaining()
        {
            return (int)(BaseStream.Length - BaseStream.Position);
        }
        public bool HasRemaining()
        {
            return Remaining() > 0;
        }

        public void Rewind()
        {
            Seek(0, SeekOrigin.Begin);
        }

        public byte[] Buffers
        {
            get { return _orginBuffer; }
        }
        #endregion
    }
}
