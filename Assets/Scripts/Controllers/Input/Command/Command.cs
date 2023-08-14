using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// consumes a input command and runs execute for the same.
/// Supported input commands:: mouse left/right click, Key "A,D" press.
/// </summary>
public class Command
{
    ~Command() { }
    public virtual void Execute() { }
}
