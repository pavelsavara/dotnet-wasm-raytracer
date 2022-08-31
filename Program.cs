using System.Runtime.InteropServices.JavaScript;
using System;

public partial class MainJS
{

    public static void Main()
    {
        Console.WriteLine ("Hello, World!");
    }

    [JSImport("renderCanvas", "main.js")]
    internal static partial void RenderCanvas([JSMarshalAs<JSType.MemoryView>] ArraySegment<byte> rgba);

    [JSExport]
    internal static void OnClick(){
        var now = DateTime.UtcNow;
        Console.WriteLine ("Rendering started");
        RayTracer tracer = new RayTracer(300, 300);
        var canvasRGBA = tracer.Render(tracer.DefaultScene);
        Console.WriteLine ("Rendering finished in "+ (DateTime.UtcNow - now).TotalMilliseconds+ " ms");
        RenderCanvas(canvasRGBA);
    }
}