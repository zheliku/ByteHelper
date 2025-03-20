// ------------------------------------------------------------
// @file       TxtHelper.cs
// @brief
// @author     zheliku
// @Modified   2024-12-06 23:12:37
// @Copyright  Copyright (c) 2024, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System.IO;
    using UnityEngine;

    public class ByteHelper
    {
        public const string EXTENSION = ".bytes";

        public static string BinarySavePath { get; set; } = Application.persistentDataPath + "/Binary";

#if UNITY_EDITOR
        [UnityEditor.MenuItem("ByteHelper/Open Binary Folder")]
        public static void OpenBinarySavePath()
        {
            if (!Directory.Exists(BinarySavePath))
            {
                Directory.CreateDirectory(BinarySavePath);
            }
            
            UnityEditor.EditorUtility.RevealInFinder(BinarySavePath);
        }
#endif

        /// <summary>
        /// Data 序列化为字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <typeparam name="TData">数据类型，需要添加 [ByteSerializableAttribute] 特性</typeparam>
        public static byte[] Serialize<TData>(TData data)
        {
            var processor = ByteFormatter.GetProcessor<TData>();
            var bytes     = new byte[processor.GetBytesLength(data)];
            processor.Write(bytes, data, 0);
            return bytes;
        }

        /// <summary>
        /// 字节数组反序列化为 Data
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <typeparam name="TData">数据类型，需要添加 [ByteSerializableAttribute] 特性</typeparam>
        public static TData Deserialize<TData>(byte[] bytes)
        {
            ByteFormatter.Read<TData>(bytes, 0, out var value);
            return value;
        }
        
        /// <summary>
        /// 存储数据为 bytes 文件
        /// </summary>
        /// <param name="filePath">文件路径，可以不写后缀</param>
        /// <param name="data">存储数据，如果是自定义结构，则需要添加 [Serializable] 特性</param>
        /// <param name="extension">文件扩展名</param>
        public static void SaveBytes<TData>(string filePath, TData data, string extension = EXTENSION)
        {
            string fullPath = Path.Combine(BinarySavePath, filePath);
            fullPath = Path.ChangeExtension(fullPath, EXTENSION);

            var directory = Path.GetDirectoryName(fullPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            byte[] bytes = Serialize(data);
            File.WriteAllBytes(fullPath, bytes);
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        
        /// <summary>
        /// 读取 bytes 文件中的数据
        /// </summary>
        /// <param name="filePath">文件路径，可以不写后缀</param>
        /// <param name="extension">文件扩展名</param>
        public static TData LoadBytes<TData>(string filePath, string extension = ByteHelper.EXTENSION)
        {
            string fullPath = Path.Combine(BinarySavePath, filePath);
            fullPath = Path.ChangeExtension(fullPath, EXTENSION);

            if (!File.Exists(fullPath))
            { // 不存在文件，则警告，并返回默认值
                Debug.LogWarning($"ByteHelper: Can't find path \"{fullPath}\"");
                return default(TData);
            }

            byte[] bytes = File.ReadAllBytes(fullPath);
            return Deserialize<TData>(bytes);
        }
    }
}