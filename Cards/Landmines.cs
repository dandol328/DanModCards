using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Landmines – bullets drop immediately and deal massive damage on contact.
    /// </summary>
    public class Landmines : CustomCard
    {
        protected override string GetTitle()       => "Landmines";
        protected override string GetDescription() => "Bullets drop straight to the ground and detonate for massive damage.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = false,
                stat           = "Bullet Gravity",
                amount         = "-100%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Bullet Speed",
                amount         = "-80%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Damage",
                amount         = "+400%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                          => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme()         => CardThemeColor.CardThemeColorType.EvilPurple;
        protected override GameObject GetCardArt()                              => null;
        public override string GetModName()                                     => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.gravity          *= 0.0f;
            gun.projectileSpeed  *= 0.2f;
            gun.damage           *= 5.0f;
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