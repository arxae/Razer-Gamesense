# Razer Gamesense

Intercept calls sent from Razer enabled games and sends them over to Steelseries Engine for output. Essentially, this will show Razer keyboard only effects on Steelseries keyboards.

## Requirements
* Steelseries Engine installed
* Apex M800. Only keyboard to provide separate per-key illumination

## Installation
* Build project
* Copy *RzChromaSDK64.dll* to Windows/System32.
* Copy *razergamesense.lsp* file to the hax0rbindings folder (%ProgramData%\SteelSeries\SteelSeries Engine 3\hax0rBindings)

### Why System32?
Razer enabled games will use the dll in the System32 folder first, this allows us to hijack the calls and divert them. 

## Remove/Disable
If you wish to uninstall/disable, just remove or rename the *RzChromaSDK64.dll* file.

## Limitations/Issues
* Only tested with Overwatch. Not all calls are captured correctly, so other games might be incorrect regarding colorusage
* Still haven't found the correct layout as sent by Razer enabled games.
* There is no real crash handeling, if the game crashes, something went wrong
* Not all keys light up

## Configuration
If a file called *RazerInterceptor.cfg* is pressent in the profile directory (so, %userprofile%/RazerInterceptor.cfg), this will be loaded. Otherwise default settings are loaded. This settings file is a simple key/value file. (eg: key = val)

The key layout can also be override by creating a file called *RazerInterceptorKeyLayout.cfg* file in your profile directory. This file contains HID codes that refer to the keys on your keyboard. Razer sends a list of colors, and this is matched to this list. So color 1 will match the ID entered on line 1. Only a single value per line. *NOTE*: SSE interprets the results as US english keyboard layout, but windows *does* translates it to the correct keyboard settings.

### Available settings
* emitlogs (default: false). Emit logs to the user profile directory. **WARNING** This will tank your FPS.