using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure.Objects;
using RelertSharp.Common;

namespace RelertSharp.GUI.RbPanel
{
    public partial class RbPanelAttribute : RbPanelBase
    {
        private AttributeChanger changer = new AttributeChanger();
        public RbPanelAttribute()
        {
            InitializeComponent();
            SetFacing(0);
        }

        #region Facing
        private Point pboxCenter = Point.Empty;
        private bool facingActivated = false;
        private int previowsFacing = -1;
        private void pboxFacing_MouseDown(object sender, MouseEventArgs e)
        {
            pboxCenter = Point.Empty;
            pboxCenter.X += pboxFacing.Size.Width / 2;
            pboxCenter.Y += pboxFacing.Size.Height / 2;
            facingActivated = true;
        }
        private void pboxFacing_MouseMove(object sender, MouseEventArgs e)
        {
            if (facingActivated)
            {
                int dy = e.Location.Y - pboxCenter.Y;
                int dx = e.Location.X - pboxCenter.X;
                double deg = -Math.Atan2(dy, dx) / Math.PI * 180f;
                if (22.5d <= deg && deg < 67.5d)
                {
                    SetFacing(0);
                }
                else if (67.5d <= deg && deg < 112.5d)
                {
                    SetFacing(224);
                }
                else if (112.5d <= deg && deg < 157.5d)
                {
                    SetFacing(192);
                }
                else if ((157.5d <= deg && deg < 180) || (-157.5f >= deg))
                {
                    SetFacing(160);
                }
                else if (-157.5d <= deg && deg < -112.5d)
                {
                    SetFacing(128);
                }
                else if (-112.5d <= deg && deg < -67.5d)
                {
                    SetFacing(96);
                }
                else if (-67.5d <= deg && deg < -22.5d)
                {
                    SetFacing(64);
                }
                else
                {
                    SetFacing(32);
                }
            }
        }
        private void pboxFacing_MouseUp(object sender, MouseEventArgs e)
        {
            facingActivated = false;
        }

        private void SetFacing(int facing)
        {
            if (previowsFacing == facing) return;
            else previowsFacing = facing;
            Image facingBase = Properties.Resources.rotationBase;
            Image arrow;
            switch (facing)
            {
                case 224:
                    arrow = Properties.Resources.Arrow0;
                    break;
                case 0:
                    arrow = Properties.Resources.Arrow1;
                    break;
                case 32:
                    arrow = Properties.Resources.Arrow2;
                    break;
                case 64:
                    arrow = Properties.Resources.Arrow3;
                    break;
                case 96:
                    arrow = Properties.Resources.Arrow4;
                    break;
                case 128:
                    arrow = Properties.Resources.Arrow5;
                    break;
                case 160:
                    arrow = Properties.Resources.Arrow6;
                    break;
                default:
                    arrow = Properties.Resources.Arrow7;
                    break;
            }
            Changer.Host.Rotation = facing;
            Graphics g = Graphics.FromImage(facingBase);
            g.DrawImage(arrow, Point.Empty);
            g.Dispose();
            pboxFacing.Image = facingBase;
            toolTip.SetToolTip(pboxFacing, Changer.Host.Rotation.ToString());
        }

        #endregion



        public AttributeChanger Changer
        {
            get
            {
                changer.bGroup = ckbGroup.Checked;
                changer.bHealthPoint = ckbHP.Checked;
                changer.bOwnerHouse = ckbOwnerHouse.Checked;
                changer.bRotation = ckbFacing.Checked;
                changer.bStatus = ckbStat.Checked;
                changer.bTaggedTrigger = ckbTags.Checked;
                changer.bVeteran = ckbVeteran.Checked;
                return changer;
            }
        }
    }
}
