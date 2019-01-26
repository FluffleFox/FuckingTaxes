using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Budget : MonoBehaviour {

    public Text Present;
    int Value;

    void ChangeValue(int delta)
    {
        Value += delta;
        Present.text = Value.ToString();
    }
}
