using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider _bossHealthSlider;
    [SerializeField] TextMeshProUGUI _playerLives;

    private void Start()
    {
        BossController.OnHealthChanged += UpdateHealthBar;
        GameManager.PlayerLiveRemaining += UpdatePlayerLive;

        EventManager.Subscribe(EEventType.BossAppear, FadeIn);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEventType.BossAppear, FadeIn);

        GameManager.PlayerLiveRemaining -= UpdatePlayerLive;
        BossController.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdatePlayerLive(int live)
    {
        _playerLives.text = live.ToString();
    }
    private void UpdateHealthBar(float current, float max)
    {
        _bossHealthSlider.value = current / max;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn_IE());
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOut_IE());
    }

    IEnumerator FadeIn_IE()
    {
        float time = 2f;
        float timeCounter = 0;
        var alpha = _bossHealthSlider.GetComponent<CanvasGroup>();
        while (timeCounter <= time)
        {
            alpha.alpha = Mathf.Lerp(0f, 1f, timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator FadeOut_IE()
    {
        float time = 2f;
        float timeCounter = 0;
        var alpha = _bossHealthSlider.GetComponent<CanvasGroup>();
        while (timeCounter <= time)
        {
            alpha.alpha = Mathf.Lerp(1f, 0f, timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }

}
