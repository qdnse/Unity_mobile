using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    void UpgradeMaxHealth() {
        PlayerManager.instance.MaxHealth += 10;
    }

    void UpgradeHealthRegen() {
        PlayerManager.instance.Heal += 1;
    }

    void UpgradeShootRate() {
        PlayerManager.instance.Damage += 2;
    }

    void UpgradeDamage() {
        PlayerManager.instance.Damage += 2;
    }

    void UpgradeSpeed() {
        PlayerManager.instance.Speed += 0.5f;
    }

    void UpgradeMoneyMultiplier() {
        PlayerManager.instance.MoneyMultiplier += +0.1f;
    }

    void UpgradeShieldRegen() {
        PlayerManager.instance.Damage -= (PlayerManager.instance.Damage / 20);
    }

    void UpgradeShieldDuration() {
        PlayerManager.instance.ShieldDuration += 0.1f;
    }

    void Endgame() {
    }
}
