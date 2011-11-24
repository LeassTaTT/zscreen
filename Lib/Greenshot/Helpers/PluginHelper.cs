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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Greenshot.Plugin;
using GreenshotPlugin.Core;
using IniFile;

namespace Greenshot.Helpers {
	/// <summary>
	/// The PluginHelper takes care of all plugin related functionality
	/// </summary>
	[Serializable]
	public class PluginHelper : IGreenshotPluginHost {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(PluginHelper));
		private static CoreConfiguration conf = IniConfig.GetIniSection<CoreConfiguration>();

		public static string pluginPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),Application.ProductName);
		public static string applicationPath = Path.GetDirectoryName(Application.ExecutablePath);
		public static string pafPath =  Path.Combine(Application.StartupPath, @"App\Greenshot");
		public static readonly PluginHelper instance = new PluginHelper();

		private static Dictionary<PluginAttribute, IGreenshotPlugin> plugins = new Dictionary<PluginAttribute, IGreenshotPlugin>();

		private PluginHelper() {
		}
		
		public void CreateImageEditorOpenEvent(IImageEditor imageEditor) {
			if (OnImageEditorOpen != null) {
				ImageEditorOpenEventArgs eventArgs = new ImageEditorOpenEventArgs(imageEditor);
				OnImageEditorOpen(this, eventArgs);
			}
		}
		
		public void CreateCaptureTakenEvent(ICapture capture) {
			if (OnCaptureTaken != null) {
				CaptureTakenEventArgs eventArgs = new CaptureTakenEventArgs(capture);
				OnCaptureTaken(this, eventArgs);
			}
		}

		public void CreateSurfaceFromCaptureEvent(ICapture capture, ISurface surface) {
			if (OnSurfaceFromCapture != null) {
				SurfaceFromCaptureEventArgs eventArgs = new SurfaceFromCaptureEventArgs(capture, surface);
				OnSurfaceFromCapture(this, eventArgs);
			}
		}

		public bool HasPlugins() {
			return (plugins != null && plugins.Count > 0);
		}

		public void Shutdown() {
			foreach(IGreenshotPlugin plugin in plugins.Values) {
				plugin.Shutdown();
			}
			plugins.Clear();
		}

		// Add plugins to the Listview
		public void FillListview(ListView listview) {
			foreach(PluginAttribute pluginAttribute in plugins.Keys) {
				ListViewItem item = new ListViewItem(pluginAttribute.Name);
                item.SubItems.Add(pluginAttribute.Version);
                item.SubItems.Add(pluginAttribute.DllFile);
                item.Tag = pluginAttribute;
                listview.Items.Add(item);
			}
		}
		
		public bool isSelectedItemConfigurable(ListView listview) {
			if (listview.SelectedItems.Count > 0) {
				PluginAttribute pluginAttribute = (PluginAttribute)listview.SelectedItems[0].Tag;
				if (pluginAttribute != null) {
					return pluginAttribute.Configurable;
				}
			}
			return false;
		}
		
		public void ConfigureSelectedItem(ListView listview) {
			if (listview.SelectedItems.Count > 0) {
				PluginAttribute pluginAttribute = (PluginAttribute)listview.SelectedItems[0].Tag;
				if (pluginAttribute != null) {
					IGreenshotPlugin plugin = plugins[pluginAttribute];
					plugin.Configure();
				}
			}
		}

		#region Implementation of IGreenshotPluginHost
		public event OnImageEditorOpenHandler OnImageEditorOpen;
		public event OnCaptureTakenHandler OnCaptureTaken;
		public event OnSurfaceFromCaptureHandler OnSurfaceFromCapture;
		private ContextMenuStrip mainMenu = null;

		public void SaveToStream(Image img, Stream stream, OutputFormat extension, int quality) {
			ImageOutput.SaveToStream(img, stream, extension, quality);
		}
		
		public string GetFilename(OutputFormat format, ICaptureDetails captureDetails) {
			string pattern = conf.OutputFileFilenamePattern;
			if (pattern == null || string.IsNullOrEmpty(pattern.Trim())) {
				pattern = "greenshot ${capturetime}";
			}
			return FilenameHelper.GetFilenameFromPattern(pattern, format, captureDetails);
		}
		
		/// <summary>
		/// Create a Thumbnail
		/// </summary>
		/// <param name="image">Image of which we need a Thumbnail</param>
		/// <returns>Image with Thumbnail</returns>
		public Image GetThumbnail(Image image, int width, int height) {
			return image.GetThumbnailImage(width, height,  new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
		}

		///  <summary>
		/// Required for GetThumbnail, but not used
		/// </summary>
		/// <returns>true</returns>
		private bool ThumbnailCallback() {
			return true;
		}

		public ContextMenuStrip MainMenu {
			get { return mainMenu;}
		}
		
		public Dictionary<PluginAttribute, IGreenshotPlugin> Plugins {
			get {return plugins;}
		}

		#endregion

		#region Plugin loading
		public PluginAttribute FindPlugin(string name) {
			foreach(PluginAttribute pluginAttribute in plugins.Keys) {
				if (name.Equals(pluginAttribute.Name)) {
					return pluginAttribute;
				}
			}
			return null;
		}
		
		private bool isNewer(string version1, string version2) {
			string [] version1Parts = version1.Split('.');
			string [] version2Parts = version2.Split('.');
			int parts = Math.Min(version1Parts.Length, version2Parts.Length);
			for(int i=0; i < parts; i++) {
				int v1 = Convert.ToInt32(version1Parts[i]);
				int v2 = Convert.ToInt32(version2Parts[i]);
				if (v1 > v2) {
					return true;
				}
				if (v1 < v2) {
					return false;
				}
			}
			return false;
		}

		public void LoadPlugins(MainForm mainForm, ICaptureHost captureHost) {
			// Copy ContextMenu
			mainMenu = mainForm.MainMenu;
			
			List<string> pluginFiles = new List<string>();

			if (IniConfig.IsPortable && Directory.Exists(pafPath)) {
				foreach(string pluginFile in Directory.GetFiles(pafPath, "*.gsp", SearchOption.AllDirectories)) {
					pluginFiles.Add(pluginFile);
				}
			} else {
				if (Directory.Exists(pluginPath)) {
					foreach(string pluginFile in Directory.GetFiles(pluginPath, "*.gsp", SearchOption.AllDirectories)) {
						pluginFiles.Add(pluginFile);
					}
				}
	
				if (Directory.Exists(applicationPath)) {
					foreach(string pluginFile in Directory.GetFiles(applicationPath, "*.gsp", SearchOption.AllDirectories)) {
						pluginFiles.Add(pluginFile);
					}
				}
			}

			Dictionary<string, PluginAttribute> tmpAttributes = new Dictionary<string, PluginAttribute>();
			Dictionary<string, Assembly> tmpAssemblies = new Dictionary<string, Assembly>();
			// Loop over the list of available files and get the Plugin Attributes
			foreach (string pluginFile in pluginFiles) {
				LOG.DebugFormat("Checking the following file for plugins: {0}", pluginFile);
				try {
					Assembly assembly = Assembly.LoadFile(pluginFile);
					PluginAttribute[] pluginAttributes = assembly.GetCustomAttributes(typeof(PluginAttribute), false) as PluginAttribute[];
					if (pluginAttributes.Length > 0) {
						PluginAttribute pluginAttribute = pluginAttributes[0];
						
						AssemblyProductAttribute[] assemblyProductAttributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false) as AssemblyProductAttribute[];
						if (assemblyProductAttributes.Length > 0) {
							pluginAttribute.Name = assemblyProductAttributes[0].Product;
						} else {
							continue;
						}
						pluginAttribute.Version = assembly.GetName().Version.ToString();
						pluginAttribute.DllFile = pluginFile;
						
						// check if this plugin is already available
						PluginAttribute checkPluginAttribute = null;
						try {
							checkPluginAttribute = tmpAttributes[pluginAttribute.Name];
						} catch {
							
						}

						if (checkPluginAttribute != null) {
							LOG.WarnFormat("Duplicate plugin {0} found", pluginAttribute.Name);
							if (isNewer(pluginAttribute.Version, checkPluginAttribute.Version)) {
								// Found is newer
								tmpAttributes[pluginAttribute.Name] = pluginAttribute;
								tmpAssemblies[pluginAttribute.Name] = assembly;
								LOG.InfoFormat("Loading the newer plugin {0} with version {1} from {2}", pluginAttribute.Name, pluginAttribute.Version, pluginAttribute.DllFile);
							} else {
								LOG.InfoFormat("Skipping (as the duplicate is newer or same version) the plugin {0} with version {1} from {2}", pluginAttribute.Name, pluginAttribute.Version, pluginAttribute.DllFile);
							}
							continue;
						} else {
							if (conf.ExcludePlugins != null && conf.ExcludePlugins.Contains(pluginAttribute.Name)) {
								LOG.WarnFormat("Exclude list: {0}", conf.ExcludePlugins.ToArray());
								LOG.WarnFormat("Skipping the excluded plugin {0} with version {1} from {2}", pluginAttribute.Name, pluginAttribute.Version, pluginAttribute.DllFile);
								continue;
							}
							if (conf.IncludePlugins != null && conf.IncludePlugins.Count > 0 && !conf.IncludePlugins.Contains(pluginAttribute.Name)) {
								// Whitelist is set
								LOG.WarnFormat("Include list: {0}", conf.IncludePlugins.ToArray());
								LOG.WarnFormat("Skipping the not included plugin {0} with version {1} from {2}", pluginAttribute.Name, pluginAttribute.Version, pluginAttribute.DllFile);
								continue;
							}
							LOG.InfoFormat("Loading the plugin {0} with version {1} from {2}", pluginAttribute.Name, pluginAttribute.Version, pluginAttribute.DllFile);
							tmpAttributes[pluginAttribute.Name] = pluginAttribute;
							tmpAssemblies[pluginAttribute.Name] = assembly;
						}
					} else {
						LOG.ErrorFormat("Can't find the needed Plugin Attribute ({0}) in the assembly of the file \"{1}\", skipping this file.", typeof(PluginAttribute), pluginFile);
					}
				} catch (Exception e) {
					LOG.Warn("Can't load file: " + pluginFile, e);
				}
			}
			foreach(string pluginName in tmpAttributes.Keys) {
				try {
					PluginAttribute pluginAttribute = tmpAttributes[pluginName];
					Assembly assembly = tmpAssemblies[pluginName];
					Type entryType = assembly.GetType(pluginAttribute.EntryType);
					if (entryType == null) {
						LOG.ErrorFormat("Can't find the in the PluginAttribute referenced type {0} in \"{1}\"", pluginAttribute.EntryType, pluginAttribute.DllFile);
						continue;
					}
					try {
						IGreenshotPlugin plugin = (IGreenshotPlugin)Activator.CreateInstance(entryType);
						if (plugin != null) {
							if (plugin.Initialize(this, captureHost, pluginAttribute)) {
								plugins.Add(pluginAttribute, plugin);
							} else {
								LOG.InfoFormat("Plugin {0} not initialized!", pluginAttribute.Name);
							}
						} else {
							LOG.ErrorFormat("Can't create an instance of the in the PluginAttribute referenced type {0} from \"{1}\"", pluginAttribute.EntryType, pluginAttribute.DllFile);
						}
					} catch(Exception e) {
						LOG.Error("Can't load Plugin: " + pluginAttribute.Name, e);
					}
				} catch(Exception e) {
					LOG.Error("Can't load Plugin: " + pluginName, e);
				}
			}
		}
		#endregion
	}
}
