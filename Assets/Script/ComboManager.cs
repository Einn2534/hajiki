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
    [SerializeField]
    float comboTimer;
    [SerializeField]
    float comboTimerLimit = 2.0f; // コンボのリセット時間を設定

    // Start is called before the first frame update
    void Start()
    {
        Combo = 0;
        comboTimer = comboTimerLimit; // 初期化
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間を減少させる
        comboTimer -= Time.deltaTime;

        // テキストの更新
        comboText.GetComponent<Text>().text = Combo.ToString() + " Combo";

        // コンボタイマーがリミットを超えたらコンボをリセット
        if (comboTimer <= 0)
        {
            Combo = 0;
            comboTimer = comboTimerLimit; // タイマーをリセット
        }
    }

    // コンボを増加させるメソッド
    public void ComboPlus()
    {
        Combo++;
        comboTimer = comboTimerLimit; // タイマーをリセット
    }
}

