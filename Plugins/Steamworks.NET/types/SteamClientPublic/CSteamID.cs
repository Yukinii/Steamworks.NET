// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2015 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// Changes to this file will be reverted when you update Steamworks.NET

namespace Steamworks {
	public struct CSteamID : System.IEquatable<CSteamID>, System.IComparable<CSteamID> {
		public static readonly CSteamID Nil = new CSteamID();
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0), 0, EUniverse.Invalid, EAccountType.Invalid);
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0), 0, EUniverse.Public, EAccountType.Invalid);
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1), 0, EUniverse.Invalid, EAccountType.Invalid);
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2), 0, EUniverse.Invalid, EAccountType.Invalid);
		public ulong _SteamID;

		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType) {
			_SteamID = 0;
			Set(unAccountID, eUniverse, eAccountType);
		}

		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType) {
			_SteamID = 0;
#if _SERVER && Assert
		Assert( ! ( ( EAccountType.Individual == eAccountType ) && ( unAccountInstance > k_unSteamUserWebInstance ) ) );	// enforce that for individual accounts, instance is always 1
#endif // _SERVER
			InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		public CSteamID(ulong ulSteamID) {
			_SteamID = ulSteamID;
		}

		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType) {
			SetAccountID(unAccountID);
			SetEUniverse(eUniverse);
			SetEAccountType(eAccountType);

			if (eAccountType == EAccountType.Clan || eAccountType == EAccountType.GameServer) {
				SetAccountInstance(0);
			}
			else {
				// by default we pick the desktop instance
				SetAccountInstance(Constants.k_unSteamUserDesktopInstance);
			}
		}

		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType) {
			SetAccountID(unAccountID);
			SetEUniverse(eUniverse);
			SetEAccountType(eAccountType);
			SetAccountInstance(unInstance);
		}

		public void Clear() {
			_SteamID = 0;
		}

		public void CreateBlankAnonLogon(EUniverse eUniverse) {
			SetAccountID(new AccountID_t(0));
			SetEUniverse(eUniverse);
			SetEAccountType(EAccountType.AnonGameServer);
			SetAccountInstance(0);
		}

		public void CreateBlankAnonUserLogon(EUniverse eUniverse) {
			SetAccountID(new AccountID_t(0));
			SetEUniverse(eUniverse);
			SetEAccountType(EAccountType.AnonUser);
			SetAccountInstance(0);
		}

		//-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous game server login that will be filled in?
		//-----------------------------------------------------------------------------
		public bool BBlankAnonAccount() => GetAccountID() == new AccountID_t(0) && BAnonAccount() && GetUnAccountInstance() == 0;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this a game server account id?  (Either persistent or anonymous)
		//-----------------------------------------------------------------------------
		public bool BGameServerAccount() => GetEAccountType() == EAccountType.GameServer || GetEAccountType() == EAccountType.AnonGameServer;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this a persistent (not anonymous) game server account id?
		//-----------------------------------------------------------------------------
		public bool BPersistentGameServerAccount() => GetEAccountType() == EAccountType.GameServer;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous game server account id?
		//-----------------------------------------------------------------------------
		public bool BAnonGameServerAccount() => GetEAccountType() == EAccountType.AnonGameServer;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this a content server account id?
		//-----------------------------------------------------------------------------
		public bool BContentServerAccount() => GetEAccountType() == EAccountType.ContentServer;


	    //-----------------------------------------------------------------------------
		// Purpose: Is this a clan account id?
		//-----------------------------------------------------------------------------
		public bool BClanAccount() => GetEAccountType() == EAccountType.Clan;


	    //-----------------------------------------------------------------------------
		// Purpose: Is this a chat account id?
		//-----------------------------------------------------------------------------
		public bool BChatAccount() => GetEAccountType() == EAccountType.Chat;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this a chat account id?
		//-----------------------------------------------------------------------------
		public bool IsLobby() => (GetEAccountType() == EAccountType.Chat)
		                         && (GetUnAccountInstance() & (int)EChatSteamIdInstanceFlags.Lobby) != 0;


	    //-----------------------------------------------------------------------------
		// Purpose: Is this an individual user account id?
		//-----------------------------------------------------------------------------
		public bool BIndividualAccount() => GetEAccountType() == EAccountType.Individual || GetEAccountType() == EAccountType.ConsoleUser;


	    //-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous account?
		//-----------------------------------------------------------------------------
		public bool BAnonAccount() => GetEAccountType() == EAccountType.AnonUser || GetEAccountType() == EAccountType.AnonGameServer;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this an anonymous user account? ( used to create an account or reset a password )
		//-----------------------------------------------------------------------------
		public bool BAnonUserAccount() => GetEAccountType() == EAccountType.AnonUser;

	    //-----------------------------------------------------------------------------
		// Purpose: Is this a faked up Steam ID for a PSN friend account?
		//-----------------------------------------------------------------------------
		public bool BConsoleUserAccount() => GetEAccountType() == EAccountType.ConsoleUser;

	    public void SetAccountID(AccountID_t other) {
			_SteamID = (_SteamID & ~(0xFFFFFFFFul << 0)) | (((ulong)(other) & 0xFFFFFFFFul) << 0);
		}

		public void SetAccountInstance(uint other) {
			_SteamID = (_SteamID & ~(0xFFFFFul << 32)) | ((other & 0xFFFFFul) << 32);
		}

		// This is a non standard/custom function not found in C++ Steamworks
		public void SetEAccountType(EAccountType other) {
			_SteamID = (_SteamID & ~(0xFul << 52)) | (((ulong)(other) & 0xFul) << 52);
		}

		public void SetEUniverse(EUniverse other) {
			_SteamID = (_SteamID & ~(0xFFul << 56)) | (((ulong)(other) & 0xFFul) << 56);
		}

		public void ClearIndividualInstance() {
			if (BIndividualAccount())
				SetAccountInstance(0);
		}

		public bool HasNoIndividualInstance() => BIndividualAccount() && (GetUnAccountInstance() == 0);

	    public AccountID_t GetAccountID() => new AccountID_t((uint)(_SteamID & 0xFFFFFFFFul));

	    public uint GetUnAccountInstance() => (uint)((_SteamID >> 32) & 0xFFFFFul);

	    public EAccountType GetEAccountType() => (EAccountType)((_SteamID >> 52) & 0xFul);

	    public EUniverse GetEUniverse() => (EUniverse)((_SteamID >> 56) & 0xFFul);

	    public bool IsValid() {
			if (GetEAccountType() <= EAccountType.Invalid || GetEAccountType() >= EAccountType.Max)
				return false;

			if (GetEUniverse() <= EUniverse.Invalid || GetEUniverse() >= EUniverse.Max)
				return false;

			if (GetEAccountType() == EAccountType.Individual) {
				if (GetAccountID() == new AccountID_t(0) || GetUnAccountInstance() > Constants.k_unSteamUserWebInstance)
					return false;
			}

			if (GetEAccountType() == EAccountType.Clan) {
				if (GetAccountID() == new AccountID_t(0) || GetUnAccountInstance() != 0)
					return false;
			}

			if (GetEAccountType() == EAccountType.GameServer) {
				if (GetAccountID() == new AccountID_t(0))
					return false;
				// Any limit on instances?  We use them for local users and bots
			}
			return true;
		}

		#region Overrides
		public override string ToString() => _SteamID.ToString();

	    public override bool Equals(object other) => other is CSteamID && this == (CSteamID)other;

	    public override int GetHashCode() => _SteamID.GetHashCode();

	    public static bool operator ==(CSteamID x, CSteamID y) => x._SteamID == y._SteamID;

	    public static bool operator !=(CSteamID x, CSteamID y) => !(x == y);

	    public static explicit operator CSteamID(ulong value) => new CSteamID(value);
	    public static explicit operator ulong(CSteamID that) => that._SteamID;

	    public bool Equals(CSteamID other) => _SteamID == other._SteamID;

	    public int CompareTo(CSteamID other) => _SteamID.CompareTo(other._SteamID);

	    #endregion
	}
}
