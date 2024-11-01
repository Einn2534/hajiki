using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    int chaild;
    [SerializeField]
    GameObject kabidani;
    [SerializeField]
    int num;
    [SerializeField]
    public Vector3 spawnAreaLD;  // スポーン位置の範囲左下
    [SerializeField]
    public Vector3 spawnAreaRU;  // スポーン位置の範囲右上

    // Start is called before the first frame update
    void Start()
    {
        chaild = this.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        chaild = this.transform.childCount;
        if (chaild == 0)
        {
            for (int i = 0; i < num; i++)
            {
                // ランダムな位置を生成
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaRU.x, spawnAreaLD.x),
                    1,
                    Random.Range(spawnAreaRU.z, spawnAreaLD.z)
                );

                // kabidaniをランダムな位置に生成
                GameObject obj = Instantiate(kabidani, randomPosition, Quaternion.identity);
                obj.transform.parent = this.transform;
            }
        }
    }
}
