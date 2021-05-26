/*
 * Just another document to communicate with each other.
 * 
 * I don't think levels will be wider than the current camera width, but they may be taller
 * 
 * I made a bubble prefab. It's under Assets/prefabs. Just drag it into the scene to make more bubbles. You can change the speed and scale in the inspector
 * 
 * Borders are set to be 0.6 units away from the edge of the camera. There is also a border prefab
 * 
 * Camera now has a min and max value. Set those equal to the bottom and top of the levels respectively. Top of all levels should be zero.
 * 
 * Added a Level Loader. This will be a prefab. It will be placed at the bottom of the level, just below the camera's minPosition (offset by 0.6 works well). In the inspector you can set the index
 * of the level it will load. NOTE: indexes start at zero! So the first level (named Level 0) has an index of zero. 
 * 
 * A good pixels/unit size for sprites seems to be 650
 * 
 * 
 * Due to me not knowing how to program better, the trident now has field called isThrown. The box next to it should never be checked!
 * 
 * Currently, I'm keeping the range for bubble speed at 0.5-2
 * 
 * 
 * All levels should start with the following: Background, level loader, and border. It may be possible to make a template that includes all of these, but I don't know how to do that yet.
 * 
 *Current Controls:
 *      A = Left
 *      S = Right
 *      Space = Swim Up
 *      Right Mouse Click = Throw Trident
*/