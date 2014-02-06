﻿using System;
using System.Collections.Generic;

using TeraWatcherAPI.Events;
using TeraWatcherAPI.Types;

namespace TeraPacketEncryption {
	class Parse : TeraWatcherAPI.IHandler {
		const int NAME_CHAR_MAX_LENGTH = 20;
		const int NAME_GUILD_MAX_LENGTH = 32;
		const int NAME_GUILDRANK_MAX_LENGTH = 32;
		const int DEBUG_LEVEL = 1;

		private delegate void Handle(byte[] data);

		public event gPacketHandler sPacket;

		public event sAbnormalAddHandler sAbnormalAdd;
		public event sAbnormalRemoveHandler sAbnormalRemove;
		public event sAbnormalUpdateHandler sAbnormalUpdate;
		public event sAbsorbDamageHandler sAbsorbDamage;
		public event sAttackResultHandler sAttackResult;
		public event sAttackStartHandler sAttackStart;
		public event sChatMessageHandler sChatMessage;
		public event sCombatStatusHandler sCombatStatus;
		public event sConditionActivateHandler sConditionActivate;
		public event sConditionAddHandler sConditionAdd;
		public event sConditionListHandler sConditionList;
		public event sConditionRemoveHandler sConditionRemove;
		public event sGuildInfoHandler sGuildInfo;
		public event sImageHandler sImage;
		public event sLifeStatusHandler sLifeStatus;
		public event sLockonHandler sLockon;
		public event sNpcCombatStatusHandler sNpcCombatStatus;
		public event sNpcEmotionHandler sNpcEmotion;
		public event sNpcHpHandler sNpcHp;
		public event sNpcStatusHandler sNpcStatus;
		public event sPartyAbnormalAddHandler sPartyAbnormalAdd;
		public event sPartyAbnormalListHandler sPartyAbnormalList;
		public event sPartyAbnormalRemoveHandler sPartyAbnormalRemove;
		public event sPartyAbnormalUpdateHandler sPartyAbnormalUpdate;
		public event sPartyConditionActivateHandler sPartyConditionActivate;
		public event sPartyConditionAddHandler sPartyConditionAdd;
		public event sPartyConditionRemoveHandler sPartyConditionRemove;
		public event sPartyDeathHandler sPartyDeath;
		public event sPartyInvitePrivHandler sPartyInvitePriv;
		public event sPartyLeaderHandler sPartyLeader;
		public event sPartyListHandler sPartyList;
		public event sPartyUpdateHandler sPartyUpdate;
		public event sPartyUpdateHpHandler sPartyUpdateHp;
		public event sPartyUpdateMpHandler sPartyUpdateMp;
		public event sPartyUpdateReHandler sPartyUpdateRe;
		public event sPlayerInfoHandler sPlayerInfo;
		public event sProjectedAttackHandler sProjectedAttack;
		public event sProjectedAttackRemoveHandler sProjectedAttackRemove;
		public event sProjectileHandler sProjectile;
		public event sProjectileRemoveHandler sProjectileRemove;
		public event sSelfStaminaHandler sSelfStamina;
		public event sTargetInfoHandler sTargetInfo;
		public event sUpdateHpHandler sUpdateHp;
		public event sUpdateMpHandler sUpdateMp;
		public event sUpdateReHandler sUpdateRe;

		public event gPacketHandler cPacket;

		public Parse() {
		}

		public void Log(int level, string format, params object[] args) {
			Console.WriteLine(format, args);
		}

