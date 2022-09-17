# Port of good old ray-tracing demo


This demo requires a browser with SIMD support

Original code at: https://github.com/microsoft/dotnet-samples/tree/master/System.Numerics/SIMD/RayTracer

## Single threaded

With .NET 7 Release Candidate 1 or later do:

```
dotnet workload install wasm-tools
dotnet publish -c Release
dotnet serve --directory bin/Release/net7.0/browser-wasm/AppBundle/
```

## Multi-threaded

With .NET 7 Release Candidate 2 or later do:

```
dotnet workload install wasm-experimental
dotnet publish -c Release -p:WasmEnableThreads=true
dotnet serve -h "Cross-Origin-Opener-Policy:same-origin" -h "Cross-Origin-Embedder-Policy:require-corp" --directory bin/Release/net7.0/browser-wasm/AppBundle/
```

For publishing we bundle https://github.com/gzuidhof/coi-serviceworker to enable COOP/COEP headers on hosts that don't serve those headers (such as Github Pages).

## Inspecting the wasm code

Look at the instructions using the `wa-info` tool

```
dotnet tool install --global wa-info
dotnet publish -c Release -p:WasmNativeStrip=false
wa-info -d bin\Release\net7.0\browser-wasm\AppBundle\dotnet.wasm -f GetRefractionRay
```

Live demo here https://pavelsavara.github.io/dotnet-wasm-raytracer/
