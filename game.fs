require settings.fs
require graphics.fs

create collision_ary width height * cells allot
create frog_pos width 2 / , 17 ,

char w constant up_key
char a constant left_key
char s constant down_key
char d constant right_key
char q constant quit_key

: draw-scene ( -- )
    page
    draw-water-line
    draw-water-line
    draw-water-line
    draw-water-line
    draw-gras-line
    draw-gras-line
    draw-white-line
    draw-street
    draw-white-line
    draw-gras-line
    draw-gras-line
    draw-gras-line
    draw-gras-line
    draw-gras-line ;


: draw-frog1 ( -- )
	frog_pos @ frog_pos cell+ @
	at-xy ." #" ;

: set-coll-ary { n1 n2 flag -- }
	collision_ary width n2 * n1 + cells + flag swap ! ;

: fetch-collision-ary-value { n1 n2 -- flag }
	collision_ary width n2 * n1 + cells + @ ;

: fetch-frog-pos ( -- n1 n2 )
	frog_pos @ frog_pos cell+ @ ;

: init-water-collision ( -- )
	4 0 u+do
		width 0 u+do
			i j true set-coll-ary
		loop
	loop ;

: draw-car1 { n1 n2 -- }
	8 0 u+do
		n1 i + n2 true set-coll-ary
	loop
	n1 n2 at-xy draw-car ;

: draw-cars ( -- )
	0 7 draw-car1 
	10 8 draw-car1
	20 10 draw-car1
	5 11 draw-car1
	;

: draw-log { n1 n2 -- }
	8 0 u+do
		n1 i + n2 false set-coll-ary
	loop
	n1 n2 at-xy draw-car ;

: draw-logs ( -- )
	0 1 draw-log 
	10 2 draw-log
	20 0 draw-log
	5 3 draw-log
	;

: check_collision ( -- n)
	fetch-frog-pos fetch-collision-ary-value ;

: end-game
    1 height 2 - at-xy
    reset-colors cr
    bye ;

: move-frog { n1 n2 -- }
	frog_pos @ frog_pos cell+ @ at-xy black-bg draw-empty-space
	frog_pos cell n1 * + dup @ n2 + swap ! 
	;

: handle-key ( key -- )
	case key 
		up_key    of 1 -1 move-frog endof
		down_key  of 1 1 move-frog endof
		left_key  of 0 -1 move-frog endof
		right_key of 0 1 move-frog endof
		quit_key  of end-game endof
	endcase ;

: move-car { n1 n2 -- }
	\ missing col-ary update
	n1 width mod n2 at-xy draw-car ;

: move-cars { n -- n1 }
	0 7 at-xy draw-street

	width n - 7 move-car
	width n 3 * - 8 move-car
	n 2 * 10 move-car
	n 11 move-car
	
	n 1 + width mod ;

: start-game
	draw-scene
	." controls: WASD to move frog; q to quit game"
	
	init-water-collision
	draw-cars
	draw-logs	

	0 begin
		50 ms
		
		key? if handle-key endif		
		\ move-cars
		draw-frog1	
		
		check_collision
		
	until
		." GAME OVER"
		end-game ; 
	
