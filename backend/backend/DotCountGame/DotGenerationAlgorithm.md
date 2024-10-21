# Fill algorithm

## Chunks

Canvas is made of pixel chunks.
All chunks are a square with the side lenght equal to the dot diameter.

Chunks can be:
- *free* (dot center WILL NOT be placed in them)
- *occupiable* (dot center CAN BE placed in them)

The if chunk is selected to have a dot center inside,
the precise position of that center is calculated as follows:

> final_position = chunk_top_left_corner + random_offset


**Note**: *free* chunks CAN (and in a lot of cases will) contain parts of a dot.

## Chunk pattern
Chunk pattern goes as follows:

```
---------
-*-*-*-*-
---------
-*-*-*-*-
---------
-*-*-*-*-
---------
-*-*-*-*-
---------
```
Here `-` marks *free* chunks and `*` marks *occupiable* chunks.

