using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGIS.MySymbol;

namespace CosmosGIS.UI
{
    public partial class SymbolSelect : Form
    {
        Symbol newsymbol;
        public Symbol NewSymbol;
        public SymbolSelect()
        {

        }
        public SymbolSelect(Map.MySpaceDataType symboltype,Symbol symbol)
        {
            switch(symboltype)
            {
                case  Map.MySpaceDataType.MyPoint:
                    newsymbol= (PointSymbol)symbol;
                    this.Controls.Add(new PointSymbolSelect((PointSymbol)newsymbol));
                    break;
                case  Map.MySpaceDataType.MyPolyLine:
                    newsymbol = (LineSymbol)symbol;
                    this.Controls.Add(new LineSymbolSelect((LineSymbol)newsymbol));
                    break;
                case Map.MySpaceDataType.MyPolygon:
                    newsymbol = (PolygonSymbol)symbol;
                    this.Controls.Add(new PolygonSymbolSelect((PolygonSymbol)newsymbol));
                    break;
            }
            InitializeComponent();
        }

        private void SymbolSelect_Load(object sender, EventArgs e)
        {

        }
    }
}
