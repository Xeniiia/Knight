using System;
using System.Collections;
using UnityEngine;

namespace Games.KnightsMove.Scripts.CoroutineExtension
{
    public class CoroutineSequence
    {
        private MonoBehaviour _monoBehaviour;
        private readonly Vector2 _pos;
        private Func<Vector2, CoroutineSequence, IEnumerator> _func;
        private CoroutineSequence _callback;
        private Coroutine _coroutine;


        public CoroutineSequence(MonoBehaviour monoBehaviour, Vector2 pos,
            Func<Vector2, CoroutineSequence, IEnumerator> func)
        {
            _monoBehaviour = monoBehaviour;
            _pos = pos;
            _func = func;
        }


        private MonoBehaviour _mono;
        private CoroutineSequence callback1;
        private Func<IEnumerator> func1;
        private Action completedAction;


        public CoroutineSequence(MonoBehaviour mono, Func<IEnumerator> func)
        {
            _mono = mono;
            func1 = func;
        }


        private CoroutineSequence Clone()
        {
            var clone = new CoroutineSequence(_mono, func1);
            clone.completedAction = completedAction;
            return clone;
        }


        public void StartCoroutine(Action onCompleted)
        {
            if (_coroutine != null) return;
            
            this.completedAction = onCompleted;
            _coroutine = _mono.StartCoroutine(SequenceRoutine());
        }


        private IEnumerator SequenceRoutine()
        {
            while (func1 != null)
            {
                yield return func1();
                func1 = null;

                if (callback1 == null) continue;
                func1 = callback1.func1;
                callback1 = callback1.callback1;
            }

            completedAction?.Invoke();
        }


        public CoroutineSequence AddCallback(Func<IEnumerator> func)
        {
            var clone = this.Clone();
            clone.callback1 = this;
            clone.func1 = func;
            return clone;
        }

        public void StartCoroutine()
        {
            _coroutine = _monoBehaviour.StartCoroutine(_func(_pos, _callback));
        }


        public CoroutineSequence AddCallback(Vector2 pos, Func<Vector2, CoroutineSequence, IEnumerator> func)
        {
            _callback = new CoroutineSequence(_monoBehaviour, pos, func);
            return _callback;
        }


        public void StopCoroutine()
        {
            if (_coroutine == null) return;
            _callback?.StopCoroutine();
            _monoBehaviour.StopCoroutine(_coroutine);
        }
    }
}