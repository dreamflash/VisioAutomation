using VisioAutomation.Shapes;
using VisioAutomation.ShapeSheet.Queries.Columns;
using SRCCON = VisioAutomation.ShapeSheet.SRCConstants;

namespace VisioAutomation.ShapeSheet.Queries.CommonQueries
{
    class XFormCellsQuery : CellGroupSingleRowQuery<VisioAutomation.Shapes.XFormCells,double>
    {
        public ColumnQuery Width { get; set; }
        public ColumnQuery Height { get; set; }
        public ColumnQuery PinX { get; set; }
        public ColumnQuery PinY { get; set; }
        public ColumnQuery LocPinX { get; set; }
        public ColumnQuery LocPinY { get; set; }
        public ColumnQuery Angle { get; set; }

        
        public XFormCellsQuery() 
        {
            this.PinX = this.query.AddCell(SRCCON.PinX, nameof(SRCCON.PinX));
            this.PinY = this.query.AddCell(SRCCON.PinY, nameof(SRCCON.PinY));
            this.LocPinX = this.query.AddCell(SRCCON.LocPinX, nameof(SRCCON.LocPinX));
            this.LocPinY = this.query.AddCell(SRCCON.LocPinY, nameof(SRCCON.LocPinY));
            this.Width = this.query.AddCell(SRCCON.Width, nameof(SRCCON.Width));
            this.Height = this.query.AddCell(SRCCON.Height, nameof(SRCCON.Height));
            this.Angle = this.query.AddCell(SRCCON.Angle, nameof(SRCCON.Angle));
        }

        public override XFormCells CellDataToCellGroup(CellData<double>[] row)
        {
            var cells = new Shapes.XFormCells
            {
                PinX = row[this.PinX],
                PinY = row[this.PinY],
                LocPinX = row[this.LocPinX],
                LocPinY = row[this.LocPinY],
                Width = row[this.Width],
                Height = row[this.Height],
                Angle = row[this.Angle]
            };
            return cells;
        }
    }
}