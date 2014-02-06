using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;

using TeraWatcherAPI.Events;

namespace TeraPacketEncryption {
	class Parse : TeraWatcherAPI.IHandler {
		const int NAME_CHAR_MAX_LENGTH = 20;
		const int NAME_GUILD_MAX_LENGTH = 32;
		const int DEBUG_LEVEL = 1;

		private delegate void Handle(byte[] data);

		public event sPlayerInfoHandler sPlayerInfo;
		public event sSelfStaminaHandler sSelfStamina;

		public Parse() {
		}

		public void Log(int level, string format, params object[] args) {
			Console.WriteLine(format, args);
		}

		public void ServerCommand(uint opCode, byte[] data) {
			Handle p_ = null;
			
			switch (opCode) {
				case 0xD6FA: ParseSelfInfo(data); break;
				case 0xF2DB: ParseSelfGuild(data); break;
				case 0xF471: p_ += _sSelfStamina; break;
				case 0x53B2: _sPlayerInfo(data); break;
				case 0xF22C: ParseAttackStart(data); break;
				//case 0xB470: ParseAttackEnd(data); break;
				case 0x94EF: ParseAttackResult(data); break;
				case 0xE628: ParseUpdateHP(data); break;
				case 0xBAAB: ParseUpdateMP(data); break;
				case 0xCE77: ParseUpdateRE(data); break;
				case 0x9CAE: ParseStatusEffectAdd(data); break;
				case 0x96D0: ParseStatusEffectUpdate(data); break;
				case 0xC280: ParseStatusEffectRemove(data); break;
				case 0xA3E2: ParseCombatStatus(data); break;
				case 0x9C09: ParseAliveStatus(data); break;
				case 0x8DD4: ParseAbsorbDamage(data); break;
				case 0xB7F5: ParseBuffList(data); break;
				case 0x63B2: ParseTargetInfo(data); break;
				case 0xC24B: ParseBuffAdd(data); break;
				case 0x9EE7: ParseBuffActivate(data); break;
				case 0x671E: ParseBuffRemove(data); break;
				case 0xEB10: ParseProjectile(data); break;
				case 0x7C3B: ParseProjectileRemove(data); break;
				case 0xC513: ParsePeriodicAoe(data); break;
				case 0xD65E: ParsePeriodicAoeRemove(data); break;
				case 0xC50B: ParsePartyList(data); break;
				case 0xE0FA: ParsePartyMemberAbnormals(data); break;
				case 0x6827: ParsePartyMemberBuffAdd(data); break;
				case 0x61CE: ParsePartyMemberBuffActivate(data); break;
				case 0xCEA4: ParsePartyMemberBuffRemove(data); break;
				case 0xAE97: ParsePartyMemberDeath(data); break;
				case 0x7695: ParsePartyMemberStats(data); break;
				case 0xF5A3: ParsePartyMemberCanInvite(data); break;
				case 0xBFCE: ParsePartyLeaderChange(data); break;
				case 0xCCD0: ParsePartyMemberUpdateHP(data); break;
				case 0x7DAF: ParsePartyMemberUpdateMP(data); break;
				case 0xBB69: ParsePartyMemberUpdateRE(data); break;
				case 0xA13E: ParsePartyStatusEffectAdd(data); break;
				case 0xD46C: ParsePartyStatusEffectUpdate(data); break;
				case 0xB426: ParsePartyStatusEffectRemove(data); break;
				case 0xD1F5: ParseOtherHP(data); break;
				case 0xA5EB: ParseNpcStatus(data); break;
				case 0x900F: ParseNpcEmotion(data); break;
				case 0xA7B4: ParseNpcInfo(data); break;
				case 0xE1B2: ParseNpcCombatStatus(data); break;
				case 0xAD3D: ParseChatMessage(data); break;
				case 0x530A: ParseImageData(data); break;
				case 0x7D18: ParseLockonResult(data); break;
				//case 0x8568: ParsePartyMemberMove(data); break;
				//case 0xE570: ParseMinionDeath(data); break;
				default: break;
			}

			if (p_ != null) p_(data);
		}

