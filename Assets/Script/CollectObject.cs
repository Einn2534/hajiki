using UnityEngine;

public class CollectObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // �X�v���C�g���I�u�W�F�N�g�ɐG�ꂽ�ꍇ�̏���
        if (other.gameObject.CompareTag("Player"))
        {
            // 3D�I�u�W�F�N�g�����
            Debug.Log("�I�u�W�F�N�g���");
            Destroy(gameObject);
        }
    }
}
