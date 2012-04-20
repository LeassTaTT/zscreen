﻿/*
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
using System.Drawing;
using System.Runtime.Serialization;

using Greenshot.Drawing.Fields;
using Greenshot.Drawing.Filters;

namespace Greenshot.Drawing {
	/// <summary>
	/// Description of ObfuscateContainer.
	/// </summary>
	[Serializable()] 
	public class HighlightContainer : FilterContainer {
		public HighlightContainer(Surface parent) : base(parent) {
			AddField(GetType(), FieldType.LINE_THICKNESS, 0);
			AddField(GetType(), FieldType.LINE_COLOR, Color.Red);
			AddField(GetType(), FieldType.SHADOW, false);
			AddField(GetType(), FieldType.PREPARED_FILTER_HIGHLIGHT, FilterContainer.PreparedFilter.TEXT_HIGHTLIGHT);
			init();
		}
		
		[OnDeserializedAttribute()]
		private void OnDeserialized(StreamingContext context) {
			init();
		}
		
		private void init() {
			FieldChanged += HighlightContainer_OnFieldChanged;
			ConfigurePreparedFilters();
		}		
		
		protected void HighlightContainer_OnFieldChanged(object sender, FieldChangedEventArgs e) {
			if(sender.Equals(this)) {
				if (e.Field.FieldType == FieldType.PREPARED_FILTER_HIGHLIGHT) {
					ConfigurePreparedFilters();
				}
			}
		}
		
		private void ConfigurePreparedFilters() {
			PreparedFilter preset = (PreparedFilter)GetFieldValue(FieldType.PREPARED_FILTER_HIGHLIGHT);
			while(Filters.Count>0) {
				Remove(Filters[0]);
			}
			switch(preset) {
				case PreparedFilter.TEXT_HIGHTLIGHT:
					Add(new HighlightFilter(this));
					break;
				case PreparedFilter.AREA_HIGHLIGHT:
					AbstractFilter bf = new BrightnessFilter(this);
					bf.Invert = true;
					Add(bf);
					bf = new BlurFilter(this);
					bf.Invert = true;
					Add(bf);
					break;
				case PreparedFilter.GRAYSCALE:
					AbstractFilter f = new GrayscaleFilter(this);
					f.Invert = true;
					Add(f);
					break;
				case PreparedFilter.MAGNIFICATION:
					Add(new MagnifierFilter(this));
					break;
			}
		}
	}
}
