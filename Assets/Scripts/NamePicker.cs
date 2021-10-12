using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePicker : MonoBehaviour
{
    public InputField nameInput;
    public string playerName;

    void Start()
    {
        nameInput = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getInput()
    {
        playerName = nameInput.text;
    }
}
