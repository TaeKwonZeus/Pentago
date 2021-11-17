namespace Engine
{
    public interface IEvaluation
    {
        double? Evaluation { get; }
        
        int? MateIn { get; }

        string AsString();
    }
}