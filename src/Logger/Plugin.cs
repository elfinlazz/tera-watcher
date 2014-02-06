using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeraWatcherAPI;
using TeraWatcherAPI.Events;

namespace Logger {
	class Plugin : IPlugin {
		public void Load(IHandler Handler) {
			Handler.sPlayerInfo += delegate(sPlayerInfoArgs e) {
				Handler.Log(2, "sPlayerInfo");
			};
		}
	}
}
