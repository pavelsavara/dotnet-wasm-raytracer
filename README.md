# Port of good old ray-tracing demo

This demo requires a browser with SIMD support and Web Worker support

Original code at: https://github.com/microsoft/dotnet-samples/tree/master/System.Numerics/SIMD/RayTracer

With Net7 RC2 or later do:
```
dotnet workload install wasm-experimental
dotnet publish -c Release
dotnet serve -h "Cross-Origin-Opener-Policy:same-origin" -h "Cross-Origin-Embedder-Policy:require-corp" --directory bin/Release/net7.0/browser-wasm/AppBundle/
```

Look at the instructions
```
dotnet tool install --global wa-info
wa-info -d bin\Release\net7.0\browser-wasm\AppBundle\dotnet.wasm -f GetRefractionRay
```

Live demo here https://lambdageek.github.io/dotnet-wasm-raytracer/


