using System.Runtime.InteropServices.JavaScript;
using System;
using RayTracer;
using System.Threading.Tasks;

public partial class MainJS
{
    struct SceneEnvironment {
        public int Width;
        public int Height;
        public int HardwareConcurrency;
        public byte[] rgbaRenderBuffer;
        public Scene Scene;
    }

    static SceneEnvironment sceneEnvironment;

    public static void Main()
    {
        Console.WriteLine ("Hello, World!");
    }

    internal static Scene ConfigureScene ()
    {
        var scene = Scene.TwoPlanes;
        scene.Camera.ReflectionDepth = 5;
        scene.Camera.FieldOfView = 120;
        return scene;
    }

    [JSExport]
    internal static Task PrepareToRender(int sceneWidth, int sceneHeight, int hardwareConcurrency)
    {
        sceneEnvironment.Width = sceneWidth;
        sceneEnvironment.Height = sceneHeight;
        sceneEnvironment.HardwareConcurrency = hardwareConcurrency;
        sceneEnvironment.Scene = ConfigureScene();
        sceneEnvironment.rgbaRenderBuffer = new byte[sceneWidth * sceneHeight * 4];
        return Task.CompletedTask;
    }

    [JSImport("renderCanvas", "main.js")]
    internal static partial void RenderCanvas([JSMarshalAs<JSType.MemoryView>] ArraySegment<byte> rgbaView);


    [JSImport("setOutText", "main.js")]
    internal static partial void SetOutText(string text);

    [JSExport]
    [return: JSMarshalAs<JSType.Promise<JSType.Void>>]
    internal static async Task OnClick(){
        var now = DateTime.UtcNow;
        string text;
        text = "Rendering started";
        Console.WriteLine(text);
        SetOutText(text);

        await sceneEnvironment.Scene.Camera.RenderScene(sceneEnvironment.Scene, sceneEnvironment.rgbaRenderBuffer, sceneEnvironment.Width, sceneEnvironment.Height, sceneEnvironment.HardwareConcurrency);

        text = $"Rendering finished in {(DateTime.UtcNow - now).TotalMilliseconds} ms";
        Console.WriteLine(text);
        SetOutText(text);
        RenderCanvas(sceneEnvironment.rgbaRenderBuffer);
    }
}
