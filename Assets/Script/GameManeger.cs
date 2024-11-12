using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    int chaild;
    [SerializeField]
    GameObject[] kabidani;
    [SerializeField]
    int num;
    [SerializeField]
    public Vector3 spawnAreaLD;  
    [SerializeField]
    public Vector3 spawnAreaRU;  
    [SerializeField]
    float spawnInterval = 5.0f; // 時間間隔（秒）
    float timeElapsed = 0.0f; // 経過時間のカウント
    bool isTouching = false;
    [SerializeField]
    LayerMask floorLayer; 
    [SerializeField]
    int tapCount;
    [SerializeField]
    int tapCountMax;
    void Start()
    {
        chaild = this.transform.childCount;
    }

    void Update()
    {
        // マウスクリック時にレイキャストで床を判定
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // レイキャストが床レイヤーにヒットしたか確認
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
            {
                tapCount++;
                if(tapCount >= tapCountMax)
                {
                    Spawn();
                    tapCount = 0;
                }
            }
        }


        // 経過時間をカウント
        timeElapsed += Time.deltaTime;

        // 一定時間経過で生成
        if (timeElapsed >= spawnInterval)
        {
            Spawn();
            timeElapsed = 0.0f; // カウントをリセット
        }

        chaild = this.transform.childCount;
    }

    void Spawn()
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaRU.x, spawnAreaLD.x),
                1,
                Random.Range(spawnAreaRU.z, spawnAreaLD.z)
            );
            GameObject obj = Instantiate(kabidani[Random.Range(0, kabidani.Length)], randomPosition, Quaternion.identity);
            obj.transform.parent = this.transform;
        }
    }
}