		public void ServerCommand(uint opCode, byte[] data) {
			var PacketArgs = new gPacketArgs {
				data = data,
				unknown = false
			};

			switch (opCode) {
				//case 0x8568: ParsePartyMemberMove(data); break;
				//case 0xA7B4: ParseNpcInfo(data); break;
				//case 0xD6FA: ParseSelfInfo(data); break;
				//case 0xE570: ParseMinionDeath(data); break;
				case 0x530A: _sImage(data); break;
				case 0x53B2: _sPlayerInfo(data); break;
				case 0x61CE: _sPartyConditionActivate(data); break;
				case 0x63B2: _sTargetInfo(data); break;
				case 0x671E: _sConditionRemove(data); break;
				case 0x6827: _sPartyConditionAdd(data); break;
				case 0x7695: _sPartyUpdate(data); break;
				case 0x7C3B: _sProjectileRemove(data); break;
				case 0x7D18: _sLockon(data); break;
				case 0x7DAF: _sPartyUpdateMp(data); break;
				case 0x8DD4: _sAbsorbDamage(data); break;
				case 0x900F: _sNpcEmotion(data); break;
				case 0x94EF: _sAttackResult(data); break;
				case 0x96D0: _sAbnormalUpdate(data); break;
				case 0x9C09: _sLifeStatus(data); break;
				case 0x9CAE: _sAbnormalAdd(data); break;
				case 0x9EE7: _sConditionActivate(data); break;
				case 0xA13E: _sPartyAbnormalAdd(data); break;
				case 0xA3E2: _sCombatStatus(data); break;
				case 0xA5EB: _sNpcStatus(data); break;
				case 0xAD3D: _sChatMessage(data); break;
				case 0xAE97: _sPartyDeath(data); break;
				case 0xB426: _sPartyAbnormalRemove(data); break;
				case 0xB470: _sAttackEnd(data); break;
				case 0xB7F5: _sConditionList(data); break;
				case 0xBAAB: _sUpdateMp(data); break;
				case 0xBB69: _sPartyUpdateRe(data); break;
				case 0xBFCE: _sPartyLeader(data); break;
				case 0xC24B: _sConditionAdd(data); break;
				case 0xC280: _sAbnormalRemove(data); break;
				case 0xC50B: _sPartyList(data); break;
				case 0xC513: _sProjectedAttack(data); break;
				case 0xCCD0: _sPartyUpdateHp(data); break;
				case 0xCE77: _sUpdateRe(data); break;
				case 0xCEA4: _sPartyConditionRemove(data); break;
				case 0xD1F5: _sNpcHp(data); break;
				case 0xD46C: _sPartyAbnormalUpdate(data); break;
				case 0xD65E: _sProjectedAttackRemove(data); break;
				case 0xE0FA: _sPartyAbnormalList(data); break;
				case 0xE1B2: _sNpcCombatStatus(data); break;
				case 0xE628: _sUpdateHp(data); break;
				case 0xEB10: _sProjectile(data); break;
				case 0xF22C: _sAttackStart(data); break;
				case 0xF2DB: _sGuildInfo(data); break;
				case 0xF471: _sSelfStamina(data); break;
				case 0xF5A3: _sPartyInvitePriv(data); break;
				default: PacketArgs.unknown = true; break;
			}

			var callback = sPacket;
			if (callback != null) callback(PacketArgs);
		}

		public void ClientCommand(uint opCode, byte[] data) {
			var PacketArgs = new gPacketArgs {
				data = data,
				unknown = false
			};

			switch (opCode) {
				case 0xF215: CParseYield(data); break;
				case 0x685D: CParseTargetSelect(data); break;
				case 0xEA79: CParseLockon(data); break;
				default: PacketArgs.unknown = true; break;
			}

			if (cPacket != null) cPacket(PacketArgs);
		}

		private string GetString(byte[] value, int startIndex, int maxLength) {
			int byteLength;
			return GetString(value, startIndex, maxLength, out byteLength);
		}

		private string GetString(byte[] value, int startIndex, int maxLength, out int byteLength) {
			int i;
			char c;
			string result = "";

			int maxIndex = startIndex + maxLength * 2;
			if (value.Length < maxIndex) maxIndex = value.Length;

			for (i = startIndex; i < maxIndex; i += 2) {
				c = BitConverter.ToChar(value, i);
				if (c == '\0') break;
				result += c;
			}

			byteLength = i - startIndex + 2;
			return result;
		}

		private void ParseSelfInfo(byte[] data) { // 0xD6FA
			/*
			Self = BitConverter.ToUInt64(data, 18);

			var Player = new Player();
			Player.UID = Self;
			Player.Name = GetString(data, 265, NAME_CHAR_MAX_LENGTH);
			GetClassAndRace(data, 14, ref Player);

			Players[Self] = Player;

			WriteEvent("sSelfInfo", String.Format("\"cID\": {0}, \"name\": {1}, \"type\": {2}",
				Player.UID,
				GetJson(Player.Name),
				BitConverter.ToUInt32(data, 14)));
			 */
		}

		private void _sGuildInfo(byte[] data) { // 0xF2DB
			var callback = sGuildInfo;
			if (callback == null) return;

			ushort numRanks = BitConverter.ToUInt16(data, 4);
			ushort offsetRank = BitConverter.ToUInt16(data, 6);
			ushort offsetName = BitConverter.ToUInt16(data, 8);
			ushort offsetTitle = BitConverter.ToUInt16(data, 10);
			ushort offsetMaster = BitConverter.ToUInt16(data, 12);
			ushort offsetMotd = BitConverter.ToUInt16(data, 14);
			ushort offsetUnk = BitConverter.ToUInt16(data, 16);
			ushort offsetAd = BitConverter.ToUInt16(data, 18);

			var ranks = new List<GuildRank>(numRanks);
			while (offsetRank > 0) {
				ushort offsetRankName = BitConverter.ToUInt16(data, offsetRank + 4);
				ranks.Add(new GuildRank {
					Id = BitConverter.ToUInt32(data, offsetRank + 6),
					Permissions = BitConverter.ToUInt32(data, offsetRank + 10),
					Name = GetString(data, offsetRankName, 32)
				});
				offsetRank = BitConverter.ToUInt16(data, offsetRank + 2);
			}

			callback(new sGuildInfoArgs {
				name = GetString(data, offsetName, 32),
				title = GetString(data, offsetTitle, 32),
				master = GetString(data, offsetMaster, 32),
				motd = GetString(data, offsetMotd, 128),
				unk = GetString(data, offsetUnk, 32),
				ad = GetString(data, offsetAd, 64),
				ranks = ranks
			});
		}

