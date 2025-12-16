using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(Play);
    }

    public void Play()
    {
        SceneController.Instance.LoadScene(ESceneName.Level_1);
    }
}
