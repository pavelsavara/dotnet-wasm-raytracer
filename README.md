# Port of good old ray-tracing demo

Original credits to: http://www.nokola.com/Raytracer/HowToBuild.aspx

With Net7 RC1 or later do:
```
dotnet workload install wasm-tools
dotnet publish -c Release
dotnet serve --directory bin\Release\net7.0\browser-wasm\AppBundle\
```

Live demo here https://pavelsavara.github.io/dotnet-wasm-raytracer/