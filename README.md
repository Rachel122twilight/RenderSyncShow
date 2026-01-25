# RenderSyncShow
Synchronize the Game view within your UnityEditor to other devices.

## Table of Contents
 - [Getting started](#getting-started)

Getting started
---
Install via [UPM Package](#upm-package) with git reference or asset package (RenderSyncShow.unitypackage) available in [releases](https://github.com/Rachel122twilight/RenderSyncShow/releases/).

In the menu bar, you can find a menu named "RenderSyncShow".

After expanding the menu, click Settings to set the server address and port to sync.

The preparations are complete. Now, when UnityEditor starts Play, it will output the relevant information and sync it to the specified address and port. You can enter this information in the accompanying application to view the scene.

Install via the UPM Package 
---
Requires a version of unity that supports path query parameter for git packages (Unity >= 2019.3.4f1, Unity >= 2020.1a21).
You can add `https://github.com/Rachel122twilight/RenderSyncShow.git?path=Plugins/RenderSyncShow` to Package Manager

or add `com.Rachel122twilight.RenderSyncShow`: `https://github.com/Rachel122twilight/RenderSyncShow.git?path=/Plugins/RenderSyncShow` to Packages/manifest.json.