		public void ClientCommand(uint opCode, byte[] data) {
			switch (opCode) {
				case 0xF215: CParseYield(data); break;
				case 0x685D: CParseTargetSelect(data); break;
				case 0xEA79: CParseLockon(data); break;
			}
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

		private void WriteEvent(string a, string b) {}
		private string GetPlayerName(ulong a) { return ""; }
		private string GetJson(string a) { return ""; }



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

		private void ParseSelfGuild(byte[] data) { // 0xF2DB
			/*
			Player self;

			string name = GetString(data, 107, NAME_GUILD_MAX_LENGTH);
			WriteEvent("sSelfGuild", "\"name\": " + GetJson(name));

			if (Self == 0 || !Players.TryGetValue(Self, out self)) return; // ?
			self.Guild = name;
			 * */
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
			// guildRankOffset = UInt16(..., 8)
			// unk1Offset <- UInt16(..., 10)
			// unk1Length <- UInt16(..., 12) = [20 00]
			// guildTitleOffset <- UInt16(..., 14)
			// guildEmblemOffset <- UInt16(..., 16)
			// unk2Offset <- UInt16(..., 18)
			// unk2Length <- UInt16(..., 20)
			ulong id1 = BitConverter.ToUInt64(data, 22);
			ulong id2 = BitConverter.ToUInt64(data, 30);
			// posX <- Single(..., 38)
			// posY <- Single(..., 42)
			// posZ <- Single(..., 46)
			// angle <- UInt16(..., 50)
			// relation <- UInt32(..., 52)
			uint model = BitConverter.ToUInt32(data, 56);
			string name = GetString(data, nameOffset, NAME_CHAR_MAX_LENGTH);
			string guild = GetString(data, guildOffset, NAME_GUILD_MAX_LENGTH);

			callback(new sPlayerInfoArgs() {
				uid = id2,
				pid = id1
			});
		}

		private void ParseAttackStart(byte[] data) { // 0xF22C
			ulong source = BitConverter.ToUInt64(data, 8);
			// pos.x <- Single(..., 16)
			// pos.y <- Single(..., 20)
			// pos.z <- Single(..., 24)
			// pos.w <- Int16(..., 28)
			uint model = BitConverter.ToUInt32(data, 30);
			uint skill = BitConverter.ToUInt32(data, 34);
			uint stage = BitConverter.ToUInt32(data, 38);
			float speed = BitConverter.ToSingle(data, 42);
			uint id = BitConverter.ToUInt32(data, 46);

			Console.WriteLine("~~ {0} began skill: {1}.{2}", GetPlayerName(source), model, skill & 0x03FFFFFF);

			WriteEvent("sAttackStart", String.Format("\"source\": {0}, \"model\": {1}, \"skill\": {2}, \"stage\": {3}, \"speed\": {4}, \"id\": {5}",
				source,
				model,
				skill,
				stage,
				speed,
				id));
		}

		private void ParseAttackEnd(byte[] data) { // 0xB470
		}

		private void ParseAttackResult(byte[] data) { // 0x94EF
			ulong p1 = BitConverter.ToUInt64(data, 8);
			ulong p2 = BitConverter.ToUInt64(data, 16);
			uint model = BitConverter.ToUInt32(data, 24);
			uint skill = BitConverter.ToUInt32(data, 28);
			uint stage = BitConverter.ToUInt32(data, 32);
			int unk1 = BitConverter.ToInt32(data, 36);
			uint id = BitConverter.ToUInt32(data, 40);
			int time = BitConverter.ToInt32(data, 44);
			uint damage = BitConverter.ToUInt32(data, 48);
			ushort type1a = BitConverter.ToUInt16(data, 52);
			ushort type1b = BitConverter.ToUInt16(data, 54);
			ushort crit = BitConverter.ToUInt16(data, 56);
			Console.WriteLine("~~ {3} -> {4}, skill: {8}.{2}.{6}, amount: {0}, type: {1} / {5}, time: {7}, unk: {9}", damage, type1a, skill - 0x4000000, GetPlayerName(p1), GetPlayerName(p2), crit, stage, time, model, unk1);
			//DebugData(data, 60);

			WriteEvent("sAttackResult", String.Format(
				"\"source\": {0}, \"target\": {1}, \"model\": {2}, \"skill\": {3}, \"stage\": {4}, \"id\": {5}, \"amount\": {6}, \"type\": {7}, \"type2\": {8}, \"crit\": {9}",
				p1,
				p2,
				model,
				skill,
				stage,
				id,
				damage,
				type1a,
				type1b,
				crit));
		}

		private void ParseUpdateHP(byte[] data) { // 0xE628
			uint hp = BitConverter.ToUInt32(data, 4);
			uint maxHp = BitConverter.ToUInt32(data, 8);
			int diff = BitConverter.ToInt32(data, 12);
			uint type = BitConverter.ToUInt32(data, 16);
			ulong target = BitConverter.ToUInt64(data, 20);
			ulong source = BitConverter.ToUInt64(data, 28);
			byte crit = data[36];
			//Console.WriteLine("~~ {0}, hp: {1} / {2} (diff {3}), from: {4}, crit: {5}, (type: {6})", GetPlayerName(target), hp, maxHp, diff, GetPlayerName(source), crit, type);

			WriteEvent("sUpdateHP", String.Format("\"current\": {0}, \"max\": {1}, \"change\": {2}, \"type\": {3}, \"target\": {4}, \"source\": {5}, \"type2\": {6}",
				hp,
				maxHp,
				diff,
				type,
				target,
				source,
				crit));
		}

		private void ParseUpdateMP(byte[] data) { // 0xBAAB
			int curMP = BitConverter.ToInt32(data, 4);
			int maxMP = BitConverter.ToInt32(data, 8);
			int diff = BitConverter.ToInt32(data, 12);
			int type = BitConverter.ToInt32(data, 16);
			ulong target = BitConverter.ToUInt64(data, 20);
			ulong source = BitConverter.ToUInt64(data, 28);
			//Console.WriteLine("~~ {0}, mp: {1} / {2} (diff {3}), from: {4}, (type: {5})", GetPlayerName(cUID), curMP, maxMP, diff, GetPlayerName(from), type);

			WriteEvent("sUpdateMP", String.Format("\"current\": {0}, \"max\": {1}, \"change\": {2}, \"type\": {3}, \"target\": {4}, \"source\": {5}",
				curMP,
				maxMP,
				diff,
				type,
				target,
				source));
		}

		private void ParseUpdateRE(byte[] data) { // 0xCE77
			int curRE = BitConverter.ToInt32(data, 4);
			int maxRE = BitConverter.ToInt32(data, 8);
			// unk1 <- Int32(..., 12)
			// unk2 <- Int32(..., 16)
			// unk3 <- Int32(..., 20)

			WriteEvent("sUpdateRE", String.Format("\"current\": {0}, \"max\": {1}",
				curRE,
				maxRE));
		}

		private void ParseStatusEffectAdd(byte[] data) { // 0x9CAE
			ulong tgt = BitConverter.ToUInt64(data, 4);
			ulong src = BitConverter.ToUInt64(data, 12);
			uint id = BitConverter.ToUInt32(data, 20);
			int length = BitConverter.ToInt32(data, 24);
			int unk = BitConverter.ToInt32(data, 28);
			int count = BitConverter.ToInt32(data, 32);
			Console.WriteLine("~~ {0} -> {1}, status: {2}, length: {3}, stacks: {5}, unk: {4}", GetPlayerName(src), GetPlayerName(tgt), id, length, unk, count);

			WriteEvent("sAbnormalAdd", String.Format("\"target\": {0}, \"source\": {1}, \"id\": {2}, \"duration\": {3}, \"count\": {4}",
				tgt,
				src,
				id,
				length,
				count));
		}

		private void ParseStatusEffectUpdate(byte[] data) { // 0x96D0
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			uint length = BitConverter.ToUInt32(data, 16);
			int unk = BitConverter.ToInt32(data, 20);
			uint count = BitConverter.ToUInt32(data, 24);
			Console.WriteLine("~~ {0}, status: {1}, length: {2}, stacks: {4}, unk: {3}", GetPlayerName(tgt), id, length, unk, count);

			WriteEvent("sAbnormalUpdate", String.Format("\"target\": {0}, \"id\": {1}, \"duration\": {2}, \"count\": {3}",
				tgt,
				id,
				length,
				count));
		}

		private void ParseStatusEffectRemove(byte[] data) { // 0xC280
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			Console.WriteLine("~~ {0} lost status {1}", GetPlayerName(tgt), id);

			WriteEvent("sAbnormalRemove", String.Format("\"target\": {0}, \"id\": {1}",
				tgt,
				id));
		}

		private void ParsePartyStatusEffectAdd(byte[] data) { // 0xA13E
			ulong pUID = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			int length = BitConverter.ToInt32(data, 16);
			int unk = BitConverter.ToInt32(data, 20);
			int count = BitConverter.ToInt32(data, 24);
			Console.WriteLine("~~ party member {0} gained status: {1}, length: {2}, stacks: {4}, unk: {3}", pUID, id, length, unk, count);

			WriteEvent("sPartyAbnormalAdd", String.Format("\"target\": {0}, \"id\": {1}, \"duration\": {2}, \"count\": {3}",
				pUID,
				id,
				length,
				count));
		}

		private void ParsePartyStatusEffectUpdate(byte[] data) { // 0xD46C
			ulong pUID = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			uint length = BitConverter.ToUInt32(data, 16);
			int unk = BitConverter.ToInt32(data, 20);
			uint count = BitConverter.ToUInt32(data, 24);
			Console.WriteLine("~~ party member {0}, status: {1}, length: {2}, stacks: {4}, unk: {3}", pUID, id, length, unk, count);

			WriteEvent("sPartyAbnormalUpdate", String.Format("\"target\": {0}, \"id\": {1}, \"duration\": {2}, \"count\": {3}",
				pUID,
				id,
				length,
				count));
		}

		private void ParsePartyStatusEffectRemove(byte[] data) { // 0xB426
			ulong pUID = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			Console.WriteLine("~~ party member {0} lost status {1}", pUID, id);

			WriteEvent("sPartyAbnormalRemove", String.Format("\"target\": {0}, \"id\": {1}",
				pUID,
				id));
		}

		private void ParseCombatStatus(byte[] data) { // 0xA3E2
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint status = BitConverter.ToUInt32(data, 12); // 0 = out, 1 = in, 2 = rest

			WriteEvent("sCombatStatus", String.Format("\"target\": {0}, \"status\": {1}",
				tgt,
				status));
		}

		private void ParseAliveStatus(byte[] data) { // 0x9C09
			ulong tgt = BitConverter.ToUInt64(data, 4);
			// pos.x <- Single(..., 12)
			// pos.y <- Single(..., 16)
			// pos.z <- Single(..., 20)
			ushort status = BitConverter.ToUInt16(data, 24);
			// (unk?) <- data[26]
			Console.WriteLine("~~ {0}, alive: {1}", GetPlayerName(tgt), status);

			WriteEvent("sLifeStatus", String.Format("\"target\": {0}, \"status\": {1}",
				tgt,
				status));
		}

		private void ParseAbsorbDamage(byte[] data) { // 0x8DD4
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint amt = BitConverter.ToUInt32(data, 12);

			WriteEvent("sAbsorbDamage", String.Format("\"target\": {0}, \"amount\": {1}",
				tgt,
				amt));
		}

		private void ParseBuffList(byte[] data) { // 0xB7F5
			ushort num = BitConverter.ToUInt16(data, 4);
			ushort offset = BitConverter.ToUInt16(data, 6);
			ulong tgt = BitConverter.ToUInt64(data, 8);

			string effects = "";

			while (offset > 0) {
				ushort next = BitConverter.ToUInt16(data, offset + 2);
				uint id = BitConverter.ToUInt32(data, offset + 4);
				uint length = BitConverter.ToUInt32(data, offset + 8);
				byte status = data[offset + 12];

				if (effects != "") effects += ", ";
				effects += String.Format("{{ \"id\": {0}, \"duration\": {1}, \"status\": {2} }}",
					id,
					length,
					status);

				offset = next;
			}

			WriteEvent("sStatusList", String.Format("\"target\": {0}, \"effects\": [ {1} ]",
				tgt,
				effects));
		}

		private void ParseTargetInfo(byte[] data) { // 0x63B2
			ushort num1 = BitConverter.ToUInt16(data, 4);
			ushort offset1 = BitConverter.ToUInt16(data, 6);
			ushort num2 = BitConverter.ToUInt16(data, 8);
			ushort offset2 = BitConverter.ToUInt16(data, 10);

			ulong tgt = BitConverter.ToUInt64(data, 12);
			float hp = BitConverter.ToSingle(data, 20);
			// unk -> 4
			uint level = BitConverter.ToUInt32(data, 28);
			// unk -> 4
			// unk -> 4
			uint edge_d = BitConverter.ToUInt32(data, 40);
			float edge_f = BitConverter.ToSingle(data, 44);
			// unk -> 4 (always 40 1F 00 00 = 8,000?)

			string effects = "";
			string statuses = "";

			while (offset1 > 0) {
				ushort next = BitConverter.ToUInt16(data, offset1 + 2);
				uint id = BitConverter.ToUInt32(data, offset1 + 4);
				uint length = BitConverter.ToUInt32(data, offset1 + 8);
				int unk = BitConverter.ToInt32(data, offset1 + 12);
				uint count = BitConverter.ToUInt32(data, offset1 + 16);

				if (effects != "") effects += ", ";
				effects += String.Format("{{ \"id\": {0}, \"duration\": {1}, \"count\": {2} }}",
					id,
					length,
					count);

				offset1 = next;
			}

			while (offset2 > 0) {
				ushort next = BitConverter.ToUInt16(data, offset2 + 2);
				uint id = BitConverter.ToUInt32(data, offset2 + 4);
				uint length = BitConverter.ToUInt32(data, offset2 + 8);
				byte status = data[offset2 + 12];

				if (statuses != "") statuses += ", ";
				statuses += String.Format("{{ \"id\": {0}, \"duration\": {1}, \"status\": {2} }}",
					id,
					length,
					status);

				offset2 = next;
			}

			WriteEvent("sTargetInfo", String.Format("\"target\": {0}, \"hp\": {1}, \"edge\": {{ \"d\": {2}, \"f\": {3} }}, \"abnormals\": [ {4} ], \"effects\": [ {5} ]",
				tgt,
				hp,
				edge_d,
				edge_f,
				effects,
				statuses));
		}

		private void ParsePartyMemberAbnormals(byte[] data) { // 0xE0FA
			ushort num1 = BitConverter.ToUInt16(data, 4);
			ushort offset1 = BitConverter.ToUInt16(data, 6);
			ushort num2 = BitConverter.ToUInt16(data, 8);
			ushort offset2 = BitConverter.ToUInt16(data, 10);

			ulong tgt = BitConverter.ToUInt64(data, 12);

			string effects = "";
			string statuses = "";

			while (offset1 > 0) {
				ushort next = BitConverter.ToUInt16(data, offset1 + 2);
				uint id = BitConverter.ToUInt32(data, offset1 + 4);
				uint length = BitConverter.ToUInt32(data, offset1 + 8);
				int unk = BitConverter.ToInt32(data, offset1 + 12);
				uint count = BitConverter.ToUInt32(data, offset1 + 16);

				if (effects != "") effects += ", ";
				effects += String.Format("{{ \"id\": {0}, \"duration\": {1}, \"count\": {2} }}",
					id,
					length,
					count);

				offset1 = next;
			}

			while (offset2 > 0) {
				ushort next = BitConverter.ToUInt16(data, offset2 + 2);
				uint id = BitConverter.ToUInt32(data, offset2 + 4);
				uint length = BitConverter.ToUInt32(data, offset2 + 8);
				byte status = data[offset2 + 12];

				if (statuses != "") statuses += ", ";
				statuses += String.Format("{{ \"id\": {0}, \"duration\": {1}, \"status\": {2} }}",
					id,
					length,
					status);

				offset2 = next;
			}

			WriteEvent("sPartyAbnormalList", String.Format("\"target\": {0}, \"abnormals\": [ {1} ], \"effects\": [ {2} ]",
				tgt,
				effects,
				statuses));
		}

		private void ParseBuffAdd(byte[] data) { // 0xC24B
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			byte status = data[16];
			uint length = BitConverter.ToUInt32(data, 17);
			Console.WriteLine("~~ {0} got buff: {1}, length: {2}, status: {3}", GetPlayerName(tgt), id, length, status);

			WriteEvent("sStatusAdd", String.Format("\"target\": {0}, \"id\": {1}, \"status\": {2}, \"duration\": {3}",
				tgt,
				id,
				status,
				length));
		}

		private void ParseBuffActivate(byte[] data) { // 0x9EE7
			uint id = BitConverter.ToUInt32(data, 4);

			WriteEvent("sSelfStatusActivate", String.Format("\"id\": {0}",
				id));
		}

		private void ParseBuffRemove(byte[] data) { // 0x671E
			uint id = BitConverter.ToUInt32(data, 4);

			WriteEvent("sSelfBuffRemove", String.Format("\"id\": {0}",
				id));
		}

		private void ParsePartyMemberBuffAdd(byte[] data) { // 0x6827
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);
			byte status = data[16];
			uint length = BitConverter.ToUInt32(data, 17);
			Console.WriteLine("~~ party member {0} got buff: {1}, length: {2}, status: {3}", tgt, id, length, status);

			WriteEvent("sPartyStatusAdd", String.Format("\"target\": {0}, \"id\": {1}, \"status\": {2}, \"duration\": {3}",
				tgt,
				id,
				status,
				length));
		}

