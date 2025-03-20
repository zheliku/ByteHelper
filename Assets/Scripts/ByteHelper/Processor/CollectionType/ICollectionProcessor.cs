// ------------------------------------------------------------
// @file       IEnumerableProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:29
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;
    using System.Collections.Generic;

    public class ICollectionProcessor<TCollection, TValue> : Processor<TCollection> where TCollection : ICollection<TValue>
    {
        public override int GetBytesLength(TCollection value)
        {
            var length = sizeof(int);
            foreach (var item in value)
            {
                length += ByteFormatter.GetBytesLength(item);
            }
            return length;
        }

        public override int Write(byte[] bytes, TCollection value, int index)
        {
            int count = value.Count;

            // 写长度
            BitConverter.GetBytes(count).CopyTo(bytes, index);

            index += sizeof(int); // 留 1 个 int 位置用于写长度

            // 写内容
            foreach (var item in value)
            {
                index = ByteFormatter.Write(bytes, item, index);
            }

            return index;
        }

        public override int Read(byte[] bytes, int index, out TCollection value)
        {
            // 1. 读取长度（元素数量）
            int length = BitConverter.ToInt32(bytes, index);
             index += sizeof(int);

            // 2. 读取内容
            value = (TCollection) Activator.CreateInstance(typeof(TCollection));
            for (int i = 0; i < length; i++)
            {
                index = ByteFormatter.Read(bytes, index, typeof(TValue), out var item);
                value.Add((TValue) item);
            }

            return index;
        }
    }
}