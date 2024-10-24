using UnityEngine;

public class FlickScript : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    bool isSwiping = false;
    float minSwipeDistance = 50f; // フリックと認識する最小距離
    [SerializeField]
    float speed = 5f; // オブジェクトの速度
    [SerializeField]
    int nakami_num;
    [SerializeField]
    float range;
    [SerializeField]
    GameObject herbivore_prefab;
    [SerializeField]
    Vector3 LD_Limit;
    [SerializeField]
    Vector3 RU_Limit;
    [SerializeField]
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
    }
    void Update()
    {
        // タッチの開始
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // オブジェクトがタッチされたかどうかを判定
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isSwiping = true;
                    startPos = Input.mousePosition;
                }
            }
        }

        // タッチの終了
        if (Input.GetMouseButtonUp(0))
        {
            if (isSwiping)
            {
                endPos = Input.mousePosition;
                Vector2 swipeVector = endPos - startPos;

                // フリックと認識するか確認
                if (swipeVector.magnitude > minSwipeDistance)
                {
                    // スプライトを移動
                    MoveObject(swipeVector.normalized);
                }
            }
            isSwiping = false;
        }

        // 画面外に出たらオブジェクトを削除
        CheckIfOutOfView();
    }

    void MoveObject(Vector2 direction)
    {
        float speed = 5f; // オブジェクトの速度

        // フリック方向をカメラの上から見たX-Z平面に変換
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

        // 移動方向を正規化
        moveDirection = moveDirection.normalized;

        // Rigidbodyを使用してオブジェクトを移動
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            for (int i = 0; i < nakami_num; i++)
            {

                float x = Random.Range(transform.position.x - range, transform.position.x + range);
                x = Mathf.Clamp(x, RU_Limit.x, LD_Limit.x);
                float z = Random.Range(transform.position.z - range, transform.position.z + range);
                z = Mathf.Clamp(z, RU_Limit.z, LD_Limit.z);
                GameObject obj = Instantiate(herbivore_prefab, new Vector3(x, transform.position.y, z), Quaternion.identity);
                obj.transform.parent = transform.parent;
            }
            rb.velocity = moveDirection * speed;
        }
    }

    void CheckIfOutOfView()
    {
        // オブジェクトのワールド座標をカメラのビューポート座標に変換
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // ビューポート座標がカメラの視界外かどうかを判定（0〜1の範囲外なら視界外）
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            // 画面外に出たのでオブジェクトを削除
            Destroy(gameObject);
        }
    }
}