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
    public Vector3 spawnAreaLD;  // �X�|�[���ʒu�͈͍̔���
    [SerializeField]
    public Vector3 spawnAreaRU;  // �X�|�[���ʒu�͈͉̔E��

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
                // �����_���Ȉʒu�𐶐�
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaRU.x, spawnAreaLD.x),
                    1,
                    Random.Range(spawnAreaRU.z, spawnAreaLD.z)
                );

                // kabidani�������_���Ȉʒu�ɐ���
                GameObject obj = Instantiate(kabidani, randomPosition, Quaternion.identity);
                obj.transform.parent = this.transform;
            }
        }
    }
}
