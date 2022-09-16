using System.Runtime.InteropServices.JavaScript;
using System;
using RayTracer;
using System.Threading.Tasks;

public partial class MainJS
{

    public static void Main()
    {
        Console.WriteLine ("Hello, World!");
    }

    [JSImport("renderCanvas", "main.js")]
    internal static partial void RenderCanvas([JSMarshalAs<JSType.MemoryView>] ArraySegment<byte> rgba);

    [JSExport]
    [return: JSMarshalAs<JSType.Promise<JSType.Void>>]
    internal static async Task OnClick(){
        var now = DateTime.UtcNow;
        Console.WriteLine ("Rendering started");

        Scene scene = Scene.TwoPlanes;
        const int width = 640;
        const int height = 480;
        scene.Camera.ReflectionDepth = 5;
        scene.Camera.FieldOfView = 120;
        byte[] canvasRGBA = new byte[width * height * 4];
        await scene.Camera.RenderScene(scene, canvasRGBA, width, height);

        Console.WriteLine ("Rendering finished in "+ (DateTime.UtcNow - now).TotalMilliseconds+ " ms");
        RenderCanvas(canvasRGBA);
    }
}
