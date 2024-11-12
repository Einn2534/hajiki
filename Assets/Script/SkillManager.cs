using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    int SkillGauge;
    [SerializeField]
    GameObject SkillGaugeObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // コンボを増加させるメソッド
    public void ComboPlus()
    {
        SkillGauge++;
    }
}

