using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Utils
{
    public class Misc
    {
        public static bool IsList(string s)
        {
            return s.Contains(",");
        }
        public static bool IsPercentFloat(string s)
        {
            return s.Contains("%");
        }
        public static string[] Split(string s, char c, int t = 1)
        {
            string[] tmp = s.Split(new char[1] { c }, t+1);
            return tmp;
        }
    }
    public static class Cons
    {
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
        }
        public static readonly string[] BoolTrue = { "yes", "True", "true" };
        public static readonly string[] BoolFalse = { "no", "False", "false" };
        public static readonly string[] NullString = { "<none>", "None", "none" };
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
                "WeaponCount","Survivor.RookiePassengerChance","Survivor.VeteranPassengerChance","Survivor.ElitePassengerChance","TechLevel"
            };
            public static readonly string[] ActiveBoolLike =
            {
                "AffectsAllies","AffectsOwner","Bounty.Display","Bright","Deployer","DeployFire","ImmuneToPsionics","ImmuneToRadiation","Malicious","OmniFire","OpportunityFire",
                "RadarInvisible","Rocker","Selectable","Trainable","Wall","Wood","Bounty","CanBeReversed","DefaultToGuardArea","IsSelectableCombatant","OpenTopped","NoManualUnload",
                "NoManualEnter","DontScore","Insignificant","ProtectedDriver","Turret","Crusher","Accelerates","ImmuneToPsionicWeapons","VehicleThief.Allowed","IsSimpleDeployer",
                "Arcing","SubjectToCliffs","SubjectToElevation","SubjectToWalls","SubjectToBuildings","Shadow","AA","AG"
            };
            public static readonly string[] PassiveBoolLike =
            {
                
            };
            public static readonly string[] AcquireBoolLike =
            {
                "Fearless"
            };
            public static readonly string[] ListLike =
            {
                "DebrisMaximums","Anim","Prerequisite","Prerequisite.StolenTechs","InitialPayload.Nums","Owner","AnimList"
            };
            public static readonly string[] NameLike =
            {
                "Name", "VoiceSecondaryWeaponAttack","ElitePrimary","EliteSecondary","Armor","VoiceDeploy","Promote.VeteranSound","Promote.EliteSound","Secondary","Primary",
                "UIName","DeathWeapon","Passengers.Allowed","InitialPayload.Types","Image","AttachEffect.Animation","Locomotor","Parachute.Anim","Convert.Deploy","Category",
                "CrushSound","DamageSound","Insignia.Rookie","Insignia.Veteran","Insignia.Elite","Projectile","Warhead","Report","OccupantAnim","AssaultAnim",
                "PreImpactAnim","OpenTransportWeapon","VoiceSpecialAttack","IFVMode","ReversedAs","Cursor.Deploy","GroupAs","OpenToppedAnim","SW.AITargeting","InfDeathAnim",
                "InfDeath"
            };
            public static readonly string[] MultiplierLike =
            {
                "BuildTimeMultiplier","DeathWeaponDamageModifier","IronCurtain.Modifier","ForceShield.Modifier","EMP.Modifier","LightningRod.Modifier",
                "Experience.MindControlSelfModifier","Experience.SpawnOwnerModifier","BallisticScatter.Max","BallisticScatter.Min"
            };
        }



        public enum Language
        {
            EnglishUS, Chinese
        }
        public enum FileExtension
        {
            Undefined,CSV,TXT,YRM,MAP,INI,LANG,
            UnknownBinary
        }
    }
}
