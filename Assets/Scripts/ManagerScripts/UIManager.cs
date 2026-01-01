using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider _bossHealthSlider;
    [SerializeField] TextMeshProUGUI _playerLives;
    [SerializeField] TextMeshProUGUI _playerGunPower;
    private void Start()
    {
        BossController.OnHealthChanged += UpdateHealthBar;
        ShipController.OnPlayerInfoChanged += UpdatePlayerInfoUI;
        EventManager.Subscribe(EEventType.BossAppear, FadeIn);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEventType.BossAppear, FadeIn);
        ShipController.OnPlayerInfoChanged -= UpdatePlayerInfoUI;
        BossController.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdatePlayerInfoUI(PlayerInforData data)
    {
        _playerLives.text = data.LivesRemaining?.ToString() ?? _playerLives.text;
        _playerGunPower.text = data.GunPower?.ToString() ?? _playerGunPower.text;
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
