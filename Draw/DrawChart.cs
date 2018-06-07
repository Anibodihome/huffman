using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Collections;

namespace Huffman.Common.Draw
{
    /// <summary>
    /// 图形生成程序
    /// </summary>
    public class DrawChart
    {
        public int[] arrCountNumber;//计算字段
        public string[] arrDisplayString;//显示字段
        public string strPictureTitle;//图片标题
        public int intImgState;//图片属性,如是饼图为宽度,如是条形图为高度

        //输入数据，生成图形
        public Stream GetData(string AnalysisGroup, string DisplayGroup, char SplitChar, string strTital, int intPicture, int DrawType)
        {
            Stream mypicstream = new MemoryStream();
            strPictureTitle = strTital;
            intImgState = intPicture;
            try
            {
                char[] chaSplit;
                chaSplit = new char[1];
                chaSplit[0] = Convert.ToChar(SplitChar);
                string[] arrAnalysisGroup = AnalysisGroup.Split(chaSplit);//拆分分析数据
                string[] arrDisplayGroup = DisplayGroup.Split(chaSplit);//拆分显示数据
                arrCountNumber = new int[arrAnalysisGroup.Length];
                arrDisplayString = new string[arrAnalysisGroup.Length];
                for (int i = 0; i < arrAnalysisGroup.Length; i++)
                {
                    arrCountNumber[i] = Convert.ToInt32(arrAnalysisGroup[i]);
                    arrDisplayString[i] = Convert.ToString(arrDisplayGroup[i]);
                }
                switch (DrawType)
                {
                    case 1:
                        mypicstream = Create_PieChart();
                        break;
                    case 2:
                        mypicstream = Create_BarChart();
                        break;
                }
            }
            catch (Exception strErr)
            {
                strPictureTitle = strErr.ToString();
                mypicstream = Create_ErrorImg();
            }
            return mypicstream;
        }

