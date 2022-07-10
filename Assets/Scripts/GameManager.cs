using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score;

    private static GameManager instance;
    
    public static GameManager Instance
    {
        get
        {
            if(instance==null)
            {
                GameObject gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
            }

            return instance;
        }

    }

    private void Awake()
    {
        instance = this;
    }

}
