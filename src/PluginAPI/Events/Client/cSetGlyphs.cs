using System.Collections.Generic;

namespace TeraWatcherAPI.Events {
	public delegate void cSetGlyphsHandler(cSetGlyphsArgs args);

	public class cSetGlyphsArgs : System.EventArgs {
		public byte unk;
		public List<uint> glyphs;
	}
}
