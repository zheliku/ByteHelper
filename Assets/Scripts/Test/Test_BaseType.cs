// ------------------------------------------------------------
// @file       BaseType.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 17:03:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Test
{
    using UnityEngine.UI;
    using ByteFormatter;

    public class Test_BaseType : MonoBehaviour
    {
        public Button Btn;

        public Text Text;

        [Header("Set Your BaseType")]
        public int Int;

        public short  Short;
        public long   Long;
        public float  Float;
        public double Double;
        public bool   Bool;
        public char   Char;
        public byte   Byte;
        public string String;

        private void Start()
        {
            SerializeData();

            Text.text = "Serialize Data Success!";
            
            Btn.onClick.AddListener(OnClick);
        }

        private void SerializeData()
        {
            ByteHelper.SaveBytes($"BaseType/{nameof(Int)}", Int);
            ByteHelper.SaveBytes($"BaseType/{nameof(Short)}", Short);
            ByteHelper.SaveBytes($"BaseType/{nameof(Long)}", Long);
            ByteHelper.SaveBytes($"BaseType/{nameof(Float)}", Float);
            ByteHelper.SaveBytes($"BaseType/{nameof(Double)}", Double);
            ByteHelper.SaveBytes($"BaseType/{nameof(Bool)}", Bool);
            ByteHelper.SaveBytes($"BaseType/{nameof(Char)}", Char);
            ByteHelper.SaveBytes($"BaseType/{nameof(Byte)}", Byte);
            ByteHelper.SaveBytes($"BaseType/{nameof(String)}", String);
        }

        public void OnClick()
        {
            Text.text = "Int: " + ByteHelper.LoadBytes<int>($"BaseType/{nameof(Int)}") + "\n" +
                        "Short: " + ByteHelper.LoadBytes<short>($"BaseType/{nameof(Short)}") + "\n" +
                        "Long: " + ByteHelper.LoadBytes<long>($"BaseType/{nameof(Long)}") + "\n" +
                        "Float: " + ByteHelper.LoadBytes<float>($"BaseType/{nameof(Float)}") + "\n" +
                        "Double: " + ByteHelper.LoadBytes<double>($"BaseType/{nameof(Double)}") + "\n" +
                        "Bool: " + ByteHelper.LoadBytes<bool>($"BaseType/{nameof(Bool)}") + "\n" +
                        "Char: " + ByteHelper.LoadBytes<char>($"BaseType/{nameof(Char)}") + "\n" +
                        "Byte: " + ByteHelper.LoadBytes<byte>($"BaseType/{nameof(Byte)}") + "\n" +
                        "String: " + ByteHelper.LoadBytes<string>($"BaseType/{nameof(String)}");
        }
    }
}