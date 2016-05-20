// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2015 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

namespace Steamworks {
	public static class SteamGameServerStats {
		/// <summary>
		/// <para> downloads stats for the user</para>
		/// <para> returns a GSStatsReceived_t callback when completed</para>
		/// <para> if the user has no stats, GSStatsReceived_t._eResult will be set to k_EResultFail</para>
		/// <para> these stats will only be auto-updated for clients playing on the server. For other</para>
		/// <para> users you'll need to call RequestUserStats() again to refresh any data</para>
		/// </summary>
		public static SteamAPICall_t RequestUserStats(CSteamID steamIDUser) {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerStats_RequestUserStats(steamIDUser);
		}

		/// <summary>
		/// <para> requests stat information for a user, usable after a successful call to RequestUserStats()</para>
		/// </summary>
		public static bool GetUserStat(CSteamID steamIDUser, string name, out int pData) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_GetUserStat(steamIDUser, name2, out pData);
			}
		}

		public static bool GetUserStat(CSteamID steamIDUser, string name, out float pData) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_GetUserStat_(steamIDUser, name2, out pData);
			}
		}

		public static bool GetUserAchievement(CSteamID steamIDUser, string name, out bool pbAchieved) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_GetUserAchievement(steamIDUser, name2, out pbAchieved);
			}
		}

		/// <summary>
		/// <para> Set / update stats and achievements.</para>
		/// <para> Note: These updates will work only on stats game servers are allowed to edit and only for</para>
		/// <para> game servers that have been declared as officially controlled by the game creators.</para>
		/// <para> Set the IP range of your official servers on the Steamworks page</para>
		/// </summary>
		public static bool SetUserStat(CSteamID steamIDUser, string name, int nData) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_SetUserStat(steamIDUser, name2, nData);
			}
		}

		public static bool SetUserStat(CSteamID steamIDUser, string name, float fData) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_SetUserStat_(steamIDUser, name2, fData);
			}
		}

		public static bool UpdateUserAvgRateStat(CSteamID steamIDUser, string name, float flCountThisSession, double dSessionLength) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_UpdateUserAvgRateStat(steamIDUser, name2, flCountThisSession, dSessionLength);
			}
		}

		public static bool SetUserAchievement(CSteamID steamIDUser, string name) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_SetUserAchievement(steamIDUser, name2);
			}
		}

		public static bool ClearUserAchievement(CSteamID steamIDUser, string name) {
			InteropHelp.TestIfAvailableGameServer();
			using (var name2 = new InteropHelp.UTF8StringHandle(name)) {
				return NativeMethods.ISteamGameServerStats_ClearUserAchievement(steamIDUser, name2);
			}
		}

		/// <summary>
		/// <para> Store the current data on the server, will get a GSStatsStored_t callback when set.</para>
		/// <para> If the callback has a result of k_EResultInvalidParam, one or more stats</para>
		/// <para> uploaded has been rejected, either because they broke constraints</para>
		/// <para> or were out of date. In this case the server sends back updated values.</para>
		/// <para> The stats should be re-iterated to keep in sync.</para>
		/// </summary>
		public static SteamAPICall_t StoreUserStats(CSteamID steamIDUser) {
			InteropHelp.TestIfAvailableGameServer();
			return (SteamAPICall_t)NativeMethods.ISteamGameServerStats_StoreUserStats(steamIDUser);
		}
	}
}