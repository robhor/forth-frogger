require settings.fs
require graphics.fs
require rnd.fs

\ helper to create enums
: enum
  create 0 ,
  does> dup @ 1 rot +! constant
;

\ define obstacle type
enum entity
entity none
entity cars

-1 constant left
 1 constant right

: water ( -- draw-one draw-xt passable obstacles )
    ['] draw-water
    ['] draw-water-line
    false cars ;

: gras ( -- draw-one draw-xt passable obstacles )
    ['] draw-gras
    ['] draw-gras-line
    true none ;

: street ( -- draw-one draw-xt passable obstacles )
    ['] draw-street
    ['] draw-street-line
    true cars ;

: street-white-line ( -- draw-one draw-xt passable obstacles )
    ['] draw-white
    ['] draw-white-line
    true none ;

: street-yellow-line ( -- draw-one draw-xt passable obstacles )
    ['] draw-yellow
    ['] draw-yellow-line
    true none ;

variable scene-length
0 scene-length !

variable cars-length 0 cars-length !

0 constant top-offset

: add-to-scene ( ex -- )
    , scene-length dup @ 1+ swap ! ;

create scene
' gras add-to-scene
' gras add-to-scene
' water add-to-scene
' water add-to-scene
' water add-to-scene
' gras add-to-scene
' street-white-line add-to-scene
' street add-to-scene
' street add-to-scene
' street add-to-scene
' street add-to-scene
' street-yellow-line add-to-scene
' street add-to-scene
' street add-to-scene
' street add-to-scene
' street add-to-scene
' street-yellow-line add-to-scene
' street add-to-scene
' street add-to-scene
' street-white-line add-to-scene
' gras add-to-scene
' gras add-to-scene

: level-height scene-length @ ;