		private void ParsePartyMemberBuffActivate(byte[] data) { // 0x61CE
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);

			WriteEvent("sPartyStatusActivate", String.Format("\"target\": {0}, \"id\": {1}",
				tgt,
				id));
		}

		private void ParsePartyMemberBuffRemove(byte[] data) { // 0xCEA4
			ulong tgt = BitConverter.ToUInt64(data, 4);
			uint id = BitConverter.ToUInt32(data, 12);

			WriteEvent("sPartyStatusRemove", String.Format("\"target\": {0}, \"id\": {1}",
				tgt,
				id));
		}

		private void ParseProjectile(byte[] data) { // 0xEB10
			ulong source = BitConverter.ToUInt64(data, 4);
			uint model = BitConverter.ToUInt32(data, 12);
			// (unk?) <- 4 bytes
			ulong id = BitConverter.ToUInt64(data, 20);
			uint skill = BitConverter.ToUInt32(data, 28);
			// x1 <- single
			// y1 <- single
			// z1 <- single
			// w1 <- short
			// x2 <- single
			// y2 <- single
			// z2 <- single
			// w2 <- short

			WriteEvent("sProjectileSpawn", String.Format("\"source\": {0}, \"model\": {1}, \"id\": {2}, \"skill\": {3}",
				source,
				model,
				id,
				skill));
		}

