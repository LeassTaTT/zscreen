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
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GreenshotPlugin.Core {
	public static class Objects {
	
		/// <summary>
		/// Perform a deep Copy of the object.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source) {
			if (!typeof(T).IsSerializable) {
				throw new ArgumentException("The type must be serializable.", "source");
			}
	
			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null)) {
				return default(T);
			}
	
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using (stream) {
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	
		public static void CloneTo<T>(this T source, T destination) {
			Type type = typeof(T);
			FieldInfo[] myObjectFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

			foreach (FieldInfo fi in myObjectFields) {
				fi.SetValue(destination, fi.GetValue(source));
			}
			PropertyInfo[] myObjectProperties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo pi in myObjectProperties) {
				pi.SetValue(destination, pi.GetValue(source, null), null);
			}
		}
	}
}
