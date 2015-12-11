require terminal.fs

\ Game dimensions
: width  tty-width ;
: height tty-height ;

\ Speed of cars / logs
1 constant min-speed
4 constant max-speed

\ Distance between cars / logs
4  constant min-separation
24 constant max-separation