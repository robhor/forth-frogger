require terminal.fs

\ Game dimensions
: width  tty-width ;
: height tty-height ;

\ Speed of cars / logs
1 constant min-speed
3 constant max-speed

\ Car size
3  constant min-car-length
10 constant max-car-length

\ Distance between cars / logs
4  constant min-separation
24 constant max-separation

\ Keys
char w constant up-key
char a constant left-key
char s constant down-key
char d constant right-key
char q constant quit-key