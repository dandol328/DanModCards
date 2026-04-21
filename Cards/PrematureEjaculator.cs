using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Premature Ejaculator – blazing fire rate but tiny magazine and reduced damage.
    /// </summary>
    public class PrematureEjaculator : CustomCard
    {
        protected override string GetTitle()       => "Premature Ejaculator";
        protected override string GetDescription() =>
            "Shoot your shot FAST and HARD… but you're done in 2 seconds flat. Sorry babe.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Fire Rate",
                amount        = "+300%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Max Ammo",
                amount        = "-80%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Damage",
                amount        = "-50%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                  => CardInfo.Rarity.Uncommon;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.FirepowerYellow;
        protected override GameObject GetCardArt()                      => null;
        public override string GetModName()                             => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.attackSpeedMultiplier *= 4.0f;
            gun.damage                *= 0.5f;
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gunAmmo.maxAmmo = UnityEngine.Mathf.Max(1, (int)(gunAmmo.maxAmmo * 0.2f));
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
        }
    }
}
