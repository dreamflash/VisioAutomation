namespace VisioAutomation.ShapeSheet.CellGroups
{
    public struct SrcFormulaPair
    {
        public readonly ShapeSheet.Src Src;
        public readonly string Formula;

        public SrcFormulaPair(ShapeSheet.Src src, string formula)
        {
            this.Src = src;
            this.Formula = formula;
        }

        public static SrcFormulaPair Create(ShapeSheet.Src src, string formula)
        {
            return new SrcFormulaPair(src,formula);
        }
    }
}