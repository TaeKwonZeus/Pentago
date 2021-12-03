using System.Data.SqlClient;

namespace Pentago.Engine
{
    public interface IEngine
    {
        public Evaluation Evaluate(Board position);

        public Move BestMove(Board position);

        public static IEngine Instance(string connectionString) => null;
    }
}