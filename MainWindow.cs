using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using relert_sharp.DrawingEngine;

namespace relert_sharp
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        #region EventHandler - MainWindow
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            pnlInMain_Resize(this, EventArgs.Empty);
            GL.ClearColor(Color.Crimson);
        }

        #endregion
    }
}
