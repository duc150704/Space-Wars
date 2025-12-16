using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 oldPos;
    [SerializeField] float _zoomOutSize;
    [SerializeField] float _zoomInSize;
    [SerializeField] float _zoomTime;
    private void Start()
    {
        oldPos = Camera.main.transform.position;
        EventManager.Subscribe(EEventType.StartPlaying, ZoomIn);
        EventManager.Subscribe(EEventType.BossAppear, ZoomOut);
        EventManager.Subscribe(EEventType.PlayerDead, Shake);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EEventType.StartPlaying, ZoomIn);
        EventManager.Unsubscribe(EEventType.BossAppear, ZoomOut);
        EventManager.Unsubscribe(EEventType.PlayerDead, Shake);
    }
    public void Shake()
    {
        StartCoroutine(Shake_IE());
    }
    IEnumerator Shake_IE()
    {
        float time = 0.5f;
        float magnitude = 0.3f;
        Vector3 originalPos = Camera.main.transform.position;
        float timeCounter = 0f;

        while (timeCounter < time)
        {
            float strength = magnitude * (1f - (timeCounter / time)); 

            Vector2 randomOffset = Random.insideUnitCircle * strength;

            Camera.main.transform.position = originalPos + new Vector3(randomOffset.x, randomOffset.y, 0);

            timeCounter += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPos;
    }

    public void ZoomOut()
    {
        if(gameObject)
        StartCoroutine(ZoomOut_IE());
    }
    public void ZoomIn()
    {
        if(gameObject)
        StartCoroutine(ZoomIn_IE());
    }
    IEnumerator ZoomOut_IE()
    {
        float s = Camera.main.orthographicSize;
        float timeCounter = 0;
        while(timeCounter <= _zoomTime)
        {
            float tmp = Mathf.Lerp(s, _zoomOutSize, timeCounter / _zoomTime);
            Camera.main.orthographicSize = tmp;
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator ZoomIn_IE()
    {
        float s = Camera.main.orthographicSize;
        float timeCounter = 0;
        while(timeCounter <= _zoomTime)
        {
            float tmp = Mathf.Lerp(s, _zoomInSize , timeCounter / _zoomTime);
            Camera.main.orthographicSize = tmp;
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
}
