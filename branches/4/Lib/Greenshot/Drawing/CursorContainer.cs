﻿/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Greenshot.Plugin.Drawing;

namespace Greenshot.Drawing {
	/// <summary>
	/// Description of CursorContainer.
	/// </summary>
	[Serializable()] 
	public class CursorContainer : DrawableContainer, ICursorContainer {
		private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(CursorContainer));

		protected Cursor cursor;

		public CursorContainer(Surface parent) : base(parent) {
		}

		public CursorContainer(Surface parent, string filename) : base(parent) {
			Load(filename);
		}

		public Cursor Cursor {
			set {
				if (cursor != null) {
					cursor.Dispose();
				}
				// Clone cursor (is this correct??)
				cursor = new Cursor(value.CopyHandle());
				Width = value.Size.Width;
				Height = value.Size.Height;
			}
			get { return cursor; }
		}

		/**
		 * Destructor
		 */
		~CursorContainer() {
			Dispose(false);
		}

		/**
		 * The public accessible Dispose
		 * Will call the GarbageCollector to SuppressFinalize, preventing being cleaned twice
		 */
		public new void Dispose() {
			Dispose(true);
			base.Dispose();
			GC.SuppressFinalize(this);
		}

		// The bulk of the clean-up code is implemented in Dispose(bool)

		/**
		 * This Dispose is called from the Dispose and the Destructor.
		 * When disposing==true all non-managed resources should be freed too!
		 */
		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				if (cursor != null) {
					cursor.Dispose();
				}
			}
			cursor = null;
		}

		public void Load(string filename) {
			if (File.Exists(filename)) {
				using (Cursor fileCursor = new Cursor(filename)) {
					Cursor = fileCursor;
					LOG.Debug("Loaded file: " + filename + " with resolution: " + Height + "," + Width);
				}
			}
		}

		public override void Draw(Graphics graphics, RenderMode rm) {
			if (cursor != null) {
				cursor.DrawStretched(graphics, Bounds);
			}
		}
	}
}
