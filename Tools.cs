using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Huffman.Common
{
    public static class Tools
    {
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int Index);

        public static Bitmap CutImage(Image original, int StartX, int StartY, int iWidth, int iHeight)
        {
            try
            {
                if (original == null) throw new ArgumentNullException();

                int w = original.Width;
                int h = original.Height;

                if (StartX >= w || StartY >= h) throw new ArgumentOutOfRangeException();

                if (StartX + iWidth > w) iWidth = w - StartX;
                if (StartY + iHeight > h) iHeight = h - StartY;

                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);

                g.DrawImage(original, new Rectangle(0, 0, iWidth, iHeight),
                    new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch { return null; }
        }

        public static Bitmap ResizeImage(Image original, int newW, int newH)
        {
            try
            {
                if (original == null) throw new ArgumentNullException();

                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                // ��ֵ�㷨������
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, newW, newH),
                    new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch { return null; }
        }

        public static byte[] imageToByteArray(Image imageIn)
        {
            if (imageIn == null) return null;

            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Bmp);

            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null) return null;

            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);

            return returnImage;
        }

        public static int MillimetersToPixelsWidth(decimal length)
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(new Form().Handle);
            IntPtr hdc = g.GetHdc();

            int width = GetDeviceCaps(hdc, 4);
            int pixels = GetDeviceCaps(hdc, 8);

            g.ReleaseHdc(hdc);

            return (int)(((double)pixels / (double)width) * (double)length);
        }

        /// <summary> 
        /// ת������Ҵ�С��� 
        /// </summary> 
        /// <param name="num">��� </param> 
        /// <returns>���ش�д��ʽ </returns> 
        public static string NumToCNum(decimal num)
        {
            string str1 = "��Ҽ��������½��ƾ�";            //0-9����Ӧ�ĺ��� 
            string str2 = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�"; //����λ����Ӧ�ĺ��� 
            string str3 = "";    //��ԭnumֵ��ȡ����ֵ 
            string str4 = "";    //���ֵ��ַ�����ʽ 
            string str5 = "";  //����Ҵ�д�����ʽ 
            int i;    //ѭ������ 
            int j;    //num��ֵ����100���ַ������� 
            string ch1 = "";    //���ֵĺ������ 
            string ch2 = "";    //����λ�ĺ��ֶ��� 
            int nzero = 0;  //����������������ֵ�Ǽ��� 
            int temp;            //��ԭnumֵ��ȡ����ֵ 

            num = Math.Round(Math.Abs(num), 2);    //��numȡ����ֵ����������ȡ2λС�� 
            str4 = ((long)(num * 100)).ToString();        //��num��100��ת�����ַ�����ʽ 
            j = str4.Length;      //�ҳ����λ 
            if (j > 15) { return "���"; }
            str2 = str2.Substring(15 - j);  //ȡ����Ӧλ����str2��ֵ���磺200.55,jΪ5����str2=��ʰԪ�Ƿ� 

            //ѭ��ȡ��ÿһλ��Ҫת����ֵ 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //ȡ����ת����ĳһλ��ֵ 
                temp = Convert.ToInt32(str3);      //ת��Ϊ���� 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //����ȡλ����ΪԪ�����ڡ������ϵ�����ʱ 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "��" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //��λ�����ڣ��ڣ���Ԫλ�ȹؼ�λ 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "��" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //�����λ����λ��Ԫλ�������д�� 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //���һλ���֣�Ϊ0ʱ�����ϡ����� 
                    str5 = str5 + '��';
                }
            }
            if (num == 0)
            {
                str5 = "��Ԫ��";
            }
            return str5;
        }

        /// <summary>
        /// ����Сдת������
        /// </summary>
        /// <param name="decNum">��ת����Сд���</param>
        /// <returns>���ش�д�����ַ���</returns>
        public static string ToMoneyCN(decimal decNum)
        {
            decNum = Math.Round(decNum, 2);						//��������λС��
            string String1 = "��Ҽ��������½��ƾ�";			//���ֵĺ��ֶ����ַ���
            string String2 = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�";	//��λ�ĺ��ֶ����ַ���
            string String3 = Convert.ToString(decNum * 100);		//��ת����ֵ��100��ת��Ϊ�ַ���
            int intLen = Convert.ToString(decNum * 100).Length;	//��ת����ֵ��100����ַ�����
            String2 = String2.Substring(String2.Length - intLen, intLen);
            string String4;
            string CN1 = "", CN2 = "";
            int intZero = 0;

            string AtoC = "";

            for (int i = 0; i < intLen; i++)
            {
                String4 = String3.Substring(i, 1);
                if (i != (intLen - 3) && i != (intLen - 7) && i != (intLen - 11) && i != (intLen - 15))
                {
                    if (String4 == "0")
                    {
                        CN1 = "";
                        CN2 = "";
                        intZero++;
                    }
                    else if (String4 != "0" && intZero != 0)
                    {
                        CN1 = "��" + String1.Substring(int.Parse(String4), 1);
                        CN2 = String2.Substring(i, 1);
                        intZero = 0;
                    }
                    else
                    {
                        CN1 = String1.Substring(int.Parse(String4), 1);
                        CN2 = String2.Substring(i, 1);
                        intZero = 0;
                    }
                }
                else
                {
                    if (String4 != "0" && intZero != 0)
                    {
                        CN1 = "��" + String1.Substring(int.Parse(String4), 1);
                        CN2 = String2.Substring(i, 1);
                        intZero = 0;
                    }
                    else if (String4 != "0" && intZero == 0)
                    {
                        CN1 = String1.Substring(int.Parse(String4), 1);
                        CN2 = String2.Substring(i, 1);
                        intZero = 0;
                    }
                    else if (String4 == "0" && intZero >= 3)
                    {
                        CN1 = "";
                        CN2 = "";
                        intZero++;
                    }
                    else
                    {
                        CN1 = "";
                        CN2 = String2.Substring(i, 1);
                        intZero++;
                    }
                    if (i == (intLen - 10) || i == (intLen - 2)) CN2 = String2.Substring(i, 1);
                }

                AtoC += CN1 + CN2;
                if (i == intLen && String4 == "0") AtoC += "��";
            }
            if (decNum == 0) AtoC = "��Ԫ��";
            return AtoC;
        }

        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="PlainStr"></param>
        /// <param name="KeyStr"></param>
        /// <returns></returns>
        public static String EncryptStr(String PlainStr, String KeyStr)
        {
            Char[] PlainChar = PlainStr.ToCharArray();
            Int32 intLen = PlainChar.Length;
            Char[] KeyChar = KeyStr.ToCharArray();
            Char[] TmpChar = new Char[intLen];
            Char[] TmpChar1 = new Char[intLen];
            Int32 Pos = 0;

            for (Int32 i = 0; i < intLen; i++)
            {
                TmpChar[i] = Convert.ToChar((Int32)PlainChar[i] ^ (Int32)KeyChar[Pos]);
                Pos++;
                if (Pos == KeyChar.Length) Pos = 0;
            }

            if (intLen % 2 == 0)
            {
                Int32 MidLen = intLen / 2;
                for (Int32 i = 0; i < MidLen; i++)
                {
                    TmpChar1[i] = TmpChar[MidLen - i - 1];
                    TmpChar1[MidLen + i] = TmpChar[MidLen + MidLen - i - 1];
                }
            }
            return new String(TmpChar1);
        }

        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="PlainStr"></param>
        /// <param name="KeyStr"></param>
        /// <returns></returns>
        public static String DecryptStr(String PlainStr, String KeyStr)
        {
            Char[] PlainChar = PlainStr.ToCharArray();
            Int32 intLen = PlainChar.Length;
            Char[] KeyChar = KeyStr.ToCharArray();
            Char[] TmpChar = new Char[intLen];
            Char[] TmpChar1 = new Char[intLen];
            Int32 Pos = 0;

            if (intLen % 2 == 0)
            {
                Int32 MidLen = intLen / 2;
                for (Int32 i = 0; i < MidLen; i++)
                {
                    TmpChar[i] = PlainChar[MidLen - i - 1];
                    TmpChar[MidLen + i] = PlainChar[MidLen + MidLen - i - 1];
                }
            }

            for (Int32 i = 0; i < intLen; i++)
            {
                TmpChar1[i] = Convert.ToChar((Int32)TmpChar[i] ^ (Int32)KeyChar[Pos]);
                Pos++;
                if (Pos == KeyChar.Length) Pos = 0;
            }
            return new String(TmpChar1);
        }

        public static void CheckDatabase(string connectionString)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (connection.State != System.Data.ConnectionState.Open) throw new Exception();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }
        }

        public static bool TestDatabase(string connectionString)
        {
            bool result = false;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open) result = true;
            }
            catch
            {
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }

            return result;
        }

        public static bool TestOleDatabase(string connectionString)
        {
            bool result = false;
            System.Data.OleDb.OleDbConnection connection = null;

            try
            {
                connection = new System.Data.OleDb.OleDbConnection(connectionString);
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open) result = true;
            }
            catch
            {
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }

            return result;
        }

        /// <summary>
        /// ��WebForm����������ֵʱ��ת��ΪDBNULL�����ݡ�
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static object CStrToObj(String strValue)
        {
            if (strValue.Trim() == "") return DBNull.Value;
            return (object)strValue;
        }

        /// <summary>
        /// ��WebForm����������ֵʱ��ת��Ϊ�ֽ���ֵ
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static byte CStrToByte(String strValue)
        {
            if (strValue.Trim() == "") return 0;
            return byte.Parse(strValue);
        }

        /// <summary>
        /// ��WebForm����������ֵʱ��ת��Ϊ������ֵ
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static short CStrToSnt(String strValue)
        {
            if (strValue.Trim() == "") return 0;
            return short.Parse(strValue);
        }

        /// <summary>
        /// ��WebForm����������ֵʱ��ת��Ϊ����ֵ
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static int CStrToInt(String strValue)
        {
            if (strValue.Trim() == "") return 0;
            return int.Parse(strValue);
        }

        /// <summary>
        /// ��WebForm����������ֵʱ��ת��Ϊ��������ֵ
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static decimal CStrToDec(String strValue)
        {
            if (strValue.Trim() == "") return 0;
            return decimal.Parse(strValue);
        }

        /// <summary>
        /// ��WebForm����������ֵʱ��ת��Ϊ����������
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static DateTime CStrToDate(String strValue)
        {
            if (strValue.Trim() == "") return DateTime.MaxValue;
            return DateTime.Parse(strValue);
        }

        /// <summary>
        /// �����ݿ����ݱ��ȡ�ֶ�ֵʱ��ת��Ϊ�ַ�������
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static String CNullToStr(object objValue)
        {
            if (objValue != null && !Convert.IsDBNull(objValue)) return Convert.ToString(objValue);
            return "";
        }

        /// <summary>
        /// �����ݿ����ݱ��ȡ�ֶ�ֵʱ��ת��Ϊ����ֵ
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int CNullToInt(object objValue)
        {
            if (objValue != null && !Convert.IsDBNull(objValue)) return Convert.ToInt32(objValue);
            return 0;
        }

        /// <summary>
        /// �����ݿ����ݱ��ȡ�ֶ�ֵʱ��ת��Ϊ������ֵ
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static short CNullToSnt(object objValue)
        {
            if (objValue != null && !Convert.IsDBNull(objValue)) return Convert.ToInt16(objValue);
            return 0;
        }

        /// <summary>
        /// �����ݿ����ݱ��ȡ�ֶ�ֵʱ��ת��Ϊ8λ�޷�������ֵ
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static byte CNullToByte(object objValue)
        {
            if (objValue != null && !Convert.IsDBNull(objValue)) return Convert.ToByte(objValue);
            return 0;
        }

        /// <summary>
        /// �����ݿ����ݱ��ȡ�ֶ�ֵʱ��ת��Ϊ��������ֵ
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static decimal CNullToDec(object objValue)
        {
            if (objValue != null && !Convert.IsDBNull(objValue)) return Convert.ToDecimal(objValue);
            return 0;
        }

        /// <summary>
        /// �����ݿ����ݱ��ȡ�ֶ�ֵʱ��ת��Ϊ����������
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static DateTime CNullToDate(object objValue)
        {
            if (objValue != null && !Convert.IsDBNull(objValue)) return Convert.ToDateTime(objValue);
            return DateTime.MaxValue;
        }

        /// <summary>
        /// ���������ȡ�ַ���ֵ����ȡ���ȡ
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static String CCToEmpty(string objValue)
        {
            if (objValue == null) return "";
            return objValue.Replace("&nbsp;", " ").Replace("<br>", "\n");
        }

        /// <summary>
        /// ���������ȡ����ֵ��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static String CCToEmpty(int objValue)
        {
            if (objValue == 0) return "";
            return objValue.ToString();
        }

        /// <summary>
        /// ���������ȡ�޷���8λ����ֵ��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static String CCToEmpty(byte objValue)
        {
            if (objValue == 0) return "";
            return objValue.ToString();
        }

        /// <summary>
        /// ���������ȡ������ֵ��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static String CCToEmpty(decimal objValue)
        {
            if (objValue == 0) return "";
            return objValue.ToString();
        }

        /// <summary>
        /// ���������ȡ���������ݣ�ת��Ϊ�ַ���
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static String CCToEmpty(DateTime objValue)
        {
            if (objValue == DateTime.MaxValue || objValue == DateTime.MinValue) return "";
            return objValue.ToShortDateString();
        }

        /// <summary>
        /// �ж������������Ƿ�Ϊ�գ����Ϊ�գ�ת��ΪDBNull
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static object CDateToObj(DateTime objValue)
        {
            if (objValue == DateTime.MaxValue) return DBNull.Value;
            return (object)objValue;
        }

        /// <summary>
        /// �ж������Ƿ�Ϊ�գ������Ϊ�գ�ת��Ϊ�ַ���
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static string CObjToStr(Object objValue)
        {
            if (objValue != null && objValue != DBNull.Value) return Convert.ToString(objValue);
            return String.Empty;
        }

        /// <summary>
        /// ����������Ϣ������ϵͳ�¼���Ϣ��
        /// </summary>
        /// <param name="message">��Ϣ����</param>
        public static void ErrLog(String message, String source)
        {
            EventLog m_eventLog = null;

            // ȷ���Ѿ�����һ���Լ����¼�Դ
            if (!(EventLog.SourceExists(source)))
            {
                EventLog.CreateEventSource(source, "Application");
            }

            if (m_eventLog == null)
            {
                m_eventLog = new EventLog("Application");
                m_eventLog.Source = source;
            }

            m_eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Error);
        }

        //ʹ��ʱ������str��ǰ���*,��Ȼ����ǹ�Ƕ�������
        public static Image bar_code(string str, int barHeight)
        {
            string strTmp = ("*" + str + "*").ToString();
            strTmp = strTmp.ToLower();

            //������Ĳ�������ת���� 
            strTmp = strTmp.Replace("0", "_|_|__||_||_|");
            strTmp = strTmp.Replace("1", "_||_|__|_|_||");
            strTmp = strTmp.Replace("2", "_|_||__|_|_||");
            strTmp = strTmp.Replace("3", "_||_||__|_|_|");
            strTmp = strTmp.Replace("4", "_|_|__||_|_||");
            strTmp = strTmp.Replace("5", "_||_|__||_|_|");
            strTmp = strTmp.Replace("7", "_|_|__|_||_||");
            strTmp = strTmp.Replace("6", "_|_||__||_|_|");
            strTmp = strTmp.Replace("8", "_||_|__|_||_|");
            strTmp = strTmp.Replace("9", "_|_||__|_||_|");
            strTmp = strTmp.Replace("a", "_||_|_|__|_||");
            strTmp = strTmp.Replace("b", "_|_||_|__|_||");
            strTmp = strTmp.Replace("c", "_||_||_|__|_|");
            strTmp = strTmp.Replace("d", "_|_|_||__|_||");
            strTmp = strTmp.Replace("e", "_||_|_||__|_|");
            strTmp = strTmp.Replace("f", "_|_||_||__|_|");
            strTmp = strTmp.Replace("g", "_|_|_|__||_||");
            strTmp = strTmp.Replace("h", "_||_|_|__||_|");
            strTmp = strTmp.Replace("i", "_|_||_|__||_|");
            strTmp = strTmp.Replace("j", "_|_|_||__||_|");
            strTmp = strTmp.Replace("k", "_||_|_|_|__||");
            strTmp = strTmp.Replace("l", "_|_||_|_|__||");
            strTmp = strTmp.Replace("m", "_||_||_|_|__|");
            strTmp = strTmp.Replace("n", "_|_|_||_|__||");
            strTmp = strTmp.Replace("o", "_||_|_||_|__|");
            strTmp = strTmp.Replace("p", "_|_||_||_|__|");
            strTmp = strTmp.Replace("r", "_||_|_|_||__|");
            strTmp = strTmp.Replace("q", "_|_|_|_||__||");
            strTmp = strTmp.Replace("s", "_|_||_|_||__|");
            strTmp = strTmp.Replace("t", "_|_|_||_||__|");
            strTmp = strTmp.Replace("u", "_||__|_|_|_||");
            strTmp = strTmp.Replace("v", "_|__||_|_|_||");
            strTmp = strTmp.Replace("w", "_||__||_|_|_|");
            strTmp = strTmp.Replace("x", "_|__|_||_|_||");
            strTmp = strTmp.Replace("y", "_||__|_||_|_|");
            strTmp = strTmp.Replace("z", "_|__||_||_|_|");
            strTmp = strTmp.Replace("-", "_|__|_|_||_||");
            strTmp = strTmp.Replace("*", "_|__|_||_||_|");
            strTmp = strTmp.Replace("/", "_|__|__|_|__|");
            strTmp = strTmp.Replace("%", "_|_|__|__|__|");
            strTmp = strTmp.Replace("+", "_|__|_|__|__|");
            strTmp = strTmp.Replace(".", "_||__|_|_||_|");

            Image result = new Bitmap(strTmp.Length, barHeight);
            Graphics g = Graphics.FromImage(result);
            float linePos = 0;

            foreach (char c in strTmp.ToCharArray())
            {
                if (c == '_')
                {
                    g.DrawLine(new Pen(Color.White), linePos, 0, linePos, barHeight);
                }

                if (c == '|')
                {
                    g.DrawLine(new Pen(Color.Black), linePos, 0, linePos, barHeight);
                }

                linePos++;
            }

            //strTmp = strTmp.Replace("_", " <span  style='height:" + height + ";width:" + width + ";background:#FFFFFF;'> </span>");
            //strTmp = strTmp.Replace("|", " <span  style='height:" + height + ";width:" + width + ";background:#000000;'> </span>");

            return result;
        }

        public static void WaitingShow(string title, string message)
        {
            Huffman.Common.Views.LonelySplashViewer.Start(title, message);
        }

        public static void WaitingDone()
        {
            Huffman.Common.Views.LonelySplashViewer.Stop();
        }
    }
}
