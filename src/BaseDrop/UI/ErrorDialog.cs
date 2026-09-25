using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BaseDrop
{
    // Replacement for MessageBox that follows the app's color mode
    public partial class ErrorDialog : Form
    {
        private ErrorDialog(string Message, IEnumerable<string> Details, Icon DialogIcon)
        {
            InitializeComponent();
            IconBox.Image = DialogIcon.ToBitmap();
            MessageLabel.Text = Message;
            if (Details != null)
            {
                // One per line, the box scrolls if there are many
                DetailsBox.Text = string.Join(Environment.NewLine, Details);
            }
            else
            {
                // Just the message, drop the details box
                DetailsBox.Visible = false;
                ClientSize = new Size(ClientSize.Width, ClientSize.Height - DetailsBox.Height - 8);
            }
        }

        public static void ShowError(IWin32Window Owner, string Message, IEnumerable<string> Details = null)
        {
            using (var Dialog = new ErrorDialog(Message, Details, SystemIcons.Error))
                Dialog.ShowDialog(Owner);
        }

        public static void ShowWarning(IWin32Window Owner, string Message, IEnumerable<string> Details = null)
        {
            using (var Dialog = new ErrorDialog(Message, Details, SystemIcons.Warning))
                Dialog.ShowDialog(Owner);
        }
    }
}
