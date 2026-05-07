using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseData
{
    public static float BaseSpeed = 0.05f;
    public static float BaseShieldTime = 4f;
}

public static class SaveData
{
    const string ENGINE_LEVEL = "EngineLevel";
    const string SHIELD_LEVEL = "ShieldLevel";
    const string REACTOR_LEVEL = "ReactorLevel";
    const string WEAPON_LEVEL = "WeaponLevel";
    const string MONEY = "Money";

    const string SPEED = "Speed";
    const string SHIELD_DURATION = "ShieldDuration";

    public static void SaveLevels(int engine, int shield, int reactor, int weapon)
    {
        PlayerPrefs.SetInt(ENGINE_LEVEL, engine);
        PlayerPrefs.SetInt(SHIELD_LEVEL, shield);
        PlayerPrefs.SetInt(REACTOR_LEVEL, reactor);
        PlayerPrefs.SetInt(WEAPON_LEVEL, weapon);

        PlayerPrefs.SetFloat(SPEED, StatsCalculator.GetSpeed(BaseData.BaseSpeed, engine));
        PlayerPrefs.SetFloat(SHIELD_DURATION, StatsCalculator.GetSpeed(BaseData.BaseShieldTime, shield));

        PlayerPrefs.Save();
    }

    public static void ResetMoneyAndLevel()
    {
        SaveLevels(0,0,0,0);
        SaveMoney(7000);
    }

    public static void SaveMoney(float money)
    {
        PlayerPrefs.SetFloat(MONEY, money);
        PlayerPrefs.Save();
    }

    //public static LoadSpeed()
    //{
        
    //}

    public static float LoadMoney => PlayerPrefs.GetFloat(MONEY);

    public static int LoadEngineLevel(int defaultLevel = 1) => PlayerPrefs.GetInt(ENGINE_LEVEL, defaultLevel);
    public static int LoadShieldLevel(int defaultLevel = 1) => PlayerPrefs.GetInt(SHIELD_LEVEL, defaultLevel);
    public static int LoadReactorLevel(int defaultLevel = 1) => PlayerPrefs.GetInt(REACTOR_LEVEL, defaultLevel);
    public static int LoadWeaponLevel(int defaultLevel = 1) => PlayerPrefs.GetInt(WEAPON_LEVEL, defaultLevel);
}