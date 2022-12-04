using AdventOfCodeUtilities;

List<string> inputList = AoCUtilities.GetInputLines();
List<Instruction> Instructions = inputList.Select(x => new Instruction(x)).ToList();

void P1()
{
    int pc = 0;
    Dictionary<char, int> file = new Dictionary<char, int>() { { 'a', 0 }, { 'b', 0 } };

    while (pc < Instructions.Count)
    {
         Instructions[pc].Execute(ref pc, file);
    }

    Console.WriteLine(file['b']);
    Console.ReadLine();
}

void P2()
{
    int pc = 0;
    Dictionary<char, int> file = new Dictionary<char, int>() { { 'a', 1 }, { 'b', 0 } };

    while (pc < Instructions.Count)
    {
        Instructions[pc].Execute(ref pc, file);
    }

    Console.WriteLine(file['b']);
    Console.ReadLine();
}

P1();
P2();

public enum OpCode
{
    hlf,
    tpl,
    inc,
    jmp,
    jie,
    jio
}

public class Instruction
{
    public OpCode OpCode { get; set; }
    public char Operand = ' ';
    public int Offset = 0;
    public Instruction(string s)
    {
        string[] split = s.Split(' ');
        switch (split[0])
        {
            case "hlf":
                OpCode = OpCode.hlf; break;
            case "tpl":
                OpCode = OpCode.tpl; break;
            case "inc":
                OpCode = OpCode.inc; break;
            case "jmp":
                OpCode = OpCode.jmp; break;
            case "jie":
                OpCode = OpCode.jie; break;
            case "jio":
                OpCode = OpCode.jio; break;
            default:
                throw new NotImplementedException();
        }

        switch (OpCode)
        {
            case OpCode.hlf:
            case OpCode.tpl:
            case OpCode.inc:
                Operand = split[1][0]; break;
            case OpCode.jmp:
                Offset = int.Parse(split[1]); break;
            case OpCode.jie:
            case OpCode.jio:
                Operand = split[1][0];
                Offset = int.Parse(split[2]);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void Execute(ref int pc, Dictionary<char, int> file)
    {
        switch (OpCode)
        {
            case OpCode.hlf:
                file[Operand] /= 2; pc++; break;
            case OpCode.tpl:
                file[Operand] *= 3; pc++; break;
            case OpCode.inc:
                file[Operand] += 1; pc++; break;
            case OpCode.jmp:
                pc += Offset; break;
            case OpCode.jie:
                pc += file[Operand] % 2 == 0 ? Offset : 1; break;
            case OpCode.jio:
                pc += file[Operand] == 1 ? Offset : 1; break;
        }
    }
}