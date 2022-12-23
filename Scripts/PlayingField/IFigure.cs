using UnityEngine;

namespace Games.KnightsMove.Scripts.PlayingField
{
    public interface IFigure
    {
        ICellBehaviour CheckingPossibilityOfMakingMove(Vector2[] localPos, Vector2Int figurePos, out Vector2[] foundPathArray);

        public Vector2Int GetPositionByMoves(Vector2 boardSize, int depth, int localPosX, int localPosY);

        public void StopMove();
        void InitStartPos(Vector2Int vector2Int);
        Vector2Int ReturnOnStartPos();
        Vector2Int RebaseOnRandomPos(ICellCollection board);
        Sprite ChangeSprite(Sprite achievementSprite);
    }
}
