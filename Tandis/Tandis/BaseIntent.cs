namespace Tandis
{
    public interface IIntent
    {
        bool HasExtra(string name);
        string GetExtra(string name);
    }
}
