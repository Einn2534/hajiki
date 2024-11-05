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


    void Start()
    {
        chaild = this.transform.childCount;
    }

   
    void Update()
    {
        chaild = this.transform.childCount;
        if (chaild == 0)
        {
            
        }
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