		private void ParseProjectileRemove(byte[] data) { // 0x7C3B
			ulong id = BitConverter.ToUInt64(data, 4);
			// (unk?) <- 1 byte (0x01)

			WriteEvent("sProjectileRemove", String.Format("\"id\": {0}",
				id));
		}

		private void ParsePeriodicAoe(byte[] data) { // 0xC513
			ulong id = BitConverter.ToUInt64(data, 4);
			// (unk?) <- 4 bytes
			// x1 <- single
			// y1 <- single
			// z1 <- single
			// w1 <- short
			// x2 <- single
			// y2 <- single
			// z2 <- single
			// w2 <- short
			// (unk?) <- 4+1 bytes
			ulong source = BitConverter.ToUInt64(data, 49);
			uint model = BitConverter.ToUInt32(data, 57);

			WriteEvent("sPeriodicAoeSpawn", String.Format("\"id\": {0}, \"source\": {1}, \"model\": {2}",
				id,
				source,
				model));
		}

		private void ParsePeriodicAoeRemove(byte[] data) { // 0xD65E
			ulong id = BitConverter.ToUInt64(data, 4);
			// (unk?) <- 1 byte (0x00)

			WriteEvent("sPeriodicAoeRemove", String.Format("\"id\": {0}",
				id));
		}

