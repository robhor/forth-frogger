require terminal.fs
require settings.fs

: times { ex n -- }   \ Execute ex n times
    n 0 DO ex execute LOOP ;

: draw-gras reset-colors green ." ░" ;
: draw-water blue-bg ."  " ;
: draw-dash ." -" ;

: draw-water-line ['] draw-water width times ;
: draw-gras-line  ['] draw-gras  width times ;
: draw-dash-line ['] draw-dash width times ;

: draw-yellow-line black-bg yellow draw-dash-line ;
: draw-white-line  black-bg white  draw-dash-line ;

: draw-empty-space ."  " ;
: draw-empty-line ['] draw-empty-space width times ;
: draw-black-line black-bg draw-empty-line ;

: draw-rect { dx dy -- } \ Draws a rectangle at the current position with dimensions dx x dy
    dy 0 DO ['] draw-empty-space dx times LOOP ;

: draw-car red-bg 8 1 draw-rect ;

: draw-frog-space green-bg ."  " ;
: draw-frog
    1 move-right ['] draw-frog-space 2 times
    3 move-left 1 move-down
    ['] draw-frog-space 4 times ;

: draw-street
    black-bg
    draw-empty-line
    draw-empty-line
    yellow draw-dash-line
    draw-empty-line
    draw-empty-line
    ;