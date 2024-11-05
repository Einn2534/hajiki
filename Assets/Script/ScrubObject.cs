using UnityEngine;

public class ShrinkOnSwipe : MonoBehaviour
{
    Vector2 previousTouchPos;
    bool isTouching = false;
    [SerializeField] float shrinkRate = 0.1f; // 一度のスワイプで縮小する割合
    [SerializeField] float minSwipeDistance = 10f; // 縮小判定とする最小移動距離
    [SerializeField] float minScale = 0.1f; // 最小スケールサイズ

    void Update()
    {
        // タッチの開始
        if (Input.GetMouseButtonDown(0))
        {
            previousTouchPos = Input.mousePosition;
            isTouching = true;
        }

        // タッチ中の処理
        if (isTouching && Input.GetMouseButton(0))
        {
            Vector2 currentTouchPos = Input.mousePosition;
            float swipeDistance = Vector2.Distance(currentTouchPos, previousTouchPos);

            // 指が一定距離以上移動したら縮小処理を行う
            if (swipeDistance > minSwipeDistance)
            {
                ShrinkObject();
                previousTouchPos = currentTouchPos; // 移動距離をリセット
            }
        }

        // タッチの終了
        if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
        }
    }

    // オブジェクトを縮小するメソッド
    void ShrinkObject()
    {
        Vector3 newScale = transform.localScale * (1f - shrinkRate);

        // スケールが最小値以下にならないように制限
        if (newScale.x > minScale && newScale.y > minScale && newScale.z > minScale)
        {
            transform.localScale = newScale;
        }
        else
        {
            Destroy(this.gameObject);
            transform.localScale = new Vector3(minScale, minScale, minScale);
        }
    }
}
