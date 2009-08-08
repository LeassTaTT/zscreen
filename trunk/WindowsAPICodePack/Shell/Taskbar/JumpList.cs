﻿//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.WindowsAPICodePack.Shell;
using MS.WindowsAPICodePack.Internal;

namespace Microsoft.WindowsAPICodePack.Taskbar
{
    /// <summary>
    /// Represents an instance of a Taskbar button jump list.
    /// </summary>
    public class JumpList
    {

        /// <summary>
        /// Create a JumpList for the application's taskbar button.
        /// </summary>
        /// <returns>A new JumpList that is associated with the app id of the main application window</returns>
        /// <remarks>If there are any other child (top-level) windows for this application and they don't have
        /// a specific JumpList created for them, they all will share the same JumpList as the main application window.
        /// In order to have a individual JumpList for a top-level window, use the overloaded method CreateJumpListForIndividualWindow.</remarks>
        public static JumpList CreateJumpList()
        {
            return new JumpList(TaskbarManager.Instance.ApplicationId);
        }

        /// <summary>
        /// Create a JumpList for the application's taskbar button.
        /// </summary>
        /// <param name="appid">Application Id for the individual window. This must be unique for each top-level window in order to have a individual JumpList.</param>
        /// <param name="windowHandle">Handle of the window associated with the new JumpList</param>
        /// <returns>A new JumpList that is associated with the specific window handle</returns>
        public static JumpList CreateJumpListForIndividualWindow(string appid, IntPtr windowHandle)
        {
            return new JumpList(appid, windowHandle);
        }

        /// <summary>
        /// Create a JumpList for the application's taskbar button.
        /// </summary>
        /// <param name="appid">Application Id for the individual window. This must be unique for each top-level window in order to have a individual JumpList.</param>
        /// <param name="window">WPF Window associated with the new JumpList</param>
        /// <returns>A new JumpList that is associated with the specific WPF window</returns>
        public static JumpList CreateJumpListForIndividualWindow(string appid, System.Windows.Window window)
        {
            return new JumpList(appid, window);
        }

        // Best practice recommends defining a private object to lock on
        private static Object syncLock = new Object();

        // Native implementation of destination list
        private ICustomDestinationList customDestinationList;

        #region Properties

        private JumpListCustomCategoryCollection customCategoriesCollection;
        /// <summary>
        /// Adds a collection of custom categories to the Taskbar jump list.
        /// </summary>
        /// <param name="customCategories">The catagories to add to the jump list.</param>
        public void AddCustomCategories(params JumpListCustomCategory[] customCategories)
        {
            if (customCategoriesCollection == null)
            {
                // Make sure that we don't create multiple instances
                // of this object
                lock (syncLock)
                {
                    if (customCategoriesCollection == null)
                    {
                        customCategoriesCollection = new JumpListCustomCategoryCollection();
                    }
                }
            }

            foreach (JumpListCustomCategory category in customCategories)
                customCategoriesCollection.Add(category);
        }

        private JumpListItemCollection<IJumpListTask> userTasks;
        /// <summary>
        /// Adds user tasks to the Taskbar JumpList. User tasks can only consist of JumpListTask or
        /// JumpListSeparator objects.
        /// </summary>
        /// <param name="tasks">The user tasks to add to the JumpList.</param>
        public void AddUserTasks(params IJumpListTask[] tasks)
        {
            if (userTasks == null)
            {
                // Make sure that we don't create multiple instances
                // of this object
                lock (syncLock)
                {
                    if (userTasks == null)
                    {
                        userTasks = new JumpListItemCollection<IJumpListTask>();
                    }
                }
            }

            foreach (IJumpListTask task in tasks)
                userTasks.Add(task);
        }

        /// <summary>
        /// Removes all user tasks that have been added.
        /// </summary>
        public void ClearAllUserTasks()
        {
            if (userTasks != null)
                userTasks.Clear();
        }

