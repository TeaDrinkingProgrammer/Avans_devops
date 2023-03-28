using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pipeline.Actions;

public class Build : Action
{ 
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitBuild(this);
    }

    public Build(string args) : base(args)
    {
    }
}