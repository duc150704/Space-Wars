using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject _mousePref;
    GameObject _mouse;
    public static event Action<GameState> OnChangedState;
    public static event Action<int> PlayerLiveRemaining;
    public GameState _currentState;
    public enum GameState
    {
        Menu,
        Shop,
        Playing,
        //Pause,
        Win,
        Lose
    }


    [SerializeField] Vector3 _playerSpawnPosition;
    [SerializeField] GameObject _player;
    int _playerLives = 2;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate GameManager detected! Destroying...");
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    private void Start()
    {

        _mouse = Instantiate(_mousePref, InputManager.Instance.GetMousePositon(), Quaternion.identity);
        EventManager.Subscribe(EEventType.PlayerDead, OnPlayerRespawn);
        EventManager.Subscribe(EEventType.PlayerRespawn, RespawnPlayer);

        OnGameStart();
        ChangeState(GameState.Playing);
        Debug.Log("vairga");
        PlayerLiveRemaining?.Invoke(_playerLives);
    }

    private void Update()
    {
        _mouse.transform.position = InputManager.Instance.GetMousePositon();
    }
    private void OnDestroy()
    {

        EventManager.Unsubscribe(EEventType.PlayerDead, OnPlayerRespawn);
        EventManager.Unsubscribe(EEventType.PlayerRespawn, RespawnPlayer);

    }

    public void ChangeState(GameState gameState)
    {
        if (_currentState == gameState)
            return;
        _currentState = gameState;
        switch (_currentState) 
        {
            case GameState.Menu:
                break;
            case GameState.Shop:
                break;
            case GameState.Playing:
                SoundsManager.PlaySound(ESoundType.BgmGamePlay, true);
                break;
            //case GameState.Pause:
            //    break;
            case GameState.Win:
                Debug.Log("Win");
                SceneController.Instance.LoadScene(ESceneName.Menu);
                break;
            case GameState.Lose:
                SceneController.Instance.LoadScene(ESceneName.Menu);
                break;
        }
        OnChangedState?.Invoke(_currentState);
    }



    void OnGameOver()
    {
        ChangeState(GameState.Lose);
        Debug.Log("Game Over!");
        return;
    }

    void RespawnPlayer() => OnGameStart();
    void OnGameStart()
    {
        Instantiate(_player, _playerSpawnPosition, Quaternion.identity);
    }

    private void OnPlayerRespawn()
    {
       if (_playerLives <= 0)
        {
            OnGameOver();
            return;
        }
        _playerLives--;
        PlayerLiveRemaining?.Invoke(_playerLives);
        EventManager.Notify(EEventType.PlayerRespawn);  
    }

    

}
