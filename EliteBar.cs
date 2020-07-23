using System;
using System.Collections.Generic;
using System.Linq;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using ImGuiNET;
using SharpDX;
using Vector2 = System.Numerics.Vector2;

namespace EliteBar
{
    public class EliteBar : BaseSettingsPlugin<EliteBarSettings>
    {
        private readonly List<string> ignoredEntites = new List<string>
        {
            "Metadata/Monsters/LeagueAffliction/Volatile/AfflictionVolatile",
            "Metadata/Monsters/VolatileCore/VolatileCore",
            
            // Delirium Ignores
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes1",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes2",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes3",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes2",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes3",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple1",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple2",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple3",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatFillet1Vanish",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatFillet2Vanish",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatRhoa1Vanish",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatRhoa2Vanish",
            
            // Conquerors Ignores
            "Metadata/Monsters/AtlasExiles/AtlasExile1@",
            "Metadata/Monsters/AtlasExiles/CrusaderInfluenceMonsters/CrusaderArcaneRune",
            "Metadata/Monsters/AtlasExiles/AtlasExile2_",
            "Metadata/Monsters/AtlasExiles/EyrieInfluenceMonsters/EyrieFrostnadoDaemon",
            "Metadata/Monsters/AtlasExiles/AtlasExile3@",
            "Metadata/Monsters/AtlasExiles/AtlasExile3AcidPitDaemon",
            "Metadata/Monsters/AtlasExiles/AtlasExile3BurrowingViperMelee",
            "Metadata/Monsters/AtlasExiles/AtlasExile3BurrowingViperRanged",
            "Metadata/Monsters/AtlasExiles/AtlasExile4@",
            "Metadata/Monsters/AtlasExiles/AtlasExile4ApparitionCascade",
            "Metadata/Monsters/AtlasExiles/AtlasExile5Apparition",
            "Metadata/Monsters/AtlasExiles/AtlasExile5Throne",

            // Incursion Ignores
            "Metadata/Monsters/LeagueIncursion/VaalSaucerRoomTurret",
            "Metadata/Monsters/LeagueIncursion/VaalSaucerTurret",
            "Metadata/Monsters/LeagueIncursion/VaalSaucerTurret",
            
            // Betrayal Ignores
            "Metadata/Monsters/LeagueBetrayal/BetrayalTaserNet",
            "Metadata/Monsters/LeagueBetrayal/FortTurret/FortTurret1Safehouse",
            "Metadata/Monsters/LeagueBetrayal/FortTurret/FortTurret1",
            "Metadata/Monsters/LeagueBetrayal/MasterNinjaCop",

            // Legion Ignores
            "Metadata/Monsters/LegionLeague/LegionVaalGeneralProjectileDaemon",
            "Metadata/Monsters/LegionLeague/LegionSergeantStampedeDaemon",
            "Metadata/Monsters/LegionLeague/LegionSandTornadoDaemon",

            // Random Ignores
            "Metadata/Monsters/InvisibleFire/InvisibleSandstorm_",
            "Metadata/Monsters/InvisibleFire/InvisibleFrostnado",
            "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionDemonColdDegen",
            "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionDemonColdDegenUnique",
            "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionCorpseDegen",
            "Metadata/Monsters/InvisibleFire/InvisibleFireEyrieHurricane",
            "Metadata/Monsters/InvisibleFire/InvisibleIonCannonFrost",
            "Metadata/Monsters/InvisibleFire/AfflictionBossFinalDeathZone",

            "Metadata/Monsters/InvisibleCurse/InvisibleFrostbiteStationary",
            "Metadata/Monsters/InvisibleCurse/InvisibleConductivityStationary",
            "Metadata/Monsters/InvisibleCurse/InvisibleEnfeeble",

            "Metadata/Monsters/InvisibleAura/InvisibleWrathStationary",

            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret1",
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret2",
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret3",
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret4",

            "Metadata/Monsters/LeagueBestiary/RootSpiderBestiaryAmbush",
            "Metadata/Monsters/LeagueBestiary/BlackScorpionBestiaryBurrowTornado",

            // "Metadata/Monsters/Labyrinth/GoddessOfJustice",
            // "Metadata/Monsters/Labyrinth/GoddessOfJusticeMapBoss",
            "Metadata/Monsters/Frog/FrogGod/SilverOrb",
            "Metadata/Monsters/Frog/FrogGod/SilverPool",
            "Metadata/Monsters/LunarisSolaris/SolarisCelestialFormAmbushUniqueMap",
            "Metadata/Monsters/Invisible/MaligaroSoulInvisibleBladeVortex",
            "Metadata/Monsters/Daemon",
            "Metadata/Monsters/Daemon/MaligaroBladeVortexDaemon",
            "Metadata/Monsters/Daemon/SilverPoolChillDaemon",
            "Metadata/Monsters/AvariusCasticus/AvariusCasticusStatue",
        };

