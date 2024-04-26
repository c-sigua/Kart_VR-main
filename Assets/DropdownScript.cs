using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DropdownScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    public static string userGender = "Non-Binary";
    // Start is called before the first frame update
    void Start()
    {

        _dropdown.onValueChanged.AddListener((v) =>
        {
            //userGender = _dropdown.itemText.ToString();
            userGender = _dropdown.options[_dropdown.value].text;

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
