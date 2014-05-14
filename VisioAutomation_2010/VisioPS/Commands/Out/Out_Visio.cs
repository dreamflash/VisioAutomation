using System.Collections.Generic;
using System.Linq;
using VA = VisioAutomation;
using VAS = VisioAutomation.Scripting;
using SMA = System.Management.Automation;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioPS.Commands
{
    [SMA.Cmdlet(SMA.VerbsData.Out, "Visio")]
    public class Out_Visio : VisioPSCmdlet
    {
        [SMA.Parameter(ParameterSetName = "orgchcart", Position = 0, Mandatory = true)]
        public VA.Models.OrgChart.OrgChartDocument OrgChart { get; set; }

        [SMA.Parameter(ParameterSetName = "grid", Position = 0, Mandatory = true)]
        public VA.Models.Grid.GridLayout GridLayout { get; set; }

        [SMA.Parameter(ParameterSetName = "directedgraph", Position = 0, Mandatory = true)]
        public List<VA.Models.DirectedGraph.Drawing> DirectedGraphs { get; set; }

        [SMA.Parameter(ParameterSetName = "datatable", Position = 0, Mandatory = true)]
        public System.Data.DataTable DataTable { get; set; }

        [SMA.Parameter(ParameterSetName = "datatable", Position = 1, Mandatory = true)]
        public double CellWidth { get; set; }

        [SMA.Parameter(ParameterSetName = "datatable", Position = 2, Mandatory = true)]
        public double CellHeight { get; set; }

        [SMA.Parameter(ParameterSetName = "datatable", Position = 3, Mandatory = true)]
        public double CellSpacing { get; set; }

        [SMA.Parameter(ParameterSetName = "piechart", Position = 0, Mandatory = true)]
        public VA.Models.Charting.PieChart PieChart;

        [SMA.Parameter(ParameterSetName = "barchart", Position = 0, Mandatory = true)]
        public VA.Models.Charting.BarChart BarChart;

        [SMA.Parameter(ParameterSetName = "areachart", Position = 0, Mandatory = true)]
        public VA.Models.Charting.AreaChart AreaChart;

        [SMA.Parameter(ParameterSetName = "systemxmldoc", Position = 0, Mandatory = true)]
        public System.Xml.XmlDocument XmlDocument;

        protected override void ProcessRecord()
        {
            var scriptingsession = this.ScriptingSession;
            if (this.OrgChart != null)
            {
                scriptingsession.Draw.OrgChart(this.OrgChart);
            }
            else if (this.GridLayout != null)
            {
                scriptingsession.Draw.Grid(this.GridLayout);
            }
            else if (this.DirectedGraphs != null)
            {
                scriptingsession.Draw.DirectedGraph(this.DirectedGraphs);
            }
            else if (this.DataTable != null)
            {
                var widths = Enumerable.Repeat<double>(CellWidth, DataTable.Columns.Count).ToList();
                var heights = Enumerable.Repeat<double>(CellHeight, DataTable.Rows.Count).ToList();
                var spacing = new VA.Drawing.Size(CellSpacing, CellSpacing);
                var shapes = scriptingsession.Draw.Table(DataTable, widths, heights, spacing);
                this.WriteObject(shapes);
            }
            else if (this.PieChart != null)
            {
                scriptingsession.Draw.PieChart(this.PieChart);
            }
            else if (this.BarChart != null)
            {
                scriptingsession.Draw.BarChart(this.BarChart);
            }
            else if (this.AreaChart != null)
            {
                scriptingsession.Draw.AreaChart(this.AreaChart);
            }
            else if (this.XmlDocument != null)
            {
                this.WriteVerboseEx("XmlDocument");
                var tree_drawing = new VA.Models.Tree.Drawing();
                build(this.XmlDocument, tree_drawing);

                tree_drawing.Render(scriptingsession.Page.Get());
            }
            else
            {
                this.WriteVerboseEx("No object to draw");
            }
        }

        private void build(System.Xml.XmlDocument xmlDocument, VA.Models.Tree.Drawing tree_drawing)
        {
            var n = new VA.Models.Tree.Node();
            tree_drawing.Root = n;
            n.Text = new VA.Text.Markup.TextElement(xmlDocument.Name);
            this.build_ch(xmlDocument.DocumentElement,n);

        }

        private void build_ch(System.Xml.XmlElement x, VA.Models.Tree.Node parent)
        {
            foreach (System.Xml.XmlElement xchild in x.ChildNodes)
            {
                var nchild = new VA.Models.Tree.Node();
                nchild.Text = new VA.Text.Markup.TextElement(xchild.Name);

                parent.Children.Add(nchild);
                build_ch(xchild,nchild);

            }
        }

    }
}