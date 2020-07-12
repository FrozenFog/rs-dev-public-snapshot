﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public static class Constant
    {
        public const string ReleaseDate = " - ver. 2020.07.12-2304 - UNSTABLE";
        public static class Logics
        {
            public static int[] EventIdLength3 = { 60, 61 };
        }
        public static class TileSetClass
        {
            public const string Clear = "TscClear";
            public const string Ramp = "TscRamp";
            public const string Rough = "TscRough";
            public const string Green = "TscGreen";
            public const string Sand = "TscSand";
            public const string Pave = "TscPave";
            public const string Water = "TscWater";

            public const string OtherClass = "TscOtherClass";

            public const string Ramps = "TscRampTypes";
            public const string Cliffs = "TscCliff";
            public const string Bridges = "TscBridges";
            public const string Roads = "TscRoads";
            public const string InWater = "TscInWater";
            public const string Features = "TscFeatures";
            public const string Railroad = "TscRail";
            public const string Tunnel = "TscTunnel";
            public const string Shore = "TscShore";
            public const string Paves = "TscPaves";
            public const string Fixes = "TscFixes";
        }
        public static class LATSystem
        {
            public const string sClear = "ClearTile";
            public const string sRough = "RoughTile";
            public const string sSand = "SandTile";
            public const string sGreen = "GreenTile";
            public const string sPave = "PaveTile";
            public static int idxClear, idxRough, idxSand, idxGreen, idxPave;
            public const string sClearToRough = "ClearToRoughLat";
            public const string sClearToSand = "ClearToSandLat";
            public const string sClearToGreen = "ClearToGreenLat";
            public const string sClearToPave = "ClearToPaveLat";
            public static int idxC2R, idxC2G, idxC2S, idxC2P;
            public static List<int> LatSet;
            public static List<int> LatConnect;
            public static List<int> LatFull;
            public static void SetLat()
            {
                LatSet = new List<int>()
                {
                    idxClear,idxRough,idxSand,idxGreen,idxPave,
                    idxC2G,idxC2P,idxC2R,idxC2S
                };
                LatConnect = new List<int>()
                {
                    idxC2G,idxC2P,idxC2R,idxC2S
                };
                LatFull = new List<int>()
                {
                    idxClear,idxRough,idxSand,idxGreen,idxPave,
                };
            }
        }
        public static class MapStructure
        {
            public const int ArgLenInfantry = 14;
            public const int ArgLenStructure = 17;
            public const int ArgLenUnit = 14;
            public const int ArgLenAircraft = 12;
            public const int ArgLenAiTrigger = 18;
        }
        public static class EntName
        {
            public static readonly string[] SystemEntity =
            {
                "General","JumpjetControls","SpecialWeapons","GenericPrerequisites","AudioVisual","CrateRules","Powerups","CombatDamage","Radiation","ElevationModel",
                "WallModel","GlobalControls","MultiplayerDialogSettings","Maximums","AI","IQ","Easy","Normal","Difficult","MouseCursors","Colors","ColorAdd","ArmorTypes",
                "Sides","GDI","Nod","ThirdSide","FourthSide","UnitedStates","Europeans","Pacific","USSR","Latin","Chinese","PsiCorps","ScorpionCell","Headquaters","Guild1",
                "Guild2","Guild3","Special","Neutral","Defaults"
            };
            public static readonly string[] DictionaryList =
            {
                "InfantryTypes","VehicleTypes","BuildingTypes","TerrainTypes","SmudgeTypes","OverlayTypes","Animations","VoxelAnims","Particles","ParticleSystems",
                "SuperWeaponTypes","Warheads","WeaponTypes","Projectiles","Countries","VariableNames","Movies","SoundList","Themes","PreviewPack","Houses","IsoMapPack5",
                "OverlayDataPack","OverlayPack","Digest"
            };
            public static readonly string[] MapList =
            {
                "Preview", "PreviewPack", "AITriggerTypes", "AITriggerTypesEnable", "Actions", "Basic", "CellTags", "Countries", "Events", "Houses", "Infantry",
                "IsoMapPack5", "Lighting", "Map", "OverlayDataPack", "OverlayPack", "Ranking", "ScriptTypes", "Smudge", "SpecialFlags", "Structures", "Tags",
                "TaskForces", "TeamTypes", "Triggers", "Units", "VariableNames", "Waypoints", "Digest"
            };
        }
        public static readonly string[] BoolTrue = { "yes", "True", "true" };
        public static readonly string[] BoolFalse = { "no", "False", "false" };
        public static readonly string[] NullString = { "<none>", "None", "none" };
        public static readonly string[] GenericArmorType = { "none", "flak", "plate", "light", "medium", "heavy", "wood", "steel", "concrete", "special_1", "special_2" };
        public static readonly List<string> TeamBoolIndex = new List<string>
        {
            "Full", "Whiner", "Droppod", "Suicide", "Loadable", "Prebuild", "Annoyance", "IonImmune","Recruiter", "Reinforce",
            "Aggressive", "Autocreate","GuardSlower","OnTransOnly","AvoidThreats","LooseRecruit","IsBaseDefense","UseTransportOrigin","OnlyTargetHouseEnemy",
            "TransportsReturnOnUnload","AreTeamMembersRecruitable"
        };
        public static class KeyName
        {
            public static readonly string[] IntKey =
            {
                "Bounty.Value","Cost","PhysicalSize","Points","Sight","Size","Soylent","Speed","Strength","ThreatPosed","GuardRange","DetectDisguiseRange",
                "Adjacent","TechLevel","Ammo","Reload","UndeployDelay","AirRangeBonus","JumpjetSpeed","JumpjetClimb","JumpJetAccel","BuildLimit","SensorsSight",
                "Passengers","AttachEffect.Duration","MaxDebris","ROT","Power","MinDebris","FireAngle","TurretCount","WeaponCount",
                "Damage","ROF","LaserDuration","EMP.Duration","EMP.Cap","AmbientDamage","Burst","DisableWeapons.Duration","EmptyReload","MaxNumberOccupants",
                "Money.Amount","SizeLimit","Sonar.Duration","TurretROT"
            };
            public static readonly string[] FloatKey =
            {
                "BuildTimeMultiplier","Range","MinimumRange","CellSpread","PercentAtMax","AttachEffect.SpeedMultiplier","AttachEffect.ArmorMultiplier",
                "AttachEffect.FirepowerMultiplier","BallisticScatter.Max","BallisticScatter.Min","DeathWeaponDamageModifier","IronCurtain.Modifier",
                "Weight"
            };
            public static readonly string[] PercentListKey =
            {
                "Verses"
            };
            public static readonly string[] PercentKey =
            {
                "ProneDamage","Survivor.VeteranPassengerChance","Survivor.RookiePassengerChance","Survivor.ElitePassengerChance"
            };
            public static readonly string[] SpaceListKey =
            {
                "Sounds","Control","FShift","Priority","Type","Delay"
            };
        }
        public static class Interpreter
        {
            public static readonly string[] SightLike =
            {
                "Sight","Size","AirRangeBonus","ThreatPosed","Soylent","Bounty.Value","JumpjetSpeed","JumpjetClimb","AmbientDamage","Ammo","AttachEffect.Duration",
                "BuildLimit","Burst","CellSpread","Cost","Damage","DisableWeapons.Duration","EMP.Cap","EMP.Duration","EmptyReload","Fearless","GuardRange",
                "LaserDuration","MaxDebris","MaxNumberOccupants","MinDebris","MinimumRange","Money.Amount","Passengers","PercentAtMax","Points","Power","Range",
                "Reload","ROF","ROT","SizeLimit","Sonar.Duration","Speed","Strength","TurretROT","UndeployDelay","Weight","SpecialThreatValue","TurretCount",
                "WeaponCount","Survivor.RookiePassengerChance","Survivor.VeteranPassengerChance","Survivor.ElitePassengerChance"
            };
            public static readonly string[] ActiveBoolLike =
            {
                "AffectsAllies","AffectsOwner","Bounty.Display","Bright","Deployer","DeployFire","ImmuneToPsionics","ImmuneToRadiation","Malicious","OmniFire","OpportunityFire",
                "RadarInvisible","Rocker","Selectable","Trainable","Wall","Wood","Bounty","CanBeReversed","DefaultToGuardArea","IsSelectableCombatant","OpenTopped","NoManualUnload",
                "NoManualEnter","DontScore","Insignificant","ProtectedDriver","Turret","Crusher","Accelerates","ImmuneToPsionicWeapons","VehicleThief.Allowed","IsSimpleDeployer",
                "Arcing","SubjectToCliffs","SubjectToElevation","SubjectToWalls","SubjectToBuildings","Shadow","AA","AG","ImmuneToEMP","Capturable"
            };
            public static readonly string[] PassiveBoolLike =
            {

            };
            public static readonly string[] AcquireBoolLike =
            {
                "Fearless"
            };
            public static readonly string[] NameListLike =
            {
                "Anim","Prerequisite","Prerequisite.StolenTechs","Owner","AnimList","SW.Inhibitors","RequiredHouses","EliteAbilities","ForbiddenHouses","VeteranAbilities",
                "Explosion","DamageParticleSystems","Dock","DebrisAnims","InitialPayload.Types"
            };
            public static readonly string[] NumListLike =
            {
                "DebrisMaximums","InitialPayload.Nums","DamageSmokeOffset","LaserInnerColor","LaserOuterColor","LaserOuterSpread","RadarColor"
            };
            public static readonly string[] NameLike =
            {
                "Name", "VoiceSecondaryWeaponAttack","ElitePrimary","EliteSecondary","VoiceDeploy","Promote.VeteranSound","Promote.EliteSound","Secondary","Primary",
                "UIName","DeathWeapon","Passengers.Allowed","Image","AttachEffect.Animation","Locomotor","Parachute.Anim","Convert.Deploy","Category",
                "CrushSound","DamageSound","Insignia.Rookie","Insignia.Veteran","Insignia.Elite","Projectile","Warhead","Report","OccupantAnim","AssaultAnim",
                "PreImpactAnim","OpenTransportWeapon","VoiceSpecialAttack","IFVMode","ReversedAs","Cursor.Deploy","GroupAs","OpenToppedAnim","SW.AITargeting","InfDeathAnim",
                "InfDeath","TechLevel","DebrisTypes","FakeOf"
            };
            public static readonly string[] MultiplierLike =
            {
                "BuildTimeMultiplier","DeathWeaponDamageModifier","IronCurtain.Modifier","ForceShield.Modifier","EMP.Modifier","LightningRod.Modifier",
                "Experience.MindControlSelfModifier","Experience.SpawnOwnerModifier","BallisticScatter.Max","BallisticScatter.Min"
            };
        }
        public static class DrawingEngine
        {
            public const int MapMaxHeight = 13;
            public const int MapMaxHeightDrawing = 17;
            public const int WaypointHeightMultiplier = 100;
            public const int CelltagHeightMultiplier = 99;
            public const int SelectRectangleMultiplier = 20;
            public const int WaypointNumWidth = 13;
            public static class Tiles
            {
                public const int Cliff = 15;
                public const int Water = 9;
                public const int Shore = 10;
                public const int Track = 6;
                public const int Roads = 12;
                public const int LAT_P = 11;
                public const int LAT_D = 14;
                public const int Clear = 13;
                public static readonly int[] Buildables = { Roads, LAT_D, LAT_P, Clear };
                public static readonly int[] Passable = { Track, Roads, LAT_P, LAT_D, Clear, Shore, Water };
            }
        }
        public static class GameRunning
        {
            public const int MaxGameSpeed = 6;
        }
    }
}
