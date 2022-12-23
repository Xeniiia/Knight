using System;
using System.Collections;
using Backend.Scripts.Main;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.CellsItems.Goals
{
    public class Goal : MonoBehaviour, ICellItem
    {
        private static readonly int Achieved = Animator.StringToHash("Achieved");
        [SerializeField] private ParticleSystem boomParticles;
        [SerializeField] private ParticleSystem particlesOnCreate;
        private Coroutine _winCoroutine;
        private Animator _animator;
        public event Action WinAction;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public void Execute()
        {
            this.RestartCoroutine(ref _winCoroutine, WinState());
            _animator.SetTrigger(Achieved);
        }

        private IEnumerator WinState()
        {
            boomParticles.Play();
            yield return new WaitForSeconds(0.5f);
            WinAction?.Invoke();
            yield return new WaitForEndOfFrame();
        }

        private void CallParticlesOnCreate()
        {
            particlesOnCreate.Play();
        }
    }
}
