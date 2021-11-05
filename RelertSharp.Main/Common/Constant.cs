using System;
using System.Collections.Generic;

namespace RelertSharp.Common
{
    public static class Constant
    {
        //public const string ReleaseDate = " - ver. 2020.11.21-1907 - UNSTABLE";
        public const int BASE_HASH = 19260817;
        public static string VersionInfo
        {
            get
            {
                string v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
                string type = "Relert Sharp - DEBUG";
#endif
#if RELEASE
                string type = "Relert Sharp - Preview Version";
#endif
                string r = string.Format("{0} {1}", type, v);
                return r;
            }
        }
        public static class RulesHead
        {
            public const string HEAD_BUILDING = "BuildingTypes";
            public const string HEAD_INFANTRY = "InfantryTypes";
            public const string HEAD_VEHICLE = "VehicleTypes";
            public const string HEAD_AIRCRAFT = "AircraftTypes";
            public const string HEAD_TERRAIN = "TerrainTypes";
            public const string HEAD_SMUDGE = "SmudgeTypes";
            public const string HEAD_OVERLAY = "OverlayTypes";
            public const string HEAD_COUNTRY = "Countries";
            public const string HEAD_TIBERIUM = "Tiberiums";
            public const string HEAD_GLOBVAR = "VariableNames";
            public const string HEAD_INCLUDE = "#include";
        }

        public const string KEY_IMAGE = "Image";
        public const string KEY_ALPHA = "AlphaImage";
        public const string KEY_NAME = "Name";
        public const string KEY_GROUP = "Group";
        public const string KEY_AUTHOR = "Author";
        public const string KEY_CUSPAL = "CustomPalette";
        public const string KEY_NEXTANIM = "Next";
        public const string KEY_TRAILER = "TrailerAnim";
        public const string KEY_TRALSEP = "TrailerSeperation";

        public const int IniMaxLineLength = 512;
        public const int OVERLAY_XY_MAX = 512;
        public const string ITEM_NONE = "<none>";
        public const string VALUE_NONE = "None";
        public const string ITEM_ALL = "<all>";
        public const string ID_INVALID = "-1";
        public const string FMT_OWNER = "#OWNER#";
        public const string EX_SHP = ".shp";
        public const string EX_VXL = ".vxl";
        public const string EX_HVA = ".hva";
        public const string EX_INI = ".ini";
        public const string EX_MIX = ".mix";
        public const string SUFF_BARREL = "barr";
        public const string SUFF_TURRET = "tur";
        public const string NULL_ICON = "xxicon.shp";
        public const string CLONE_SUFFIX = " - Clone";
        public const string TAG_SUFFIX = " - Tag";
        public const string DefaultHouseName = "US Default";
        public const string FMT_HOUSE = "{0} House";

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
            public const int ArgLenLightSource = 7;
            public const int CAPACITY_LOCALVAR = 100;
            public const int INVALID_HEIGHT = -1;
            public const string CivilianCountrySide = "Civilian";
            public const string DefaultCivilianHouse = "Neutral";

            public const string ENT_HOUSE = "House";
            public const string ENT_SCRIPT = "Script";
            public const string ENT_TASKFORCE = "TaskForce";
            public const string KEY_GROUP = "Group";
            public const string ENT_RANK = "Ranking";
            public const string KEY_RANK_ET = "ParTimeEasy";
            public const string KEY_RANK_MT = "ParTimeMedium";
            public const string KEY_RANK_HT = "ParTimeHard";
            public const string KEY_RANK_OT = "OverParTitle";
            public const string KEY_RANK_OS = "OverParMessage";
            public const string KEY_RANK_UT = "UnderParTitle";
            public const string KEY_RANK_US = "UnderParMessage";