		private void _sSelfStamina(byte[] data) { // 0xF471
			var callback = sSelfStamina;
			if (callback == null) return;

			callback(new sSelfStaminaArgs() {
				current = BitConverter.ToInt32(data, 4),
				max = BitConverter.ToInt32(data, 8),
				tier = BitConverter.ToInt16(data, 12)
			});
		}

		private void _sPlayerInfo(byte[] data) { // 0x53B2
			var callback = sPlayerInfo;
			if (callback == null) return;

			ushort nameOffset = BitConverter.ToUInt16(data, 4);
			ushort guildOffset = BitConverter.ToUInt16(data, 6);
			ushort guildRankOffset = BitConverter.ToUInt16(data, 8);
			// unk1Offset <- UInt16(..., 10)
			// unk1Length <- UInt16(..., 12) = [20 00]
			ushort guildTitleOffset = BitConverter.ToUInt16(data, 14);
			ushort guildEmblemOffset = BitConverter.ToUInt16(data, 16);
			// unk2Offset <- UInt16(..., 18)
			// unk2Length <- UInt16(..., 20)
			string name = GetString(data, nameOffset, NAME_CHAR_MAX_LENGTH);
			string guild = GetString(data, guildOffset, NAME_GUILD_MAX_LENGTH);
			string guildRank = GetString(data, guildRankOffset, NAME_GUILDRANK_MAX_LENGTH);
			string guildTitle = GetString(data, guildTitleOffset, NAME_GUILDRANK_MAX_LENGTH);
			string guildEmblem = GetString(data, guildEmblemOffset, 64);

			callback(new sPlayerInfoArgs {
				pid = BitConverter.ToUInt64(data, 22),
				uid = BitConverter.ToUInt64(data, 30),
				position = new Position {
					X = BitConverter.ToSingle(data, 38),
					Y = BitConverter.ToSingle(data, 42),
					Z = BitConverter.ToSingle(data, 46)
				},
				angle = BitConverter.ToInt16(data, 50),
				relation = BitConverter.ToInt32(data, 52),
				model = BitConverter.ToUInt32(data, 56),
				name = name,
				guild = guild,
				guildRank = guildRank,
				guildTitle = guildTitle,
				guildEmblem = guildEmblem
			});
		}

		private void _sAttackStart(byte[] data) { // 0xF22C
			var callback = sAttackStart;
			if (callback == null) return;

			callback(new sAttackStartArgs {
				source = BitConverter.ToUInt64(data, 8),
				position = new Position {
					X = BitConverter.ToSingle(data, 16),
					Y = BitConverter.ToSingle(data, 20),
					Z = BitConverter.ToSingle(data, 24)
				},
				angle = BitConverter.ToInt16(data, 28),
				model = BitConverter.ToUInt32(data, 30),
				skill = BitConverter.ToUInt32(data, 34),
				stage = BitConverter.ToUInt32(data, 38),
				speed = BitConverter.ToSingle(data, 42),
				id = BitConverter.ToUInt32(data, 46)
			});
		}

		private void _sAttackEnd(byte[] data) { // 0xB470
		}

		private void _sAttackResult(byte[] data) { // 0x94EF
			var callback = sAttackResult;
			if (callback == null) return;

			callback(new sAttackResultArgs {
				source = BitConverter.ToUInt64(data, 8),
				target = BitConverter.ToUInt64(data, 16),
				model = BitConverter.ToUInt32(data, 24),
				skill = BitConverter.ToUInt32(data, 28),
				stage = BitConverter.ToUInt32(data, 32),
				unk1 = BitConverter.ToInt32(data, 36),
				id = BitConverter.ToUInt32(data, 40),
				time = BitConverter.ToInt32(data, 44),
				damage = BitConverter.ToUInt32(data, 48),
				type1a = BitConverter.ToUInt16(data, 52),
				type1b = BitConverter.ToUInt16(data, 54),
				crit = BitConverter.ToUInt16(data, 56)
			});
		}

