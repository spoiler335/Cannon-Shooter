using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{

    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + GameManager.Instance.Score;
    }
}
