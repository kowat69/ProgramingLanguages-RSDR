class Rsdr{
    static readonly int mem_MAX = 16;
    private int[] mem = new int[mem_MAX];
    private int pointer = 0;
    public Rsdr(){
        for(int i = 0; i < mem.Length; i++){
            mem[i] = 0;
        }
    }
    public void print(){
        Console.WriteLine(mem[pointer] + "   "+ pointer);
    }
    public void printChar(){
        Console.WriteLine((char)mem[pointer]);
    }
    public int Get(){
        return mem[pointer];
    }
    public int getPointer(){
        return pointer;
    }
    public void Set(int i){
        mem[pointer] = i;
    }
    public void setPointer(int p){
        if(mem_MAX > p && p > -1)
            pointer = p;
    }
    public void Plus(){
        mem[pointer]++;
    }
    public void Minus(){
        mem[pointer]--;
    }
    public void Mult(){
        mem[pointer] <<= 1;
    }
    public void Div(){
        mem[pointer] >>= 1;
    }
    public void Plus(int i){
        mem[pointer] += i;
    }
    public void Minus(int i){

        mem[pointer] -= i;
    }


}
