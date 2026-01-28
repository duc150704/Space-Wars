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
    [SerializeField] TextMeshProUGUI _waveNameTitle;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ShipAttack.OnGunPowerChanged += UpdatePlayerGunPower;
        ShipHealth.OnLivesChanged += UpdatePlayerLive;
        BossController.OnHealthChanged += UpdateHealthBar;
        EventManager.Subscribe(EEvent.OnBossAppear, FadeIn);
        
    }

    private void OnDisable()
    {
        ShipAttack.OnGunPowerChanged += UpdatePlayerGunPower;
        ShipHealth.OnLivesChanged += UpdatePlayerLive;
        EventManager.Unsubscribe(EEvent.OnBossAppear, FadeIn);
        BossController.OnHealthChanged -= UpdateHealthBar;
    }

    private void Start()
    {
        _waveNameTitle.alpha = 0f;
    }

    public void ShowWaveName(string waveName, float time = 1f)
    {
        StartCoroutine(ShowWaveNameIE(waveName, time));
    }

    private IEnumerator ShowWaveNameIE(string name, float time)
    {
        time = time / 3;
        _waveNameTitle.text = name;
        yield return Fade(time, false);
        yield return new WaitForSeconds(time + 1);
        yield return Fade(time, true);
    }

    private void UpdatePlayerLive(int live)
    {
        _playerLives.text = live.ToString();
    }
    private void UpdatePlayerGunPower(int gunPower)
    {
        _playerGunPower.text = gunPower.ToString();
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

    IEnumerator Fade(float time, bool reverse)
    {
        float timeCounter = 0;
        while (timeCounter <= time)
        {
            _waveNameTitle.alpha = Mathf.Lerp((reverse) ? 1f : 0f, (reverse) ? 0f : 1f, timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }

}
