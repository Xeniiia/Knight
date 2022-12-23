using System;
using System.Collections;
using Games.KnightsMove.Scripts.CoroutineExtension;
using Games.KnightsMove.Scripts.PlayingField.CellsItems.Goals;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures
{
    public abstract class Figure : MonoBehaviour, IFigure
    {
        private bool _cancellationToken;
        private Vector2 _startPos;
        private Vector2Int _localStartPos;
        private Vector2 _newCellPos;
        private Vector2Int _localNewCellPos;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public abstract ICellBehaviour CheckingPossibilityOfMakingMove(Vector2[] localPos, Vector2Int figurePos, out Vector2[] foundPathArray);


        public abstract Vector2Int GetPositionByMoves(Vector2 boardSize, int depth, int localPosX, int localPosY);


        public void StopMove()
        {
            _cancellationToken = true;
        }


        protected (CoroutineSequence, CoroutineSequence) MoveFigure(Vector2[] points)   //TODO: анимация передвижения
        {
            _cancellationToken = false;
            var firstCoroutine = this.CreateCoroutine(points[0], MoveTo);
            CoroutineSequence nextCoroutine = firstCoroutine;
            for (int i = 1; i < points.Length; i++)
            {
                nextCoroutine = nextCoroutine.AddCallback(points[i], MoveTo);
            }

            return (nextCoroutine, firstCoroutine);
        }


        protected bool CheckOfBound(int sideXcountCells, int sideYcountCells, int x, int y) //TODO: сделать нормальные названия и красиво
        {
            return x >= 0 && y >= 0 && x < sideXcountCells && y < sideYcountCells;
        }


        private IEnumerator MoveTo(Vector2 pos, CoroutineSequence coroutine)
        {
            var startMove = transform.position;
            
            // Вариант через корутину
            // yield return UtilCoroutines.ElapsedCoroutine(2f, f =>
            // {
            //     transform.position = Vector2.Lerp(startMove, pos, f);
            // });
            //
            // Вариант через yield инструкцию
            // yield return new YieldElapsedTime(2f, f => {transform.position = Vector2.Lerp()})
            
            float t = Time.deltaTime;
            while (t < 1 && _cancellationToken == false)
            {
                t += Time.deltaTime * 3;
                transform.position = Vector2.Lerp(startMove,
                    pos,
                    t);
                yield return new WaitForEndOfFrame();
            }

            coroutine?.StartCoroutine();
        }

        public void InitStartPos(Vector2Int localPos)
        {
            _startPos = transform.position;
            _localStartPos = localPos;
        }

        
        public Vector2Int ReturnOnStartPos()
        {
            _animator.SetTrigger("Falling");
            
            return _localStartPos;
        }

        private void SetOnStartPos()
        {
            transform.position = _startPos;
        }


        public Vector2Int RebaseOnRandomPos(ICellCollection board)
        {
            var newCell = board.GetRandomCell(out var newLocalPos);
            while (newCell.CellItem != null)
            {
                newCell = board.GetRandomCell(out newLocalPos);
            }
            _newCellPos = newCell.Position;
            _localNewCellPos = newLocalPos;
            _animator.SetTrigger("Turning");
            
            return _localNewCellPos;
        }

        
        private void SetOnNewCellPos()
        {
            transform.position = _newCellPos;
        }

        
        public Sprite ChangeSprite(Sprite newSprite)
        {
            var previousSprite = spriteRenderer.sprite;
            var size = spriteRenderer.size;
            spriteRenderer.sprite = newSprite;
            spriteRenderer.size = size;

            return previousSprite;
        }
    }
}
