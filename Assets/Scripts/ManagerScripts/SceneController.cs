using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneName
{
    Menu,
    Level_1
}
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField] Animator _animator;

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

    IEnumerator LoadScene_IE(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while(operation.progress < 0.9f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        operation.allowSceneActivation = true;
    }
}
