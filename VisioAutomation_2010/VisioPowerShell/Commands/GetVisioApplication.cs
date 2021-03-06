using System.Management.Automation;

namespace VisioPowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, VisioPowerShell.Commands.Nouns.VisioApplication)]
    public class GetVisioApplication : VisioCmdlet
    {
        protected override void ProcessRecord()
        {
            var app = this.Client.Application.Get();
            if (app == null)
            {
                this.WriteVerbose("A Visio Application Instance is NOT Attached");
                this.WriteObject(null);
            }
            else
            {
                this.WriteVerbose("A Visio Application Instance is Attached");
                this.WriteObject(app);
            }
        }
    }
}