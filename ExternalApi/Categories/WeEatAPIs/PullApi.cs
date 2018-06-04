using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using TableModel;
using Helps;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace WeEatAPIs
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var client = new RestClient("https://api.yelp.com/v3/categories");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "5cd8736b-2bc7-45a2-afb1-39ad3f30ee6e");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Bearer roEZmmapeiPzRYY4MC5Y5xgV4Jt8n-3ZeOgjsKfgle920o6UMGtyBj63YYB2EyQRxvs0PkxDVZ-Ser3s67diEdqgigrmA1d7YtMZ6zaI3AtLBdQIJamHS6CH_4TdWnYx");
            IRestResponse response = client.Execute(request);

            string content = response.Content;

            //DataTable dt =  (DataTable)JsonConvert.DeserializeObject(content);
            //DataTable dt = JsonStringToDataTable(content); 
            //DataTable dt = JsonToDataTable(content, content.Substring(content.IndexOf("{"),content.IndexOf(":")).Replace(@"'","") );
            //List<categories> UserList = JsonConvert.DeserializeObject<List<categories>>(content);
            //DataTable dTable = (DataTable)JsonConvert.DeserializeObject(content.Replace("{", "").Replace("}", ""));
            //ToDataTable(content);
            DataTable Output = ReadDataFromJson(content);
            //using (var command = new SqlCommand("InsertTable") { CommandType = CommandType.StoredProcedure })
            //{
            //    command.Parameters.Add(new SqlParameter("@myTableType", Output));
            //    SqlHelper.Exec(command);
            //}

        }
        public static void BlkInsert(string TableName, DataTable Table)
        {
            SqlConnection Connection = new SqlConnection("ConnectionString");
            //I assume you know better what is your connection string

            SqlDataAdapter adapter = new SqlDataAdapter("Select * from " + TableName, Connection);

            adapter.Fill(Table);
        }
        private static DataTable ReadDataFromJson(String jsonString)
        {
            var xd = new XmlDocument();
            jsonString = "{ \"Root_Node\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + @"} }";
            xd = JsonConvert.DeserializeXmlNode(jsonString);
            var result = new DataSet();
            result.ReadXml(new XmlNodeReader(xd));
            //Console.WriteLine(result.GetXml());
            //Console.WriteLine(result.GetXmlSchema());
            DataTable dt = result.Tables[0];
            return dt;
        }


        public static DataTable ToDataTable(string json)
        {


            //json = json.Replace("{", "").Replace("}", "");
            //json = json.Substring(json.IndexOf("alias") - 1, json.Length - (json.IndexOf("alias") - 1)).ToString();
            var dataSet = JsonConvert.DeserializeObject<DataSet>(json);
            var table = dataSet.Tables[0];



            //List<string> jsonList = new List<string>;

            //string[] jsonArray = json.Split("},{");
            

            var data = new DataTable();

            return data;
        }


        //public static DataTable ToDataTable<T>(this IList<T> data)
        //{
        //    PropertyDescriptorCollection props =
        //    TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}

        public class categories
        {
            public string alias { get; set; }
            public string title { get; set; }
            public string parent_aliases { get; set; }
            public string country_whitelist { get; set; }
            public string country_blacklist { get; set; }
        }


        //private static DataTable JsonStringToDataTable(string jsonString)
        //{
        //    DataTable dt = new DataTable();
        //    string[] jsonStringArray = Regex.Split(jsonString.Replace("{{","").Replace("categories",""), "},{");
        //    List<string> ColumnsName = new List<string>();
        //    foreach (string jSA in jsonStringArray)
        //    {
        //        string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
        //        foreach (string ColumnsNameData in jsonStringData)
        //        {
        //            try
        //            {
        //                int idx = ColumnsNameData.IndexOf(":");
        //                string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
        //                if (!ColumnsName.Contains(ColumnsNameString))
        //                {
        //                    ColumnsName.Add(ColumnsNameString);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
        //                //throw ex;
        //            }
        //        }
        //        break;
        //    }
        //    foreach (string AddColumnName in ColumnsName)
        //    {
        //        dt.Columns.Add(AddColumnName);
        //    }
        //    foreach (string jSA in jsonStringArray)
        //    {
        //        string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
        //        DataRow nr = dt.NewRow();
        //        foreach (string rowData in RowData)
        //        {
        //            try
        //            {
        //                int idx = rowData.IndexOf(":");
        //                string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
        //                string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
        //                nr[RowColumns] = RowDataString;
        //            }
        //            catch (Exception ex)
        //            {
        //                continue;
        //            }
        //        }
        //        dt.Rows.Add(nr);
        //    }
        //    return dt;
        //}

        //private static DataTable objToDataTable(object obj)
        //{
        //    DataTable dt = new DataTable();
        //    object objmkt = new object();
        //    dt.Columns.Add("Column_Name");
        //    foreach (PropertyInfo info in typeof(object).GetProperties())
        //    {
        //        dt.Rows.Add(info.Name);
        //    }
        //    dt.AcceptChanges();
        //    return dt;
        //}
        //public static DataTable ConvertStringToDatatable(string data)
        //{
        //    DataTable x = new DataTable("x");
        //    string[] columns = data.Split('\t');
        //    foreach (string dataColumn in columns)
        //    {
        //        x.Columns.Add(dataColumn);
        //    }
        //    DataRow dataRow = x.NewRow();
        //    /*foreach (string row in data)
        //    {
        //        string[] rowCell = row.Split('\t');
        //        x.Rows.Add(row.Split('\t'));
        //    }
        //    System.Console.WriteLine(x);*/
        //    return x;

        //}

    }


}
