// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

import { dotnet } from './dotnet.js'

function drawWaitingForRendering(canvas) {
    const ctx = canvas.getContext('2d');
    ctx.save();
    ctx.fillStyle = "rgba(255,255,255,0.7)";
    ctx.fillRect(0, 0, canvas.width, canvas.height);
    ctx.restore();
    ctx.save();
    ctx.font = "bold 50px Arial, sans-serif";
    ctx.fillStyle = "darkred";
    ctx.textAlign = "center";
    ctx.fillText("Rendering...", canvas.width / 2, canvas.height / 2);
    ctx.restore();
}

function renderCanvas() {
    const ctx = canvas.getContext('2d');
    const clamped = new Uint8ClampedArray(rgbaView.slice());
    const image = new ImageData(clamped, canvas.width, canvas.height);
    ctx.putImageData(image, 0, 0);
    canvas.style = "";
}

function setOutText(text) {
    outText.innerText = text;
}

async function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet.create();
setModuleImports("main.js", { renderCanvas, setOutText });
const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);
const canvas = document.getElementById("out");
const outText = document.getElementById("outText");
const btnRender = document.getElementById("btnRender");

globalThis.onClick = async function () {
    btnRender.disabled = true;
    drawWaitingForRendering(canvas);
    await delay(10);
    await exports.MainJS.OnClick();
    btnRender.disabled = false;
}

await dotnet.run();
const rgbaView = exports.MainJS.PrepareToRender(canvas.width, canvas.height);
btnRender.disabled = false;

// when done, call rgbaView.dispose();
