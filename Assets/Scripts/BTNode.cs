using System;
using System.Collections.Generic;

public enum NodeResult { Success, Failure, Running }

public abstract class BTNode
{
    public abstract NodeResult Evaluate();
}

// Tries its children in order and stops at the first one that does not fail
public class Selector : BTNode
{
    readonly List<BTNode> children;

    public Selector(params BTNode[] children)
    {
        this.children = new List<BTNode>(children);
    }

    public override NodeResult Evaluate()
    {
        foreach (BTNode child in children)
        {
            NodeResult result = child.Evaluate();
            if (result != NodeResult.Failure) return result;
        }
        return NodeResult.Failure;
    }
}

// Runs its children in order and stops at the first one that does not succeed
public class Sequence : BTNode
{
    readonly List<BTNode> children;

    public Sequence(params BTNode[] children)
    {
        this.children = new List<BTNode>(children);
    }

    public override NodeResult Evaluate()
    {
        foreach (BTNode child in children)
        {
            NodeResult result = child.Evaluate();
            if (result != NodeResult.Success) return result;
        }
        return NodeResult.Success;
    }
}

// A leaf that asks a yes or no question
public class ConditionNode : BTNode
{
    readonly Func<bool> check;

    public ConditionNode(Func<bool> check)
    {
        this.check = check;
    }

    public override NodeResult Evaluate()
    {
        return check() ? NodeResult.Success : NodeResult.Failure;
    }
}

// A leaf that does something and reports how it went
public class ActionNode : BTNode
{
    readonly Func<NodeResult> action;

    public ActionNode(Func<NodeResult> action)
    {
        this.action = action;
    }

    public override NodeResult Evaluate()
    {
        return action();
    }
}