        private readonly Queue<Entity> EntityAddedQueue = new Queue<Entity>();
        private bool showWindow = true;
        private Vector2 vect = Vector2.Zero;

        public override void OnLoad()
        {
            CanUseMultiThreading = true;
            Graphics.InitImage("directions.png");
            Graphics.InitImage("healthbar.png");
            Graphics.InitImage("healthbar_bg.png");
        }

        public override Job Tick()
        {
            if (Settings.MultiThreading)
                return GameController.MultiThreadManager.AddJob(TickLogic, nameof(EliteBar));

            TickLogic();
            return null;
        }

        private void TickLogic()
        {
            while (EntityAddedQueue.Count > 0)
            {
                var entity = EntityAddedQueue.Dequeue();

                if (ignoredEntites.Any(x => entity.Path.Contains(x))) continue;

                if (entity.IsValid && !entity.IsAlive) continue;

                if (!entity.IsHostile) continue;
                var rarity = entity.Rarity;

                if (!Settings.ShowWhite && rarity == MonsterRarity.White) continue;
                if (!Settings.ShowMagic && rarity == MonsterRarity.Magic) continue;
                if (!Settings.ShowRare && rarity == MonsterRarity.Rare) continue;
                if (!Settings.ShowUnique && rarity == MonsterRarity.Unique) continue;

                var color = Color.Red;

                switch (rarity)
                {
                    case MonsterRarity.White:
                        color = Settings.NormalMonster;
                        break;
                    case MonsterRarity.Magic:
                        color = Settings.MagicMonster;
                        break;
                    case MonsterRarity.Rare:
                        color = Settings.RareMonster;
                        break;
                    case MonsterRarity.Unique:
                        color = Settings.UniqueMonster;
                        break;
                    case MonsterRarity.Error:
                        color = Color.LightGray;
                        break;
                    default:
                        color = Color.Red;
                        break;
                }

                entity.SetHudComponent(new EliteDrawBar(entity, color));
            }

            foreach (var entity in GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster])
            {
                var drawCmd = entity.GetHudComponent<EliteDrawBar>();
                drawCmd?.Update();
            }
        }

        public override void EntityAdded(Entity Entity)
        {
            if (!Settings.Enable.Value) return;
            if (Entity.Type == EntityType.Monster) EntityAddedQueue.Enqueue(Entity);
        }

        public override void AreaChange(AreaInstance area)
        {
            EntityAddedQueue.Clear();
        }

        public override bool Initialise()
        {
            Settings.RefreshEntities.OnPressed += () =>
            {
                foreach (var Entity in GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster])
                {
                    EntityAdded(Entity);
                }
            };

