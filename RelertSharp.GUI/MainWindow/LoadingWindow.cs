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
            Thread.Sleep(500);
        }
        public void ResetBar()
        {
            ResetValue(progMain);
        }
        public void Begin()
        {
            SetText("Loading mix...", lblCurrentStatus);
        }

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
