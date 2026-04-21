using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Victor's Meow – grants an extra block charge and, whenever the player blocks,
    /// unleashes a shockwave that knocks back nearby enemies.
    /// </summary>
    public class VictorsMeow : CustomCard
    {
        protected override string GetTitle()       => "Victor's Meow";
        protected override string GetDescription() =>
            "Emits a shockwave on block that knocks back nearby enemies. Grants an extra block charge.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = true,
                stat           = "Blocks",
                amount         = "+1",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Shockwave",
                amount         = "On Block",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                          => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme()         => CardThemeColor.CardThemeColorType.MagicPink;
        protected override GameObject GetCardArt()                              => null;
        public override string GetModName()                                     => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            // Grant one extra block charge as a permanent card stat.
            block.additionalBlocks += 1;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<VictorsMeowEffect>();
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            var effect = player.gameObject.GetComponent<VictorsMeowEffect>();
            if (effect != null)
            {
                Destroy(effect);
            }
        }
    }
}
