using System.Collections;
using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Zoomies Burst – every few seconds the player is hit by an uncontrollable burst of
    /// speed and jump power for a short duration.
    /// </summary>
    public class ZoomiesBurst : CustomCard
    {
        protected override string GetTitle()       => "Zoomies Burst";
        protected override string GetDescription() =>
            "Every few seconds you get a sudden speed and jump boost, for a short duration. " +
            "The timing is uncontrollable, and you don't decide when it happens.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = true,
                stat           = "Speed Burst",
                amount         = "+150%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Jump Burst",
                amount         = "+100%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Burst Timing",
                amount         = "Random",
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
            // No permanent baseline stat changes – the effect is entirely driven by the MonoBehaviour.
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<ZoomiesBurstEffect>();
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            var effect = player.gameObject.GetComponent<ZoomiesBurstEffect>();
            if (effect != null)
            {
                Destroy(effect);
            }
        }
    }
}
