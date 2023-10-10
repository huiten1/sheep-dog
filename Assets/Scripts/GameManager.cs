using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : Singleton<GameManager>
{
    public GameState State { get; private set; }
    public static event Action<GameState> OnStateChange;
    
    public GameData GameData
    {
        get;
        private set;
    }
    public IObjectPool<GameObject> FloatingTextPool { get; private set; }
    private void Awake()
    {
        LoadData();
        DontDestroyOnLoad(this);
        
    }
    public void LoadData()=> GameData = SaveManager.Load<GameData>();
    private void Start()
    {
        UpdateGameState(GameState.PlayerSelect);
        FloatingTextPool = new ObjectPool<GameObject>(
            ()=>{
                var go =Instantiate(Resources.Load<GameObject>("UI/UI_FLoatingText"));
                go.AddComponent<ReturnToPool>().pool = FloatingTextPool ;
                return go;
            },
            (go)=>go.gameObject.SetActive(true),
            (go)=>go.SetActive(false),
            (go)=>Destroy(go));
    }

    public void StartGame()
    {
        UpdateGameState(GameState.Started);
    }

    public void Complete()
    {
        UpdateGameState(GameState.Finished);
    }

    public void GameOver()
    {
        UpdateGameState(GameState.GameOver);
    }
    public void LoadMain()
    {
        SceneManager.LoadScene(0);
        UpdateGameState(GameState.PlayerSelect);
        FloatingTextPool.Clear();
    }
    public void UpdateGameState(GameState gameState)
    {
        if(gameState==State) return;
        State = gameState;
        
        OnStateChange?.Invoke(State);
    }
}

public enum GameState
{
    PlayerSelect,
    Started,
    Finished,
    GameOver,
}
