# Port of good old ray-tracing demo

Original code at: https://github.com/microsoft/dotnet-samples/tree/master/System.Numerics/SIMD/RayTracer

With Net7 RC1 or later do:
```
dotnet workload install wasm-tools
dotnet publish -c Release
dotnet serve --directory bin\Release\net7.0\browser-wasm\AppBundle\
```

Look at the instructions
```
dotnet tool install --global wa-info
wa-info -d bin\Release\net7.0\browser-wasm\AppBundle\dotnet.wasm -f GetRefractionRay
```

Live demo here https://pavelsavara.github.io/dotnet-wasm-raytracer/