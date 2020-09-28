using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    public partial class BaseAttributeForm : Form
    {
        protected AttributeChanger changer;
        protected bool initialized = false;
        protected AttributeChanger Changer
        {
            get
            {
                return changer;
            }
        }


        public BaseAttributeForm()
        {
            InitializeComponent();
            changer = new AttributeChanger(true);
        }
        protected void LoadCombobox(IEnumerable<HouseItem> houses, IEnumerable<TagItem> tags)
        {
            int houseMX, tagMX = 1;
            cbbOwnerHouse.Items.Clear();
            cbbTags.Items.Clear();
            cbbOwnerHouse.Items.AddRange(houses.ToArray());
            cbbTags.Items.Add(new TechnoPair("None", ""));
            cbbTags.Items.AddRange(tags.ToArray());
            cbbStatus.LoadAs(Constant.MapStructure.ObjectStatus);
            cbbOwnerHouse.UpdateTextAs(changer.Host.OwnerHouse);
            houseMX = houses.Max(x => x.ToString().Length) * 7;
            if (tags.Count() > 0) tagMX = tags.Max(x => x.ToString().Length) * 7;
            cbbOwnerHouse.DropDownWidth = houseMX;
            cbbTags.DropDownWidth = tagMX;
        }
        protected virtual void UpdateGuiFromHost()
        {
            SetFacing(changer.Host.Rotation);
            int hp = changer.Host.HealthPoint.TrimTo(1, 256);
            int veteran = changer.Host.VeterancyPercentage.TrimTo(0, 200);
            trkbHP.Value = hp;
            trkbVeteran.Value = veteran;
            mtxbHP.Text = changer.Host.HealthPoint.ToString();
            mtxbVeteran.Text = changer.Host.VeterancyPercentage.ToString();
            cbbTags.UpdateTextAs(changer.Host.TaggedTrigger);
            cbbStatus.UpdateTextAs(changer.Host.Status);
            cbbOwnerHouse.UpdateTextAs(changer.Host.OwnerHouse);
            txbGroup.Text = changer.Host.Group.ToString();
        }
        protected virtual void SetLanguage()
        {
            foreach (Control c in Controls)
            {
                Language.SetControlLanguage(c);
            }
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
                case 192:
                    arrow = Properties.Resources.Arrow7;
                    break;
                default:
                    arrow = Properties.Resources.Arrow1;
                    break;
            }
            changer.Host.Rotation = facing;
            Graphics g = Graphics.FromImage(facingBase);
            g.DrawImage(arrow, Point.Empty);
            g.Dispose();
            pboxFacing.Image = facingBase;
            toolTip.SetToolTip(pboxFacing, changer.Host.Rotation.ToString());
        }

        #endregion


        private void cbbOwnerHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                HouseItem h = cbbOwnerHouse.SelectedItem as HouseItem;
                if (h != null) changer.Host.OwnerHouse = h.ID;
            }
        }

        private bool trackerHpLock = false;
        private void mtxbHP_Validated(object sender, EventArgs e)
        {
            if (initialized)
            {
                trackerHpLock = true;
                int num = 0;
                try
                {
                    num = int.Parse(mtxbHP.Text);
                }
                catch
                {
                    num = 1;
                }
                if (num <= 0) num = 1;
                if (num > 256) num = 256;
                trkbHP.Value = num;
                changer.Host.HealthPoint = num;
                trackerHpLock = false;
            }
        }

        private void trkbHP_Scroll(object sender, EventArgs e)
        {
            if (!trackerHpLock && initialized)
            {
                mtxbHP.Text = trkbHP.Value.ToString();
                string percent = string.Format("{0}%", trkbHP.Value * 100 / 256);
                toolTip.SetToolTip(trkbHP, percent);
                changer.Host.HealthPoint = trkbHP.Value;
            }
        }

        private bool trackerVeteranLock = false;
        private void mtxbVeteran_Validated(object sender, EventArgs e)
        {
            if (initialized)
            {
                trackerVeteranLock = true;
                int num = 0;
                try
                {
                    num = int.Parse(mtxbVeteran.Text);
                }
                catch
                {
                    num = 0;
                }
                if (num < 0) num = 0;
                if (num > 200) num = 200;
                trkbVeteran.Value = num;
                changer.Host.VeterancyPercentage = num;
                trackerVeteranLock = false;
            }
        }

        private void trkbVeteran_Scroll(object sender, EventArgs e)
        {
            if (!trackerVeteranLock && initialized)
            {
                mtxbVeteran.Text = trkbVeteran.Value.ToString();
                string veteran = "Rookie";
                if (trkbVeteran.Value >= 100 && trkbVeteran.Value < 200) veteran = "Veteran";
                else if (trkbVeteran.Value == 200) veteran = "Elite";
                toolTip.SetToolTip(trkbVeteran, veteran);
                changer.Host.VeterancyPercentage = trkbVeteran.Value;
            }
        }

        private void cbbTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                TagItem tag = cbbTags.SelectedItem as TagItem;
                if (tag != null) changer.Host.TaggedTrigger = tag.ID;
            }
        }

        private void cbbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                changer.Host.Status = cbbStatus.Text;//TODO: stat list impl
            }
        }

        private void txbGroup_TextChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                int group = -1;
                try
                {
                    group = int.Parse(txbGroup.Text);
                }
                catch { }
                changer.Host.Group = group;
            }
        }
    }
}
