﻿using System.Management.Automation;

namespace VisioPowerShell.Commands
{
    [Cmdlet(VerbsDiagnostic.Test, VisioPowerShell.Commands.Nouns.VisioApplication)]
    public class TestVisioApplication: VisioCmdlet
    {
        protected override void ProcessRecord()
        {
            bool valid_app = this.Client.Application.Validate();
            this.WriteObject(valid_app);
        }
    }
}