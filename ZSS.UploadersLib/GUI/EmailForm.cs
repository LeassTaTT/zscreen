﻿#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.Windows.Forms;

namespace UploadersLib.GUI
{
    public partial class EmailForm : Form
    {
        public string ToEmail { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public EmailForm()
        {
            InitializeComponent();
        }

        public EmailForm(string toEmail, string subject, string body)
            : this()
        {
            txtToEmail.Text = toEmail;
            txtSubject.Text = subject;
            txtMessage.Text = body;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ToEmail = txtToEmail.Text;
            Subject = txtSubject.Text;
            Body = txtMessage.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}