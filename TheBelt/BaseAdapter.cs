using System.Threading.Tasks;

namespace TheBelt
{
    public abstract class BaseAdapter
    {
        public virtual bool Finished { get; protected set; } = false;
        public abstract Task Start();
        public abstract Task<string> GetResult();
    }
}
