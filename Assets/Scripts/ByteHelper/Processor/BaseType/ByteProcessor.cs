// ------------------------------------------------------------
// @file       ByteProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:52
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    public class ByteProcessor : Processor<byte>
    {
        public override int GetBytesLength(byte value)
        {
            return sizeof(byte);
        }
        
        public override int Write(byte[] bytes, byte value, int index)
        {
            bytes[index] = value;
            return index + sizeof(byte);
        }

        public override int Read(byte[] bytes, int index, out byte value)
        {
            value = bytes[index];
            return index + sizeof(byte);
        }
    }
}