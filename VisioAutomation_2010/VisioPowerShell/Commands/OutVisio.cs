using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using VA = VisioAutomation;
using Node = VisioAutomation.Models.Layouts.Tree.Node;

namespace VisioPowerShell.Commands
{
    [Cmdlet(VerbsData.Out, VisioPowerShell.Commands.Nouns.Visio)]
    public class OutVisio : VisioCmdlet
    {
        [Parameter(ParameterSetName = "orgchcart", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public VA.Models.Documents.OrgCharts.OrgChartDocument OrgChart { get; set; }

        [Parameter(ParameterSetName = "grid", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public VA.Models.Layouts.Grid.GridLayout GridLayout { get; set; }

        [Parameter(ParameterSetName = "directedgraph", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public List<VA.Models.Layouts.DirectedGraph.DirectedGraphLayout> DirectedGraphs { get; set; }

        [Parameter(ParameterSetName = "datatable", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public DataTable DataTable { get; set; }

        [Parameter(ParameterSetName = "datatable", Position = 1, Mandatory = true, ValueFromPipeline = true)]
        public double CellWidth { get; set; }

        [Parameter(ParameterSetName = "datatable", Position = 2, Mandatory = true, ValueFromPipeline = true)]
        public double CellHeight { get; set; }

        [Parameter(ParameterSetName = "datatable", Position = 3, Mandatory = true, ValueFromPipeline = true)]
        public double CellSpacing { get; set; }

        [Parameter(ParameterSetName = "piechart", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public VA.Models.Charting.PieChart PieChart;

        [Parameter(ParameterSetName = "barchart", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public VA.Models.Charting.BarChart BarChart;

        [Parameter(ParameterSetName = "areachart", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public VA.Models.Charting.AreaChart AreaChart;

        [Parameter(ParameterSetName = "systemxmldoc", Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public XmlDocument XmlDocument;

        protected override void ProcessRecord()
        {
            if (this.OrgChart != null)
            {
                this.Client.Draw.OrgChart(this.OrgChart);
            }
            else if (this.GridLayout != null)
            {
                this.Client.Draw.Grid(this.GridLayout);
            }
            else if (this.DirectedGraphs != null)
            {
                this.Client.Draw.DirectedGraph(this.DirectedGraphs);
            }
            else if (this.DataTable != null)
            {
                var widths = Enumerable.Repeat<double>(this.CellWidth, this.DataTable.Columns.Count).ToList();
                var heights = Enumerable.Repeat<double>(this.CellHeight, this.DataTable.Rows.Count).ToList();
                var spacing = new VisioAutomation.Geometry.Size(this.CellSpacing, this.CellSpacing);
                var shapes = this.Client.Draw.Table(this.DataTable, widths, heights, spacing);
                this.WriteObject(shapes);
            }
            else if (this.PieChart != null)
            {
                this.Client.Draw.PieChart(this.PieChart);
            }
            else if (this.BarChart != null)
            {
                this.Client.Draw.BarChart(this.BarChart);
            }
            else if (this.AreaChart != null)
            {
                this.Client.Draw.AreaChart(this.AreaChart);
            }
            else if (this.XmlDocument != null)
            {
                this.WriteVerbose("XmlDocument");
                var tree_drawing = new VA.Models.Layouts.Tree.Drawing();
                this.build_from_xml_doc(this.XmlDocument, tree_drawing);

                tree_drawing.Render(this.Client.Page.Get());
            }
            else
            {
                this.WriteVerbose("No object to draw");
            }
        }

        private void build_from_xml_doc(XmlDocument xml_document, VA.Models.Layouts.Tree.Drawing tree_drawing)
        {
            var n = new Node();
            tree_drawing.Root = n;
            n.Text = new VisioAutomation.Models.Text.Element(xml_document.Name);
            this.build_from_xml_element(xml_document.DocumentElement,n);

        }

        private void build_from_xml_element(XmlElement x, Node parent)
        {
            foreach (XmlNode xchild in x.ChildNodes)
            {
                if (xchild is XmlElement)
                {
                    var nchild = new Node();
                    nchild.Text = new VisioAutomation.Models.Text.Element(xchild.Name);

                    parent.Children.Add(nchild);
                    this.build_from_xml_element( (XmlElement) xchild, nchild);
                }
            }
        }
    }
}