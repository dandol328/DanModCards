using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// The Unlubed Dildo – raw, brutal stats across the board, but hurts you just as much.
    /// </summary>
    public class TheUnlubedDildo : CustomCard
    {
        private const float SelfDmgPercent = 0.40f;

        protected override string GetTitle()       => "The Unlubed Dildo";
        protected override string GetDescription() =>
            "No lube. No mercy. It hurts so good going in… and so bad coming back out. " +
            "Raw, brutal penetration for everyone involved.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Damage",
                amount        = "+400%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bullet Speed",
                amount        = "+250%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Self Damage on Fire",
                amount        = "40%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Reload Time",
                amount        = "+180%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Player Size",
                amount        = "+80%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                  => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.DestructiveRed;
        protected override GameObject GetCardArt()                      => null;
        public override string GetModName()                             => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage                   *= 5.0f;
            gun.projectileSpeed          *= 3.5f;
            gun.reloadTime               *= 2.8f;
            statModifiers.sizeMultiplier *= 1.8f;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.gameObject.AddComponent<SelfDamageOnFireEffect>().DamagePercent = SelfDmgPercent;
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            foreach (var effect in gun.gameObject.GetComponents<SelfDamageOnFireEffect>())
            {
                if (Mathf.Approximately(effect.DamagePercent, SelfDmgPercent))
                {
                    Destroy(effect);
                    break;
                }
            }
        }
    }
}
