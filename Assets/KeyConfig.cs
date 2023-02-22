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
        [SerializeField] private TextMeshProUGUI beforeKeyConfigKey;
        [SerializeField] private TextMeshProUGUI afterKeyConfigKey;
        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(
            StringBuilder pwszKLID);

        private void Awake()
        {
            _testActions = new TestActions();
            _testActions.KeyConfigTest.Enable();
        }

        private void Start()
        {
            var keyboardName = new StringBuilder();

            GetKeyboardLayoutName(keyboardName);

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
    }
}