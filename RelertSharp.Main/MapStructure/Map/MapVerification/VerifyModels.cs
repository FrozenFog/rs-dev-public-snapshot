﻿using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        EmptyTaskforce = 4,
        EmptyScript = 5,
        TeamInvalidTaskforce = 6,
        TeamInvalidScript = 7,
        CombatObjectLowHealth = 8
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
        public LogicType LogicType { get; set; }
    }
}