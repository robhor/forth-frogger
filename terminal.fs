: csi 27 emit 91 emit ;

\ Color definitions

: black      ( -- ) csi ." 30m" ;
: red        ( -- ) csi ." 31m" ;
: green      ( -- ) csi ." 32m" ;
: yellow     ( -- ) csi ." 33m" ;
: blue       ( -- ) csi ." 34m" ;
: magenta    ( -- ) csi ." 35m" ;
: cyan       ( -- ) csi ." 36m" ;
: white      ( -- ) csi ." 37m" ;

: black-bg   ( -- ) csi ." 40m" ;
: red-bg     ( -- ) csi ." 41m" ;
: green-bg   ( -- ) csi ." 42m" ;
: yellow-bg  ( -- ) csi ." 43m" ;
: blue-bg    ( -- ) csi ." 44m" ;
: magenta-bg ( -- ) csi ." 45m" ;
: cyan-bg    ( -- ) csi ." 46m" ;
: white-bg   ( -- ) csi ." 47m" ;

: reset-colors ( -- ) csi ." 0m" ;

\ Controling the cursor

: move-up    ( n -- ) csi . ." A" ;
: move-down  ( n -- ) csi . ." B" ;
: move-right ( n -- ) csi . ." C" ;
: move-left  ( n -- ) csi . ." D" ;

\ Terminal info
: tty-width  ( -- w ) form swap drop ;
: tty-height ( -- h ) form drop ;
