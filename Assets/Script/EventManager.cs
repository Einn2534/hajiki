using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // イベント発生時間のリスト
    private readonly float[] eventTimes = { 40f, 60f, 80f, 100f, 120f, 140f, 160f, 180f };

    // ギミックの種類の数
    private const int GimmickCount = 3;  // 3種類のギミック

    // 各ギミックの発生回数のリスト
    private Dictionary<int, int> gimmickCount = new Dictionary<int, int>();

    // 直前に発生したギミックIDを記録
    private int previousGimmick = -1;

    void Start()
    {
        // ギミック発生回数の初期化
        for (int i = 0; i < GimmickCount; i++)
        {
            gimmickCount[i] = 0;
        }
        
        // イベントを順番に開始
        StartCoroutine(StartEvents());
    }

    IEnumerator StartEvents()
    {
        foreach (float time in eventTimes)
        {
            // 指定した秒数待機
            yield return new WaitForSeconds(time);
            
            // ギミックを発生させる
            int gimmickID = GetRandomGimmick();
            ActivateGimmick(gimmickID);
        }
    }

    int GetRandomGimmick()
    {
        List<int> possibleGimmicks = new List<int>();

        // ギミックの発生回数が2回未満のギミックを選択肢に追加
        for (int i = 0; i < GimmickCount; i++)
        {
            if (gimmickCount[i] < 2 && i != previousGimmick)
            {
                possibleGimmicks.Add(i);
            }
        }

        // ランダムでギミックを選ぶ
        int selectedGimmick = possibleGimmicks[Random.Range(0, possibleGimmicks.Count)];

        // 発生回数と前回ギミックを更新
        gimmickCount[selectedGimmick]++;
        previousGimmick = selectedGimmick;

        return selectedGimmick;
    }

    void ActivateGimmick(int gimmickID)
    {
    
        Debug.Log("Gimmick " + gimmickID + " を発生させました。");
        
        // ギミックの処理をここで実装する
        
    }
}
