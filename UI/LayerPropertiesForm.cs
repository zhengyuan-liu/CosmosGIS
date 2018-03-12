using System.Windows.Forms;
using CosmosGIS.Map;
using CosmosGIS.Geometry;
using System.Drawing;
using System.Drawing.Text;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using CosmosGIS.MyRenderer;
using CosmosGIS.MySymbol;

namespace CosmosGIS.UI
{
    public partial class LayerPropertiesForm : Form
    {
        MyLayer slayer;
        MyLayer inputlayer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="layer">图层</param>
        internal LayerPropertiesForm(MyLayer layer)
        {
            inputlayer = layer;
            InitializeComponent();
            textBoxLayerName.Text = layer.LayerName;
            textBoxDescription.Text = layer.Description;
            MyRectangle bound = layer.SpaceData.Bound;
            textBoxTop.Text = bound.MaxY.ToString();
            textBoxBottom.Text = bound.MinY.ToString();
            textBoxRight.Text = bound.MaxX.ToString();
            textBoxLeft.Text = bound.MinX.ToString();
            if (layer.DataType != MySpaceDataType.MyGrid)
                listBoxDataSource.Items[0] = "Data Type: Feature Class";
            else
                listBoxDataSource.Items[0] = "Data Type: Raster Data";
            listBoxDataSource.Items[1] = "File Path: " + layer.FilePath;
            listBoxDataSource.Items[2] = "Geometry Type: " + layer.DataType;
            listBoxDataSource.Items[3] = "Geographic Coordinate System: " + "WGS_1984";

            if (layer.DataType != MySpaceDataType.MyGrid)
            {
                slayer = new MyLayer(layer.FilePath);
                InitializeLabel(layer);
                CopyRenderer(layer, slayer);
                ShowSymbols(slayer);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPageLabels);
                tabControl1.TabPages.Remove(tabPageSymbology);
            }
        }

        #region 注记相关控件函数

        private Font newfont;
        private Color newcolor;
        //显示字段列表
        private void InitializeLabel(MyLayer layer)
        {
            CB_LabelFields.Items.Clear();
            //添加字段
            int sFieldCount = layer.SpaceData.Property.FieldNum;
            foreach (string s in layer.SpaceData.Property.Fields.Keys)
            {
                CB_LabelFields.Items.Add(s);
            }         
            
            for (int i = 10; i < 64; i++)
            {
                CB_FontSize.Items.Add(i);
            }

            InstalledFontCollection MyFont = new InstalledFontCollection();
            FontFamily[] MyFontFamilies = MyFont.Families;
            int Count = MyFontFamilies.Length;
            for (int i = 0; i < Count; i++)
            {
                CB_FontStyle.Items.Add(MyFontFamilies[i].Name);
            }

            if (layer.HasText == true)
            {
                HasLabel.Checked = true;
                //HasTextInitial(layer);
            }
            else
                HasLabel.Checked = false;
            newfont = new Font("宋体", 12);
            newcolor = Color.Black;
                               
        }

        private void HasTextInitial(MyLayer layer)
        {
            CB_fields.Enabled = true;
            SQL.Enabled = true;
            TextStyleMessage.Enabled = true;
            if (layer.Textrender != null)
            {
                CB_LabelFields.SelectedItem = layer.Textrender.Field;
                if (layer.Textrender.HasSQL == true)
                {
                    HasSQL.Checked = true;
                    TB_SQL.Text = layer.Textrender.SQL;
                    newfont = layer.Textrender.TextSymol.TextStyle.TextFont;
                    newcolor = layer.Textrender.TextSymol.TextStyle.TextColor;
                }
                else
                {
                    HasSQL.Checked = false;
                }
            }
            else
            {
                CB_LabelFields.SelectedItem = CB_LabelFields.Items[0];
            }
            CB_FontStyle.SelectedItem = "宋体";
            CB_FontSize.SelectedItem = 12;
            btn_SelectColor.BackColor = Color.Black;
            
        }


