public class ResponseModel
{
    public MainData Main { get; set; }
    public WeatherData[] Weather { get; set; }
    public WindData Wind { get; set; }
    public string Name { get; set; }
}

public class MainData
{
    public double Temp { get; set; }
    public double Humidity { get; set; }
    public double Feels_Like { get; set; }
}

public class WeatherData
{
    public string Description { get; set; }
}

public class WindData
{
    public double Speed { get; set; }
}