using System.Threading.Tasks;

namespace TheBelt
{
    public abstract class BaseAdapter
    {
        public abstract ResultType ResultType { get; }
        public virtual bool Finished { get; protected set; } = false;
        public abstract Task Start();
        public abstract Task<string> GetResult();
    }

    public enum ResultType
    {
        Unknown,
        File,
        Directory,
        Url,
        Value
    }
}
