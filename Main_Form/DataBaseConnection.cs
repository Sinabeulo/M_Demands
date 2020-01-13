using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using DACLayer_DF;
//using DACLayer_DF.Querys;
//using NF_Component;

namespace Main_Form
{
    public partial class DataBaseConnection : UserControl
    {
        public DataBaseConnection()
        {
            InitializeComponent();
        }
        
        private void Test_btn_Click(object sender, EventArgs e)
        {
            /*
            Class1 ca = new Class1();
            
            lb_Test.Text = ca.TEST_Dapper(tb_Id.Text, tb_Pw.Text);
            */
        }

        private void btn_S_Click(object sender, EventArgs e)
        {
            /*
            // 프로시저 검색
            //using (IDbConnection connection = new SqlConnection($@"Data Source=192.16.1.104\MES_TEST, 10004;Initial Catalog=CAMMSYS_MES_TEST;User ID={id};Password={password}"))
            QueryManager queryManager = new QueryManager("datasource","initial","id","pw");
            //var result1 = queryManager.EmptyQueryToStringList("SELECT OBJECT_NAME(object_id) AS Text FROM sys.procedures ORDER BY OBJECT_NAME(object_id)");
            //var result1 = queryManager.EmptyQuery("sp_helptext sp_MATR_01_001");
            //var tempList = result1.Item2.ToList<string>();
            var result1 = queryManager.EmptyQuery_GetTable("select * from information_schema.tables");

            string path = Environment.CurrentDirectory;

            //foreach(var item in result1.Item2)
            //{
            //    if (item == "sp_COM_SendMsmqMessage") continue;
            //    var result2 = queryManager.EmptyQueryToStringList($"sp_helptext '{item}'");
            //    //if (result2.Item1 == false) continue;

            //    FileManager.SingleFileManager.FileWriter(path + "\\" + item + ".txt", result2.Item2.ToList());
            //}
            */
        }
        
    }
}
