using UnityEngine;
using UnityEngine.UI;

public class FlickObject : MonoBehaviour
{
    Vector2 startPos; // フリックの開始位置
    Vector2 endPos;   // フリックの終了位置
    [SerializeField] bool isSwiping = false; // フリック中かどうかを判定するフラグ
    float minSwipeDistance = 50f; // フリックと認識する最小距離
    [SerializeField] float speed = 5f; // フリックした後飛んでく速度
    [SerializeField] float chargeTime; // 必要なチャージ時間
    [SerializeField] bool isCharge; // チャージ中かどうかを判定するフラグ
    [SerializeField] float chargeTimeStart; // チャージの開始時間
    [SerializeField] float chargeTimeEnd;   // チャージの終了時間
    [SerializeField] int nakami_num; // 生成するオブジェクトの数
    [SerializeField] float range; // オブジェクト生成の範囲
    [SerializeField] GameObject herbivore_prefab; // 生成するオブジェクトのプレハブ
    [SerializeField] Vector3 Limit_LD; // 生成範囲の左下限
    [SerializeField] Vector3 Limit_RU; // 生成範囲の右上限
    [SerializeField] Rigidbody rb; // Rigidbodyコンポーネント
    GameManeger gamemaneger; // GameManegerの参照
    public Slider sliderPrefab; // スライダーのプレハブ
    [SerializeField] ComboManager combo; // ComboManagerの参照

    private Slider sliderInstance; // スライダー
    private Canvas canvas; // Canvas

    [SerializeField] float moveSpead;//移動スピード
    private Vector3 randomDirection; // ランダムな方向
    private float changeDirectionInterval = 2.0f; // 方向を変える間隔
    private float timeSinceLastDirectionChange = 0f; // 前回の方向変更からの時間

    private void Start()
    {
        // 初期設定
        gamemaneger = GameObject.FindWithTag("GameController").GetComponent<GameManeger>();
        combo = GameObject.FindWithTag("GameController").GetComponent<ComboManager>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Limit_RU = gamemaneger.spawnAreaRU;
        Limit_LD = gamemaneger.spawnAreaLD;

        // スライダーの生成と初期化
        canvas = GameObject.FindObjectOfType<Canvas>();
        sliderInstance = Instantiate(sliderPrefab, canvas.transform);
        sliderInstance.value = 0;
        sliderInstance.gameObject.SetActive(false); // 初期状態で非表示

        SetRandomDirection(); // 初期のランダムな方向を設定
    }

    void Update()
    {
        // スライダーの位置を更新
        UpdateSliderPosition();

        // タッチの開始判定
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // チャージ開始
                    isCharge = true;
                    startPos = Input.mousePosition;
                    chargeTimeStart = Time.time;
                    sliderInstance.gameObject.SetActive(true); // スライダー表示
                }
            }
        }

        // チャージ処理
        if (isCharge)
        {
            if (Time.time - chargeTimeStart >= chargeTime)
            {
                isSwiping = true; // チャージ時間が経過したらフリック可能に
            }
            if (chargeTimeStart != 0)
            {
                chargeTimeEnd = Time.time - chargeTimeStart;
                sliderInstance.value = chargeTimeEnd / chargeTime; // スライダーの値を更新
            }
        }

        // タッチの終了判定
        if (Input.GetMouseButtonUp(0))
        {
            if (isSwiping)
            {
                endPos = Input.mousePosition;
                Vector2 swipeVector = endPos - startPos;

                if (swipeVector.magnitude > minSwipeDistance)
                {
                    MoveObject(swipeVector.normalized); // フリック方向にオブジェクトを移動
                }
            }
            isCharge = false;
            isSwiping = false;
            sliderInstance.value = 0;
            sliderInstance.gameObject.SetActive(false); // タッチ終了時にスライダーを非表示
            chargeTimeStart = 0;
            chargeTimeEnd = 0;
        }

        // ランダム方向への移動
        MoveRandomly();
        // 画面外チェック
        CheckIfOutOfView();
    }

    // オブジェクトを移動するメソッド
    void MoveObject(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y).normalized;

        if (rb != null)
        {
            for (int i = 0; i < nakami_num; i++)
            {
                float x = Random.Range(transform.position.x - range, transform.position.x + range);
                x = Mathf.Clamp(x, Limit_RU.x, Limit_LD.x);
                float z = Random.Range(transform.position.z - range, transform.position.z + range);
                z = Mathf.Clamp(z, Limit_RU.z, Limit_LD.z);
                GameObject obj = Instantiate(herbivore_prefab, new Vector3(x, transform.position.y, z), Quaternion.identity);
                obj.transform.parent = transform.parent;
            }
            rb.velocity = moveDirection * speed; // 指定した方向に速度を与える
        }
    }

    // 画面外に出たかを確認するメソッド
    void CheckIfOutOfView()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            combo.ComboPlus(); // コンボを増加
            Destroy(gameObject); // 自分自身を削除
            Destroy(sliderInstance.gameObject); // スライダーも削除
        }
    }

    // スライダーの位置を更新するメソッド
    void UpdateSliderPosition()
    {
        if (sliderInstance != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.forward * 1.5f); // オブジェクトの上方に配置
            sliderInstance.transform.position = screenPosition;
        }
    }

        void SetRandomDirection()
    {
        // ランダムな2D方向を3Dベクトルに変換
        float angle = Random.Range(0f, 360f);
        randomDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    void MoveRandomly()
    {
        // 方向を定期的に変更
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            SetRandomDirection();
            timeSinceLastDirectionChange = 0f;
        }

        // ランダムな方向にゆっくりと移動
        rb.velocity = randomDirection * (moveSpead); // ゆっくり動かす
    }

}
