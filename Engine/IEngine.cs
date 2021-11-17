namespace Engine
{
    public interface IEngine
    {
        IEvaluation Evaluate(IPosition position);

        IMove BestMove(IPosition position);
    }
}