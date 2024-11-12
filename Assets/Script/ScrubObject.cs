using UnityEngine;

public class ScrubObject : MonoBehaviour
{
    Vector2 previousTouchPos;
    bool isTouching = false;
    bool isTargetObject = false; // タッチ対象かどうか
    [SerializeField] float shrinkRate = 0.1f; // 一度のスワイプで縮小する割合
    [SerializeField] float growRate = 0.01f; // 時間経過で拡大する割合
    [SerializeField] float minSwipeDistance = 10f; // 縮小判定とする最小移動距離
    [SerializeField] float minScale = 0.1f; // 最小スケールサイズ
    [SerializeField] float maxScale = 2.0f; // 最大スケールサイズ
    [SerializeField] ComboManager combo;
    [SerializeField] ScoreManager score;

    void Start()
    {
        combo = GameObject.FindWithTag("GameController").GetComponent<ComboManager>();
        score = GameObject.FindWithTag("GameController").GetComponent<ScoreManager>();
    }

    void Update()
    {
        // 時間経過で少しずつ拡大する処理
        GrowObjectOverTime();

        // タッチの開始
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycastでタッチしたオブジェクトが自分自身かどうかを確認
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isTouching = true;
                    isTargetObject = true; // タッチ対象として認識
                    previousTouchPos = Input.mousePosition;
                }
            }
        }

        // タッチ中の処理
        if (isTouching && isTargetObject && Input.GetMouseButton(0))
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
            isTargetObject = false; // タッチ対象リセット
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
            combo.ComboPlus();
            score.ScorePlus(100);
            Destroy(this.gameObject);
        }
    }

    // 時間経過でオブジェクトを拡大するメソッド
    void GrowObjectOverTime()
    {
        Vector3 newScale = transform.localScale * (1f + growRate * Time.deltaTime);

        // スケールが最大値を超えないように制限
        if (newScale.x < maxScale && newScale.y < maxScale && newScale.z < maxScale)
        {
            transform.localScale = newScale;
        }
    }
}
