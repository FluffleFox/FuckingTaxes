using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Budget : MonoBehaviour {

    public Text Present;
    public int Value;

    private void Start()
    {
        Value = int.Parse(Present.text);
    }

    void ChangeValue(int delta)
    {
        Value += delta;
        Present.text = Value.ToString();
    }
}
