using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    float Score;
    [SerializeField]
    GameObject ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.GetComponent<Text>().text = "Sucore" + Score.ToString();
    }

    public void ScorePlus(int plus)
    {
        Score += plus;
    }
}
