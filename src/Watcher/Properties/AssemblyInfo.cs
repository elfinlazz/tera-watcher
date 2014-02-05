using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Tera Watcher")]
[assembly: AssemblyProduct("Tera Watcher")]

[assembly: ComVisible(false)]
[assembly: Guid("5be5f28e-75f1-457b-8a85-53b8fbef3e73")]
[assembly: AssemblyVersion("0.1.*")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