        /// <summary>
        /// Gets the recommended number of items to add to the jump list.  
        /// </summary>
        /// <remarks>
        /// This number doesn’t 
        /// imply or suggest how many items will appear on the jump list.  
        /// This number should only be used for reference purposes since
        /// the actual number of slots in the jump list can change after the last
        /// refresh due to items being pinned or removed and resolution changes. 
        /// The jump list can increase in size accordingly.
        /// </remarks>
        public uint MaxSlotsInList
        {
            get
            {
                // Because we need the correct number for max slots, start a commit, get the max slots
                // and then abort. If we wait until the user calls RefreshTaskbarlist(), it will be too late.
                // The user needs to use this number before they update the jumplist.

                object removedItems;
                uint maxSlotsInList = 10; // default

                // Native call to start adding items to the taskbar destination list
                HRESULT hr = customDestinationList.BeginList(
                    out maxSlotsInList,
                    ref TaskbarNativeMethods.IID_IObjectArray,
                    out removedItems);

                if (CoreErrorHelper.Succeeded((int)hr))
                    customDestinationList.AbortList();

                return maxSlotsInList;
            }
        }

        /// <summary>
        /// Gets or sets the type of known categories to display.
        /// </summary>
        public JumpListKnownCategoryType KnownCategoryToDisplay { get; set; }

        private int knownCategoryOrdinalPosition = 0;
        /// <summary>
        /// Gets or sets the value for the known category location relative to the 
        /// custom category collection.
        /// </summary>
        public int KnownCategoryOrdinalPosition
        {
            get
            {
                return knownCategoryOrdinalPosition;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "Negative numbers are not allowed for the ordinal position.");

                knownCategoryOrdinalPosition = value;
            }

        }

        /// <summary>
        /// Gets or sets the application ID to use for this jump list.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This is a public property that can be used by the application calling our library")]
        public string ApplicationId { get; private set; }

        #endregion

        /// <summary>
        /// Creates a new instance of the JumpList class with the specified
        /// appId. The JumpList is associated with the main window of the application.
        /// </summary>
        /// <param name="appID">Application Id to use for this instace.</param>
        internal JumpList(string appID)
            : this(appID, TaskbarManager.Instance.OwnerHandle)
        {
        }

        /// <summary>
        /// Creates a new instance of the JumpList class with the specified
        /// appId. The JumpList is associated with the given WPF Window.
        /// </summary>
        /// <param name="appID">Application Id to use for this instace.</param>
        /// <param name="window">WPF Window that is associated with this JumpList</param>
        internal JumpList(string appID, System.Windows.Window window)
            : this(appID, (new System.Windows.Interop.WindowInteropHelper(window)).Handle)
        {
        }

        /// <summary>
        /// Creates a new instance of the JumpList class with the specified
        /// appId. The JumpList is associated with the given window.
        /// </summary>
        /// <param name="appID">Application Id to use for this instace.</param>
        /// <param name="windowHandle">Window handle for the window that is associated with this JumpList</param>
        private JumpList(string appID, IntPtr windowHandle)
        {
            // Throw exception if not running on Win7 or newer
            CoreHelpers.ThrowIfNotWin7();

            // Native implementation of destination list
            customDestinationList = (ICustomDestinationList)new CDestinationList();

            // Set application user model ID
            if (!string.IsNullOrEmpty(appID))
            {
                ApplicationId = appID;

                // If the user hasn't yet set the application id for the whole process,
                // use the first JumpList's AppId for the whole process. This will ensure
                // we have the same JumpList for all the windows (unless user overrides and creates a new
                // JumpList for a specific child window)
                if (!TaskbarManager.Instance.ApplicationIdSetProcessWide)
                    TaskbarManager.Instance.ApplicationId = appID;

                TaskbarManager.Instance.SetApplicationIdForSpecificWindow(windowHandle, appID);
            }
        }

