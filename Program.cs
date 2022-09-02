using System.Runtime.InteropServices.JavaScript;
using System;
using RayTracer;

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

        Scene scene = Scene.TwoPlanes;
        scene.Camera.ReflectionDepth = 5;
        scene.Camera.FieldOfView = 120;
        var canvasRGBA = scene.Camera.RenderScene(scene, 640, 480);

        Console.WriteLine ("Rendering finished in "+ (DateTime.UtcNow - now).TotalMilliseconds+ " ms");
        RenderCanvas(canvasRGBA);
    }
}