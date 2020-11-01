using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    /// <summary>
    /// Возвращает последний элемент в списке.
    /// Если массив пуст, возвращает <paramref name="null"/>
    /// </summary>
    public static TSource Last<TSource>(this List<TSource> list) where TSource : new() 
    {
        if (list.Count - 1 >= 0)
        {
            return list[list.Count - 1];
        }
        else
        {
            return new TSource();
        }
    }
}