        /// <summary>
        /// Reports document usage to the shell.
        /// </summary>
        /// <param name="destination">The full path of the file to report usage.</param>
        public void AddToRecent(string destination)
        {
            TaskbarNativeMethods.SHAddToRecentDocs(destination);
        }

        /// <summary>
        /// Commits the pending JumpList changes and refreshes the Taskbar.
        /// </summary>
        public void Refresh()
        {
            // Let the taskbar know which specific jumplist we are updating
            if(!string.IsNullOrEmpty(ApplicationId))
                customDestinationList.SetAppID(ApplicationId);

            // Begins rendering on the taskbar destination list
            BeginList();

            // Add custom categories
            AppendCustomCategories();

            // Add user tasks
            AppendTaskList();

            // End rendering of the taskbar destination list
            customDestinationList.CommitList();
        }

        private void BeginList()
        {
            // Get list of removed items from native code
            object removedItems;
            uint maxSlotsInList = 10; // default

            // Native call to start adding items to the taskbar destination list
            HRESULT hr = customDestinationList.BeginList(
                out maxSlotsInList,
                ref TaskbarNativeMethods.IID_IObjectArray,
                out removedItems);

            if (!CoreErrorHelper.Succeeded((int)hr))
                Marshal.ThrowExceptionForHR((int)hr);

            // Process the deleted items
            IEnumerable removedItemsArray = ProcessDeletedItems((IObjectArray)removedItems);

            // Raise the event if items were removed
            if (JumpListItemsRemoved != null && removedItemsArray != null && removedItemsArray.GetEnumerator().MoveNext())
                JumpListItemsRemoved(this, new UserRemovedJumpListItemsEventArgs(removedItemsArray));
        }

        /// <summary>
        /// Occurs when items are removed from the Taskbar's jump list since the last
        /// refresh. 
        /// </summary>
        /// <remarks>
        /// This event is not triggered
        /// immediately when a user removes an item from the jump list but rather
        /// when the application refreshes the task bar list directly.
        /// </remarks>
        public event EventHandler<UserRemovedJumpListItemsEventArgs> JumpListItemsRemoved = delegate { };

        /// <summary>
        /// Retrieves the current list of destinations that have been removed from the existing jump list by the user. 
        /// The removed destinations may become items on a custom jump list.
        /// </summary>
        /// <value>A collection of items (filenames) removed from the existing jump list by the user.</value>
        public IEnumerable RemovedDestinations
        {
            get
            {
                // Get list of removed items from native code
                object removedItems;

                customDestinationList.GetRemovedDestinations(ref TaskbarNativeMethods.IID_IObjectArray, out removedItems);

                return ProcessDeletedItems((IObjectArray)removedItems);
            }
        }

        private IEnumerable ProcessDeletedItems(IObjectArray removedItems)
        {
            uint count;
            removedItems.GetCount(out count);

            if (count == 0)
                return new string[] { };

            // String array passed to the user via the JumpListItemsRemoved
            // event
            List<string> removedItemsArray = new List<string>();

            // Process each removed item based on it's type
            for (uint i = 0; i < count; i++)
            {
                // Native call to retrieve objects from IObjectArray
                object item;
                removedItems.GetAt(i,
                    ref TaskbarNativeMethods.IID_IUnknown,
                    out item);

                // Process item
                if (item is IShellItem)
                {
                    removedItemsArray.Add(RemoveCustomCategoryItem((IShellItem)item));
                }
                else if (item is IShellLinkW)
                {
                    removedItemsArray.Add(RemoveCustomCategoryLink((IShellLinkW)item));
                }
            }

            return removedItemsArray;
        }

        private string RemoveCustomCategoryItem(IShellItem item)
        {
            string path = null;

            if (customCategoriesCollection != null)
            {
                IntPtr pszString = IntPtr.Zero;
                HRESULT hr = item.GetDisplayName(ShellNativeMethods.SIGDN.SIGDN_FILESYSPATH, out pszString);
                if (hr == HRESULT.S_OK && pszString != IntPtr.Zero)
                {
                    path = Marshal.PtrToStringAuto(pszString);
                    // Free the string
                    Marshal.FreeCoTaskMem(pszString);
                }

                // Remove this item from each category
                foreach (JumpListCustomCategory category in customCategoriesCollection)
                    category.RemoveJumpListItem(path);

            }

            return path;
        }


