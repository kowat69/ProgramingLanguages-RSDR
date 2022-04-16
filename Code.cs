class Code{
    public static Rsdr rsdr = new Rsdr();

    public static string[]? arg = null;

    private static char[] op = { '+', '-', '*', '/'};
    private static int line = 0;
    private static int row = 0;
    private static bool successed = true;
    private static bool _continue = true;

    private static CodeData? codeNowPos = null;
    private static CodeData? definePos = null;
//    private static bool isInDefine = false;
    private static List<CodeData> loopPos = new List<CodeData>();
    public static int Run(char str, int index, int _case){
        
        
        if(_continue){
            if(arg != null){
                char[] chars = arg[line].ToCharArray();
                row = index;
                
                if(_case == 0){
                    bool preOp = (expect(str,  op))? true: false;
                    switch(str){
                        case ' ':
                        break;
                        case '+':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Plus();
                        if(preOp) break;
                        break;
                        case '-':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Minus();
                        if(preOp) break;
                        break;
                        case '*':
    //                    return (expect(str, index + 1, '+', '-'))? 1 + Code.Run(str, index + 1, 1) : 1;
                        rsdr.Mult();
                        if(preOp) break;
                        break;
                        case '/':
    //                    return (expect(str, index + 1, '+', '-'))? -1 + Code.Run(str, index + 1, 1) : -1;
                        rsdr.Div();
                        if(preOp) break;
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
                                loopPos.Add(new CodeData( line, row));
                                int count = loopPos.Count;
                                if(count > 1){
                                    if(loopPos[count - 2].getRow() == loopPos[count - 1].getRow()){
                                        loopPos[count - 1].setLoopTime(loopPos[count - 2].getLoopTime() + 1);
                                    }
                                }
                            }else{
                                int co = 0;
                                for(int i = 1; exist(chars, i + row); i++){
                                    if(expect(chars[row + i], '[')){
                                        co++;
                                    }else if(expect(chars[row + i], ']')){
                                        if(co == 0){
                                            row = row + i;
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
//                                    System.Console.WriteLine("loop");
                                    row = loopPos[count - 1].getRow() - 1;
                                }
                                looptime = loopPos[count - 1].getLoopTime();
                                loopPos.RemoveAt( count - 1);
                                
                                if(looptime > 0) break;;
                            }else{
                                fault(chars, line , row, "require [");
                            }
                        }
                        break;
                        case '!':
                        if(row == 0){
                            if(definePos == null){
                                definePos = new CodeData(line, row);
//                                Console.WriteLine(line+" "+row);
                            
                            }
                            else{
                                definePos.setLine(line);
                                definePos.setRow(row);
 //                               Console.WriteLine(line+" "+row);

                            }
                        }
                        line++;
                        row = -1;
                        break;
                        case ':':
                        if(definePos != null){
                            if(codeNowPos == null) codeNowPos = new CodeData( line, row);
                            else {
                                codeNowPos.setLine(line);
                                codeNowPos.setRow(row);
                            }
                            int l = line;
                            int r = row;
                            Console.WriteLine("a");
                            line = definePos.getLine();
                            row = definePos.getRow();
                            row++;
                            while(true){
                                if(arg[definePos.getLine()].Length > row){
                                    Code.Run(arg[definePos.getLine()].ToCharArray()[row], row , 0);

                                }else{
                                    break;
                                }
                            }
//                            Console.WriteLine("times:"+i);
                            line = l;
                            row = r;
                        }else{
                            fault( chars, line, row, "require !");
                        }
                        
                        break;
                        default:
                        fault(chars, line, row, "0:default" );
                        break;
                    }
                }
                else{
                    successed = false;
                }
                if(successed == false) fault(chars, line, row, "not successed" );

                
                
            }else{
//                Console.WriteLine("count"+loopPos.Count);
            }
            
            Code.setRow(Code.getRow() + 1);
        }
        return 0;
    }
    public static int Run(char[] str, int index, int _case){
    
        return 0;
    }
    private static string GetAlphabet(char[] str){
        int pointer = row;
        List<char> ret = new List<char>();
        for(;pointer < str.Length; pointer++){
            if(isAlphabet(str[pointer])) ret.Add(str[pointer]);
        }
        return new string(ret.ToArray());
    }
    public static int getLine(){
        return line;
    }public static int getRow(){
        return row;
    }public static void setLine(int l){
        line = l;
    }public static void setRow(int r){
        row = r;
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