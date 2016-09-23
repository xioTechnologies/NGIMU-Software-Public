using System.Windows.Forms;
using NgimuApi;

namespace NgimuGui
{
    class ConnectionManagerInfo
    {
        public IConnectionInfo ConnectionInfo { get; set; }

        public ToolStripMenuItem MenuItem { get; set; }

        public bool IsValid { get; set; }
    }
}
