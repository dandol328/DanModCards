using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Balls Deep – endless pierce and ludicrous bullet speed, but spread and gravity are punishing.
    /// </summary>
    public class BallsDeep : CustomCard
    {
        protected override string GetTitle()       => "Balls Deep";
        protected override string GetDescription() =>
            "Go all the way in. Bullets travel forever and pierce through everything… " +
            "including your own dignity when you miss.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Pierce",
                amount        = "+8",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bullet Speed",
                amount        = "+300%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Spread",
                amount        = "+150%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bullet Gravity",
                amount        = "-90%",
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
            gun.projectileSpeed *= 4.0f;
            gun.spread          *= 2.5f;
            gun.gravity         *= 0.1f;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.SetFieldValue("pierce", (int)gun.GetFieldValue("pierce") + 8);
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.SetFieldValue("pierce", (int)gun.GetFieldValue("pierce") - 8);
        }
    }
}
