﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.IniSystem;
using RelertSharp.GUI.Model.BrushModel;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel : UserControl
    {
        internal event EventHandler BrushObjectSelected;


        private Map Map { get { return CurrentMapDocument.Map; } }
        private BrushModel brush = new BrushModel();
        private bool drew = false;
        private bool shiftHidden = false;


        public PickPanel()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            SetLanguage();
            InitializeGeneralPanel();

            initialized = true;
        }
        public void DrawComplete()
        {
            drew = true;
        }
        public void ShiftHide()
        {
            if (!shiftHidden)
            {
                shiftHidden = true;
                Result.BrushObject.Dispose();
                Engine.Refresh();
            }
        }
        public void ShiftUnHide()
        {
            if (shiftHidden)
            {
                shiftHidden = false;
                Result.RedrawBrushObject();
                Engine.Refresh();
            }
        }
        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }

        public BrushModel Result { get { return brush; } }
    }
}