            public static class CustomComponents
            {
                public const string LightsourceTitle = "Rs_LightSource";
            }
        }
        public static class EntName
        {
            public const string RsLightCompileHead = "GARSLSRC";
            public const string RsLightCompileName = "RS Compiled LightSource No.";
            public const string RsLightTocIndex = "RSCLT";
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
        public static readonly string[] BoolTrue = { "yes", "Yes", "True", "true", "1" };
        public static readonly string[] BoolFalse = { "no", "No", "False", "false", "0" };
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
            public const int MapMaxHeight = 14;
            public const int MapMaxHeightDrawing = 17;
            public const int WaypointHeightMultiplier = 150;
            public const int CelltagHeightMultiplier = 99;
            public const int LightSourceHeightMultiplier = 110;
            public const int SelectRectangleMultiplier = 200;
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
            public static class ZAdjust
            {
                public const float HiBridgeZAdjust = -60;
                public const float Shadow = 1;
                public const float VxlSelf = -1;
                public const float VxlBarrel = -3;
                public const float VxlTurret = -2;
                public const float Waypoint = -100f;
                public const float CellTag = -99f;
                public const float SelectingRectangle = -1000f;
            }
            public static class Offset
            {
                public static float Ground = -5f;
                public static float ShadowSelf = -2f;
                public static float Smudge = -3f;

                public static float ShadowAAnim1 = -1.75f;
                public static float ShadowAAnim2 = -1.7f;
                public static float ShadowAAnim3 = -1.65f;
                public static float ShadowIdle = -1.6f;
                public static float ShadowSuper = -1.55f;
                public static float ShadowBarrel = -1.5f;
                public static float ShadowTurret = -3.45f;
                public static float ShadowBib = -1.4f;
                public static float ShadowPlug3 = -1.35f;
                public static float ShadowPlug2 = -1.3f;
                public static float ShadowPlug1 = -1.25f;

                public static float Self = 3f;
                public static float AAnim1 = 3.15f;
                public static float AAnim2 = 3.2f;
                public static float AAnim3 = 3.25f;
                public static float Idle = 3.3f;
                public static float Super = 3.35f;
                public static float Turret = 3.4f;
                public static float Barrel = 3.5f;
                public static float Bib = 3.55f;
                public static float Plug1 = 3.6f;
                public static float Plug2 = 3.65f;
                public static float Plug3 = 3.7f;

                public static float BaseNode = 6;
            }
        }
        public static class GameRunning
        {
            public const int MaxGameSpeed = 6;
        }
        public static class Config
        {
            public const string Path = "./configs/default.xml";
            public const string UserConfig = "user.xml";
            public const string TYPE_BOOL = "bool";
            public const string TYPE_INT = "int";
            public const string TYPE_STRING = "string";
            public const string PARAM_DEF = "0";
            public const string PARAM_LAST = "A";
            public static class DefaultComboType
            {
                public const string TYPE_MAP_OBJSTATE = "MapUnitState";
                public const string TYPE_MAP_SPOTLIGHT = "SpotlightType";
                public const string TYPE_WAYPOINT = "Waypoints";
                public const string TYPE_HOUSES = "Houses";
                public const string TYPE_IDX_HOUSE = "HouseIndex";
                public const string TYPE_OWNERCOUNTRY = "OwnerCountry";
                public const string TYPE_SCRIPTS = "Scripts";
                public const string TYPE_TEAMS = "Teams";
                public const string TYPE_TASKFORCES = "Taskforces";
                public const string TYPE_TRIGGERS = "Triggers";
                public const string TYPE_TECHTYPE = "TechType";
                public const string TYPE_TAGS = "Tags";
                public const string TYPE_SIDEINDEX = "SideIndex";
                public const string TYPE_BUDUPG = "BuildingUpgrade";
                public const string TYPE_GLOBAL = "GlobalVar";
                public const string TYPE_LOCAL = "LocalVar";
                public const string TYPE_BUDREG = "BuildingReg";
                public const string TYPE_IDX_UNT = "UnitIndex";
                public const string TYPE_IDX_INF = "InfantryIndex";
                public const string TYPE_IDX_AIR = "AircraftIndex";
                public const string TYPE_IDX_ANIM = "AnimationIndex";
                public const string TYPE_IDX_PRTC = "ParticalAnimIndex";
                public const string TYPE_IDX_WEP = "WeaponIndex";
                public const string TYPE_IDX_VOX = "VoxelAnimIndex";
                public const string TYPE_REG_SUP = "SuperWeaponReg";
                public const string TYPE_IDX_SUP = "SuperWeaponIndex";
                public const string TYPE_MOVIE = "Movies";
                public const string TYPE_REG_SND = "SoundReg";
                public const string TYPE_REG_THM = "ThemeReg";
                public const string TYPE_REG_EVA = "EvaReg";
                public const string TYPE_CSF = "CsfLabel";
            }
        }
    }
}
