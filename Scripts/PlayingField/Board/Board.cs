using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backend.Scripts.Main;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories;
using Games.KnightsMove.Scripts.PlayingField.Board.Cells.Factories.ColorsOfCell;
using Games.KnightsMove.Scripts.PlayingField.Figures;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board
{
    public class Board : MonoBehaviour, ICellCollection, IPositionsObserver, ICellSelection, ICorrect, IError
    {
        [SerializeField, Range(4, 7)] private int sideCountCells = 5;
        [SerializeField] private float height = 9.13f;
        [SerializeField] private float width = 9.13f;
        [SerializeField] private Cell cellProto;
        [SerializeField] private float timeToHandleCells;
        private const int CellsCount = 4;
        private List<Vector2> _selectedCells;
        private ICellsControl[,] _cells;
        private float _sizeCell;
        private Coroutine _handleCellsCoroutine;

        public Vector2 CellCount => new Vector2(sideCountCells, sideCountCells);
        public float SizeOfSideCell => _sizeCell;
        private float CheckTimer { get; set; }

        public event Action<Vector2[]> PositionSelected;
        private event Action BoardCleared;
        private event Action ResetCellsOffset;

        public ICellsControl this[int x, int y] => _cells[x, y];


        private void Awake()
        {
            _selectedCells = new List<Vector2>();
            _sizeCell = height / sideCountCells;
            var cellFactory = new CellFactory(new BlackCell(), cellProto);
            Generate(cellFactory, _sizeCell);
        }


        #region GenerateBoard

        private void Generate(ICellFactory cellFactory, float sizeCell)
        {
            var startPos = GetStartPosition();
            FillCells(startPos.x, startPos, sideCountCells, cellFactory, sizeCell);
        }

        private void FillCells(float startX, Vector2 nextPos, int sizeCountCell, ICellFactory cellFactory,
            float sizeCell) //TODO: in DTO
        {
            var isEven = sizeCountCell % 2 == 0;

            _cells = new ICellsControl[sideCountCells, sideCountCells];
            var thisTransform = transform;
            int startIndex = 0;
            for (int y = 0; y < sizeCountCell; y++)
            {
                for (int x = startIndex; x < sizeCountCell; x++)
                {
                    var dto = new InstCellDTO()
                    {
                        CellSelection = this,
                        LocalPos = new Vector2(x, y),
                        NextPos = nextPos,
                        ParentTransform = thisTransform,
                        Size = sizeCell
                    };

                    var inst = cellFactory.GetCell(dto);

                    cellFactory.InversionColor();

                    _cells[x, y] = inst;
                    BoardCleared += inst.DeleteCellItem;
                    ResetCellsOffset += inst.ResetOffset;
                    nextPos.x += sizeCell;
                }

                if (isEven) cellFactory.InversionColor();
                startIndex = 0;
                nextPos.x = startX;
                nextPos.y += sizeCell;
            }
        }

        private Vector2 GetStartPosition()
        {
            float halfCell = _sizeCell / 2f;
            float X = 0 - height / 2f + halfCell;
            float Y = 0 - width / 2f + halfCell;

            return new Vector2(X, Y);
        }

        #endregion


        public void ClearBoard()
        {
            BoardCleared?.Invoke(); //todo: foreach
            ResetCellsOffset?.Invoke(); //todo: foreach
        }
        

        public Vector2[] ParseToLocalCoordinates(Vector2[] posCell) //todo: length
        {
            Vector2[] localCoordinates = new Vector2[posCell.Count()];
            var startPos = GetStartPosition();

            for (int i = 0; i < posCell.Count(); i++)
            {
                var xFromStart = CalibrationCoordinateFromStart(posCell[i].x, startPos.x);
                var yFromStart = CalibrationCoordinateFromStart(posCell[i].y, startPos.y);

                var x = (int)Math.Truncate(xFromStart / _sizeCell);
                var y = (int)Math.Truncate(yFromStart / _sizeCell);
                localCoordinates[i] = new Vector2(x, y);
            }

            var listLocalCoordinates = localCoordinates.ToList<Vector2>();
            listLocalCoordinates = listLocalCoordinates.Distinct().ToList();
            Vector2[] localCoordinatesNoDupes = new Vector2[listLocalCoordinates.Count()];
            for (int i = 0; i < listLocalCoordinates.Count(); i++)
            {
                localCoordinatesNoDupes[i] = listLocalCoordinates[i];
            }

            return localCoordinatesNoDupes;
        }

        private static float CalibrationCoordinateFromStart(float currentCoord, float startPosAxis)
        {
            float coordFromStart = 0;
            if (currentCoord >= 0 && startPosAxis <= 0)
                coordFromStart = Math.Abs(startPosAxis) + currentCoord;
            else if (currentCoord >= 0 && startPosAxis >= 0)
                coordFromStart = currentCoord - startPosAxis;
            else if (currentCoord <= 0 && startPosAxis < 0)
                coordFromStart = Math.Abs(startPosAxis) - Math.Abs(currentCoord);

            return coordFromStart;
        }


        public void ResetMiddleDepthForCells()
        {
            foreach (ICalibrated c in _cells)
            {
                c.UpdateDefaultMiddleDepth();
            }
        }


        public void ResetOffsetDepthForCells()
        {
            foreach (ICalibrated c in _cells)
            {
                c.ResetOffset();
            }
        }


        public ICellsControl GetRandomCell(out Vector2Int localPos)
        {
            System.Random rnd = new System.Random();
            localPos = new Vector2Int(rnd.Next(0, sideCountCells), rnd.Next(0, sideCountCells));

            return _cells[localPos.x, localPos.y];
        }


        public ICellsControl[] GetCellsOnLocalCoordinates(Vector2[] pos) =>
            pos.Select(v => _cells[(int)v.x, (int)v.y]).ToArray(); 


        public void CellSelected(Vector2 localPos)
        {
            if (!_selectedCells.Contains(localPos))
            {
                _selectedCells.Add(localPos);
                this.RestartCoroutine(ref _handleCellsCoroutine, HandleSelectedCells(), () => CheckTimer = 0);
            }
        }

        public void CellUnselected(Vector2 localPos)
        {
            if (_selectedCells.Contains(localPos)) _selectedCells.Remove(localPos);
            this.RestartCoroutine(ref _handleCellsCoroutine, HandleSelectedCells());
        }

        private IEnumerator HandleSelectedCells()
        {
            if (_selectedCells.Count == CellsCount)
            {
                CheckTimer = 0;
                while(CheckTimer <= 1f)
                {
                    CheckTimer += Time.deltaTime / timeToHandleCells;
                    yield return null;
                }

                PositionSelected?.Invoke(_selectedCells.ToArray());
            }
            yield return null;
        }


        public void Correct(ICellsControl[] cells)
        {
            //_selectedCells.Clear();
            foreach (var t in cells)
            {
                t.ActionOnCorrectSelect();
            }
        }


        public void Error(ICellsControl[] cells)
        {
            foreach (var t in cells)
            {
                t.ActionOnErrorSelect();
            }
        }
    }
}