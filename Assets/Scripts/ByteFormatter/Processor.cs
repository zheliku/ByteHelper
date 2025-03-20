// ------------------------------------------------------------
// @file       Processor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:06
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    // 抽象类 Processor，用于处理对象和字节数组之间的转换
    public abstract class Processor
    {
        /// <summary>
        /// 获取对象在字节数组中占用的字节数
        /// </summary>
        /// <param name="value">对象实例</param>
        /// <returns>value 的字节长度</returns>
        public abstract int GetBytesLength(object value);

        /// <summary>
        /// 将对象写入 bytes 数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="value">对象实例</param>
        /// <param name="index">写入位置</param>
        /// <returns>写入对象的末尾位置</returns>
        public abstract int Write(byte[] bytes, object value, int index);

        
        /// <summary>
        /// 从 bytes 数组中读取对象
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="index">读取位置</param>
        /// <param name="value">对象取出值</param>
        /// <returns>读取对象的末尾位置</returns>
        public abstract int Read(byte[] bytes, int index, out object value);
    }

    public abstract class Processor<TValue> : Processor
    {
        /// <summary>
        /// 获取对象在字节数组中占用的字节数
        /// </summary>
        /// <param name="value">对象实例</param>
        /// <returns>value 的字节长度</returns>
        public abstract int GetBytesLength(TValue value);

        /// <summary>
        /// 将对象写入 bytes 数组
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="value">对象实例</param>
        /// <param name="index">写入位置</param>
        /// <returns>写入对象的末尾位置</returns>
        public abstract int Write(byte[] bytes, TValue value, int index);

        /// <summary>
        /// 从 bytes 数组中读取对象
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="index">读取位置</param>
        /// <param name="value">对象取出值</param>
        /// <returns>读取对象的末尾位置</returns>
        public abstract int Read(byte[] bytes, int index, out TValue value);

        public override int GetBytesLength(object value)
        {
            return GetBytesLength((TValue) value);
        }

        public override int Write(byte[] bytes, object value, int index)
        {
            return Write(bytes, (TValue) value, index);
        }

        public override int Read(byte[] bytes, int index, out object value)
        {
            // 调用泛型 Read 方法，并将结果赋值给 out object value
            int result = Read(bytes, index, out TValue typedValue);
            value = typedValue; // 将泛型结果赋值给 object 类型的 value
            return result;
        }
    }
}