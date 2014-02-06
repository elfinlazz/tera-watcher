using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Watcher {
	class Handler {
		private Dictionary<Type, Delegate> events;

		public bool On(Delegate d) {
			// get parameter list for delegate
			ParameterInfo[] pi = d.GetType().DeclaringMethod.GetParameters();

			// verify parameter count = 1
			if (pi.Count() != 1) return false;

			// verify type of parameter is derived from System.EventArgs
			Type t = pi[0].ParameterType;
			if (!typeof(EventArgs).IsAssignableFrom(t)) return false;

			// add to event handlers
			if (!events.ContainsKey(t)) events.Add(t, null);
			events[t] = Delegate.Combine(events[t], d);

			// return success
			return true;
		}

		public void Emit(EventArgs e) {
			Delegate d;
			if (events.TryGetValue(e.GetType(), out d)) {
				d.DynamicInvoke(e);
			}
		}
	}
}
