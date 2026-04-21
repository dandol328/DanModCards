using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Ball Buster – enormous damage and recoil, but every shot deals self-damage.
    /// </summary>
    public class BallBuster : CustomCard
    {
        private const float SelfDmgPercent = 0.30f;

        protected override string GetTitle()       => "Ball Buster";
        protected override string GetDescription() =>
            "Bust balls on sight. Enemies take huge damage… but every shot kicks you right in the nuts too.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Damage",
                amount        = "+250%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Self Damage on Fire",
                amount        = "30%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Recoil",
                amount        = "+200%",
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
            gun.damage            *= 3.5f;
            gun.recoilMuiltiplier *= 3.0f;
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
