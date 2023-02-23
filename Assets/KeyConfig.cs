using System;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class KeyConfig : MonoBehaviour
    {
        const int KL_NAMELENGTH = 9;
        private TestActions _testActions;
        [SerializeField] private TextMeshProUGUI keyboardName;
        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(
            StringBuilder pwszKLID);
        private readonly StringBuilder _keyboardNameStringBuilder = new StringBuilder();
        private readonly StringBuilder _keyboardIdentifier = new StringBuilder();

        private void Awake()
        {
            _testActions = new TestActions();
            _testActions.KeyConfigTest.Enable();
        }

        private void Start()
        {
            Debug.Log("keyboard name:" + keyboardName);
            Console.WriteLine("keyboard name:" + keyboardName);
            _testActions.KeyConfigTest.Q.performed += _ =>
            {
                Debug.Log("down Q key");
                Debug.Log("Q key name: " + Keyboard.current.qKey.displayName);
            };

            _testActions.KeyConfigTest.SwitchJapanese.performed += _ =>
            {
                // Application.systemLanguage = SystemLanguage.Japanese;
                Debug.Log("down SwitchJapanese key");
            };

            _testActions.KeyConfigTest.SwitchFranch.performed += _ =>
            {
                Debug.Log("down SwitchFranch key");
            };
        }
        private void Update()
        {
            _keyboardIdentifier.Clear();
            _keyboardNameStringBuilder.Clear();
            GetKeyboardLayoutName(_keyboardIdentifier);
            _keyboardNameStringBuilder.Append("接続しているキーボード配列種類\n");
            _keyboardNameStringBuilder.Append($"{_keyboardIdentifier}\n");
            _keyboardNameStringBuilder.Append($"{GetKeyBoardEnum(_keyboardIdentifier.ToString())}");
            keyboardName.text = _keyboardNameStringBuilder.ToString();
        }

        private static string GetKeyBoardEnum(string keyboardIdentifier)
        {
            return keyboardIdentifier switch
            {
                "00000411" => "日本語配列",
                "0000040C" => "フランス語配列",
                _ => "不明"
            };
        }
    }
}