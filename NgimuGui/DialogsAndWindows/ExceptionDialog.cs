using System;

namespace NgimuGui.DialogsAndWindows
{
    public partial class ExceptionDialog : BaseForm
    {
        public string Title { get; set; }

        public string Label { get; set; }

        public string Detail { get; set; }

        public ExceptionDialog()
        {
            InitializeComponent();
        }

        private void ExceptionDialog_Load(object sender, EventArgs e)
        {
            this.Text = Title;
            textBox1.Text = Label;
            textBox2.Text = String.IsNullOrEmpty(Detail) == true ? String.Empty : Detail;
        }
    }
}