		private void ParsePartyList(byte[] data) { // 0xC50B
			ushort count = BitConverter.ToUInt16(data, 4);
			ushort offset = BitConverter.ToUInt16(data, 6);
			// IMS? <- 1 byte
			byte type = data[9];
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			ulong leader = BitConverter.ToUInt64(data, 22);
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 2 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 4 bytes
			// (unk?) <- 1 byte

			string list = "";

			while (offset > 0) {
				ushort next = BitConverter.ToUInt16(data, offset + 2);
				ulong pUID = BitConverter.ToUInt64(data, offset + 6);
				int level = BitConverter.ToInt32(data, offset + 14);
				int cls = BitConverter.ToInt32(data, offset + 18);
				byte status = data[offset + 22];
				ulong cUID = BitConverter.ToUInt64(data, offset + 23);
				int pos = BitConverter.ToInt32(data, offset + 31);
				byte invite = data[offset + 35];
				string name = GetString(data, offset + 36, NAME_CHAR_MAX_LENGTH);
				Console.WriteLine(".. {0}: {1} ({2}), level: {3}, class: {4}, status: {5}, invite: {6}", pos, pUID, GetPlayerName(cUID), level, cls, status, invite);

				if (list != "") list += ", ";
				list += String.Format("{{ \"pID\": {0}, \"level\": {1}, \"class\": {2}, \"status\": {3}, \"cID\": {4}, \"position\": {5}, \"canInvite\": {6}, \"name\": {7} }}",
					pUID,
					level,
					cls,
					status,
					cUID,
					pos,
					invite,
					GetJson(name));

				offset = next;
			}

			WriteEvent("sPartyList", String.Format("\"type\": {0}, \"leader\": {1}, \"members\": [ {2} ]",
				type,
				leader,
				list));
		}

