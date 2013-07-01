using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

/// <summary>
/// CollectionExtensions class.
/// </summary>
public static class CollectionExtension
{
    /// <summary>
    /// Add a range of objects to a Collection object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="values">Range of values to add.</param>
    public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> values)
    {
        foreach (var item in values)
        {
            collection.Add(item);
        }
    }

    /// <summary>
    /// Find an object within a Collection object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns default object if none were found or first object matching terms.</returns>
    public static T Find<T>(this Collection<T> collection, Predicate<T> predicate)
    {
        foreach (var item in collection)
        {
            if (predicate(item))
            {
                return item;
            }
        }

        return default(T);
    }

    /// <summary>
    /// Find all objects within a Collection object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns default object if none were found or all objects matching terms.</returns>
    public static Collection<T> FindAll<T>(this Collection<T> collection, Predicate<T> predicate)
    {
        Collection<T> all = new Collection<T>();
        foreach (var item in collection)
        {
            if (predicate(item))
            {
                all.Add(item);
            }
        }

        return all;
    }

    /// <summary>
    /// Find the index of an object within a Collection object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object index.</returns>
    public static int FindIndex<T>(this Collection<T> collection, Predicate<T> predicate)
    {
        return FindIndex<T>(collection, 0, predicate);
    }

    /// <summary>
    /// Find the index of an object within a Collection object starting at a specified index.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="startIndex">Starting index.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object index.</returns>
    public static int FindIndex<T>(this Collection<T> collection, int startIndex, Predicate<T> predicate)
    {
        return FindIndex(collection, startIndex, collection.Count, predicate);
    }

    /// <summary>
    /// Find the index of an object within a Collection object at a specified index and only a certain length.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="startIndex">Starting index.</param>
    /// <param name="count">Count.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object index.</returns>
    public static int FindIndex<T>(this Collection<T> collection, int startIndex, int count, Predicate<T> predicate)
    {
        for (int i = startIndex; i < count; i++)
        {
            if (predicate(collection[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Find the last object within a Collection object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object.</returns>
    public static T FindLast<T>(this Collection<T> collection, Predicate<T> predicate)
    {
        for (int i = collection.Count - 1; i >= 0; i--)
        {
            if (predicate(collection[i]))
            {
                return collection[i];
            }
        }

        return default(T);
    }

    /// <summary>
    /// Find the last index of an object within a Collection object.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object index.</returns>
    public static int FindLastIndex<T>(this Collection<T> collection, Predicate<T> predicate)
    {
        return FindLastIndex<T>(collection, collection.Count - 1, predicate);
    }

    /// <summary>
    /// Find the last index of an object within a Collection object starting at a specified index.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="startIndex">Start index.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object index.</returns>
    public static int FindLastIndex<T>(this Collection<T> collection, int startIndex, Predicate<T> predicate)
    {
        return FindLastIndex<T>(collection, startIndex, startIndex + 1, predicate);
    }

    /// <summary>
    /// Find the last index of an object within a Collection object starting at a specified index and length.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="startIndex">Start index.</param>
    /// <param name="count">Count.</param>
    /// <param name="predicate">Predicate object.</param>
    /// <returns>Returns object index.</returns>
    public static int FindLastIndex<T>(this Collection<T> collection, int startIndex, int count, Predicate<T> predicate)
    {
        for (int i = startIndex; i >= startIndex - count; i--)
        {
            if (predicate(collection[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Performs a given action on each object in the Collectoin.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="action">Action to perform.</param>
    public static void ForEach<T>(this Collection<T> collection, Action<T> action)
    {
        foreach (var item in collection)
        {
            action(item);
        }
    }

    /// <summary>
    /// Remove all object from a Collection.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="match">Predicate object.</param>
    /// <returns>Number of object removed.</returns>
    public static int RemoveAll<T>(this Collection<T> collection, Predicate<T> match)
    {
        int count = 0;
        for (int i = 0; i < collection.Count; i++)
        {
            if (match(collection[i]))
            {
                collection.Remove(collection[i]);
                count++;
                i--;
            }
        }

        return count;
    }

    /// <summary>
    /// Determines if all objects within a Collection match a predicate.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="collection">Collection object.</param>
    /// <param name="match">Predicate object.</param>
    /// <returns>A value indicating whether all objects match the predicate.</returns>
    public static bool TrueForAll<T>(this Collection<T> collection, Predicate<T> match)
    {
        foreach (var item in collection)
        {
            if (!match(item))
            {
                return false;
            }
        }

        return true;
    }
}