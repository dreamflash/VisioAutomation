using System.Collections.Generic;
using VisioAutomation.ShapeSheet.CellGroups;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioAutomation.Shapes
{
    public class ControlCells : ShapeSheet.CellGroups.CellGroupMultiRow
    {
        public VisioAutomation.ShapeSheet.CellValueLiteral CanGlue { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral Tip { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral X { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral Y { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral YBehavior { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral XBehavior { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral XDynamics { get; set; }
        public VisioAutomation.ShapeSheet.CellValueLiteral YDynamics { get; set; }

        public override IEnumerable<SrcFormulaPair> SrcFormulaPairs
        {
            get
            {
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlCanGlue, this.CanGlue.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlTip, this.Tip.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlX, this.X.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlY, this.Y.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlYBehavior, this.YBehavior.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlXBehavior, this.XBehavior.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlXDynamics, this.XDynamics.Value);
                yield return SrcFormulaPair.Create(ShapeSheet.SrcConstants.ControlYDynamics, this.YDynamics.Value);
            }
        }

        public static List<List<ControlCells>> GetFormulas(IVisio.Page page, IList<int> shapeids)
        {
            var query = ControlCells.lazy_query.Value;
            return query.GetFormulas(page, shapeids);
        }

        public static List<List<ControlCells>> GetResults(IVisio.Page page, IList<int> shapeids)
        {
            var query = ControlCells.lazy_query.Value;
            return query.GetResults(page, shapeids);
        }


        public static List<ControlCells> GetFormulas(IVisio.Shape shape)
        {
            var query = ControlCells.lazy_query.Value;
            return query.GetFormulas(shape);
        }

        public static List<ControlCells> GetResults(IVisio.Shape shape)
        {
            var query = ControlCells.lazy_query.Value;
            return query.GetResults(shape);
        }

        private static readonly System.Lazy<ControlCellsReader> lazy_query = new System.Lazy<ControlCellsReader>();
    }
}