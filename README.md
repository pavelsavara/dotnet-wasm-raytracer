# Port of good old ray-tracing demo

Original code at: https://github.com/microsoft/dotnet-samples/tree/master/System.Numerics/SIMD/RayTracer

With Net7 RC1 or later do:
```
dotnet workload install wasm-tools
dotnet publish -c Release
dotnet serve -h "Cross-Origin-Opener-Policy:same-origin" -h "Cross-Origin-Embedder-Policy:require-corp" --directory bin/Release/net7.0/browser-wasm/AppBundle/
```

Live demo here https://lambdageek.github.io/dotnet-wasm-raytracer/