		/*
		private void ParsePartyMemberMove(byte[] data) { // 0x8568
			ulong cUID = BitConverter.ToUInt64(data, 4);
			int pos = BitConverter.ToInt32(data, 12);
			Console.WriteLine("~~ {0} moved to position {1}", GetPlayerName(cUID), pos);
		}
		 */

		private void ParsePartyMemberDeath(byte[] data) { // 0xAE97
			ulong pUID = BitConverter.ToUInt64(data, 4);

			WriteEvent("sPartyDeath", String.Format("\"target\": {0}",
				pUID));
		}

		private void ParsePartyMemberStats(byte[] data) { // 0x7695
			ulong pUID = BitConverter.ToUInt64(data, 4);
			int curHP = BitConverter.ToInt32(data, 12);
			int curMP = BitConverter.ToInt32(data, 16);
			int maxHP = BitConverter.ToInt32(data, 20);
			int maxMP = BitConverter.ToInt32(data, 24);
			short level = BitConverter.ToInt16(data, 28);
			short combat = BitConverter.ToInt16(data, 30);
			short vitality = BitConverter.ToInt16(data, 32);
			byte status = data[34];
			int stamina = BitConverter.ToInt32(data, 35);
			int curRE = BitConverter.ToInt32(data, 39);
			int maxRE = BitConverter.ToInt32(data, 43);
			int unk = BitConverter.ToInt32(data, 47);
			Console.WriteLine("~~ party member {0}, hp: {4}/{5}, mp: {6}/{7}, re: {8}/{9}, st: {10} ({1}), status: {2}, unk: {3}", pUID, vitality, status, unk, curHP, maxHP, curMP, maxMP, curRE, maxRE, stamina);

			WriteEvent("sPartyStatsUpdate", String.Format(
				"\"target\": {0}, " +
				"\"hp\": {{ \"current\": {1}, \"max\": {2} }}, " +
				"\"mp\": {{ \"current\": {3}, \"max\": {4} }}, " +
				"\"re\": {{ \"current\": {5}, \"max\": {6} }}, " +
				"\"level\": {7}, " +
				"\"combat\": {8}, " +
				"\"stamina\": {9}, " +
				"\"vitality\": {10}, " +
				"\"status\": {11}",
				pUID,
				curHP,
				maxHP,
				curMP,
				maxMP,
				curRE,
				maxRE,
				level,
				combat,
				stamina,
				vitality,
				status));
		}

