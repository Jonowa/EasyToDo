using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyToDo.Controls
{
    class EasyTextBox : TextBox
    {
        //
        // Thanks to Ryan Farley for the following code snippets
        // http://ryanfarley.com/blog/archive/2004/04/07/511.aspx
        //

        private const int EM_GETLINECOUNT = 0xBA;
        private const int EM_LINEINDEX = 0xBB;
        private const int EM_LINELENGTH = 0xC1;

        public int LineCount
        {
            get {
                Message msg = Message.Create(this.Handle, EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero);
                base.DefWndProc(ref msg);
                return msg.Result.ToInt32();
            }
        }

        public int LineIndex(int Index)
        {
            Message msg = Message.Create(this.Handle, EM_LINEINDEX, (IntPtr)Index, IntPtr.Zero);
            base.DefWndProc(ref msg);
            return msg.Result.ToInt32();
        }

        public int LineLength(int Index)
        {
            Message msg = Message.Create(this.Handle, EM_LINELENGTH, (IntPtr)Index, IntPtr.Zero);
            base.DefWndProc(ref msg);
            return msg.Result.ToInt32();
        }
    }
}
