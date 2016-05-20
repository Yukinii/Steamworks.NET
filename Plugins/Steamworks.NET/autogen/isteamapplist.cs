// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2015 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

using System.Runtime.InteropServices;

namespace Steamworks {
	public static class SteamAppList {
		public static uint GetNumInstalledApps() {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamAppList_GetNumInstalledApps();
		}

		public static uint GetInstalledApps(AppId_t[] appId, uint unMaxAppIDs) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamAppList_GetInstalledApps(appId, unMaxAppIDs);
		}

		/// <summary>
		/// <para> returns -1 if no name was found</para>
		/// </summary>
		public static int GetAppName(AppId_t appId, out string name, int maxNameLen) {
			InteropHelp.TestIfAvailableClient();
			var name2 = Marshal.AllocHGlobal(maxNameLen);
			var ret = NativeMethods.ISteamAppList_GetAppName(appId, name2, maxNameLen);
			name = ret != -1 ? InteropHelp.PtrToStringUTF8(name2) : null;
			Marshal.FreeHGlobal(name2);
			return ret;
		}

		/// <summary>
		/// <para> returns -1 if no dir was found</para>
		/// </summary>
		public static int GetAppInstallDir(AppId_t appId, out string pchDirectory, int maxNameLen) {
			InteropHelp.TestIfAvailableClient();
			var pchDirectory2 = Marshal.AllocHGlobal(maxNameLen);
			var ret = NativeMethods.ISteamAppList_GetAppInstallDir(appId, pchDirectory2, maxNameLen);
			pchDirectory = ret != -1 ? InteropHelp.PtrToStringUTF8(pchDirectory2) : null;
			Marshal.FreeHGlobal(pchDirectory2);
			return ret;
		}

		/// <summary>
		/// <para> return the buildid of this app, may change at any time based on backend updates to the game</para>
		/// </summary>
		public static int GetAppBuildId(AppId_t appId) {
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamAppList_GetAppBuildId(appId);
		}
	}
}