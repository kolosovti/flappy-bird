using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Предоставляет доступ к компоненту Text. 
/// </summary>
public class AbstractText : MonoBehaviour
{
    protected Text text;

    protected virtual void Awake()
    {
        text = GetComponent<Text>();
    }
}
