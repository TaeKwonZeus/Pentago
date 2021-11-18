namespace Engine
{
    public interface IEngine
    {
        Evaluation Evaluate(Position position);

        Move BestMove(Position position);

        static IEngine Instance()
        {
            return null;
        }
    }
}