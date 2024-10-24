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
    Vector3 spawnAreaMin;  // �X�|�[���ʒu�̍ŏ��͈�
    [SerializeField]
    Vector3 spawnAreaMax;  // �X�|�[���ʒu�̍ő�͈�

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
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    1,
                    Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                );

                // kabidani�������_���Ȉʒu�ɐ���
                GameObject obj = Instantiate(kabidani, randomPosition, Quaternion.identity);
                obj.transform.parent = this.transform;
            }
        }
    }
}
