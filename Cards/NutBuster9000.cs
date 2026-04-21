using DanModCards.Effects;
using UnboundLib.Cards;
using UnityEngine;

namespace DanModCards.Cards
{
    /// <summary>
    /// Nut Buster 9000 – colossal damage and bullet size, but recoil launches you and shots hurt you.
    /// </summary>
    public class NutBuster9000 : CustomCard
    {
        private const float SelfDmgPercent = 0.25f;

        protected override string GetTitle()       => "Nut Buster 9000";
        protected override string GetDescription() =>
            "Busts nuts so hard you'll hear the echo. " +
            "Massive damage + self-recoil that launches you into orbit.";

        protected override CardInfoStat[] GetStats() => new[]
        {
            new CardInfoStat
            {
                positive      = true,
                stat          = "Damage",
                amount        = "+300%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Recoil",
                amount        = "+500%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = true,
                stat          = "Bullet Size",
                amount        = "+200%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
            new CardInfoStat
            {
                positive      = false,
                stat          = "Self Damage on Fire",
                amount        = "25%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
            },
        };

        protected override CardInfo.Rarity GetRarity()                  => CardInfo.Rarity.Rare;
        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.FirepowerYellow;
        protected override GameObject GetCardArt()                      => null;
        public override string GetModName()                             => DanModCards.ModInitials;

        public override void SetupCard(
            CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            gun.damage            *= 4.0f;
            gun.recoilMuiltiplier *= 6.0f;
            gun.size              *= 3.0f;
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
