using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Deep Throat – insane bullet speed and pierce, but accuracy suffers at range.
    /// </summary>
    public class DeepThroat : CustomCard
    {
        private const int PierceAmount = 4;

        protected override string GetTitle()       => "Deep Throat";
        protected override string GetDescription() =>
            "Swallow those bullets whole. Insane speed and pierce, but your aim gets a little sloppy at the end.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bullet Speed",
                amount        = "+180%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Pierce",
                amount        = "+4",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Spread",
                amount        = "+100%",
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
            gun.projectileSpeed *= 2.8f;
            gun.spread          *= 2.0f;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.gameObject.AddComponent<PierceEffect>().PierceCount = PierceAmount;
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            foreach (var effect in gun.gameObject.GetComponents<PierceEffect>())
            {
                if (effect.PierceCount == PierceAmount)
                {
                    Destroy(effect);
                    break;
                }
            }
        }
    }
}
