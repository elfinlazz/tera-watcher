using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using SharpPcap;

namespace TeraPacketEncryption {
	class Program {
		static void Main(string[] args) {
			var devices = CaptureDeviceList.Instance;
			if (devices.Count < 1) {
				Console.WriteLine("No capturable devices found on this machine.");
				return;
			}

			int num = 0;
			if (args.Length > 0) int.TryParse(args[0], out num);
			
			if (num < 1 || num > devices.Count) {
				Console.WriteLine();
				Console.WriteLine("Usage: {0} <device #> <IP>", Process.GetCurrentProcess().ProcessName);
				Console.WriteLine();
				Console.WriteLine("Available devices:");
				ICaptureDevice dev;
				for (int i = 0; i < devices.Count; i++) {
					dev = devices[i];
					Console.Write("  {0}. {1}", i + 1, dev.Name);
					if (dev.Description.Length > 0) Console.Write(" ({0})", dev.Description);
					Console.WriteLine();
				}
				Console.WriteLine();
				Console.WriteLine("Server IPs:");
				Console.WriteLine("  Ascension Valley : 208.67.49.84");
				Console.WriteLine("  Celestial Hills  : 208.67.49.68");
				Console.WriteLine("  Lake of Tears    : 208.67.49.52");
				Console.WriteLine("  Mount Tyrannas   : 208.67.49.92");
				Console.WriteLine("  Tempest Reach    : 208.67.49.28");
				Console.WriteLine("  Valley of Titans : 208.67.49.100");
				Console.WriteLine();
				return;
			}

			var parse = new Parse();

			Console.WriteLine();
			Console.WriteLine("Device : {0}", devices[num - 1].Name);
			Console.WriteLine("IP     : {0}", "208.67.49.68");
			Console.WriteLine();

			Console.WriteLine("loading plugins...");
			var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\plugins";
			var plugins = Directory.EnumerateFiles(directory);
			foreach (string file in plugins) {
				var plugin = Assembly.LoadFrom(file);
				foreach (Type type in plugin.GetTypes()) {
					if (type.GetInterface("TeraWatcherAPI.IPlugin") != null) {
						var module = plugin.CreateInstance(type.FullName) as TeraWatcherAPI.IPlugin;
						try {
							module.Load(parse);
							Console.WriteLine("- successfully loaded {0}", type.FullName);
						} catch (Exception e) {
							Console.WriteLine("- failed to load {0} ({1})", type.FullName, Path.GetFileName(file));
							Console.WriteLine(e);
						}
					}
				}
			}
			Console.WriteLine();

			var capture = new Capture(num - 1, "208.67.49.68", parse);

			return;
		}
	}
}