		private void _sUpdateHp(byte[] data) { // 0xE628
			var callback = sUpdateHp;
			if (callback == null) return;

			callback(new sUpdateHpArgs {
				current = BitConverter.ToUInt32(data, 4),
				max = BitConverter.ToUInt32(data, 8),
				diff = BitConverter.ToInt32(data, 12),
				type = BitConverter.ToUInt32(data, 16),
				target = BitConverter.ToUInt64(data, 20),
				source = BitConverter.ToUInt64(data, 28),
				crit = data[36]
			});
		}

		private void _sUpdateMp(byte[] data) { // 0xBAAB
			var callback = sUpdateMp;
			if (callback == null) return;

			callback(new sUpdateMpArgs {
				current = BitConverter.ToUInt32(data, 4),
				max = BitConverter.ToUInt32(data, 8),
				diff = BitConverter.ToInt32(data, 12),
				type = BitConverter.ToUInt32(data, 16),
				target = BitConverter.ToUInt64(data, 20),
				source = BitConverter.ToUInt64(data, 28)
			});
		}

		private void _sUpdateRe(byte[] data) { // 0xCE77
			var callback = sUpdateRe;
			if (callback == null) return;

			callback(new sUpdateReArgs {
				current = BitConverter.ToUInt32(data, 4),
				max = BitConverter.ToUInt32(data, 8),
				unk1 = BitConverter.ToInt32(data, 12),
				unk2 = BitConverter.ToInt32(data, 16),
				unk3 = BitConverter.ToInt32(data, 20)
			});
		}

		private void _sAbnormalAdd(byte[] data) { // 0x9CAE
			var callback = sAbnormalAdd;
			if (callback == null) return;

			callback(new sAbnormalAddArgs {
				target = BitConverter.ToUInt64(data, 4),
				source = BitConverter.ToUInt64(data, 12),
				abnormal = new Abnormal {
					Id = BitConverter.ToUInt32(data, 20),
					Duration = BitConverter.ToInt32(data, 24),
					Unk = BitConverter.ToInt32(data, 28),
					Stacks = BitConverter.ToInt32(data, 32)
				}
			});
		}

		private void _sAbnormalUpdate(byte[] data) { // 0x96D0
			var callback = sAbnormalUpdate;
			if (callback == null) return;

			callback(new sAbnormalUpdateArgs {
				target = BitConverter.ToUInt64(data, 4),
				abnormal = new Abnormal {
					Id = BitConverter.ToUInt32(data, 12),
					Duration = BitConverter.ToInt32(data, 16),
					Unk = BitConverter.ToInt32(data, 20),
					Stacks = BitConverter.ToInt32(data, 24)
				}
			});
		}

		private void _sAbnormalRemove(byte[] data) { // 0xC280
			var callback = sAbnormalRemove;
			if (callback == null) return;

			callback(new sAbnormalRemoveArgs {
				target = BitConverter.ToUInt64(data, 4),
				id = BitConverter.ToUInt32(data, 12)
			});
		}

		private void _sPartyAbnormalAdd(byte[] data) { // 0xA13E
			var callback = sPartyAbnormalAdd;
			if (callback == null) return;

			callback(new sPartyAbnormalAddArgs {
				target = BitConverter.ToUInt64(data, 4),
				abnormal = new Abnormal {
					Id = BitConverter.ToUInt32(data, 12),
					Duration = BitConverter.ToInt32(data, 16),
					Unk = BitConverter.ToInt32(data, 20),
					Stacks = BitConverter.ToInt32(data, 24)
				}
			});
		}

		private void _sPartyAbnormalUpdate(byte[] data) { // 0xD46C
			var callback = sPartyAbnormalUpdate;
			if (callback == null) return;

			callback(new sPartyAbnormalUpdateArgs {
				target = BitConverter.ToUInt64(data, 4),
				abnormal = new Abnormal {
					Id = BitConverter.ToUInt32(data, 12),
					Duration = BitConverter.ToInt32(data, 16),
					Unk = BitConverter.ToInt32(data, 20),
					Stacks = BitConverter.ToInt32(data, 24)
				}
			});
		}

		private void _sPartyAbnormalRemove(byte[] data) { // 0xB426
			var callback = sPartyAbnormalRemove;
			if (callback == null) return;

			callback(new sPartyAbnormalRemoveArgs {
				target = BitConverter.ToUInt64(data, 4),
				id = BitConverter.ToUInt32(data, 12)
			});
		}

		private void _sCombatStatus(byte[] data) { // 0xA3E2
			var callback = sCombatStatus;
			if (callback == null) return;

			callback(new sCombatStatusArgs {
				target = BitConverter.ToUInt64(data, 4),
				status = BitConverter.ToUInt32(data, 12) // 0 = out, 1 = in, 2 = rest
			});
		}

