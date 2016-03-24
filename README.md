# NailClipr
Tool written in C# for FFXI.

![](http://puu.sh/nRNpP/5a25a77464.png)

#### Position
`+ | -` 

Nudge player forward/backward on X/Y/Z axis.

#### Checkboxes
`Maintenance Mode` 

Allows walking through walls. Constantly rewritten to immediately counteract server updating status back to normal.

`Player Detection` 

Reverts from bottom speed slider to top speed slider when a player is rendered in user's memory.
`Stay on Top` 

Form control.

`Save Settings` 

Checkboxes and default speed are saved into `/resources/Settings.xml`.

#### Warp
`Save` 

Saves current position and zone id into `Settings.xml` with name from dropdown. If input name exists in dropdown or existing name is selected, the warp point will be updated in `Settings.xml`.

`Delete` 

...

`Request` 

Sends coordinates and zone id to chat to request party teleportation.

![](http://puu.sh/nRNSv/49abd4c826.jpg)

`Accept` 

Enabled upon request receipt. Disabled upon zone. This prevents players from teleporting to areas outside of map range.


#### To Do
1. Echo saves/deletes into chat.
2. Control bot via windower commands (functionality yet not supported).
3. Integrate a whitelist for Player Detection feature.
4. Add screenshots.
