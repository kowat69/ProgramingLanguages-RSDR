class Code{
    public static Rsdr rsdr = new Rsdr();

    public static string[]? arg = null;

    private static char[] op = { '+', '-', '*', '/'};
    private static int line = 0;
    private static int bias = 0;
    private static bool successed = true;
    private static bool _continue = true;
    private static CodeData? define = null;
    private static List<CodeData> loopPos = new List<CodeData>();

    public static int Run(char[] str, int index, int _case){
        if(_continue){
            int strlen = str.Length;
            if(exist(str, index) && arg != null){
                bias = index;
                char s = str[bias];

                
                if(_case == 0){
                    bool preOp = (expect(str, bias - 1, op))? true: false;
                    switch(s){
                        case ' ':
                        break;
                        case '+':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Plus();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        if(preOp) return 0;
                        break;
                        case '-':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Minus();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        if(preOp) return 0;
                        break;
                        case '*':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Mult();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        if(preOp) return 0;
                        break;
                        case '/':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Div();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        if(preOp) return 0;
                        break;
                        case '=':
                        rsdr.Set(0);
                        break;
                        case '<':
                        rsdr.setPointer(rsdr.getPointer() - 1);
                        break;
                        case '>':

                        rsdr.setPointer(rsdr.getPointer() + 1);
                        break;
                        case ';':
                        rsdr.setPointer(0);
                        break;
                        case '.':
                        rsdr.print();
                        
                        break;
                        case ',':
                        rsdr.printChar();
                        
                        break;
                        case '[':
                        {
                            if(rsdr.Get() > 0){
                                loopPos.Add(new CodeData( line, bias));
                                int count = loopPos.Count;
                                if(count > 1){
                                    if(loopPos[count - 2].getRow() == loopPos[count - 1].getRow()){
                                        loopPos[count - 1].setLoopTime(loopPos[count - 2].getLoopTime() + 1);
                                    }
                                }
                            }else{
                                int co = 0;
                                for(int i = 1; exist(str, i + bias); i++){
                                    if(expect(str[bias + i], '[')){
                                        co++;
                                    }else if(expect(str[bias + i], ']')){
                                        if(co == 0){
                                            bias = bias + i;
                                            break;
                                        }
                                        co--;
                                    }
                                }
                            }
                        }
                        
                        break;
                        case ']':
                        {
                            int count = loopPos.Count;
                            int looptime = 0;
                            if(count > 0){
                                if(rsdr.Get() > 0){
                                    Code.Run(str, loopPos[count - 1].getRow(), 0);
                                }
                                looptime = loopPos[count - 1].getLoopTime();
                                loopPos.RemoveAt( count - 1);
                                
                                if(looptime > 0) return 0;
                            }else{
                                fault(str, line , bias, "require [");
                            }
                        }
                        break;
                        case '!':
                        if(bias == 0){
                            if(define == null){
                                define = new CodeData(line, bias);
//                                Console.WriteLine(line+" "+bias);
                            
                            }
                            else{
                                define.setLine(line);
                                define.setRow(bias);
 //                               Console.WriteLine(line+" "+bias);

                            }
                        }
                        return 0;
                        case ':':
                        if(define != null){
                            int _bias = bias;
                            Code.Run( arg[define.getLine()].ToCharArray(), define.getRow() + 1, 0);
                            bias = _bias;
                        }else{
                            fault( str, line, bias, "require !");
                        }
                        
                        break;
                        default:
                        fault(str, line, bias, "0:default" );
                        break;
                    }
                }
                /*else if( _case == 1){
                    switch(s){
                        case '+':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Plus();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 1);
                        }
                        break;
                        case '-':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Minus();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 1);
                        }
                        break;
                        case '*':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Mult();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 1);
                        }
                        break;
                        case '/':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Div();
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 1);
                        }
                        break;
                    }
                }
                else if( _case == 2){
                    switch(s){
                        case '+':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Set(1);
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        break;
                        case '-':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Set(-1);
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        break;
                        case '*':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Set(0);
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        break;
                        case '/':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Set(0);
                        if(expect(str, bias + 1, op)){
                            Code.Run(str, bias + 1, 0);
                        }
                        break;
                    }
                }*/
                else{
                    successed = false;
                }
                if(successed == false) fault(str, line, bias, "not successed" );
                Code.Run(str, bias + 1, 0);

                
                
            }else{
//                Console.WriteLine("count"+loopPos.Count);
            }
            if(index == 0){
                line++;
            }
        }
        return 0;
    }
    private static string GetAlphabet(char[] str){
        int pointer = bias;
        List<char> ret = new List<char>();
        for(;pointer < str.Length; pointer++){
            if(isAlphabet(str[pointer])) ret.Add(str[pointer]);
        }
        return new string(ret.ToArray());
    }
    public static void SetArg(string[] argv){
        arg = argv;
    }
    private static bool exist(char[] c, int num){
        if(c.Length > num) return true;
        return false;

    }
    private static bool isDight(string c){
        foreach(char _char in c.ToCharArray()){
            
            if( !(_char >= '0' && _char <= '9') ) return false;
            
        }
        return true;
    }private static bool isAlphabet(char c){
        if('a' <= c && 'z' >= c) return true;
        return false;
    }
    private static bool expect(char c, params char[] chars){
        foreach(char _char in chars){
            if(c == _char) return true;
        }
        return false;
    }
    private static bool expect(char[] c, int num, params char[] chars){
        if( num < 0) return false;
        if(c.Length <= num) return false;
        foreach(char _char in chars){
            if(c[num] == _char) return true;
        }
        return false;
    }
    public static void fault(char[] str, int line, int row){
        row++;
        line++;
        Console.WriteLine("Fault!:Line:{0}, Row:{1}", line, row );
        Console.WriteLine(str);
        for(int i = 0; i < row - 1; i++){
            Console.Write(" ");
        }
        Console.WriteLine("^");
        Environment.Exit(0);
    }
    public static void fault(char[] str, int line, int row, string error){
        row++;
        line++;
        Console.WriteLine("Fault!:Line:{0}, Row:{1} :{2}", line, row, error );
        Console.WriteLine(str);
        for(int i = 0; i < row - 1; i++){
            Console.Write(" ");
        }
        Console.WriteLine("^");
        Environment.Exit(0);
    }
}