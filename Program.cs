using System.Runtime.InteropServices.JavaScript;
using System;
using RayTracer;
using System.Threading;
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
	var c = SynchronizationContext.Current?.GetType().ToString() ?? "<none>";
	Console.WriteLine ($" Synchronization context on main thread is {c}");
        var now = DateTime.UtcNow;
        Console.WriteLine ("Rendering started");

        Scene scene = Scene.TwoPlanes;
        scene.Camera.ReflectionDepth = 5;
        scene.Camera.FieldOfView = 120;
        var canvasRGBA = await scene.Camera.RenderScene(scene, 640, 480);

        Console.WriteLine ("Rendering finished in "+ (DateTime.UtcNow - now).TotalMilliseconds+ " ms");
        RenderCanvas(canvasRGBA);
    }
}
