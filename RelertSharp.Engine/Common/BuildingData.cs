namespace RelertSharp.Engine.Common
{
    internal struct BuildingData
    {
        public string SelfId { get; set; }
        public short nSelf { get; set; }
        public string ActivateAnim { get; set; }
        public string ActivateAnimTwo { get; set; }
        public string ActivateAnimThree { get; set; }
        public string ActivateAnimFour { get; set; }
        public short nActivateAnim { get; set; }
        public short nActivateAnimTwo { get; set; }
        public short nActivateAnimThree { get; set; }
        public short nActivateAnimFour { get; set; }
        public int ActiveAnimZAdjust { get; set; }
        public int ActiveAnimTwoZAdjust { get; set; }
        public int ActiveAnimThreeZAdjust { get; set; }
        public int ActiveAnimFourZAdjust { get; set; }

        public string IdleAnim { get; set; }
        public short nIdleAnim { get; set; }
        public string BibAnim { get; set; }
        public short nBibAnim { get; set; }
        public string ProductionAnim { get; set; }
        public short nProductionAnim { get; set; }
        public int IdleAnimZAdjust { get; set; }

        public string SpecialAnim { get; set; }
        public string SpecialAnimTwo { get; set; }
        public string SpecialAnimThree { get; set; }
        public string SpecialAnimFour { get; set; }
        public short nSpecialAnim { get; set; }
        public short nSpecialAnimTwo { get; set; }
        public short nSpecialAnimThree { get; set; }
        public short nSpecialAnimFour { get; set; }

        public string SuperAnim { get; set; }
        public string SuperAnimTwo { get; set; }
        public string SuperAnimThree { get; set; }
        public string SuperAnimFour { get; set; }
        public short nSuperAnim { get; set; }
        public short nSuperAnimTwo { get; set; }
        public short nSuperAnimThree { get; set; }
        public short nSuperAnimFour { get; set; }
        public int SuperAnimZAdjust { get; set; }

        public string TurretAnim { get; set; }
        public short nTurretAnim { get; set; }
        public bool TurretAnimIsVoxel { get; set; }
        public string TurretBarrel { get; set; }
        public int TurretAnimZAdjust { get; set; }

        public string AlphaImage { get; set; }

        public bool IsEmpty { get; set; }

        public int Plug1ZAdjust { get; set; }
        public int Plug2ZAdjust { get; set; }
        public int Plug3ZAdjust { get; set; }
    }
}
