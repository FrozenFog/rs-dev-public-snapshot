using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public enum VerifyAlertLevel
    {
        Success = 0,
        Suggest = 1,
        Warning = 2,
        Critical = 3,
        CannotBeSaved = 4
    }
    public enum VerifyType
    {
        CelltagIdNotFound = 0,
        SigleCellOverlap = 1,
        BuildingBasePosOverlap = 2,
        CombatObjectOverlap = 3,
        TaskforceEmpty = 4,
        ScriptEmpty = 5,
        TeamInvalidTaskforce = 6,
        TeamInvalidScript = 7,
        CombatObjectLowHealth = 8,
        TaskforceOverflow = 9,
        TaskforceMemberRepeated = 10,
        TriggerHasNoTagAsso = 11,
        TagHasNoTrigger = 12,
        TriggerHasNoAction = 13,
        TriggerHasNoEvent = 14,
        TriggerParameterInvalid = 15,
        CombatObjectInvalid = 16,
        TriggerEventOverflow = 17,
        TriggerActionOverflow = 18
    }

    public class VerifyResultItem
    {
        public override string ToString()
        {
            return Message;
        }
        public string Message { get; set; }
        public VerifyAlertLevel Level { get; set; }
        public VerifyType VerifyType { get; set; }
        public I2dLocateable Pos { get; set; }
        public string IdNavigator { get; set; }
        public LogicType? LogicType { get; set; }
    }
}
