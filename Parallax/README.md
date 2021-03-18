## Parallax
Applies Parallax effect to 2D scenes using an orthographic camera

The final setup should look something like this:
- Layers (Game Object with ParallaxController.cs)
  - Layer 1 (Game Object with ParallaxLayer.cs)
  - Layer 2 (Game Object with ParallaxLayer.cs)

### ParallaxController.cs
Drag it to the parent game object containing the layer children

### ParallaxLayer.cs
Drag it to all the layer children game objects

Change the parallax amount at will:
- -1 makes the layer look further
- +1 makes the layer look closer