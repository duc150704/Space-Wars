using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName ="UpgradeInfor", menuName = "Ship/Ship Upgrade")]
public class ShipUpgradeInforSO : ScriptableObject
{
    public List<UpgradeItemInfor> UpgradeItemInfors;

    public int NumberItem => UpgradeItemInfors.Count;
}


[System.Serializable]
public class UpgradeItemInfor
{
    public UpgradeType UpgradeType;

    public string Name;
    public string Description;

    public Sprite Sprite;

    public List<UpgradeInfor> UpgradeInfors;

    public int UpgradeCount => UpgradeInfors.Count;
}

[System.Serializable]
public class UpgradeInfor
{
    public float UpgradePerLevel;
    public float CostPerLevel;
}

public enum UpgradeType
{
    Weapon, Engine, Reactor, Shield
}
