using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Throat Goat Supreme – insane pierce and speed, but spread and player gravity are brutal.
    /// </summary>
    public class ThroatGoatSupreme : CustomCard
    {
        protected override string GetTitle()       => "Throat Goat Supreme";
        protected override string GetDescription() =>
            "The GOAT at taking it all the way down. Insane range and pierce, " +
            "but your aim gets throat-fucked at the end.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Pierce",
                amount        = "+6",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bullet Speed",
                amount        = "+400%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Spread",
                amount        = "+180%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Player Gravity",
                amount        = "+200%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                  => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.NatureBrown;
        protected override GameObject GetCardArt()                      => null;
        public override string GetModName()                             => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.projectileSpeed     *= 5.0f;
            gun.spread              *= 2.8f;
            statModifiers.gravity   *= 3.0f;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.SetFieldValue("pierce", (int)gun.GetFieldValue("pierce") + 6);
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.SetFieldValue("pierce", (int)gun.GetFieldValue("pierce") - 6);
        }
    }
}