        private string RemoveCustomCategoryLink(IShellLinkW link)
        {
            string path = null;

            if (customCategoriesCollection != null)
            {
                StringBuilder sb = new StringBuilder(256);
                link.GetPath(sb, sb.Capacity, IntPtr.Zero, 2);

                path = sb.ToString();

                // Remove this item from each category
                foreach (JumpListCustomCategory category in customCategoriesCollection)
                    category.RemoveJumpListItem(path);
            }

            return path;
        }

        private void AppendCustomCategories()
        {
            // Initialize our current index in the custom categories list
            int currentIndex = 0;

            // Keep track whether we add the Known Categories to our list
            bool knownCategoriesAdded = false;

            if (customCategoriesCollection != null)
            {
                // Append each category to list
                foreach (JumpListCustomCategory category in customCategoriesCollection)
                {
                    // If our current index is same as the KnownCategory OrdinalPosition,
                    // append the Known Categories
                    if (!knownCategoriesAdded && currentIndex == KnownCategoryOrdinalPosition)
                    {
                        AppendKnownCategories();
                        knownCategoriesAdded = true;
                    }

                    // Don't process empty categories
                    if (category.JumpListItems.Count == 0)
                        continue;

                    IObjectCollection categoryContent =
                        (IObjectCollection)new CEnumerableObjectCollection();

                    // Add each link's shell representation to the object array
                    foreach (IJumpListItem link in category.JumpListItems)
                    {
                        if (link is JumpListItem)
                            categoryContent.AddObject(((JumpListItem)link).NativeShellItem);
                        else if (link is JumpListLink)
                            categoryContent.AddObject(((JumpListLink)link).NativeShellLink);
                    }

                    // Add current category to destination list
                    HRESULT hr = customDestinationList.AppendCategory(
                        category.Name,
                        (IObjectArray)categoryContent);

                    if (!CoreErrorHelper.Succeeded((int)hr))
                    {
                        if ((uint)hr == 0x80040F03)
                            throw new InvalidOperationException("The file type is not registered with this application.");
                        else
                            Marshal.ThrowExceptionForHR((int)hr);
                    }

                    // Increase our current index
                    currentIndex++;
                }
            }

            // If the ordinal position was out of range, append the Known Categories
            // at the end
            if (!knownCategoriesAdded)
                AppendKnownCategories();
        }

        private void AppendTaskList()
        {
            if (userTasks == null || userTasks.Count == 0)
                return;

            IObjectCollection taskContent =
                    (IObjectCollection)new CEnumerableObjectCollection();

            // Add each task's shell representation to the object array
            foreach (IJumpListTask task in userTasks)
            {
                if (task is JumpListLink)
                    taskContent.AddObject(((JumpListLink)task).NativeShellLink);
                else if (task is JumpListSeparator)
                    taskContent.AddObject(((JumpListSeparator)task).NativeShellLink);
            }

            // Add tasks to the taskbar
            HRESULT hr = customDestinationList.AddUserTasks((IObjectArray)taskContent);

            if (!CoreErrorHelper.Succeeded((int)hr))
            {
                if ((uint)hr == 0x80040F03)
                    throw new InvalidOperationException("The file type is not registered with this application.");
                else
                    Marshal.ThrowExceptionForHR((int)hr);
            }
        }

        private void AppendKnownCategories()
        {
            if (KnownCategoryToDisplay == JumpListKnownCategoryType.Recent)
                customDestinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_RECENT);
            else if (KnownCategoryToDisplay == JumpListKnownCategoryType.Frequent)
                customDestinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_FREQUENT);
        }
    }
}
