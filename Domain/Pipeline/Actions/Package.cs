using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pipeline.Actions;

public class Package : Action
{
    public override void Accept(IVisitor visitor)
    {
        visitor.VisitPackage(this);
    }

    public Package(string args) : base(args)
    {
    }
}