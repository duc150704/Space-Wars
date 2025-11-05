using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI a;
    // Start is called before the first frame update
    void Start()
    {
        a.text = "ihfgoiuhgg";
        StartCoroutine(f());
    }

    IEnumerator f()
    {
        yield return new WaitForSeconds(2f);
        a.gameObject.SetActive(true);
    }
}
