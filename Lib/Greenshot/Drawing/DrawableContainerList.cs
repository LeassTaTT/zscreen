/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2012  Thomas Braun, Jens Klingen, Robin Krom
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
using System.Collections.Generic;
using System.Drawing;
using Greenshot.Drawing.Fields;
using Greenshot.Memento;
using Greenshot.Plugin;
using Greenshot.Plugin.Drawing;
using System.Windows.Forms;
using GreenshotPlugin.Core;
using Greenshot.IniFile;
using Greenshot.Configuration;
using Greenshot.Helpers;

namespace Greenshot.Drawing {
	/// <summary>
	/// Dispatches most of a DrawableContainer's public properties and methods to a list of DrawableContainers.
	/// </summary>
	[Serializable()] 
	public class DrawableContainerList : List<DrawableContainer> {
		private static CoreConfiguration conf = IniConfig.GetIniSection<CoreConfiguration>();
		private static System.ComponentModel.ComponentResourceManager editorFormResources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditorForm));

		public DrawableContainerList() {
		}
		
		public EditStatus Status {
			get { 
				return this[Count-1].Status; 
			}
			set {
				foreach(DrawableContainer dc in this) dc.Status = value; 
			}
		}
		
		public List<IDrawableContainer> AsIDrawableContainerList() {
			List<IDrawableContainer> interfaceList = new List<IDrawableContainer>();
			foreach(IDrawableContainer container in this) {
				interfaceList.Add(container);
			}
			return interfaceList;
		}
		
		/// <summary>
		/// Gets or sets the selection status of the elements.
		/// If several elements are in the list, true is only returned when all elements are selected.
		/// </summary>
		public bool Selected {
			get { 
				bool ret = true;
				foreach(DrawableContainer dc in this) {
					ret &= dc.Selected;
				}
				return ret;
			}
			set { 
				foreach(DrawableContainer dc in this) {
					dc.Selected = value;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the parent control of the elements in the list.
		/// If there are several elements, the parent control of the last added is returned.
		/// </summary>
		public ISurface Parent {
			get {
				if(Count > 0) {
					return this[Count-1].Parent;
				}
				return null;
			}
			set { 
				foreach(DrawableContainer dc in this) {
					dc.Parent = value;
				}
			}
		}
		
		/// <summary>
		/// Make a following bounds change on this containerlist undoable!
		/// </summary>
		/// <param name="allowMerge">true means allow the moves to be merged</param>
		public void MakeBoundsChangeUndoable(bool allowMerge) {
			List<IDrawableContainer> movingList = new List<IDrawableContainer>();
			Surface surface = null;
			foreach(DrawableContainer dc in this) {
				movingList.Add(dc);
				surface = dc.parent;
			}
			if (movingList.Count > 0 && surface != null) {
				surface.MakeUndoable(new DrawableContainerBoundsChangeMemento(movingList), allowMerge);
			}
		}

		/// <summary>
		/// Moves all elements in the list by the given amount of pixels.
		/// </summary>
		/// <param name="dx">pixels to move horizontally</param>
		/// <param name="dy">pixels to move vertically</param>
		public void MoveBy(int dx, int dy) {
			// Track modifications
			bool modified = false;

			// Invalidate before moving, otherwise the old locations aren't refreshed
			this.Invalidate();
			foreach(DrawableContainer dc in this) {
				dc.Left += dx;
				dc.Top += dy;
				modified = true;
			}
			// Invalidate after
			this.Invalidate();

			// If we moved something, tell the surface it's modified!
			if (modified) {
				Parent.Modified = true;
			}
		}
		
		/// <summary>
		/// Hides the grippers of all elements in the list.
		/// </summary>
		public void HideGrippers() {
			foreach(DrawableContainer dc in this) {
				dc.HideGrippers();
				dc.Invalidate();
			}
		}
		
		/// <summary>
		/// Shows the grippers of all elements in the list.
		/// </summary>
		public void ShowGrippers() {
			foreach(DrawableContainer dc in this) {
				dc.ShowGrippers();
				dc.Invalidate();
			}
		}
		
		/// <summary>
		/// Indicates whether on of the elements is clickable at the given location
		/// </summary>
		/// <param name="x">x coordinate to be checked</param>
		/// <param name="y">y coordinate to be checked</param>
		/// <returns>true if one of the elements in the list is clickable at the given location, false otherwise</returns>
		public bool ClickableAt(int x, int y) {
			bool ret = false;
			foreach(DrawableContainer dc in this) {
				ret |= dc.ClickableAt(x, y);
			}
			return ret;
		}
		
		/// <summary>
		/// retrieves the topmost element being clickable at the given location
		/// </summary>
		/// <param name="x">x coordinate to be checked</param>
		/// <param name="y">y coordinate to be checked</param>
		/// <returns>the topmost element from the list being clickable at the given location, null if there is no clickable element</returns>
		public DrawableContainer ClickableElementAt(int x, int y) {
			for(int i=Count-1; i>=0; i--) {
				if(this[i].ClickableAt(x,y)) {
					return this[i];
				}
			}
			return null;
		}
		
		/// <summary>
		/// Dispatches OnDoubleClick to all elements in the list.
		/// </summary>
		public void OnDoubleClick() {
			foreach(DrawableContainer dc in this) {
				dc.OnDoubleClick();
			}
		}

		/// <summary>
		/// Check if there are any intersecting filters, if so we need to redraw more
		/// </summary>
		/// <param name="clipRectangle"></param>
		/// <returns>true if an filter intersects</returns>
		public bool hasIntersectingFilters(Rectangle clipRectangle) {
			foreach(DrawableContainer dc in this) {
				if (dc.DrawingBounds.IntersectsWith(clipRectangle) && dc.hasFilters && dc.Status == EditStatus.IDLE) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Check if any of the drawableContainers are inside the rectangle
		/// </summary>
		/// <param name="clipRectangle"></param>
		/// <returns></returns>
		public bool IntersectsWith(Rectangle clipRectangle) {
			foreach(DrawableContainer dc in this) {
				if (dc.DrawingBounds.IntersectsWith(clipRectangle)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Triggers all elements in the list ot be redrawn.
		/// </summary>
		/// <param name="g">the related Graphics object</param>
		/// <param name="renderMode">the rendermode in which the element is to be drawn</param>
		public void Draw(Graphics g, Bitmap bitmap, RenderMode renderMode, Rectangle clipRectangle) {
			foreach(DrawableContainer dc in this) {
				if (dc.DrawingBounds.IntersectsWith(clipRectangle)) {
					dc.DrawContent(g, bitmap, renderMode, clipRectangle);
				}
			}
		}
		
		/// <summary>
		/// Pass the field changed event to all elements in the list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void HandleFieldChangedEvent(object sender, FieldChangedEventArgs e) {
			foreach(DrawableContainer dc in this) {
				dc.HandleFieldChanged(sender, e);
			}
		}		

		/// <summary>
		/// Invalidate the bounds of all the DC's in this list
		/// </summary>
		public void Invalidate() {
			foreach(DrawableContainer dc in this) {
				dc.Invalidate();
			}
		}
		/// <summary>
		/// Indicates whether the given list of elements can be pulled up, 
		/// i.e. whether there is at least one unselected element higher in hierarchy
		/// </summary>
		/// <param name="elements">list of elements to pull up</param>
		/// <returns>true if the elements could be pulled up</returns>
		public bool CanPullUp(DrawableContainerList elements) {
			if (elements.Count == 0 || elements.Count == this.Count) {
				return false;
			}
			foreach(DrawableContainer element in elements) {
				if (this.IndexOf(element) < this.Count - elements.Count) {
					return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Pulls one or several elements up one level in hierarchy (z-index).
		/// </summary>
		/// <param name="elements">list of elements to pull up</param>
		public void PullElementsUp(DrawableContainerList elements) {
			for(int i=this.Count-1; i>=0; i--) {
				DrawableContainer dc = this[i];
				if (elements.Contains(dc)) {
					if (Count > (i+1) && !elements.Contains(this[i+1])) {
						SwapElements(i,i+1);
					}
				}
			}
		}
		
		/// <summary>
		/// Pulls one or several elements up to the topmost level(s) in hierarchy (z-index).
		/// </summary>
		/// <param name="elements">of elements to pull to top</param>
		public void PullElementsToTop(DrawableContainerList elements) {
			DrawableContainer[] dcs = this.ToArray();
			for(int i=0; i<dcs.Length; i++) {
				DrawableContainer dc = dcs[i];
				if (elements.Contains(dc)) {
					this.Remove(dc);
					this.Add(dc);
					Parent.Modified = true;
				}
			}
		}
		
		/// <summary>
		/// Indicates whether the given list of elements can be pushed down, 
		/// i.e. whether there is at least one unselected element lower in hierarchy
		/// </summary>
		/// <param name="elements">list of elements to push down</param>
		/// <returns>true if the elements could be pushed down</returns>
		public bool CanPushDown(DrawableContainerList elements) {
			if (elements.Count == 0 || elements.Count == this.Count) {
				return false;
			}
			foreach(DrawableContainer element in elements) {
				if (this.IndexOf(element) >= elements.Count) {
					return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Pushes one or several elements down one level in hierarchy (z-index).
		/// </summary>
		/// <param name="elements">list of elements to push down</param>
		public void PushElementsDown(DrawableContainerList elements) {
			for(int i=0; i<Count; i++) {
				DrawableContainer dc = this[i];
				if(elements.Contains(dc)) {
					if((i>0) && !elements.Contains(this[i-1])) {
						SwapElements(i,i-1);
					}
				}
			}
		}
		
		/// <summary>
		/// Pushes one or several elements down to the bottommost level(s) in hierarchy (z-index).
		/// </summary>
		/// <param name="elements">of elements to push to bottom</param>
		public void PushElementsToBottom(DrawableContainerList elements) {
			DrawableContainer[] dcs = this.ToArray();
			for(int i=dcs.Length-1; i>=0; i--) {
				DrawableContainer dc = dcs[i];
				if(elements.Contains(dc)) {
					this.Remove(dc);
					this.Insert(0, dc);
					Parent.Modified = true;
				}
			}
		}
		
		/// <summary>
		/// swaps two elements in hierarchy (z-index), 
		/// checks both indices to be in range
		/// </summary>
		/// <param name="index1">index of the 1st element</param>
		/// <param name="index2">index of the 2nd element</param>
		private void SwapElements(int index1, int index2) {
			if(index1 >= 0 && index1 < Count && index2 >= 0 && index2 < Count && index1 != index2) {
				DrawableContainer dc = this[index1];
				this[index1] = this[index2];
				this[index2] = dc;
				Parent.Modified = true;
			}
		}

		/// <summary>
		/// Add items to a context menu for the selected item
		/// </summary>
		/// <param name="menu"></param>
		public virtual void AddContextMenuItems(ContextMenuStrip menu, Surface surface) {
			bool push = surface.Elements.CanPushDown(this);
			bool pull = surface.Elements.CanPullUp(this);

			ToolStripMenuItem item;

			// Pull "up"
			if (pull) {
				item = new ToolStripMenuItem(Language.GetString(LangKey.editor_uptotop));
				item.Click += delegate {
					surface.Elements.PullElementsToTop(this);
					surface.Elements.Invalidate();
				};
				menu.Items.Add(item);
				item = new ToolStripMenuItem(Language.GetString(LangKey.editor_uponelevel));
				item.Click += delegate {
					surface.Elements.PullElementsUp(this);
					surface.Elements.Invalidate();
				};
				menu.Items.Add(item);
			}
			// Push "down"
			if (push) {
				item = new ToolStripMenuItem(Language.GetString(LangKey.editor_downtobottom));
				item.Click += delegate {
					surface.Elements.PushElementsToBottom(this);
					surface.Elements.Invalidate();
				};
				menu.Items.Add(item);
				item = new ToolStripMenuItem(Language.GetString(LangKey.editor_downonelevel));
				item.Click += delegate {
					surface.Elements.PushElementsDown(this);
					surface.Elements.Invalidate();
				};
				menu.Items.Add(item);
			}

			// Duplicate
			item = new ToolStripMenuItem(Language.GetString(LangKey.editor_duplicate));
			item.Click += delegate {
				DrawableContainerList dcs = this.Clone();
				dcs.Parent = surface;
				dcs.MoveBy(10, 10);
				surface.AddElements(dcs);
				surface.DeselectAllElements();
				surface.SelectElements(dcs);
			};
			menu.Items.Add(item);

			// Copy
			item = new ToolStripMenuItem(Language.GetString(LangKey.editor_copytoclipboard));
			item.Image = ((System.Drawing.Image)(editorFormResources.GetObject("copyToolStripMenuItem.Image")));
			item.Click += delegate {
				ClipboardHelper.SetClipboardData(typeof(DrawableContainerList), this);
			};
			menu.Items.Add(item);

			// Cut
			item = new ToolStripMenuItem(Language.GetString(LangKey.editor_cuttoclipboard));
			item.Image = ((System.Drawing.Image)(editorFormResources.GetObject("btnCut.Image")));
			item.Click += delegate {
				ClipboardHelper.SetClipboardData(typeof(DrawableContainerList), this);
				foreach (DrawableContainer container in this) {
					surface.RemoveElement(container, true);
				}
			};
			menu.Items.Add(item);

			// Delete
			item = new ToolStripMenuItem(Language.GetString(LangKey.editor_deleteelement));
			item.Image = ((System.Drawing.Image)(editorFormResources.GetObject("removeObjectToolStripMenuItem.Image")));
			item.Click += delegate {
				foreach(DrawableContainer container in this) {
					surface.RemoveElement(container, true);
				}
			};
			menu.Items.Add(item);

			// Reset
			bool canReset = false;
			foreach (DrawableContainer container in this) {
				if (container.hasDefaultSize) {
					canReset = true;
				}
			}
			if (canReset) {
				item = new ToolStripMenuItem("Reset size");
				//item.Image = ((System.Drawing.Image)(editorFormResources.GetObject("removeObjectToolStripMenuItem.Image")));
				item.Click += delegate {
					foreach (DrawableContainer container in this) {
						if (container.hasDefaultSize) {
							Size defaultSize = container.DefaultSize;
							container.Invalidate();
							container.MakeBoundsChangeUndoable(false);
							container.Width = defaultSize.Width;
							container.Height = defaultSize.Height;
							container.Invalidate();
						}
					}
				};
				menu.Items.Add(item);
			}
		}

		public virtual void ShowContextMenu(MouseEventArgs e, Surface surface) {
			if (conf.isExperimentalFeatureEnabled("Contextmenu")) {
				bool hasMenu = false;
				foreach (DrawableContainer container in this) {
					if (container.hasContextMenu) {
						hasMenu = true;
						break;
					}
				}
				if (hasMenu) {
					ContextMenuStrip menu = new ContextMenuStrip();
					AddContextMenuItems(menu, surface);
					if (menu.Items.Count > 0) {
						menu.Show(surface, e.Location);
					}
				}
			}
		}
	}
}
