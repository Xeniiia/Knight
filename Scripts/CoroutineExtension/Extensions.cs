using System;
using System.Collections;
using UnityEngine;

namespace Games.KnightsMove.Scripts.CoroutineExtension
{
    public static class Extensions
    {
        public static CoroutineSequence CreateCoroutine(this MonoBehaviour monoBehaviour, Vector2 pos, Func<Vector2, CoroutineSequence, IEnumerator> func)
        {
            return new CoroutineSequence(monoBehaviour, pos, func);
        }
    }
}
