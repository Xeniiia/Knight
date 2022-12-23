using System;
using System.Collections.Generic;
using System.Linq;
using Games.KnightsMove.Scripts.PlayingField.Figures.StatesMovesResult;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures.FiguresTypes
{
    public class BishopFigure : Figure  //Слон
    {
        private Vector2 _oldFigurePos;


        public override ICellBehaviour CheckingPossibilityOfMakingMove(Vector2[] localPos, Vector2Int figurePos, out Vector2[] foundPathArray)
        {
            List<Vector2> posList = localPos.ToList<Vector2>();
            posList = posList.Distinct().ToList();

            List<Vector2> foundPath = new List<Vector2>();
            foundPathArray = localPos;

            posList = SelectFigurePosCheck(posList, figurePos, out var cellWithFigure);

            FindPathOnDiagonal(figurePos, posList, foundPath);

            if (foundPath.Count > 0 &&
                cellWithFigure &&
                LengthCheck(figurePos, foundPath, foundPath.Count() - 1) &&
                foundPath.Last() != _oldFigurePos)
            {
                foundPathArray[0] = figurePos;
                foundPath.ToArray().CopyTo(foundPathArray, 1);
                _oldFigurePos = figurePos;
                return new CorrectState(x => MoveFigure(x));
            }
            else
            {
                return new ErrorState();
            }
        }

        private bool LengthCheck(Vector2Int figurePos, List<Vector2> foundPath, int v)
        {
            return (Math.Abs(figurePos.x - foundPath.Last().x) == Math.Abs(figurePos.y - foundPath.Last().y));
        }

        private List<Vector2> SelectFigurePosCheck(List<Vector2> posList, Vector2 pos, out bool cellWithFigure)
        {
            cellWithFigure = false;
            for (int i = 0; i < posList.Count(); i++)
            {
                if (posList[i].x == pos.x && posList[i].y == pos.y)
                {
                    cellWithFigure = true;
                    posList.RemoveAt(i);
                    break;
                }
            }

            return posList;
        }

        private static void FindPathOnDiagonal(Vector2 pos, List<Vector2> posList, List<Vector2> foundPath)
        {
            if (posList.Count == 0) return;

            for (int i = 0; i < posList.Count(); i++)
            {
                if (Math.Abs(posList[i].x - pos.x) == 1 && Math.Abs(posList[i].y - pos.y) == 1)
                {
                    var newCurrentPos = posList[i];
                    foundPath.Add(newCurrentPos);
                    posList.Remove(newCurrentPos);
                    FindPathOnDiagonal(newCurrentPos, posList, foundPath);
                }
            }

            return;
        }


        #region Позиции достижимой цели
        public override Vector2Int GetPositionByMoves(Vector2 boardSize, int depth, int localPosX, int localPosY)
        {
            List<Vector2Int> reachableCells = new List<Vector2Int>();
            FindingTargetsCells(reachableCells, boardSize, localPosX, localPosY, (int)boardSize.x);

            DeleteFigurePos(localPosX, localPosY, reachableCells);

            for (int i = 1; i < depth; i++)
            {
                System.Random rnd = new System.Random();
                var randomCell = reachableCells[rnd.Next(0, reachableCells.Count)];

                List<Vector2Int> newReachableCells = new List<Vector2Int>();
                FindingTargetsCells(newReachableCells, boardSize, randomCell.x, randomCell.y, (int)boardSize.x);
                reachableCells = newReachableCells.Except(reachableCells).ToList();
            }

            return reachableCells[new System.Random().Next(0, reachableCells.Count())];
        }

        private static void DeleteFigurePos(int x0, int y0, List<Vector2Int> reachebleCells)
        {
            var ind = reachebleCells.IndexOf(new Vector2Int(x0, y0));
            if (ind != -1) reachebleCells.RemoveAt(ind);
        }

        private void FindingTargetsCells(List<Vector2Int> listCurrentCells, Vector2 boardSize, int x0, int y0, int depth)
        {
            if (depth == 0)
            {
                if (!listCurrentCells.Contains(new Vector2Int(x0, y0)))
                {
                    listCurrentCells.Add(new Vector2Int(x0, y0));
                }

                return;
            }

            int[] dx = { -1,  1, -1,  1 };
            int[] dy = { -1, -1,  1,  1 };

            for (int i = 0; i < 4; i++)
            {
                int x = x0 + dx[i];
                int y = y0 + dy[i];

                if (CheckOfBound((int)boardSize.x, (int)boardSize.y, x, y))
                {
                    if (!listCurrentCells.Contains(new Vector2Int(x, y)))
                    {
                        listCurrentCells.Add(new Vector2Int(x, y));
                    }

                    FindingTargetsCells(listCurrentCells, boardSize, x, y, depth - 1);
                }
            }
        }
        #endregion
    }
}
