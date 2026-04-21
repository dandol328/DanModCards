using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Landmines – a massive, slow-drifting bullet that phases through walls, bounces 7 times,
    /// stays alive for 30 seconds, and cannot be blocked. A roaming landmine of death.
    /// </summary>
    public class Landmines : CustomCard
    {
        private const int BounceCount = 7;
        protected override string GetTitle()       => "Landmines";
        protected override string GetDescription() =>
            $"Fires a huge, lumbering bullet that drifts through walls and can't be blocked. " +
            $"It bounces {BounceCount} times and lingers for 30 seconds – a roaming landmine. Don't stand still.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive       = true,
                stat           = "Damage",
                amount         = "+400%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = false,
                stat           = "Bullet Speed",
                amount         = "-90%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Bullet Gravity",
                amount         = "-90%",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Smart Bounces",
                amount         = $"+{BounceCount}",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Bullet Lifetime",
                amount         = "30s",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Ignores Walls",
                amount         = "Yes",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive       = true,
                stat           = "Unblockable",
                amount         = "Yes",
                simepleAmount  = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                          => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme()         => CardThemeColor.CardThemeColorType.EvilPurple;
        protected override GameObject GetCardArt()                              => null;
        public override string GetModName()                                     => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage              *= 5.0f;    // +400% damage
            gun.projectileSpeed     *= 0.1f;    // Very slow – drifts like a landmine
            gun.gravity             *= 0.1f;    // 10% gravity – mostly horizontal drift
            gun.size                *= 2.5f;    // Large bullet – visually imposing
            gun.destroyBulletAfter   = 30f;     // Stays alive for 30 seconds
            gun.ignoreWalls          = true;    // Phases through walls
            gun.unblockable          = true;    // Cannot be blocked
            gun.smartBounce          = BounceCount;  // smart bounces (aims toward enemies)
        }

        public override void OnAddCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.reflects += BounceCount;
        }

        public override void OnRemoveCard(
            Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block,
            CharacterStatModifiers characterStats)
        {
            gun.reflects -= BounceCount;
        }
    }
}