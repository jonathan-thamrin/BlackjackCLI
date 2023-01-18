using System;
using System.Collections.Generic;
using Blackjack.Interfaces;

namespace Blackjack.Test;

public class WriterFake : IWriter
{
    public List<string> StringBuffer { get;}

    public WriterFake()
    {
        StringBuffer = new List<string>();
    }
    
    public void Write(string output)
    {
        StringBuffer.Add(output);
    }

    public void WriteLine(string output)
    {
        StringBuffer.Add(output + "\n");
    }
}