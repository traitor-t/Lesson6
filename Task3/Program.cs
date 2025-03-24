public delegate void TemperatureChangedEventHandler(object sender, TemperatureChangedEventArgs e);
public class TemperatureChangedEventArgs : EventArgs
{
    public float NewTemperature { get; }
    public TemperatureChangedEventArgs(float newTemperature)
    {
        NewTemperature = newTemperature;
    }
}

public class TemperatureSensor
{
    private float _currentTemperature;
    public event TemperatureChangedEventHandler TemperatureChanged;
    public void SetTemperature(float newTemperature)
    {
        if (_currentTemperature != newTemperature)
        {
            _currentTemperature = newTemperature;
            OnTemperatureChanged(newTemperature);
        }
    }
    protected virtual void OnTemperatureChanged(float newTemperature)
    {
        TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(newTemperature));
    }
}

public class CoolingSystem
{
    public void OnTemperatureChanged(object sender, TemperatureChangedEventArgs e)
    {
        if (e.NewTemperature > 25.0f)
        {
            Console.WriteLine("Система охлаждения: Включение кондиционера");
        }
    }
}

public class WarningSystem
{
    public void OnTemperatureChanged(Object sender, TemperatureChangedEventArgs e)
    {
        if(e.NewTemperature > 40.0f)
        {
            Console.WriteLine("Система предупреждения: Предупреждение! Перегрев!");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TemperatureSensor sensor = new TemperatureSensor();
        CoolingSystem coolingSystem = new CoolingSystem();
        WarningSystem warningSystem = new WarningSystem();

        sensor.TemperatureChanged += coolingSystem.OnTemperatureChanged;
        sensor.TemperatureChanged += warningSystem.OnTemperatureChanged;

        sensor.SetTemperature(22.0f);
        sensor.SetTemperature(26.0f);
        sensor.SetTemperature(45.0f);

    }
}