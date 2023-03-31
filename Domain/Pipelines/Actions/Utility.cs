using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pipelines.Actions;

public class Utility : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitUtility(this);
    }

    public Utility(string args) : base(args)
    {
    }
}