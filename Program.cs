// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
class Program{
    public static void Main(string[] argv){
        if(argv.Length == 0){
            System.Console.WriteLine("fault");
            return;
        }
        Code.SetArg(argv);
/*
        foreach(string arg in argv){

            Code.Run(arg.ToCharArray(), 0, 0);
        }*/
        while(true){
            Code.Run( argv[Code.getLine()].ToCharArray()[Code.getRow()], Code.getRow(), 0);
            if( argv[Code.getLine()].ToCharArray().Length > Code.getRow()) continue;
            Code.setLine(Code.getLine() + 1);
            if(! (argv.Length > Code.getLine())) break;
        }
        return;
    }
}

