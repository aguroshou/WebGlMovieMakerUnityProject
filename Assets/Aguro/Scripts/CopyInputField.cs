using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CopyInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField _tmpInputField;

    [SerializeField] private TMP_Text _tmpText;
    // Update is called once per frame
    void Update()
    {
        _tmpText.text = _tmpInputField.text;
    }
}
