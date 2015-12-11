require settings.fs
require graphics.fs
require scene.fs
require rnd.fs

create frog-pos 0 , 0 ,

: reset-frog-pos width 2 / frog-pos ! level-height 1- frog-pos cell+ ! ;

: save-car { x length line speed direction -- }
    x , line , length , speed , direction ,
    cars-length @ 1+ cars-length !
    ;

: generate-cars ( line -- )
    0 1 rnd-between 0 = if left else right endif
    min-speed max-speed rnd-between
    { line direction speed }
    
    0 \ currently used space on the street
    BEGIN
        min-car-length max-car-length rnd-between \ length of car
        ( used-space length )
        dup ( used-space length length )
        -rot ( length used-space length )
        + ( length used-space+length )
        dup width < ( length used-space fits )
        if 
            \ car fits, save it
            ( length used-space )
            2dup ( length used-space length x+length )
            swap - ( length used-space x )
            rot line speed direction ( used-space x length line speed direction )
            save-car ( used-space )

            \ add some free space after car
            min-separation max-separation rnd-between +
        else
            ( length used-space )
            swap drop ( used-space )
        endif

        ( used-space )
        dup width >
    UNTIL drop ;

: init-cars ( -- )
    scene-length @ 0 ?DO
        scene i cells + @ execute { draw-one draw-xt passable obstacles }
        obstacles cars = if
            \ generate cars!
            i generate-cars
        endif
    LOOP ;

CREATE cars-ary init-cars

: draw-background ( -- )
    scene-length @ 0 ?DO
        scene i cells + @ execute { draw-one draw-xt passable obstacles }
        0 i top-offset + at-xy draw-xt execute
    LOOP ;

: fetch-frog-pos ( -- x y )
    frog-pos @ frog-pos cell+ @ ;

: draw-frog1 ( -- )
    fetch-frog-pos at-xy green-bg white ." O" ;

: car-at-index ( car-index -- car-addr )
    5 * cells cars-ary + ;

: draw-cars ( -- )
    cars-length @ 0 ?DO
        i car-at-index ( car-addr )
        dup cell+ @ top-offset +  ( car-addr y )
        swap dup @ ( y car-addr x )
        rot at-xy ( car-addr )
        2 cells + @ 1 red-bg draw-rect
    LOOP ;

: draw-scene ( -- )
    draw-background
    draw-cars ;

: check-game-won ( -- flag )
    frog-pos cell+ @ 0 = ;

: collides-with-car ( car-index -- flag )
    fetch-frog-pos { frog-x frog-y }
    car-at-index
    dup cell+ @ ( car-addr y )
    frog-y = if
        \ frog and car on same line
        ( car-addr )
        dup @ ( car-addr x )
        swap 2 cells + @ ( x length )
        over + ( start-x end-x )
        frog-x > swap frog-x <= and
    else
        drop false
    endif ;

: frog-on-passable-environment ( -- flag )
    fetch-frog-pos nip
    cells scene + @ execute
    drop nip nip
    ;

: check-car-collision ( -- flag )
    false
    cars-length @ 0 ?DO
        i collides-with-car if
            drop true UNLOOP EXIT
        endif
    LOOP ;

: check-collision ( -- flag )
    frog-on-passable-environment
    check-car-collision
    = ;

: end-game
    1 height 2 - at-xy
    reset-colors cr
    bye ;

: next-frog-pos { n1 n2 -- n3 } \ calculates the next frog position
    frog-pos cell n1 * + @ n2 + ;

: restore-scene { x y -- }
    x y at-xy
    scene y cells + @ execute
    drop drop drop
    execute
    ;
    
: move-frog { n1 n2 -- } \ if n1=1 then up/down else left/right
    frog-pos @ frog-pos cell+ @ restore-scene

    \ keep frog within boundaries
    n1 1 = if \ up-down
        n1 n2 next-frog-pos 0 >=
        n1 n2 next-frog-pos level-height <
        and if n1 n2 next-frog-pos frog-pos cell+ ! endif
    else \ left-right
        n1 n2 next-frog-pos 0 >=
        n1 n2 next-frog-pos width <
        and if n1 n2 next-frog-pos frog-pos ! endif
    endif ;

: handle-key ( key -- )
    case key 
        up-key    of 1 -1 move-frog endof
        down-key  of 1  1 move-frog endof
        left-key  of 0 -1 move-frog endof
        right-key of 0  1 move-frog endof
        quit-key  of end-game endof
    endcase ;

: move-car { x y -- }
    x width mod y at-xy draw-car ;

: clear-car ( car-index -- )
    car-at-index
    dup @ ( car-addr x )
    swap dup cell+ @ ( x car-addr y )
    rot swap dup -rot at-xy ( car-addr y )
    cells scene + @ execute { draw-one draw-xt passable obstacles }
    2 cells + @ draw-one swap times ;

: move-cars { tick -- }
    cars-length @ 0 ?DO
        i car-at-index ( car-addr )
        dup 3 cells + @ ( car-addr speed )
        tick swap mod 0= if
            ( car-addr )
            i clear-car
            dup @ ( car-addr x )
            swap dup 4 cells + @ ( x car-addr direction )
            rot ( car-addr direction x )
            + ( car-addr x+direction )
            width mod
            swap !
        else
            drop
        endif
    LOOP ;

: hide-cursor width height at-xy ;

Defer game-over-menu

: start-game
    page
    draw-scene
    reset-colors
    reset-frog-pos
    0 scene-length @ at-xy ." controls: WASD to move frog; q to quit game"

    0 begin
        50 ms
        1+ \ increase tick counter
        
        key? if handle-key endif
        dup move-cars
        draw-cars
        draw-frog1
        hide-cursor

        check-collision check-game-won or
        
    until
    game-over-menu ; 

: draw-game-over-sign
    yellow-bg red 
    width 2 / 6 - scene-length @ 2 / 2dup 2dup
    1- at-xy ."            "
       at-xy ."  GAME OVER "
    1+ at-xy ."            " ;

:noname
    draw-game-over-sign
    reset-colors
    hide-cursor

    BEGIN
        key dup 13 = if clearstack start-game endif
        quit-key =
    UNTIL
    end-game ; IS game-over-menu
