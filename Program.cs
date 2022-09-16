using System.Runtime.InteropServices.JavaScript;
using System;
using RayTracer;
using System.Threading;
using System.Threading.Tasks;

public partial class MainJS
{
    struct SceneEnvironment {
        public int Width;
        public int Height;
        public byte[] rgbaRenderBuffer;
    }

    static SceneEnvironment sceneEnvironment;

    public static void Main()
    {
        Console.WriteLine ("Hello, World!");
    }

    [JSExport]
    [return: JSMarshalAs<JSType.MemoryView>]
    internal static ArraySegment<byte> PrepareToRender(int sceneWidth, int sceneHeight)
    {
        sceneEnvironment.Width = sceneWidth;
        sceneEnvironment.Height = sceneHeight;
        sceneEnvironment.rgbaRenderBuffer = new byte[sceneWidth * sceneHeight * 4];
        return sceneEnvironment.rgbaRenderBuffer;
    }

    [JSImport("renderCanvas", "main.js")]
    internal static partial void RenderCanvas();

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
        scene.Camera.ReflectionDepth = 5;
        scene.Camera.FieldOfView = 120;
        await scene.Camera.RenderScene(scene, sceneEnvironment.rgbaRenderBuffer, sceneEnvironment.Width, sceneEnvironment.Height);

        text = $"Rendering finished in {(DateTime.UtcNow - now).TotalMilliseconds} ms";
        Console.WriteLine(text);
        SetOutText(text);
        RenderCanvas();
    }
}
