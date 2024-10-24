using UnityEngine;

public class DragObjectTopDown : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    // �I�u�W�F�N�g���N���b�N���ꂽ�Ƃ��ɌĂ΂��
    void OnMouseDown()
    {
        // �I�u�W�F�N�g��z���W���L���i�J��������̋����j
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // �}�E�X�ƃI�u�W�F�N�g�̈ʒu�������v�Z
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    // �I�u�W�F�N�g���h���b�O����Ă���Ƃ��ɌĂ΂��
    void OnMouseDrag()
    {
        // �}�E�X�̓����Ɋ�Â��ăI�u�W�F�N�g�𓮂���
        Vector3 newPosition = GetMouseWorldPos() + offset;
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }

    // �}�E�X�̃��[���h���W���擾����
    private Vector3 GetMouseWorldPos()
    {
        // �}�E�X�̃X�N���[�����W���擾
        Vector3 mousePoint = Input.mousePosition;

        // z���W��ݒ�
        mousePoint.z = zCoord;

        // �X�N���[�����W�����[���h���W�ɕϊ����ĕԂ�
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
