using System;

public delegate void NameChangeEventHandler(object sender, NameChangeEventArgs args);
public class NameChangeEventArgs : EventArgs
{
    public string Name { get; private set; }

    public NameChangeEventArgs(string name)
    {
        this.Name = name;
    }
}
public class Dispatcher
{
    private string name;
    
    public event NameChangeEventHandler NameChange;
    
    public string Name
    {
        get { return this.name; }
        set
        {
            if (this.name != value)
            {
                this.name = value;
                OnNameChange(new NameChangeEventArgs(value));
            }
        }
    }
    
    protected virtual void OnNameChange(NameChangeEventArgs args)
    {
        if (this.NameChange != null)
        {
            this.NameChange(this, args);
        }
    }
}

public class Handler
{
    public void OnDispatcherNameChange(object sender, NameChangeEventArgs args)
    {
        Console.WriteLine($"Dispatcher's name changed to {args.Name}.");
    }
}
public class Program
{
    public static void Main()
    {
        Dispatcher dispatcher = new Dispatcher();
        Handler handler = new Handler();
        
        dispatcher.NameChange += handler.OnDispatcherNameChange;
        
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            dispatcher.Name = input;
        }
    }
}
