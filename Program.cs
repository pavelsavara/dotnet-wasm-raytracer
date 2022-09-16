using System.Runtime.InteropServices.JavaScript;
using System;
using RayTracer;
using System.Threading.Tasks;

public partial class MainJS
{
    struct SceneEnvironment {
        public int Width;
        public int Height;
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
    [return: JSMarshalAs<JSType.MemoryView>]
    internal static ArraySegment<byte> PrepareToRender(int sceneWidth, int sceneHeight)
    {
        sceneEnvironment.Width = sceneWidth;
        sceneEnvironment.Height = sceneHeight;
        sceneEnvironment.Scene = ConfigureScene();
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
        var now = DateTime.UtcNow;
        string text;
        text = "Rendering started";
        //Console.WriteLine(text);
        SetOutText(text);

        await sceneEnvironment.Scene.Camera.RenderScene(sceneEnvironment.Scene, sceneEnvironment.rgbaRenderBuffer, sceneEnvironment.Width, sceneEnvironment.Height);

        text = $"Rendering finished in {(DateTime.UtcNow - now).TotalMilliseconds} ms";
        //Console.WriteLine(text);
        SetOutText(text);
        RenderCanvas();
    }
}
