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

: init-scene
	['] gras add-to-scene \ finish lines
	['] gras add-to-scene
	1 4 rnd-between 0 ?DO \ 1-4 street or watersegments
		0 1 rnd-between 0 = if 
			\ water
			2 4 rnd-between 0 ?DO
				['] water add-to-scene
			LOOP
		else
			\ street
			['] street-white-line add-to-scene
			2 4 rnd-between 0 ?DO
				['] street add-to-scene
			LOOP
			['] street-white-line add-to-scene
		endif
		0 2 rnd-between 0 ?DO
			['] gras add-to-scene
		LOOP
	LOOP
	\ starting line 
	['] gras add-to-scene ;

create scene init-scene

: level-height scene-length @ ;
