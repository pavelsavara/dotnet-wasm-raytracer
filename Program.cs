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

    [JSImport("setOutText", "main.js")]
    internal static partial void SetOutText(string text);

    [JSExport]
    [return: JSMarshalAs<JSType.Promise<JSType.Void>>]
    internal static async Task OnClick(){
	var c = SynchronizationContext.Current?.GetType().ToString() ?? "<none>";
	Console.WriteLine ($" Synchronization context on main thread is {c}");
        var now = DateTime.UtcNow;
        string text;
        text = "Rendering started";
        Console.WriteLine(text);
        SetOutText(text);

        Scene scene = Scene.TwoPlanes;
        const int width = 640;
        const int height = 480;
        scene.Camera.ReflectionDepth = 5;
        scene.Camera.FieldOfView = 120;
        byte[] canvasRGBA = new byte[width * height * 4];
        await scene.Camera.RenderScene(scene, canvasRGBA, width, height);

        text = $"Rendering finished in {(DateTime.UtcNow - now).TotalMilliseconds} ms";
        Console.WriteLine(text);
        SetOutText(text);
        RenderCanvas(canvasRGBA);
    }
}
