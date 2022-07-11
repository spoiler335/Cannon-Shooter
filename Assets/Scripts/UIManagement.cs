using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    float currentTime = 0;
    [SerializeField] float stratingTime = 10f;
    [SerializeField] Text scoreText;
    [SerializeField] Text countDownText;
    [SerializeField] GameObject gameOverPanel;

    public static bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = stratingTime;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + GameManager.Instance.Score;
        currentTime -= Time.deltaTime;
        countDownText.text = "Remaining Time : " + currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
            isGameOver = true;
            gameOverPanel.SetActive(true);
        }

    }
}
