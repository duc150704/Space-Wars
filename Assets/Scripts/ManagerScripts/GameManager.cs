using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    ShipController _player;
    BossController _boss;

    [SerializeField] GameObject _mousePref;
    [SerializeField] GameObject _bonus;
    [SerializeField] GameObject _playerPref;
    [SerializeField] Vector3 _playerSpawnPosition;
    public Vector3 PlayerSpawnPosition 
    { 
        get => _playerSpawnPosition; 
    }
    public static event Action<GameState> OnChangedState;
    private GameState _currentState;
    public GameState CurrentState => _currentState;
    public enum GameState
    {
        None,
        Menu,
        Shop,
        Playing,
        //Pause,
        Win,
        Lose
    }



    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterPlayer(ShipController controller)
    {
        _player = controller;
    }

    public void RegisterBoss(BossController boss)
    {
        _boss = boss;
    }

    public Vector3 GetPlayerPosition()
    {
        if (_player == null)
            return Vector3.zero;
        return _player.Transform.position;
    }

    public void ChangeState(GameState gameState)
    {
        if (_currentState == gameState)
            return;
        _currentState = gameState;
        switch (_currentState) 
        {
            case GameState.Menu:
                SoundsManager.PlaySound(ESoundType.BgmMenu, true);
                break;
            case GameState.Shop:
                break;
            case GameState.Playing:
                
                SoundsManager.PlaySound(ESoundType.BgmGamePlay, true);
                Cursor.visible = false;
                Instantiate(_playerPref, PlayerSpawnPosition, Quaternion.identity);
                Instantiate(_mousePref, InputManager.Instance.MousePositon(), Quaternion.identity);
                break;
            //case GameState.Pause://
            //    break;
            case GameState.Win:
                StartCoroutine(GameWin());
                break;
            case GameState.Lose:
                StartCoroutine(GameLose());
                break;
        }
        OnChangedState?.Invoke(_currentState);
    }

    private IEnumerator GameWin()
    {
        SoundsManager.Instance.StopBgm();
        yield return null;
        SceneController.Instance.LoadScene(ESceneName.Menu);
    }
 
    private IEnumerator GameLose()
    {
        SoundsManager.Instance.StopBgm();
        UIManager.Instance.ShowWaveName("GAME OVER", 3f);
        yield return new WaitForSeconds(5f);
        SceneController.Instance.LoadScene(ESceneName.Menu);
    }

}
