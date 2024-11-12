using UnityEngine;

public class DragObjectTopDown : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    // オブジェクトがクリックされたときに呼び出される
    void OnMouseDown()
    {
        // オブジェクトの位置をスクリーン座標に変換し、z座標を取得
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // マウス位置とオブジェクト位置の差分を計算してオフセットとして保持
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    // オブジェクトがドラッグされている間、毎フレーム呼び出される
    void OnMouseDrag()
    {
        // マウスの移動に合わせてオブジェクトの位置を更新（y座標は固定）
        Vector3 newPosition = GetMouseWorldPos() + offset;
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }

    // マウス位置をワールド座標に変換して返す
    private Vector3 GetMouseWorldPos()
    {
        // マウス位置（スクリーン座標）を取得
        Vector3 mousePoint = Input.mousePosition;

        // z座標を設定してワールド座標の計算に使用
        mousePoint.z = zCoord;

        // スクリーン座標をワールド座標に変換して返す
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