		private void ParsePartyMemberUpdateHP(byte[] data) { // 0xCCD0
			ulong pUID = BitConverter.ToUInt64(data, 4);
			int curHP = BitConverter.ToInt32(data, 12);
			int maxHP = BitConverter.ToInt32(data, 16);

			WriteEvent("sPartyUpdateHP", String.Format(
				"\"target\": {0}, \"current\": {1}, \"max\": {2}",
				pUID,
				curHP,
				maxHP));
		}

		private void ParsePartyMemberUpdateMP(byte[] data) { // 0x7DAF
			ulong pUID = BitConverter.ToUInt64(data, 4);
			int curMP = BitConverter.ToInt32(data, 12);
			int maxMP = BitConverter.ToInt32(data, 16);

			WriteEvent("sPartyUpdateMP", String.Format(
				"\"target\": {0}, \"current\": {1}, \"max\": {2}",
				pUID,
				curMP,
				maxMP));
		}

		private void ParsePartyMemberUpdateRE(byte[] data) { // 0xBB69
			ulong pUID = BitConverter.ToUInt64(data, 4);
			int curRE = BitConverter.ToInt32(data, 12);
			int maxRE = BitConverter.ToInt32(data, 16);
			int unk = BitConverter.ToInt32(data, 20);
			Console.WriteLine("~~ party member {0} re: {1}/{2}, unk: {3}", pUID, curRE, maxRE, unk);

			WriteEvent("sPartyUpdateRE", String.Format(
				"\"target\": {0}, \"current\": {1}, \"max\": {2}",
				pUID,
				curRE,
				maxRE));
		}

		private void ParsePartyMemberCanInvite(byte[] data) { // 0xF5A3
			ulong pUID = BitConverter.ToUInt64(data, 4);
			byte canInvite = data[12];
			Console.WriteLine("~~ party member {0:X8} can invite: {1}", pUID, canInvite);
		}

		private void ParsePartyLeaderChange(byte[] data) { // 0xBFCE
			// shift <- UInt16(..., 4)
			ulong cUID = BitConverter.ToUInt64(data, 6);
			// name <- string
			Console.WriteLine("~~ {0} is now party leader", GetPlayerName(cUID));

			WriteEvent("sPartyLeader", String.Format("\"target\": {0}",
				cUID));
		}