		private void _sLifeStatus(byte[] data) { // 0x9C09
			var callback = sLifeStatus;
			if (callback == null) return;

			callback(new sLifeStatusArgs {
				target = BitConverter.ToUInt64(data, 4),
				position = new Position {
					X = BitConverter.ToSingle(data, 12),
					Y = BitConverter.ToSingle(data, 16),
					Z = BitConverter.ToSingle(data, 20)
				},
				status = BitConverter.ToUInt16(data, 24),
				unk = data[26]
			});
		}

		private void _sAbsorbDamage(byte[] data) { // 0x8DD4
			var callback = sAbsorbDamage;
			if (callback == null) return;

			callback(new sAbsorbDamageArgs {
				target = BitConverter.ToUInt64(data, 4),
				amount = BitConverter.ToInt32(data, 12)
			});
		}

		private void _sConditionList(byte[] data) { // 0xB7F5
			var callback = sConditionList;
			if (callback == null) return;

			ushort count = BitConverter.ToUInt16(data, 4);
			ushort offset = BitConverter.ToUInt16(data, 6);

			var conditions = new List<Condition>(count);
			while (offset > 0) {
				conditions.Add(new Condition {
					Id = BitConverter.ToUInt32(data, offset + 4),
					Duration = BitConverter.ToInt32(data, offset + 8),
					Status = data[offset + 12]
				});
				offset = BitConverter.ToUInt16(data, offset + 2);
			}

			callback(new sConditionListArgs {
				target = BitConverter.ToUInt64(data, 8),
				conditions = conditions
			});
		}

		private void _sTargetInfo(byte[] data) { // 0x63B2
			var callback = sTargetInfo;
			if (callback == null) return;

			ushort numAbnormals = BitConverter.ToUInt16(data, 4);
			ushort offsetAbnormals = BitConverter.ToUInt16(data, 6);
			ushort numConditions = BitConverter.ToUInt16(data, 8);
			ushort offsetConditions = BitConverter.ToUInt16(data, 10);

			var abnormals = new List<Abnormal>(numAbnormals);
			var conditions = new List<Condition>(numConditions);

			while (offsetAbnormals > 0) {
				abnormals.Add(new Abnormal {
					Id = BitConverter.ToUInt32(data, offsetAbnormals + 4),
					Duration = BitConverter.ToInt32(data, offsetAbnormals + 8),
					Unk = BitConverter.ToInt32(data, offsetAbnormals + 12),
					Stacks = BitConverter.ToInt32(data, offsetAbnormals + 16)
				});
				offsetAbnormals = BitConverter.ToUInt16(data, offsetAbnormals + 2);
			}

			while (offsetConditions > 0) {
				conditions.Add(new Condition {
					Id = BitConverter.ToUInt32(data, offsetConditions + 4),
					Duration = BitConverter.ToInt32(data, offsetConditions + 8),
					Status = data[offsetConditions + 12]
				});
				offsetConditions = BitConverter.ToUInt16(data, offsetConditions + 2);
			}

			callback(new sTargetInfoArgs {
				target = BitConverter.ToUInt64(data, 12),
				hp = BitConverter.ToSingle(data, 20),
				level = BitConverter.ToUInt32(data, 28),
				// unk -> 4
				// unk -> 4
				edge = new Edge {
					d = BitConverter.ToInt32(data, 40),
					f = BitConverter.ToSingle(data, 44)
				},
				// unk -> 4 (always 40 1F 00 00 = 8,000?)
				abnormals = abnormals,
				conditions = conditions
			});
		}

		private void _sPartyAbnormalList(byte[] data) { // 0xE0FA
			var callback = sPartyAbnormalList;
			if (callback == null) return;

			ushort numAbnormals = BitConverter.ToUInt16(data, 4);
			ushort offsetAbnormals = BitConverter.ToUInt16(data, 6);
			ushort numConditions = BitConverter.ToUInt16(data, 8);
			ushort offsetConditions = BitConverter.ToUInt16(data, 10);

			var abnormals = new List<Abnormal>(numAbnormals);
			var conditions = new List<Condition>(numConditions);

			while (offsetAbnormals > 0) {
				abnormals.Add(new Abnormal {
					Id = BitConverter.ToUInt32(data, offsetAbnormals + 4),
					Duration = BitConverter.ToInt32(data, offsetAbnormals + 8),
					Unk = BitConverter.ToInt32(data, offsetAbnormals + 12),
					Stacks = BitConverter.ToInt32(data, offsetAbnormals + 16)
				});
				offsetAbnormals = BitConverter.ToUInt16(data, offsetAbnormals + 2);
			}

			while (offsetConditions > 0) {
				conditions.Add(new Condition {
					Id = BitConverter.ToUInt32(data, offsetConditions + 4),
					Duration = BitConverter.ToInt32(data, offsetConditions + 8),
					Status = data[offsetConditions + 12]
				});
				offsetConditions = BitConverter.ToUInt16(data, offsetConditions + 2);
			}

			callback(new sPartyAbnormalListArgs {
				target = BitConverter.ToUInt64(data, 12),
				abnormals = abnormals,
				conditions = conditions
			});
		}

