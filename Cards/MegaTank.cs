using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Mega Tank – drastically boosts health and gravity at the cost of movement speed.
    /// </summary>
    public class MegaTank : CustomCard
    {
        protected override string GetTitle()       => "Mega Tank";
        protected override string GetDescription() => "Become an unstoppable wall of health. You're nearly impossible to kill – just don't expect to move quickly.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = true,
                stat           = "Health",
                amount         = "+500%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Character Gravity",
                amount         = "+50%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Movement Speed",
                amount         = "-50%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                          => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme()         => CardThemeColor.CardThemeColorType.TechWhite;
        protected override GameObject GetCardArt()                              => null;
        public override string GetModName()                                     => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health        *= 6.0f;
            statModifiers.gravity       *= 1.5f;
            statModifiers.movementSpeed *= 0.5f;
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
