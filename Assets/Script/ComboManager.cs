using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboManager : MonoBehaviour
{
    [SerializeField]
    int Combo;
    [SerializeField]
    GameObject comboText;
    // Start is called before the first frame update
    void Start()
    {
        Combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        comboText.GetComponent<Text>().text = Combo.ToString() + "Combo";
    }

    public void ConboPlus()
    {
        Combo++;
    }
}
