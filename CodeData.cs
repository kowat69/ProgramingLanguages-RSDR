class CodeData{
    private int line;
    private int row;
    private int looptime = 0;
    public CodeData(int l, int r){
        line = l;
        row = r;
    }

    public int getLine(){
        return line;
    }
    public int getRow(){
        return row;
    }
    public int getLoopTime(){
        return looptime;
    }
    public void setLine(int l){
        line = l;
    }
    public void setRow(int r){
        row = r;
    }
    public void setLoopTime(int l){
        looptime = l;
    }
}