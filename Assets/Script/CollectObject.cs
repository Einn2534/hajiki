using UnityEngine;

public class CollectObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // スプライトがオブジェクトに触れた場合の処理
        if (other.gameObject.CompareTag("Player"))
        {
            // 3Dオブジェクトを回収
            Debug.Log("オブジェクト回収");
            Destroy(gameObject);
        }
    }
}