		private void ParseOtherHP(byte[] data) { // 0xD1F5
			ulong target = BitConverter.ToUInt64(data, 4);
			float hp = BitConverter.ToSingle(data, 12);
			byte unk = data[16];
			uint edge_d = BitConverter.ToUInt32(data, 17);
			float edge_f = BitConverter.ToSingle(data, 21);
			// unk <- 4 (14 00 00 00 = 8,000?)
			// unk <- 4 (03 00 00 00)

			WriteEvent("sOtherHP", String.Format("\"target\": {0}, \"hp\": {1}, \"unk\": {2}, \"edge\": {{ \"d\": {3}, \"f\": {4} }}",
				target,
				hp,
				unk,
				edge_d,
				edge_f));
		}

		private void ParseNpcStatus(byte[] data) { // 0xA5EB
			ulong creature = BitConverter.ToUInt64(data, 4);
			byte enraged = data[12];
			int unk1 = BitConverter.ToInt32(data, 13); // (HP / 20)?
			ulong target = BitConverter.ToUInt64(data, 17);
			int unk2 = BitConverter.ToInt32(data, 25);

			Console.WriteLine("~~ {0} status (enraged: {1}, unk1: {2}) - target: {3}, unk2: {4}",
				creature,
				enraged,
				unk1,
				GetPlayerName(target),
				unk2);

			WriteEvent("sNpcStatus", String.Format("\"creature\": {0}, \"enraged\": {1}, \"unk1\": {2}, \"target\": {3}, \"unk2\": {4}",
				creature,
				enraged,
				unk1,
				target,
				unk2));
		}

		private void ParseNpcEmotion(byte[] data) { // 0x900F
			ulong target = BitConverter.ToUInt64(data, 4);
			ulong creature = BitConverter.ToUInt64(data, 12);
			int emotion = BitConverter.ToInt32(data, 20);

			Console.WriteLine("~~ {1} -> {0} emotion: {2}", creature, GetPlayerName(target), emotion);

			WriteEvent("sNpcEmotion", String.Format("\"target\": {0}, \"creature\": {1}, \"emotion\": {2}",
				target,
				creature,
				emotion));
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

			WriteEvent("sNpcInfo", String.Format("\"uID\": {0}, \"npc\": {1}, \"type\": {2}, \"model\": {3}, \"owner\": {4}",
				uID,
				npcID,
				npcType,
				npcModel,
				owner));
		}

		private void ParseNpcCombatStatus(byte[] data) { // 0xE1B2
			ulong id = BitConverter.ToUInt64(data, 4);
			byte status = data[12];

			WriteEvent("sNpcCombat", String.Format("\"id\": {0}, \"status\": {1}",
				id,
				status));
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

		private void ParseChatMessage(byte[] data) { // 0xAD3D
			ushort senderOffset = BitConverter.ToUInt16(data, 4);
			ushort messageOffset = BitConverter.ToUInt16(data, 6);
			uint type = BitConverter.ToUInt32(data, 8);
			ulong authorID = BitConverter.ToUInt64(data, 12);
			// ? <- data[20]
			// GM? <- data[21]
			// (own message?) <- data[22]
			string authorName = GetString(data, senderOffset, NAME_CHAR_MAX_LENGTH);
			string message = GetString(data, messageOffset, 1023);

			WriteEvent("sChatMessage", String.Format("\"sender\": {{ \"id\": {0}, \"name\": {1} }}, \"type\": {2}, \"message\": {3}",
				authorID,
				GetJson(authorName),
				type,
				GetJson(message)));
		}

		private void ParseImageData(byte[] data) { // 0x530A
			ushort filenameOffset = BitConverter.ToUInt16(data, 4);
			ushort dataOffset = BitConverter.ToUInt16(data, 6);
			ushort dataLength = BitConverter.ToUInt16(data, 8);
			string filename = GetString(data, filenameOffset, 64);
			string imageData = "";
			for (uint i = 0; i < dataLength; i++) {
				if (imageData != "") imageData += ",";
				imageData += data[dataOffset + i];
			}

			WriteEvent("sImageData", String.Format("\"filename\": {0}, \"data\": [{1}]",
				GetJson(filename),
				imageData));
		}

		private void ParseLockonResult(byte[] data) { // 0x7D18
			ulong target = BitConverter.ToUInt64(data, 4);
			uint attack = BitConverter.ToUInt32(data, 12);
			byte result = data[16];
		}

		private void CParseYield(byte[] data) { // 0xF215
			WriteEvent("cYield", "");
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
