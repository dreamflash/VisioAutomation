using System.Collections.Generic;
using VisioAutomation.ShapeSheet.CellGroups;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioAutomation.Pages
{
    public class PageFormatCells : ShapeSheet.CellGroups.CellGroupSingleRow
    {
        public ShapeSheet.CellData DrawingScale { get; set; }
        public ShapeSheet.CellData DrawingScaleType { get; set; }
        public ShapeSheet.CellData DrawingSizeType { get; set; }
        public ShapeSheet.CellData InhibitSnap { get; set; }
        public ShapeSheet.CellData Height { get; set; }
        public ShapeSheet.CellData Scale { get; set; }
        public ShapeSheet.CellData Width { get; set; }
        public ShapeSheet.CellData ShadowObliqueAngle { get; set; }
        public ShapeSheet.CellData ShadowOffsetX { get; set; }
        public ShapeSheet.CellData ShadowOffsetY { get; set; }
        public ShapeSheet.CellData ShadowScaleFactor { get; set; }
        public ShapeSheet.CellData ShadowType { get; set; }
        public ShapeSheet.CellData UIVisibility { get; set; }
        public ShapeSheet.CellData DrawingResizeType { get; set; } // new in visio 2010

        public override IEnumerable<SrcFormulaPair> SrcFormulaPairs
        {
            get
            { 
                yield return this.newpair(ShapeSheet.SrcConstants.PageDrawingScale, this.DrawingScale.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageDrawingScaleType, this.DrawingScaleType.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageDrawingSizeType, this.DrawingSizeType.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageInhibitSnap, this.InhibitSnap.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageHeight, this.Height.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageScale, this.Scale.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageWidth, this.Width.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageShadowObliqueAngle, this.ShadowObliqueAngle.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageShadowOffsetX, this.ShadowOffsetX.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageShadowOffsetY, this.ShadowOffsetY.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageShadowScaleFactor, this.ShadowScaleFactor.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageShadowType, this.ShadowType.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageUIVisibility, this.UIVisibility.ValueF);
                yield return this.newpair(ShapeSheet.SrcConstants.PageDrawingResizeType, this.DrawingResizeType.ValueF);
            }
        }

        public static PageFormatCells GetCells(IVisio.Shape shape)
        {
            var query = PageFormatCells.lazy_query.Value;
            return query.GetCellGroup(shape);
        }

        private static readonly System.Lazy<PageFormatCellsReader> lazy_query = new System.Lazy<PageFormatCellsReader>();
    }
}