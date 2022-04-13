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
        foreach(string arg in argv){

            Code.Run(arg.ToCharArray(), 0, 0);
        }
        return;
    }
}

