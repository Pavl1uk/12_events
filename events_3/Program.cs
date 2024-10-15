using System;
using System.Collections.Generic;

public interface IEmployee
{
    string Name { get; }
    int WorkHoursPerWeek { get; }
}

public class StandardEmployee : IEmployee
{
    public string Name { get; private set; }
    public int WorkHoursPerWeek => 40;

    public StandardEmployee(string name)
    {
        this.Name = name;
    }
}

public class PartTimeEmployee : IEmployee
{
    public string Name { get; private set; }
    public int WorkHoursPerWeek => 20;

    public PartTimeEmployee(string name)
    {
        this.Name = name;
    }
}

public class Job
{
    public string JobName { get; private set; }
    public int HoursOfWorkRequired { get; private set; }
    public IEmployee Employee { get; private set; }

    public event EventHandler JobCompleted;

    public Job(string jobName, int hoursOfWorkRequired, IEmployee employee)
    {
        this.JobName = jobName;
        this.HoursOfWorkRequired = hoursOfWorkRequired;
        this.Employee = employee;
    }

    public void Update()
    {
        this.HoursOfWorkRequired -= this.Employee.WorkHoursPerWeek;
        if (this.HoursOfWorkRequired <= 0)
        {
            Console.WriteLine($"Job {this.JobName} done!");
            OnJobCompleted();
        }
    }

    protected virtual void OnJobCompleted()
    {
        JobCompleted?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return $"Job: {this.JobName} Hours Remaining: {this.HoursOfWorkRequired}";
    }
}

public class JobCollection : List<Job>
{
    public void OnJobCompleted(object sender, EventArgs e)
    {
        var job = sender as Job;
        if (job != null)
        {
            this.Remove(job);
        }
    }

    public void UpdateJobs()
    {
        foreach (var job in this.ToList())
        {
            job.Update();
        }
    }

    public void PrintStatus()
    {
        foreach (var job in this)
        {
            Console.WriteLine(job.ToString());
        }
    }
}

public class Program
{
    public static void Main()
    {
        var employees = new Dictionary<string, IEmployee>();
        var jobs = new JobCollection();

        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            var tokens = input.Split();

            if (tokens[0] == "StandardEmployee")
            {
                string name = tokens[1];
                employees[name] = new StandardEmployee(name);
            }
            else if (tokens[0] == "PartTimeEmployee")
            {
                string name = tokens[1];
                employees[name] = new PartTimeEmployee(name);
            }
            else if (tokens[0] == "Job")
            {
                string jobName = tokens[1];
                int hoursOfWorkRequired = int.Parse(tokens[2]);
                string employeeName = tokens[3];

                if (employees.ContainsKey(employeeName))
                {
                    var employee = employees[employeeName];
                    var job = new Job(jobName, hoursOfWorkRequired, employee);
                    job.JobCompleted += jobs.OnJobCompleted;
                    jobs.Add(job);
                }
            }
            else if (tokens[0] == "Pass")
            {
                if (tokens[1] == "Week")
                {
                    jobs.UpdateJobs();
                }
            }
            else if (tokens[0] == "Status")
            {
                jobs.PrintStatus();
            }
        }
    }
}
