require settings.fs
require graphics.fs

: end-game
    1 height 2 - at-xy
    reset-colors cr
    bye ;

: start-game
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
    draw-gras-line

    9 7 at-xy draw-car
    28 10 at-xy draw-car
    32 10 at-xy draw-car
    18 7 at-xy draw-frog
    48 13 at-xy draw-frog
    48 10 at-xy draw-frog
    48 7 at-xy draw-frog
    48 4 at-xy draw-frog
    48 1 at-xy draw-frog
    48 1 at-xy 1 move-right green-bg red ." xx"
    48 4 at-xy 1 move-right green-bg magenta ." ●●"
    ;