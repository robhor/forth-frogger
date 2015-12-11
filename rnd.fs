
VARIABLE (RND)   
utime drop (rnd) ! \ seed 

: rnd ( -- n) 
    (rnd) @ dup 13 lshift xor 
    dup 17 rshift xor 
    dup DUP 5 lshift xor (rnd) ! 
;

: rnd-between { min max -- n }
    rnd max min - 1+ mod min + ;
