using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Spray Plus Plus – massively increases attack speed and ammo at the cost of damage.
    /// </summary>
    public class SprayPlusPlus : CustomCard
    {
        protected override string GetTitle() => "Spray Plus Plus";
        protected override string GetDescription() => "Hold mouse to fire 69 bullets at extreme speed.";
        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat { stat = "Bullets", amount = "69", positive = true },
            new CardInfoStat { stat = "Fire Rate", amount = "Extreme", positive = true },
            new CardInfoStat { stat = "Fire Mode", amount = "Single Shot", positive = true }
        };
        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.EvilPurple;
        protected override GameObject GetCardArt() => null;

        public override string GetModName() => "DMC";

        public override void OnAddCard(
            Player player,
            Gun gun,
            GunAmmo gunAmmo,
            CharacterData data,
            HealthHandler health,
            Gravity gravity,
            Block block,
            CharacterStatModifiers characterStats)
        {
            // Fire one bullet at a time
            gun.numberOfProjectiles = 1;

            gunAmmo.maxAmmo += 69;

            // Keep the rapid-fire feel
            gun.attackSpeed = 0.05f;
            gun.attackSpeedMultiplier = 0.05f;

            // Give enough ammo for the burst
            gun.ammo = 999;
            gun.ammoReg = 999f;

            // Tight spread
            gun.spread = 0f;
            gun.multiplySpread = 0f;
        }
    }
}