        private void HasLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (HasLabel.Checked == true)
                HasTextInitial(inputlayer);
            else
            {
                CB_fields.Enabled =false;
                SQL.Enabled = false;
                TextStyleMessage.Enabled = false;
            }

        }
        /// <summary>
        /// 字体颜色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelectColor_Click(object sender, System.EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn_SelectColor.BackColor = colorDialog1.Color;
                TextShow.ForeColor = colorDialog1.Color;
                newcolor = TextShow.ForeColor;
            }
        }
        /// <summary>
        /// 字体加粗更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_B_Click(object sender, System.EventArgs e)
        {
            if (btn_B.BackColor == Color.White)
            {
                btn_B.BackColor = Color.Gray;
                if (TextShow.Font.Italic == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Bold | FontStyle.Italic);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Bold);
            }
            else
            {
                btn_B.BackColor = Color.White;
                if (TextShow.Font.Italic == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Italic);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Regular);

            }
            newfont = TextShow.Font;
        }
        /// <summary>
        /// 字体倾斜更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_I_Click(object sender, System.EventArgs e)
        {
            if (btn_I.BackColor == Color.White)
            {
                btn_I.BackColor = Color.Gray;
                if (TextShow.Font.Bold == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Italic|FontStyle.Bold);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Italic);
            }
            else
            {
                btn_I.BackColor = Color.White;
                if (TextShow.Font.Bold == true)
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Bold);
                else
                    TextShow.Font = new Font(TextShow.Font, FontStyle.Regular);
            }
            newfont = TextShow.Font;
        }
        /// <summary>
        /// 字体样式更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_FontStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CB_FontSize.SelectedItem != null)
            {
                TextShow.Font = new Font(CB_FontStyle.SelectedItem.ToString(), (float)Convert.ToDouble(CB_FontSize.SelectedItem.ToString()), TextShow.Font.Style);
                newfont = TextShow.Font;
            }
        }

        /// <summary>
        /// 字体大小更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_FontSize_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CB_FontStyle.SelectedItem != null)
            {
                TextShow.Font = new Font(CB_FontStyle.SelectedText, (float)Convert.ToDouble(CB_FontSize.SelectedItem.ToString()), TextShow.Font.Style);
                newfont = TextShow.Font;
            }
        }

        /// <summary>
        /// 设置SQL语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetSQL_Click(object sender, EventArgs e)
        {
            SelectByAttributesForm selectform = new SelectByAttributesForm(slayer.SpaceData.Property);
            selectform.ShowDialog();
            if (selectform.DialogResult == DialogResult.OK)
            {
                TB_SQL.Text = selectform.SQLstring;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (HasLabel.Checked == true)
            {
                inputlayer.HasText = true;
                TextSymbol newtextsymbol = new TextSymbol();
                newtextsymbol.TextStyle = new LetterStyle(newcolor, newfont);
                MyRenderer.TextRenderer newtextrenderer = new MyRenderer.TextRenderer(newtextsymbol, CB_LabelFields.SelectedItem.ToString());
                inputlayer.Textrender = newtextrenderer;
                if(HasSQL.Checked==true)
                {
                    inputlayer.Textrender.SQL = TB_SQL.Text;

                }
            }
            else
            {
                inputlayer.HasText = false;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (HasLabel.Checked == true)
            {
                inputlayer.HasText = true;
                TextSymbol newtextsymbol = new TextSymbol();
                newtextsymbol.TextStyle = new LetterStyle(newcolor, newfont);
                MyRenderer.TextRenderer newtextrenderer = new MyRenderer.TextRenderer(newtextsymbol, CB_LabelFields.SelectedItem.ToString());
                inputlayer.Textrender = newtextrenderer;
                if (HasSQL.Checked == true)
                {
                    inputlayer.Textrender.SQL = TB_SQL.Text;

                }
            }
            else
            {
                inputlayer.HasText = false;
            }
        }

        #endregion

        #region 符号选择页面相关控件设计

        private void CopyRenderer(MyLayer from,MyLayer to)
        {
            int count;
            switch (from.rendertype)
            {
                case RendererType.SimpleRenderer:
                    to.Renderer = new SimpleRenderer(to.DataType);
                    to.rendertype = RendererType.SimpleRenderer;
                    break;
                case RendererType.UniqueValueRenderer:
                    count = from.Renderer.SymbolCount;
                    List<string> stringvalues = new List<string>();
                    for (int i = 0; i < count; i++)
                        stringvalues.Add(((UniqueValueRenderer)from.Renderer).FindValue(i));
                    to.Renderer = new UniqueValueRenderer(to.DataType, ((UniqueValueRenderer)from.Renderer).Field, stringvalues);
                    to.rendertype = RendererType.UniqueValueRenderer;
                    break;
                case RendererType.ClassBreakRenderer:
                    to.Renderer = new ClassBreakRenderer(to.DataType, ((ClassBreakRenderer)from.Renderer).Field, ((ClassBreakRenderer)from.Renderer).Value);
                    to.rendertype = RendererType.ClassBreakRenderer;
                    break;
            }
            to.Renderer.CopyRenderer(from.Renderer);
        }

        //显示字段列表
        private void ShowFields(MyLayer layer)
        {
            CB_fields.Items.Clear();           //清除刷新
            CB_fields.BeginUpdate();            //让控件一次刷新而不逐条刷新
            //添加字段
            int sFieldCount = layer.SpaceData.Property.FieldNum;
            foreach(string s in layer.SpaceData.Property.Fields.Keys)
            {
                CB_fields.Items.Add(s);
            }
            CB_fields.EndUpdate();
            CB_fields.Refresh();
        }

        //显示符号
        private void ShowSymbols(MyLayer layer)
        {
            switch (layer.rendertype)
            {
                case MyRenderer.RendererType.SimpleRenderer:
                    //放置位图
                    radioButton1.Checked = true;                    
                    MyRenderer.SimpleRenderer mySrenderer = (MyRenderer.SimpleRenderer)layer.Renderer;
                    Bitmap sBitmap = mySrenderer.SymbolStyle.GetBitmap();
                    dataGridView1.Rows.Clear();
                    dataGridView1[0, 0].Value = sBitmap;
                    dataGridView1[1, 0].Value = layer.LayerName;
                    break;
                case MyRenderer.RendererType.UniqueValueRenderer:
                    radioButton2.Checked = true;
                    MyRenderer.UniqueValueRenderer myUrenderer = (MyRenderer.UniqueValueRenderer)layer.Renderer;
                  
                    int uValueCount = myUrenderer.SymbolCount;
                    dataGridView1.Rows.Clear();
                    dataGridView1.RowCount = uValueCount;
                    for (short i = 0; i < uValueCount; i++)
                    {
                        //放置位图
                        Bitmap uBitmap = myUrenderer.FindSymbolByIndex(i).GetBitmap();
                        dataGridView1[0, i].Value = uBitmap;
                        //显示值
                        
                        dataGridView1[1, i].Value = myUrenderer.FindValue(i);

                    }
                    break;
                case MyRenderer.RendererType.ClassBreakRenderer:
                    radioButton3.Checked = true;
                     MyRenderer.ClassBreakRenderer myCrenderer = (MyRenderer.ClassBreakRenderer)layer.Renderer;
                    int cValueCount = myCrenderer.SymbolCount;
                    dataGridView1.Rows.Clear();
                    dataGridView1.RowCount = cValueCount+1;
                    //dataGridView1[0, 0].Value =myCrenderer.ShowLinearGradient() ;
                    CB_color.Image= myCrenderer.ShowLinearGradient();
                    for (short i = 0; i < cValueCount; i++)
                    {
                        //放置位图
                        Bitmap cBitmap = myCrenderer.FindSymbolByIndex(i).GetBitmap();
                        dataGridView1[0, i].Value = cBitmap;
                        //显示值
                        if (i == 0)
                            dataGridView1[1, i].Value = myCrenderer.MinValue.ToString() + " - " + myCrenderer.Breaks[i].ToString();
                        else if (i != cValueCount - 1)
                            dataGridView1[1, i ].Value = myCrenderer.Breaks[i-1].ToString() + " - " + myCrenderer.Breaks[i].ToString();
                        else
                            dataGridView1[1, i].Value = myCrenderer.Breaks[i-1].ToString() + " - " + myCrenderer.MaxValue.ToString();
                    }
                    break;

            }
            if (CB_fields.Items.Count == 0)
                ShowFields(slayer);
            CB_fields.SelectedItem = slayer.Renderer.Field;
        }
                
        /// <summary>
        /// 字段更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_fields_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(slayer.rendertype)
            {
                case RendererType.SimpleRenderer:
                    break;
                case RendererType.UniqueValueRenderer:
                    string field_uvr = CB_fields.SelectedItem.ToString();
                    //获得属性唯一值
                    DataTable dt_uvr = slayer.SpaceData.Property.SqlQuery("Select Distinct " + field_uvr + " From " + slayer.LayerName);
                    List<string> values_uvr = new List<string>();
                    for (int j = 0; j < dt_uvr.Rows.Count; j++)
                    {
                        values_uvr.Add(dt_uvr.Rows[j][field_uvr].ToString());
                    }
                    slayer.rendertype = RendererType.UniqueValueRenderer;
                    slayer.Renderer = new UniqueValueRenderer(slayer.DataType, field_uvr, values_uvr);
                    ((UniqueValueRenderer)slayer.Renderer).Field = field_uvr;
                    ShowSymbols(slayer);
                    break;
                case RendererType.ClassBreakRenderer:
                    renderchanging = true;
                    string field_cbr = CB_fields.SelectedItem.ToString();
                    DataTable dt_cbr = slayer.SpaceData.Property.SqlQuery("Select Distinct " + field_cbr + " From " + slayer.LayerName);
                    try
                    {
                        List<double> values_cbr = new List<double>();
                        for (int j = 0; j < dt_cbr.Rows.Count; j++)
                        {
                            values_cbr.Add(Convert.ToDouble(dt_cbr.Rows[j][field_cbr]));
                        }
                        slayer.rendertype = RendererType.ClassBreakRenderer;
                        slayer.Renderer = new ClassBreakRenderer(slayer.DataType, field_cbr, values_cbr);
                        ((ClassBreakRenderer)slayer.Renderer).Field = field_cbr;
                        ShowSymbols(slayer);
                        renderchanging = false;
                        ShowBreaks();
                    }
                    //回退
                    catch
                    {
                        MessageBox.Show("错误，该字段不能使用分级渲染");
                        CB_fields.SelectedItem = ((ClassBreakRenderer)slayer.Renderer).Field;
                    }
                    break;
            }
        }
        /// <summary>
        /// 辅助回退标记
        /// </summary>
        bool renderchanging = false;
        /// <summary>
        /// 选择简单渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                if (radioButton1.Checked == true&&renderchanging==false)
                {
                    groupBox5.Visible = false;
                    //判断是否初次设定
                    if (CB_fields.Items.Count != 0)
                    {
                        slayer.rendertype = RendererType.SimpleRenderer;
                        slayer.Renderer = new SimpleRenderer(slayer.DataType);
                        ShowSymbols(slayer);
                    }
                }
                else
                {
                    renderchanging = false;
                }

        }
        /// <summary>
        /// 选择唯一值渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                groupBox5.Visible = false;
                //判断是否初次设定
                if (CB_fields.Items.Count != 0&&renderchanging==false)
                {
                    string field_uvr;
                    if (CB_fields.SelectedItem == null)
                    {
                        field_uvr = CB_fields.Items[0].ToString();
                        CB_fields.SelectedItem = CB_fields.Items[0];
                    }
                    else
                        field_uvr = CB_fields.SelectedItem.ToString();

                    //TODO 获得属性唯一值
                    DataTable dt_uvr = slayer.SpaceData.Property.SqlQuery("Select Distinct " + field_uvr + " From " + slayer.LayerName);
                    List<string> values_uvr = new List<string>();
                    for (int j = 0; j < dt_uvr.Rows.Count; j++)
                    {
                        values_uvr.Add(dt_uvr.Rows[j][field_uvr].ToString());
                    }
                    slayer.rendertype = RendererType.UniqueValueRenderer;
                    slayer.Renderer = new UniqueValueRenderer(slayer.DataType, field_uvr, values_uvr);
                    ShowSymbols(slayer);
                }
                else
                { renderchanging = false; }
            }
        }
        /// <summary>
        /// 选择分级渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                groupBox5.Visible = true;
                //判断是否初次设定
                if (CB_fields.Items.Count != 0&&renderchanging==false)
                {
                    renderchanging = true;
                    string field_cbr;
                    if (CB_fields.SelectedItem == null)
                    {
                        field_cbr = CB_fields.Items[0].ToString();
                        CB_fields.SelectedItem = CB_fields.Items[0];
                    }
                    else
                        field_cbr = CB_fields.SelectedItem.ToString();

                    List<double> values_cbr = new List<double>();
                    DataTable dt_cbr = slayer.SpaceData.Property.SqlQuery("Select Distinct " + field_cbr + " From " + slayer.LayerName);
                    try
                    {
                        
                        for (int j = 0; j < dt_cbr.Rows.Count; j++)
                        {
                            values_cbr.Add(Convert.ToDouble(dt_cbr.Rows[j][field_cbr]));
                        }
                        slayer.rendertype = RendererType.ClassBreakRenderer;
                        slayer.Renderer = new ClassBreakRenderer(slayer.DataType, field_cbr, values_cbr);
                        ShowSymbols(slayer);
                        renderchanging = false;
                        ShowBreaks();
                    }
                    //回退
                    catch
                    {
                        MessageBox.Show("错误，该字段不能使用分级渲染");
                        switch (slayer.rendertype)
                        {
                            case RendererType.SimpleRenderer:
                                radioButton1.Checked = true;
                                break;
                            case RendererType.UniqueValueRenderer:
                                radioButton2.Checked = true;
                                break;
                        }
                        groupBox5.Visible = false;
                        return;
                    }
                    
                }
            }
        }
        /// <summary>
        /// 更改断点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Changbreak_Click(object sender, EventArgs e)
        {
            int count = DG_break.RowCount - 1;
            ClassBreakRenderer renderer = (ClassBreakRenderer)slayer.Renderer;
            List<double> newbreak = new List<double>();
            newbreak.Add(renderer.MinValue);
            newbreak.Add(renderer.MaxValue);
            try
            {
                for (int i = 0; i < count; i++)
                {
                    double value = Convert.ToDouble(DG_break[1, i].Value);
                    if (value > renderer.MinValue && value < renderer.MaxValue)
                    {
                        newbreak.Add(value);
                    }
                }
                newbreak= newbreak.Distinct().ToList();
                newbreak.Sort();
                newbreak.RemoveAt(0);
                newbreak.RemoveAt(newbreak.Count - 1);

                if (RB_1.Checked == true)
                {
                    CB_color.Enabled = true;
                    ((ClassBreakRenderer)slayer.Renderer).Mode = 1;
                    ((ClassBreakRenderer)slayer.Renderer).ClassBreakChange(newbreak);
                    Bitmap newcolor = new Bitmap(CB_color.Image);
                    Color startcolor = newcolor.GetPixel(1, newcolor.Height / 2);
                    Color endcolor = newcolor.GetPixel(newcolor.Width - 1, newcolor.Height / 2);
                    ((ClassBreakRenderer)slayer.Renderer).SetColor(startcolor, endcolor);

                }
                else
                {
                    CB_color.Enabled = false;
                    ((ClassBreakRenderer)slayer.Renderer).Mode = 2;
                    ((ClassBreakRenderer)slayer.Renderer).ClassBreakChange(newbreak);
                }

                ShowSymbols(slayer);
                ShowBreaks();
            }
            catch
            {
                MessageBox.Show("错误，出现异常");
                ShowBreaks();
            }
        }
        /// <summary>
        /// 显示断点表格
        /// </summary>
        private void ShowBreaks()
        {
            
            ClassBreakRenderer renderer = (ClassBreakRenderer)slayer.Renderer;
            if (renderer.Mode == 1)
                RB_1.Checked = true;
            else
                RB_2.Checked = true;
            if (DG_break.RowCount == 0)
            {
                CB_color.Image = new Bitmap(((ClassBreakRenderer)slayer.Renderer).ShowLinearGradient(),CB_color.Size);
            }
            int sFieldCount = renderer.BreakCount;
            DG_break.RowCount = sFieldCount + 3;
            for(int i=0;i<sFieldCount+2;i++)
            {
                
                //显示值
                if (i == 0)
                {
                   DG_break[0, i ].Value = "Min";
                    DG_break[1, i].Value =renderer.MinValue;
                }
                else if (i != sFieldCount+1)
                {
                   DG_break[0, i].Value = i.ToString();
                   DG_break[1, i].Value =renderer.Breaks[i-1];
                }
                else
                {
                    DG_break[0, i].Value = "Max";
                    DG_break[1, i].Value = renderer.MaxValue;
                }
            }            
        }
        /// <summary>
        /// 修改断点表格更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_break_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            List<double> newbreak = new List<double>();
                try
                {
                    //DG_break.RowCount += 1;
                    for (int i = 0; i < DG_break.RowCount -1; i++)
                    {
                        newbreak.Add(Convert.ToDouble(DG_break[1, i].Value.ToString()));
                    }

                    DG_break[0, DG_break.RowCount - 2].Value = "Max";
                    DG_break[0, DG_break.RowCount - 3].Value = (DG_break.RowCount - 3).ToString();
                    newbreak.Sort();
                for (int i = 0; i < DG_break.RowCount-1; i++)
                    {
                        DG_break[1, i].Value = newbreak[i];
                    }
                }

                catch
                {
                    MessageBox.Show("错误，出现异常字符");
                }
            
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            inputlayer.Renderer = slayer.Renderer;
            inputlayer.rendertype = slayer.rendertype;
            this.DialogResult = DialogResult.Yes;
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            inputlayer.Renderer = slayer.Renderer;
            inputlayer.rendertype = slayer.rendertype;
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 设置单个符号弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex < dataGridView1.RowCount)
            {
                SymbolSelect select=new SymbolSelect();
                switch (slayer.rendertype)
                {
                    case RendererType.SimpleRenderer:
                        select = new SymbolSelect(slayer.DataType, ((SimpleRenderer)slayer.Renderer).SymbolStyle);
                        select.ShowDialog();
                        break;
                    case RendererType.UniqueValueRenderer:
                        select = new SymbolSelect(slayer.DataType, ((UniqueValueRenderer)slayer.Renderer).FindSymbolByIndex(e.RowIndex));
                        select.ShowDialog();
                        break;
                    case RendererType.ClassBreakRenderer:
                        select = new SymbolSelect(slayer.DataType, ((ClassBreakRenderer)slayer.Renderer).FindSymbolByIndex(e.RowIndex));
                        select.ShowDialog();
                        break;
                }
                if(select.DialogResult==DialogResult.OK)
                {
                    switch (slayer.rendertype)
                    {
                        case RendererType.SimpleRenderer:
                            ((SimpleRenderer)slayer.Renderer).SymbolStyle = select.NewSymbol;
                            break;
                        case RendererType.UniqueValueRenderer:
                            ((UniqueValueRenderer)slayer.Renderer).SetSymbol(e.RowIndex,select.NewSymbol);
                             break;
                        case RendererType.ClassBreakRenderer:
                            ((ClassBreakRenderer)slayer.Renderer).SetSymbol(e.RowIndex,select.NewSymbol);
                            break;
                    }                    
                    ShowSymbols(slayer);
                }
            }
        }


        #endregion

        private void CB_color_Click(object sender, EventArgs e)
        {
            Bitmap bit = ((ClassBreakRenderer)slayer.Renderer).ShowLinearGradient();
            LinerColorSelect select = new LinerColorSelect(bit);
            select.ShowDialog();
            if(select.DialogResult==DialogResult.OK)
            {
                CB_color.Image = new Bitmap( select.result,CB_color.Size);
            }
        }

        private void RB_1_CheckedChanged(object sender, EventArgs e)
        {
            CB_color.Enabled = true;
        }

        private void RB_2_CheckedChanged(object sender, EventArgs e)
        {
            CB_color.Enabled = false;
        }
    }
}
