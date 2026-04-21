using BepInEx;
using DanModCards.Cards;
using HarmonyLib;
using UnboundLib.Cards;

namespace DanModCards
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class DanModCards : BaseUnityPlugin
    {
        public const string ModId      = "com.dandolfriends.rounds.danmodcards";
        public const string ModName    = "DanModCards";
        public const string Version    = "1.0.0";
        public const string ModInitials = "DMC";

        private void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        private void Start()
        {
            CustomCard.BuildCard<ZoomiesBurst>();
            CustomCard.BuildCard<VictorsMeow>();
            CustomCard.BuildCard<AntiGravity>();
            CustomCard.BuildCard<InfinityMirror>();
            CustomCard.BuildCard<SprayPlusPlus>();
            CustomCard.BuildCard<RailCannon>();
            CustomCard.BuildCard<MegaTank>();
            CustomCard.BuildCard<Landmines>();
            CustomCard.BuildCard<GlassDildo>();
            CustomCard.BuildCard<CantCatchMe>();

            // Adult-themed cards
            CustomCard.BuildCard<CockAndLoad>();
            CustomCard.BuildCard<BigDickEnergy>();
            CustomCard.BuildCard<DeepThroat>();
            CustomCard.BuildCard<BallBuster>();
            CustomCard.BuildCard<ReverseCowgirl>();
            CustomCard.BuildCard<TheUnlubedDildo>();
            CustomCard.BuildCard<CumshotCannon>();
            CustomCard.BuildCard<BallsDeep>();
            CustomCard.BuildCard<EdgingMaster>();
            CustomCard.BuildCard<CreampieDeluxe>();
            CustomCard.BuildCard<ThroatGoatSupreme>();
            CustomCard.BuildCard<NutBuster9000>();
            CustomCard.BuildCard<DoublePenetration>();
            CustomCard.BuildCard<BackdoorBandit>();
        }
    }
}