		private void _sConditionAdd(byte[] data) { // 0xC24B
			var callback = sConditionAdd;
			if (callback == null) return;

			callback(new sConditionAddArgs {
				target = BitConverter.ToUInt64(data, 4),
				condition = new Condition {
					Id = BitConverter.ToUInt32(data, 12),
					Status = data[16],
					Duration = BitConverter.ToInt32(data, 17)
				}
			});
		}

		private void _sConditionActivate(byte[] data) { // 0x9EE7
			var callback = sConditionActivate;
			if (callback == null) return;

			callback(new sConditionActivateArgs {
				id = BitConverter.ToUInt32(data, 4)
			});
		}

		private void _sConditionRemove(byte[] data) { // 0x671E
			var callback = sConditionRemove;
			if (callback == null) return;

			callback(new sConditionRemoveArgs {
				id = BitConverter.ToUInt32(data, 4)
			});
		}

		private void _sPartyConditionAdd(byte[] data) { // 0x6827
			var callback = sPartyConditionAdd;
			if (callback == null) return;

			callback(new sPartyConditionAddArgs {
				target = BitConverter.ToUInt64(data, 4),
				condition = new Condition {
					Id = BitConverter.ToUInt32(data, 12),
					Status = data[16],
					Duration = BitConverter.ToInt32(data, 17)
				}
			});
		}

		private void _sPartyConditionActivate(byte[] data) { // 0x61CE
			var callback = sPartyConditionActivate;
			if (callback == null) return;

			callback(new sPartyConditionActivateArgs {
				target = BitConverter.ToUInt64(data, 4),
				id = BitConverter.ToUInt32(data, 12)
			});
		}

		private void _sPartyConditionRemove(byte[] data) { // 0xCEA4
			var callback = sPartyConditionRemove;
			if (callback == null) return;

			callback(new sPartyConditionRemoveArgs {
				target = BitConverter.ToUInt64(data, 4),
				id = BitConverter.ToUInt32(data, 12)
			});
		}

		private void _sProjectile(byte[] data) { // 0xEB10
			var callback = sProjectile;
			if (callback == null) return;

			callback(new sProjectileArgs {
				source = BitConverter.ToUInt64(data, 4),
				model = BitConverter.ToUInt32(data, 12),
				unk = BitConverter.ToInt32(data, 16),
				id = BitConverter.ToUInt64(data, 20),
				skill = BitConverter.ToUInt32(data, 28),
				startPosition = new Position {
					X = BitConverter.ToSingle(data, 32),
					Y = BitConverter.ToSingle(data, 36),
					Z = BitConverter.ToSingle(data, 40)
				},
				startAngle = BitConverter.ToInt16(data, 44),
				targetPosition = new Position {
					X = BitConverter.ToSingle(data, 46),
					Y = BitConverter.ToSingle(data, 50),
					Z = BitConverter.ToSingle(data, 54)
				},
				targetAngle = BitConverter.ToInt16(data, 58)
			});
		}

		private void _sProjectileRemove(byte[] data) { // 0x7C3B
			var callback = sProjectileRemove;
			if (callback == null) return;

			callback(new sProjectileRemoveArgs {
				id = BitConverter.ToUInt64(data, 4),
				unk = data[12]
			});
		}

		private void _sProjectedAttack(byte[] data) { // 0xC513
			var callback = sProjectedAttack;
			if (callback == null) return;

			callback(new sProjectedAttackArgs {
				id = BitConverter.ToUInt64(data, 4),
				// (unk?) <- 4 bytes
				startPosition = new Position {
					X = BitConverter.ToSingle(data, 16),
					Y = BitConverter.ToSingle(data, 20),
					Z = BitConverter.ToSingle(data, 24)
				},
				startAngle = BitConverter.ToInt16(data, 28),
				targetPosition = new Position {
					X = BitConverter.ToSingle(data, 30),
					Y = BitConverter.ToSingle(data, 34),
					Z = BitConverter.ToSingle(data, 38)
				},
				targetAngle = BitConverter.ToInt16(data, 42),
				// (unk?) <- 4+1 bytes
				source = BitConverter.ToUInt64(data, 49),
				model = BitConverter.ToUInt32(data, 57)
			});
		}

		private void _sProjectedAttackRemove(byte[] data) { // 0xD65E
			var callback = sProjectedAttackRemove;
			if (callback == null) return;

			callback(new sProjectedAttackRemoveArgs {
				id = BitConverter.ToUInt64(data, 4),
				unk = data[12]
			});
		}

