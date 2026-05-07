using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipInfor", menuName = "Ship/ShipInfor")]
public class ShipBaseInforSO : ScriptableObject
{
    public string Name;
    public GameObject Prefabs;

    [Header("Base")]
    public int BaseEngineLevel = 1;
    public int BaseShieldLevel = 1;
    public int BaseReactorLevel = 1;
    public int BaseWeaponLevel = 1;

    public float BaseSpeed;
    public float BaseShield;


    [Header("Max Level")]
    public int MaxEngineLevel;
    public int MaxWeaponLevel;
    public int MaxReactorLevel;
    public int MaxShieldLevel;
}
