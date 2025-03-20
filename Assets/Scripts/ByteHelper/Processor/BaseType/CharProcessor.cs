// ------------------------------------------------------------
// @file       CharProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 17:03:28
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;

    public class CharProcessor : Processor<char>
    {
        public override int GetBytesLength(char value)
        {
            return sizeof(char);
        }

        public override int Write(byte[] bytes, char value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(char);
        }

        public override int Read(byte[] bytes, int index, out char value)
        {
            value = BitConverter.ToChar(bytes, index);
            return index + sizeof(char);
        }
    }
}