		private void _sPartyList(byte[] data) { // 0xC50B
			var callback = sPartyList;
			if (callback == null) return;

			ushort count = BitConverter.ToUInt16(data, 4);
			ushort offset = BitConverter.ToUInt16(data, 6);
			// IMS? <- 1 byte
			//byte type = data[9];
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			//ulong leader = BitConverter.ToUInt64(data, 22);
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 2 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 1 byte

			var members = new List<PartyMember>(count);

			while (offset > 0) {
				members.Add(new PartyMember {
					pID = BitConverter.ToUInt64(data, offset + 6),
					level = BitConverter.ToInt32(data, offset + 14),
					job = BitConverter.ToInt32(data, offset + 18),
					status = data[offset + 22],
					cID = BitConverter.ToUInt64(data, offset + 23),
					partyPosition = BitConverter.ToInt32(data, offset + 31),
					canInvite = data[offset + 35],
					name = GetString(data, offset + 36, NAME_CHAR_MAX_LENGTH)
				});
				offset = BitConverter.ToUInt16(data, offset + 2);
			}

			callback(new sPartyListArgs {
				type = data[9],
				leader = BitConverter.ToUInt64(data, 22),
				members = members
			});
		}

		/*
		private void ParsePartyMemberMove(byte[] data) { // 0x8568
			ulong cUID = BitConverter.ToUInt64(data, 4);
			int pos = BitConverter.ToInt32(data, 12);
			Console.WriteLine("~~ {0} moved to position {1}", GetPlayerName(cUID), pos);
		}
		 */

		private void _sPartyDeath(byte[] data) { // 0xAE97
			var callback = sPartyDeath;
			if (callback == null) return;

			callback(new sPartyDeathArgs {
				target = BitConverter.ToUInt64(data, 4)
			});
		}

		private void _sPartyUpdate(byte[] data) { // 0x7695
			var callback = sPartyUpdate;
			if (callback == null) return;

			callback(new sPartyUpdateArgs {
				target = BitConverter.ToUInt64(data, 4),
				currentHp = BitConverter.ToInt32(data, 12),
				currentMp = BitConverter.ToInt32(data, 16),
				maxHp = BitConverter.ToInt32(data, 20),
				maxMp = BitConverter.ToInt32(data, 24),
				level = BitConverter.ToInt16(data, 28),
				inCombat = BitConverter.ToInt16(data, 30),
				vitality = BitConverter.ToInt16(data, 32),
				status = data[34],
				stamina = BitConverter.ToInt32(data, 35),
				currentRe = BitConverter.ToInt32(data, 39),
				maxRe = BitConverter.ToInt32(data, 43),
				unk = BitConverter.ToInt32(data, 47)
			});
		}

		private void _sPartyUpdateHp(byte[] data) { // 0xCCD0
			var callback = sPartyUpdateHp;
			if (callback == null) return;

			callback(new sPartyUpdateHpArgs {
				target = BitConverter.ToUInt64(data, 4),
				current = BitConverter.ToInt32(data, 12),
				max = BitConverter.ToInt32(data, 16)
			});
		}

		private void _sPartyUpdateMp(byte[] data) { // 0x7DAF
			var callback = sPartyUpdateMp;
			if (callback == null) return;

			callback(new sPartyUpdateMpArgs {
				target = BitConverter.ToUInt64(data, 4),
				current = BitConverter.ToInt32(data, 12),
				max = BitConverter.ToInt32(data, 16)
			});
		}

		private void _sPartyUpdateRe(byte[] data) { // 0xBB69
			var callback = sPartyUpdateRe;
			if (callback == null) return;

			callback(new sPartyUpdateReArgs {
				target = BitConverter.ToUInt64(data, 4),
				current = BitConverter.ToInt32(data, 12),
				max = BitConverter.ToInt32(data, 16),
				unk = BitConverter.ToInt32(data, 20)
			});
		}

		private void _sPartyInvitePriv(byte[] data) { // 0xF5A3
			var callback = sPartyInvitePriv;
			if (callback == null) return;

			callback(new sPartyInvitePrivArgs {
				target = BitConverter.ToUInt64(data, 4),
				canInvite = data[12]
			});
		}

		private void _sPartyLeader(byte[] data) { // 0xBFCE
			var callback = sPartyLeader;
			if (callback == null) return;

			ushort offset = BitConverter.ToUInt16(data, 4);
			callback(new sPartyLeaderArgs {
				target = BitConverter.ToUInt64(data, 6),
				name = GetString(data, offset, 32)
			});
		}

