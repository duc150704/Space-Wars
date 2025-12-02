using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static Vector3 oldPos;
    private void Start()
    {
        oldPos = Camera.main.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Shake());
        }
    }
    public static IEnumerator Shake(float time = 0.5f, float magnitude = 0.3f)
    {
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


    public static IEnumerator ZoomOut(float time, float size)
    {
        float s = Camera.main.orthographicSize;
        float timeCounter = 0;
        while(timeCounter <= time)
        {
            float tmp = Mathf.Lerp(s, size, timeCounter / time);
            Camera.main.orthographicSize = tmp;
            timeCounter += Time.deltaTime;
            yield return null;
        }
    }
}
