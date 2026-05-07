using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public Image Image;
    public Button UpgradeButton;

    public GameObject Progress;
    public GameObject ProgressBlock;

    public int MaxLevel;
    public int CurrentLevel;

    public string Decription;

    public UpgradeType UpgradeType;
    public Action<UpgradeItemUI> OnUpgradeButtonClicked;

    private void Start()
    {
        if(CurrentLevel == MaxLevel)
        {
            Cost.text = "MAX";
        }
    }
    public void UpgradeClick()
    {
        OnUpgradeButtonClicked?.Invoke(this);
    }

    public void UpgradeLevel(int level)
    { 

        if (level < 1 || level > MaxLevel)
        {
            return;
        }

        foreach (Transform child in Progress.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < level; i++)
        {
            GameObject go = Instantiate(ProgressBlock);
            go.transform.SetParent(Progress.transform, false);
        }

        CurrentLevel = level;
    }
}
