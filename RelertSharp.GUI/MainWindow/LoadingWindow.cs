using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace RelertSharp.GUI
{
    internal partial class LoadingWindow : Form
    {
        public enum LoadingFlag
        {
            Tiles = 1,
            Overlays = 2,
            Smudges = 3,
            Terrains = 4,
            Units = 5,
            Infantries = 6,
            Buildings = 7,
            Aircrafts = 8,
            BaseNodes = 9,
            Waypoints = 10,
            Celltags = 11
        }
        private delegate void DSetText(string lblText, Label dest);
        private delegate void DSetValue(int nValue, ProgressBar pgb);
        private delegate void DIncreValue(ProgressBar pgb);
        private string progressText = "Current";
        private int maxValue, currentValue;
        private bool isSleeping;


        public LoadingWindow()
        {
            InitializeComponent();
        }
        public void StartDrawing(int maxCount, string type)
        {
            ResetBar();
            progressText = "Drawing " + type + "...({0}/{1})";
            SetMaxValue(maxCount, progMain);
            Refresh();
        }
        public void Incre()
        {
            currentValue++;
            IncreValue(progMain);
            if (!isSleeping)
            {
                progMain.Refresh();
                SetText(string.Format(progressText, currentValue, maxValue), lblCurrentStatus);
                Task.Run(() =>
                {
                    isSleeping = true;
                    Thread.Sleep(17);
                    isSleeping = false;
                });
            }
        }
        public void EndDrawing()
        {
            SetText("Finished", lblCurrentStatus);
            progMain.Maximum = 1;
            progMain.Value = 1;
            Refresh();
            Thread.Sleep(1000);
        }
        public void ResetBar()
        {
            ResetValue(progMain);
        }
        public void Begin()
        {
            SetText("Loading mix...", lblCurrentStatus);
        }
        #region Items
        public void EndItems(LoadingFlag flag)
        {
            string item = "";
            Label dest = null;
            PictureBox pbx = null;
            switch (flag)
            {
                case LoadingFlag.Tiles:
                    item = "Tiles";
                    dest = lblTile;
                    pbx = pbxTiles;
                    break;
                case LoadingFlag.Overlays:
                    item = "Overlays";
                    dest = lblOverlays;
                    pbx = pbxOverlays;
                    break;
                case LoadingFlag.Infantries:
                    item = "Infantries";
                    dest = lblInfantries;
                    pbx = pbxInfantries;
                    break;
                case LoadingFlag.Units:
                    item = "Units";
                    dest = lblUnits;
                    pbx = pbxUnits;
                    break;
                case LoadingFlag.Buildings:
                    item = "Buildings";
                    dest = lblBuildings;
                    pbx = pbxBuildings;
                    break;
                case LoadingFlag.Aircrafts:
                    item = "Aircrafts";
                    dest = lblAircrafts;
                    pbx = pbxAircrafts;
                    break;
                case LoadingFlag.Terrains:
                    item = "Terrains";
                    dest = lblTerrains;
                    pbx = pbxTerrains;
                    break;
                case LoadingFlag.Smudges:
                    item = "Smudges";
                    dest = lblSmudges;
                    pbx = pbxSmudges;
                    break;
                case LoadingFlag.BaseNodes:
                    item = "BaseNodes";
                    dest = lblNodes;
                    pbx = pbxNode;
                    break;
                case LoadingFlag.Waypoints:
                    item = "Waypoints";
                    dest = lblWaypoints;
                    pbx = pbxWaypoints;
                    break;
                case LoadingFlag.Celltags:
                    item = "Celltags";
                    dest = lblCelltags;
                    pbx = pbxCelltags;
                    break;
            }
            SetText(string.Format("{0} {1} loaded", maxValue, item), dest);
            SetPbxComplete(pbx);

        }
        #endregion



        #region Base
        private void StartThread(Action action)
        {
            Task.Run(action);
        }
        private void SetText(string text, Label dest)
        {
            //BaseInvoke(new DSetText(SetText), dest, new object[] { text, dest }, () =>
            //  {
            //      dest.Text = text;
            //      dest.Refresh();
            //  });
            dest.Text = text;
            dest.Refresh();
        }
        private void IncreValue(ProgressBar dest)
        {
            //BaseInvoke(new DIncreValue(IncreValue), dest, new object[] { dest }, () =>
            //{
            //    dest.Increment(1);
            //});
            dest.Increment(1);
        }
        private void SetMaxValue(int nValue, ProgressBar dest)
        {
            //BaseInvoke(new DSetValue(SetMaxValue), dest, new object[] { nValue, dest }, () =>
            //  {
            //      maxValue = nValue;
            //      dest.Maximum = nValue;
            //      dest.Refresh();
            //  });
            maxValue = nValue;
            dest.Maximum = nValue;
            dest.Refresh();
        }
        private void SetPbxComplete(PictureBox dest)
        {
            dest.Image = Properties.Resources.load1;
            dest.Refresh();
        }
        private void ResetValue(ProgressBar dest)
        {
            //BaseInvoke(new DIncreValue(ResetValue), dest, new object[] { dest }, () =>
            // {
            //     currentValue = 0;
            //     maxValue = 0;
            //     dest.Value = 0;
            //     dest.Refresh();
            // });
            currentValue = 0;
            maxValue = 0;
            dest.Value = 0;
            dest.Refresh();
        }
        private void BaseInvoke(Delegate d, Control dest, object[] parameters, Action a)
        {
            if (dest.InvokeRequired)
            {
                dest.Invoke(d, parameters);
            }
            else
            {
                a.Invoke();
            }
        }
        #endregion
    }
}