        //查询数据库，生成图形
        public Stream DataBading(string strConn, string strTableName, string intCountNumber, string strDisplayString, string strTital, int intPicture, int DrawType)
        {
            Stream mypicstream = new MemoryStream();
            strPictureTitle = strTital;
            intImgState = intPicture;
            try
            {
                //数据联接
                string strDataConn = strConn;
                SqlConnection Sql_Conn = new SqlConnection(strDataConn);
                Sql_Conn.Open();

                //得到数据
                string strSql = "SELECT " + intCountNumber + "," + strDisplayString + " FROM " + strTableName + " ORDER BY " + strDisplayString;
                SqlCommand Sql_Com = new SqlCommand(strSql, Sql_Conn);

                // 绑定DataSet
                DataSet ds = new DataSet();
                SqlDataAdapter Sql_Command = new SqlDataAdapter(Sql_Com);
                Sql_Command.Fill(ds);

                arrCountNumber = new int[ds.Tables[0].Rows.Count];
                arrDisplayString = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < arrCountNumber.Length; i++)
                {
                    arrCountNumber[i] = Convert.ToInt32(ds.Tables[0].Rows[i][intCountNumber]);
                    arrDisplayString[i] = Convert.ToString(ds.Tables[0].Rows[i][strDisplayString]);
                }
                // 关闭联接
                Sql_Conn.Close();
                switch (DrawType)
                {
                    case 1:
                        mypicstream = Create_PieChart();
                        break;
                    case 2:
                        mypicstream = Create_BarChart();
                        break;
                }
            }
            catch (Exception strErr)
            {
                strPictureTitle = strErr.ToString();
                mypicstream = Create_ErrorImg();
            }
            return mypicstream;
        }
        public Stream Create_ErrorImg()
        {
            Stream mystream = new MemoryStream();

            //设置字体,大小
            Font fontLegend = new Font("Verdana", 9),
                fontTitle = new Font("Verdana", 9, FontStyle.Bold);

            //创建一个Bitmap实例
            Bitmap objBitmap = new Bitmap(450, 30);
            Graphics objGraphics = Graphics.FromImage(objBitmap);

            SolidBrush blackBrush = new SolidBrush(Color.Black);

            //设置背景颜色为白色
            objGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, 450, 30);
            // 创建图的标题
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            objGraphics.DrawString(strPictureTitle, fontTitle, blackBrush, new Rectangle(0, 0, 450, 15), stringFormat);
            //保存图片
            objBitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Gif);
            return mystream;
            //摧毁对象
            //objGraphics.Dispose();
            //objBitmap.Dispose();
        }
        public Stream Create_PieChart()
        {
            Stream mystream = new MemoryStream();
            try
            {
                int chartWidth = intImgState;
                //得到数据的总和
                float folTotal = 0.0F, folTemp;
                int intLoop;
                for (intLoop = 0; intLoop < arrCountNumber.Length; intLoop++)
                {
                    folTemp = Convert.ToSingle(arrCountNumber[intLoop]);
                    folTotal += folTemp;
                }

                //设置字体,大小
                Font fontLegend = new Font("Verdana", 9),
                    fontTitle = new Font("Verdana", 10, FontStyle.Bold);

                //必须要设置图的大小,与标题
                const int bufferSpace = 15;//空出15
                int legendHeight = fontLegend.Height * (arrCountNumber.Length + 1) + bufferSpace;//图的高度
                int titleHeight = fontTitle.Height + bufferSpace;//标题高度
                int height = chartWidth + legendHeight + titleHeight + bufferSpace;//图片的总高
                int pieHeight = chartWidth;	//饼图的宽度为定义宽度

                //设置图片的总的大小
                Rectangle pieRect = new Rectangle(0, titleHeight, chartWidth, pieHeight);

                //设置饼图内的颜色
                ArrayList colors = new ArrayList();
                Random rnd = new Random(1);
                for (intLoop = 0; intLoop < arrCountNumber.Length; intLoop++)
                    colors.Add(new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))));

                float currentDegree = 0.0F;

                //创建一个Bitmap实例
                Bitmap objBitmap = new Bitmap(chartWidth, height);
                Graphics objGraphics = Graphics.FromImage(objBitmap);

                SolidBrush blackBrush = new SolidBrush(Color.Black);

                //设置背景颜色为白色
                objGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, chartWidth, height);

                for (intLoop = 0; intLoop < arrCountNumber.Length; intLoop++)
                {
                    objGraphics.FillPie((SolidBrush)colors[intLoop], pieRect, currentDegree, Convert.ToSingle(arrCountNumber[intLoop]) / folTotal * 360);
                    currentDegree += Convert.ToSingle(arrCountNumber[intLoop]) / folTotal * 360;
                }

                // 创建图的标题
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                objGraphics.DrawString(strPictureTitle, fontTitle, blackBrush, new Rectangle(0, 0, chartWidth, titleHeight), stringFormat);


                //创建饼图
                objGraphics.DrawRectangle(new Pen(Color.Black, 2), 0, height - legendHeight, chartWidth, legendHeight);
                for (intLoop = 0; intLoop < arrCountNumber.Length; intLoop++)
                {
                    objGraphics.FillRectangle((SolidBrush)colors[intLoop], 5, height - legendHeight + fontLegend.Height * intLoop + 5, 10, 10);
                    objGraphics.DrawString(((String)arrDisplayString[intLoop]) + " ---> " + (float)((int)(Convert.ToSingle(arrCountNumber[intLoop]) * 10000 / folTotal)) / 100 + "%", fontLegend, blackBrush, 20, height - legendHeight + fontLegend.Height * intLoop + 1);
                }

                //显示饼图
                objGraphics.DrawString("总计: " + Convert.ToString(folTotal), fontLegend, blackBrush, 5, height - fontLegend.Height - 5);

                //设置输出的图片类型
                //Response.ContentType = "image/gif";

                //保存图片
                objBitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Gif);
            }
            catch (Exception strErr)
            {
                strPictureTitle = strErr.ToString();
                mystream = Create_ErrorImg();
            }

            return mystream;
            //摧毁对象
            //objGraphics.Dispose();
            //objBitmap.Dispose();
        }

        public Stream Create_BarChart()
        {
            Stream mystream = new MemoryStream();
            try
            {
                int intChartHeight = intImgState;
                Pen rp = new Pen(Color.Black, 2);
                SolidBrush bru = new SolidBrush(Color.Purple);

                Font fonLegend = new Font("Verdana", 9), fontTitle = new Font("Verdana", 10, FontStyle.Bold);
                int inc = 40;//条形块的间距
                int max = 0;//最大值
                int cmp = 0, l = 0, y = intChartHeight - intChartHeight / 6, cmps = 0;
                int factor = 0, y3 = 0;
                float folTotal = 0.0F, folTemp;
                int fact = 1;
                int chartWidth = 0;
                if (Convert.ToInt32(arrCountNumber.Length) < 5)
                {
                    chartWidth = 180;
                }
                else
                {
                    chartWidth = (inc + 10) * (Convert.ToInt32(arrCountNumber.Length));
                }
                //取得最大值
                for (int j = 0; j < arrCountNumber.Length; j++)
                {
                    if (max < arrCountNumber[j])
                    {
                        max = arrCountNumber[j];
                    }
                    folTemp = Convert.ToSingle(arrCountNumber[j]);
                    folTotal += folTemp;
                }
                int x2 = chartWidth;//X轴终点
                //取得最大值座标值的最小值
                if (max % 5 != 0)
                {
                    cmp = max / 5 + 1;
                }
                else
                {
                    cmp = max / 5;
                }
                fact = cmp;
                int x1 = 40;//X轴起始点
                int y1 = 40;//Y1轴起始点
                int y2 = intChartHeight - intChartHeight / 6;//Y轴终点

                //取得数据中最大的数据与总和
                //设置图片输出大小
                Bitmap objBitmap = new Bitmap(chartWidth, intChartHeight);
                Graphics objGraphics = Graphics.FromImage(objBitmap);
                //设置输出图片背景颜色为白色
                objGraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, chartWidth, intChartHeight);

                //画条状图标题
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                SolidBrush blackBrush = new SolidBrush(Color.Black);
                int titleHeight = fontTitle.Height;
                objGraphics.DrawString(strPictureTitle, fontTitle, blackBrush, chartWidth / 3, 0);

                //生成座标值
                for (int k = 0; k <= 5; k++)
                {
                    l = k + 1;
                    cmps = cmp * l;
                    y = y - intChartHeight / 9;//座标Y轴间距
                    objGraphics.DrawString(Convert.ToString(cmps), fonLegend, bru, 0, y - 10);
                    objGraphics.DrawLine(rp, new Point(30, y), new Point(41, y));
                }

                l = x1;

                //plot the bar for each value in the array

                for (int m = 0; m < arrCountNumber.Length; m++)
                {
                    factor = (arrCountNumber[m] * intChartHeight / 9) / fact;
                    l += inc;
                    y3 = y2 - factor;
                    objGraphics.DrawRectangle(new Pen(Color.Black, 2), l - 10, y3, 10, factor);
                    objGraphics.FillRectangle(bru, l - 10, y3, 10, factor);
                    objGraphics.DrawString(Convert.ToString(arrCountNumber[m]), fonLegend, bru, l - 15, y3 - 20);
                    objGraphics.DrawString(Convert.ToString(arrDisplayString[m]), fonLegend, bru, l - 15, y2 + 10);
                }
                objGraphics.DrawString("总计: " + Convert.ToString(folTotal), fonLegend, new SolidBrush(Color.Red), x1 - 20, y2 + 30);

                //输出竖座标直线
                objGraphics.DrawLine(rp, new Point(x1, y1), new Point(x1, y2));
                //输出横座标直线
                objGraphics.DrawLine(rp, new Point(x1 - 1, y2), new Point(x2 + 20, y2));

                //设置图片输出格式
                //			Response.ContentType = "image/gif";

                //输出图片
                //			objBitmap.Save(Response.OutputStream, ImageFormat.Gif);
                objBitmap.Save(mystream, System.Drawing.Imaging.ImageFormat.Gif);
            }
            catch (Exception strErr)
            {
                strPictureTitle = strErr.ToString();
                mystream = Create_ErrorImg();
            }
            return mystream;
        }
    }
}
