using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    public class DoublePenetration : CustomCard
    {
        protected override string GetTitle() => "Double Penetration";

        protected override string GetDescription()
        {
            return "Fire two bullets in a tight volley\n<color=#00FF00>+25% damage</color>\n<color=#FF0000>+50% reload time</color>";
        }

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive = true,
                stat = "Projectiles",
                amount = "+1",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            },
            new CardInfoStat
            {
                positive = true,
                stat = "Damage",
                amount = "+25%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            },
            new CardInfoStat
            {
                positive = false,
                stat = "Reload Time",
                amount = "+50%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            }
        };

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.DestructiveRed;
        protected override GameObject GetCardArt() => null;
        public override string GetModName() => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.numberOfProjectiles += 1;
            gun.spread += 0.05f;
            gun.damage *= 1.25f;
            gun.reloadTime *= 1.5f;
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
