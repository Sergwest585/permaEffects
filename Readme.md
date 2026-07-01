#permaEffects - Tshock plugin that give permament effects to player who has more than 30 potions in any inventory

##It is support:
All effects potions, All food (And all stuff, that has .buffType) and
Ammo Box, Bewitching, Sharpening, War Table, Crystal Ball

#Install:
put `permaEffects.dll` in your tshock plugin folder

#build:
download all needed dlls outside project to `dlls/` folder:
-permaEffects/
-dlls/
--TShockAPI.dll
--TerrariaServer.dll
--OTAPI.dll

after that:
```dotnet build```
(make sure you have net9.0)

#Thanks:
[Permabuffs](https://github.com/SyntaxVoid/Permabuffs/) - much better than my plugin in controll, but all potions are hardcoded, no ammo box and other stuff, and I dont like use DB for such project