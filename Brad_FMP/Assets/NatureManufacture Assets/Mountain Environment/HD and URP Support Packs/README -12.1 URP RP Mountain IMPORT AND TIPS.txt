
BEFORE YOU START:
- you need Unity 2021.3+
- you need URP SRP pipline 12.1 if you use higher please import 12.1 support pack.
- wind setup is in wind prefab at each scene
Be patient URP RP tech is still fluid and fresh...

Step 1 - Setup Shadows and other render setups. Find File "UniversalRP-HighQuality" 
    - Change shadow distance to 300 or higer
	- Turn on "Opaque Texture" this will fix water translucency and distortion
	- Turn on "Depth Texture" this will fix water visibility at playmode
	- Optionaly use 1k or 2k shadow resolution. We used 2k.
	- Turn on HDR if its turned off

Step 2 Go to project settings: 
    - Player and set:  Color Space to Linear
    - Quality settings: Go to quality settings and: 
	     * use ultra level 
	     * turn turn off vsync
		 * lod bias should be around 1.5-2 and 1 for low end devices.
                        

Step 3 Find "URP Mountain Scene" and open it.

Step 4 - chose way of movment. Movie track or free movment.
	Chose camera and turn on or off "playable directior" and "animation" or leave free camera movment turned on.

Step 5 - HIT PLAY!:)

Step 6 -  Make note that unity often compile shaders even after you hit play for long time, so performance will rise up after unity end shader compilation
Wait a moment until it end. 
At urp and hd rp seams unity SRP batched batches are not counted as saved by batching so they can lead to misunderstanding.
At unity standard render scene got 1500 batches and rest is saved by batching. 
At hd and urp there is alot more batches in counter but unity don't show how much of them are batched by srp batcher.
In the past unity count srp batches into save by batching now it's not. We use srp batcher in hd and urp scenes so stats value can lead to misunderstanding. 


About scene construction:
		- There is post process profile: Manage post process by scene post process object.
		- Prefab wind manage wind speed and direction at the scene

