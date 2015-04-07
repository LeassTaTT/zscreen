

# Introduction #

You can have two watermark types: Text or Image.

# Text #
## Text Background Settings ##
### Gradient ###
#### Simple ####
#### Advanced ####
Supported color formats:

  * RGB: 255,0,128
  * ARGB: 255,255,0,128
  * Hex (RGB or ARGB): #FF0080 or #FFFF0080
  * Color name: Red, Blue etc.

Offset needs to be within 0.0 and 1.0

You will need at least two colors configured for the gradient to work.
Each line represents all the settings required for a particular color.

Example: showing 4 colors configured in 4 different formats

```
White          0
128,50,128     0.3
#00FF1E        0.7
255,0,150,200  1
```

produces:

![http://i38.tinypic.com/2pq259y.png](http://i38.tinypic.com/2pq259y.png)