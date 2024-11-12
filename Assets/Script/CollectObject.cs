using UnityEngine;

public class CollectObject : MonoBehaviour
{
    [SerializeField] ScoreManager score;
    void Start()
    {
        score = GameObject.FindWithTag("GameController").GetComponent<ScoreManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        // プレイヤーオブジェクトに接触したときの処理
        if (other.gameObject.CompareTag("Player"))
        {
            // このオブジェクトを削除
            Destroy(gameObject);
            score.ScorePlus(50);
        }
    }
}
