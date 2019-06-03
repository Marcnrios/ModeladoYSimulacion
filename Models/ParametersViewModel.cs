using System.Collections.Generic;

public class ParametersViewModel
{
    public ParametersViewModel()
    {
        var initialize = new List<Point>();
        initialize.Add(new Point() { x = 0, y = 0 });
    }
    public float x { get; set; }
    public float t { get; set; }
    public float xt { get; set; }
    public float a { get; set; }
    public float b { get; set; }
    public float h { get; set; }
    public string function { get; set; }
    public List<Point> EulerMejoradoPoints { get; set; }
    public List<Point> EulerPoints { get; set; }
}