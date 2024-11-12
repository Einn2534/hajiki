using UnityEngine;
using UnityEngine.UI;

public class FlickObject : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;
    [SerializeField] bool isSwiping = false;
    float minSwipeDistance = 50f; // フリックと認識する最小距離
    [SerializeField] float speed = 5f; // オブジェクトの速度
    [SerializeField] float chargeTime; // 何秒ドラッグするか
    [SerializeField] bool isCharge;
    [SerializeField] float chargeTimeStart;
    [SerializeField] float chargeTimeEnd;
    [SerializeField] int nakami_num;
    [SerializeField] float range;
    [SerializeField] GameObject herbivore_prefab;
    [SerializeField] Vector3 Limit_LD;
    [SerializeField] Vector3 Limit_RU;
    [SerializeField] Rigidbody rb;
    GameManeger gamemaneger;
    public Slider sliderPrefab; // スライダープレハブをアタッチ
    [SerializeField]
    ComboManager combo;

    private Slider sliderInstance; // スライダーのインスタンス
    private Canvas canvas; // Canvasの参照

    private void Start()
    {
        gamemaneger = GameObject.FindWithTag("GameController").GetComponent<GameManeger>();
        combo = GameObject.FindWithTag("GameController").GetComponent<ComboManager>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Limit_RU = gamemaneger.spawnAreaRU;
        Limit_LD = gamemaneger.spawnAreaLD;

        // スライダーのインスタンスを生成
        canvas = GameObject.FindObjectOfType<Canvas>(); // Canvasを取得
        sliderInstance = Instantiate(sliderPrefab, canvas.transform); // Canvasの子として生成
        sliderInstance.value = 0;
        sliderInstance.gameObject.SetActive(false); // 初期状態で非表示
    }

    void Update()
    {
        // スライダーの位置を更新
        UpdateSliderPosition();

        // タッチの開始
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isCharge = true;
                    startPos = Input.mousePosition;
                    chargeTimeStart = Time.time;
                    sliderInstance.gameObject.SetActive(true); // タッチ時にスライダーを表示
                }
            }
        }

        // チャージ処理
        if (isCharge)
        {
            if (Time.time - chargeTimeStart >= chargeTime)
            {
                isSwiping = true;
            }
            if (chargeTimeStart != 0)
            {
                chargeTimeEnd = Time.time - chargeTimeStart;
                sliderInstance.value = chargeTimeEnd / chargeTime; // スライダーの値を更新
            }
        }

        // タッチの終了
        if (Input.GetMouseButtonUp(0))
        {
            if (isSwiping)
            {
                endPos = Input.mousePosition;
                Vector2 swipeVector = endPos - startPos;

                if (swipeVector.magnitude > minSwipeDistance)
                {
                    MoveObject(swipeVector.normalized);
                }
            }
            isCharge = false;
            isSwiping = false;
            sliderInstance.value = 0;
            sliderInstance.gameObject.SetActive(false); // タッチ終了時にスライダーを非表示
            chargeTimeStart = 0;
            chargeTimeEnd = 0;
        }

        CheckIfOutOfView();
    }

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
            rb.velocity = moveDirection * speed;
        }
    }

    void CheckIfOutOfView()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            combo.ConboPlus();
            Destroy(gameObject);
            Destroy(sliderInstance.gameObject); // オブジェクトが削除されたらスライダーも削除
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
}
