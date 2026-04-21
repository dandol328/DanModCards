using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Spray Plus Plus – massively increases attack speed and ammo at the cost of damage.
    /// </summary>
    public class SprayPlusPlus : CustomCard
    {
        protected override string GetTitle()       => "Spray++";
        protected override string GetDescription() => "Spray and pray – fire at blinding speed with tons of ammo, but each bullet hits softer.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = true,
                stat           = "Attack Speed",
                amount         = "+1000%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Ammo",
                amount         = "+36",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Damage",
                amount         = "-75%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                          => CardInfo.Rarity.Uncommon;
        protected override CardThemeColor.CardThemeColorType GetTheme()         => CardThemeColor.CardThemeColorType.TechWhite;
        protected override GameObject GetCardArt()                              => null;
        public override string GetModName()                                     => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.attackSpeedMultiplier   *= 11.0f;
            gun.damage                  *= 0.25f;
            gun.gunAmmo.maxAmmo         += 36;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
        }
    }
}
