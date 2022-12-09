using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console.UI.Exercises.Intermediate
{

    public class Stopwatch
    {
        private DateTime? startTime { get; set; }


        public void Start()
        {
            if (startTime.HasValue)
                throw new InvalidOperationException("The stowatch has already started; stop first.");

            startTime = DateTime.Now;

        }
        public TimeSpan Stop()
        {
            if (!startTime.HasValue)
                throw new InvalidOperationException("The stopwatch must start before stopping.");

            TimeSpan duration = DateTime.Now - startTime.Value;
            startTime = null;
            return duration;
        }

    }

    public class Post
    {

        public string title { get; set; }
        public string description { get; set; }

        public DateTime createdDate { get; private set; }

        public int voteCount { get; private set; }

        public Post()
        {
            createdDate = DateTime.Now;
            voteCount = 0;
        }

        public void UpVote()
        {
            voteCount++;
        }
        public void DownVote()
        {
            voteCount--;
        }
    }

    public abstract class DbConnection
    {
        public string ConnectionString { get; set; }

        readonly public TimeSpan Timeout = new TimeSpan(0, 0, 5);

        public DbConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Invalid connection string.");
            }

            ConnectionString = connectionString;
        }

        public abstract void OpenConnection();
        public abstract void CloseConnection();
    }
    public class SqlConnection : DbConnection
    {
        public SqlConnection(string connectionString) : base(connectionString)
        {
        }

        public override void CloseConnection()
        {
            System.Console.WriteLine($"Closing Connection: {ConnectionString}");
        }

        public override void OpenConnection()
        {
            DateTime startConnTime = DateTime.Now;
            if (Timeout < (DateTime.Now - startConnTime))
                throw new TimeoutException($"{this} timeout");

            System.Console.WriteLine($"Opening Connection: {ConnectionString}");

        }
    }
    public class OracleConnection : DbConnection
    {
        public OracleConnection(string connectionString) : base(connectionString)
        {
        }

        public override void CloseConnection()
        {
            System.Console.WriteLine($"Closing Connection: {ConnectionString}");
        }

        public override void OpenConnection()
        {
            DateTime startConnTime = DateTime.Now;
            if (Timeout < (DateTime.Now - startConnTime))
                throw new TimeoutException($"{this} timeout");

            System.Console.WriteLine($"Opening Connection: {ConnectionString}");
        }
    }

    public class DbCommand
    {
        public DbConnection DbConnection { get; set; }
        public string DbCommandInstruction { get; set; }

        public DbCommand(DbConnection dbConnection, string dbCommandInstruction)
        {
            if (string.IsNullOrWhiteSpace(dbCommandInstruction))
            {
                throw new ArgumentException("Invalid db command instruction.");
            }

            this.DbConnection = dbConnection ?? throw new ArgumentException("Invalid connection string.");
            this.DbCommandInstruction = dbCommandInstruction;
        }

        public void Execute()
        {
            DbConnection.OpenConnection();

            System.Console.WriteLine($"Executing Instructions: {DbCommandInstruction}");

            DbConnection.CloseConnection();

        }

    }
    public class RunNormal : ITask
    {
        public void Execute()
        {

            System.Console.WriteLine("Executing RunNormal");
        }
    }
    public class RunFast : ITask
    {
        public void Execute()
        {

            System.Console.WriteLine("Executing RunFast");
        }
    }
    public class WalkNormal : ITask
    {
        public void Execute()
        {

            System.Console.WriteLine("Executing WalkNormal");
        }
    }
    public class WalkSlow : ITask
    {
        public void Execute()
        {

            System.Console.WriteLine("Executing WalkSlow");
        }
    }
    public class WorkFlow : IWorkFlow
    {
        private readonly IList<ITask> _tasks;

        public WorkFlow()
        {
            _tasks = new List<ITask>();
        }

        public void Add(ITask task)
        {
            _tasks.Add(task);
        }

        public void Remove(ITask task)
        {
            _tasks.Remove(task);
        }

        public IEnumerable<ITask> GetTasks()
        {
            return _tasks;
        }

    }
    public class WorkflowEngine
    {
        public void Run(IWorkFlow workflow)
        {
            foreach (ITask w in workflow.GetTasks())
            {
                try
                {
                    w.Execute();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}