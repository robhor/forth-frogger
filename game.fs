require settings.fs
require graphics.fs

char w constant up_key
char a constant left_key
char s constant down_key
char d constant right_key
char q constant quit_key

18 constant level_height 

create collision_ary width height * cells allot
create frog_pos width 2 / , level_height ,

: set-collision-ary-value { n1 n2 flag -- }
	collision_ary width n2 * n1 + cells + flag swap ! ;

: get-collision-ary-value { n1 n2 -- flag }
	collision_ary width n2 * n1 + cells + @ ;

: get-frog-pos ( -- n1 n2 )
	frog_pos @ frog_pos cell+ @ ;

: init-water-collision ( -- )
	5 1 u+do
		width 0 u+do
			i j true set-collision-ary-value
		loop
	loop ;

: draw-frog1 ( -- )
	frog_pos @ frog_pos cell+ @
	at-xy green ." #" ;

: draw-car1 { n1 n2 -- }
	8 0 u+do
		n1 i + n2 true set-collision-ary-value
	loop
	n1 n2 at-xy draw-car ;

: draw-cars ( -- )
	0 8 draw-car1 
	10 9 draw-car1
	20 11 draw-car1
	5 12 draw-car1
	;

: draw-log { n1 n2 -- }
	8 0 u+do
		n1 i + n2 false set-collision-ary-value
	loop
	n1 n2 at-xy draw-car ;

: draw-logs ( -- )
	8 1 draw-log 
	10 2 draw-log
	15 3 draw-log
	9 4 draw-log
	;

: check_collision ( -- flag )
	get-frog-pos get-collision-ary-value ;

: check-game-won ( -- flag )
	frog_pos cell+ @ 0 = ;

: end-game
    1 height 2 - at-xy
    reset-colors cr
    bye ;

: next-frog-pos { n1 n2 -- n3 } \ calculates the next frog position
	frog_pos cell n1 * + @ n2 + ;

: restore-scene { n1 n2 -- } \ TODO
	n1 n2 at-xy 
	n2 0 = n2 5 = n2 6 = n2 14 >= or or or if draw-gras endif
	n2 0 > n2 5 < and if draw-water endif
	n2 7 = n2 13 = or if white draw-dash endif
	n2 10 = if yellow draw-dash endif 
	n2 8 = n2 9 = n2 11 = n2 12 = or or or if draw-empty-space endif ;
	
: move-frog { n1 n2 -- } \ if n1=1 then up/down else left/right
	frog_pos @ frog_pos cell+ @ restore-scene

	\ keep frog within boundaries
	n1 1 = if \ up-down
		n1 n2 next-frog-pos 0 >=
		n1 n2 next-frog-pos level_height <=
		and if n1 n2 next-frog-pos frog_pos cell+ ! endif
	else \ left-right
		n1 n2 next-frog-pos 0 >=
		n1 n2 next-frog-pos width <
		and if n1 n2 next-frog-pos frog_pos ! endif
	endif ;

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
	n1 width mod n2 at-xy draw-car 
	reset-colors ;

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
		
		check_collision check-game-won or
		
	until
		0 height 2 - at-xy
		check-game-won if ." GAME WON" else ." GAME OVER" endif
		end-game ; 
	
