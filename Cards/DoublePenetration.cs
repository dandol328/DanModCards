using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    class DoublePenetration : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            // Fire 2 bullets per shot
            gun.numberOfProjectiles += 1;

            // Slight spread so they don't stack perfectly
            gun.spread += 0.05f;

            // Increased damage
            gun.damage *= 1.25f;

            // Longer reload
            gun.reloadTime *= 1.5f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers statModifiers)
        {
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers statModifiers)
        {
        }

        protected override string GetTitle()
        {
            return "Double Penetration";
        }

        protected override string GetDescription()
        {
            return "Fire two bullets in a tight volley\n<color=#00FF00>+25% damage</color>\n<color=#FF0000>+50% reload time</color>";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.OffensiveRed;
        }

        public override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Projectiles",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.increased
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.increased
                }
            };
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override CardInfoStat[] GetHiddenStats()
        {
            return null;
        }

        public override string GetModName()
        {
            return "DanMod";
        }
    }
}
