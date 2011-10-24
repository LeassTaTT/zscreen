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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using Greenshot.Configuration;
using Greenshot.Drawing.Filters;
using Greenshot.Helpers;
using Greenshot.Helpers.IEInterop;
using Greenshot.Plugin;
using Greenshot.UnmanagedHelpers;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using IniFile;

namespace Greenshot.Helpers {
	/// <summary>
	/// The code for this helper comes from: http://www.codeproject.com/KB/graphics/IECapture.aspx
	/// The code is modified with some of the suggestions in different comments and there still were leaks which I fixed.
	/// On top I modified it to use the already available code in Greenshot.
	/// Many thanks to all the people who contributed here!
	/// </summary>
	public class IECaptureHelper {
		private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(IECaptureHelper));
		private static CoreConfiguration configuration = IniConfig.GetIniSection<CoreConfiguration>();
		private static ILanguage language = Language.GetInstance();

		// Helper method to activate a certain IE Tab
		public static void ActivateIETab(WindowDetails ieWindowDetails, int tabIndex) {
			WindowDetails directUIWindowDetails = GetDirectUI(ieWindowDetails);
			// Bring window to the front
			ieWindowDetails.Restore();
			// Get accessible
			Accessible ieAccessible = new Accessible(directUIWindowDetails);
			// Activate Tab
			ieAccessible.ActivateIETab(tabIndex);
		}
		
		/// <summary>
		/// Find the DirectUI window for MSAA (Accessible)
		/// </summary>
		/// <param name="browserWindowDetails">The browser WindowDetails</param>
		/// <returns>WindowDetails for the DirectUI window</returns>
		private static WindowDetails GetDirectUI(WindowDetails browserWindowDetails) {
			WindowDetails tmpWD = browserWindowDetails;
			// Since IE 9 the TabBandClass is less deep!
			if (IEHelper.IEVersion() < 9) {
				tmpWD = tmpWD.GetChild("CommandBarClass");
				if (tmpWD != null) {
					tmpWD = tmpWD.GetChild("ReBarWindow32");
				}
			}
			if (tmpWD != null) {
				tmpWD = tmpWD.GetChild("TabBandClass");
			}
			if (tmpWD != null) {
				tmpWD = tmpWD.GetChild("DirectUIHWND");;
			}
			return tmpWD;
		}
		
