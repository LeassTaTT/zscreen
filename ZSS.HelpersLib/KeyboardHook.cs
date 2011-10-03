#region License Information (GPL v2)

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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HelpersLib
{
    public class KeyboardHook : IDisposable
    {
        public event KeyEventHandler KeyDown, KeyUp;

        private NativeMethods.HookProc keyboardHookProc;
        private IntPtr keyboardHookHandle = IntPtr.Zero;

        public KeyboardHook()
        {
            keyboardHookProc = KeyboardHookProc;
            keyboardHookHandle = NativeMethods.SetHook(NativeMethods.WH_KEYBOARD_LL, keyboardHookProc);
        }

        ~KeyboardHook()
        {
            Dispose();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                bool handled = false;

                switch ((KeyEvent)wParam)
                {
                    case KeyEvent.WM_KEYDOWN:
                    case KeyEvent.WM_SYSKEYDOWN:
                        handled = OnKeyDown(lParam);
                        break;
                    case KeyEvent.WM_KEYUP:
                    case KeyEvent.WM_SYSKEYUP:
                        handled = OnKeyUp(lParam);
                        break;
                }

                if (handled)
                {
                    return keyboardHookHandle;
                }
            }

            return NativeMethods.CallNextHookEx(keyboardHookHandle, nCode, wParam, lParam);
        }

        private bool OnKeyDown(IntPtr key)
        {
            if (KeyDown != null)
            {
                KeyEventArgs keyEventArgs = GetKeyEventArgs(key);
                KeyDown(null, keyEventArgs);
                return keyEventArgs.Handled || keyEventArgs.SuppressKeyPress;
            }

            return false;
        }

        private bool OnKeyUp(IntPtr key)
        {
            if (KeyUp != null)
            {
                KeyEventArgs keyEventArgs = GetKeyEventArgs(key);
                KeyUp(null, keyEventArgs);
                return keyEventArgs.Handled || keyEventArgs.SuppressKeyPress;
            }

            return false;
        }

        private KeyEventArgs GetKeyEventArgs(IntPtr key)
        {
            Keys keyData = (Keys)Marshal.ReadInt32(key) | Control.ModifierKeys;
            return new KeyEventArgs(keyData);
        }

        public void Dispose()
        {
            NativeMethods.UnhookWindowsHookEx(keyboardHookHandle);
        }
    }
}