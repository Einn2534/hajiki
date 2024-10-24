using UnityEngine;

public class DragObjectTopDown : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    // オブジェクトがクリックされたときに呼ばれる
    void OnMouseDown()
    {
        // オブジェクトのz座標を記憶（カメラからの距離）
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // マウスとオブジェクトの位置差分を計算
        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    // オブジェクトがドラッグされているときに呼ばれる
    void OnMouseDrag()
    {
        // マウスの動きに基づいてオブジェクトを動かす
        Vector3 newPosition = GetMouseWorldPos() + offset;
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }

    // マウスのワールド座標を取得する
    private Vector3 GetMouseWorldPos()
    {
        // マウスのスクリーン座標を取得
        Vector3 mousePoint = Input.mousePosition;

        // z座標を設定
        mousePoint.z = zCoord;

        // スクリーン座標をワールド座標に変換して返す
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
