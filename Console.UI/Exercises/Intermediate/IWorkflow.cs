using System.Collections.Generic;

namespace Console.UI.Exercises.Intermediate
{
    public interface ITask
    {
        void Execute();
    }


    public interface IWorkFlow
    {
        void Add(ITask task);
        void Remove(ITask task);

        IEnumerable<ITask> GetTasks();
    }
}