            vect = new Vector2(-0.1f, -0.25f);
            return true;
        }

        public override void Render()
        {
            var index = 0;

            foreach (var entity in GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster])
            {
                var structValue = entity.GetHudComponent<EliteDrawBar>();
                if (structValue == null) continue;
                if (!structValue.IsAlive) continue;
                var structValueCurLife = structValue.CurLife;
                var space = Settings.Space * index;
                var delta = structValue.Entity.GridPos - GameController.Player.GridPos;
                var distance = delta.GetPolarCoordinates(out var phi);
                var monsterText = $"{(int)distance} | {structValue.Name} => {structValueCurLife:###' '###' '###} | {structValue.PercentLife * 100}%";
                if (Settings.Debug)
                    monsterText = $"{(int)distance} | {structValue.Entity.Path} => {structValueCurLife:###' '###' '###} | {structValue.PercentLife * 100}%";
                var position = new SharpDX.Vector2(Settings.X + Settings.StartTextX, Settings.Y + space + Settings.StartTextY);
                var rectangleF = new RectangleF(Settings.X, Settings.Y + space, Settings.Width, Settings.Height);

                if (Settings.DynamicWidth)
                {
                    int width = (int)Graphics.MeasureText(monsterText).X;
                    rectangleF = new RectangleF(Settings.X, Settings.Y + space, width, Settings.Height);
                }

                if (Settings.UseImguiForDraw)
                {
                    ImGui.SetNextWindowPos(new Vector2(rectangleF.X, rectangleF.Y), ImGuiCond.Always, new Vector2(0.0f, 0.0f));
                    ImGui.SetNextWindowSize(new Vector2(rectangleF.Width, rectangleF.Height), ImGuiCond.Always);
                    ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0f, 0f));

                    ImGui.Begin($"##Progress bar{index}", ref showWindow,
                        ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize |
                        ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar |
                        ImGuiWindowFlags.NoScrollbar);

                    ImGui.PushStyleColor(ImGuiCol.PlotHistogram, structValue.Color.ToImguiVec4());
                    ImGui.PushStyleColor(ImGuiCol.Text, Settings.TextColor.Value.ToImguiVec4());
                    ImGui.PushStyleColor(ImGuiCol.FrameBg, Settings.ImguiDrawBg.Value.ToImguiVec4());
                    ImGui.ProgressBar(structValue.PercentLife, vect, $"{monsterText}");
                    ImGui.PopStyleColor(3);

                    ImGui.End();
                    ImGui.PopStyleVar();
                }
                else
                {
                    Graphics.DrawImage("healthbar_bg.png", rectangleF, Color.Black);
                    rectangleF.Width *= structValue.PercentLife;
                    Graphics.DrawImage("healthbar.png", rectangleF, structValue.Color);
                    rectangleF.Inflate(1, 1);
                    Graphics.DrawFrame(rectangleF, Settings.BorderColor, 1);
                    Graphics.DrawText(monsterText, position, Settings.TextColor);
                }

                var rectUV = MathHepler.GetDirectionsUV(phi, distance);
                rectangleF.Left -= rectangleF.Height + 5;
                rectangleF.Width = rectangleF.Height;

                Graphics.DrawImage("directions.png", rectangleF, rectUV);

                index++;
            }
        }

        public class EliteDrawBar
        {
            public EliteDrawBar(Entity Entity, Color color)
            {
                this.Entity = Entity;
                Color = color;
                Name = Entity.RenderName;
                IsAlive = true;
                CurLife = 0;
                PercentLife = 0;
            }

            public Entity Entity { get; }
            public string Name { get; }
            public Color Color { get; }
            public int CurLife { get; private set; }
            public float PercentLife { get; private set; }
            public bool IsAlive { get; private set; }

            public void Update()
            {
                IsAlive = Entity.IsValid && Entity.IsAlive;
                var lifeComponent = Entity.GetComponent<Life>();

                CurLife = lifeComponent == null ? 0 : lifeComponent.CurHP + lifeComponent.CurES;
                PercentLife = lifeComponent == null ? 0 : CurLife / (float) (lifeComponent.MaxHP + lifeComponent.MaxES);
            }
        }
    }
}
