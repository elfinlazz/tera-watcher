using System;
using System.Collections.Generic;

namespace TeraPacketEncryption {
	class PriorityQueue<T> {
		private SortedList<uint, T> _list;
		public int Count {
			get { return _list.Count; }
		}

		public PriorityQueue() {
			_list = new SortedList<uint, T>();
		}

		public void Clear() {
			_list.Clear();
		}

		public void Enqueue(T item, uint priority) {
			_list.Add(priority, item);
		}

		public T Dequeue() {
			T item = Peek();
			_list.RemoveAt(0);
			return item;
		}

		public T Peek() {
			if (Count <= 0) throw new InvalidOperationException();
			return _list[_list.Keys[0]];
		}
	}
}
