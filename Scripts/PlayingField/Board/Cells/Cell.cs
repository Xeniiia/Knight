using System;
using System.Collections;
using Backend.Scripts.Main;
using Games.KnightsMove.Scripts.PlayingField.CellsItems.Goals;
using OpenCVForUnity.CoreModule;
using TMPro;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Board.Cells
{
    public class Cell : MonoBehaviour, ICell, ICalibrated, ICellsControl
    {
        [SerializeField] private TextMeshPro textMesh;
        private SpriteRenderer _spriteRenderer;
        private ICellSelection _board;
        private ICellSpriteCollection _spriteCollection;
        private Vector2 _localPos;
        private Vector2 _normalSize;
        private Vector2 _selectedSize;
        private Color32 _cellColor;
        private int _depthOffset;
        private int _defaultMiddleDepth;
        private int _numOfSprite;
        private bool _dark;
        private bool _selected;
        private bool _wasSelected;

        public int MiddleDepth
        {
            get => this.GetMiddleDepth() + _depthOffset;
        }

        public int DefaultMiddleDepth
        {
            get => 0;
        }

        public RotatedRect Rectangle { get; set; }
        public ICellItem CellItem { get; set; }

        public Vector2 Position => gameObject.transform.position;

        public Vector2Int GetLocalPos() => new Vector2Int((int)_localPos.x, (int)_localPos.y);


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteCollection = transform.parent.gameObject.GetComponent<ICellSpriteCollection>();
            SetRectangle(_spriteRenderer);
            //textMesh.fontSize = 2;
        }

        private void FixedUpdate()
        {
            int difference = Difference();

            if (difference > 8)
                Select();
            else
                Unselect();
#if UNITY_EDITOR
            // if (CellItem is Goal) textMesh.text = "GOAL!";
            // else if (CellItem is Pit) textMesh.text = "PIT!";
            // else if (CellItem is Storm) textMesh.text = "STORM!";
            // else textMesh.text = "";
            //textMesh.text = $"c: {MiddleDepth}\n d: {_defaultMiddleDepth}";
#endif
        }

        [Sirenix.OdinInspector.Button]
        private void DebugLogDifference()
        {
            Debug.Log(Difference());
        }

        private int Difference()
        {
            var curDepth = MiddleDepth;
            var midDepth = _defaultMiddleDepth;
            var dif = curDepth > midDepth ? Math.Abs(curDepth - midDepth) : Math.Abs(midDepth - curDepth) * -1;

            return dif;
        }

        private void Select()
        {
            if (!_selected)
            {
                _selected = true;
                _spriteCollection.SetSelectedCellSprite(_dark, _numOfSprite, _spriteRenderer, _selectedSize);
                _board.CellSelected(_localPos);
            }
        }

        private void Unselect()
        {
            if (_selected)
            {
                _selected = false;
                _spriteCollection.SetUnselectedCellSprite(_dark, _numOfSprite, _spriteRenderer, _normalSize);
                _board.CellUnselected(_localPos);
            }
        }


        private void OnMouseDown()
        {
            if (_depthOffset != 0)
            {
                ResetOffset();
                return;
            }
            if (!_selected)
            {
                SetOffsetForSelect();
            }
            else
            {
                SetOffsetForUnselect();
            }
        }

        public void ResetOffset()
        {
            _depthOffset = 0;
        }

        private void SetOffsetForSelect()
        {
            var curDepth = this.GetMiddleDepth();
            var newDepth = _defaultMiddleDepth + 20;
            _depthOffset = (curDepth - newDepth) * -1;
        }

        private void SetOffsetForUnselect()
        {
            var curDepth = MiddleDepth;
            var newDepth = _defaultMiddleDepth;
            _depthOffset = (curDepth - newDepth) * -1;
        }


        public bool CallResultState()
        {
            var res = false;
            if (CellItem != null)
            {
                var tempCellItem = CellItem;
                if (CellItem is Goal)
                {
                    res = true;
                    CellItem = null;
                }

                tempCellItem.Execute();
            }

            return res;
        }


        public void DeleteCellItem()
        {
            CellItem = null;
        }


        public void Init(ICellSelection board, Vector2 pos)
        {
            _board = board;
            _localPos = pos;
            _defaultMiddleDepth = this.GetMiddleDepth();
        }


        public void UpdateDefaultMiddleDepth()
        {
            _defaultMiddleDepth = this.GetMiddleDepth();
        }


        public void SetSprite(bool color)
        {
            _dark = color;
            _numOfSprite = _spriteCollection.SetRandomSprite(color, _spriteRenderer);
            var size = _spriteRenderer.size;
            _normalSize = size;
            _selectedSize = new Vector2(size.x * 1.16f, size.y * 1.16f);
        }


        public void ActionOnErrorSelect()
        {
            //StartCoroutine(ChangeColorError(stateColor));
        }


        public void ActionOnCorrectSelect()
        {
            //StartCoroutine(ChangeColorCorrect(stateColor));
            //tartCoroutine(FullResetDepth());
        }

        private IEnumerator FullResetDepth()
        {
            yield return new WaitForSeconds(1.5f);
            _defaultMiddleDepth = this.GetMiddleDepth();
            _depthOffset = 0;
        }


        private void SetRectangle(SpriteRenderer sr)
        {
            this.GetRectangleFromWorld(sr);
        }
    }
}