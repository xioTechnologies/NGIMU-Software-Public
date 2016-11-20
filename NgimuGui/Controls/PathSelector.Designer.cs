namespace NgimuGui.Controls
{
    partial class PathSelector
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseButton = new System.Windows.Forms.Button();
            this.textbox = new NgimuGui.Controls.TextBoxWithHelpText();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(168, 0);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(24, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // textbox
            // 
            this.textbox.AllowDrop = true;
            this.textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox.HelpText = "";
            this.textbox.HelpTextColor = System.Drawing.Color.Gray;
            this.textbox.Location = new System.Drawing.Point(0, 2);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(162, 20);
            this.textbox.TabIndex = 0;
            this.textbox.TextChanged += new System.EventHandler(this.textbox_TextChanged);
            this.textbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textbox_DragDrop);
            this.textbox.DragEnter += new System.Windows.Forms.DragEventHandler(this.textbox_DragEnter);
            // 
            // PathSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.textbox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PathSelector";
            this.Size = new System.Drawing.Size(192, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBoxWithHelpText textbox;
        private System.Windows.Forms.Button browseButton;
    }
}
