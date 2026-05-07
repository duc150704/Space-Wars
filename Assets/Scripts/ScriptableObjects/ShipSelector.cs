using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="shipInfors", menuName ="Ship Selector")]
public class ShipSelector : MonoBehaviour
{
    public ShipInfor[] ShipInfors;
    public TextMeshProUGUI Money;

    public RectTransform Position;

    public UpgradeItemContainer UpgradeItemContainer;
    List<UpgradeItemUI> _upgradeItemUIs = new List<UpgradeItemUI>();

    public GameObject upgradeItemUI;

    int _shipIndex = 0;
    ShipInfor _ship;

    public Button PlayButton;

    public void OnPlayButtonClick()
    {

    }

    public void onResetClick()
    {
        // Reset save data
        SaveData.ResetMoneyAndLevel();

        // Reset UI tiền
        Money.text = SaveData.LoadMoney.ToString();

        // Reset từng item UI về level 1
        foreach (var ui in _upgradeItemUIs)
        {
            ui.CurrentLevel = 0;
            ui.UpgradeLevel(1);

            var upgradeItem = _ship.ShipUpgradeInforSO.UpgradeItemInfors
                .Find(x => x.UpgradeType == ui.UpgradeType);
            ui.Cost.text = upgradeItem.UpgradeInfors[0].CostPerLevel.ToString();
        }
    }

    private void Start()
    {
        Money.text = SaveData.LoadMoney.ToString();
          _ship =  GetShip(_shipIndex);

        GameObject go = Instantiate(_ship.PrefabsUI, Position);
        go.transform.localScale = new Vector3(200, 200, 0);


        for (int i = 0; i < _ship.ShipUpgradeInforSO.NumberItem; i++)
        {
            GameObject item = Instantiate(upgradeItemUI);
            UpgradeItemUI ui = item.GetComponent<UpgradeItemUI>();

            ui.Name.text = _ship.ShipUpgradeInforSO.UpgradeItemInfors[i].Name;
            ui.Cost.text = _ship.ShipUpgradeInforSO.UpgradeItemInfors[i].UpgradeInfors[0].CostPerLevel.ToString();
            ui.Image.sprite = _ship.ShipUpgradeInforSO.UpgradeItemInfors[i].Sprite;
            ui.MaxLevel = _ship.ShipUpgradeInforSO.UpgradeItemInfors[i].UpgradeCount;
            ui.UpgradeType = _ship.ShipUpgradeInforSO.UpgradeItemInfors[i].UpgradeType;
            ui.OnUpgradeButtonClicked += Upgrade;

            int savedLevel = LoadLevel(_ship.ShipUpgradeInforSO.UpgradeItemInfors[i].UpgradeType);
            ui.CurrentLevel = 0;
            ui.MaxLevel = _ship.ShipUpgradeInforSO.UpgradeItemInfors[i].UpgradeCount;
            ui.UpgradeLevel(savedLevel);
            _upgradeItemUIs.Add(ui);
            item.transform.SetParent(UpgradeItemContainer.transform, false);
        }

        int LoadLevel(UpgradeType type) => type switch
        {
            UpgradeType.Engine => SaveData.LoadEngineLevel(),
            UpgradeType.Shield => SaveData.LoadShieldLevel(),
            UpgradeType.Reactor => SaveData.LoadReactorLevel(),
            UpgradeType.Weapon => SaveData.LoadWeaponLevel(),
            _ => 1
        };

        void Upgrade(UpgradeItemUI ui)
        {

            if (ui.CurrentLevel >= ui.MaxLevel)
            {
                return;
            }

            if (!TryUpgradeItem(ui))
                return;

            ui.CurrentLevel++;
            ui.UpgradeLevel(ui.CurrentLevel);

            
            var upgradeItem = _ship.ShipUpgradeInforSO.UpgradeItemInfors
                .Find(x => x.UpgradeType == ui.UpgradeType);

            if (ui.CurrentLevel < ui.MaxLevel)
                ui.Cost.text = upgradeItem.UpgradeInfors[ui.CurrentLevel].CostPerLevel.ToString();
            else
                ui.Cost.text = "MAX";


            SaveData.SaveLevels(
            GetUILevel(UpgradeType.Engine),
            GetUILevel(UpgradeType.Shield),
            GetUILevel(UpgradeType.Reactor),
            GetUILevel(UpgradeType.Weapon));

        }

        int GetUILevel(UpgradeType type)
        {
            foreach (Transform child in UpgradeItemContainer.transform)
            {
                var ui = child.GetComponent<UpgradeItemUI>();
                if (ui != null && ui.UpgradeType == type)
                    return ui.CurrentLevel;
            }
            return 1;
        }

        bool TryUpgradeItem(UpgradeItemUI ui)
        {
            
            var upgradeItem = _ship.ShipUpgradeInforSO.UpgradeItemInfors
                .Find(x => x.UpgradeType == ui.UpgradeType);

            float cost = upgradeItem.UpgradeInfors[ui.CurrentLevel].CostPerLevel;

            float money = float.Parse(Money.text);

            if (money < cost)
            {
                return false;
            }

            money -= cost;

            Money.text = money.ToString();

            SaveData.SaveMoney(money);
            return true;
        }

    }
    public ShipInfor GetShip(int index)
    {
        return ShipInfors[index];   
    }
}

[System.Serializable]
public class ShipInfor
{
    public GameObject PrefabsUI;
    public ShipBaseInforSO ShipBaseInforSO;
    public ShipUpgradeInforSO ShipUpgradeInforSO;
}
