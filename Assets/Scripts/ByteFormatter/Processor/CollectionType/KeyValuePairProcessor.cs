// ------------------------------------------------------------
// @file       KeyValuePairProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-21 04:03:02
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;
    using System.Collections.Generic;

    public class KeyValuePairProcessor<TKey, TValue> : Processor<KeyValuePair<TKey, TValue>>
    {
        public override int GetBytesLength(KeyValuePair<TKey, TValue> value)
        {
            return sizeof(int) + ByteFormatter.GetBytesLength(value.Key) + ByteFormatter.GetBytesLength(value.Value);
        }

        public override int Write(byte[] bytes, KeyValuePair<TKey, TValue> value, int index)
        {
            var length = ByteFormatter.GetBytesLength(value.Key, value.Value);
            BitConverter.GetBytes(length).CopyTo(bytes, index);
            index += sizeof(int);

            index = ByteFormatter.Write(bytes, value.Key, index);   // 先写 key，依据类型递归查找写入
            index = ByteFormatter.Write(bytes, value.Value, index); // 再写 value，依据类型递归查找写入
            return index;
        }

        public override int Read(byte[] bytes, int index, out KeyValuePair<TKey, TValue> value)
        {
            var length = BitConverter.ToInt32(bytes, index);
            index += sizeof(int);
            
            index = ByteFormatter.Read<TKey>(bytes, index, out var key);
            index = ByteFormatter.Read<TValue>(bytes, index, out var v);
            value = new KeyValuePair<TKey, TValue>(key, v);
            
            return index;
        }
    }
}