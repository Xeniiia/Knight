using System;
using System.Collections.Generic;
using System.Linq;
using Games.KnightsMove.Scripts.PlayingField.Figures.StatesMovesResult;
using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField.Figures.FiguresTypes
{
    public class KnightFigure : Figure
    {
        public override ICellBehaviour CheckingPossibilityOfMakingMove(Vector2[] localPos, Vector2Int figurePos, out Vector2[] foundPathArray)
        {

            List<Vector2> posList = localPos.ToList();
            posList = posList.Distinct().ToList();

            List<Vector2> foundPath = new List<Vector2>();
            foundPathArray = localPos;

            posList = SelectFigurePosCheck(posList, figurePos, out var cellWithFigure);

            FindPathLitterG(figurePos, posList, foundPath);

            if (foundPath.Count == 3 && 
                LengthCheck(figurePos, foundPath, foundPath.Count() - 1) && 
                cellWithFigure)
            {
                foundPathArray[0] = figurePos;
                foundPath.ToArray().CopyTo(foundPathArray, 1);
                return new CorrectState(x => MoveFigure(x));
            }
            else
            {
                return new ErrorState();
            }
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

        private bool LengthCheck(Vector2Int figurePos, List<Vector2> foundPath, int n)
        {
            return (Math.Pow(Math.Abs(foundPath[n].x - figurePos.x), 2) + Math.Pow(Math.Abs(foundPath[n].y - figurePos.y), 2) == 5);
        }

        private static void FindPathLitterG(Vector2 pos, List<Vector2> posList, List<Vector2> foundPath)
        {
            if (posList.Count == 0) return;

            for (int i = 0; i < posList.Count(); i++)
            {
                if (Math.Abs(posList[i].x - pos.x) == 1 && Math.Abs(posList[i].y - pos.y) == 0 ||
                    Math.Abs(posList[i].y - pos.y) == 1 && Math.Abs(posList[i].x - pos.x) == 0)
                {
                    var newCurrentPos = posList[i];
                    foundPath.Add(newCurrentPos);
                    posList.Remove(newCurrentPos);
                    FindPathLitterG(newCurrentPos, posList, foundPath);
                }
            }

            return;
        }


        #region Позиции достижимой цели
        public override Vector2Int GetPositionByMoves(Vector2 boardSize, int numOfMoves, int localPosX, int localPosY)
        {
            var reachableCells = FillListWithTarget(boardSize, localPosX, localPosY, numOfMoves);

            DeleteHorsePos(localPosX, localPosY, reachableCells);

            var rnd = new System.Random();
            return reachableCells[rnd.Next(0, reachableCells.Count)]; //TODO: Проверить выход за пределы коллекции
        }

        private List<Vector2Int> FillListWithTarget(Vector2 boardSize, int x0, int y0, int depth)
        {
            var reachableCells = new List<Vector2Int>();
            if (depth > 1)
            {
                var extraCells = new List<Vector2Int>();
                FindingTargetsCells(extraCells, boardSize, x0, y0, depth - 1);
                FindingTargetsCells(reachableCells, boardSize, x0, y0, depth);
                reachableCells = reachableCells.Except(extraCells).ToList();
            }
            else
            {
                FindingTargetsCells(reachableCells, boardSize, x0, y0, depth);
            }

            return reachableCells;
        }

        private static void DeleteHorsePos(int x0, int y0, List<Vector2Int> reachableCells)
        {
            var ind = reachableCells.IndexOf(new Vector2Int(x0, y0));
            if (ind != -1) reachableCells.RemoveAt(ind);
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

            int[] dx = { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] dy = { -1, 1, -2, 2, -2, 2, -1, 1 };

            for (int i = 0; i < 8; i++)
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
