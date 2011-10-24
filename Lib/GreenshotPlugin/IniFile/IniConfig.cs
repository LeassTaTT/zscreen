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
using System.Text;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Threading;

namespace IniFile {
	public class IniConfig {
		private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(IniConfig));
		private const string INI_EXTENSION = ".ini";
		private const string DEFAULTS_POSTFIX = "-defaults";
		private const string FIXED_POSTFIX = "-fixed";
		private static FileSystemWatcher watcher;

		private static string applicationName;
		private static string configName;
		private static Dictionary<string, IniSection> sectionMap = new Dictionary<string, IniSection>();
		private static Dictionary<string, Dictionary<string, string>> sections = new Dictionary<string, Dictionary<string, string>>();
		public static event FileSystemEventHandler IniChanged;
		private static bool portableCheckMade = false;
		private static bool portable = false;
		public static bool IsPortable {
			get {
				return portable;
			}
		}
		/// <summary>
		/// Initialize the ini config
		/// </summary>
		/// <param name="applicationName"></param>
		/// <param name="configName"></param>
		public static void Init(string applicationName, string configName) {
			IniConfig.applicationName = applicationName;
			IniConfig.configName = configName;
			Reload();
			WatchConfigFile(true);
		}
		
		/// <summary>
		/// Default init
		/// </summary>
		public static void Init() {
			AssemblyProductAttribute[] assemblyProductAttributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false) as AssemblyProductAttribute[];
			if (assemblyProductAttributes.Length > 0) {
				LOG.DebugFormat("Using ProductName {0}", assemblyProductAttributes[0].Product);
				Init(assemblyProductAttributes[0].Product, assemblyProductAttributes[0].Product);
			} else {
				throw new InvalidOperationException("Assembly ProductName not set.");
			}
		}

		/// <summary>
		/// Enable watching the configuration
		/// </summary>
		/// <param name="sendEvents"></param>
		private static void WatchConfigFile(bool sendEvents) {
			string iniLocation = CreateIniLocation(configName + INI_EXTENSION);
			// Wait with watching until the file is there
			if (Directory.Exists(Path.GetDirectoryName(iniLocation))) {
				if (watcher == null) {
					//LOG.DebugFormat("Starting FileSystemWatcher for {0}", iniLocation);
					// Monitor the ini file
					watcher = new FileSystemWatcher();
					watcher.Path = Path.GetDirectoryName(iniLocation);
					watcher.Filter = "*.ini";
					watcher.NotifyFilter = NotifyFilters.LastWrite;
					watcher.Changed += new FileSystemEventHandler(ConfigFileChanged);
				}
			}
			if (watcher != null) {
				watcher.EnableRaisingEvents = sendEvents;
			}
		}

		public static string ConfigLocation {
			get {
				return CreateIniLocation(configName + INI_EXTENSION);
			}
		}
		private static void ConfigFileChanged(object source, FileSystemEventArgs e) {
			string iniLocation = CreateIniLocation(configName + INI_EXTENSION);
			if (iniLocation.Equals(e.FullPath)) {
				//LOG.InfoFormat("Config file {0} was changed, reloading", e.FullPath);

				// Try to reread the configuration
				int retries = 10;
				bool configRead = false;
				while (!configRead && retries != 0) {
					try {
						IniConfig.Reload();
						configRead = true;
					} catch (IOException) {
						retries--;
						Thread.Sleep(100);
					}
				}

				if (configRead && IniChanged != null) {
					IniChanged.Invoke(source, e);
				}
			}
		}
	
		/// <summary>
		/// Create the location of the configuration file
		/// </summary>
		private static string CreateIniLocation(string configFilename) {
			if (applicationName == null || configName == null) {
				throw new InvalidOperationException("IniConfig.Init not called!");
			}
			string iniFilePath = null;
			string applicationStartupPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			string pafPath = Path.Combine(applicationStartupPath, @"App\" + applicationName);
			
			if (portable || !portableCheckMade) {
				if (!portable) {
					LOG.Info("Checking for portable mode.");
					portableCheckMade = true;
					if (Directory.Exists(pafPath)) {
						portable = true;
						LOG.Info("Portable mode active!");
					}
				}
				if (portable) {
					string pafConfigPath = Path.Combine(applicationStartupPath, @"Data\Settings");
					try {
						Directory.CreateDirectory(pafConfigPath);
						iniFilePath = Path.Combine(pafConfigPath, configFilename);
					} catch(Exception e) {
						LOG.InfoFormat("Portable mode NOT possible, couldn't create directory '{0}'! Reason: {1}", pafConfigPath, e.Message);
					}
				}
			}
			if (iniFilePath == null) {
				// check if file is in the same location as started from, if this is the case
				// we will use this file instead of the Applicationdate folder
				// Done for Feature Request #2741508
				iniFilePath = Path.Combine(applicationStartupPath, configFilename);
				if (!File.Exists(iniFilePath)) {
					string iniDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), applicationName);
					if (!Directory.Exists(iniDirectory)) {
						Directory.CreateDirectory(iniDirectory);
					}
					iniFilePath = Path.Combine(iniDirectory, configFilename);
				}
			}
			LOG.InfoFormat("Using ini file {0}", iniFilePath);
			return iniFilePath;
		}

		/// <summary>
		/// Reload the Ini file
		/// </summary>
		public static void Reload() {
			// Clear the current properties
			sections = new Dictionary<string, Dictionary<string, string>>();
			// Load the defaults
			Read(CreateIniLocation(configName + DEFAULTS_POSTFIX + INI_EXTENSION));
			// Load the normal
			Read(CreateIniLocation(configName + INI_EXTENSION));
			// Load the fixed settings
			Read(CreateIniLocation(configName + FIXED_POSTFIX + INI_EXTENSION));

			foreach (IniSection section in sectionMap.Values) {
				FillIniSection(section);
			}
		}

		/// <summary>
		/// Read the ini file into the Dictionary
		/// </summary>
		/// <param name="iniLocation">Path & Filename of ini file</param>
		private static void Read(string iniLocation) {
			if (!File.Exists(iniLocation)) {
				//LOG.Info("Can't find file: " + iniLocation);
				return;
			}
			LOG.DebugFormat("Loading ini-file: {0}", iniLocation);
			//LOG.Info("Reading ini-properties from file: " + iniLocation);
			Dictionary<string, Dictionary<string, string>> newSections = IniReader.read(iniLocation, Encoding.UTF8);
			// Merge the newly loaded properties to the already available
			foreach(string section in newSections.Keys) {
				Dictionary<string, string> newProperties = newSections[section];
				if (!sections.ContainsKey(section)) {
					// This section is not yet loaded, simply add the complete section
					sections.Add(section, newProperties);
				} else {
					// Overwrite or add every property from the newly loaded section to the available one
					Dictionary<string, string> currentProperties = sections[section];
					foreach(string propertyName in newProperties.Keys) {
						string propertyValue = newProperties[propertyName];
						if (currentProperties.ContainsKey(propertyName)) {
							// Override current value as we are loading in a certain order which insures the default, current and fixed
							currentProperties[propertyName] = propertyValue;
						} else {
							// Add "new" value
							currentProperties.Add(propertyName, propertyValue);
						}
					}
				}
			}
		}

		/// <summary>
		/// A generic method which returns an instance of the supplied type, filled with it's configuration
		/// </summary>
		/// <returns>Filled instance of IniSection type which was supplied</returns>
		public static T GetIniSection<T>() where T : IniSection {
			T section;

			Type iniSectionType = typeof(T);
			string sectionName = getSectionName(iniSectionType);
			if (sectionMap.ContainsKey(sectionName)) {
				//LOG.Debug("Returning pre-mapped section " + sectionName);
				section = (T)sectionMap[sectionName];
			} else {
				// Create instance of this type
				section = (T)Activator.CreateInstance(iniSectionType);

				// Store for later save & retrieval
				sectionMap.Add(sectionName, section);
				FillIniSection(section);
				//LOG.Debug("Returning newly mapped section " + sectionName);
			}
			if (section.IsDirty) {
				IniConfig.Save();
			}
			return section;
		}

		private static void FillIniSection(IniSection section) {
			Type iniSectionType = section.GetType();
			string sectionName = getSectionName(iniSectionType);
			// Get the properties for the section
			Dictionary<string, string> properties = null;
			if (sections.ContainsKey(sectionName)) {
				properties = sections[sectionName];
			} else {
				sections.Add(sectionName, new Dictionary<string, string>());
				properties = sections[sectionName];
			}

			// Iterate over the members and fill them
			List<MemberInfo> members = new List<MemberInfo>();

			foreach (FieldInfo fieldInfo in iniSectionType.GetFields()) {
				members.Add(fieldInfo);
			}
			foreach (PropertyInfo propertyInfo in iniSectionType.GetProperties()) {
				members.Add(propertyInfo);
			}

			foreach (MemberInfo field in members) {
				if (Attribute.IsDefined(field, typeof(IniPropertyAttribute))) {
					IniPropertyAttribute iniPropertyAttribute = (IniPropertyAttribute)field.GetCustomAttributes(typeof(IniPropertyAttribute), false)[0];
					string propertyName = iniPropertyAttribute.Name;
					string propertyDefaultValue = iniPropertyAttribute.DefaultValue;
					string fieldSeparator = iniPropertyAttribute.Separator;
					// Get the type, or the underlying type for nullables
					Type valueType;
					if (field is FieldInfo) {
						valueType = ((FieldInfo)field).FieldType;
					} else if (field is PropertyInfo) {
						valueType = ((PropertyInfo)field).PropertyType;
					} else {
						continue;
					}

					// Get the value from the ini file, if there is none take the default
					if (!properties.ContainsKey(propertyName) && propertyDefaultValue != null) {
						// Mark as dirty, we didn't use properties from the file (even defaults from the default file are allowed)
						section.IsDirty = true;
						//LOG.Debug("Passing default: " + propertyName + "=" + propertyDefaultValue);
					}

					// Try to get the field value from the properties or use the default value
					object fieldValue = null;
					try {
						fieldValue = CreateValue(valueType, section, sectionName, propertyName, propertyDefaultValue, fieldSeparator);
					} catch (Exception) {
						//LOG.Warn("Couldn't parse field: " + sectionName + "." + propertyName, e);
					}

					// If still no value, check if the GetDefault delivers a value
					if (fieldValue == null) {
						// Use GetDefault to fill the field if none is set
						fieldValue = section.GetDefault(propertyName);
					}

					// Set the value
					try {
						if (field is FieldInfo) {
							((FieldInfo)field).SetValue(section, fieldValue);
						} else if (field is PropertyInfo) {
							((PropertyInfo)field).SetValue(section, fieldValue, null); ;
						}
					} catch (Exception) {
						//LOG.Warn("Couldn't set field: " + sectionName + "." + propertyName, e);
					}
				}
			}
			section.AfterLoad();
		}

		/// <summary>
		/// Helper method for creating a value
		/// </summary>
		/// <param name="valueType">Type of the value to create</param>
		/// <param name="propertyValue">Value as string</param>
		/// <returns>object instance of the value</returns>
		private static object CreateValue(Type valueType, IniSection section, string sectionName, string propertyName, string defaultValue, string arraySeparator) {
			Dictionary<string, string> properties = sections[sectionName];
			bool defaultUsed = false;
			string propertyValue = null;
			if (properties.ContainsKey(propertyName) && properties[propertyName] != null) {
				propertyValue = section.PreCheckValue(propertyName, properties[propertyName]);
			} else if (defaultValue != null && defaultValue.Trim().Length != 0) {
				propertyValue = defaultValue;
				defaultUsed = true;
			} else {
				//LOG.DebugFormat("Property {0} has no value or default value", propertyName);
			}

			// Now set the value
			if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(List<>)) {
				object list = Activator.CreateInstance(valueType);
				// Logic for List<>
				if (propertyValue == null) {
					return list;
				}
				string[] arrayValues = propertyValue.Split(new string[] { arraySeparator }, StringSplitOptions.None);
				if (arrayValues == null || arrayValues.Length == 0) {
					return list;
				}
				bool addedElements = false;
				bool parseProblems = false;
				MethodInfo addMethodInfo = valueType.GetMethod("Add");

				foreach (string arrayValue in arrayValues) {
					if (arrayValue != null && arrayValue.Length > 0) {
						object newValue = null;
						try {
							newValue = ConvertValueToValueType(valueType.GetGenericArguments()[0], arrayValue);
						} catch (Exception) {
							//LOG.Error("Problem converting " + arrayValue + " to type " + fieldType.FullName, e);
							parseProblems = true;
						}
						if (newValue != null) {
							addMethodInfo.Invoke(list, new object[] { newValue });
							addedElements = true;
						}
					}
				}
				// Try to fallback on a default
				if (!addedElements && parseProblems) {
					try {
						object fallbackValue = ConvertValueToValueType(valueType.GetGenericArguments()[0], defaultValue);
						addMethodInfo.Invoke(list, new object[] { fallbackValue });
						return list;
					} catch (Exception) {
						//LOG.Error("Problem converting " + defaultValue + " to type " + fieldType.FullName, e);
					}
				}

				return list;
			} else if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) {
				// Logic for Dictionary<,>
				Type type1 = valueType.GetGenericArguments()[0];
				Type type2 = valueType.GetGenericArguments()[1];
				//LOG.Info(String.Format("Found Dictionary<{0},{1}>", type1.Name, type2.Name));
				object dictionary = Activator.CreateInstance(valueType);
				MethodInfo addMethodInfo = valueType.GetMethod("Add");
				bool addedElements = false;
				foreach (string key in properties.Keys) {
					if (key != null && key.StartsWith(propertyName + ".")) {
						// What "key" do we need to store it under?
						string subPropertyName = key.Substring(propertyName.Length + 1);
						string stringValue = properties[key];
						object newValue1 = null;
						object newValue2 = null;
						try {
							newValue1 = ConvertValueToValueType(type1, subPropertyName);
						} catch (Exception) {
							//LOG.Error("Problem converting " + subPropertyName + " to type " + type1.FullName, e);
						}
						try {
							newValue2 = ConvertValueToValueType(type2, stringValue);
						} catch (Exception) {
							//LOG.Error("Problem converting " + stringValue + " to type " + type2.FullName, e);
						}
						addMethodInfo.Invoke(dictionary, new object[] { newValue1, newValue2 });
						addedElements = true;
					}
				}
				// No need to return something that isn't filled!
				if (addedElements) {
					return dictionary;
				}
			} else {
				if (valueType.IsGenericType && valueType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
					// We are dealing with a generic type that is nullable
					valueType = Nullable.GetUnderlyingType(valueType);
				}
				object newValue = null;
				try {
					newValue = ConvertValueToValueType(valueType, propertyValue);
				} catch (Exception) {
					newValue = null;
					if (!defaultUsed) {
						try {
							newValue = ConvertValueToValueType(valueType, defaultValue);
						} catch (Exception) {
							//LOG.Error("Problem converting " + propertyValue + " to type " + fieldType.FullName, e2);
						}
					} else {
						//LOG.Error("Problem converting " + propertyValue + " to type " + fieldType.FullName, e1);
					}
				}
				return newValue;
			}
			return null;
		}

		private static object ConvertValueToValueType(Type valueType, string valueString) {
			if (valueString == null) {
				return null;
			}
			if (valueType == typeof(string)) {
				return valueString;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(valueType);
			if (converter != null) {
				return converter.ConvertFromInvariantString(valueString);
			}
			//LOG.Debug("No convertor for " + fieldType.ToString());
			if (valueType == typeof(object)) {
				if (valueString.Length > 0) {
					//LOG.Debug("Parsing: " + valueString);
					string[] values = valueString.Split(new Char[] { ':' });
					//LOG.Debug("Type: " + values[0]);
					//LOG.Debug("Value: " + values[1]);
					Type fieldTypeForValue = Type.GetType(values[0], true);
					//LOG.Debug("Type after GetType: " + fieldTypeForValue);
					return ConvertValueToValueType(fieldTypeForValue, values[1]);
				}
			} else if (valueType.IsEnum) {
				if (valueString.Length > 0) {
					try {
						return Enum.Parse(valueType, valueString);
					} catch (ArgumentException ae) {
						//LOG.InfoFormat("Couldn't match {0} to {1}, trying case-insentive match", valueString, fieldType);
						foreach (Enum enumValue in Enum.GetValues(valueType)) {
							if (enumValue.ToString().Equals(valueString, StringComparison.InvariantCultureIgnoreCase)) {
								//LOG.Info("Match found...");
								return enumValue;
							}
						}
						throw ae;
					}
				}
			}
			return null;
		}

		private static string ConvertValueToString(Type valueType, object valueObject) {
			if (valueObject == null) {
				// If there is nothing, deliver nothing!
				return "";
			}
			TypeConverter converter = TypeDescriptor.GetConverter(valueType);
			if (converter != null) {
				return converter.ConvertToInvariantString(valueObject);
			} else if (valueType == typeof(object)) {
				// object to String, this is the hardest
				// Format will be "FQTypename[,Assemblyname]:Value"

				// Get the type so we can call ourselves recursive
				Type objectType = valueObject.GetType();

				// Get the value as string
				string ourValue = ConvertValueToString(objectType, valueObject);

				// Get the valuetype as string
				string valueTypeName = objectType.FullName;
				// Find the assembly name and only append it if it's not already in the fqtypename (like System.Drawing)
				string assemblyName = objectType.Assembly.FullName;
				// correct assemblyName, this also has version information etc.
				if (assemblyName.StartsWith("Green")) {
					assemblyName = assemblyName.Substring(0, assemblyName.IndexOf(','));
				}
				return String.Format("{0},{1}:{2}", valueTypeName, assemblyName, ourValue);
			}
			// All other types
			return valueObject.ToString();
		}

		private static string getSectionName(Type iniSectionType) {
			Attribute[] classAttributes = Attribute.GetCustomAttributes(iniSectionType);
			foreach (Attribute attribute in classAttributes) {
				if (attribute is IniSectionAttribute) {
					IniSectionAttribute iniSectionAttribute = (IniSectionAttribute)attribute;
					return iniSectionAttribute.Name;
				}
			}
			return null;
		}

		public static void Save() {
			string iniLocation = CreateIniLocation(configName + INI_EXTENSION);
			try {
				SaveInternally(iniLocation);
			} catch (Exception) {
				//LOG.Error("A problem occured while writing the configuration file to: " + iniLocation, e);
			}
		}
		
		public static void SaveIniSectionToWriter(TextWriter writer, IniSection section, bool onlyProperties) {
			section.BeforeSave();
			Type classType = section.GetType();
			Attribute[] classAttributes = Attribute.GetCustomAttributes(classType);
			foreach (Attribute attribute in classAttributes) {
				if (attribute is IniSectionAttribute) {
					IniSectionAttribute iniSectionAttribute = (IniSectionAttribute)attribute;
					if (!onlyProperties) {
						writer.WriteLine("; {0}", iniSectionAttribute.Description);
					}
					writer.WriteLine("[{0}]", iniSectionAttribute.Name);

					// Iterate over the members and fill them
					List<MemberInfo> members = new List<MemberInfo>();

					foreach (FieldInfo fieldInfo in classType.GetFields()) {
						members.Add(fieldInfo);
					}
					foreach (PropertyInfo propertyInfo in classType.GetProperties()) {
						members.Add(propertyInfo);
					}

					foreach (MemberInfo member in members) {
						if (Attribute.IsDefined(member, typeof(IniPropertyAttribute))) {
							IniPropertyAttribute iniPropertyAttribute = (IniPropertyAttribute)member.GetCustomAttributes(typeof(IniPropertyAttribute), false)[0];
							if (!onlyProperties) {
								writer.WriteLine("; {0}", iniPropertyAttribute.Description);
							}
							object value;
							Type valueType;
							if (member is FieldInfo) {
								value = ((FieldInfo)member).GetValue(section);
								valueType = ((FieldInfo)member).FieldType;
							} else if (member is PropertyInfo) {
								value = ((PropertyInfo)member).GetValue(section, null);
								valueType = ((PropertyInfo)member).PropertyType;
							} else {
								continue;
							}
							if (value == null) {
								value = iniPropertyAttribute.DefaultValue;
								valueType = typeof(string);
							}
							if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(List<>)) {
								Type specificValueType = valueType.GetGenericArguments()[0];
								writer.Write("{0}=", iniPropertyAttribute.Name);
								int listCount = (int)valueType.GetProperty("Count").GetValue(value, null);
								// Loop though generic list
								for (int index = 0; index < listCount; index++) {
									object item = valueType.GetMethod("get_Item").Invoke(value, new object[] { index });

									// Now you have an instance of the item in the generic list
									if (index < listCount - 1) {
										writer.Write("{0}" + iniPropertyAttribute.Separator, ConvertValueToString(specificValueType, item));
									} else {
										writer.Write("{0}", ConvertValueToString(specificValueType, item));
									}
								}
								writer.WriteLine();
							} else if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) {
								// Handle dictionaries.
								Type valueType1 = valueType.GetGenericArguments()[0];
								Type valueType2 = valueType.GetGenericArguments()[1];
								// Get the methods we need to deal with dictionaries.
								var keys = valueType.GetProperty("Keys").GetValue(value, null);
								var item = valueType.GetProperty("Item");
								var enumerator = keys.GetType().GetMethod("GetEnumerator").Invoke(keys, null);
								var moveNext = enumerator.GetType().GetMethod("MoveNext");
								var current = enumerator.GetType().GetProperty("Current").GetGetMethod();
								// Get all the values.
								while ((bool)moveNext.Invoke(enumerator, null)) {
									var key = current.Invoke(enumerator, null);
									var valueObject = item.GetValue(value, new object[] { key });
									// Write to ini file!
									writer.WriteLine("{0}.{1}={2}", iniPropertyAttribute.Name, ConvertValueToString(valueType1, key), ConvertValueToString(valueType2, valueObject));
								}
							} else {
								writer.WriteLine("{0}={1}", iniPropertyAttribute.Name, ConvertValueToString(valueType, value));
							}
						}
					}
				}
				section.AfterSave();
			}
		}

		private static void SaveInternally(string iniLocation) {
			WatchConfigFile(false);

			//LOG.Info("Saving configuration to: " + iniLocation);
			if (!Directory.Exists(Path.GetDirectoryName(iniLocation))) {
				Directory.CreateDirectory(Path.GetDirectoryName(iniLocation));
			}
			TextWriter writer = new StreamWriter(iniLocation, false, Encoding.UTF8);
			foreach (IniSection section in sectionMap.Values) {
				SaveIniSectionToWriter(writer, section, false);
				section.IsDirty = false;
			}
			writer.WriteLine();
			// Write left over properties
			foreach (string sectionName in sections.Keys) {
				// Check if the section is one that is "registered", if so skip it!
				if (!sectionMap.ContainsKey(sectionName)) {
					writer.WriteLine("; The section {0} is not registered, maybe a plugin hasn't claimed it due to errors or some functionality isn't used yet.", sectionName);
					// Write section name
					writer.WriteLine("[{0}]", sectionName);
					Dictionary<string, string> properties = sections[sectionName];
					// Loop and write properties
					foreach (string propertyName in properties.Keys) {
						writer.WriteLine("{0}={1}", propertyName, properties[propertyName]);
					}
					writer.WriteLine();
				}
			}
			writer.Close();
			WatchConfigFile(true);
		}
	}
}
