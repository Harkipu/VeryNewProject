using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        
    }

    public float currentScore = 0f;

    public Data data;

    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();

    public UnityEvent onGameOver = new UnityEvent();

    private void Start()
    {
        string loadedData = SaveLoad.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<Data>(loadedData);
        }
        else
        {
            data = new Data();
        }
    }


    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }

    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
    }

    public void GameOver()
    {
        
        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;
            string saveString = JsonUtility.ToJson(data);
            SaveLoad.Save("save", saveString);
        }
        onGameOver.Invoke();
        isPlaying = false;
    }

    public string BetterScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string BetterhighScore()
    {
        return Mathf.RoundToInt(data.highscore).ToString();
    }
}