		/// <summary>
		/// Simple check if IE is running
		/// </summary>
		/// <returns>bool</returns>
		public static bool IsIERunning() {
			foreach (WindowDetails shellWindow in WindowDetails.GetAllWindows("IEFrame")) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets a list of all IE Windows with the captions of the open tabs
		/// </summary>
		/// <returns>List<KeyValuePair<WindowDetails, string>></returns>
		public static List<KeyValuePair<WindowDetails, string>> GetTabList() {
			List<IntPtr> ieHandleList = new List<IntPtr>();
			Dictionary<WindowDetails, List<string>> browserWindows = new Dictionary<WindowDetails, List<string>>();

			// Find the IE windows
			foreach (WindowDetails ieWindow in WindowDetails.GetAllWindows("IEFrame")) {
				try {
					if (!ieHandleList.Contains(ieWindow.Handle)) {
						WindowDetails directUIWD = GetDirectUI(ieWindow);
						if (directUIWD != null) {
							Accessible accessible = new Accessible(directUIWD);
							browserWindows.Add(ieWindow, accessible.IETabCaptions);
						} else {
							List<string> singleWindowText = new List<string>();
							singleWindowText.Add(ieWindow.Text);
							browserWindows.Add(ieWindow, singleWindowText);
						}
						ieHandleList.Add(ieWindow.Handle);
					}
				} catch (Exception) {
					LOG.Warn("Can't get Info from " + ieWindow.ClassName);
				}
			}

			List<KeyValuePair<WindowDetails, string>> returnList = new List<KeyValuePair<WindowDetails, string>>();
			foreach(WindowDetails windowDetails in browserWindows.Keys) {
				foreach(string tab in browserWindows[windowDetails]) {
					returnList.Add(new KeyValuePair<WindowDetails, string>(windowDetails, tab));
				}
			}
			return returnList;
		}

		/// <summary>
		/// Helper method which will retrieve the IHTMLDocument2 for the supplied window,
		///  or return the first if none is supplied.
		/// </summary>
		/// <param name="browserWindowDetails">The WindowDetails to get the IHTMLDocument2 for</param>
		/// <param name="document2">Ref to the IHTMLDocument2 to return</param>
		/// <returns>The WindowDetails to which the IHTMLDocument2 belongs</returns>
		private static DocumentContainer GetDocument(WindowDetails activeWindow) {
			DocumentContainer returnDocumentContainer = null;
			WindowDetails returnWindow = null;
			IHTMLDocument2 returnDocument2 = null;
			// alternative if no match
			WindowDetails alternativeReturnWindow = null;
			IHTMLDocument2 alternativeReturnDocument2 = null;

			// Find the IE window
			foreach (WindowDetails ieWindow in WindowDetails.GetAllWindows("IEFrame")) {
				LOG.DebugFormat("Processing {0} - {1}", ieWindow.ClassName, ieWindow.Text);
				
				Accessible ieAccessible = null;
				WindowDetails directUIWD = GetDirectUI(ieWindow);
				if (directUIWD != null) {
					ieAccessible = new Accessible(directUIWD);
				}
				if (ieAccessible == null) {
					LOG.InfoFormat("Active Window is {0}", activeWindow.Text);
					if (!ieWindow.Equals(activeWindow)) {
						LOG.WarnFormat("No ieAccessible for {0}", ieWindow.Text);
						continue;
					}
					LOG.DebugFormat("No ieAccessible, but the active window is an IE window: {0}, ", ieWindow.Text);
				}

				try {
					// Get the Document
					IHTMLDocument2 document2 = null;
					int windowMessage = User32.RegisterWindowMessage("WM_HTML_GETOBJECT");
					if (windowMessage == 0) {
						LOG.WarnFormat("Couldn't register WM_HTML_GETOBJECT");
						continue;
					}


					WindowDetails ieServer= ieWindow.GetChild("Internet Explorer_Server");
					if (ieServer == null) {
						LOG.WarnFormat("No Internet Explorer_Server for {0}", ieWindow.Text);
						continue;
					}
					LOG.DebugFormat("Trying WM_HTML_GETOBJECT on {0}", ieServer.ClassName);
					int response;
					User32.SendMessageTimeout(ieServer.Handle, windowMessage, 0, 0,  0x2, 1000, out response);
					if (response != 0) {
						Guid IID_IHTMLDocument = new Guid("626FC520-A41E-11CF-A731-00A0C9082637");
						int hr = Accessible.ObjectFromLresult(response, ref IID_IHTMLDocument, 0, ref document2);
						if (document2 == null) {
							LOG.Error("No IHTMLDocument2 found");
							continue;
						}
					} else {
						LOG.Error("No answer on WM_HTML_GETOBJECT.");
						continue;
					}

					// Get the content window handle for the shellWindow.Document
					IOleWindow oleWindow = (IOleWindow)document2;
					IntPtr contentWindowHandle = IntPtr.Zero;
					if (oleWindow != null) {
						oleWindow.GetWindow(out contentWindowHandle);
					}

					if (contentWindowHandle != IntPtr.Zero) {
						// Get the HTMLDocument to check the hasFocus
						// See: http://social.msdn.microsoft.com/Forums/en-US/vbgeneral/thread/60c6c95d-377c-4bf4-860d-390840fce31c/
						IHTMLDocument4 document4 = (IHTMLDocument4)document2;

						if (document4.hasFocus()) {
							LOG.DebugFormat("Matched focused document: {0}", document2.title);
							// Look no further, we got what we wanted!
							returnDocument2 = document2;
							returnWindow = new WindowDetails(contentWindowHandle);
							break;
						}
						try {
							if (ieWindow.Equals(activeWindow)) {
								returnDocument2 = document2;
								returnWindow = new WindowDetails(contentWindowHandle);
								break;
							} else if (ieAccessible != null && returnWindow == null && document2.title.Equals(ieAccessible.IEActiveTabCaption) ) {
								LOG.DebugFormat("Title: {0}", document2.title);
								returnDocument2 = document2;
								returnWindow = new WindowDetails(contentWindowHandle);
							} else {
								alternativeReturnDocument2 = document2;
								alternativeReturnWindow = new WindowDetails(contentWindowHandle);								
							}
						} catch (Exception) {
							alternativeReturnDocument2 = document2;
							alternativeReturnWindow = new WindowDetails(contentWindowHandle);
						}
					}
				} catch (Exception e) {
					LOG.Error(e);
					LOG.DebugFormat("Major problem: Problem retrieving Document from {0}", ieWindow.Text);
				}
			}

			// check if we have something to return
			if (returnWindow != null) {
				// As it doesn't have focus, make sure it's active
				returnWindow.Restore();
				returnWindow.GetParent();

				// Create the container
				returnDocumentContainer = new DocumentContainer(returnDocument2, returnWindow);
			}

			if (returnDocumentContainer == null && alternativeReturnDocument2 != null) {
				// As it doesn't have focus, make sure it's active
				alternativeReturnWindow.Restore();
				alternativeReturnWindow.GetParent();
				// Create the container
				returnDocumentContainer =  new DocumentContainer(alternativeReturnDocument2, alternativeReturnWindow);
			}
			return returnDocumentContainer;
		}

		/// <summary>
		/// Here the logic for capturing the IE Content is located
		/// </summary>
		/// <param name="capture">ICapture where the capture needs to be stored</param>
		/// <returns>ICapture with the content (if any)</returns>
		public static ICapture CaptureIE(ICapture capture) {
			WindowDetails activeWindow = WindowDetails.GetActiveWindow();
			
			// Show backgroundform after retrieving the active window..
			BackgroundForm backgroundForm = new BackgroundForm(language.GetString(LangKey.contextmenu_captureie), language.GetString(LangKey.wait_ie_capture));
			backgroundForm.Show();
			//BackgroundForm backgroundForm = BackgroundForm.ShowAndWait(language.GetString(LangKey.contextmenu_captureie), language.GetString(LangKey.wait_ie_capture));
			try {
				//Get IHTMLDocument2 for the current active window
				DocumentContainer documentContainer = GetDocument(activeWindow);
	
				// Nothing found
				if (documentContainer == null) {
					LOG.Debug("Nothing to capture found");
					return null;
				}
				LOG.DebugFormat("Window class {0}", documentContainer.ContentWindow.ClassName);
				LOG.DebugFormat("Window location {0}", documentContainer.ContentWindow.Location);

				// The URL is available unter "document2.url" and can be used to enhance the meta-data etc.
				capture.CaptureDetails.AddMetaData("url", documentContainer.Url);
	
				// bitmap to return
				Bitmap returnBitmap = null;
				try {
					returnBitmap = capturePage(documentContainer, capture);
				} catch (Exception e) {
					LOG.Error("Exception found, ignoring and returning nothing! Error was: ", e);
				}
				
				if (returnBitmap == null) {
					return null;
				}
	
				// Store the bitmap for further processing
				capture.Image = returnBitmap;
				// Store the location of the window
				capture.Location = documentContainer.ContentWindow.Location;
				// Store the title of the Page
				capture.CaptureDetails.Title = documentContainer.Name;
	
				// Only move the mouse to correct for the capture offset
				capture.MoveMouseLocation(-documentContainer.ViewportRectangle.X, -documentContainer.ViewportRectangle.Y);
				// Used to be: capture.MoveMouseLocation(-(capture.Location.X + documentContainer.CaptureOffset.X), -(capture.Location.Y + documentContainer.CaptureOffset.Y));
			} finally {
				// Always close the background form
				backgroundForm.CloseDialog();
			}
			return capture;
		}

		/// <summary>
		/// Capture the actual page (document)
		/// </summary>
		/// <param name="documentContainer">The document wrapped in a container</param>
		/// <returns>Bitmap with the page content as an image</returns>
		private static Bitmap capturePage(DocumentContainer documentContainer, ICapture capture) {
			WindowDetails contentWindowDetails = documentContainer.ContentWindow;
			// Calculate the page size
			int pageWidth = documentContainer.ScrollWidth;
			int pageHeight = documentContainer.ScrollHeight;

			// Here we loop over all the frames and try to make sure they don't overlap
			bool movedFrame;
			do {
				movedFrame = false;
				foreach(DocumentContainer currentFrame in documentContainer.Frames) {
					foreach(DocumentContainer otherFrame in documentContainer.Frames) {
						if (otherFrame.ID == currentFrame.ID) {
							continue;
						}
						// check if we need to move
						if (otherFrame.DestinationRectangle.IntersectsWith(currentFrame.DestinationRectangle) && !otherFrame.SourceRectangle.IntersectsWith(currentFrame.SourceRectangle)) {
							bool horizalResize = currentFrame.SourceSize.Width < currentFrame.DestinationSize.Width;
							bool verticalResize = currentFrame.SourceSize.Width < currentFrame.DestinationSize.Width;
							bool horizalMove = currentFrame.SourceLeft < currentFrame.DestinationLeft;
							bool verticalMove = currentFrame.SourceTop < currentFrame.DestinationTop;
							bool leftOf = currentFrame.SourceRight <= otherFrame.SourceLeft;
							bool belowOf = currentFrame.SourceBottom <= otherFrame.SourceTop;
							
							if ((horizalResize || horizalMove) && leftOf) {
								// Current frame resized horizontally, so move other horizontally
								LOG.DebugFormat("Moving Frame {0} horizontally to the right of {1}", otherFrame.Name, currentFrame.Name);
								otherFrame.DestinationLeft = currentFrame.DestinationRight;
								movedFrame = true;
							} else if ((verticalResize || verticalMove) && belowOf){
								// Current frame resized vertically, so move other vertically
								LOG.DebugFormat("Moving Frame {0} vertically to the bottom of {1}", otherFrame.Name, currentFrame.Name);
								otherFrame.DestinationTop = currentFrame.DestinationBottom;
								movedFrame = true;
							} else {
								LOG.DebugFormat("Frame {0} intersects with {1}", otherFrame.Name, currentFrame.Name);
							}
						}
					}
				}
			} while(movedFrame);

			bool movedMouse = false;
			// Correct cursor location to be inside the window
			capture.MoveMouseLocation(-documentContainer.ContentWindow.Location.X, -documentContainer.ContentWindow.Location.Y);
			// See if the page has the correct size, as we capture the full frame content AND might have moved them
			// the normal pagesize will no longer be enough
			foreach(DocumentContainer frameData in documentContainer.Frames) {
				if (!movedMouse && frameData.SourceRectangle.Contains(capture.CursorLocation)) {
					// Correct mouse cursor location for scrolled position (so it shows on the capture where it really was)
					capture.MoveMouseLocation(frameData.ScrollLeft, frameData.ScrollTop);
					movedMouse = true;
					// Apply any other offset changes
					int offsetX = frameData.DestinationLocation.X - frameData.SourceLocation.X;
					int offsetY = frameData.DestinationLocation.Y - frameData.SourceLocation.Y;
					capture.MoveMouseLocation(offsetX, offsetY);
				}

				//Get Frame Width & Height
				pageWidth = Math.Max(pageWidth, frameData.DestinationRight);
				pageHeight = Math.Max(pageHeight, frameData.DestinationBottom);
			}
			
			// If the mouse hasn't been moved, it wasn't on a frame. So correct the mouse according to the scroll position of the document
			if (!movedMouse) {
				// Correct mouse cursor location
				capture.MoveMouseLocation(documentContainer.ScrollLeft, documentContainer.ScrollTop);
			}
			
			// Limit the size as the editor currently can't work with sizes > short.MaxValue
			if (pageWidth > short.MaxValue) {
				LOG.WarnFormat("Capture has a width of {0} which bigger than the maximum supported {1}, cutting width to the maxium.", pageWidth, short.MaxValue);
				pageWidth = Math.Min(pageWidth, short.MaxValue);
			}
			if (pageHeight > short.MaxValue) {
				LOG.WarnFormat("Capture has a height of {0} which bigger than the maximum supported {1}, cutting height to the maxium", pageHeight, short.MaxValue);
				pageHeight = Math.Min(pageHeight, short.MaxValue);
			}
			
			//Create a target bitmap to draw into with the calculated page size
			Bitmap returnBitmap = new Bitmap(pageWidth, pageHeight, PixelFormat.Format24bppRgb);
			using (Graphics graphicsTarget = Graphics.FromImage(returnBitmap)) {
				// Clear the target with the backgroundcolor
				Color clearColor = documentContainer.BackgroundColor;
				LOG.DebugFormat("Clear color: {0}", clearColor);
				graphicsTarget.Clear(clearColor);

				// Get the base document & draw it
				drawDocument(documentContainer, contentWindowDetails, graphicsTarget);
				//ParseElements(documentContainer, graphicsTarget, returnBitmap);
				// Loop over the frames and clear their source area so we don't see any artefacts
				foreach(DocumentContainer frameDocument in documentContainer.Frames) {
					using(Brush brush = new SolidBrush(clearColor)) {
						graphicsTarget.FillRectangle(brush, frameDocument.SourceRectangle);
					}
				}
				// Loop over the frames and capture their content
				foreach(DocumentContainer frameDocument in documentContainer.Frames) {
					drawDocument(frameDocument, contentWindowDetails, graphicsTarget);
					//ParseElements(frameDocument, graphicsTarget, returnBitmap);
				}
			}
			return returnBitmap;
		}
		
		/// <summary>
		/// Used as an example
		/// </summary>
		/// <param name="documentContainer"></param>
		/// <param name="graphicsTarget"></param>
		/// <param name="returnBitmap"></param>
		private static void ParseElements(DocumentContainer documentContainer, Graphics graphicsTarget, Bitmap returnBitmap) {
			foreach(ElementContainer element in documentContainer.GetElementsByTagName("input", new string[]{"greenshot"})) {
				if (element.attributes.ContainsKey("greenshot") && element.attributes["greenshot"] != null) {
					string greenshotAction = element.attributes["greenshot"];
					if ("hide".Equals(greenshotAction)) {
						PixelizationFilter.Apply(graphicsTarget, returnBitmap, element.rectangle, 4);
					} else if ("red".Equals(greenshotAction)) {
						using (Brush brush = new SolidBrush(Color.Red)) {
							graphicsTarget.FillRectangle(brush, element.rectangle);
						}
					}
				}
			}
		}

		/// <summary>
		/// This method takes the actual capture of the document (frame)
		/// </summary>
		/// <param name="frameDocument"></param>
		/// <param name="contentWindowDetails">Needed for referencing the location of the frame</param>
		/// <returns>Bitmap with the capture</returns>
		private static void drawDocument(DocumentContainer documentContainer, WindowDetails contentWindowDetails, Graphics graphicsTarget) {
			documentContainer.setAttribute("scroll", 1);

			//Get Browser Window Width & Height
			int pageWidth = documentContainer.ScrollWidth;
			int pageHeight = documentContainer.ScrollHeight;
			if (pageWidth * pageHeight == 0) {
				LOG.WarnFormat("Empty page for DocumentContainer {0}: {1}", documentContainer.Name, documentContainer.Url);
				return;
			}

			//Get Screen Width & Height (this is better as the WindowDetails.ClientRectangle as the real visible parts are there!
			int viewportWidth = documentContainer.ClientWidth;
			int viewportHeight = documentContainer.ClientHeight;
			if (viewportWidth * viewportHeight == 0) {
				LOG.WarnFormat("Empty viewport for DocumentContainer {0}: {1}", documentContainer.Name, documentContainer.Url);
				return;
			}

			// Store the current location so we can set the browser back and use it for the mouse cursor
			int startLeft = documentContainer.ScrollLeft;
			int startTop = documentContainer.ScrollTop;

			LOG.DebugFormat("Capturing {4} with total size {0},{1} displayed with size {2},{3}", pageWidth, pageHeight, viewportWidth, viewportHeight, documentContainer.Name);

			// Variable used for looping horizontally
			int horizontalPage = 0;
			
			// The location of the browser, used as the destination into the bitmap target
			Point targetOffset = new Point();
			
			// Loop of the pages and make a copy of the visible viewport
			while ((horizontalPage * viewportWidth) < pageWidth) {
				// Scroll to location
				documentContainer.ScrollLeft = viewportWidth * horizontalPage;
				targetOffset.X = documentContainer.ScrollLeft;

				// Variable used for looping vertically
				int verticalPage = 0;
				while ((verticalPage * viewportHeight) < pageHeight) {
					// Scroll to location
					documentContainer.ScrollTop = viewportHeight * verticalPage;
					//Shoot visible window
					targetOffset.Y  = documentContainer.ScrollTop;

					// Draw the captured fragment to the target, but "crop" the scrollbars etc while capturing 
					Size viewPortSize = new Size(viewportWidth, viewportHeight);
					Rectangle clientRectangle = new Rectangle(documentContainer.SourceLocation, viewPortSize);
					Image fragment = contentWindowDetails.PrintWindow();
					if (fragment != null) {
						LOG.DebugFormat("Captured fragment size: {0}x{1}", fragment.Width, fragment.Height);
						try {
							// cut all junk, due to IE "border" we need to remove some parts
							Rectangle viewportRect = documentContainer.ViewportRectangle;
							if (!Rectangle.Empty.Equals(viewportRect)) {
								LOG.DebugFormat("Cropping to viewport: {0}", viewportRect);
								ImageHelper.Crop(ref fragment, ref viewportRect);
							}
							LOG.DebugFormat("Cropping to clientRectangle: {0}", clientRectangle);
							// Crop to clientRectangle
							if (ImageHelper.Crop(ref fragment, ref clientRectangle)) {
								Point targetLocation = new Point(documentContainer.DestinationLocation.X, documentContainer.DestinationLocation.Y);
								LOG.DebugFormat("Fragment targetLocation is {0}", targetLocation);
								targetLocation.Offset(targetOffset);
								LOG.DebugFormat("After offsetting the fragment targetLocation is {0}", targetLocation);
								LOG.DebugFormat("Drawing fragment of size {0} to {1}", fragment.Size, targetLocation);
								graphicsTarget.DrawImage(fragment, targetLocation);
								graphicsTarget.Flush();
							} else {
								// somehow we are capturing nothing!?
								LOG.WarnFormat("Crop of {0} failed?", documentContainer.Name);
								break;
							}
						} finally {
							fragment.Dispose();
						}
					} else {
						LOG.WarnFormat("Capture of {0} failed!", documentContainer.Name);
					}
					verticalPage++;
				}
				horizontalPage++;
			}
			// Return to where we were
			documentContainer.ScrollLeft = startLeft;
			documentContainer.ScrollTop = startTop;
		}
	}
}
