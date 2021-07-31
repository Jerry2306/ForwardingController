using System.Threading.Tasks;

namespace SharedItems.Helper
{
    public class TaskAwaiter
    {
        public static T WaitForResult<T>(Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}