using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pipelines.Actions;

public class Test : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitTest(this);
    }

    public Test(string args) : base(args)
    {
    }
}