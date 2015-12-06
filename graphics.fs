require terminal.fs
require settings.fs

: times { ex n -- }   \ Execute ex n times
    n 0 DO ex execute LOOP ;

: set-gras-color reset-colors green ;
: set-water-color blue-bg ;
: set-street-color reset-colors black-bg ;
: set-street-white-color reset-colors black-bg white ;
: set-street-yellow-color reset-colors black-bg yellow ;

: draw-gras set-gras-color ." ░" ;
: draw-water set-water-color ."  " ;
: draw-dash ." -" ;
: draw-white black-bg white ." -" ;
: draw-yellow black-bg yellow ." -" ;
: draw-empty-space ."  " ;

: draw-empty-line ['] draw-empty-space width times ;
: draw-water-line ['] draw-water width times ;
: draw-gras-line  ['] draw-gras  width times ;
: draw-dash-line ['] draw-dash width times ;
: draw-yellow-line black-bg yellow draw-dash-line ;
: draw-white-line  black-bg white  draw-dash-line ;
: draw-street-line set-street-color draw-empty-line ;

: draw-rect { dx dy -- } \ Draws a rectangle at the current position with dimensions dx x dy
    dy 0 DO ['] draw-empty-space dx times LOOP ;

: draw-car-size ( w h -- ) red-bg draw-rect ;
: draw-car red-bg 8 1 draw-rect ;

: draw-frog-space green-bg ."  " ;
: draw-frog
    1 move-right ['] draw-frog-space 2 times
    3 move-left 1 move-down
    ['] draw-frog-space 4 times ;

: draw-street black-bg ."  " ;
