using System;
using UnityEngine;

public interface IDie 
{
    GameObject gameObject { get; }

    event Action<IDie> OnDie;
    event Action<int, int> OnHealthChange;
}
