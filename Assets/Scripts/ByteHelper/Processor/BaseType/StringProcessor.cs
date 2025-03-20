// ------------------------------------------------------------
// @file       StringProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:58
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;
    using System.Text;

    public class StringProcessor : Processor<string>
    {
        public override int GetBytesLength(string value)
        {
            return sizeof(int) + value.Length;
        }
        
        public override int Write(byte[] bytes, string value, int index)
        {
            BitConverter.GetBytes(value.Length).CopyTo(bytes, index);
            Encoding.UTF8.GetBytes(value).CopyTo(bytes, index + sizeof(int));
            return index + sizeof(int) + value.Length;
        }

        public override int Read(byte[] bytes, int index, out string value)
        {
            int length = BitConverter.ToInt32(bytes, index);
            value = Encoding.UTF8.GetString(bytes, index + sizeof(int), length);
            return index + sizeof(int) + length;
        }
    }
}