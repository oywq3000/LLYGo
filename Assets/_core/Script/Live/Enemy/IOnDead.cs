using System;
using UnityEngine.Events;

namespace _core.Script.Enemy
{
    public interface IOnDead
    {
        //provide to outside to get the recall
        UnityAction OnDead { get; set; }
    }
}