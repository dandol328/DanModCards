using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Double Penetration – fires two extra projectiles with wild spread and reduced damage.
    /// </summary>
    public class DoublePenetration : CustomCard
    {
        protected override string GetTitle()       => "Double Penetration";
        protected override string GetDescription() =>
            "Why use one when you can use two? " +
            "Double the bullets, double the fun… double the absolute chaos.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Extra Projectiles",
                amount        = "+2",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Spread",
                amount        = "+200%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Damage",
                amount        = "-40%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bounce",
                amount        = "+4",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                  => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.FirepowerYellow;
        protected override GameObject GetCardArt()                      => null;
        public override string GetModName()                             => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage *= 0.6f;
            gun.spread *= 3.0f;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.numberOfProjectiles += 2;
            gun.reflects            += 4;
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.numberOfProjectiles -= 2;
            gun.reflects            -= 4;
        }
    }
}
