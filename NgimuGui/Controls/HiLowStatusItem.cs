using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NgimuGui.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
                                        ToolStripItemDesignerAvailability.ContextMenuStrip |
                                        ToolStripItemDesignerAvailability.StatusStrip)]
    class HiLowStatusItem : ToolStripControlHost
    {
        private AdvancedRadioButton m_RadioButton;

        public new string Text
        {
            get { return m_RadioButton.Text; }
            set { m_RadioButton.Text = value; }
        }

        public bool Readonly
        {
            get { return !m_RadioButton.Enabled; }
            set { m_RadioButton.Enabled = !value; }
        }

        public bool Checked
        {
            get { return m_RadioButton.Checked; }
            set { m_RadioButton.Checked = value; }
        }

        public string GroupName
        {
            get { return m_RadioButton.GroupName; }
            set { m_RadioButton.GroupName = value; }
        }

        public new event EventHandler Click;

        public HiLowStatusItem()
            : base(new AdvancedRadioButton())
        {
            m_RadioButton = Control as AdvancedRadioButton;
            m_RadioButton.MouseClick += m_RadioButton_MouseClick;
            //m_RadioButton.Click += m_RadioButton_Click;
        }

        void m_RadioButton_Click(object sender, EventArgs e)
        {
            if (Click != null)
            {
                Click(sender, e);
            }
        }

        void m_RadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (Click != null)
            {
                Click(sender, e);
            }
        }
    }
}
