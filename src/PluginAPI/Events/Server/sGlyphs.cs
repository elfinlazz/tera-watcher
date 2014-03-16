using System.Collections.Generic;

namespace TeraWatcherAPI.Events {
	public delegate void sGlyphsHandler(sGlyphsArgs args);

	public class sGlyphsArgs : System.EventArgs {
		public int pointsUsed;
		public int pointsAvailable;
		public Dictionary<uint, byte> glyphs;
	}
}
