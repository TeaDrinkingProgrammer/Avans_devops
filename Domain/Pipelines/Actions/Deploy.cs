using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pipelines.Actions;

public class Deploy : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitDeploy(this);
    }

    public Deploy(string args) : base(args)
    {
    }
}