using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    // InputFieldのText参照用
    public InputField Field;

    // Start と Update は省略

    // InputFieldの文字が変更されたらコールバックされる
    public void OnValueChanged()
    {
        string input = Field.GetComponent<InputField>().text;
        GetComponent<TMP_Text>().text = input;
    }
}