		private void _sNpcHp(byte[] data) { // 0xD1F5
			var callback = sNpcHp;
			if (callback == null) return;

			callback(new sNpcHpArgs {
				target = BitConverter.ToUInt64(data, 4),
				hp = BitConverter.ToSingle(data, 12),
				unk1 = data[16],
				edge = new Edge {
					d = BitConverter.ToInt32(data, 17),
					f = BitConverter.ToSingle(data, 21)
				}
				// unk2 <- 4 (14 00 00 00 = 8,000?)
				// unk3 <- 4 (03 00 00 00)
			});
		}

		private void _sNpcStatus(byte[] data) { // 0xA5EB
			var callback = sNpcStatus;
			if (callback == null) return;

			callback(new sNpcStatusArgs {
				creature = BitConverter.ToUInt64(data, 4),
				enraged = data[12],
				unk1 = BitConverter.ToInt32(data, 13), // (HP / 20)?
				target = BitConverter.ToUInt64(data, 17),
				unk2 = BitConverter.ToInt32(data, 25)
			});
		}

		private void _sNpcEmotion(byte[] data) { // 0x900F
			var callback = sNpcEmotion;
			if (callback == null) return;

			callback(new sNpcEmotionArgs {
				target = BitConverter.ToUInt64(data, 4),
				creature = BitConverter.ToUInt64(data, 12),
				emotion = BitConverter.ToInt32(data, 20)
			});
		}

		private void ParseNpcInfo(byte[] data) { // 0xA7B4
			// (unk?) <- UInt32(..., 4)
			// shift <- UInt16(..., 8)
			ulong uID = BitConverter.ToUInt64(data, 10);
			// target <- UInt64(data, 18);
			// pos.x <- Single(..., 26)
			// pos.y <- Single(..., 30)
			// pos.z <- Single(..., 34)
			// pos.w <- Int16(..., 38)
			// (unk?) <- 4 bytes, usually 0x0000000C
			uint npcID = BitConverter.ToUInt32(data, 42);
			ushort npcType = BitConverter.ToUInt16(data, 46);
			uint npcModel = BitConverter.ToUInt32(data, 48);
			// (unk?) <- ...
			ulong owner = BitConverter.ToUInt64(data, 85);
			// (unk?) <- ...
		}

		private void _sNpcCombatStatus(byte[] data) { // 0xE1B2
			var callback = sNpcCombatStatus;
			if (callback == null) return;

			callback(new sNpcCombatStatusArgs {
				target = BitConverter.ToUInt64(data, 4),
				status = data[12]
			});
		}

		/*
		private void ParseMinionDeath(byte[] data) { // 0xE570
			ulong uid = BitConverter.ToUInt64(data, 4);
			// pos.x <- Single(..., 12)
			// pos.y <- Single(..., 16)
			// pos.z <- Single(..., 20)
			// (unk?) <- UInt32(..., 24)
			// (unk?) <- UInt32(..., 28)
		}
		 * */

		private void _sChatMessage(byte[] data) { // 0xAD3D
			var callback = sChatMessage;
			if (callback == null) return;

			ushort senderOffset = BitConverter.ToUInt16(data, 4);
			ushort messageOffset = BitConverter.ToUInt16(data, 6);

			callback(new sChatMessageArgs {
				type = BitConverter.ToUInt32(data, 8),
				authorId = BitConverter.ToUInt64(data, 12),
				unk1 = data[20],
				gm = data[21],
				unk2 = data[22], // own message?
				authorName = GetString(data, senderOffset, NAME_CHAR_MAX_LENGTH),
				message = GetString(data, messageOffset, 1023)
			});
		}

		private void _sImage(byte[] data) { // 0x530A
			var callback = sImage;
			if (callback == null) return;

			ushort nameOffset = BitConverter.ToUInt16(data, 4);
			ushort dataOffset = BitConverter.ToUInt16(data, 6);
			ushort dataLength = BitConverter.ToUInt16(data, 8);

			var imageData = new byte[dataLength];
			Array.Copy(data, dataOffset, imageData, 0, dataLength);

			callback(new sImageArgs {
				name = GetString(data, nameOffset, 64),
				data = imageData
			});
		}

		private void _sLockon(byte[] data) { // 0x7D18
			var callback = sLockon;
			if (callback == null) return;

			callback(new sLockonArgs {
				target = BitConverter.ToUInt64(data, 4),
				attack = BitConverter.ToUInt32(data, 12),
				result = data[16]
			});
		}

		private void CParseYield(byte[] data) { // 0xF215
			//WriteEvent("cYield", "");
		}

		private void CParseTargetSelect(byte[] data) { // 0x685D
			ulong target = BitConverter.ToUInt64(data, 4);
		}

		private void CParseLockon(byte[] data) { // 0xEA79
			ulong target = BitConverter.ToUInt64(data, 4);
			uint attack = BitConverter.ToUInt32(data, 12);
		}
	}
}
