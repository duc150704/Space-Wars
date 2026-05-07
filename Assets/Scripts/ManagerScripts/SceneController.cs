using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum ESceneName
{
    Menu,
    Level_1
}
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] GameObject _cover;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //_animator = GetComponentInChildren<Animator>();
    }

    public void LoadScene(ESceneName sceneName)
    {
        StartCoroutine(LoadScene_IE(sceneName.ToString()));
    }
    public void LoadScene(ESceneName sceneName, Action onComplete)
    {
        StartCoroutine(LoadScene_IE(sceneName.ToString(), onComplete));
    }

    IEnumerator LoadScene_IE(string sceneName, Action onComplete = null)
    {
        _cover.transform.DOScale(new Vector3(60, 60, 1), 1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while(operation.progress < 0.9f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        onComplete?.Invoke();

        _cover.transform.DOScale(Vector3.zero, 1f);
    }
}
