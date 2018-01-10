using System;
using System.Threading;
using System.Windows.Forms;
using NgimuApi;
using NgimuForms.Controls;

namespace NgimuSynchronisedNetworkManager
{
    public class ConnectionRow
    {
        public object[] Cells;
        public DateTime[] Timestamps;

        public Connection Connection { get; set; }

        public DataGridViewRow Row { get; set; }

        public ConnectionIcon Icon { get; set; } = ConnectionIcon.Information;
        public IconInfo Error { get; set; }
        public IconInfo Warning { get; set; }
        public IconInfo Information { get; set; }

        private int needsToRedrawIcons = 0;

        public bool CheckForIconRedraw()
        {
            return Interlocked.Exchange(ref needsToRedrawIcons, 0) != 0;
        }

        public void SetError(string message)
        {
            Error.Message = message;
            Error.Visible = true;

            Interlocked.Exchange(ref needsToRedrawIcons, 1);
        }

        public void SetWarning(string message)
        {
            Warning.Message = message;
            Warning.Visible = true;

            Interlocked.Exchange(ref needsToRedrawIcons, 1);
        }

        public void SetInformation(string message)
        {
            Information.Message = message;
            Information.Visible = true;

            Interlocked.Exchange(ref needsToRedrawIcons, 1);
        }
    }
}