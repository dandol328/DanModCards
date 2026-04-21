using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Infinity Mirror – every bullet spawns 2 more bullets recursively.
    /// Secondary bullets deal half damage. Maximum of 30 bullets can be spawned.
    /// </summary>
    public class InfinityMirror : CustomCard
    {
        protected override string GetTitle()       => "Infinity Mirror";
        protected override string GetDescription() =>
            "Every bullet you fire spawns 2 more bullets… which also spawn bullets… recursively. " +
            "Secondary bullets deal half damage. Maximum bullet cap: 30.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = true,
                stat           = "Bullets Spawned",
                amount         = "2 per shot",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Secondary Damage",
                amount         = "-50%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Max Bullets",
                amount         = "30",
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
            // No permanent baseline stat changes – the effect is entirely driven by the MonoBehaviour.
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.gameObject.GetOrAddComponent<InfinityMirrorEffect>();
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            var effect = gun.gameObject.GetComponent<InfinityMirrorEffect>();
            if (effect != null)
            {
                Destroy(effect);
            }
        }
    }
}