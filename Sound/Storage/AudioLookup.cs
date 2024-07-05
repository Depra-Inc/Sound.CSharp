using System.Collections.Generic;
using Depra.Sound.Exceptions;

namespace Depra.Sound.Storage
{
	/// <summary>
	/// Represents a collection of items indexed by a key.
	/// </summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	public abstract class AudioLookup<TKey, TValue>
	{
		private readonly Dictionary<TKey, TValue> _map = new();

		/// <summary>
		/// Resolves the value associated with the specified key.
		/// </summary>
		/// <param name="key">The key of the value to resolve.</param>
		/// <returns>The value associated with the specified key.</returns>
		/// <exception cref="KeyNotDefinedException">Thrown when the key is not found in the collection.</exception>
		public TValue Resolve(TKey key) => _map.TryGetValue(key, out var value)
			? value
			: throw new KeyNotDefinedException(key);

		/// <summary>
		/// Registers a new key-value pair in the collection.
		/// </summary>
		/// <param name="key">The key to add.</param>
		/// <param name="value">The value to add.</param>
		/// <exception cref="KeyAlreadyDefinedException">Thrown when the key already exists in the collection.</exception>
		public void Register(TKey key, TValue value)
		{
			if (_map.TryAdd(key, value) == false)
			{
				throw new KeyAlreadyDefinedException(key);
			}
		}
	}
}